using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Google.GData.Client;
using Google.Documents;
using System.IO;

namespace DevAppGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }





        // ----------------------------------------------------------------------------------------------------


        private void btnGetDocList_Click(object sender, EventArgs e)
        {
            if (_threadGetDocList != null && _threadGetDocList.IsAlive)
            {
                MessageBox.Show("Already running");
                return;
            }

            textBox1.Text = "";
            _threadGetDocList = new Thread(GetDocListExec);
            _threadGetDocList.IsBackground = true;
            _threadGetDocList.Name = "Thread_GetDocList";
            _threadGetDocList.Start();
        }

        private Thread _threadGetDocList;


        private void GetDocListExec()
        {
            WriteMessage("*** GetDocList ***");
            WriteMessage("--- START ---");

            try
            {
                string username = tbUserName.Text; ;
                string password = tbPassword.Text;

                GDataCredentials credentials = new GDataCredentials(username, password);
                RequestSettings settings = new RequestSettings("GDocBackup", credentials);
                settings.AutoPaging = true;
                settings.PageSize = 100;
                DocumentsRequest request = new DocumentsRequest(settings);

                Feed<Document> feed = request.GetEverything();
                List<Document> docs = new List<Document>();
                foreach (Document entry in feed.Entries)
                    docs.Add(entry);

                StreamWriter outFile = new StreamWriter("doclist.txt", false);
                StreamWriter outFile2 = new StreamWriter("doclistdetails.txt", false);

                WriteMessage("Exporting document list. Please wait...");

                foreach (Document doc in docs)
                {
                    string s = doc.Title + "\t" + doc.ResourceId;
                    //WriteMessage(s);
                    outFile.WriteLine(s);
                    outFile2.WriteLine(s);
                    foreach (string pf in doc.ParentFolders)
                        outFile2.WriteLine("\t\t\t" + pf);
                }

                WriteMessage("Created file: doclist.txt");
                WriteMessage("Created file: doclistdetails.txt");


                outFile.Close();
                outFile2.Close();
            }
            catch (Exception ex)
            {
                WriteMessage("EXCEPTION: " + ex.ToString());
            }


            WriteMessage("--- END ---");


        }


        private void WriteMessage(string s)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { WriteMessage(s); });
            }
            else
            {
                textBox1.Text = s + Environment.NewLine + textBox1.Text;
            }
        }


    }
}
