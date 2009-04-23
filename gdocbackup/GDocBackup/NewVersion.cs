using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GDocBackup
{
    public partial class NewVersion : Form
    {
        public Version LocalVersion;
        public Version RemoteVersion;

        public NewVersion()
        {
            InitializeComponent();
        }

        private void NewVersion_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Logo;
            lblYourVersion.Text += this.LocalVersion.ToString();
            lblNewVersion.Text += this.RemoteVersion.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }
        
    }
}