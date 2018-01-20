using System;
using System.Windows.Input;

namespace Substation_Builder.NodesEditor
{
    public class Command : ICommand
    {
        private readonly Action _action;
        private bool _isEnabled;

        public Command(Action action, bool isEnabled = true)
        {
            _action = action;
            _isEnabled = isEnabled;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                this.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecute(object parameter)
        {
            return _isEnabled;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}