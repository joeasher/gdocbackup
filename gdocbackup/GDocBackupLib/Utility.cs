/*
   Copyright 2009  Fabrizio Accatino

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Google.Documents;
using System.Security.Cryptography;
using System.IO;


namespace GDocBackupLib
{



    /// <summary>
    /// Misc utilities
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Add entroty to protected data
        /// </summary>
        private static readonly byte[] _protectDataExtraEntropy = new byte[] { 2, 92, 5, 20, 1, 19, 3, 2, 12, 7, 71, 5, 73 };


        /// <summary>
        /// Encrypts data using Data Protection API (DPAPI)
        /// </summary>
        public static string ProtectData(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            byte[] encryptedData = ProtectedData.Protect(dataBytes, _protectDataExtraEntropy, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }


        /// <summary>
        /// Decrypts data using Data Protection API (DPAPI)
        /// </summary>
        public static string UnprotectData(string encryptedData)
        {
            // if an administrator reset the user password, ProtectedData.Unprotect will throw an exception (by design)
            try
            {
                byte[] encryptedDataBytes = Convert.FromBase64String(encryptedData);
                byte[] dataBytes = ProtectedData.Unprotect(encryptedDataBytes, _protectDataExtraEntropy, DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(dataBytes);
            }
            catch (CryptographicException cex)
            {
                try
                {
                    List<String> cndLineArgs = new List<String>(Environment.GetCommandLineArgs());
                    if (cndLineArgs.Contains("-debug"))
                        File.AppendAllText("gdocbackup.debug.txt",
                            DateTime.Now.ToString() + Environment.NewLine +
                            "UnprotectData ERROR " + cex.ToString() + Environment.NewLine +
                            new String('-', 80) + Environment.NewLine);
                }
                catch { }
                return null;
            }
        }


        /// <summary>
        /// Creates an instance of a IWebProxy based on current configuration (app.config).
        /// </summary>
        /// <returns>An instance of WebProxy or null</returns>
        public static IWebProxy GetProxy(bool proxyExplicit, bool directConnection, string hostPortSource,
            string hostName, int portTcp, string authMode, string username, string password)
        {
            IWebProxy proxy = null;

            if (proxyExplicit)
            {
                if (directConnection)
                {
                    proxy = new WebProxy();    // empty webproxy --> direct connection
                }
                else
                {
                    ProxyHostPortSource phps = Utility.ParseEnum<ProxyHostPortSource>(hostPortSource);
                    switch (phps)
                    {
                        case ProxyHostPortSource.Default:
                            proxy = HttpWebRequest.DefaultWebProxy;
                            break;
                        case ProxyHostPortSource.HostPort:
                            proxy = new WebProxy(hostName, portTcp);
                            break;
                        default:
                            throw new ApplicationException("Not supported ProxyHostPortSource");
                    }

                    ProxyAuthMode proxyAuthMode = Utility.ParseEnum<ProxyAuthMode>(authMode);
                    switch (proxyAuthMode)
                    {
                        case ProxyAuthMode.NotAuthenticated:
                            proxy.Credentials = null;
                            break;
                        case ProxyAuthMode.DefaultCredential:
                            proxy.Credentials = CredentialCache.DefaultCredentials;   // was: proxy.UseDefaultCredentials = true;
                            break;
                        case ProxyAuthMode.UsernamePassword:
                            proxy.Credentials = new NetworkCredential(
                                username, Utility.UnprotectData(password));
                            break;
                        default:
                            throw new ApplicationException("Not supported proxyAuthMode");
                    }
                }
            }

            return proxy;
        }


        /// <summary>
        /// ...
        /// </summary>
        public static T ParseEnum<T>(string s)
        {
            // Sometimes Mono has problem (see Issue 25 - System.ArgumentException: An empty string is not considered a valid value.)
            // So, in case of error, ParseEnum returns the default value.
            // [THIS IS A TEMP WORKAROUND]
            try
            {
                return (T)Enum.Parse(typeof(T), s);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return default(T);
            }
        }


        /// <summary>
        /// Utility: ....
        /// </summary>
        public static List<Document.DownloadType> DecodeDownloadTypeArray(string s)
        {
            return DecodeDownloadTypeArray(s, '|');
        }


        /// <summary>
        /// Utility: ....
        /// </summary>
        public static List<Document.DownloadType> DecodeDownloadTypeArray(string s, char separator)
        {
            List<Document.DownloadType> list = new List<Document.DownloadType>();

            if (String.IsNullOrEmpty(s))
                return list;

            string[] tokens = s.Split(separator);
            if (tokens.Length == 0)
                return list;

            for (int i = 0; i < tokens.Length; i++)
                list.Add(Utility.ParseEnum<Document.DownloadType>(tokens[i]));
            return list;
        }



        /// <summary>
        /// Utility: ....
        /// </summary>
        public static string EncodeDownloadTypeArray(List<Document.DownloadType> list)
        {
            return String.Join("|", list.ConvertAll<String>(delegate(Document.DownloadType x) { return x.ToString(); }).ToArray());
        }



        /// <summary>
        /// Search for duplicates items in a List of Strings
        /// </summary>
        public static List<string> FindDuplicates(List<string> data)
        {
            List<string> outList = new List<string>();
            List<string> checkList = new List<string>();
            foreach (string docname in data)
                if (checkList.Contains(docname))
                    outList.Add(docname);
                else
                    checkList.Add(docname);
            return outList;
        }
    }



}
