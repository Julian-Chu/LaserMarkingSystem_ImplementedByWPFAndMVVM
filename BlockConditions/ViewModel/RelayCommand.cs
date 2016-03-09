using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlockConditionsWindow.ViewModel
{
    public class RelayCommand:ICommand
    {
        private Action<object> _execute;

        private Predicate<object> _canExecute;

        private event EventHandler _CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            if (canExecute == null) throw new ArgumentNullException("canExecute");
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute):this(execute,DefaultCanExecute)
        { }

        private static bool DefaultCanExecute(object obj)
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && this._canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this._CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                this._CanExecuteChangedInternal -= value;
            }
        }

        public void Execute(object parameter)
        {
            if(_canExecute!=null && this._canExecute(parameter))
            this._execute(parameter);
        }


        public void OnCanExecuteChanged()
        {
            EventHandler handler = this._CanExecuteChangedInternal;
            if (handler != null) handler.Invoke(this, EventArgs.Empty);
        }

        public void Destroy()
        {
            this._execute = o => { return; };
            this._canExecute=o=>false;
        }
    }
}
