using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TaskHub.Controlls.Commands;
using TaskHub.DAL;
using TaskHub.Model;

namespace TaskHub.ViewModels
{
    public delegate void newEntryHandler(TaskViewModel sender);

    public class TaskViewModel : ModelBase
    {
        public event newEntryHandler newOrUpdateEntry;

        #region private members

        private TaskModel _Model;
        private bool _CanDelete;
        private string _DelButtonText;
        private string _SubmitButtonText;

        public DateTime DateAdded { get; set; }
        public string PostedBy { get; set; }
        
        public string SubmitButtonText
        {
            get => _SubmitButtonText;
            set
            {
                _SubmitButtonText = value;
                OnPropertyChanged();
            }
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
        public bool CanDelete
        {
            get => _CanDelete;
            set
            {
                _CanDelete = value;
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


        public ICommand MarkAsDoneCommand => _MarkAsDoneCommand ??= new RelayCommand(() => OnNewOrUpdateEntry());
        public ICommand DeleteCommand => _DeleteCommand ??= new RelayCommand(() => OnDeleteThis());

        #endregion


        #region Constructor

        public TaskViewModel(TaskModel model)
        {
            _Model = model;

            _DelButtonText = "Delete";
            _SubmitButtonText = "Done";
        }

        #endregion


        #region Methods


        /// <summary>
        /// Switch between active and inactive state
        /// </summary>
        private void OnNewOrUpdateEntry()
        {
                _Model.IsActive = _Model.IsActive ? false : true;
                _Model.TaskStatus = _Model.IsActive?
                                    _Model.TaskStatus = Enum.GetName(typeof(ActivityCheck), ActivityCheck.active) : _Model.TaskStatus = Enum.GetName(typeof(ActivityCheck), ActivityCheck.inactive);
        }


        /// <summary>
        /// if Button was pressed before delete the entry on first click set the button Text to Confirm.
        /// 
        /// BUG: !!! buttontext does not change on Realtime need a workarround -> hardcode in TaskCardControl
        /// 
        /// TODO: use for multydelete!
        /// </summary>
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
