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


namespace GDocBackupLib
{

    /// <summary>
    /// Execute backup of docs 
    /// </summary>
    public class Backup
    {
        private string _userName;
        private string _password;
        private string _outDir;
        private bool _downloadAll;
        private Document.DownloadType[] _docExpType;
        private Document.DownloadType[] _sprdExpType;
        private Document.DownloadType[] _presExpType;
        private IWebProxy _iwebproxy;
        private bool _bypassHttpsChecks;
        private bool _debugMode;
        private int? _dateDiff;
        private Dictionary<string, string> _folderDict;
        private double _lastPercent = 0;
        private Exception _lastException = null;


        /// <summary>
        /// Last exception
        /// </summary>
        public Exception LastException { get { return _lastException; } }


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
            _lastPercent = percent;
            System.Diagnostics.Debug.WriteLine(message);
            if (Feedback != null)
                Feedback(this, new FeedbackEventArgs(message, percent));
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
            if (_debugMode)
            {
                if (Feedback != null)
                    Feedback(this, new FeedbackEventArgs(message, _lastPercent));
            }
        }


        /// <summary>
        /// [Constructor]
        /// </summary>
        public Backup(
            string userName,
            string password,
            string outDir,
            bool downloadAll,
            Document.DownloadType[] docExpType,
            Document.DownloadType[] sprdExpType,
            Document.DownloadType[] presExpType,
            IWebProxy webproxy,
            bool bypassHttpsChecks,
            bool debugMode,
            int? dateDiff)
        {
            _userName = userName;
            _password = password;
            _outDir = outDir;
            _downloadAll = downloadAll;
            _docExpType = docExpType;
            _sprdExpType = sprdExpType;
            _presExpType = presExpType;
            _iwebproxy = webproxy;
            _bypassHttpsChecks = bypassHttpsChecks;
            _debugMode = debugMode;
            _dateDiff = dateDiff;
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
                return false;
            }
        }


        /// <summary>
        /// Exec backup (internal)
        /// </summary>
        private int ExecInternal()
        {
            DoFeedback(new string('*', 60));
            DoFeedback("****** START BACKUP PROCESS ******");

            _lastException = null;

            // Bypass Https checks?
            // I know, CertificatePolicy is deprecated. I should use ServerCertificateValidationCallback but Mono does not support it.  :(
            if (_bypassHttpsChecks)
            {
                DoFeedback("BypassHttpsCertCheck ACTIVE");
                ServicePointManager.CertificatePolicy = new BypassHttpsCertCheck();
            }

            // Setup credentials and connection
            DoFeedback("Setup connection & get doc list");
            GDataCredentials credentials = new GDataCredentials(_userName, _password);
            RequestSettings settings = new RequestSettings("GDocBackup", credentials);
            settings.AutoPaging = true;
            settings.PageSize = 100;

            DocumentsRequest request = new DocumentsRequest(settings);
            if (_iwebproxy != null)
            {
                // OLD CODE
                // GDataRequestFactory gdrf = request.Service.RequestFactory as GDataRequestFactory;
                // gdrf.Proxy = _iwebproxy;

                // new DocumentRequest support proxy setting :)
                request.Proxy = _iwebproxy;
            }

            // Get doc list from GDocs
            Feed<Document> feed = request.GetEverything();
            List<Document> docs = new List<Document>();
            foreach (Document entry in feed.Entries)
                docs.Add(entry);

            // Builds/updates local folder structure
            this.BuildFolders(null, docs, _outDir);
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
                        downloadTypes = _docExpType;
                        break;
                    case Document.DocumentType.Presentation:
                        downloadTypes = _presExpType;
                        break;
                    case Document.DocumentType.Spreadsheet:
                        downloadTypes = _sprdExpType;
                        break;
                    case Document.DocumentType.PDF:
                        // --- NOT Completely supported by GDoc API 2.0 for .NET ---
                        downloadTypes = new Document.DownloadType[] { Document.DownloadType.pdf };
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
                        bool isDrawing = doc.ResourceId.StartsWith("drawing:");   // drawing:14TBycKwlpXJ25N......
                        if (isDrawing)
                            downloadTypes = new Document.DownloadType[] { Document.DownloadType.pdf };

                        foreach (Document.DownloadType downloadtype in downloadTypes)
                        {
                            // Build local file path
                            string outFolderPath;
                            if (doc.ParentFolders.Count == 0)
                            {
                                outFolderPath = _outDir;
                            }
                            else
                            {
                                DoFeedback("Try to get folder from dict using key=[" + doc.ParentFolders[0] + "]");
                                outFolderPath = _folderDict[doc.ParentFolders[0]];
                            }
                            string outFileFP =
                                (doc.Type == Document.DocumentType.Unknown && !isDrawing) ?
                                Path.Combine(outFolderPath, this.RemoveInvalidChars(doc.Title, true)) :
                                Path.Combine(outFolderPath, this.RemoveInvalidChars(doc.Title, false) + "." + downloadtype.ToString());

                            // Get current local file in infos
                            FileInfo fi = new FileInfo(outFileFP);
                            DateTime locFileDateTime = fi.LastWriteTime;
                            DateTime gdocFileDateTime = doc.Updated;

                            // Mono and/or Ubuntu (...linux) does not support milliseconds info when saving DataTime to FileInfo.LastWriteTime. So... I remove it!   :)
                            locFileDateTime = this.RemoveMilliseconds(locFileDateTime);
                            gdocFileDateTime = this.RemoveMilliseconds(gdocFileDateTime);

                            bool downloadDoc = (!fi.Exists || _downloadAll);

                            if (_dateDiff.HasValue)
                            {
                                if (Math.Abs(locFileDateTime.Subtract(gdocFileDateTime).TotalSeconds) > _dateDiff.Value)
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
                                        if (isDrawing)
                                            downloadUrl += "&exportFormat=" + downloadtype.ToString();
                                        Uri downloadUri = new Uri(downloadUrl);
                                        gdocStream = request.Service.Query(downloadUri);
                                    }
                                    else if (doc.Type == Document.DocumentType.Document)
                                    {
                                        // Questo dovrebbe essere il modo giusto. Ma non funziona (google bug!)
                                        // gdocStream = request.Download(doc, downloadtype);
                                        // Anche questo non funziona :(
                                        // gdocStream = request.Download(doc, downloadtype.ToString());
                                        // 
                                        // In attesa di una soluzione, ecco la mandrakata!
                                        gdocStream = Mandrakata.GetDocExportStream(request, doc, downloadtype);
                                    }
                                    else if (doc.Type == Document.DocumentType.Spreadsheet)
                                    {
                                        // dovrei usare questo: gdocStream = request.Download(doc, downloadtype);
                                        // ma non funziona. Questo funziona... boh...
                                        gdocStream = request.Download(doc, downloadtype.ToString());
                                    }
                                    else if (doc.Type != Document.DocumentType.PDF)
                                    {
                                        gdocStream = request.Download(doc, downloadtype);
                                    }
                                    else
                                    {
                                        // This is a workaround for downloading Pdf (new API 3.0 will support)                                            
                                        String downloadUrl = doc.DocumentEntry.Content.Src.ToString();
                                        Uri downloadUri = new Uri(downloadUrl);
                                        gdocStream = request.Service.Query(downloadUri);
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
                                doc.Title,
                                doc.Type.ToString(),
                                (doc.Type == Document.DocumentType.Unknown && !isDrawing) ? "BIN" : downloadtype.ToString(),
                                downloadDoc ? "BCKUP" : "SKIP",
                                "", locFileDateTime, gdocFileDateTime));
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        DoFeedback("DOC-ERROR: " + ex.ToString());
                        DoFeedback(new FeedbackObject(
                            doc.Title, doc.Type.ToString(), "", "ERROR",
                            "", null, null));
                    }

                }
                else
                {
                    if (doc.Type != Document.DocumentType.Folder)
                        DoFeedback(new FeedbackObject(doc.Title, doc.Type.ToString(), "", "NONE", "", null, null));
                }
            }

            DoFeedback("****** END BACKUP PROCESS ******");
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
                            _folderDict.Add(doc.Self, newCurrPath);

                            if (!Directory.Exists(newCurrPath))
                                Directory.CreateDirectory(newCurrPath);

                            BuildFolders(doc, docs, newCurrPath);
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

    }

}
