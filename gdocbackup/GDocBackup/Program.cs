using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace GDocBackup
{
    static class Program
    {
        /// <summary>
        /// Application entry point!
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //throw new ApplicationException("TEST");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<string> argList = new List<string>(args); 
            
            MainForm mf = new MainForm();
            mf.AutoStart = argList.Contains("-autostart");
            mf.WriteLog = argList.Contains("-writelog");
            Application.Run(mf);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            BuildAndSendFeedBack("Application_ThreadException", e.Exception);
            Application.Exit();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            BuildAndSendFeedBack("CurrentDomain_UnhandledException", e.ExceptionObject);
            Application.Exit();
        }

        private static void BuildAndSendFeedBack(string mainEx, object subEx)
        {
            // Detect if running on Mono framework
            Type tmono = Type.GetType("Mono.Runtime");

            // Build feedback data
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Operating System: " + Environment.OSVersion.Platform + " - " + Environment.OSVersion.VersionString);
            sb.AppendLine("Framework: " + Environment.Version.ToString());
            if (tmono != null)
                sb.AppendLine("Running on Mono [" + tmono.ToString() + "]");
            sb.AppendLine(new String('-', 40));
            sb.AppendLine(mainEx);
            sb.AppendLine(new String('-', 40));
            if (subEx != null)
            {
                sb.AppendLine(subEx.ToString());
                sb.AppendLine(new String('-', 40));
            }

            // Show SendFeedback window
            SendFeedbackForm sf = new SendFeedbackForm();
            sf.DataTitle = "GDocBackup encountered an unexpected error!";
            sf.DataBody = sb.ToString();
            sf.ShowDialog();
        }

    }
}