Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DX_MVVM.Helpers

Namespace DX_MVVM
    ''' <summary>
    ''' Interaction logic for App.xaml
    ''' </summary>
    Partial Public Class App
        Inherits Application


        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            MyBase.OnStartup(e)
            ApplicationThemeHelper.ApplicationThemeName = "Office2007Silver"
            MainWindow = New DXWindow() With {.Title = "How to use the DevExpress components with MVVM pattern", .Width = 525, .Height = 350}
            MainWindow.Content = New MainPage()
            MainWindow.DataContext = PersonsViewModelCreator.PersonsViewModel
            MainWindow.Show()
        End Sub

    End Class
End Namespace
