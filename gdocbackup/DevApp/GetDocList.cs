using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Client;
using Google.Documents;

namespace DevApp
{
    internal class GetDocList
    {
        public static void Exec(string[] args)
        {
            Console.WriteLine("*** GetDocList ***");
            Console.WriteLine("--- START ---");

            string username = args[1];
            string password = args[2];

            GDataCredentials credentials = new GDataCredentials(username, password);
            RequestSettings settings = new RequestSettings("GDocBackup", credentials);
            settings.AutoPaging = true;
            settings.PageSize = 100;
            DocumentsRequest request = new DocumentsRequest(settings);

            Feed<Document> feed = request.GetEverything();
            List<Document> docs = new List<Document>();
            foreach (Document entry in feed.Entries)
                docs.Add(entry);


            // .... TODO .....

            Console.WriteLine("--- END ---");
        }
    }
}
