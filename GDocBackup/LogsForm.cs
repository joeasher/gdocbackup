using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GDocBackup
{
    public partial class LogsForm : Form
    {
        public string[] Logs = null;

        public LogsForm()
        {
            InitializeComponent();
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Logo;
            if (Logs != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in Logs)
                    sb.AppendLine(s);
                this.textBox1.Text = sb.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}