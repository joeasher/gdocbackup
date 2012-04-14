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


namespace GDocBackupLib
{
    /// <summary>
    /// Contains more feedback information about document/folder
    /// </summary>
    public class FeedbackObject
    {
        public string FileName;
        public string DocType;
        public string ExportFormat;
        public string Action;
        public string Folder;
        public DateTime? LocalDateTime;
        public DateTime? RemoteDateTime;

        public FeedbackObject(
            string fileName, string docType, string exportFormat, string action, string folder,
            DateTime? localDateTime, DateTime? remoteDateTime)
        {
            FileName = fileName;
            DocType = docType;
            ExportFormat = exportFormat;
            Action = action;
            Folder = folder;
            LocalDateTime = localDateTime;
            RemoteDateTime = remoteDateTime;
        }

        public override string ToString()
        {
            return "FileName=" + FileName + " DocType=" + DocType + " ExpFrm=" + ExportFormat + " Action=" + Action +
                " Folder=" + Folder + " LocalDateTime=" + LocalDateTime + " RemoteDateTime=" + RemoteDateTime;
        }
    }
}
