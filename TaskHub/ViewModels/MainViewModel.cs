using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Threading.Tasks;
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


        #region private members
        private ObservableCollection<TaskModel> _TasksList;
        private UserModel _User;
        private List<TaskModel> data = new List<TaskModel>();
        private TaskModel _ActiveTask;
        private TaskModel _TaskModel;
        private ApplicationPage _CurrentPage = ApplicationPage.Home;


        #endregion

        #region public propertys
        public ApplicationPage CurrentPage
        {
            get => _CurrentPage;
            set 
            { 
                _CurrentPage = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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



        #region commands

        /// <summary>
        /// TODO: create all task commands needed for nav buttons and the card buttons
        /// </summary>
        /// 


        private ICommand _HomeCommand;
        private ICommand _DataGridCommand;
        private ICommand _NewTaskCommand;
        private ICommand _SwitchTaskStatusCommand;
        private ICommand _MarkAsDoneCommand;
        
        public ICommand HomeCommand => _HomeCommand ??= new RelayCommand(() => CurrentPage = ApplicationPage.Home);
        public ICommand DataGridCommand => _DataGridCommand ??= new RelayCommand(() => CurrentPage = ApplicationPage.DataGrid);
        public ICommand NewTaskCommand => _NewTaskCommand ??= new RelayCommand(() => CurrentPage = ApplicationPage.NewTask);

        public ICommand SwitchTaskStatusCommand => _SwitchTaskStatusCommand ??= new ParameterizedRelayCommand(parameter =>  SwitchTaskStatus(parameter));
        public ICommand MarkAsDoneCommand => _MarkAsDoneCommand ??= new RelayCommand(() => MarkAsDone());
        private Action SwitchTaskStatus(Object task)
        {
            throw new NotImplementedException();
        }



        #endregion



        #region constructor


        public MainViewModel()
        {
            data = DataAccess.ReadTaskDB().ToList();
            TasksList = new ObservableCollection<TaskModel>();


            foreach (var task in DataAccess.ReadTaskDB())
            {
                TasksList.Add(task);
                task.DeleteThis += Task_DeleteThis;
            }

            _User = new UserModel() {UserName = "jack" };
        }

        private void Task_DeleteThis(TaskModel model, EventArgs deleteThisArgs)
        {
            TasksList.Remove(model);
        }



        #endregion

        #region Methods


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
            var task = new TaskModel(taskName, _User.UserName, taskDescr, status, active);
            task.NewEntry();
            _TasksList.Add(task);

            CollectionViewSource.GetDefaultView(_TasksList).Refresh();

        }

        public void FilterData(string filter)
        {
            _TasksList.Clear();
            var filteredData = data.Where(x => x.TaskStatus == filter).Select(t => t).ToList();

            foreach (var task in filteredData)
            {
                _TasksList.Add(task);
            }

        }

        /// <summary>
        /// TODo: remove from list and trigger delete from SQL DB on DAL
        /// </summary>
        private void DeleteTask(TaskModel task)
        {

            TasksList.Remove(task);
        }


        /// <summary>
        /// TODO: Check of Task and make inaccessible while marked as done
        /// </summary>
        private void MarkAsDone()
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}