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
        private ObservableCollection<TaskViewModel> _TasksList;
        private UserModel _User;
        private List<TaskModel> data = new List<TaskModel>();
        private TaskModel _ActiveTask;
        private TaskModel _TaskModel;
        private ApplicationPage _CurrentPage = ApplicationPage.Home;

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

        public ObservableCollection<TaskViewModel> TasksList
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

        public ICommand HomeCommand => _HomeCommand ??= new RelayCommand(() => CurrentPage = ApplicationPage.Home);
        public ICommand DataGridCommand => _DataGridCommand ??= new RelayCommand(() => CurrentPage = ApplicationPage.DataGrid);


        #endregion



        #region constructor


        public MainViewModel()
        {
            data = DataAccess.ReadTaskDB().ToList();
            TasksList = new ObservableCollection<TaskViewModel>();


            foreach (var task in DataAccess.ReadTaskDB())
            {
                TasksList.Add(new TaskViewModel(task));
                task.DeleteThis += Task_DeleteThis;
            }
            TasksList.Add(new TaskViewModel(new TaskModel()));

            foreach ( var taskVM in TasksList)
            {
                taskVM.newOrUpdateEntry += TaskVM_newOrUpdateEntry;
            }

        }




        #endregion

        #region Methods




        /// <summary>
        /// HACK: check if the sender is the last entry in the list if thats the case add new entry else update old one
        /// </summary>
        /// <param name="sender"></param>
        private void TaskVM_newOrUpdateEntry(TaskViewModel sender)
        {
            
            if (sender == TasksList[TasksList.Count - 1])
            {
                sender.Model.NewEntry();
                TasksList.Prepend(sender);
                TasksList.Add(new TaskViewModel(new TaskModel()));

            }
            else
                sender.Model.UpdateEntry();
        }

        private void Task_DeleteThis(TaskModel model, EventArgs deleteThisArgs)
        {
            for (int i = 0; i < TasksList.Count; i++)
            {
                if ( TasksList[i].Model == model)
                TasksList.RemoveAt(i); 
            }
        }
        public void FilterData(string filter)
        {
            _TasksList.Clear();
            var filteredData = data.Where(x => x.TaskStatus == filter).Select(t => t).ToList();

            foreach (var task in filteredData)
            {
                _TasksList.Add(new TaskViewModel(task));
            }

        }
        #endregion
    }
}