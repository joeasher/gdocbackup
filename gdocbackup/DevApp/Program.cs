using System;
using System.Collections.Generic;
using System.Text;

namespace DevApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("\n\nPlease, specify parameter(s)\n\n");
                return;
            }

            if (args[0].StartsWith("-runmode="))
            {
                string[] tokens = args[0].Split('=');
                switch (tokens[1])
                {
                    case "getdoclist": GetDocList.Exec(args); break;
                    default: Console.WriteLine("\n\nUnknown options\n\n"); break;
                }
            }
            else
            {
                Console.WriteLine("\n\nNeed -runmode parameter\n\n");
            }

        }
    }
}
