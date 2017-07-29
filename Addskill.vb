Public Class Addskill
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit1.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Addskill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Timer1.Start()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()
        selectemp.Close()
        With selectemp
            .Show()
        End With
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        Timer1.Stop()
    End Sub

    Public Sub skills()
        Dim sqlCommand As New MySqlCommand
        Dim str_carSql As String
        Dim sk As MySqlCommand = SQLConnection.CreateCommand
        sk.CommandText = "select idrec from db_skills where idrec = '" & txtidrecc.Text & "'"
        Dim sk1 As String = CStr(sk.ExecuteScalar)
        If sk1 = "" Then
            If skill1.Text = "" OrElse skill2.Text = "" OrElse skill3.Text = "" OrElse skill4.Text = "" OrElse skill5.Text = "" OrElse txtidrecc.Text = "" Then
                MsgBox("Please fill the required fields", MsgBoxStyle.Exclamation)
            Else
                Try
                    str_carSql = "INSERT INTO db_skills " +
                           "(IdRec, FullName, Skill1, Skill2, Skill3, Skill4, Skill5, Interviewer) " +
                           "values (@Idrec, @fullname, @skill1, @skill2, @skill3, @skill4, @skill5, @interviewer)"
                    sqlCommand.Connection = SQLConnection
                    sqlCommand.CommandText = str_carSql
                    sqlCommand.Parameters.AddWithValue("@IdRec", txtidrecc.Text)
                    sqlCommand.Parameters.AddWithValue("@FullName", txtname.Text)
                    sqlCommand.Parameters.AddWithValue("@skill1", skill1.Text)
                    sqlCommand.Parameters.AddWithValue("@skill2", skill2.Text)
                    sqlCommand.Parameters.AddWithValue("@skill3", skill3.Text)
                    sqlCommand.Parameters.AddWithValue("@skill4", skill4.Text)
                    sqlCommand.Parameters.AddWithValue("@skill5", skill5.Text)
                    sqlCommand.Parameters.AddWithValue("@interviewer", TextEdit1.Text)
                    sqlCommand.ExecuteNonQuery()
                    MsgBox("Data Succesfully Added", MsgBoxStyle.Information)
                    Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            MsgBox("The data already exists", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        skills()
    End Sub
End Class