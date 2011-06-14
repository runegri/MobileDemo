using System;
using System.Windows.Input;

namespace DemoLib
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<object, bool> _canExecuteMethod;
        private readonly Action<object> _executeMethod;

        public DelegateCommand(Action<object> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        public DelegateCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
            : this(executeMethod)
        {
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeMethod(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void UpdateCanExecute()
        {
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged != null)
            {
                canExecuteChanged(this, new EventArgs());
            }
        }

    }
}
