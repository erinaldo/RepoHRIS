Public Class NewUser
    Private Sub NewUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
    End Sub

    Sub reg()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "insert into db_user (username, password, leveluser, lastvisit) values (@username, MD5(@password), @leveluser, @lastvisit)"
        query.Parameters.AddWithValue("@username", TextEdit1.Text)
        query.Parameters.AddWithValue("@password", TextEdit2.Text)
        query.Parameters.AddWithValue("@leveluser", "1")
        query.Parameters.AddWithValue("@lastvisit", Date.Now)
        query.ExecuteNonQuery()
        MsgBox("Registered!", MsgBoxStyle.Information)
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If TextEdit1.Text = "" OrElse TextEdit2.Text = "" Then
            MsgBox("Please fill the empty fields", MsgBoxStyle.Information)
        ElseIf TextEdit2.Text <> TextEdit3.Text Then
            MsgBox("Password didn't match!", MsgBoxStyle.Exclamation)
        Else
            Try
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select count(username) from db_user where username = '" & TextEdit1.Text & "'"
                Dim quer As Integer = CInt(query.ExecuteScalar)
                If quer > 0 Then
                    MsgBox("User already exist, Please use another one", MsgBoxStyle.Information)
                Else
                    reg()
                    Close()
                End If
            Catch ex As Exception
                MsgBox("SignUp Part--> " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
End Class