using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskHub.Controlls.Commands;
using TaskHub.DAL;
using TaskHub.Models;
using TaskHub.Models.Exeption;

namespace TaskHub.Model
{
    public delegate void InvalidInputHandler(ModelBase model, EventArgs invalidInput);

    public class TaskModel:ModelBase
    {
        public event InvalidInputHandler InvalidInput;

        

        public int TaskId { get; set; }
        private string _TaskName;
        private string _TaskDescription;
        private string _PostedBy;
        public DateTime DateAdded { get; }
        private bool _IsActive;
        private string _TaskStatus;



        public string TaskName
        {
            get { return _TaskName; }
            set
            {
                if (value.Length > 0)
                {
                    _TaskName = value;
                }
            }
        }
        
        public string TaskDescription
        {
            get => _TaskDescription;
            set
            {
                _TaskDescription = value;
                OnPropertyChanged();
            }
        }

        public string PostedBy
        {
            get => _PostedBy;
            set
            {
                _PostedBy = value;
                OnPropertyChanged();
            }
        }

        public string TaskStatus
        {
            get => _TaskStatus;
            set => _TaskStatus = value;
        }
        public bool isActive
        {
            get => _IsActive;
            set
            {
                _IsActive = value;
                OnPropertyChanged();
            }

        }

        public TaskModel() : this("", "unknown", "", "inactive", true)
        {

        }

        public TaskModel(string name, string user, string descr, string status, bool active)
        {
             DateAdded = DateTime.Now;
             _TaskName = name;
             _TaskDescription = descr;
             _PostedBy = user;
             _TaskStatus = status;
             _IsActive = active;
        }


        protected virtual void OnInvalidInput(InvalidInputEventArgs e) => InvalidInput?.Invoke(this, e);

        public void UpdateStatusInDb() => DataAccess.UpdateDb(this);

        public void NewEntry() => DataAccess.WriteNewEntry(this);


    }
}
