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
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;


namespace GDocBackup
{

    /// <summary>
    /// Checks if an update is available
    /// </summary>
    internal class CheckUpdates
    {

        /// <summary>
        /// Gets the version of the last available release of GDocBackup form gs.fhtino.it
        /// </summary>
        public static bool Exec(out Version localVersion, out Version remoteVersion)
        {
            try
            {
                HttpWebRequest req = HttpWebRequest.Create("http://gs.fhtino.it/gdocbackup/lastversion?GDBCheckVer=1") as HttpWebRequest;
                req.Timeout = 3000;   // wait max 3 seconds.
                req.Proxy = Utility.GetProxy();
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
                if (match.Success)
                {
                    string s = match.Result("${version}");
                    remoteVersion = new Version(s);
                    localVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    return (remoteVersion > localVersion);
                }
            }
            catch (Exception)
            {
                // ignore exception
            }

            localVersion = null;
            remoteVersion = null;
            return false;
        }
    }
}
