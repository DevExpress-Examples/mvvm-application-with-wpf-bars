Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Data
Imports DevExpress.Xpf.Bars.Native

Namespace DXBarsAndMVVM.Commands
    Public Interface IMenuCommand
        ReadOnly Property Caption() As String
        ReadOnly Property Glyph() As Uri
        ReadOnly Property Binding() As Binding
        ReadOnly Property Command() As ICommand
        ReadOnly Property CommandParameter() As Object

    End Interface
End Namespace
