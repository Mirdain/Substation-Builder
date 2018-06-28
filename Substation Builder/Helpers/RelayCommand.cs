using System;
using System.Diagnostics;
using System.Windows.Input;


namespace Substation_Builder.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> execute) : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public event EventHandler CanExecuteChanged
        {
            add {CommandManager.RequerySuggested += value;}
            remove {CommandManager.RequerySuggested -= value;}
        }

        public bool CanExecute(object parameter)
        {
            return canExecute != null && canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }

}
