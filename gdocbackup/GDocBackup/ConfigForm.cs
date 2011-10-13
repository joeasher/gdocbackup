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
using GDocBackupLib;


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

            // --- Main TAB ---
            if (!conf.AppsMode)
            {
                TbUsername.Text = conf.UserName;
                TbPassword.Text = String.IsNullOrEmpty(conf.Password) ? null : Utility.UnprotectData(conf.Password);
            }
            else
            {
                TbUsername.Text = "";
                TbPassword.Text = "";
            }
            CbStorePassword.Checked = !String.IsNullOrEmpty(conf.Password);
            TbBackupDir.Text = conf.BackupDir;
            cbDisableUpdateCheck.Checked = conf.DisableUpdateCheck;
            cbCheckForBetaVersion.Checked = conf.CheckForBeta;
            cbBypassCertificatesCheck.Checked = conf.BypassCertificateChecks;
            TbDateDelta.Text = conf.DateDelta.ToString();
            cbDisableDuplicatedItemsWarning.Checked = conf.DisableDuplicatedItemWarnings;
            rbRunModeNormal.Checked = !conf.AppsMode;
            rbRunModeGoogleApps.Checked = conf.AppsMode;
            panelRunModeNormal.Visible = !conf.AppsMode;
            panelRunModeGoogleApps.Visible = conf.AppsMode;
            panelRunModeGoogleApps.Location = panelRunModeNormal.Location;
            TbDomain.Text = conf.AppsDomain;
            TbOAuthSecret.Text = String.IsNullOrEmpty(conf.AppsOAuthSecretEncrypted) ? null : Utility.UnprotectData(conf.AppsOAuthSecretEncrypted);
            cbUseOnlyOauth.Checked = conf.AppsOAuthOnly;
            if (conf.AppsMode)
            {
                tbGAUserName.Text = String.IsNullOrEmpty(conf.UserName) ? String.Empty : conf.UserName;
                tbGAPassword.Text = String.IsNullOrEmpty(conf.Password) ? String.Empty : Utility.UnprotectData(conf.Password);
            }
            else
            {
                tbGAUserName.Text = "";
                tbGAPassword.Text = "";
            }



            // --- Data format TAB ---
            cbEnableMultiExport.Checked = conf.MultiExportEnabled;

            List<Document.DownloadType> docsDownTypes = Utility.DecodeDownloadTypeArray(conf.DocumentExportFormat);
            List<Document.DownloadType> sprsDownTypes = Utility.DecodeDownloadTypeArray(conf.SpreadsheetExportFormat);
            List<Document.DownloadType> presDownTypes = Utility.DecodeDownloadTypeArray(conf.PresentationExportFormat);
            List<Document.DownloadType> drawDownTypes = Utility.DecodeDownloadTypeArray(conf.DrawingExportFormat);

            if (docsDownTypes.Count > 0) cbDocFormat.SelectedItem = docsDownTypes[0];
            if (sprsDownTypes.Count > 0) cbSprShFormat.SelectedItem = sprsDownTypes[0];
            if (presDownTypes.Count > 0) cbPresFormat.SelectedItem = presDownTypes[0];
            if (drawDownTypes.Count > 0) cbDrawFormat.SelectedItem = drawDownTypes[0];

            this.CLBActivateItems(clbDocFormat, docsDownTypes);
            this.CLBActivateItems(clbSprShFormat, sprsDownTypes);
            this.CLBActivateItems(clbPresFormat, presDownTypes);
            this.CLBActivateItems(clbDrawFormat, drawDownTypes);


            // --- Proxy TAB ---
            cbSetProxy.Checked = conf.ProxyExplicit;
            cbDirectConnection.Checked = conf.ProxyDirectConnection;
            comboProxyHostSource.SelectedItem = Utility.ParseEnum<ProxyHostPortSource>(conf.ProxyHostPortSource);
            tbProxyHost.Text = conf.ProxyHost;
            tbProxyPort.Text = conf.ProxyPort.ToString();
            comboProxyAuthType.SelectedItem = Utility.ParseEnum<ProxyAuthMode>(conf.ProxyAuthMode);
            tbProxyLogin.Text = conf.ProxyUsername;
            tbProxyPassword.Text = String.IsNullOrEmpty(conf.ProxyPassword) ? null : Utility.UnprotectData(conf.ProxyPassword);


            // Setup controls status
            this.SetAllControlStatus();
        }



        /// <summary>
        /// ...
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Main TAB
            if (rbRunModeNormal.Checked)
            {
                conf.UserName = TbUsername.Text;
                conf.Password = CbStorePassword.Checked ? Utility.ProtectData(TbPassword.Text) : null;
            }
            else
            {
                conf.UserName = tbGAUserName.Text;
                conf.Password = Utility.ProtectData(tbGAPassword.Text);
            }
            conf.BackupDir = TbBackupDir.Text;
            conf.DisableUpdateCheck = cbDisableUpdateCheck.Checked;
            conf.CheckForBeta = cbCheckForBetaVersion.Checked;
            conf.BypassCertificateChecks = cbBypassCertificatesCheck.Checked;
            conf.DateDelta = int.Parse(TbDateDelta.Text);
            conf.DisableDuplicatedItemWarnings = cbDisableDuplicatedItemsWarning.Checked;
            conf.AppsMode = rbRunModeGoogleApps.Checked;
            conf.AppsOAuthSecret = null;  // Chiave non più usata. Svuotarla!
            conf.AppsOAuthOnly = cbUseOnlyOauth.Checked;

            if (conf.AppsMode)
            {
                conf.AppsDomain = TbDomain.Text;
                conf.AppsOAuthSecretEncrypted = String.IsNullOrEmpty(TbOAuthSecret.Text) ? null : Utility.ProtectData(TbOAuthSecret.Text);
            }

            if (conf.AppsMode && conf.AppsOAuthOnly)
            {
                conf.UserName = String.Empty;
                conf.Password = Utility.ProtectData(String.Empty);
            }

            if (!conf.AppsMode)
            {
                conf.AppsDomain = String.Empty;
                conf.AppsOAuthSecretEncrypted = Utility.ProtectData(String.Empty);
            }

            // Data format TAB
            conf.MultiExportEnabled = cbEnableMultiExport.Checked;
            if (cbEnableMultiExport.Checked)
            {
                List<Document.DownloadType> docsDownTypes = this.CLBGetActiveItems(clbDocFormat);
                List<Document.DownloadType> sprsDownTypes = this.CLBGetActiveItems(clbSprShFormat);
                List<Document.DownloadType> presDownTypes = this.CLBGetActiveItems(clbPresFormat);
                List<Document.DownloadType> drawDownTypes = this.CLBGetActiveItems(clbDrawFormat);
                if (docsDownTypes.Count > 0) conf.DocumentExportFormat = Utility.EncodeDownloadTypeArray(docsDownTypes);
                if (sprsDownTypes.Count > 0) conf.SpreadsheetExportFormat = Utility.EncodeDownloadTypeArray(sprsDownTypes);
                if (presDownTypes.Count > 0) conf.PresentationExportFormat = Utility.EncodeDownloadTypeArray(presDownTypes);
                if (drawDownTypes.Count > 0) conf.DrawingExportFormat = Utility.EncodeDownloadTypeArray(drawDownTypes);
            }
            else
            {
                if (cbDocFormat.SelectedItem != null) conf.DocumentExportFormat = cbDocFormat.SelectedItem.ToString();
                if (cbSprShFormat.SelectedItem != null) conf.SpreadsheetExportFormat = cbSprShFormat.SelectedItem.ToString();
                if (cbPresFormat.SelectedItem != null) conf.PresentationExportFormat = cbPresFormat.SelectedItem.ToString();
                if (cbDrawFormat.SelectedItem != null) conf.DrawingExportFormat = cbDrawFormat.SelectedItem.ToString();
            }

            // Proxy TAB
            conf.ProxyExplicit = cbSetProxy.Checked;
            conf.ProxyDirectConnection = cbDirectConnection.Checked;
            conf.ProxyHostPortSource = comboProxyHostSource.SelectedItem.ToString();
            conf.ProxyHost = tbProxyHost.Text;
            conf.ProxyPort = int.Parse(tbProxyPort.Text);
            conf.ProxyAuthMode = comboProxyAuthType.SelectedItem.ToString();
            conf.ProxyUsername = tbProxyLogin.Text;
            conf.ProxyPassword = String.IsNullOrEmpty(tbProxyPassword.Text) ? null : Utility.ProtectData(tbProxyPassword.Text);

            // Save configuration
            conf.Save();
            this.DialogResult = DialogResult.OK;
        }


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

        private void cbEnableMultiExport_CheckedChanged(object sender, EventArgs e)
        {
            this.SetAllControlStatus();
        }


        /// <summary>
        /// Checks all control dependecies and sets visual properties.
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

            // Export format tab
            panelMultiExport.Visible = cbEnableMultiExport.Checked;
            panelSingleExport.Visible = !cbEnableMultiExport.Checked;
        }


        /// <summary>
        /// Setup and preload of many controls
        /// </summary>
        private void SetupControls()
        {
            panelSingleExport.Location = new Point(0, 0);
            panelMultiExport.Location = new Point(0, 0);

            comboProxyAuthType.Items.Add(ProxyAuthMode.NotAuthenticated);
            comboProxyAuthType.Items.Add(ProxyAuthMode.DefaultCredential);
            comboProxyAuthType.Items.Add(ProxyAuthMode.UsernamePassword);

            comboProxyHostSource.Items.Add(ProxyHostPortSource.Default);
            comboProxyHostSource.Items.Add(ProxyHostPortSource.HostPort);


            Document.DownloadType[] docsDownTypes = new Document.DownloadType[] {
                Document.DownloadType.doc,
                Document.DownloadType.odt,
                Document.DownloadType.rtf,
                Document.DownloadType.txt,
                Document.DownloadType.pdf,
            };

            Document.DownloadType[] sprsDownType = new Document.DownloadType[]{
                Document.DownloadType.xls,
                Document.DownloadType.csv,
                Document.DownloadType.ods,
                Document.DownloadType.tsv,
                Document.DownloadType.pdf,
            };

            Document.DownloadType[] presDownType = new Document.DownloadType[]{ 
                Document.DownloadType.ppt,
                Document.DownloadType.pdf,
                //Document.DownloadType.swf, 
                Document.DownloadType.txt  
            };

            Document.DownloadType[] drawDownType = new Document.DownloadType[]{
                Document.DownloadType.pdf, 
                Document.DownloadType.svg,
                Document.DownloadType.jpeg,
                Document.DownloadType.png 
            };

            for (int i = 0; i < docsDownTypes.Length; i++) cbDocFormat.Items.Add(docsDownTypes[i]);
            for (int i = 0; i < sprsDownType.Length; i++) cbSprShFormat.Items.Add(sprsDownType[i]);
            for (int i = 0; i < presDownType.Length; i++) cbPresFormat.Items.Add(presDownType[i]);
            for (int i = 0; i < drawDownType.Length; i++) cbDrawFormat.Items.Add(drawDownType[i]);

            for (int i = 0; i < docsDownTypes.Length; i++) clbDocFormat.Items.Add(docsDownTypes[i]);
            for (int i = 0; i < sprsDownType.Length; i++) clbSprShFormat.Items.Add(sprsDownType[i]);
            for (int i = 0; i < presDownType.Length; i++) clbPresFormat.Items.Add(presDownType[i]);
            for (int i = 0; i < drawDownType.Length; i++) clbDrawFormat.Items.Add(drawDownType[i]);

        }


        /// <summary>
        /// Utility: ....
        /// </summary>
        private void CLBActivateItems(CheckedListBox clb, List<Document.DownloadType> activeTypes)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (activeTypes.Contains((Document.DownloadType)clb.Items[i]))
                    clb.SetItemChecked(i, true);
            }
        }


        /// <summary>
        /// Utility: ....
        /// </summary>
        private List<Document.DownloadType> CLBGetActiveItems(CheckedListBox clb)
        {
            List<Document.DownloadType> list = new List<Document.DownloadType>();
            for (int i = 0; i < clb.CheckedItems.Count; i++)
                list.Add((Document.DownloadType)clb.CheckedItems[i]);
            return list;
        }


        private void rbRunModeNormal_CheckedChanged(object sender, EventArgs e)
        {
            panelRunModeNormal.Visible = rbRunModeNormal.Checked;
            panelRunModeGoogleApps.Visible = rbRunModeGoogleApps.Checked;
        }

        private void cbUseOnlyOauth_CheckedChanged(object sender, EventArgs e)
        {
            panelGAppsUserName.Visible = !cbUseOnlyOauth.Checked;
        }

    }
}