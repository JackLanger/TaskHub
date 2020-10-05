using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHub.Model;

namespace TaskHub.ViewModels
{
    public class TaskCardViewModel:ModelBase
    {

        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DateAdded { get; set; }
        public string PostedBy { get; set; }
        public string TaskStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }

        public TaskCardViewModel(string name, string descr, DateTime date, string user, string status, bool active)
        {
            TaskName = name;
            TaskDescription = descr;
            DateAdded = date;
            PostedBy = user;
            this.TaskStatus = status;
            IsActive = active;
        }
    }
}
