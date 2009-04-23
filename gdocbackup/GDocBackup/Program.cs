using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GDocBackup
{
    static class Program
    {
        /// <summary>
        /// Application entry point!
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //if (args.Length > 0 && args[0] == "-console")
            //{
            //    .... TODO .....
            //    RunFromConsole();
            //    Environment.ExitCode = 0;
            //    return;
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}