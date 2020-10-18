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

        #endregion


        #region Constructors

        #endregion


        #region Methods


        #endregion

    }
}
