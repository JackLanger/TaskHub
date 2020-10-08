using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.Models.events
{
    public class NewEntryEventArgs : EventArgs
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
        }

        private string _Info;

        public string Info
        {
            get { return _Info; }
        }
        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }



        public NewEntryEventArgs(string name, string info, string status)
        {
            _Name = name;
            _Info = info;
            _Status = status;
        }

    }
}
