Imports DX_MVVM.Models
Imports DX_MVVM.ViewModels

Namespace DX_MVVM.Helpers
    Public NotInheritable Class PersonsViewModelCreator

        Private Sub New()
        End Sub
        Public Shared ReadOnly PersonsViewModel As PersonsViewModel

        Shared Sub New()
            PersonsViewModel = New PersonsViewModel(PersonsCreator.Persons, AddressOf CreatingMethod)
        End Sub
        Private Shared Function CreatingMethod(ByVal person As Person) As PersonViewModel
            For Each personViewModel As PersonViewModel In PersonViewModelCreator.PersonViewModel
                If personViewModel.Person Is person Then
                    Return personViewModel
                End If
            Next personViewModel
            Return New PersonViewModel(person)
        End Function
    End Class
End Namespace
