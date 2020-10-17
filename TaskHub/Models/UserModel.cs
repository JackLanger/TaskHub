using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TaskHub.DAL;

namespace TaskHub.Models
{
    public class UserModel : ModelBase
    {
        #region private Members

        private string _UserName;
        private string _UserType;

        #endregion

        #region public Members
        public int UserID { get; set; }
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }

        public string UserType
        {
            get { return _UserType; }
            set
            {
                _UserType = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public UserModel(string name) : this(name, "standardUser") { }

        public UserModel(string name, string Type = "standardUser")
        {
            _UserName = name;
            UserType = Type;
        }

        #endregion

        public void RegisterNewUser() => DataAccess.RegisterNewUser(this);
        public void GetUser(string userName) => DataAccess.GetUser(userName);
        /// <summary>
        /// TODO:implement hashing of UserName so we can retrive UserKey dynamically and don't have to save it to DB
        /// </summary>
        /// <returns></returns>
        private string GetKey() => _UserName;

        public string GetPass()
        {
            return DataAccess.FetchPassword(GetKey());
        }
    }
}
