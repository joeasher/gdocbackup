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
    public partial class SendFeedback : Form
    {

        public String DataTitle
        {
            get { return this.LblTitle.Text; }
            set { this.LblTitle.Text = value; }
        }

        public String DataBody
        {
            get { return this.TbData.Text; }
            set { this.TbData.Text = value; }
        }


        public SendFeedback()
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
            IWebProxy webproxy = Utility.GetProxy(conf.ProxyExplicit, conf.ProxyDirectConnection, conf.ProxyHostPortSource, conf.ProxyHost, conf.ProxyPort, conf.ProxyAuthMode, conf.ProxyUsername, conf.ProxyPassword);
            String resOutput = ExecHttpPost("http://fhtino.appspot.com/clientfeedback", paramColl, webproxy);

            this.Close();
        }

        private string ExecHttpPost(string url, NameValueCollection parameters, IWebProxy proxy)
        {
            // prepare data
            List<String> tokens = new List<string>();
            foreach (string key in parameters.AllKeys)
                tokens.Add(key + "=" + MyUrlEncode(parameters[key]));
            string data = String.Join("&", tokens.ToArray());


            Encoding reqEncoding = Encoding.GetEncoding("ISO-8859-1");
            byte[] dataBytes = reqEncoding.GetBytes(data);

            // open http socket
            HttpWebRequest httpReq = (HttpWebRequest)HttpWebRequest.Create(url);
            httpReq.ContentType = "application/x-www-form-urlencoded";
            httpReq.Method = "POST";
            httpReq.Timeout = 5000;
            httpReq.ReadWriteTimeout = 5000;
            if (proxy != null)
                httpReq.Proxy = new WebProxy();

            // send http request body
            Stream reqStr = httpReq.GetRequestStream();
            reqStr.Write(dataBytes, 0, dataBytes.Length);
            reqStr.Close();

            // get http response
            HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();

            // read response
            Encoding resEncoding = Encoding.GetEncoding(httpRes.CharacterSet);
            StreamReader rdr = new StreamReader(httpRes.GetResponseStream(), resEncoding);
            string resOutput = rdr.ReadToEnd();
            rdr.Close();

            return resOutput;
        }

        private static string MyUrlEncode(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            // convert string to utf8 bytes
            byte[] buffer = Encoding.UTF8.GetBytes(s);

            // escape "special" characters
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < buffer.Length; i++)
            {
                char c = (char)buffer[i];
                if (c == ' ')
                    sb.Append('+');
                else if (IsSafe(c))
                    sb.Append(c);
                else
                    sb.Append("%" + ((int)c).ToString("X2"));
            }

            return sb.ToString();
        }

        internal static bool IsSafe(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '\'':
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }

    }
}