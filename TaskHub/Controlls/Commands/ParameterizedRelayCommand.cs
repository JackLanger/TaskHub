using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskHub.Controlls.Commands
{
    public class ParameterizedRelayCommand : ICommand
    {

        private readonly Action<object> _MyAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public ParameterizedRelayCommand(Action<object> action) => _MyAction = action;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _MyAction(parameter);
    }
}
