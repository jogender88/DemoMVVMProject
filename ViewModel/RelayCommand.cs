using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoCrudMVVM.ViewModel
{
    public class RelayCommand : ICommand
    {
        Action<object> executeAction;
        Func<object, bool> canExecute;

        public RelayCommand(Action<object> executeAction, Func<object,bool> canExecute)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }

        }
      
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
