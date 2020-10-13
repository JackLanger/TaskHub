using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskHub.Controlls.Commands
{
    public class ParameterizedRelayCommand<T> : ICommand
    {

        private readonly Action<T> _MyAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public ParameterizedRelayCommand(Action<T> action) => _MyAction = action;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _MyAction((T)parameter);
    }
}
