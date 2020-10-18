using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using TaskHub.Controlls.Commands;
using TaskHub.Models;

namespace TaskHub.ViewModels
{

    public delegate void FilterProjectHandler(string projectName);

    public class ProjectViewModel : ModelBase
    {

        public event FilterProjectHandler FilterProjects;

        #region private Members

        private ProjectModel _Project;
        private string _ProjectName;
        private int _ActiveTasks; 

        #endregion

        #region public Members

        public string ProjectName
        {
            get => _ProjectName;
            set 
            {
                _ProjectName = value; 
                OnPropertyChanged();
            }
        }
        public int ActiveTasks
        {
            get => _ActiveTasks;
            set
            {
                _ActiveTasks = value;
                OnPropertyChanged();
            }
        }
        public ProjectModel Project
        {
            get => _Project;
            set
            {
                _Project = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        public ProjectViewModel(ProjectModel project)
        {
            _Project = project;
            _ProjectName = project.ProjectName;
        }

        private ICommand _FilterCommand;

        public ICommand FilterCommand => _FilterCommand??= new ParameterizedRelayCommand<string>(p => FilterProjects?.Invoke(p));

    }
}
