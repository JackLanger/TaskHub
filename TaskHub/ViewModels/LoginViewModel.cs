using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskHub.Controlls.Commands;
using TaskHub.Models;

namespace TaskHub.ViewModels
{
    public class LoginViewModel : ModelBase
    {

        #region Members

        private UserModel _User;

        public UserModel User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        private ICommand _LoginCommand;
        private ICommand _RegisterNewUserCommand;

        //public ICommand RegisterNewUserCommand => _RegisterNewUserCommand?? new RelayCommand(()=>RegisterNewUser())

        public ICommand LoginCommand => _LoginCommand ??= new ParameterizedRelayCommand<string>((param) => Login(param));


        #endregion

        #region Constructor

        public LoginViewModel()
        {

        }

        #endregion

        #region Methods
        /// <summary>
        /// get the User with the Username and check make sure the hashed password matches the input.
        /// if thats the case set the user this user else display error message
        /// </summary>
        /// <param name="name"></param>
        public void Login(string name)
        {
             
        }

        #endregion
    }
}
