Public Class Pending

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        GroupControl2.Visible = False
    End Sub

    Private Sub Pending_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit2.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            TextEdit1.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        GroupControl2.Visible = True
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Close()
    End Sub

    Sub updation()
        Dim dt As Date
        txtinterviewdate.Format = DateTimePickerFormat.Custom
        txtinterviewdate.CustomFormat = "yyyy-MM-dd"
        dt = txtinterviewdate.Value
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_recruitment set status = @status, Interviewdates = @d1 WHERE idrec = @id"
        query.Parameters.AddWithValue("@status", "In Progress")
        query.Parameters.AddWithValue("@d1", dt.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@id", TextEdit1.Text)
        query.ExecuteNonQuery()
        MsgBox("Update Succesfully", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        updation()
    End Sub
End Class