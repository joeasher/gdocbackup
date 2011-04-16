namespace GDocBackup
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.label1 = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictRotor = new System.Windows.Forms.PictureBox();
            this.lblCheckNewVer = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LblGoogleVer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictRotor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "GDocBackup";
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVersion.ForeColor = System.Drawing.Color.Maroon;
            this.LblVersion.Location = new System.Drawing.Point(9, 39);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(203, 14);
            this.LblVersion.TabIndex = 1;
            this.LblVersion.Text = "Version  ";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(9, 92);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(161, 15);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://gs.fhtino.it/gdocbackup";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictRotor
            // 
            this.pictRotor.Image = global::GDocBackup.Properties.Resources.running;
            this.pictRotor.Location = new System.Drawing.Point(12, 242);
            this.pictRotor.Name = "pictRotor";
            this.pictRotor.Size = new System.Drawing.Size(20, 20);
            this.pictRotor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictRotor.TabIndex = 7;
            this.pictRotor.TabStop = false;
            // 
            // lblCheckNewVer
            // 
            this.lblCheckNewVer.Location = new System.Drawing.Point(38, 242);
            this.lblCheckNewVer.Name = "lblCheckNewVer";
            this.lblCheckNewVer.Size = new System.Drawing.Size(231, 20);
            this.lblCheckNewVer.TabIndex = 8;
            this.lblCheckNewVer.Text = "Checking for updates...";
            this.lblCheckNewVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 122);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(337, 103);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // LblGoogleVer
            // 
            this.LblGoogleVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblGoogleVer.ForeColor = System.Drawing.Color.Maroon;
            this.LblGoogleVer.Location = new System.Drawing.Point(9, 57);
            this.LblGoogleVer.Name = "LblGoogleVer";
            this.LblGoogleVer.Size = new System.Drawing.Size(278, 18);
            this.LblGoogleVer.TabIndex = 1;
            this.LblGoogleVer.Text = "Lib version";
            this.LblGoogleVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 279);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblCheckNewVer);
            this.Controls.Add(this.pictRotor);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.LblGoogleVer);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictRotor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblVersion;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictRotor;
        private System.Windows.Forms.Label lblCheckNewVer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LblGoogleVer;
    }
}