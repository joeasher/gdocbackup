namespace GDocBackup
{
    partial class LoginForm
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
            this.PasswordTB = new System.Windows.Forms.TextBox();
            this.UsernameTB = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PasswordTB
            // 
            this.PasswordTB.Location = new System.Drawing.Point(81, 40);
            this.PasswordTB.Name = "PasswordTB";
            this.PasswordTB.PasswordChar = '*';
            this.PasswordTB.Size = new System.Drawing.Size(182, 20);
            this.PasswordTB.TabIndex = 0;
            this.PasswordTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PasswordTB_KeyUp);
            // 
            // UsernameTB
            // 
            this.UsernameTB.Location = new System.Drawing.Point(81, 12);
            this.UsernameTB.Name = "UsernameTB";
            this.UsernameTB.ReadOnly = true;
            this.UsernameTB.Size = new System.Drawing.Size(182, 20);
            this.UsernameTB.TabIndex = 7;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(171, 66);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(67, 28);
            this.Cancel.TabIndex = 9;
            this.Cancel.Text = "&Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(98, 66);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(67, 28);
            this.Login.TabIndex = 10;
            this.Login.Text = "&Login";
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Password:";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 105);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PasswordTB);
            this.Controls.Add(this.UsernameTB);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Login);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasswordTB;
        private System.Windows.Forms.TextBox UsernameTB;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}