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
using GDocBackupLib;
using System.IO;
using Google.Documents;
using System.Reflection;


namespace GDocBackupCMD
{
    class Program
    {
        static int Main(string[] args)
        {
            AssemblyName assembName = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine("\n" + assembName.Name + " - ver. " + assembName.Version.ToString() + " - " + "http://gs.fhtino.it/gdocbackup" + "\n\n");


            Dictionary<string, string> parameters = ParseParameters(args);

            if (!parameters.ContainsKey("mode"))
                parameters.Add("mode", "help");


            if (parameters["mode"] == "encodepassword")
            {
                if (parameters.ContainsKey("password"))
                    Console.WriteLine(GDocBackupLib.Utility.ProtectData(parameters["password"]));
                return 0;
            }


            if (parameters["mode"] == "backup")
            {
                bool resOK;
                try
                {
                    resOK = DoBackup(parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("INTERNAL ERROR: " + ex.Message);
                    Console.WriteLine(new String('-', 40));
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(new String('-', 40));
                    resOK=false;
                }

                if (resOK)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("\n\n****** WARNING: there are errrors! ******");
                    return 1;
                }
            }

            ShowUsage();
            return 0;
        }




        private static bool DoBackup(Dictionary<string, string> parameters)
        {
            // Get username
            string username = parameters.ContainsKey("username") ? parameters["username"] : null;
            if (username == null)
                throw new ApplicationException("Empty username");

            // Get password
            string password = null;
            if (parameters.ContainsKey("password"))
                password = parameters["password"];
            if (parameters.ContainsKey("passwordEnc"))
                password = Utility.UnprotectData(parameters["passwordEnc"]);
            if (parameters.ContainsKey("passwordEncFile"))
                password = Utility.UnprotectData(File.ReadAllText(parameters["passwordEncFile"]));
            if (password == null)
                throw new ApplicationException("Empty password");

            // Get destDir
            string destDir = parameters.ContainsKey("destDir") ? parameters["destDir"] : null;
            if (destDir == null)
                throw new ApplicationException("Empty destDir");

            // Get document export format
            string docF = parameters.ContainsKey("docF") ? parameters["docF"] : null;
            string sprsF = parameters.ContainsKey("sprsF") ? parameters["sprsF"] : null;
            string presF = parameters.ContainsKey("presF") ? parameters["presF"] : null;
            if (docF == null) throw new ApplicationException("Empty docF");
            if (sprsF == null) throw new ApplicationException("Empty sprsF");
            if (presF == null) throw new ApplicationException("Empty presF");
            List<Document.DownloadType> docTypes = Utility.DecodeDownloadTypeArray(docF, '+');
            List<Document.DownloadType> sprsTypes = Utility.DecodeDownloadTypeArray(sprsF, '+');
            List<Document.DownloadType> presTypes = Utility.DecodeDownloadTypeArray(presF, '+');

            // Output parameters
            Console.WriteLine(new String('-', 40));
            Console.WriteLine("Parameters: ");
            Console.WriteLine("Username:     " + username);
            Console.WriteLine("Password:     " + "[hidden]");
            Console.WriteLine("DestDir:      " + destDir);
            Console.WriteLine("Document:     " + docF);
            Console.WriteLine("Spreadsheet:  " + sprsF);
            Console.WriteLine("Presentation: " + presF);
            Console.WriteLine(new String('-', 40));

            // Exec backup
            Backup backup = new Backup(username, password, destDir, docTypes.ToArray(), sprsTypes.ToArray(), presTypes.ToArray(), null);
            backup.Feedback += new EventHandler<FeedbackEventArgs>(backup_Feedback);
            bool resOK = backup.Exec();

            return resOK;
        }


        private static void backup_Feedback(object sender, FeedbackEventArgs e)
        {
            int percent = (int)(e.PerCent * 100);
            string msg = percent.ToString("000") + " - " + e.Message;
            Console.WriteLine("LOG> " + msg);
            if (e.FeedbackObj != null)
                Console.WriteLine(percent.ToString("000") + " - FeedbackObj: " + e.FeedbackObj.ToString());
        }



        /// <summary>
        /// ...
        /// </summary>
        private static void ShowUsage()
        {
            Console.WriteLine("USAGE   [TODO]");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("\n\n");
        }


        /// <summary>
        /// ...
        /// </summary>
        private static Dictionary<string, string> ParseParameters(string[] args)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string arg in args)
            {
                if (arg.Length > 1)
                {
                    if (arg[0] == '-')
                    {
                        int eqSignPosition = arg.IndexOf('=');
                        if (eqSignPosition != -1)
                        {
                            string paramKey = arg.Substring(1, eqSignPosition - 1);
                            string paramValue = arg.Substring(eqSignPosition + 1);
                            if (!parameters.ContainsKey(paramKey))
                                parameters.Add(paramKey, paramValue);
                        }
                    }
                }
            }
            return parameters;
        }

    }
}
