using System;
using System.Windows.Input;

namespace RealSense.Commands
{
    public class RelayCommand:ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action _methodToExecute;
        private Func<bool> _canExecuteEvaluator;

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute):this(methodToExecute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteEvaluator?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _methodToExecute.Invoke();
        }
    }
}