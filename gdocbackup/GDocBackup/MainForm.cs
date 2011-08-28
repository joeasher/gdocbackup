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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Net;
using Google.Documents;
using GDocBackupLib;


namespace GDocBackup
{
    public partial class MainForm : Form
    {

        private Thread _workingThread;


        /// <summary>
        /// Log data
        /// </summary>
        private List<string> _logs = null;


        /// <summary>
        /// Autostart flag
        /// </summary>
        public bool AutoStart = false;


        /// <summary>
        /// Autostart flag
        /// </summary>
        // public bool WriteLog = false;


        /// <summary>
        /// Debugmode flag
        /// </summary>
        private bool _debugMode = false;
        public bool DebugMode
        {
            get
            {
                return _debugMode;
            }
            set
            {
                _debugMode = value;
                this.debugModeToolStripMenuItem.Checked = _debugMode;
            }
        }


        /// <summary>
        /// ...
        /// </summary>
        private string _debugModeLogSessionID = "GDocBackup_" + DateTime.Now.ToString("yyyyMMdd-HHmmss");


        /// <summary>
        /// [Constructor]
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Form load event
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            string gdocbakcupVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Icon = Properties.Resources.Logo;

            this.Text += " - Ver. " + gdocbakcupVersion;

            this.StoreLogMsgInfo(-1, "FORM_LOAD: " + this.Text);

            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
                Properties.Settings.Default.Save();
            }

            this.ExecCheckUpdates();

            if (String.IsNullOrEmpty(Properties.Settings.Default.BackupDir))
            {
                using (ConfigForm cf = new ConfigForm())
                {
                    cf.ShowDialog();
                }
            }
        }


        /// <summary>
        /// Form Shown event
        /// </summary>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (AutoStart)
                this.ExecBackUp(false);
        }


        /// <summary>
        /// Check if an update is available. If yes, show a Message Form.
        /// </summary>
        private void ExecCheckUpdates()
        {
            if (!Properties.Settings.Default.DisableUpdateCheck)
            {
                if (DateTime.Now.Subtract(Properties.Settings.Default.LastUpdateCheck).TotalDays > 1.0)
                {
                    Thread chkUpdtThread = new Thread(delegate()
                        {
                            Version localVersion;
                            Version remoteVersion;
                            bool errorPresent;
                            if (CheckUpdates.Exec(out localVersion, out remoteVersion, out errorPresent))
                            {
                                this.BeginInvoke((MethodInvoker)delegate()
                                    {
                                        using (NewVersion nv = new NewVersion())
                                        {
                                            nv.LocalVersion = localVersion;
                                            nv.RemoteVersion = remoteVersion;
                                            nv.ShowDialog(this);
                                        }
                                    });
                            }
                            Properties.Settings.Default.LastUpdateCheck = DateTime.Now;
                            Properties.Settings.Default.Save();
                        }
                    );

                    chkUpdtThread.IsBackground = true;
                    chkUpdtThread.Start();
                }
            }
        }


        /// <summary>
        /// Add log message to list and file
        /// </summary>
        /// <param name="s"></param>
        private void StoreLogMsgInfo(int percent, string s)
        {
            string msg = DateTime.Now.ToString() + " - " + percent.ToString("000") + " > " + s;
            if (_logs != null)
                _logs.Add(msg);
            if (_debugMode)
            {
                string logFileName = _debugModeLogSessionID + ".log";
                string logFileNameFP = Path.Combine(Path.GetTempPath(), logFileName);
                File.AppendAllText(logFileNameFP, msg + Environment.NewLine);
            }
        }


        #region  ---- User click on Buttons, Menu items & C.  ----

        private void BtnExec_Click(object sender, EventArgs e)
        {
            if (this.BtnExec.Text != "STOP")
                this.ExecBackUp(false);
            else
                if (_workingThread != null && _workingThread.IsAlive)
                    _workingThread.Abort();
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.BtnExec.Text != "STOP")
                this.ExecBackUp(false);
        }

        private void downloadAllagainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.BtnExec.Text != "STOP")
            {
                if (MessageBox.Show(
                        "GDocBackup will donwload again ALL documents from Google Docs. Are you sure?",
                        "GDocBackup",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.OK)
                    this.ExecBackUp(true);
            }
        }

        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DebugMode = !this.DebugMode;
        }

        private void configToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (ConfigForm cf = new ConfigForm())
            {
                cf.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm af = new AboutForm())
                af.ShowDialog();
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LogsForm lf = new LogsForm())
            {
                if (_logs != null)
                    lf.Logs = _logs.ToArray();
                lf.ShowDialog();
            }
        }

        private void techDocListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string userName = Properties.Settings.Default.UserName;
                string password =
                    String.IsNullOrEmpty(Properties.Settings.Default.Password) ?
                    String.Empty :
                    Utility.UnprotectData(Properties.Settings.Default.Password);

                if (String.IsNullOrEmpty(userName))
                {
                    SimpleInputBox sib = new SimpleInputBox();
                    sib.Title = "Username";
                    sib.MaskInput = false;
                    if (sib.ShowDialog() != DialogResult.OK)
                        return;
                    userName = sib.TextValue;
                }

                if (String.IsNullOrEmpty(password))
                {
                    SimpleInputBox sib = new SimpleInputBox();
                    sib.Title = "Password";
                    sib.MaskInput = true;
                    if (sib.ShowDialog() != DialogResult.OK)
                        return;
                    password = sib.TextValue;
                }

                MessageBox.Show("Starting");

                TechSupport.ExportDocList(fbd.SelectedPath, userName, password);

                MessageBox.Show("Done");
            }
        }


        #endregion --------------------------------



        #region ---- Backup thread ----

        /// <summary>
        /// Start backup in a separate (background) thread
        /// </summary>
        private void ExecBackUp(bool downloadAll)
        {
            string userName = Properties.Settings.Default.UserName;
            string password =
                String.IsNullOrEmpty(Properties.Settings.Default.Password) ?
                String.Empty :
                Utility.UnprotectData(Properties.Settings.Default.Password);
            string backupDir = Properties.Settings.Default.BackupDir;

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(backupDir))
            {
                LoginForm login = new LoginForm();
                login.UserName = userName;
                login.BackupDir = backupDir;
                if (login.ShowDialog() == DialogResult.OK)
                {
                    userName = login.UserName;
                    password = login.Password;
                    backupDir = login.BackupDir;
                }
                else
                    return;
            }

            _logs = new List<string>();
            this.dataGV.Rows.Clear();
            this.BtnExec.Text = "STOP";
            this.Cursor = Cursors.WaitCursor;
            this.dataGV.Cursor = Cursors.WaitCursor;   // Need to set it (perhaps a bug of DataGridView...)

            // Start working thread
            _workingThread = new Thread(ExecBackupThread);
            _workingThread.IsBackground = true;
            _workingThread.Name = "BackupExecThread";
            _workingThread.Start(new string[] { userName, password, backupDir, downloadAll ? "ALL" : "" });
        }


        /// <summary>
        /// Exec backup (new thread "body")
        /// </summary>
        private void ExecBackupThread(object data)
        {
            String[] parameters = data as String[];

            Properties.Settings conf = Properties.Settings.Default;

            IWebProxy webproxy = Utility.GetProxy(conf.ProxyExplicit, conf.ProxyDirectConnection, conf.ProxyHostPortSource, conf.ProxyHost, conf.ProxyPort, conf.ProxyAuthMode, conf.ProxyUsername, conf.ProxyPassword);

            Config config = new Config(
                parameters[0],
                parameters[1],
                parameters[2],
                parameters[3] == "ALL",
                Utility.DecodeDownloadTypeArray(conf.DocumentExportFormat).ToArray(),
                Utility.DecodeDownloadTypeArray(conf.SpreadsheetExportFormat).ToArray(),
                Utility.DecodeDownloadTypeArray(conf.PresentationExportFormat).ToArray(),
                Utility.DecodeDownloadTypeArray(conf.DrawingExportFormat).ToArray(),
                webproxy,
                conf.BypassCertificateChecks,
                this.DebugMode,
                conf.DateDelta,
                conf.AppsMode,
                conf.AppsDomain,
                conf.AppsOAuthSecret);

            Backup b = new Backup(config);
            b.Feedback += new EventHandler<FeedbackEventArgs>(Backup_Feedback);
            bool result = b.Exec();
            this.BeginInvoke((MethodInvoker)delegate() { EndDownload(result, b.DuplicatedDocNames, b.LastException); });
        }


        /// <summary>
        /// Backup feedback event handler
        /// </summary>
        private void Backup_Feedback(object sender, FeedbackEventArgs e)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new EventHandler<FeedbackEventArgs>(Backup_Feedback), sender, e);
            else
            {
                int percent = (int)(e.PerCent * 100);
                this.progressBar1.Value = percent;

                if (!String.IsNullOrEmpty(e.Message))
                    this.StoreLogMsgInfo(percent, e.Message);

                if (e.FeedbackObj != null)
                {
                    this.StoreLogMsgInfo(percent, "FO> " + e.FeedbackObj.ToString());
                    this.dataGV.Rows.Add(
                        new object[] { 
                            e.FeedbackObj.FileName, 
                            e.FeedbackObj.DocType, 
                            e.FeedbackObj.ExportFormat.ToUpper(),
                            e.FeedbackObj.Action,
                            e.FeedbackObj.RemoteDateTime.ToString() });
                }
            }
        }


        /// <summary>
        /// End download event handler
        /// </summary>
        private void EndDownload(bool isOK, List<string> duplicatedDocNames, Exception ex)
        {
            this.progressBar1.Value = 0;
            this.Cursor = Cursors.Default;
            this.dataGV.Cursor = Cursors.Default;    // Need to set it (perhaps a bug of DataGridView...)
            this.BtnExec.Enabled = true;
            this.BtnExec.Text = "Exec";

            if (duplicatedDocNames != null && duplicatedDocNames.Count > 0 && Properties.Settings.Default.DisableDuplicatedItemWarnings == false)
            {
                int totDupDocs = duplicatedDocNames.Count;
                if (totDupDocs > 20)
                {
                    duplicatedDocNames = duplicatedDocNames.GetRange(0, 20);
                    duplicatedDocNames.Add("...(list truncated)");
                }

                string msg =
                    "Warning: there are " + totDupDocs + " items with the same name in the same folder. " + Environment.NewLine +
                    ((totDupDocs > 20) ? ("First 20 items: " + Environment.NewLine) : ("")) +
                    String.Join(Environment.NewLine, duplicatedDocNames.ToArray());
                MessageBox.Show(msg, "GDocBackup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (isOK)
            {
                if (AutoStart)
                {
                    Application.Exit();
                    return;   // Win does not requires it. Mono on Ubuntu, yes!  Is a Mono bug???   ... TO BE MORE TESTED  :(
                }
                else
                    MessageBox.Show("Backup completed.", "GDocBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string msg = "Backup completed. There are errors! " + Environment.NewLine + Environment.NewLine +
                   "Please review Logs for details. (Menu 'Action' --> 'View Logs')";
                if (ex != null)
                {
                    this.StoreLogMsgInfo(-1, "############### EXCEPTION ###############");
                    this.StoreLogMsgInfo(-1, ex.ToString());
                    this.StoreLogMsgInfo(-1, "#########################################");
                    msg += Environment.NewLine + Environment.NewLine +
                        "[ERROR: " + ex.GetType().Name + " : " + ex.Message + "]";
                }
                MessageBox.Show(msg, "GDocBackup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion ----------------------------



    }
}