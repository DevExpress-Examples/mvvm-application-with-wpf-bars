Imports System
Imports DX_MVVM.Models

Namespace DX_MVVM.ViewModels
    Public Class PersonViewModel
        Inherits ViewModelBase

        Public Property FirstName() As String
            Get
                Return Person.FirstName
            End Get
            Set(ByVal value As String)
                If Person.FirstName = value Then
                    Return
                End If
                Person.FirstName = value
                OnPropertyChanged("FirstName")
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return Person.LastName
            End Get
            Set(ByVal value As String)
                If Person.LastName = value Then
                    Return
                End If
                Person.LastName = value
                OnPropertyChanged("LastName")
            End Set
        End Property
        Public Property Photo() As Uri
            Get
                Return Person.Photo
            End Get
            Set(ByVal value As Uri)
                If Person.Photo = value Then
                    Return
                End If
                Person.Photo = value
                OnPropertyChanged("Photo")
            End Set
        End Property
        Public Property Email() As String
            Get
                Return Person.Email
            End Get
            Set(ByVal value As String)
                If Person.Email = value Then
                    Return
                End If
                Person.Email = value
                OnPropertyChanged("Email")
            End Set
        End Property
        Public Property IsReadOnly() As Boolean
            Get
                Return isReadOnly_Renamed
            End Get
            Set(ByVal value As Boolean)
                If isReadOnly_Renamed = value Then
                    Return
                End If
                isReadOnly_Renamed = value
                OnPropertyChanged("IsReadOnly")
            End Set
        End Property
        Private privatePerson As Person
        Public Property Person() As Person
            Get
                Return privatePerson
            End Get
            Private Set(ByVal value As Person)
                privatePerson = value
            End Set
        End Property

        Public Sub New(ByVal person As Person)
            Me.Person = person
        End Sub


        Private isReadOnly_Renamed As Boolean = False
    End Class
End Namespace
