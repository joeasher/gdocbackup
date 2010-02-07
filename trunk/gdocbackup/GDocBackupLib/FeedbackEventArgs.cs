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


namespace GDocBackupLib
{
    /// <summary>
    /// Feedback event argument
    /// </summary>
    public class FeedbackEventArgs : EventArgs
    {
        // -- privates --
        private string _message;
        private double _percent;
        private FeedbackObject _feedbackobj;
        private bool _isVerbose;

        // -- properties --
        public string Message { get { return _message; } }
        public double PerCent { get { return _percent; } }
        public FeedbackObject FeedbackObj { get { return _feedbackobj; } }
        public bool IsVerbose { get { return _isVerbose; } }

        // -- constructors --
        public FeedbackEventArgs(string message, double percent)
            : this(message, percent, false)
        {
        }

        public FeedbackEventArgs(string message, double percent, bool isVerbose)
        {
            _message = message;
            _percent = percent;
            _isVerbose = isVerbose;
        }

        public FeedbackEventArgs(string message, double percent, FeedbackObject fo)
        {
            _message = message;
            _percent = percent;
            _feedbackobj = fo;
            _isVerbose = false;
        }
    }
}
