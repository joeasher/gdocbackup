/*
   Copyright 2009-2012  Fabrizio Accatino

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
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using GDocBackupLib;


namespace GDocBackup
{

    /// <summary>
    /// Checks if an update is available
    /// </summary>
    internal class CheckUpdates
    {

        /// <summary>
        /// Gets the version of the last available release of GDocBackup from fhtino.appspot.com
        /// </summary>
        /// <param name="localVersion">Version of the running assembly</param>
        /// <param name="remoteVersion">Version of the latest version available</param>
        /// <param name="errorPresent">True if there was error getting the latest version number</param>
        public static bool Exec(out Version localVersion, out Version remoteVersion, out bool errorPresent)
        {
            try
            {
                remoteVersion = GetRemoteVersion(false);
                if (Properties.Settings.Default.CheckForBeta)
                {
                    Version betaVersion = GetRemoteVersion(true);
                    if (betaVersion > remoteVersion)
                        remoteVersion = betaVersion;
                }

                localVersion = Assembly.GetExecutingAssembly().GetName().Version;
                errorPresent = false;

                return (remoteVersion > localVersion);
            }
            catch (Exception)
            {
                // ignore exception
                errorPresent = true;
                localVersion = null;
                remoteVersion = null;
                return false;
            }
        }


        private static Version GetRemoteVersion(bool beta)
        {
            Properties.Settings conf = Properties.Settings.Default;

            IWebProxy webproxy = Utility.GetProxy(conf.ProxyExplicit, conf.ProxyDirectConnection, conf.ProxyHostPortSource, conf.ProxyHost, conf.ProxyPort, conf.ProxyAuthMode, conf.ProxyUsername, conf.ProxyPassword);

            string url = "http://fhtino.appspot.com/getinformation?code=gdocbackupver" + (beta ? "beta" : "");

            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Timeout = 6000;   // wait max 6 seconds.
            if (webproxy != null)
                req.Proxy = webproxy;
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            string text = null;
            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                    text = sr.ReadToEnd();
            }

            return new Version(text);
        }


        /*
        /// <summary>
        /// Gets the version of the last available release of GDocBackup form gs.fhtino.it
        /// </summary>
        /// <param name="localVersion">Version of the running assembly</param>
        /// <param name="remoteVersion">Version of the latest version available</param>
        /// <param name="errorPresent">True if there was error getting the latest version number</param>
        public static bool Exec_OLD_NOTUSED(out Version localVersion, out Version remoteVersion, out bool errorPresent)
        {
            try
            {
                Properties.Settings conf = Properties.Settings.Default;

                IWebProxy webproxy = Utility.GetProxy(conf.ProxyExplicit, conf.ProxyDirectConnection, conf.ProxyHostPortSource, conf.ProxyHost, conf.ProxyPort, conf.ProxyAuthMode, conf.ProxyUsername, conf.ProxyPassword);

                HttpWebRequest req = HttpWebRequest.Create("http://gs.fhtino.it/gdocbackup/lastversion?GDBCheckVer=1") as HttpWebRequest;
                req.Timeout = 3000;   // wait max 3 seconds.
                if (webproxy != null)
                    req.Proxy = webproxy;
                HttpWebResponse res = req.GetResponse() as HttpWebResponse;

                string text = null;
                using (Stream stream = res.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                        text = sr.ReadToEnd();
                }

                // Sample:   ###LASTVERSION=0.0.9###
                Regex regex = new Regex("###LASTVERSION=(?<version>(.)*)###");
                Match match = regex.Match(text);
                bool newVersionAvailable = false;
                if (match.Success)
                {
                    string s = match.Result("${version}");
                    remoteVersion = new Version(s);
                    localVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    newVersionAvailable = (remoteVersion > localVersion);
                }
                else
                {
                    localVersion = null;
                    remoteVersion = null;
                    newVersionAvailable = false;
                }
                errorPresent = false;
                return newVersionAvailable;
            }
            catch (Exception)
            {
                // ignore exception
                errorPresent = true;
                localVersion = null;
                remoteVersion = null;
                return false;
            }
        }
        */
    }
}
