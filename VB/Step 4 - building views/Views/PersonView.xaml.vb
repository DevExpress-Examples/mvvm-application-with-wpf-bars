Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Bars

Namespace DX_MVVM.Views
    ''' <summary>
    ''' Interaction logic for PersonView.xaml
    ''' </summary>
    Partial Public Class PersonView
        Inherits UserControl

        Public ReadOnly Property ChildBar() As Bar
            Get
                Return manager.Bars("PART_childBar1")
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
