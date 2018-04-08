Imports System
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports DevExpress.Xpf.Core
Imports DX_MVVM.Commands
Imports DX_MVVM.Models

Namespace DX_MVVM.ViewModels
    Public Class PersonsViewModel
        Inherits LockableCollection(Of PersonViewModel)

        Private privateNewPersonCommand As CommandBase
        Public Property NewPersonCommand() As CommandBase
            Get
                Return privateNewPersonCommand
            End Get
            Private Set(ByVal value As CommandBase)
                privateNewPersonCommand = value
            End Set
        End Property
        Private privateDeletePersonCommand As CommandBase
        Public Property DeletePersonCommand() As CommandBase
            Get
                Return privateDeletePersonCommand
            End Get
            Private Set(ByVal value As CommandBase)
                privateDeletePersonCommand = value
            End Set
        End Property
        Private privatePersons As Persons
        Public Property Persons() As Persons
            Get
                Return privatePersons
            End Get
            Private Set(ByVal value As Persons)
                privatePersons = value
            End Set
        End Property
        Public Property Selected() As PersonViewModel
            Get
                Return Me(SelectedIndex)
            End Get
            Protected Set(ByVal value As PersonViewModel)
                SelectedIndex = IndexOf(value)
            End Set
        End Property
        Public Property SelectedIndex() As Integer
            Get
                Return selectedIndex_Renamed
            End Get
            Set(ByVal value As Integer)
                value = If(IsValidSelectedIndex(value), value, -1)
                If selectedIndex_Renamed = value Then
                    Return
                End If
                selectedIndex_Renamed = value
                OnSelectedIndexChanged()
            End Set
        End Property
        Default Public ReadOnly Overloads Property Item(ByVal person As Person) As PersonViewModel
            Get
                If person Is Nothing Then
                    Return Nothing
                End If
                For Each viewModel As PersonViewModel In Me
                    If viewModel.Person Is person Then
                        Return viewModel
                    End If
                Next viewModel
                Return Nothing
            End Get
        End Property

        Public Sub New(ByVal persons As Persons)
            Me.New(persons, Nothing)
        End Sub
        Friend Sub New(ByVal persons As Persons, ByVal creatingMethod As Func(Of Person, PersonViewModel))
            NewPersonCommand = New CommandBase(AddressOf OnNewPersonCommadExecute)
            DeletePersonCommand = New CommandBase(AddressOf OnDeletePersonCommadExecute, AddressOf OnDeletePersonCommadCanExecute)
            Me.CreatingMethod = creatingMethod
            Me.Persons = persons
            AddHandler Me.Persons.CollectionChanged, AddressOf OnPersonsCollectionChanged
            SyncCollection()
        End Sub
        Protected Overridable Sub OnNewPersonCommadExecute(ByVal paremeter As Object)
            Dim person As New Person() With {.FirstName = "First Name", .LastName = "Last Name"}
            Persons.Add(person)
            Selected = Me(person)
        End Sub
        Protected Overridable Sub OnDeletePersonCommadExecute(ByVal paremeter As Object)

            Dim selectedIndex_Renamed As Integer = SelectedIndex
            Persons.Remove(Selected.Person)
            If IsValidSelectedIndex(selectedIndex_Renamed) Then
                SelectedIndex = selectedIndex_Renamed
            ElseIf selectedIndex_Renamed >= Count Then
                SelectedIndex = Count - 1
            ElseIf selectedIndex_Renamed < 0 Then
                selectedIndex_Renamed = 0
            End If
        End Sub
        Protected Overridable Function OnDeletePersonCommadCanExecute(ByVal paremeter As Object) As Boolean
            Return IsValidSelectedIndex(SelectedIndex)
        End Function
        Protected Overridable Sub OnSelectedIndexChanged()
            OnPropertyChanged(New PropertyChangedEventArgs("Selected"))
            OnPropertyChanged(New PropertyChangedEventArgs("SelectedIndex"))
            DeletePersonCommand.RaiseCanExecuteChanged()
        End Sub
        Protected Overridable Sub OnPersonsCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            SyncCollection()
        End Sub
        Protected Sub SyncCollection()
            BeginUpdate()
            Clear()
            For Each person As Person In Persons
                Dim personViewModel As PersonViewModel
                If CreatingMethod IsNot Nothing Then
                    personViewModel = CreatingMethod(person)
                Else
                    personViewModel = New PersonViewModel(person)
                End If
                Add(personViewModel)
            Next person
            EndUpdate()
            If SelectedIndex = -1 AndAlso Count > 0 Then
                SelectedIndex = 0
            End If
        End Sub

        Private Function IsValidSelectedIndex(ByVal selectedIndex As Integer) As Boolean
            Return selectedIndex >= 0 AndAlso selectedIndex < Count
        End Function


        Private selectedIndex_Renamed As Integer = -1
        Private CreatingMethod As Func(Of Person, PersonViewModel) = Nothing
    End Class
End Namespace
