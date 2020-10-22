using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHub.DAL;

namespace TaskHub.Models
{
    public class ProjectModel : ModelBase
    {
        #region Private Members
        private int _ProjectID;
        private string _ProjectName;
        private int _ActiveProjects;
        #endregion


        #region public Members

        public int ActiveProjects
        {
            get => _ActiveProjects;
            set
            {
                _ActiveProjects = value; 
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

        public int ProjectID
        { get => _ProjectID;
            set
            {
                _ProjectID = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Constructors

        public ProjectModel() :this ("",999)
        {

        }
        public ProjectModel(string name,int id)
        {
            _ProjectName = name;
            _ProjectID = id;
        }

        #endregion


        #region Methods
        public void Delete() => DataAccess.RemoveEntry(this);
        public void Update() => DataAccess.UpdateDb(this);
        public void Create() => DataAccess.WriteNewEntry(this);

        #endregion

    }
}
