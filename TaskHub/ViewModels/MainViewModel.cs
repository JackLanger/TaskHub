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
        private ObservableCollection<ProjectViewModel> _Projects;
        private List<TaskModel> data = new List<TaskModel>();
        private TaskModel _TaskModel;
        private ProjectViewModel _Project;
        private string _ProjectName;



        public ObservableCollection<TaskViewModel> TasksList
        {
            get => _TasksList;
            set
            {
                _TasksList = value;
                OnPropertyChanged();
            }
        }

        public string ProjectName
        {
            get => _ProjectName;
            set
            {
                _ProjectName = value;
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

        public ObservableCollection<ProjectViewModel> Projects
        {
            get => _Projects;
            private set
            {
                _Projects = value;
                OnPropertyChanged();
            }
        }

        #endregion



        #region commands

        /// <summary>
        /// TODO: create all task commands needed for nav buttons and the card buttons
        /// </summary>
        /// 
        private ICommand _AddNewTaskCommand;
        private ICommand _DeleteTaskCommand;

        public ICommand DeleteTaskCommand => _DeleteTaskCommand ??= new RelayCommand(() => DeleteTask());
        public ICommand AddNewTaskCommand => _AddNewTaskCommand ??= new RelayCommand(() => NewTask());
        #endregion



        #region constructor

        public MainViewModel()
        {
            _ProjectName = "";
            data = (List<TaskModel>)DataAccess.ReadTaskDB();
            _TasksList = new ObservableCollection<TaskViewModel>();
            _Projects = new ObservableCollection<ProjectViewModel>();


            foreach (var project in DataAccess.ReadProjectDb())
            {
                _Projects.Add(new ProjectViewModel(project));
                
            }

            foreach (var task in DataAccess.ReadTaskDB())
            {
                TasksList.Add(new TaskViewModel(task));
                task.DeleteThis += Task_DeleteThis;
            }

            foreach ( var taskVM in TasksList)
            {
                taskVM.newOrUpdateEntry += TaskVM_newOrUpdateEntry;
            }

            foreach ( var project in _Projects)
            {
                project.ActiveTasks = TasksList
                                        .Where(t => t.Model.IsActive && t.Model.ProjectName == project.ProjectName)
                                        .Count();
            }
            foreach(var p in _Projects)
            {
                p.FilterProjects += FilterProjects;
            }
        }


        #endregion



        #region Methods

        /// <summary>
        /// HACK: check if the sender is the last entry in the list if thats the case add new entry else update old one
        /// </summary>
        /// <param name="sender"></param>
        /// 

        private void DeleteTask()
        {
            var del = TasksList.Where(t => t.CanDelete).ToArray();

            for (int i = 0; i < del.Length; i++)
            {
                del[i].Model.DeleteEntry();
                TasksList.Remove(del[i]);
            }

        }
        private void NewTask()
        {
            var newTask = new TaskViewModel(new TaskModel(_ProjectName));
            _TasksList.Add(newTask);
            newTask.Model.NewEntry();
        }
        private void TaskVM_newOrUpdateEntry(TaskViewModel sender)
        {
            
            if (sender == TasksList[TasksList.Count - 1])
            {
                sender.Model.NewEntry();
                _TasksList.Prepend(sender);
                _TasksList.Add(new TaskViewModel(new TaskModel(_ProjectName)));
            }
            else
                sender.Model.UpdateEntry();
        }
        
        private void Task_DeleteThis(TaskModel model, EventArgs deleteThisArgs)
        {
            for (int i = 0; i < TasksList.Count; i++)
            {
                if ( TasksList[i].Model == model)
                _TasksList.RemoveAt(i); 
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
        /// <summary>
        /// BUG: View not updating
        /// </summary>
        /// <param name="projectName"></param>
        private void FilterProjects(string projectName)
        {
            var filteredList = DataAccess.ReadTaskDB().Where(t => t.ProjectName == projectName).ToArray();
            TasksList.Clear();

            for (int i = 0; i < filteredList.Length; i++)
            {
                TasksList.Add(new TaskViewModel(filteredList[i]));
            }
        }

        #endregion
    }
}