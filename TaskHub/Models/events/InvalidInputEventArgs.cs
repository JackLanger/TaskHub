using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.Models.Exeption
{
    public class InvalidInputEventArgs:EventArgs
    {
        private string _InvalidString;
        private Exception _Error;

        public Exception Error
        {
            get => _Error;
        }


        private string _PropertyName;

        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }


        public string InvalidString
        {
            get { return _InvalidString; }
            set { _InvalidString = value; }
        }

        public InvalidInputEventArgs(string input, string propertyName, Exception error)
        {
            _Error = error;
            _InvalidString = input;


            if ( propertyName == ""|| propertyName == null)
            {
                _PropertyName = "unknown"; 
            } else
            _PropertyName = propertyName;
        }

    }
}
