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
