Public Class AttendanceInput
    Private Sub AttendanceInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
    End Sub

    Sub insertion()
        Dim dta As Date
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        dta = DateTimePicker1.Value
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "insert into db_absensi (tanggal, employeecode, fullname, jammulai, jamselesai, manualinput) values (@tgl, @ec, @fullname, @jammulai, @jamselesai, @manualinput)"
        query.Parameters.AddWithValue("@tgl", dta.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@ec", TextEdit1.Text)
        query.Parameters.AddWithValue("@fullname", TextEdit2.Text)
        query.Parameters.AddWithValue("@jammulai", TimeEdit2.Time)
        query.Parameters.AddWithValue("@jamselesai", TimeEdit1.Time)
        query.Parameters.AddWithValue("@manualinput", "1")
        query.ExecuteNonQuery()
        MsgBox("Inserted", MsgBoxStyle.Information)
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_absensi where employeecode = @emp and tanggal = @tgl"
        query.Parameters.AddWithValue("@emp", TextEdit1.Text)
        query.Parameters.AddWithValue("@tgl", DateTimePicker1.Value.Date)
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If quer = 0 Then
            insertion()
            Close()
        Else
            MsgBox("Data already exists", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit2.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            TextEdit1.Text = quer2.ToString
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub SimpleButton22_Click(sender As Object, e As EventArgs) Handles SimpleButton22.Click
        Timer1.Start()
        selectemp.Close()
        With selectemp
            .Show()
        End With
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        Timer1.Stop()
    End Sub
End Class