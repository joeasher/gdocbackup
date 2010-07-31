using System;
using System.Collections.Generic;
using System.Text;
using Google.Documents;
using Google.GData.Client;
using System.IO;


namespace GDocBackupLib
{
    internal class Mandrakata
    {
        /*
        public static Stream GetDocUri_NON_FUNZIONA(DocumentsRequest request, Document doc, Document.DownloadType type)
        {
            // NON FUNZIONA (porca putt...!)
            string docID = doc.ResourceId.Replace("document:", "");
            string uriS = "http://docs.google.com/feeds/download/documents/Export?" +
                "docID=" + docID +
                "&exportFormat=" + type.ToString();
            Uri target = new Uri(uriS);
            return request.Service.Query(target);
        }
        */

        public static Stream GetDocExportStream(DocumentsRequest request, Document doc, Document.DownloadType downloadtype)
        {
            // Questa funziona ma mi pare na stronz...
            string format = downloadtype.ToString();
            string url =
                doc.DocumentEntry.Content.AbsoluteUri +
                "&exportFormat=" + format + "&format=" + format;
            return request.Service.Query(new Uri(url));
        }
    }
}
