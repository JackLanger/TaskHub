using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.Models
{
    public class UserModel:ModelBase
    {
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _UserType;

        public string UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        public int UserId { get; set; }


    }
}
