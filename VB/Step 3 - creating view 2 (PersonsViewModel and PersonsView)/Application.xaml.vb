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

#If Not SILVERLIGHT Then
        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            MyBase.OnStartup(e)
            ApplicationThemeHelper.ApplicationThemeName = "Office2007Silver"
            MainWindow = New DXWindow() With {.Title = "How to use the DevExpress components with MVVM pattern", .Width = 525, .Height = 350}
            MainWindow.Content = New MainPage()
            MainWindow.DataContext = PersonsViewModelCreator.PersonsViewModel
            MainWindow.Show()
        End Sub
#Else
        Public Sub New()
            AddHandler Me.Startup, AddressOf Me.Application_Startup
            AddHandler Me.Exit, AddressOf Me.Application_Exit
            AddHandler Me.UnhandledException, AddressOf Me.Application_UnhandledException

            ThemeManager.ApplicationThemeName = "Office2007Silver"
            InitializeComponent()
        End Sub

        Private Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)

            Dim mainPage_Renamed As New MainPage()
            mainPage_Renamed.DataContext = PersonsViewModelCreator.PersonsViewModel
            Me.RootVisual = mainPage_Renamed
        End Sub

        Private Sub Application_Exit(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Private Sub Application_UnhandledException(ByVal sender As Object, ByVal e As ApplicationUnhandledExceptionEventArgs)
            ' If the app is running outside of the debugger then report the exception using
            ' the browser's exception mechanism. On IE this will display it a yellow alert 
            ' icon in the status bar and Firefox will display a script error.
            If Not System.Diagnostics.Debugger.IsAttached Then

                ' NOTE: This will allow the application to continue running after an exception has been thrown
                ' but not handled. 
                ' For production applications this error handling should be replaced with something that will 
                ' report the error to the website and stop the application.
                e.Handled = True
                Deployment.Current.Dispatcher.BeginInvoke(Sub() ReportErrorToDOM(e))
            End If
        End Sub

        Private Sub ReportErrorToDOM(ByVal e As ApplicationUnhandledExceptionEventArgs)
            Try
                Dim errorMsg As String = e.ExceptionObject.Message + e.ExceptionObject.StackTrace
                errorMsg = errorMsg.Replace(""""c, "'"c).Replace(ControlChars.CrLf, ControlChars.Lf)

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(""Unhandled Error in Silverlight Application " & errorMsg & """);")
            Catch e1 As Exception
            End Try
        End Sub
#End If
    End Class
End Namespace
