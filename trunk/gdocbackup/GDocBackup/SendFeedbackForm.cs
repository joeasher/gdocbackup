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
using System.Collections.Specialized;
using System.Net;
using System.IO;
using GDocBackupLib;

namespace GDocBackup
{
    public partial class SendFeedbackForm : Form
    {

        public String DataTitle
        {
            set { this.LblTitle.Text = value; }
        }

        public String DataBody
        {
            set { this.TbData.Text = value; }
        }

        public SendFeedbackForm()
        {
            InitializeComponent();
        }

        private void SendFeedback_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Logo;
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            string body =
                "EMAIL=" + this.TbEmail.Text + Environment.NewLine +
                new String('-', 40) + Environment.NewLine +
                this.TbData.Text;

            NameValueCollection paramColl = new NameValueCollection();
            paramColl.Add("AppName", "GDocBackup");
            paramColl.Add("MsgType", "Feedback");
            paramColl.Add("MsgTitle", this.LblTitle.Text);
            paramColl.Add("MsgBody", body);

            Properties.Settings conf = Properties.Settings.Default;
            IWebProxy webproxy = Utility.GetProxy(
                conf.ProxyExplicit,
                conf.ProxyDirectConnection,
                conf.ProxyHostPortSource,
                conf.ProxyHost,
                conf.ProxyPort,
                conf.ProxyAuthMode,
                conf.ProxyUsername,
                conf.ProxyPassword);

            SendFeedback sf = new SendFeedback();
            if (sf.Exec(paramColl, webproxy))
            {
                MessageBox.Show("Informations correctly sent. Thank you.");
            }
            else
            {
                MessageBox.Show(
                    "Error during transmission [" +
                    ((sf.InternalException != null) ? sf.InternalException.Message : "?") +
                    "]");
            }

            this.Close();
        }


    }
}