using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskHub.Controlls.Commands;
using TaskHub.DAL;
using TaskHub.Models;
using TaskHub.Models.Exeption;

namespace TaskHub.Model
{
    public delegate void InvalidInputHandler(ModelBase model, EventArgs invalidInput);
    public delegate void DeleteTHisHandler(TaskModel model, EventArgs deleteThisArgs);

    public class TaskModel:ModelBase
    {
        public event InvalidInputHandler InvalidInput;
        public event DeleteTHisHandler DeleteThis; 

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand => _DeleteCommand ??= new RelayCommand(() => OnDeleteThis());

        private void DoSomething()
        {
            _CanDelete = true;
        }

        public int TaskId { get; set; }
        private string _TaskName;
        private string _TaskDescription;
        private string _PostedBy;
        public DateTime DateAdded { get; }
        private bool _IsActive;
        private string _TaskStatus;
        private bool _CanDelete;

        public bool CanDelete
        {
            get => _CanDelete;
            private set
            {
                _CanDelete = value;
                OnPropertyChanged();
            }
        }




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
            _CanDelete = false;
            
        }


        protected virtual void OnInvalidInput(InvalidInputEventArgs e) => InvalidInput?.Invoke(this, e);

        protected virtual void OnDeleteThis ()
        {
            DeleteThis?.Invoke(this, new EventArgs());
        }

        public void UpdateStatusInDb() => DataAccess.UpdateDb(this);

        public void NewEntry() => DataAccess.WriteNewEntry(this);

        public void DeleteEntry() => DataAccess.RemoveEntry(this);
    }
}
