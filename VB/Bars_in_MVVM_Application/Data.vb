Imports DevExpress.Mvvm
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace Bars_in_MVVM_Application

    Public Enum Priority
        Low
        Normal
        High
    End Enum

    Public Enum Tag
        Urgent
        NeedResearch
        Complex
        Easy
        Postponed
    End Enum

    Public Class Issue
        Inherits BindableBase

        <[ReadOnly](True)>
        Public Property Id As Integer
            Get
                Return GetValue(Of Integer)()
            End Get

            Set(ByVal value As Integer)
                SetValue(value)
            End Set
        End Property

        Public Property Subject As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property Priority As Priority
            Get
                Return GetValue(Of Priority)()
            End Get

            Set(ByVal value As Priority)
                SetValue(value)
            End Set
        End Property

        Public Property Completed As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
                SetValue(value)
            End Set
        End Property

        Public Property Tags As List(Of Object)
            Get
                Return GetValue(Of List(Of Object))()
            End Get

            Set(ByVal value As List(Of Object))
                SetValue(value)
            End Set
        End Property
    End Class

    Public Class IssueDataModel

        Public Shared Function GetIssues() As ObservableCollection(Of Issue)
            Dim issies = New ObservableCollection(Of Issue)() From {New Issue() With {.Id = 0, .Subject = "Will we track sales history in our system?", .Priority = Priority.Normal, .Completed = True, .Tags = New List(Of Object)() From {Tag.Urgent, Tag.NeedResearch}}, New Issue() With {.Id = 1, .Subject = "What database types will we support?", .Priority = Priority.Low, .Completed = True, .Tags = New List(Of Object)() From {Tag.Complex, Tag.Postponed}}, New Issue() With {.Id = 2, .Subject = "We are using different paths for different modules.", .Priority = Priority.High, .Completed = False, .Tags = New List(Of Object)() From {Tag.Complex, Tag.NeedResearch}}, New Issue() With {.Id = 3, .Subject = "Inconsistency. Please fix it.", .Priority = Priority.High, .Completed = False, .Tags = New List(Of Object)() From {Tag.Urgent, Tag.Easy}}, New Issue() With {.Id = 4, .Subject = "Somebody has to look at it.", .Priority = Priority.Normal, .Completed = False, .Tags = New List(Of Object)() From {Tag.Postponed, Tag.Easy}}}
            Return issies
        End Function
    End Class
End Namespace
