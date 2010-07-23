//using System;
//using System.Collections.Generic;
//using System.Text;
//using Google.Documents;
//using Google.GData.Client;


//namespace GDocBackupLib
//{
//    internal class Mandrakata
//    {

//        public string BuildDownloadUri(Document doc, Document.DownloadType type)
//        {
//            int sheetNumber = 0;

//            if (doc.Type == Document.DocumentType.Unknown)
//                throw new ArgumentException("Document has an unknown type");  //cambiare

//            if (doc.Type == Document.DocumentType.Folder)
//                throw new ArgumentException("Document is a folder, can not be downloaded");   //cambiare

//            string docID = doc.ResourceId.Replace("document:", "").Replace("spreadsheet:", "").Replace("presentation:", ""); ;

//            string queryUri = null;
//            switch (doc.Type)
//            {
//                case Document.DocumentType.Spreadsheet:
//                    {
//                        queryUri =
//                            "http://spreadsheets.google.com/" +
//                            "feeds/download/spreadsheets/Export?key=" + docID + "&exportFormat=";
//                        switch (type)
//                        {
//                            case Document.DownloadType.xls: queryUri += "xls"; break;
//                            case Document.DownloadType.csv: queryUri += "csv&gid=" + sheetNumber.ToString(); break;
//                            case Document.DownloadType.pdf: queryUri += "pdf"; break;
//                            case Document.DownloadType.ods: queryUri += "ods"; break;
//                            case Document.DownloadType.tsv: queryUri += "tsv&gid=" + sheetNumber.ToString(); ; break;
//                            case Document.DownloadType.html: queryUri += "html"; break;
//                            default: throw new ArgumentException("type is invalid for a spreadsheet");
//                        }
//                        break;
//                    }
//                case Document.DocumentType.Presentation:
//                    {
//                        throw new ApplicationException("todo!");
//                        break;
//                    }
//                case Document.DocumentType.Document:
//                    {
//                        queryUri =
//                            "http://docs.google.com/" +
//                            "feeds/download/documents/Export?docID=" + docID + "&exportFormat=" + type.ToString();
//                        break;

//                        break;
//                    }
//                default:
//                    throw new ApplicationException("Unmanaged doc type [" + doc.Type + "]");
//            }

//        }


//        public Stream Download(Document document, Document.DownloadType type, string baseDomain, int sheetNumber)
//        {
//            //doc.ResourceId

//            if (document.Type == Document.DocumentType.Unknown)
//            {
//                throw new ArgumentException("Document has an unknown type");
//            }

//            if (document.Type == Document.DocumentType.Folder)
//            {
//                throw new ArgumentException("Document is a folder, can not be downloaded");
//            }

//            // now figure out the parameters
//            string queryUri = "";

//            //Service s = this.Service;

//            switch (document.Type)
//            {

//                case Document.DocumentType.Spreadsheet:
//                    // spreadsheet has a different parameter
//                    if (baseDomain == null)
//                    {
//                        baseDomain = "http://spreadsheets.google.com/";
//                    }
//                    queryUri = baseDomain + "feeds/download/spreadsheets/Export?key=" + document.ResourceId + "&exportFormat=";
//                    //s = this.spreadsheetsService;
//                    switch (type)
//                    {
//                        case Document.DownloadType.xls:
//                            queryUri += "xls";
//                            break;
//                        case Document.DownloadType.csv:
//                            queryUri += "csv&gid=" + sheetNumber.ToString();
//                            break;
//                        case Document.DownloadType.pdf:
//                            queryUri += "pdf";
//                            break;
//                        case Document.DownloadType.ods:
//                            queryUri += "ods";
//                            break;
//                        case Document.DownloadType.tsv:
//                            queryUri += "tsv&gid=" + sheetNumber.ToString(); ;
//                            break;
//                        case Document.DownloadType.html:
//                            queryUri += "html";
//                            break;
//                        default:
//                            throw new ArgumentException("type is invalid for a spreadsheet");

//                    }
//                    break;

//                case Document.DocumentType.Presentation:
//                    if (baseDomain == null)
//                    {
//                        baseDomain = "http://docs.google.com/";
//                    }

//                    queryUri = baseDomain + "feeds/download/presentations/Export?docID=" + document.ResourceId + "&exportFormat=";
//                    switch (type)
//                    {
//                        case Document.DownloadType.swf:
//                            queryUri += "swf";
//                            break;
//                        case Document.DownloadType.pdf:
//                            queryUri += "pdf";
//                            break;
//                        case Document.DownloadType.ppt:
//                            queryUri += "ppt";
//                            break;
//                        default:
//                            throw new ArgumentException("type is invalid for a presentation");
//                    }
//                    break;
//                default:
//                    if (baseDomain == null)
//                    {
//                        baseDomain = "http://docs.google.com/";
//                    }

//                    queryUri = baseDomain + "feeds/download/documents/Export?docID=" + document.ResourceId + "&exportFormat=" + type.ToString();
//                    break;

//            }

//            Uri target = new Uri(queryUri);
//            return s.Query(target);
//        }
//    }
//}
