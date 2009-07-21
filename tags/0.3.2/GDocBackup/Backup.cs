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



namespace GDocBackup
{

    /// <summary>
    /// Execute backup of docs
    /// </summary>
    public class Backup
    {
        private string _userName;
        private string _password;
        private string _outDir;
        private Document.DownloadType _docExpType;
        private Document.DownloadType _sprdExpType;
        private Document.DownloadType _presExpType;
        private IWebProxy _iwebproxy;
        private List<Document> _docs;
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
            if (Feedback != null)
                Feedback(this, new FeedbackEventArgs(message, _lastPercent));
        }


        private void DoFeedback(string message, double percent)
        {
            _lastPercent = percent;
            if (Feedback != null)
                Feedback(this, new FeedbackEventArgs(message, percent));
        }


        private void DoFeedback(FeedbackObject fo)
        {
            if (Feedback != null)
                Feedback(this, new FeedbackEventArgs("", _lastPercent, fo));
        }



        /// <summary>
        /// [Constructor]
        /// </summary>
        public Backup(string userName, string password, string outDir,
            Document.DownloadType docExpType, Document.DownloadType sprdExpType,
            Document.DownloadType presExpType, IWebProxy webproxy)
        {
            _userName = userName;
            _password = password;
            _outDir = outDir;
            _docExpType = docExpType;
            _sprdExpType = sprdExpType;
            _presExpType = presExpType;
            _iwebproxy = webproxy;
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
            _lastException = null;

            GDataCredentials credentials = new GDataCredentials(_userName, _password);
            RequestSettings settings = new RequestSettings("GDocBackup", credentials);
            settings.AutoPaging = true;
            settings.PageSize = 100;

            DoFeedback("Get doc list");
            DocumentsRequest request = new DocumentsRequest(settings);


            if (_iwebproxy != null)
            {
                GDataRequestFactory gdrf = request.Service.RequestFactory as GDataRequestFactory;
                gdrf.Proxy = _iwebproxy;

                // BETTER/FUTURE SOLUTION (DocumentsRequest.Proxy property will be available in the next release of the GDoc NET Lib)
                // (see http://code.google.com/p/google-gdata/issues/detail?id=234  )
                //
                // request.Proxy = _iwebproxy;
                //
            }

            // Get doc list from GDocs
            Feed<Document> feed = request.GetEverything();
            _docs = new List<Document>();
            foreach (Document entry in feed.Entries)
                _docs.Add(entry);

            // Builds/updates local folder structure
            this.BuildFolders(null, _docs, _outDir);

            // Docs loop!
            int errorCount = 0;
            for (int i = 0; i < _docs.Count; i++)
            {
                Document doc = _docs[i];
                DoFeedback("Item: " + i + " / " + _docs.Count, ((double)i) / _docs.Count);

                Document.DownloadType downloadtype = Document.DownloadType.pdf;
                bool downloadDoc = false;
                switch (doc.Type)
                {
                    case Document.DocumentType.Document:
                        downloadtype = _docExpType;
                        downloadDoc = true;
                        break;
                    case Document.DocumentType.Presentation:
                        downloadtype = _presExpType;
                        downloadDoc = true;
                        break;
                    case Document.DocumentType.Spreadsheet:
                        downloadtype = _sprdExpType;
                        downloadDoc = true;
                        break;
                    //case Document.DocumentType.PDF:
                    //    --- NOT SUPPORTED by GDoc API ---
                    //    downloadtype = Document.DownloadType.pdf;
                    //    break;
                    default:
                        downloadDoc = false;
                        break;
                }

                if (downloadDoc)
                {
                    try
                    {
                        string outFolderPath = (doc.ParentFolders.Count == 0) ? _outDir : _folderDict[doc.ParentFolders[0]];
                        string outFileFP = Path.Combine(outFolderPath, RemoveInvalidChars(doc.Title) + "." + downloadtype.ToString());

                        FileInfo fi = new FileInfo(outFileFP);

                        DateTime locFileDateTime = fi.LastWriteTime;
                        DateTime gdocFileDateTime = doc.Updated;

                        // Mono and/or Ubuntu (...linux) does not support milliseconds info when saving DataTime to FileInfo.LastWriteTime
                        // So... I remove it!   :)
                        locFileDateTime = this.RemoveMilliseconds(locFileDateTime);
                        gdocFileDateTime = this.RemoveMilliseconds(gdocFileDateTime);

                        DoFeedback("Check file exist - Result:" + fi.Exists + " RemoteDT: " + gdocFileDateTime + " LocalDT: " + locFileDateTime);

                        downloadDoc = (!fi.Exists || locFileDateTime != gdocFileDateTime);
                        if (downloadDoc)
                        {
                            DoFeedback("Downloading: " + doc.Title);
                            DoFeedback("Type: " + doc.Type);

                            Stream stream = request.Download(doc, downloadtype);
                            using (FileStream outFile = new FileStream(outFileFP, FileMode.Create, FileAccess.Write))
                            {
                                byte[] buffer = new byte[8192];
                                int bytesRead;
                                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                                    outFile.Write(buffer, 0, bytesRead);
                                outFile.Close();
                            }
                            new FileInfo(outFileFP).LastWriteTime = doc.Updated;
                        }
                        else
                        {
                            DoFeedback("Skipped DOC: " + doc.Title);
                        }

                        DoFeedback(new FeedbackObject(
                            doc.Title, doc.Type.ToString(), downloadDoc ? "BCKUP" : "SKIP",
                            "", locFileDateTime, gdocFileDateTime));
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        DoFeedback("DOC-ERROR: " + ex.ToString());
                        DoFeedback(new FeedbackObject(
                            doc.Title, doc.Type.ToString(), "ERROR",
                            "", null, null));
                    }
                }
                else
                {
                    if (doc.Type != Document.DocumentType.Folder)
                        DoFeedback(new FeedbackObject(doc.Title, doc.Type.ToString(), "NONE", "", null, null));
                }
            }

            DoFeedback("End", 0);
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
                            string newCurrPath = Path.Combine(currentPath, doc.Title);

                            _folderDict.Add(doc.Id, newCurrPath);

                            if (!Directory.Exists(newCurrPath))
                                Directory.CreateDirectory(newCurrPath);

                            BuildFolders(doc, docs, newCurrPath);
                        }
                    }
                    else
                    {
                        // Level > Zero
                        if (doc.ParentFolders.Contains(parentDir.Id))
                        {
                            // child found!
                            string newCurrPath = Path.Combine(currentPath, doc.Title);

                            _folderDict.Add(doc.Id, newCurrPath);

                            if (!Directory.Exists(newCurrPath))
                                Directory.CreateDirectory(newCurrPath);

                            BuildFolders(doc, docs, newCurrPath);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Removes invalids chars from string
        /// </summary>
        private string RemoveInvalidChars(string s)
        {
            StringBuilder x = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (Char.IsLetter(c) || Char.IsNumber(c) || c == '-' || c == '_')
                    x.Append(c);
                else
                    x.Append('_');
            }
            return x.ToString();
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

    }

}
