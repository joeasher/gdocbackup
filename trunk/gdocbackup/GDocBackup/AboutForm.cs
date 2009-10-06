using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace GDocBackup
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Logo;
            LblVersion.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Thread checkNewVersionThrd = new Thread(ExecCheckNewVersion);
            checkNewVersionThrd.Name = "CheckNewVersionExists";
            checkNewVersionThrd.IsBackground = true;
            checkNewVersionThrd.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }

        private void ExecCheckNewVersion()
        {
            Version localVersion;
            _newveravailable = CheckUpdates.Exec(out localVersion, out _newversion);
            try
            {
                Thread.Sleep(5);
                this.BeginInvoke((MethodInvoker)delegate() { this.ShowCheckNewVersionResult(); });
            }
            catch (Exception) { }   // the form could be closed during thread run
        }

        private bool _newveravailable;
        private Version _newversion;

        private void ShowCheckNewVersionResult()
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