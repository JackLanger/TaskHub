using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using TaskHub.DAL;
using TaskHub.Model;
using TaskHub.Models;

namespace TaskHub.ViewModels
{
    public class MainViewModel:ModelBase
    {


        /// <summary>
        /// add notification to collection so grid displays correctly
        /// </summary>
        /// 
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<TaskModel> _TasksList;
        private UserModel _User;
        private List<TaskModel> data = new List<TaskModel>();
        private TaskModel _TaskModel;


        public TaskModel taskModel
        {
            get => _TaskModel;
            set
            {
                _TaskModel = value;
                OnPropertyChanged();
            }
        }



        protected void NotifyPropertyChanged([CallerMemberName]String info = null)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }



        public ObservableCollection<TaskModel> TasksList
        {
            get => _TasksList;
            private set
            {
                _TasksList = value;
            }
        }

        private TaskModel _ActiveTask;


        public TaskModel ActiveTask
        {
            get => _ActiveTask;
            set
            {
                _ActiveTask = value;
                NotifyPropertyChanged("_ActiveTask");
            }
        }

        public MainViewModel()
        {
            data = DataAccess.ReadTaskDB().ToList();

            _ActiveTask = new TaskModel("jack is great", "jack", "hack this", ActivityCheck.active.ToString(), true);
            
            TasksList = new ObservableCollection<TaskModel>();
            foreach (var task in DataAccess.ReadTaskDB())
            {

                
                TasksList.Add(task);
                //task.PropertyChanged += TaskModel_PropertyChanged;
            }

            _User = new UserModel() {UserName = "jack" };
        }

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