Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports DX_MVVM.Helpers
Imports DX_MVVM.Views

Namespace DX_MVVM
    Partial Public Class MainPage
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub PersonsView_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim personsView_Renamed As PersonsView = DirectCast(sender, PersonsView)
            manager.Bars("mainBar").UnMerge()
            manager.Bars("mainBar").Merge(personsView_Renamed.ChildBar)
        End Sub
        Private Sub PersonView_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim personView_Renamed As PersonView = DirectCast(sender, PersonView)
            manager.Bars("mainBar").UnMerge()
            manager.Bars("mainBar").Merge(personView_Renamed.ChildBar)
        End Sub
    End Class
End Namespace
