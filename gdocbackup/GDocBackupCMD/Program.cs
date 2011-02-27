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
using System.IO;
using GDocBackupLib;
using Google.Documents;
using System.Reflection;


namespace GDocBackupCMD
{
    class Program
    {
        static int Main(string[] args)
        {
            AssemblyName assembName = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine("\n" + assembName.Name + " - ver. " + assembName.Version.ToString() +
                " - " + "http://gs.fhtino.it/gdocbackup" + "\n\n");

            Dictionary<string, string> parameters = ParseParameters(args);
            if (!parameters.ContainsKey("mode"))
                parameters.Add("mode", "help");

            if (parameters["mode"] == "encodepassword")
            {
                if (parameters.ContainsKey("password"))
                {
                    string encodedpassword = GDocBackupLib.Utility.ProtectData(parameters["password"]);
                    if (parameters.ContainsKey("outfile"))
                    {
                        string outfile = parameters["outfile"];
                        File.WriteAllText(outfile, encodedpassword);
                        Console.WriteLine("Encoded password written to file " + outfile);
                    }
                    else
                    {
                        Console.WriteLine(encodedpassword);
                    }
                }
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
                    resOK = false;
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


        /// <summary>
        /// ...
        /// </summary>
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
            string downloadAll = parameters.ContainsKey("downloadAll") ? parameters["downloadAll"] : null;

            // Get BypassHttpsCertChecks
            bool bypassHttpsCertChecks = parameters.ContainsKey("bypassHttpsCertChecks");

            // Output parameters
            Console.WriteLine(new String('-', 40));
            Console.WriteLine("Parameters: ");
            Console.WriteLine("Username:     " + username);
            Console.WriteLine("Password:     " + "[hidden]");
            Console.WriteLine("DestDir:      " + destDir);
            Console.WriteLine("Document:     " + docF);
            Console.WriteLine("Spreadsheet:  " + sprsF);
            Console.WriteLine("Presentation: " + presF);
            Console.WriteLine("DownloadAll:  " + downloadAll);
            Console.WriteLine(new String('-', 40));

            // Exec backup
            Backup backup = new Backup(
                username, password, destDir,
                downloadAll == "yes",
                docTypes.ToArray(), sprsTypes.ToArray(), presTypes.ToArray(),
                null, bypassHttpsCertChecks, false, null);
            backup.Feedback += new EventHandler<FeedbackEventArgs>(backup_Feedback);
            bool resOK = backup.Exec();

            return resOK;
        }


        /// <summary>
        /// ...
        /// </summary>
        private static void backup_Feedback(object sender, FeedbackEventArgs e)
        {
            int percent = (int)(e.PerCent * 100);
            Console.WriteLine("LOG> " + percent.ToString("000") + " : " + e.Message);
            if (e.FeedbackObj != null)
                Console.WriteLine("FBK> " + percent.ToString("000") + " : " + e.FeedbackObj.ToString());
        }


        /// <summary>
        /// ...
        /// </summary>
        private static void ShowUsage()
        {
            Console.WriteLine("USAGE   (draft)");
            Console.WriteLine("");
            Console.WriteLine("GDocBackupCMD.exe -mode=backup|encodepassword");
            Console.WriteLine("");
            Console.WriteLine(">>> mode=backup <<<");
            Console.WriteLine("  -username: google username");
            Console.WriteLine("  -password: password (clear text)");
            Console.WriteLine("  -passwordEnc: encoded password");
            Console.WriteLine("  -passwordEncFile: file containing the encoded password");
            Console.WriteLine("  -destDir: path to the local destination directory");
            Console.WriteLine("  -docF : export format (for Documents)");
            Console.WriteLine("  -sprsF: export format (for Spreadsheets)");
            Console.WriteLine("  -presF: export format (for Presentations)");
            Console.WriteLine("  -downloadall: if \"yes\" download all documents");
            Console.WriteLine("  -bypassHttpsCertChecks:  bypass the checks of https certificate (at your own risk!!!)");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(">>> mode=encodepassword <<<");
            Console.WriteLine("  -password: string to be encoded");
            Console.WriteLine("  -outfile: output file name");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Examples:");
            Console.WriteLine("");
            Console.WriteLine("GDocBackupCMD.exe -mode=backup -username=foo -password=bar -destDir=c:\\temp\\docs\\ -docF=pdf -sprsF=csv -presF=ppt");
            Console.WriteLine("");
            Console.WriteLine("GDocBackupCMD.exe -mode=backup -username=foo -password=bar -destDir=c:\\temp\\docs\\ -docF=pdf -sprsF=csv -presF=ppt -downloadall=yes");
            Console.WriteLine("");
            Console.WriteLine("GDocBackupCMD.exe -mode=backup -username=foo -passwordEncFile=pass.txt -destDir=c:\\temp\\docs\\ -docF=pdf -sprsF=csv -presF=ppt");
            Console.WriteLine("");
            Console.WriteLine("GDocBackupCMD.exe -mode=backup -bypassHttpsCertChecks -username=foo -passwordEncFile=pass.txt -destDir=c:\\temp\\docs\\ -docF=pdf -sprsF=csv -presF=ppt");
            Console.WriteLine("");
            Console.WriteLine("GDocBackupCMD.exe -mode=encodepassword -password=foo");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
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
                        string paramKey;
                        string paramValue;
                        int eqSignPosition = arg.IndexOf('=');
                        if (eqSignPosition != -1)
                        {
                            paramKey = arg.Substring(1, eqSignPosition - 1);
                            paramValue = arg.Substring(eqSignPosition + 1);
                        }
                        else
                        {
                            paramKey = arg.Substring(1);    // ignore the first char '-'                            
                            paramValue = null;
                        }
                        if (!parameters.ContainsKey(paramKey))
                            parameters.Add(paramKey, paramValue);
                    }
                }
            }
            return parameters;
        }

    }
}
