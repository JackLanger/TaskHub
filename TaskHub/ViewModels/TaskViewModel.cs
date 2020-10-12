using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskHub.Controlls.Commands;
using TaskHub.DAL;
using TaskHub.Model;

namespace TaskHub.ViewModels
{
    public class TaskViewModel : ModelBase
    {


        #region private members

        private TaskModel _Model;



        private string _TaskName;
        private string _TaskDescription;
        private string _TaskStatus;
        private bool _CanDelete;
        private bool _IsActive;
        private string _DelButtonText;
        private string _SubmitButtonText;



        public DateTime DateAdded { get; set; }
        public string PostedBy { get; set; }
        
        public string SubmitButtonText
        {
            get { return _SubmitButtonText; }
            set { _SubmitButtonText = value; }
        }
        public string DelButtonText
        {
            get => _DelButtonText;
            set
            {
                _DelButtonText = value;
                OnPropertyChanged();
            }
        }


        public TaskModel Model
        {
            get => _Model;
            set 
            {
                _Model = value;
                OnPropertyChanged();
            }
        }
        public bool IsActive
        {
            get => _IsActive;
            set
            {
                _IsActive = value;
                OnPropertyChanged();
            }
        }


        public bool CanDelete
        {
            get => _CanDelete;
            set
            {
                _CanDelete = value;
                OnPropertyChanged();
            }
        }

        public string TaskName
        {
            get => _TaskName;
            set
            {
                _TaskName = value;
                _Model.TaskName = value;
                OnPropertyChanged();
            }
        }

        public string TaskDescription
        {
            get => _TaskDescription;
            set
            {
                _TaskDescription = value;
                _Model.TaskDescription = value;
                OnPropertyChanged();
            }
        }

        public string TaskStatus
        {
            get => _TaskStatus;
            set
            {
                _TaskStatus = value;
                _Model.TaskStatus = value;
                OnPropertyChanged();
            }
        }


        #endregion


        #region Commands 

        /// <summary>
        /// TODO: need to be moved to VM at some point
        /// </summary>
        /// 

        private ICommand _MarkAsDoneCommand;
        private ICommand _DeleteCommand;


        public ICommand MarkAsDoneCommand => _MarkAsDoneCommand ??= new RelayCommand(() => AddOrUpdateEntry());
        public ICommand DeleteCommand => _DeleteCommand ??= new RelayCommand(() => OnDeleteThis());

        #endregion


        #region Constructor

        public TaskViewModel(TaskModel model)
        {
            _Model = model;

            _TaskName = _Model.TaskName;
            _TaskDescription = _Model.TaskDescription;
            _TaskStatus = _Model.TaskStatus;
            DateAdded = _Model.DateAdded;
            PostedBy = _Model.PostedBy;
            _IsActive = _Model.isActive;
            _DelButtonText = "Delete";
            _SubmitButtonText = _TaskName == "new Task" ? "add" : "Done";
        }

        #endregion


        #region Methods

        /// <summary>
        /// TODO: implement check active  and CHeck Active COmmands 
        /// </summary>
        private void AddOrUpdateEntry()
        {

        }

        private void OnDeleteThis()
            
        {
            _DelButtonText = _DelButtonText == "Delete" ? "Confirm" : "Delete";


            if (_CanDelete) 
                _Model.DeleteEntry();

            _CanDelete = _CanDelete ? false : true;
        }

        #endregion
    }
}
