using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GDocBackup
{
    public partial class LoginForm : Form
    {

        public string UserName { get { return this.UsernameTB.Text; } set { this.UsernameTB.Text = value; } }
        public string Password { get { return this.PasswordTB.Text; } }
        public string BackupDir { get { return this.BackupDirTB.Text; } set { this.BackupDirTB.Text = value; } }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Logo;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void PasswordTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


    }
}