using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Client;
using Google.Documents;
using System.IO;

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

            StreamWriter outFile = new StreamWriter("doclist.txt", false);
            StreamWriter outFile2 = new StreamWriter("doclistdetails.txt", false);
            foreach (Document doc in docs)
            {
                string s = doc.Title + "\t" + doc.ResourceId;
                Console.WriteLine(s);
                outFile.WriteLine(s);
                outFile2.WriteLine(s);
                foreach (string pf in doc.ParentFolders)
                    outFile2.WriteLine("\t\t\t" + pf);
            }
            outFile.Close();
            outFile2.Close();

            Console.WriteLine("--- END ---");
        }
    }
}
