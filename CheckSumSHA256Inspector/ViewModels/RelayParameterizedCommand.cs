using System;
using System.Windows.Input;

namespace CheckSumSHA256Inspector.ViewModels
{
    public class RelayParameterizedCommand : ICommand
    {
        private Action<object> _action;

        public RelayParameterizedCommand(Action<object> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

    }
}