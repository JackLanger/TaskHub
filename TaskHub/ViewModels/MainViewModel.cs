using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TaskHub.Controlls.Commands;
using TaskHub.DAL;
using TaskHub.Model;
using TaskHub.Models;
using TaskHub.ViewModels;

namespace TaskHub.ViewModels
{
    public class MainViewModel:ModelBase
    {

        #region events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region private members
        private ObservableCollection<TaskModel> _TasksList;
        private UserModel _User;
        private List<TaskModel> data = new List<TaskModel>();
        private TaskModel _ActiveTask;
        private TaskModel _TaskModel;


        #endregion

        #region public members
        public ICommand HomeCommand { get; set; }
        private ICommand _NewTaskCommand;



        public ICommand DataGridCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Home;
        


        //task not working overwork needed
        public ICommand NewTaskCommand
        {
            get
            {
                if (_NewTaskCommand == null )
                {
                    _NewTaskCommand = new RelayCommand(
                        () => NewTask());
                }
                return _NewTaskCommand;
            }
        }

        public TaskModel taskModel
        {
            get => _TaskModel;
            set
            {
                _TaskModel = value;
                OnPropertyChanged();
            }
        }

        public TaskModel ActiveTask
        {
            get => _ActiveTask;
            set
            {
                _ActiveTask = value;
                NotifyPropertyChanged("_ActiveTask");
            }
        }

        public ObservableCollection<TaskModel> TasksList
        {
            get => _TasksList;
            private set
            {
                _TasksList = value;
            }
        }
        #endregion


        protected void NotifyPropertyChanged([CallerMemberName]String info = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        


        #region constructor


        public MainViewModel()
        {
            data = DataAccess.ReadTaskDB().ToList();
            TasksList = new ObservableCollection<TaskModel>();

            // TODO:create the commands


            foreach (var task in DataAccess.ReadTaskDB())
            {
                TasksList.Add(task);
            }

            _User = new UserModel() {UserName = "jack" };
        }

        #endregion

        public void NewTask() => CurrentPage = ApplicationPage.NewTask;

        internal void UpdateTaskModel(Object task)
        {
            var t = task as TaskModel;
            _ActiveTask.TaskName = t.TaskName; 
            _ActiveTask.TaskDescription = t.TaskDescription; 
            _ActiveTask.TaskStatus = t.TaskStatus; 
            _ActiveTask.PostedBy = t.PostedBy; 
        }

        //private void TaskModel_PropertyChanged(object sender, PropertyChangedEventArgs e) => DataAccess.UpdateDb(sender as TaskModel);

        public void AddNewEntry(string taskName, string taskDescr, string status, bool active = true)
        {
            var task = new TaskModel(taskName,_User.UserName,taskDescr,status,active);
            task.NewEntry();
            _TasksList.Add(task);

            CollectionViewSource.GetDefaultView(_TasksList).Refresh();
            
        }

        public void FilterData(string filter)
        {
            _TasksList.Clear();
            var filteredData = data.Where(x => x.TaskStatus == filter).Select(t => t).ToList();

            foreach( var task in filteredData)
            {
                _TasksList.Add(task);
            }

        }

    }
}