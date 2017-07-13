Public Class setting
    Dim connectionString As String = "Server=localhost; User Id=root; Password=; Database=db_hris"
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        SQLConnection = New MySqlConnection()
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Dim tbl_par As New DataTable
        Dim sqlCommand As New MySqlCommand
        sqlCommand.CommandText = "SELECT * FROM db_user WHERE username ='" + teUsername.Text + "' and password='" + tePassword.Text + "'"
        sqlCommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par)
        If tbl_par.Rows.Count > 0 Then
            For index As Integer = 0 To tbl_par.Rows.Count - 1
                SQLConnection = New MySqlConnection()
                SQLConnection.ConnectionString = connectionString
                SQLConnection.Open()
                Try
                    sqlCommand.CommandText = "UPDATE db_user SET username = @username, password= @password, ChangeDate=@tanggal ) " +
                                             "WHERE id_user=" + tbl_par.Rows(0).Item(0).ToString()
                    sqlCommand.Parameters.AddWithValue("@username", teUsername.Text)
                    sqlCommand.Parameters.AddWithValue("@password", teNewPassword.Text)
                    sqlCommand.Parameters.AddWithValue("@tanggal", Date.Now().ToString())
                    sqlCommand.Connection = SQLConnection
                    sqlCommand.ExecuteNonQuery()
                    SQLConnection.Close()
                    MessageBox.Show("Succesfully Changed!!")
                Catch ex As Exception
                    SQLConnection.Close()
                    MsgBox(ex.Message)
                End Try
            Next
        Else
            MsgBox("Username & Password Didn't Match")
        End If
        SQLConnection.Close()
    End Sub

    Private Sub setting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class