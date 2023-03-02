Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace Bars_in_MVVM_Application

    Public Class ViewModel
        Inherits ViewModelBase

        Public Sub New()
            Issues = IssueDataModel.GetIssues()
            SelectedIssue = Issues.FirstOrDefault()
        End Sub

        Public Property Issues As ObservableCollection(Of Issue)
            Get
                Return GetValue(Of ObservableCollection(Of Issue))()
            End Get

            Set(ByVal value As ObservableCollection(Of Issue))
                SetValue(value)
            End Set
        End Property

        Public Property SelectedIssue As Issue
            Get
                Return GetValue(Of Issue)()
            End Get

            Set(ByVal value As Issue)
                SetValue(value)
            End Set
        End Property

        <Command>
        Public Sub AddIssue()
            Dim newId As Integer = If(Issues.Count = 0, 0, Issues.Max(Function(p) p.Id) + 1)
            Dim issue As Issue = New Issue() With {.Id = newId, .Subject = "New Issue " & newId, .Completed = False, .Priority = Priority.Low}
            Issues.Add(issue)
        End Sub

        <Command>
        Public Sub RemoveIssue()
            Issues.Remove(SelectedIssue)
        End Sub

        Public Function CanRemoveIssue() As Boolean
            Return SelectedIssue IsNot Nothing
        End Function
    End Class
End Namespace
