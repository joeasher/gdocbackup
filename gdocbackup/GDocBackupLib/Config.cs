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
using System.Text;
using Google.Documents;
using System.Net;

namespace GDocBackupLib
{
    public class Config
    {
        public string userName;
        public string password;
        public bool appsMode;
        public string appsDomain;
        public string appsOAuthSecret;
        public bool appsOnlyOAuth;
        public string outDir;
        public bool downloadAll;
        public Document.DownloadType[] docExpType;
        public Document.DownloadType[] sprdExpType;
        public Document.DownloadType[] presExpType;
        public Document.DownloadType[] drawExpType;
        public IWebProxy iwebproxy;
        public bool bypassHttpsChecks;
        public bool debugMode;
        public int? dateDiff;        


        public Config()
        {
        }

        public Config(string userName,
            string password,
            string outDir,
            bool downloadAll,
            Document.DownloadType[] docExpType,
            Document.DownloadType[] sprdExpType,
            Document.DownloadType[] presExpType,
            Document.DownloadType[] drawExpType,
            IWebProxy webproxy,
            bool bypassHttpsChecks,
            bool debugMode,
            int? dateDiff,
            bool appsMode,
            string appsDomain,
            string appsOAuthSecret,
            bool appsOnlyOAuth)
        {
            this.userName = userName;
            this.password = password;
            this.outDir = outDir;
            this.downloadAll = downloadAll;
            this.docExpType = docExpType;
            this.sprdExpType = sprdExpType;
            this.presExpType = presExpType;
            this.drawExpType = drawExpType;
            this.iwebproxy = webproxy;
            this.bypassHttpsChecks = bypassHttpsChecks;
            this.debugMode = debugMode;
            this.dateDiff = dateDiff;
            this.appsMode = appsMode;
            this.appsDomain = appsDomain;
            this.appsOAuthSecret = appsOAuthSecret;
            this.appsOnlyOAuth = appsOnlyOAuth;
        }

    }
}
