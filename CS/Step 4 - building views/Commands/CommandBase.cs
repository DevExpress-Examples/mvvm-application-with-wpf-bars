using System;
using System.Windows.Input;
namespace DX_MVVM.Commands {
    public class CommandBase : ICommand {
        public event EventHandler CanExecuteChanged {
            add { if(canExecute != null) canExecuteChanged += value; }
            remove { if(canExecute != null) canExecuteChanged -= value; }
        }
        public CommandBase(Action<object> execute) : 
            this(execute, null) {
        }
        public CommandBase(Action<object> execute, Func<object, bool> canExecute) {
            if(execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter) {
            if(canExecute == null) return true;
            return canExecute(parameter);
        }
        public void Execute(object parameter) {
            execute(parameter);
        }
        public void RaiseCanExecuteChanged() {
            if(canExecuteChanged != null) canExecuteChanged(this, EventArgs.Empty);
        }

        Action<object> execute;
        Func<object, bool> canExecute;
        event EventHandler canExecuteChanged;
    }
}