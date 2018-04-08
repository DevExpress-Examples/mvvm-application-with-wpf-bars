Imports System
Imports System.Windows.Input
Namespace DX_MVVM.Commands
    Public Class CommandBase
        Implements ICommand

        Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                If canExecute_Renamed IsNot Nothing Then
                    AddHandler canExecuteChangedHandler, value
                End If
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                If canExecute_Renamed IsNot Nothing Then
                    RemoveHandler canExecuteChangedHandler, value
                End If
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
                RaiseEvent canExecuteChangedHandler(sender, e)
            End RaiseEvent
        End Event

        Public Sub New(ByVal execute As Action(Of Object))
            Me.New(execute, Nothing)
        End Sub
        Public Sub New(ByVal execute As Action(Of Object), ByVal canExecute As Func(Of Object, Boolean))
            If execute Is Nothing Then
                Throw New ArgumentNullException("execute")
            End If

            Me.execute_Renamed = execute
            Me.canExecute_Renamed = canExecute
        End Sub
        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            If canExecute_Renamed Is Nothing Then
                Return True
            End If
            Return canExecute_Renamed(parameter)
        End Function
        Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            execute_Renamed(parameter)
        End Sub
        Public Sub RaiseCanExecuteChanged()
            RaiseEvent canExecuteChangedHandler(Me, EventArgs.Empty)
        End Sub


        Private execute_Renamed As Action(Of Object)
        Private canExecute_Renamed As Func(Of Object, Boolean)

        Private Event canExecuteChangedHandler As EventHandler
    End Class
End Namespace