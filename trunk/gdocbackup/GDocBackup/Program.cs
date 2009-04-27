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
            List<string> argList = new List<string>(args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mf = new MainForm();
            mf.AutoStart = argList.Contains("-autostart");
            Application.Run(mf);
        }
    }
}