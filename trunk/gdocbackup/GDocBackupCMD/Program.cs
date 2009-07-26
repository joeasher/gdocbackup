using System;
using System.Collections.Generic;
using System.Text;
using GDocBackupLib;
using System.IO;
using Google.Documents;


namespace GDocBackupCMD
{
    class Program
    {
        static void Main(string[] args)
        {


            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string arg in args)
            {
                if (arg.Length > 1)
                {
                    if (arg[0] == '-')
                    {
                        int x = arg.IndexOf('=');
                        if (x != -1)
                        {
                            string paramKey = arg.Substring(1, x - 1);
                            string paramValue = arg.Substring(x + 1);

                            Console.WriteLine("FOUND: " + paramKey + " --> " + paramValue);

                            if (!parameters.ContainsKey(paramKey))
                            {
                                parameters.Add(paramKey, paramValue);
                            }
                        }
                    }
                }
            }


            if (parameters.ContainsKey("encodepassword"))
            {
                Console.WriteLine(GDocBackupLib.Utility.ProtectData(parameters["encodepassword"]));
                return;
            }



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
            string docF = parameters.ContainsKey("docF") ? parameters["destDir"] : null;
            if (docF == null)
                throw new ApplicationException("Empty docF");
            List<Document.DownloadType> docTypes = Utility.DecodeDownloadTypeArray(docF);

            //presF sprsF 


            Console.ReadKey();


        }


        // private 
    }
}
