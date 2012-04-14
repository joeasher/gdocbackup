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
