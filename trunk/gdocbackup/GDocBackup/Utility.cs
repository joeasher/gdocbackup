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
using System.Security.Cryptography;
using System.Net;


namespace GDocBackup
{

    /// <summary>
    /// Misc utilities
    /// </summary>
    internal class Utility
    {

        /// <summary>
        /// Add entroty to protected data
        /// </summary>
        private static readonly byte[] _protectDataExtraEntropy = new byte[] { 2, 92, 5, 20, 1, 19, 3, 2, 12, 7, 71, 5, 73 };


        /// <summary>
        /// ...
        /// </summary>
        public static string ProtectData(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            byte[] encryptedData = ProtectedData.Protect(dataBytes, _protectDataExtraEntropy, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }


        /// <summary>
        /// ...
        /// </summary>
        public static string UnprotectData(string encryptedData)
        {
            byte[] encryptedDataBytes = Convert.FromBase64String(encryptedData);
            byte[] dataBytes = ProtectedData.Unprotect(encryptedDataBytes, _protectDataExtraEntropy, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(dataBytes);
        }


        /// <summary>
        /// Creates an instance of WebProxy based on current configuration (app.config).
        /// </summary>
        /// <returns>An instance of WebProxy or null</returns>
        public static IWebProxy GetProxy()
        {
            Properties.Settings conf = Properties.Settings.Default;

            IWebProxy proxy = null;

            if (conf.ProxyExplicit)
            {
                if (conf.ProxyDirectConnection)
                {
                    proxy = new WebProxy();    // empty webproxy --> direct connection
                }
                else
                {
                    ProxyHostPortSource phps = Utility.ParseEnum<ProxyHostPortSource>(conf.ProxyHostPortSource);
                    switch (phps)
                    {
                        case ProxyHostPortSource.Default:
                            proxy = HttpWebRequest.DefaultWebProxy;   // was: WebProxy.GetDefaultProxy();  // OBSOLETE!!! ... da modificare!!!               
                            break;
                        case ProxyHostPortSource.HostPort:
                            proxy = new WebProxy(conf.ProxyHost, conf.ProxyPort);
                            break;
                        default:
                            throw new ApplicationException("Not supported ProxyHostPortSource");
                    }

                    ProxyAuthMode proxyAuthMode = Utility.ParseEnum<ProxyAuthMode>(conf.ProxyAuthMode);
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
                                conf.ProxyUsername, Utility.UnprotectData(conf.ProxyPassword));
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
            return (T)Enum.Parse(typeof(T), s);
        }

    }

}
