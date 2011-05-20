namespace DevAppGUI
{
    partial class MainForm
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
            this.btnGetDocList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetDocList
            // 
            this.btnGetDocList.Location = new System.Drawing.Point(12, 12);
            this.btnGetDocList.Name = "btnGetDocList";
            this.btnGetDocList.Size = new System.Drawing.Size(106, 39);
            this.btnGetDocList.TabIndex = 0;
            this.btnGetDocList.Text = "GetDocList";
            this.btnGetDocList.UseVisualStyleBackColor = true;
            this.btnGetDocList.Click += new System.EventHandler(this.btnGetDocList_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 211);
            this.Controls.Add(this.btnGetDocList);
            this.Name = "MainForm";
            this.Text = "GDocBackup - DevAppGUI";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetDocList;
    }
}

