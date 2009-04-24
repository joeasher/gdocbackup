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
using Google.Documents;


namespace GDocBackup
{
    public partial class ConfigForm : Form
    {
        private Properties.Settings conf = Properties.Settings.Default;

        /// <summary>
        /// ...
        /// </summary>
        public ConfigForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ...
        /// </summary>
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Logo;
            this.SetupControls();

            // Main TAB
            TbUsername.Text = conf.UserName;
            TbPassword.Text = String.IsNullOrEmpty(conf.Password) ? null : Utility.UnprotectData(conf.Password);
            CbStorePassword.Checked = !String.IsNullOrEmpty(conf.Password);
            TbBackupDir.Text = conf.BackupDir;
            cbDisableUpdateCheck.Checked = conf.DisableUpdateCheck;

            // Data format TAB
            cbDocFormat.SelectedItem = Utility.ParseEnum<Document.DownloadType>(conf.DocumentExportFormat);
            cbSprShFormat.SelectedItem = Utility.ParseEnum<Document.DownloadType>(conf.SpreadsheetExportFormat);
            cbPresFormat.SelectedItem = Utility.ParseEnum<Document.DownloadType>(conf.PresentationExportFormat);


            // Proxy TAB
            cbSetProxy.Checked = conf.ProxyExplicit;
            cbDirectConnection.Checked = conf.ProxyDirectConnection;
            comboProxyHostSource.SelectedItem = Utility.ParseEnum<ProxyHostPortSource>(conf.ProxyHostPortSource);
            tbProxyHost.Text = conf.ProxyHost;
            tbProxyPort.Text = conf.ProxyPort.ToString();
            comboProxyAuthType.SelectedItem = Utility.ParseEnum<ProxyAuthMode>(conf.ProxyAuthMode);
            tbProxyLogin.Text = conf.ProxyUsername;
            tbProxyPassword.Text = String.IsNullOrEmpty(conf.ProxyPassword) ? null : Utility.UnprotectData(conf.ProxyPassword);

            this.SetAllControlStatus();
        }


        /// <summary>
        /// ...
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Main TAB
            conf.UserName = TbUsername.Text;
            conf.Password = CbStorePassword.Checked ? Utility.ProtectData(TbPassword.Text) : null;
            conf.BackupDir = TbBackupDir.Text;
            conf.DisableUpdateCheck = cbDisableUpdateCheck.Checked;

            // Data format TAB
            conf.DocumentExportFormat = cbDocFormat.SelectedItem.ToString();
            conf.SpreadsheetExportFormat = cbSprShFormat.SelectedItem.ToString();
            conf.PresentationExportFormat = cbPresFormat.SelectedItem.ToString();

            // Proxy TAB
            conf.ProxyExplicit = cbSetProxy.Checked;
            conf.ProxyDirectConnection = cbDirectConnection.Checked;
            conf.ProxyHostPortSource = comboProxyHostSource.SelectedItem.ToString();
            conf.ProxyHost = tbProxyHost.Text;
            conf.ProxyPort = int.Parse(tbProxyPort.Text);
            conf.ProxyAuthMode = comboProxyAuthType.SelectedItem.ToString();
            conf.ProxyUsername = tbProxyLogin.Text;
            conf.ProxyPassword = String.IsNullOrEmpty(tbProxyPassword.Text) ? null : Utility.ProtectData(tbProxyPassword.Text);

            conf.Save();
            this.DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// ...
        /// </summary>
        private void BtnSelectDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = TbBackupDir.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
                TbBackupDir.Text = fbd.SelectedPath;
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void CbStorePassword_CheckedChanged(object sender, EventArgs e)
        {
            this.SetAllControlStatus();
        }

        private void CbUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.SetAllControlStatus();
        }

        private void CbDefaultProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.SetAllControlStatus();
        }

        private void comboProxyAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetAllControlStatus();
        }

        private void cbDirectConnection_CheckedChanged(object sender, EventArgs e)
        {
            this.SetAllControlStatus();
        }

        private void comboProxyAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetAllControlStatus();
        }

        /// <summary>
        /// Checks all control dependecies and sets visul properties.
        /// </summary>
        private void SetAllControlStatus()
        {
            // Username & password tab
            TbPassword.Enabled = CbStorePassword.Checked;

            // Proxy tab
            panelProxy.Enabled = cbSetProxy.Checked;
            panelProxy2.Enabled = !cbDirectConnection.Checked;
            if (comboProxyHostSource.SelectedItem != null)
                panelProxyHostPort.Enabled = ((ProxyHostPortSource)comboProxyHostSource.SelectedItem == ProxyHostPortSource.HostPort);
            if (comboProxyAuthType.SelectedItem != null)
                panelProxyUserPwd.Enabled = ((ProxyAuthMode)comboProxyAuthType.SelectedItem == ProxyAuthMode.UsernamePassword);
        }


        /// <summary>
        /// ...
        /// </summary>
        private void SetupControls()
        {
            comboProxyAuthType.Items.Add(ProxyAuthMode.NotAuthenticated);
            comboProxyAuthType.Items.Add(ProxyAuthMode.DefaultCredential);
            comboProxyAuthType.Items.Add(ProxyAuthMode.UsernamePassword);

            comboProxyHostSource.Items.Add(ProxyHostPortSource.Default);
            comboProxyHostSource.Items.Add(ProxyHostPortSource.HostPort);

            cbDocFormat.Items.Add(Document.DownloadType.doc);
            cbDocFormat.Items.Add(Document.DownloadType.odt);
            cbDocFormat.Items.Add(Document.DownloadType.png);
            cbDocFormat.Items.Add(Document.DownloadType.rtf);
            cbDocFormat.Items.Add(Document.DownloadType.txt);
            cbDocFormat.Items.Add(Document.DownloadType.pdf);

            cbSprShFormat.Items.Add(Document.DownloadType.xls);
            cbSprShFormat.Items.Add(Document.DownloadType.csv);
            cbSprShFormat.Items.Add(Document.DownloadType.ods);
            cbSprShFormat.Items.Add(Document.DownloadType.tsv);
            cbSprShFormat.Items.Add(Document.DownloadType.pdf);

            cbPresFormat.Items.Add(Document.DownloadType.ppt);
            cbPresFormat.Items.Add(Document.DownloadType.pdf);
        }




    }
}