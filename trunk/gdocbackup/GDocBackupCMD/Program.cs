using System;
using System.Collections.Generic;
using System.Text;


namespace GDocBackupCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(i + "\t" + args[i]);
            }





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


                            Console.WriteLine(paramKey + " --> " + paramValue);

                            //switch (tokens[0])
                            //{
                            //    case "username": break;
                            //    case "password": break;
                            //    case "passwordEnc": break;
                            //}


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

            }



            Console.ReadKey();


        }


        private 
    }
}
