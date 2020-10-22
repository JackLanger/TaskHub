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
        private string _NewProjectName;

        public string NewProjectName
        {
            get => _NewProjectName;
            set {
                _NewProjectName = value;
                OnPropertyChanged();
            }
        }




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
        private ICommand _DeleteProjectCommand;
        private ICommand _NewProjectCommand;


        public ICommand DeleteTaskCommand => _DeleteTaskCommand ??= new RelayCommand(() => DeleteTask());
        public ICommand AddNewTaskCommand => _AddNewTaskCommand ??= new RelayCommand(() => NewTask());
        public ICommand NewProjectCommand => _NewProjectCommand ??= new ParameterizedRelayCommand<string>((param) => NewProject(param));
        public ICommand DeleteProjectCommand => _DeleteProjectCommand ??= new RelayCommand(() => DeleteProject());

        #endregion



        #region constructor

        public MainViewModel()
        {
            _ProjectName = "";
            data = (List<TaskModel>)DataAccess.ReadTaskDB();
            _TasksList = new ObservableCollection<TaskViewModel>();
            _Projects = new ObservableCollection<ProjectViewModel>();
            _Project = new ProjectViewModel(new ProjectModel("show All"));
            _Projects.Add(_Project);

            foreach (var project in DataAccess.ReadProjectDb())
            {
                _Projects.Add(new ProjectViewModel(project));
            }

            foreach (var pvm in _Projects)
            {
                if (pvm.Project.ProjectID == 0) pvm.Project.ProjectID = _Projects.Last().Project.ProjectID++;
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

                if (project.ProjectName=="show All")
                {
                    project.ActiveTasks = TasksList
                                          .Where(t => t.Model.IsActive)
                                          .Count();
                }
                else
                {

                    project.ActiveTasks = TasksList
                                          .Where(t => t.Model.IsActive && t.Model.ProjectName == project.ProjectName)
                                          .Count();
                }
            }
            foreach(var p in _Projects)
            {
                p.FilterProjects += FilterProjects;
            }
        }


        #endregion



        #region Methods

        /// <summary>
        /// TODO: add new Project on button press and populate the project with a standard name like new Project.
        ///         write project to DB
        /// TODO: add a delete button for the project selected.
        /// </summary>
        private void NewProject(string name)
        {
            _Projects.Add(new ProjectViewModel(new ProjectModel(name, _Projects[_Projects.Count() - 1].Project.ProjectID++)));
            _Projects[_Projects.Count() - 1].Project.Create();
        }
        /// <summary>
        /// delete the selected project from db and remove from list
        /// </summary>
        /// <param name="projectName"></param>
        private void DeleteProject()
        {
            if (_ProjectName != null && _Projects.Any(p=> p .ProjectName == _ProjectName)){
                ProjectViewModel ProjectVM = _Projects.First(p => p.ProjectName == _ProjectName);

                DataAccess.RemoveEntry(ProjectVM.Project);
                _Projects.Remove(ProjectVM);
            }
        }
        /// <summary>
        /// delete all tasks where CanDelete is true
        /// </summary>
        private void DeleteTask()
        {
            var del = TasksList.Where(t => t.CanDelete).ToArray();

            for (int i = 0; i < del.Length; i++)
            {
                del[i].Model.DeleteEntry();
                TasksList.Remove(del[i]);
            }
        }
        /// <summary>
        /// add a new Task to the list and create a new entry in the db
        /// </summary>
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
        /// <summary>
        /// if the projectname == show All select all tasks, else select the tasks with the given project name
        /// </summary>
        /// <param name="projectName"></param>
        private void FilterProjects(string projectName)
        {
            var filteredList = DataAccess.ReadTaskDB().Where(t => t.ProjectName == projectName).ToArray();

            if (projectName == "show All")
            {
                filteredList = DataAccess.ReadTaskDB().ToArray();
            }

            TasksList.Clear();

            for (int i = 0; i < filteredList.Length; i++)
            {
                TasksList.Add(new TaskViewModel(filteredList[i]));
            }
            _ProjectName = projectName;
        }

        #endregion
    }
}