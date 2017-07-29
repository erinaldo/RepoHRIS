Imports System.IO
Public Class ModAK
    Private Sub CheckEdit3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit3.CheckedChanged
        If CheckEdit3.Checked = True Then
            TextEdit4.Enabled = True
            SimpleButton1.Enabled = True
        Else
            TextEdit4.Enabled = False
            SimpleButton1.Enabled = False
        End If
    End Sub

    Private Sub ModAK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        GridView1.BestFitColumns()
    End Sub

    Sub clear()
        TextEdit1.Text = ""
        TextEdit2.Text = ""
        TextEdit3.Text = ""
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
    End Sub

    Sub insertion2()
        Try
            Dim lastm As String
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(goals) from db_targetbor where code = '" & TextEdit1.Text & "'"
            Dim quer As Integer = CInt(query.ExecuteScalar)
            If quer = 0 Then
                lastm = "Target 1"
            Else
                lastm = "Target " & quer + 1 & ""
            End If
            query.CommandText = "insert into db_targetbor" +
                                "(Goals, Quantity, Code, Name)" +
                                " values(@goals, @quantity, @code, @name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@goals", lastm)
            query.Parameters.AddWithValue("@quantity", TextEdit4.Text)
            query.Parameters.AddWithValue("@code", TextEdit1.Text)
            query.Parameters.AddWithValue("@name", TextEdit2.Text)
            query.ExecuteNonQuery()
            MsgBox("Success", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub insertion()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(tasks) from db_calcbor where tasks = '" & TextEdit1.Text & "'"
            Dim quer As Integer = CInt(query.ExecuteScalar)
            If quer = 0 Then
                query.CommandText = "insert into db_calcbor" +
                            "(tasks, Nama, Amount, Disabled, Periodic)" +
                            " values (@tasks, @Nama, @Amount, @Disabled, @Periodic)"
                query.Parameters.AddWithValue("@tasks", TextEdit1.Text)
                query.Parameters.AddWithValue("@nama", TextEdit2.Text)
                query.Parameters.AddWithValue("@Amount", TextEdit3.Text)
                query.Parameters.AddWithValue("@Disabled", CheckEdit1.Checked)
                query.Parameters.AddWithValue("@Periodic", CheckEdit2.Checked)
                query.ExecuteNonQuery()
                MsgBox("Added", MsgBoxStyle.Information)
                ' clear()
                CheckEdit3.Enabled = True
            Else
                MsgBox("There's already exist a tasks with a same name", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If TextEdit1.Text = "" OrElse TextEdit2.Text = "" Then
            MsgBox("Please fill the empty fields", MsgBoxStyle.Exclamation)
        Else
            insertion()
        End If
    End Sub

    Sub showtarget()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_targetbor where code = '" & TextEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If TextEdit4.Text = "" Then
            MsgBox("Fill the empty fields", MsgBoxStyle.Information)
        Else
            insertion2()
            showtarget()
        End If
    End Sub

    Private Sub CheckEdit2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            CheckEdit3.Checked = False
        End If
    End Sub
End Class