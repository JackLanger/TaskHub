using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskHub.Controlls.Commands
{
    public class RelayCommand : ICommand
    {

        private readonly Action _MyAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand( Action action)
        {
            _MyAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _MyAction();
        }
    }
}
