Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Core

Namespace DX_MVVM
    ''' <summary>
    ''' Interaction logic for App.xaml
    ''' </summary>
    Partial Public Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            MyBase.OnStartup(e)
            MainWindow = New DXWindow() With {.Width = 525, .Height = 350}
            MainWindow.Content = New MainPage()
            MainWindow.Show()
        End Sub
    End Class
End Namespace
