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
            // note: Process.Start crashes on some systems (issue 17)
            try { System.Diagnostics.Process.Start((sender as LinkLabel).Text); }
            catch (Exception) { }
        }

    }
}