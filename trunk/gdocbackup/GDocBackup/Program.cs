using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Threading;

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<string> argList = new List<string>(args);

            MainForm mf = new MainForm();
            mf.AutoStart = argList.Contains("-autostart");
            mf.WriteLog = argList.Contains("-writelog");
            Application.Run(mf);
        }

        /// <summary>
        /// Handles exceptions from (sub)threads
        /// </summary>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            BuildAndSendFeedBack("Application_ThreadException", e.Exception);
            Application.Exit();
        }

        /// <summary>
        /// Handles exceptions from main thread (of appdomain)
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            BuildAndSendFeedBack("CurrentDomain_UnhandledException", e.ExceptionObject);
            Application.Exit();
        }


        /// <summary>
        /// ...
        /// </summary>
        private static void BuildAndSendFeedBack(string mainEx, object subEx)
        {

            // Build feedback data
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mainEx);
            sb.AppendLine(new String('-', 40));
            if (subEx != null)
            {
                sb.AppendLine(subEx.ToString());
                sb.AppendLine(new String('-', 40));
            }

            // Show SendFeedback window
            using (SendFeedbackForm sf = new SendFeedbackForm())
            {
                sf.DataTitle = "GDocBackup encountered an unexpected error!";
                sf.DataBody = sb.ToString();
                sf.ShowDialog();
            }
        }

    }
}