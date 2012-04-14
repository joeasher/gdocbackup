/*
   Copyright 2009-2012  Fabrizio Accatino

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

        private void sendLogsToGDocBackupDevelopersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Logs == null)
                return;

            using (SendFeedbackForm sf = new SendFeedbackForm())
            {
                sf.DataTitle = "GDocBackup Logs";
                sf.DataBody = String.Join(Environment.NewLine, Logs);
                sf.ShowDialog();
            }
        }
    }
}