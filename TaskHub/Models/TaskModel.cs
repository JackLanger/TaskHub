using System;
using System.Collections;
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
    public delegate void DeleteThisHandler(TaskModel model, EventArgs deleteThisArgs);

    public class TaskModel : ModelBase
    {
        #region Events

        public event InvalidInputHandler InvalidInput;
        public event DeleteThisHandler DeleteThis; 
        #endregion


        #region Private Members

        public int TaskId { get; set; }
        public DateTime DateAdded { get; }
        private string _TaskName;
        private string _TaskDescription;
        private string _PostedBy;
        private bool _IsActive;
        private string _TaskStatus;

        #endregion


        #region Public Members

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
            }
        }

        public string PostedBy
        {
            get => _PostedBy;
            set
            {
                _PostedBy = value;
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

        #endregion
        
        
        #region Constructor

        public TaskModel() : this("new Task", "unknown", "description", "active", true)
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

        #endregion

        #region Methods

        protected virtual void OnInvalidInput(InvalidInputEventArgs e) => InvalidInput?.Invoke(this, e);

        protected virtual void OnDeleteThis() => DeleteThis?.Invoke(this, new EventArgs());

        public void UpdateEntry() => DataAccess.UpdateDb(this);

        public void NewEntry() => DataAccess.WriteNewEntry(this);

        public void DeleteEntry()
        {
            DataAccess.RemoveEntry(this);
            OnDeleteThis();
        }

        #endregion
    }
}
