Imports DX_MVVM.Models

Namespace DX_MVVM.Helpers
    Public NotInheritable Class PersonsCreator

        Private Sub New()
        End Sub
        Public Shared ReadOnly Persons As Persons

        Shared Sub New()
            Persons = New Persons()
            For Each person As Person In PersonCreator.Person
                Persons.Add(person)
            Next person
        End Sub
    End Class
End Namespace
