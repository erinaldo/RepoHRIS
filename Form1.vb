Public Class Form1
    Sub hol()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandType = CommandType.StoredProcedure
        sqlcommand.CommandText = "rangedate"
        Dim p1 As New MySqlParameter
        Dim p2 As New MySqlParameter
        p1.ParameterName = "@date1"
        p2.ParameterName = "@date2"
        p1.Value = date1.Value.Date
        p2.Value = date2.Value.Date
        sqlcommand.Parameters.Add(p1)
        sqlcommand.Parameters.Add(p2)
        Dim dt As New DataTable
        dt.Load(sqlcommand.ExecuteReader)
        GridControl1.DataSource = dt
        sqlcommand.Connection = SQLConnection
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
    End Sub

    Sub insertion()
        Dim d1 As Date = date1.Value.Date
        Dim d2 As Date = date2.Value.Date
        For j As Integer = 1 To CInt(DateDiff("D", d1, d2)) '*parse to integer     
            d1 = d1.AddDays(1)
            Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
            sqlcommand.CommandText = "insert into db_absensi" +
                                        "(FullName, EmployeeCode, Tanggal, Shift, JamMulai, JamSelesai, OvertimeType, OvertimeHours, isHoliday, Remarks)" +
                                        " Values (@FullName, @EmployeeCode, @tanggal, @shift, @JamMulai, @jamselesai, @overtimetype, @overtimehours, @isholiday, @remarks)"
            sqlcommand.Parameters.AddWithValue("@FullName", "Steven")
            sqlcommand.Parameters.AddWithValue("@EmployeeCode", "17-4-00001")
            sqlcommand.Parameters.AddWithValue("@tanggal", d1.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@Shift", "1")
            sqlcommand.Parameters.AddWithValue("@JamMulai", d1.ToString("yyyy-MM-dd") & "08:00:00")
            sqlcommand.Parameters.AddWithValue("@jamselesai", d1.ToString("yyyy-MM-dd") & "17:00:00")
            sqlcommand.Parameters.AddWithValue("@overtimetype", "")
            sqlcommand.Parameters.AddWithValue("@overtimehours", "")
            sqlcommand.Parameters.AddWithValue("@isHoliday", "")
            sqlcommand.Parameters.AddWithValue("@remarks", "")
            sqlcommand.ExecuteNonQuery()
            MsgBox("added")
        Next
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        insertion()
        'Dim Report1 As New CrystalReport4
        'Dim TextObject1 As TextObject = CType(Report1.Section1.ReportObjects("Text29"), TextObject)
        'TextObject1.Text = "xxxxxx"
        'crys.CrystalReportViewer1.ReportSource = Report1
        'If crys Is Nothing OrElse crys.IsDisposed OrElse crys.MinimizeBox Then
        '    crys.Close()
        '    crys = New cryrep
        'End If
        'crys.Show()
    End Sub
End Class