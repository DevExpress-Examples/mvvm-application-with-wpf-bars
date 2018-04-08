Imports System
Imports DX_MVVM.Models
Imports System.Windows.Media.Imaging

Namespace DX_MVVM.Helpers
    Public NotInheritable Class PersonCreator

        Private Sub New()
        End Sub
        Public Shared ReadOnly Person() As Person

        Shared Sub New()
            Person = New Person(3){}
            Person(0) = New Person() With {.FirstName = "Anne", .LastName = "Dodsworth", .Photo = New Uri("/DX_MVVM;component/Images/AnneDodsworth.jpg", UriKind.Relative), .Email = "Anne.Dodsworth@persons.com"}
            Person(1) = New Person() With {.FirstName = "Nancy", .LastName = "Davolio", .Photo = New Uri("/DX_MVVM;component/Images/NancyDavolio.jpg", UriKind.Relative), .Email = "Nancy.Davolio@persons.com"}
            Person(2) = New Person() With {.FirstName = "Robert", .LastName = "King", .Photo = New Uri("/DX_MVVM;component/Images/RobertKing.jpg", UriKind.Relative), .Email = "Robert.King@persons.com"}
            Person(3) = New Person() With {.FirstName = "Steven", .LastName = "Buchanan", .Photo = New Uri("/DX_MVVM;component/Images/StevenBuchanan.jpg", UriKind.Relative), .Email = "Steven.Buchanan@persons.com"}
        End Sub
    End Class
End Namespace
