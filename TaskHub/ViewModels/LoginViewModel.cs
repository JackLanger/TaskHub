using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskHub.Controlls.Commands;
using TaskHub.DAL;
using TaskHub.Models;

namespace TaskHub.ViewModels
{

    public delegate void LoginHandler(object user);
    public class LoginViewModel : ModelBase
    {

        #region events
        public event LoginHandler LoginSuccessful;

        #endregion

        #region private Members

        private UserModel _User = null;
        private string _UserName;
        private string _PassW;
        private ApplicationPage _CurrentPage = ApplicationPage.Register;
        private string _LoginErrorMsg;
        private string _InvalidUserMsg;
        private string _InvalidPasswordMsg;

        #endregion

        #region public Members

        public ApplicationPage CurrentPage
        {
            get => _CurrentPage;
            set
            {
                _CurrentPage = value;
                OnPropertyChanged();
            }
        }


        public string InvalidUserMsg
        {
            get => _InvalidUserMsg;
            set
            {
                _InvalidUserMsg = value;
                OnPropertyChanged();
            }
        }

        public string InvalidPasswordMsg
        {
            get => _InvalidPasswordMsg;
            set
            {
                _InvalidPasswordMsg = value;
                OnPropertyChanged();
            }
        }

        public string LoginErrorMsg
        {
            get => _LoginErrorMsg;
            set
            {
                _LoginErrorMsg = value;
                OnPropertyChanged();
            }
        }

        public UserModel User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }

        public string PassW
        {
            get { return _PassW; }
            set
            {
                _PassW = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands

        private ICommand _LoginCommand;
        private ICommand _ContinueLoginCommand;
        private ICommand _RegisterNewUserCommand;
        private ICommand _GoToLoginCommand;
        private ICommand _GoToRegisterUser;


        public ICommand RegisterNewUserCommand => _RegisterNewUserCommand ?? new RelayCommand(() => RegisterNewUser());
        public ICommand LoginCommand => _LoginCommand ??= new RelayCommand(() => Login());
        public ICommand ContinueLoginCommand => _ContinueLoginCommand ??= new RelayCommand(() => OnLoginSuccess());
        public ICommand GoToRegisterUser => _GoToRegisterUser ??= new RelayCommand(() => CurrentPage = ApplicationPage.Register);
        public ICommand GoToLoginCommand => _GoToLoginCommand ??= new RelayCommand(() => CurrentPage = ApplicationPage.Login);


        #endregion


        #region Methods
        /// <summary>
        /// get the User with the Username and check make sure the hashed password matches the input.
        /// if thats the case set the user this user else display error message
        /// </summary>
        /// <param name="name"></param>
        private void OnLoginSuccess() => LoginSuccessful?.Invoke(_User);
        public void Login()
        {
            GetUser(_UserName, _PassW);
            if (_PassW == _User.GetPass())
                OnLoginSuccess();
        }

        private void GetUser(string name, string pass)
        {
            _User = new UserModel(name, pass);
            _User.GetUser(name);
        }

        private void RegisterNewUser()
        {
            if (_PassW == null || _PassW.Length<5)
            {
                InvalidPasswordMsg = "The Password is need to be at least 5 Characters long!";
            }
        }

        #endregion
    }
}
