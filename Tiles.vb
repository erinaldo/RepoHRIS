Public Class Tiles
    Private Sub Tiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        Try
            BackColor = Color.Azure
            TransparencyKey = Color.Azure
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select leveluser from db_user where username = '" & TextBox1.Text & "'"
            Dim quers As String = CStr(query.ExecuteScalar)
            If quers = "5" Then
                TileItem14.Visible = True
            Else
                TileItem14.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Dim cus As New Customize

    Private Sub TileItem5_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem5.ItemClick
        If TileItem5.Text = "Main Application" Then
            MainClone.Close()
            With MainClone
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
        ElseIf TileItem5.Text = "Reports" Then
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem5.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        End If
    End Sub

    Private Sub TileItem6_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem6.ItemClick
        Dim pesan As String
        pesan = CType(MsgBox("Log Off Application?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Try
                Dim del As MySqlCommand = SQLConnection.CreateCommand
                del.CommandText = "truncate db_hasil"
                del.ExecuteNonQuery()
                Dim dele As MySqlCommand = SQLConnection.CreateCommand
                dele.CommandText = "truncate db_temp"
                dele.ExecuteNonQuery()
                dele.CommandText = "truncate db_tmpname"
                dele.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("tileitem6 " & ex.Message, MsgBoxStyle.Critical)
            End Try
            SQLConnection.Close()
            Close()
            Login.Close()
            StartPages.Close()
            Application.Exit()
        End If
    End Sub

    Private Sub TileItem8_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem8.ItemClick
        If TileItem8.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        ElseIf TileItem8.Text = "Reports" Then
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem8.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        End If
    End Sub

    Dim asp As New AspekKerja

    Private Sub TileItem9_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem9.ItemClick
        If TileItem8.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        ElseIf TileItem8.Text = "Reports" Then
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem8.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        End If
    End Sub

    Private Sub TileItem10_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem10.ItemClick
        If TileItem10.Text = "Reports" Then
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem10.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        ElseIf TileItem10.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        End If
    End Sub

    Private Sub TileItem11_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem11.ItemClick
        If TileItem11.Text = "Approvement" Then
            Approvement.Close()
            With Approvement
                .Label2.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem11.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        ElseIf TileItem11.Text = "Reports" Then
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        End If
    End Sub

    Dim us As New NewUser

    Private Sub TileItem13_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem13.ItemClick
        If us Is Nothing OrElse us.IsDisposed OrElse us.MinimizeBox Then
            us.Close()
            us = New NewUser
        End If
        us.Show()
    End Sub

    Dim auth As New Authorization

    Private Sub TileItem14_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem14.ItemClick
        If auth Is Nothing OrElse auth.IsDisposed OrElse auth.MinimizeBox Then
            auth.Close()
            auth = New Authorization
        End If
        auth.Show()
    End Sub
End Class