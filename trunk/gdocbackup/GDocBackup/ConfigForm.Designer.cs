namespace GDocBackup
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TbUsername = new System.Windows.Forms.TextBox();
            this.TbBackupDir = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSelectDir = new System.Windows.Forms.Button();
            this.cbDocFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPresFormat = new System.Windows.Forms.ComboBox();
            this.cbSprShFormat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TbPassword = new System.Windows.Forms.TextBox();
            this.CbStorePassword = new System.Windows.Forms.CheckBox();
            this.panelProxy = new System.Windows.Forms.Panel();
            this.panelProxy2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.comboProxyHostSource = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panelProxyUserPwd = new System.Windows.Forms.Panel();
            this.tbProxyLogin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbProxyPassword = new System.Windows.Forms.TextBox();
            this.comboProxyAuthType = new System.Windows.Forms.ComboBox();
            this.panelProxyHostPort = new System.Windows.Forms.Panel();
            this.tbProxyHost = new System.Windows.Forms.TextBox();
            this.LabelProxyAddress = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbProxyPort = new System.Windows.Forms.TextBox();
            this.cbDirectConnection = new System.Windows.Forms.CheckBox();
            this.cbSetProxy = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelRunModeNormal = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbRunModeNormal = new System.Windows.Forms.RadioButton();
            this.rbRunModeGoogleApps = new System.Windows.Forms.RadioButton();
            this.label21 = new System.Windows.Forms.Label();
            this.panelRunModeGoogleApps = new System.Windows.Forms.Panel();
            this.panelGAppsUserName = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.tbGAUserName = new System.Windows.Forms.TextBox();
            this.tbGAPassword = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbUseOnlyOauth = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.TbDomain = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.TbOAuthSecret = new System.Windows.Forms.TextBox();
            this.cbDisableDuplicatedItemsWarning = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TbDateDelta = new System.Windows.Forms.TextBox();
            this.cbCheckForBetaVersion = new System.Windows.Forms.CheckBox();
            this.cbBypassCertificatesCheck = new System.Windows.Forms.CheckBox();
            this.cbDisableUpdateCheck = new System.Windows.Forms.CheckBox();
            this.tabPageNormalExport = new System.Windows.Forms.TabPage();
            this.cbEnableMultiExport = new System.Windows.Forms.CheckBox();
            this.panelMultiExport = new System.Windows.Forms.Panel();
            this.clbDrawFormat = new System.Windows.Forms.CheckedListBox();
            this.clbPresFormat = new System.Windows.Forms.CheckedListBox();
            this.clbSprShFormat = new System.Windows.Forms.CheckedListBox();
            this.clbDocFormat = new System.Windows.Forms.CheckedListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panelSingleExport = new System.Windows.Forms.Panel();
            this.cbDrawFormat = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbMultiExpCalc = new System.Windows.Forms.TextBox();
            this.tbMultiExpDoc = new System.Windows.Forms.TextBox();
            this.tbMultiExpPres = new System.Windows.Forms.TextBox();
            this.panelProxy.SuspendLayout();
            this.panelProxy2.SuspendLayout();
            this.panelProxyUserPwd.SuspendLayout();
            this.panelProxyHostPort.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelRunModeNormal.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelRunModeGoogleApps.SuspendLayout();
            this.panelGAppsUserName.SuspendLayout();
            this.tabPageNormalExport.SuspendLayout();
            this.panelMultiExport.SuspendLayout();
            this.panelSingleExport.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Backup directory:";
            // 
            // TbUsername
            // 
            this.TbUsername.Location = new System.Drawing.Point(77, 8);
            this.TbUsername.Name = "TbUsername";
            this.TbUsername.Size = new System.Drawing.Size(98, 20);
            this.TbUsername.TabIndex = 1;
            // 
            // TbBackupDir
            // 
            this.TbBackupDir.Location = new System.Drawing.Point(124, 205);
            this.TbBackupDir.Name = "TbBackupDir";
            this.TbBackupDir.Size = new System.Drawing.Size(165, 20);
            this.TbBackupDir.TabIndex = 3;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnSave.Location = new System.Drawing.Point(131, 444);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(84, 29);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnCancel.Location = new System.Drawing.Point(221, 444);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(84, 30);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSelectDir
            // 
            this.BtnSelectDir.Location = new System.Drawing.Point(295, 205);
            this.BtnSelectDir.Name = "BtnSelectDir";
            this.BtnSelectDir.Size = new System.Drawing.Size(31, 20);
            this.BtnSelectDir.TabIndex = 6;
            this.BtnSelectDir.Text = "-->";
            this.BtnSelectDir.UseVisualStyleBackColor = true;
            this.BtnSelectDir.Click += new System.EventHandler(this.BtnSelectDir_Click);
            // 
            // cbDocFormat
            // 
            this.cbDocFormat.FormattingEnabled = true;
            this.cbDocFormat.Location = new System.Drawing.Point(89, 18);
            this.cbDocFormat.Name = "cbDocFormat";
            this.cbDocFormat.Size = new System.Drawing.Size(90, 21);
            this.cbDocFormat.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Document:";
            // 
            // cbPresFormat
            // 
            this.cbPresFormat.FormattingEnabled = true;
            this.cbPresFormat.Location = new System.Drawing.Point(89, 72);
            this.cbPresFormat.Name = "cbPresFormat";
            this.cbPresFormat.Size = new System.Drawing.Size(90, 21);
            this.cbPresFormat.TabIndex = 8;
            // 
            // cbSprShFormat
            // 
            this.cbSprShFormat.FormattingEnabled = true;
            this.cbSprShFormat.Location = new System.Drawing.Point(89, 45);
            this.cbSprShFormat.Name = "cbSprShFormat";
            this.cbSprShFormat.Size = new System.Drawing.Size(90, 21);
            this.cbSprShFormat.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Presentation:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Spreadsheet:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Password:";
            // 
            // TbPassword
            // 
            this.TbPassword.Location = new System.Drawing.Point(77, 34);
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.PasswordChar = '*';
            this.TbPassword.Size = new System.Drawing.Size(98, 20);
            this.TbPassword.TabIndex = 2;
            // 
            // CbStorePassword
            // 
            this.CbStorePassword.AutoSize = true;
            this.CbStorePassword.Location = new System.Drawing.Point(181, 36);
            this.CbStorePassword.Name = "CbStorePassword";
            this.CbStorePassword.Size = new System.Drawing.Size(77, 17);
            this.CbStorePassword.TabIndex = 9;
            this.CbStorePassword.Text = "Remember";
            this.CbStorePassword.UseVisualStyleBackColor = true;
            this.CbStorePassword.CheckedChanged += new System.EventHandler(this.CbStorePassword_CheckedChanged);
            // 
            // panelProxy
            // 
            this.panelProxy.Controls.Add(this.panelProxy2);
            this.panelProxy.Controls.Add(this.cbDirectConnection);
            this.panelProxy.Enabled = false;
            this.panelProxy.Location = new System.Drawing.Point(0, 33);
            this.panelProxy.Name = "panelProxy";
            this.panelProxy.Size = new System.Drawing.Size(360, 161);
            this.panelProxy.TabIndex = 6;
            // 
            // panelProxy2
            // 
            this.panelProxy2.Controls.Add(this.label12);
            this.panelProxy2.Controls.Add(this.comboProxyHostSource);
            this.panelProxy2.Controls.Add(this.label7);
            this.panelProxy2.Controls.Add(this.panelProxyUserPwd);
            this.panelProxy2.Controls.Add(this.comboProxyAuthType);
            this.panelProxy2.Controls.Add(this.panelProxyHostPort);
            this.panelProxy2.Location = new System.Drawing.Point(3, 31);
            this.panelProxy2.Name = "panelProxy2";
            this.panelProxy2.Size = new System.Drawing.Size(354, 126);
            this.panelProxy2.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Proxy address:";
            // 
            // comboProxyHostSource
            // 
            this.comboProxyHostSource.FormattingEnabled = true;
            this.comboProxyHostSource.Location = new System.Drawing.Point(94, 8);
            this.comboProxyHostSource.Name = "comboProxyHostSource";
            this.comboProxyHostSource.Size = new System.Drawing.Size(155, 21);
            this.comboProxyHostSource.TabIndex = 8;
            this.comboProxyHostSource.SelectedIndexChanged += new System.EventHandler(this.comboProxyAddress_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Auth. type:";
            // 
            // panelProxyUserPwd
            // 
            this.panelProxyUserPwd.Controls.Add(this.tbProxyLogin);
            this.panelProxyUserPwd.Controls.Add(this.label9);
            this.panelProxyUserPwd.Controls.Add(this.label8);
            this.panelProxyUserPwd.Controls.Add(this.tbProxyPassword);
            this.panelProxyUserPwd.Location = new System.Drawing.Point(11, 95);
            this.panelProxyUserPwd.Name = "panelProxyUserPwd";
            this.panelProxyUserPwd.Size = new System.Drawing.Size(339, 27);
            this.panelProxyUserPwd.TabIndex = 7;
            // 
            // tbProxyLogin
            // 
            this.tbProxyLogin.Location = new System.Drawing.Point(67, 3);
            this.tbProxyLogin.Name = "tbProxyLogin";
            this.tbProxyLogin.Size = new System.Drawing.Size(92, 20);
            this.tbProxyLogin.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(175, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Password:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Username:";
            // 
            // tbProxyPassword
            // 
            this.tbProxyPassword.Location = new System.Drawing.Point(239, 3);
            this.tbProxyPassword.Name = "tbProxyPassword";
            this.tbProxyPassword.PasswordChar = '*';
            this.tbProxyPassword.Size = new System.Drawing.Size(92, 20);
            this.tbProxyPassword.TabIndex = 4;
            // 
            // comboProxyAuthType
            // 
            this.comboProxyAuthType.FormattingEnabled = true;
            this.comboProxyAuthType.Location = new System.Drawing.Point(78, 70);
            this.comboProxyAuthType.Name = "comboProxyAuthType";
            this.comboProxyAuthType.Size = new System.Drawing.Size(133, 21);
            this.comboProxyAuthType.TabIndex = 3;
            this.comboProxyAuthType.SelectedIndexChanged += new System.EventHandler(this.comboProxyAuthType_SelectedIndexChanged);
            // 
            // panelProxyHostPort
            // 
            this.panelProxyHostPort.Controls.Add(this.tbProxyHost);
            this.panelProxyHostPort.Controls.Add(this.LabelProxyAddress);
            this.panelProxyHostPort.Controls.Add(this.label10);
            this.panelProxyHostPort.Controls.Add(this.tbProxyPort);
            this.panelProxyHostPort.Location = new System.Drawing.Point(11, 35);
            this.panelProxyHostPort.Name = "panelProxyHostPort";
            this.panelProxyHostPort.Size = new System.Drawing.Size(293, 27);
            this.panelProxyHostPort.TabIndex = 6;
            // 
            // tbProxyHost
            // 
            this.tbProxyHost.Location = new System.Drawing.Point(40, 3);
            this.tbProxyHost.Name = "tbProxyHost";
            this.tbProxyHost.Size = new System.Drawing.Size(141, 20);
            this.tbProxyHost.TabIndex = 2;
            // 
            // LabelProxyAddress
            // 
            this.LabelProxyAddress.AutoSize = true;
            this.LabelProxyAddress.Location = new System.Drawing.Point(2, 6);
            this.LabelProxyAddress.Name = "LabelProxyAddress";
            this.LabelProxyAddress.Size = new System.Drawing.Size(32, 13);
            this.LabelProxyAddress.TabIndex = 0;
            this.LabelProxyAddress.Text = "Host:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(187, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Port:";
            // 
            // tbProxyPort
            // 
            this.tbProxyPort.Location = new System.Drawing.Point(222, 3);
            this.tbProxyPort.Name = "tbProxyPort";
            this.tbProxyPort.Size = new System.Drawing.Size(59, 20);
            this.tbProxyPort.TabIndex = 2;
            // 
            // cbDirectConnection
            // 
            this.cbDirectConnection.AutoSize = true;
            this.cbDirectConnection.Location = new System.Drawing.Point(8, 8);
            this.cbDirectConnection.Name = "cbDirectConnection";
            this.cbDirectConnection.Size = new System.Drawing.Size(110, 17);
            this.cbDirectConnection.TabIndex = 8;
            this.cbDirectConnection.Text = "Direct connection";
            this.cbDirectConnection.UseVisualStyleBackColor = true;
            this.cbDirectConnection.CheckedChanged += new System.EventHandler(this.cbDirectConnection_CheckedChanged);
            // 
            // cbSetProxy
            // 
            this.cbSetProxy.AutoSize = true;
            this.cbSetProxy.Location = new System.Drawing.Point(3, 12);
            this.cbSetProxy.Name = "cbSetProxy";
            this.cbSetProxy.Size = new System.Drawing.Size(70, 17);
            this.cbSetProxy.TabIndex = 1;
            this.cbSetProxy.Text = "Set proxy";
            this.cbSetProxy.UseVisualStyleBackColor = true;
            this.cbSetProxy.CheckedChanged += new System.EventHandler(this.CbUseProxy_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageNormalExport);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(414, 424);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelRunModeNormal);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panelRunModeGoogleApps);
            this.tabPage1.Controls.Add(this.cbDisableDuplicatedItemsWarning);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.TbDateDelta);
            this.tabPage1.Controls.Add(this.cbCheckForBetaVersion);
            this.tabPage1.Controls.Add(this.cbBypassCertificatesCheck);
            this.tabPage1.Controls.Add(this.cbDisableUpdateCheck);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.BtnSelectDir);
            this.tabPage1.Controls.Add(this.TbBackupDir);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(406, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelRunModeNormal
            // 
            this.panelRunModeNormal.BackColor = System.Drawing.Color.AliceBlue;
            this.panelRunModeNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRunModeNormal.Controls.Add(this.TbUsername);
            this.panelRunModeNormal.Controls.Add(this.TbPassword);
            this.panelRunModeNormal.Controls.Add(this.label6);
            this.panelRunModeNormal.Controls.Add(this.CbStorePassword);
            this.panelRunModeNormal.Controls.Add(this.label1);
            this.panelRunModeNormal.Location = new System.Drawing.Point(6, 38);
            this.panelRunModeNormal.Name = "panelRunModeNormal";
            this.panelRunModeNormal.Size = new System.Drawing.Size(325, 66);
            this.panelRunModeNormal.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbRunModeNormal);
            this.panel2.Controls.Add(this.rbRunModeGoogleApps);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Location = new System.Drawing.Point(5, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(239, 26);
            this.panel2.TabIndex = 25;
            // 
            // rbRunModeNormal
            // 
            this.rbRunModeNormal.AutoSize = true;
            this.rbRunModeNormal.Location = new System.Drawing.Point(68, 3);
            this.rbRunModeNormal.Name = "rbRunModeNormal";
            this.rbRunModeNormal.Size = new System.Drawing.Size(58, 17);
            this.rbRunModeNormal.TabIndex = 21;
            this.rbRunModeNormal.TabStop = true;
            this.rbRunModeNormal.Text = "Normal";
            this.rbRunModeNormal.UseVisualStyleBackColor = true;
            this.rbRunModeNormal.CheckedChanged += new System.EventHandler(this.rbRunModeNormal_CheckedChanged);
            // 
            // rbRunModeGoogleApps
            // 
            this.rbRunModeGoogleApps.AutoSize = true;
            this.rbRunModeGoogleApps.Location = new System.Drawing.Point(132, 3);
            this.rbRunModeGoogleApps.Name = "rbRunModeGoogleApps";
            this.rbRunModeGoogleApps.Size = new System.Drawing.Size(86, 17);
            this.rbRunModeGoogleApps.TabIndex = 22;
            this.rbRunModeGoogleApps.TabStop = true;
            this.rbRunModeGoogleApps.Text = "Google Apps";
            this.rbRunModeGoogleApps.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Run mode:";
            // 
            // panelRunModeGoogleApps
            // 
            this.panelRunModeGoogleApps.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panelRunModeGoogleApps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRunModeGoogleApps.Controls.Add(this.panelGAppsUserName);
            this.panelRunModeGoogleApps.Controls.Add(this.cbUseOnlyOauth);
            this.panelRunModeGoogleApps.Controls.Add(this.label19);
            this.panelRunModeGoogleApps.Controls.Add(this.TbDomain);
            this.panelRunModeGoogleApps.Controls.Add(this.label18);
            this.panelRunModeGoogleApps.Controls.Add(this.TbOAuthSecret);
            this.panelRunModeGoogleApps.Location = new System.Drawing.Point(30, 116);
            this.panelRunModeGoogleApps.Name = "panelRunModeGoogleApps";
            this.panelRunModeGoogleApps.Size = new System.Drawing.Size(301, 147);
            this.panelRunModeGoogleApps.TabIndex = 19;
            // 
            // panelGAppsUserName
            // 
            this.panelGAppsUserName.Controls.Add(this.label22);
            this.panelGAppsUserName.Controls.Add(this.tbGAUserName);
            this.panelGAppsUserName.Controls.Add(this.tbGAPassword);
            this.panelGAppsUserName.Controls.Add(this.label20);
            this.panelGAppsUserName.Location = new System.Drawing.Point(-1, 80);
            this.panelGAppsUserName.Name = "panelGAppsUserName";
            this.panelGAppsUserName.Size = new System.Drawing.Size(290, 62);
            this.panelGAppsUserName.TabIndex = 24;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 13);
            this.label22.TabIndex = 20;
            this.label22.Text = "Username:";
            // 
            // tbGAUserName
            // 
            this.tbGAUserName.Location = new System.Drawing.Point(85, 6);
            this.tbGAUserName.Name = "tbGAUserName";
            this.tbGAUserName.Size = new System.Drawing.Size(108, 20);
            this.tbGAUserName.TabIndex = 21;
            // 
            // tbGAPassword
            // 
            this.tbGAPassword.Location = new System.Drawing.Point(85, 33);
            this.tbGAPassword.Name = "tbGAPassword";
            this.tbGAPassword.PasswordChar = '*';
            this.tbGAPassword.Size = new System.Drawing.Size(108, 20);
            this.tbGAPassword.TabIndex = 22;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 36);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 19;
            this.label20.Text = "Password:";
            // 
            // cbUseOnlyOauth
            // 
            this.cbUseOnlyOauth.AutoSize = true;
            this.cbUseOnlyOauth.Location = new System.Drawing.Point(7, 6);
            this.cbUseOnlyOauth.Name = "cbUseOnlyOauth";
            this.cbUseOnlyOauth.Size = new System.Drawing.Size(100, 17);
            this.cbUseOnlyOauth.TabIndex = 23;
            this.cbUseOnlyOauth.Text = "Use only OAuth";
            this.cbUseOnlyOauth.UseVisualStyleBackColor = true;
            this.cbUseOnlyOauth.CheckedChanged += new System.EventHandler(this.cbUseOnlyOauth_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 59);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "OAuth secret:";
            // 
            // TbDomain
            // 
            this.TbDomain.Location = new System.Drawing.Point(84, 29);
            this.TbDomain.Name = "TbDomain";
            this.TbDomain.Size = new System.Drawing.Size(205, 20);
            this.TbDomain.TabIndex = 16;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(46, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "Domain:";
            // 
            // TbOAuthSecret
            // 
            this.TbOAuthSecret.Location = new System.Drawing.Point(84, 55);
            this.TbOAuthSecret.Name = "TbOAuthSecret";
            this.TbOAuthSecret.Size = new System.Drawing.Size(205, 20);
            this.TbOAuthSecret.TabIndex = 17;
            // 
            // cbDisableDuplicatedItemsWarning
            // 
            this.cbDisableDuplicatedItemsWarning.AutoSize = true;
            this.cbDisableDuplicatedItemsWarning.Location = new System.Drawing.Point(14, 326);
            this.cbDisableDuplicatedItemsWarning.Name = "cbDisableDuplicatedItemsWarning";
            this.cbDisableDuplicatedItemsWarning.Size = new System.Drawing.Size(190, 17);
            this.cbDisableDuplicatedItemsWarning.TabIndex = 14;
            this.cbDisableDuplicatedItemsWarning.Text = "Disable \"duplicated items\" warning";
            this.cbDisableDuplicatedItemsWarning.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 234);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Date delta (seconds):";
            // 
            // TbDateDelta
            // 
            this.TbDateDelta.Location = new System.Drawing.Point(124, 231);
            this.TbDateDelta.Name = "TbDateDelta";
            this.TbDateDelta.Size = new System.Drawing.Size(37, 20);
            this.TbDateDelta.TabIndex = 12;
            // 
            // cbCheckForBetaVersion
            // 
            this.cbCheckForBetaVersion.AutoSize = true;
            this.cbCheckForBetaVersion.Location = new System.Drawing.Point(14, 280);
            this.cbCheckForBetaVersion.Name = "cbCheckForBetaVersion";
            this.cbCheckForBetaVersion.Size = new System.Drawing.Size(167, 17);
            this.cbCheckForBetaVersion.TabIndex = 11;
            this.cbCheckForBetaVersion.Text = "Also check for BETA releases";
            this.cbCheckForBetaVersion.UseVisualStyleBackColor = true;
            // 
            // cbBypassCertificatesCheck
            // 
            this.cbBypassCertificatesCheck.AutoSize = true;
            this.cbBypassCertificatesCheck.Location = new System.Drawing.Point(14, 303);
            this.cbBypassCertificatesCheck.Name = "cbBypassCertificatesCheck";
            this.cbBypassCertificatesCheck.Size = new System.Drawing.Size(273, 17);
            this.cbBypassCertificatesCheck.TabIndex = 10;
            this.cbBypassCertificatesCheck.Text = "Bypass https certificates checks (... at your own risk)";
            this.cbBypassCertificatesCheck.UseVisualStyleBackColor = true;
            // 
            // cbDisableUpdateCheck
            // 
            this.cbDisableUpdateCheck.AutoSize = true;
            this.cbDisableUpdateCheck.Location = new System.Drawing.Point(14, 257);
            this.cbDisableUpdateCheck.Name = "cbDisableUpdateCheck";
            this.cbDisableUpdateCheck.Size = new System.Drawing.Size(130, 17);
            this.cbDisableUpdateCheck.TabIndex = 10;
            this.cbDisableUpdateCheck.Text = "Disable update check";
            this.cbDisableUpdateCheck.UseVisualStyleBackColor = true;
            // 
            // tabPageNormalExport
            // 
            this.tabPageNormalExport.Controls.Add(this.cbEnableMultiExport);
            this.tabPageNormalExport.Controls.Add(this.panelMultiExport);
            this.tabPageNormalExport.Controls.Add(this.panelSingleExport);
            this.tabPageNormalExport.Location = new System.Drawing.Point(4, 22);
            this.tabPageNormalExport.Name = "tabPageNormalExport";
            this.tabPageNormalExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNormalExport.Size = new System.Drawing.Size(406, 398);
            this.tabPageNormalExport.TabIndex = 1;
            this.tabPageNormalExport.Text = "Export format";
            this.tabPageNormalExport.UseVisualStyleBackColor = true;
            // 
            // cbEnableMultiExport
            // 
            this.cbEnableMultiExport.AutoSize = true;
            this.cbEnableMultiExport.Location = new System.Drawing.Point(3, 174);
            this.cbEnableMultiExport.Name = "cbEnableMultiExport";
            this.cbEnableMultiExport.Size = new System.Drawing.Size(115, 17);
            this.cbEnableMultiExport.TabIndex = 13;
            this.cbEnableMultiExport.Text = "Enable multi-export";
            this.cbEnableMultiExport.UseVisualStyleBackColor = true;
            this.cbEnableMultiExport.CheckedChanged += new System.EventHandler(this.cbEnableMultiExport_CheckedChanged);
            // 
            // panelMultiExport
            // 
            this.panelMultiExport.Controls.Add(this.clbDrawFormat);
            this.panelMultiExport.Controls.Add(this.clbPresFormat);
            this.panelMultiExport.Controls.Add(this.clbSprShFormat);
            this.panelMultiExport.Controls.Add(this.clbDocFormat);
            this.panelMultiExport.Controls.Add(this.label11);
            this.panelMultiExport.Controls.Add(this.label13);
            this.panelMultiExport.Controls.Add(this.label16);
            this.panelMultiExport.Controls.Add(this.label14);
            this.panelMultiExport.Location = new System.Drawing.Point(10, 6);
            this.panelMultiExport.Name = "panelMultiExport";
            this.panelMultiExport.Size = new System.Drawing.Size(361, 156);
            this.panelMultiExport.TabIndex = 12;
            // 
            // clbDrawFormat
            // 
            this.clbDrawFormat.FormattingEnabled = true;
            this.clbDrawFormat.Location = new System.Drawing.Point(276, 25);
            this.clbDrawFormat.Name = "clbDrawFormat";
            this.clbDrawFormat.Size = new System.Drawing.Size(70, 109);
            this.clbDrawFormat.TabIndex = 7;
            // 
            // clbPresFormat
            // 
            this.clbPresFormat.FormattingEnabled = true;
            this.clbPresFormat.Location = new System.Drawing.Point(188, 25);
            this.clbPresFormat.Name = "clbPresFormat";
            this.clbPresFormat.Size = new System.Drawing.Size(70, 109);
            this.clbPresFormat.TabIndex = 7;
            // 
            // clbSprShFormat
            // 
            this.clbSprShFormat.FormattingEnabled = true;
            this.clbSprShFormat.Location = new System.Drawing.Point(102, 25);
            this.clbSprShFormat.Name = "clbSprShFormat";
            this.clbSprShFormat.Size = new System.Drawing.Size(70, 109);
            this.clbSprShFormat.TabIndex = 7;
            // 
            // clbDocFormat
            // 
            this.clbDocFormat.FormattingEnabled = true;
            this.clbDocFormat.Location = new System.Drawing.Point(16, 25);
            this.clbDocFormat.Name = "clbDocFormat";
            this.clbDocFormat.Size = new System.Drawing.Size(70, 109);
            this.clbDocFormat.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(102, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Spreadsheet:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Document:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(273, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Drawing:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(185, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Presentation:";
            // 
            // panelSingleExport
            // 
            this.panelSingleExport.Controls.Add(this.cbDrawFormat);
            this.panelSingleExport.Controls.Add(this.cbPresFormat);
            this.panelSingleExport.Controls.Add(this.label4);
            this.panelSingleExport.Controls.Add(this.cbSprShFormat);
            this.panelSingleExport.Controls.Add(this.label3);
            this.panelSingleExport.Controls.Add(this.label17);
            this.panelSingleExport.Controls.Add(this.cbDocFormat);
            this.panelSingleExport.Controls.Add(this.label5);
            this.panelSingleExport.Location = new System.Drawing.Point(377, 31);
            this.panelSingleExport.Name = "panelSingleExport";
            this.panelSingleExport.Size = new System.Drawing.Size(279, 133);
            this.panelSingleExport.TabIndex = 9;
            // 
            // cbDrawFormat
            // 
            this.cbDrawFormat.FormattingEnabled = true;
            this.cbDrawFormat.Location = new System.Drawing.Point(89, 99);
            this.cbDrawFormat.Name = "cbDrawFormat";
            this.cbDrawFormat.Size = new System.Drawing.Size(90, 21);
            this.cbDrawFormat.TabIndex = 8;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Drawing:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panelProxy);
            this.tabPage3.Controls.Add(this.cbSetProxy);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(406, 398);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Proxy";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbMultiExpCalc
            // 
            this.tbMultiExpCalc.Location = new System.Drawing.Point(3, 40);
            this.tbMultiExpCalc.Name = "tbMultiExpCalc";
            this.tbMultiExpCalc.Size = new System.Drawing.Size(142, 20);
            this.tbMultiExpCalc.TabIndex = 0;
            // 
            // tbMultiExpDoc
            // 
            this.tbMultiExpDoc.Location = new System.Drawing.Point(3, 13);
            this.tbMultiExpDoc.Name = "tbMultiExpDoc";
            this.tbMultiExpDoc.Size = new System.Drawing.Size(142, 20);
            this.tbMultiExpDoc.TabIndex = 0;
            // 
            // tbMultiExpPres
            // 
            this.tbMultiExpPres.Location = new System.Drawing.Point(3, 67);
            this.tbMultiExpPres.Name = "tbMultiExpPres";
            this.tbMultiExpPres.Size = new System.Drawing.Size(142, 20);
            this.tbMultiExpPres.TabIndex = 0;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 481);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Config";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.panelProxy.ResumeLayout(false);
            this.panelProxy.PerformLayout();
            this.panelProxy2.ResumeLayout(false);
            this.panelProxy2.PerformLayout();
            this.panelProxyUserPwd.ResumeLayout(false);
            this.panelProxyUserPwd.PerformLayout();
            this.panelProxyHostPort.ResumeLayout(false);
            this.panelProxyHostPort.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panelRunModeNormal.ResumeLayout(false);
            this.panelRunModeNormal.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelRunModeGoogleApps.ResumeLayout(false);
            this.panelRunModeGoogleApps.PerformLayout();
            this.panelGAppsUserName.ResumeLayout(false);
            this.panelGAppsUserName.PerformLayout();
            this.tabPageNormalExport.ResumeLayout(false);
            this.tabPageNormalExport.PerformLayout();
            this.panelMultiExport.ResumeLayout(false);
            this.panelMultiExport.PerformLayout();
            this.panelSingleExport.ResumeLayout(false);
            this.panelSingleExport.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbUsername;
        private System.Windows.Forms.TextBox TbBackupDir;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSelectDir;
        private System.Windows.Forms.ComboBox cbDocFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPresFormat;
        private System.Windows.Forms.ComboBox cbSprShFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbPassword;
        private System.Windows.Forms.CheckBox CbStorePassword;
        private System.Windows.Forms.CheckBox cbSetProxy;
        private System.Windows.Forms.Label LabelProxyAddress;
        private System.Windows.Forms.TextBox tbProxyHost;
        private System.Windows.Forms.ComboBox comboProxyAuthType;
        private System.Windows.Forms.TextBox tbProxyPassword;
        private System.Windows.Forms.TextBox tbProxyLogin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbProxyPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panelProxy;
        private System.Windows.Forms.Panel panelProxyHostPort;
        private System.Windows.Forms.Panel panelProxyUserPwd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageNormalExport;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panelProxy2;
        private System.Windows.Forms.CheckBox cbDirectConnection;
        private System.Windows.Forms.ComboBox comboProxyHostSource;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbDisableUpdateCheck;
        private System.Windows.Forms.TextBox tbMultiExpCalc;
        private System.Windows.Forms.TextBox tbMultiExpDoc;
        private System.Windows.Forms.TextBox tbMultiExpPres;
        private System.Windows.Forms.Panel panelSingleExport;
        private System.Windows.Forms.Panel panelMultiExport;
        private System.Windows.Forms.CheckBox cbEnableMultiExport;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckedListBox clbDocFormat;
        private System.Windows.Forms.CheckedListBox clbPresFormat;
        private System.Windows.Forms.CheckedListBox clbSprShFormat;
        private System.Windows.Forms.CheckBox cbBypassCertificatesCheck;
        private System.Windows.Forms.CheckBox cbCheckForBetaVersion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TbDateDelta;
        private System.Windows.Forms.CheckedListBox clbDrawFormat;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbDrawFormat;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox cbDisableDuplicatedItemsWarning;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox TbOAuthSecret;
        private System.Windows.Forms.TextBox TbDomain;
        private System.Windows.Forms.Panel panelRunModeGoogleApps;
        private System.Windows.Forms.RadioButton rbRunModeGoogleApps;
        private System.Windows.Forms.RadioButton rbRunModeNormal;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelRunModeNormal;
        private System.Windows.Forms.CheckBox cbUseOnlyOauth;
        private System.Windows.Forms.TextBox tbGAUserName;
        private System.Windows.Forms.TextBox tbGAPassword;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panelGAppsUserName;
    }
}