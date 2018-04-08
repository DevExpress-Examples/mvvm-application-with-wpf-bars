Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Input

Namespace DXBarsAndMVVM.Commands
    'public class MenuCommandBase : IMenuCommand {
    '    public string Caption { get; set; }
    '    public Uri Glyph { get; set; }
    '    public event EventHandler CanExecuteChanged {
    '        add { if(canExecute != null) CommandManager.RequerySuggested += value; }
    '        remove { if(canExecute != null) CommandManager.RequerySuggested -= value; }
    '    }

    '    public MenuCommandBase(Action<object> execute, Func<object, bool> canExecute) {
    '        if(execute == null)
    '            throw new ArgumentNullException("execute");
    '        this.execute = execute;
    '        this.canExecute = canExecute;
    '    }
    '    public bool CanExecute(object parameter) {
    '        if(canExecute == null) return true;
    '        return canExecute(parameter);
    '    }
    '    public void Execute(object parameter) {
    '        execute(parameter);
    '    }

    '    Action<object> execute;
    '    Func<object, bool> canExecute;
    '}
End Namespace
