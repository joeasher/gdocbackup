using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GDocBackup
{
    public partial class SimpleInputBox : Form
    {

        public String Title
        {
            set { this.Text = value; }
        }

        public String TextValue
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public bool MaskInput
        {
            set
            {
                if (value)
                    textBox1.PasswordChar = '*';
                else
                    textBox1.PasswordChar = (char)0;
            }
        }

        public SimpleInputBox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
