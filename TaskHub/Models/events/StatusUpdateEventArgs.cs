using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.Models.events
{
    public class StatusUpdateEventArgs:EventArgs
    {

        private string _Status;

        public string Status
        {
            get { return _Status; }
        }

        public StatusUpdateEventArgs(string status)
        {
            _Status = status;
        }

    }
}
