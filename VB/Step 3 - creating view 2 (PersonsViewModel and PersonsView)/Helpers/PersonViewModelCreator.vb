Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DX_MVVM.ViewModels

Namespace DX_MVVM.Helpers
    Public NotInheritable Class PersonViewModelCreator

        Private Sub New()
        End Sub

        Public Shared ReadOnly PersonViewModel() As PersonViewModel
        Public Shared ReadOnly Property DesignPersonViewModel() As PersonViewModel
            Get
                Return PersonViewModel(0)
            End Get
        End Property

        Shared Sub New()
            Dim length As Integer = PersonCreator.Person.Length
            PersonViewModel = New PersonViewModel(length - 1){}
            For i As Integer = 0 To length - 1
                PersonViewModel(i) = New PersonViewModel(PersonCreator.Person(i)) With {.IsReadOnly = True}
            Next i
        End Sub
    End Class
End Namespace
