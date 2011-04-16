using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using Google.Documents;

namespace GDocBackup
{
    public partial class AboutForm : Form
    {
        private bool _newveravailable;
        private bool _newversionerror;
        private Version _newversion;


        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            Version googleVer = typeof(Document).Assembly.GetName().Version;
            string googleLibVersion = googleVer.Major + "." + googleVer.Minor + "." + googleVer.Build + "   (subversion revision " + googleVer.Revision + ")";
            string gdocbakcupVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Icon = Properties.Resources.Logo;
            LblVersion.Text += gdocbakcupVersion;
            LblGoogleVer.Text = "google-gdata ver. " + googleLibVersion;

            Thread checkNewVersionThrd = new Thread(ExecCheckNewVersion);
            checkNewVersionThrd.Name = "CheckNewVersionExists";
            checkNewVersionThrd.IsBackground = true;
            checkNewVersionThrd.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // note: Process.Start crashes on some systems (issue 17)
            try { System.Diagnostics.Process.Start((sender as LinkLabel).Text); }
            catch (Exception) { }
        }

        private void ExecCheckNewVersion()
        {
            Version localVersion;
            _newveravailable = CheckUpdates.Exec(out localVersion, out _newversion, out _newversionerror);
            try
            {
                this.BeginInvoke((MethodInvoker)delegate() { this.ShowCheckNewVersionResult(); });
            }
            catch (Exception) { }   // the form could be closed during thread run
        }

        private void ShowCheckNewVersionResult()
        {
            if (_newversionerror)
            {
                lblCheckNewVer.Text = "Error detecting latest available version.";
                pictRotor.Image = Properties.Resources.error_20x20;
            }
            else
            {
                if (_newveravailable)
                {
                    lblCheckNewVer.Text = "A new version is available (" + _newversion.ToString() + ")";
                    pictRotor.Image = Properties.Resources.warning_20x20;
                }
                else
                {
                    lblCheckNewVer.Text = "You have the latest version.";
                    pictRotor.Image = Properties.Resources.ok_20x20;
                }
            }
        }

    }
}