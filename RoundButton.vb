Imports System.Drawing.Drawing2D
Public Class RoundButton
    Inherits Button
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim grPath As GraphicsPath = New GraphicsPath()
        grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height)
        Me.Region = New Region(grPath)
        MyBase.OnPaint(e)
    End Sub
End Class
