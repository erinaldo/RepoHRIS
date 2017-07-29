Public Class StartPages
    Private Sub StartPages_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Location = New Point(Screen.PrimaryScreen.WorkingArea.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - Height)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Visible = True
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 10
        ElseIf ProgressBar1.Value = 100 Then
            Timer1.Stop()
        End If
        If ProgressBar1.Value = 40 Then
            With Login
                .Show()
                .Hide()
            End With
        ElseIf ProgressBar1.Value = 100 Then
            Login.Show()
            Hide()
        End If
    End Sub
End Class