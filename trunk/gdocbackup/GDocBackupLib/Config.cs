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
