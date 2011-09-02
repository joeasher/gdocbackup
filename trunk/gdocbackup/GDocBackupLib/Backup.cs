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
using Google.GData.Client;
using Google.Documents;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Reflection;
using Google.GData.Apps;


namespace GDocBackupLib
{

    /// <summary>
    /// Execute backup of docs 
    /// </summary>
    public class Backup
    {

        private Config _config;

        private Dictionary<string, string> _folderDict;
        private double _lastPercent = 0;
        private int _currentAppsUserIndex = -1;  // first user --> value = 0
        private int _totalAppUsers = -1;
        private Exception _lastException = null;
        private List<string> _duplicatedDocNames;


        /// <summary>
        /// Last exception
        /// </summary>
        public Exception LastException { get { return _lastException; } }


        /// <summary>
        /// List of duplicated doc names in the same folder
        /// </summary>
        public List<string> DuplicatedDocNames { get { return _duplicatedDocNames; } }


        /// <summary>
        /// Feedback event (gives informations about processing)
        /// </summary>
        public event EventHandler<FeedbackEventArgs> Feedback;

        private void DoFeedback(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            if (Feedback != null)
                Feedback(this, new FeedbackEventArgs(message, _lastPercent));
        }

        private void DoFeedback(string message, double percent)
        {
            if (_config.appsMode)
            {
                double x = 1.0 / _totalAppUsers;
                _lastPercent = x * _currentAppsUserIndex + percent * x;
                if (_lastPercent > 1.0)
                    _lastPercent = 1.0;   // non dovrebbe mai capitare.... ma non si sa mai!  :)
            }
            else
            {
                _lastPercent = percent;
            }
            System.Diagnostics.Debug.WriteLine(message);
            if (Feedback != null)
                Feedback(this, new FeedbackEventArgs(message, _lastPercent));
        }

        private void DoFeedback(FeedbackObject fo)
        {
            System.Diagnostics.Debug.WriteLine(fo.ToString());
            if (Feedback != null)
                Feedback(this, new FeedbackEventArgs("", _lastPercent, fo));
        }

        private void DoFeedbackDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            if (_config.debugMode)
            {
                if (Feedback != null)
                    Feedback(this, new FeedbackEventArgs(message, _lastPercent));
            }
        }


        /// <summary>
        /// [Constructor]
        /// </summary>
        /// <param name="conf"></param>
        public Backup(Config conf)
        {
            _config = conf;
        }


        /// <summary>
        /// Exec backup
        /// </summary>
        /// <returns>True: all OK.  False: there are errors</returns>
        public bool Exec()
        {
            try
            {
                int errorCount = this.ExecInternal();
                return (errorCount == 0);
            }
            catch (ThreadAbortException tae)
            {
                Thread.ResetAbort();
                _lastException = tae;
                DoFeedback("STOP (ThreadAbortException):  " + tae.Message, 0);
                return false;
            }
            catch (Exception ex)
            {
                _lastException = ex;
                DoFeedback("GLOBAL-ERROR:  " + ex.Message, 0);
                DoFeedback("GLOBAL-ERROR (exception details): " + ex.ToString(), 0);
                return false;
            }
        }


        /// <summary>
        /// ...
        /// </summary>
        private int ExecInternal()
        {
            DoFeedback(new string('*', 60));
            DoFeedback("****** START BACKUP PROCESS ******");
            AssemblyName assembName = Assembly.GetExecutingAssembly().GetName();
            DoFeedback(assembName.Name + " - ver. " + assembName.Version.ToString());

            int errorCount = -1;
            if (_config.appsMode == false)
            {
                errorCount = this.ExecBackupSingleUser(null);
            }
            else
            {
                errorCount = this.ExecInternalApps();
            }

            DoFeedback("****** END BACKUP PROCESS ******");
            return errorCount;
        }


        /// <summary>
        /// ....
        /// </summary>
        private int ExecInternalApps()
        {
            string domainAdminUsername = this.GetDomainAdminFullUserName();

            // Retrieve user list
            List<String> usernames = new List<string>();
            AppsService appsServ = new AppsService(_config.appsDomain, domainAdminUsername, _config.password);
            UserFeed usersFeed = appsServ.RetrieveAllUsers();
            foreach (UserEntry entry in usersFeed.Entries)
                usernames.Add(entry.Login.UserName);

            // Build output folders, one for each user
            foreach (string username in usernames)
            {
                string x = Path.Combine(_config.outDir, username);
                if (!Directory.Exists(x))
                    Directory.CreateDirectory(x);
            }

            // Do work fopr each user!
            int errCount = 0;
            _totalAppUsers = usernames.Count;
            for (int i = 0; i < usernames.Count; i++)
            {
                _currentAppsUserIndex = i;
                try
                {
                    errCount += this.ExecBackupSingleUser(usernames[i]);
                }
                catch (Exception ex)
                {
                    errCount++;
                    DoFeedback("ERROR: " + ex.ToString(), 0);
                }
            }

            return errCount;
        }



        /// <summary>
        /// ...
        /// </summary>
        private string GetDomainAdminFullUserName()
        {
            string domainAdminUsername = _config.userName;
            if (!domainAdminUsername.ToLower().EndsWith(_config.appsDomain.ToLower()))
                domainAdminUsername = domainAdminUsername + "@" + _config.appsDomain;
            return domainAdminUsername;
        }


        /// <summary>
        /// Exec backup (internal)
        /// </summary>
        private int ExecBackupSingleUser(string username)
        {
            DoFeedback(new string('-', 80));
            DoFeedback("--- ExecBackupSingleUser - username=" + username + " ---");
            DoFeedback(new string('-', 80));

            _lastException = null;
            _duplicatedDocNames = new List<string>();

            // Bypass Https checks?
            // I know, CertificatePolicy is deprecated. I should use ServerCertificateValidationCallback but Mono does not support it.  :(
            if (_config.bypassHttpsChecks)
            {
                DoFeedback("BypassHttpsCertCheck ACTIVE");
                ServicePointManager.CertificatePolicy = new BypassHttpsCertCheck();
            }

            // Setup credentials and connection
            DoFeedback("Setup connection & get doc list");
            RequestSettings settings;
            if (_config.appsMode == false)
            {
                GDataCredentials credentials = new GDataCredentials(_config.userName, _config.password);
                settings = new RequestSettings("GDocBackup", credentials);
                settings.AutoPaging = true;
                settings.PageSize = 100;
            }
            else
            {
                settings = new RequestSettings("GDocBackup", _config.appsDomain, _config.appsOAuthSecret, username, _config.appsDomain);
                settings.AutoPaging = true;
                settings.PageSize = 100;
                //settings.Maximum = 10000;  
            }

            DocumentsRequest request = new DocumentsRequest(settings);
            if (_config.iwebproxy != null)
                request.Proxy = _config.iwebproxy;


            // Get doc list from GDocs
            Feed<Document> feed = request.GetEverything();
            List<Document> docs = new List<Document>();
            foreach (Document entry in feed.Entries)
                docs.Add(entry);


            // Search for duplicated doc names in the same folder
            _duplicatedDocNames = this.FindDuplicatedNames(docs);
            DoFeedback("Duplicated Doc Names [" + _duplicatedDocNames.Count + "]");
            _duplicatedDocNames.ForEach(delegate(string s) { DoFeedback(" - " + s); });


            // Builds/updates local folder structure
            if (_config.appsMode)
                this.BuildFolders(null, docs, Path.Combine(_config.outDir, username));
            else
                this.BuildFolders(null, docs, _config.outDir);
            foreach (String k in _folderDict.Keys)
                DoFeedbackDebug("FolderDict: " + k + " --> " + _folderDict[k]);
            this.DumpAllDocInfo(docs);

            // Docs loop!
            int errorCount = 0;
            for (int i = 0; i < docs.Count; i++)
            {
                Document doc = docs[i];
                DoFeedback("ITEM: " + doc.Title + " (" + doc.Type + ") [" + (i + 1).ToString() + "/" + docs.Count + "]", ((double)i) / docs.Count);

                Document.DownloadType[] downloadTypes = null;
                switch (doc.Type)
                {
                    case Document.DocumentType.Document:
                        downloadTypes = _config.docExpType;
                        break;
                    case Document.DocumentType.Presentation:
                        downloadTypes = _config.presExpType;
                        break;
                    case Document.DocumentType.Spreadsheet:
                        downloadTypes = _config.sprdExpType;
                        break;
                    case Document.DocumentType.PDF:
                        downloadTypes = new Document.DownloadType[] { Document.DownloadType.pdf };
                        break;
                    case Document.DocumentType.Drawing:
                        downloadTypes = _config.drawExpType;
                        break;
                    case Document.DocumentType.Unknown:
                        downloadTypes = new Document.DownloadType[] { Document.DownloadType.zip };  // download format not used! It's only a "place-holder".
                        break;
                    default:
                        break;
                }


                if (downloadTypes != null)
                {
                    try
                    {
                        // * WorkAround for drawing *
                        // Detect if drawing and then force downloadtype to pdf
                        //bool isDrawing = doc.ResourceId.StartsWith("drawing:");   // drawing:14TBycKwlpXJ25N......
                        //if (isDrawing)
                        //    downloadTypes = new Document.DownloadType[] { Document.DownloadType.pdf };
                        // bool isDrawing = false;

                        foreach (Document.DownloadType downloadtype in downloadTypes)
                        {
                            // Build local file path
                            string outFolderPath;
                            if (doc.ParentFolders.Count == 0)
                            {
                                outFolderPath = _config.appsMode ? Path.Combine(_config.outDir, username) : _config.outDir;
                            }
                            else
                            {
                                DoFeedback("Try to get folder from dict using key=[" + doc.ParentFolders[0] + "]");
                                outFolderPath = _folderDict[doc.ParentFolders[0]];
                            }
                            string outFileFP =
                                (doc.Type == Document.DocumentType.Unknown) ?
                                    Path.Combine(outFolderPath, this.RemoveInvalidChars(doc.Title, true)) :
                                    Path.Combine(outFolderPath, this.RemoveInvalidChars(doc.Title, false) + "." + downloadtype.ToString());

                            // Get current local file in infos
                            FileInfo fi = new FileInfo(outFileFP);
                            DateTime locFileDateTime = fi.LastWriteTime;
                            DateTime gdocFileDateTime = doc.Updated;

                            // Mono and/or Ubuntu (...linux) does not support milliseconds info when saving DataTime to FileInfo.LastWriteTime. So... I remove it!   :)
                            locFileDateTime = this.RemoveMilliseconds(locFileDateTime);
                            gdocFileDateTime = this.RemoveMilliseconds(gdocFileDateTime);

                            bool downloadDoc = (!fi.Exists || _config.downloadAll);

                            if (_config.dateDiff.HasValue)
                            {
                                if (Math.Abs(locFileDateTime.Subtract(gdocFileDateTime).TotalSeconds) > _config.dateDiff.Value)
                                    downloadDoc = true;
                            }
                            else
                            {
                                if (locFileDateTime != gdocFileDateTime)
                                    downloadDoc = true;
                            }


                            if (downloadDoc)
                            {
                                DoFeedback("Start exporting " + doc.Title + "(Type=" + doc.Type + ") --> " + downloadtype.ToString());
                                Stream gdocStream = null;
                                try
                                {
                                    if (doc.Type == Document.DocumentType.Unknown)
                                    {
                                        String downloadUrl = doc.DocumentEntry.Content.Src.ToString();
                                        Uri downloadUri = _config.appsMode ?
                                            new Uri(downloadUrl + "&xoauth_requestor_id=" + this.GetDomainAdminFullUserName()) :
                                            new Uri(downloadUrl);
                                        gdocStream = request.Service.Query(downloadUri);
                                    }
                                    else if (doc.Type == Document.DocumentType.Document)
                                    {
                                        gdocStream = request.Download(doc, downloadtype.ToString());
                                    }
                                    else if (doc.Type == Document.DocumentType.Spreadsheet)
                                    {
                                        gdocStream = request.Download(doc, downloadtype.ToString());
                                    }
                                    else if (doc.Type == Document.DocumentType.Presentation)
                                    {
                                        gdocStream = request.Download(doc, downloadtype.ToString());
                                    }
                                    else if (doc.Type == Document.DocumentType.Drawing)
                                    {
                                        gdocStream = request.Download(doc, downloadtype.ToString());
                                    }
                                    else if (doc.Type != Document.DocumentType.PDF)
                                    {
                                        // *** ??? ***
                                        gdocStream = request.Download(doc, downloadtype);
                                    }
                                    else
                                    {
                                        // *** PDF ***
                                        if (_config.appsMode)
                                        {
                                            // add xoauth_requestor_id to the doc url
                                            string url = doc.DocumentEntry.Content.Src.ToString();
                                            doc.DocumentEntry.Content.Src = new AtomUri(url + "&xoauth_requestor_id=" + this.GetDomainAdminFullUserName());
                                        }
                                        gdocStream = request.Download(doc, null);
                                    }

                                    using (FileStream outFile = new FileStream(outFileFP, FileMode.Create, FileAccess.Write))
                                    {
                                        byte[] buffer = new byte[8192];
                                        int bytesRead;
                                        while ((bytesRead = gdocStream.Read(buffer, 0, buffer.Length)) > 0)
                                            outFile.Write(buffer, 0, bytesRead);
                                        outFile.Close();
                                    }
                                    gdocStream.Close();
                                }
                                finally
                                {
                                    if (gdocStream != null)
                                        gdocStream.Dispose();
                                }

                                new FileInfo(outFileFP).LastWriteTime = doc.Updated;
                                DoFeedback("End exporting " + doc.Title + "(Type=" + doc.Type + ") --> " + downloadtype.ToString());
                            }
                            else
                            {
                                DoFeedback("Skipped doc: " + doc.Title);
                            }

                            // Send Feedback                            
                            DoFeedback(new FeedbackObject(
                                (_config.appsMode ? username + "#" + doc.Title : doc.Title),
                                doc.Type.ToString(),
                                (doc.Type == Document.DocumentType.Unknown) ? "BIN" : downloadtype.ToString(),
                                downloadDoc ? "BCKUP" : "SKIP",
                                "", locFileDateTime, gdocFileDateTime));
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        DoFeedback("DOC-ERROR: " + ex.ToString());
                        DoFeedback(new FeedbackObject(
                            (_config.appsMode ? username + "#" + doc.Title : doc.Title),
                            doc.Type.ToString(),
                            "", "ERROR", "", null, null));
                    }
                }
                else
                {
                    if (doc.Type != Document.DocumentType.Folder)
                        DoFeedback(new FeedbackObject(doc.Title, doc.Type.ToString(), "", "NONE", "", null, null));
                }
            }

            return errorCount;
        }



        /// <summary>
        /// Builds (updates) output folders tree
        /// </summary>
        private void BuildFolders(Document parentDir, List<Document> docs, string currentPath)
        {
            if (parentDir == null)
                _folderDict = new Dictionary<string, string>();

            foreach (Document doc in docs)
            {
                if (doc.Type == Document.DocumentType.Folder)
                {
                    if (parentDir == null)
                    {
                        // Level = Zero
                        if (doc.ParentFolders.Count == 0)
                        {
                            string folderName = doc.Title;
                            folderName = this.RemoveInvalidChars(folderName, false);
                            string newCurrPath = Path.Combine(currentPath, folderName);

                            //OLD_folderDict.Add(doc.Id, newCurrPath);
                            if (!_folderDict.ContainsKey(doc.Self))
                            {
                                _folderDict.Add(doc.Self, newCurrPath);

                                if (!Directory.Exists(newCurrPath))
                                    Directory.CreateDirectory(newCurrPath);

                                BuildFolders(doc, docs, newCurrPath);
                            }
                        }
                    }
                    else
                    {
                        // Level > Zero
                        //OLD if (doc.ParentFolders.Contains(parentDir.Id))
                        if (doc.ParentFolders.Contains(parentDir.Self))
                        {
                            // child found!
                            string folderName = doc.Title;
                            folderName = this.RemoveInvalidChars(folderName, false);
                            string newCurrPath = Path.Combine(currentPath, folderName);

                            //OLD_folderDict.Add(doc.Id, newCurrPath);
                            if (!_folderDict.ContainsKey(doc.Self))
                            {
                                _folderDict.Add(doc.Self, newCurrPath);
                                if (!Directory.Exists(newCurrPath))
                                    Directory.CreateDirectory(newCurrPath);
                                BuildFolders(doc, docs, newCurrPath);
                            }
                            else
                            {
                                DoFeedbackDebug("WARNING: Folder already present in DICT. This is an open issue. Sorry. " + doc.Self + " - " + newCurrPath);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Removes invalids chars from string
        /// </summary>
        private string RemoveInvalidChars(string s, bool convertMultipleToSingleDot)
        {
            //StringBuilder x = new StringBuilder();
            //for (int i = 0; i < s.Length; i++)
            //{
            //    char c = s[i];
            //    if (Char.IsLetter(c) || Char.IsNumber(c) || c == '-' || c == '_' || c == '.')
            //        x.Append(c);
            //    else
            //        x.Append('_');
            //}            

            //return convertMultipleToSingleDot ?
            //    Regex.Replace(x.ToString(), "(\\.){1,}", ".") :
            //    Regex.Replace(x.ToString(), "(\\.)", "_");

            List<char> invalidList = new List<char>();
            invalidList.AddRange(Path.GetInvalidPathChars());
            invalidList.AddRange(Path.GetInvalidFileNameChars());

            char[] sChars = s.ToCharArray();
            for (int i = 0; i < sChars.Length; i++)
                if (invalidList.Contains(sChars[i]))
                    sChars[i] = '_';

            string sNew = new String(sChars);

            return convertMultipleToSingleDot ?
                Regex.Replace(sNew, "(\\.){1,}", ".") :
                Regex.Replace(sNew, "(\\.)", "_");
        }


        /// <summary>
        /// Removes milliseconds info from DateTime
        /// </summary>
        private DateTime RemoveMilliseconds(DateTime dt)
        {
            return new DateTime(
                dt.Year, dt.Month, dt.Day,
                dt.Hour, dt.Minute, dt.Second);
        }


        /// <summary>
        /// [Only for debug/testing]
        /// </summary>
        private void DumpAllDocInfo(List<Document> docs)
        {
            DoFeedbackDebug(new String('-', 80));
            DoFeedbackDebug("DUMP_ALL_DOC_INFO");
            foreach (Document doc in docs)
            {
                DoFeedbackDebug("*** " + doc.Title + " ***");
                DoFeedbackDebug("[ID] " + doc.Id);
                DoFeedbackDebug("[SELF] " + doc.Self);
                foreach (String pfid in doc.ParentFolders)
                    DoFeedbackDebug(" ----- PF> " + pfid);
            }
            DoFeedbackDebug(new String('-', 80));
        }


        /// <summary>
        /// Search for duplicates document names in the same folder
        /// </summary>
        private List<string> FindDuplicatedNames(List<Document> allDocs)
        {
            List<string> warningList = new List<string>();

            // documents in folders
            foreach (Document folderX in allDocs)
            {
                if (folderX.Type == Document.DocumentType.Folder)
                {
                    List<Document> docnameInFolder = new List<Document>();
                    foreach (Document doc in allDocs)
                    {
                        if (doc.ParentFolders.Contains(folderX.Self))
                        {
                            docnameInFolder.Add(doc);
                        }
                    }

                    List<Document> dupDocs = Utility.FindDuplicates(docnameInFolder);
                    foreach (Document doc in dupDocs)
                    {
                        string s = doc.Title + "@" + folderX.Title + " {" + doc.Type + "}";
                        warningList.Add(s);
                        DoFeedbackDebug(" - DupItem: " + s + " (" + doc.ResourceId + ")");
                    }
                }
            }

            // documents in root folder (= no parent folder)
            {
                List<Document> docnameInFolder = new List<Document>();
                foreach (Document doc in allDocs)
                {
                    if (doc.ParentFolders.Count == 0)
                    {
                        docnameInFolder.Add(doc);
                    }
                }
                List<Document> dupDocs = Utility.FindDuplicates(docnameInFolder);
                foreach (Document doc in dupDocs)
                {
                    string s = doc.Title + "@" + "[RootFolder] {" + doc.Type + "}";
                    warningList.Add(s);
                    DoFeedbackDebug(" - DupItem: " + s + " (" + doc.ResourceId + ")");
                }
            }


            return warningList;
        }
    }

}
