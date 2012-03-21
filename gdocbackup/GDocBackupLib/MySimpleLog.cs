using System;
using System.Collections.Generic;
using System.Text;

namespace GDocBackupLib
{
    public class MySimpleLog
    {

        private List<MySimpleLogEntry> _logEntries;

        private int _maxCount = 10000;
        private int _maxCountWindows = 100;


        public MySimpleLog()
        {
            _logEntries = new List<MySimpleLogEntry>();
        }

        public void Reset()
        {
            _logEntries = new List<MySimpleLogEntry>();
        }

        public void Add(string msg)
        {
            _logEntries.Add(new MySimpleLogEntry() { DT = DateTime.Now, Message = msg });

            if (_logEntries.Count > _maxCount + _maxCountWindows)
            {
                _logEntries.RemoveRange(0, _maxCountWindows);
            }
        }

        public string[] DumpToStrArray()
        {
            List<string> list = new List<string>();
            foreach (MySimpleLogEntry entry in _logEntries)            
                list.Add(entry.DT + " : " + entry.Message);
            return list.ToArray();           
        }

    }


    public class MySimpleLogEntry
    {
        public DateTime DT;
        public String Message;
    }

}
