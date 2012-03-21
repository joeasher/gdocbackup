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
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.IO;

namespace GDocBackup
{
    internal class SendFeedback
    {

        private Exception _internalException;
        private String _responseContent;


        public Exception InternalException
        {
            get { return _internalException; }
        }


        public String ResponseContent
        {
            get { return _responseContent; }
        }


        /// <summary>
        /// ...
        /// </summary>
        public bool Exec(NameValueCollection parameters, IWebProxy proxy)
        {
            _internalException = null;
            _responseContent = null;

            try
            {
                String url = "http://fhtino.appspot.com/clientfeedback";

                // prepare data
                List<String> tokens = new List<string>();
                foreach (string key in parameters.AllKeys)
                    tokens.Add(key + "=" + MyUrlEncode(parameters[key]));
                string data = String.Join("&", tokens.ToArray());

                // truncate data > 200000 chars
                if (data.Length > 200000)
                    data = data.Substring(0, 200000) + "...TRUNCATED...";

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
                _responseContent = rdr.ReadToEnd();
                rdr.Close();

                return true;
            }
            catch (Exception ex)
            {
                _internalException = ex;
                return false;
            }
        }


        /// <summary>
        /// ...
        /// </summary>
        private string MyUrlEncode(string s)
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

        private bool IsSafe(char ch)
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
