Public Class LeaveRequest
    Dim sel As New selectemp

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Timer1.Start()
        If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
            sel.Close()
            sel = New selectemp
        End If
        sel.Show()
    End Sub

    Sub changer()
        Dim maxid As Integer
        Dim newid As Integer = maxid + 1
        Dim ynow As String = Format(Now, "yy").ToString
        Dim mnow As String = Month(Now).ToString
        Dim lastn As Integer
        Dim updmon As String
        Dim lastcode As String = ""
        Dim tmp As Integer
        Dim cd As MySqlCommand = SQLConnection.CreateCommand
        Try
            Dim cmd = SQLConnection.CreateCommand()
            cmd.CommandText = "SELECT last_num FROM lastmemo2"
            lastn = DirectCast(cmd.ExecuteScalar(), Integer)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT MemoNo FROM db_attrec ORDER BY MemoNo DESC LIMIT 1"
            lastcode = DirectCast(cmd.ExecuteScalar(), String)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT MID(MemoNo, 8, 1) FROM db_attrec where MemoNo = '" & lastcode & "'"
            updmon = DirectCast(cmd.ExecuteScalar(), String)
            If CInt(updmon) <> CInt(mnow) Then
                tmp = 1
            Else
                tmp = lastn + 1
            End If
        Catch ex2 As Exception
        End Try
        Dim actualcode As String = "Memo" & ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
        txtmemo.Text = actualcode.ToString
    End Sub

    Private Sub LeaveRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        changer()
        autofill()
    End Sub

    Sub autofill()
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter("select EmployeeCode from db_pegawai", SQLConnection)
        da.Fill(dt)
        Dim r As DataRow
        TextBox2.AutoCompleteCustomSource.Clear()
        For Each r In dt.Rows
            TextBox2.AutoCompleteCustomSource.Add(r.Item(0).ToString)
        Next
    End Sub

    Dim dt1 As Date
    Dim dt2 As Date
    Dim dt3 As TimeSpan
    Dim diff As Double
    Dim hasil As String

    Sub remainoff()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select reason from db_attrec where employeecode ='" & TextBox3.Text & "'"
        Dim tab As New DataTable
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tab)
        For index As Integer = 0 To tab.Rows.Count - 1
        Next
    End Sub

    Sub test()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select workdate from db_pegawai where employeecode = '" & TextBox3.Text & "'"
        Dim quer11 As Date = CDate(query.ExecuteScalar)
        dt1 = CDate(quer11.ToShortDateString)
        dt2 = Date.Now
        dt3 = (dt2 - dt1)
        diff = dt3.Days
        hasil = CType(Int(diff / 30), String)
        Dim cuti As Integer
        If CInt(hasil) < 6 Then
            cuti = 0
        ElseIf CInt(hasil) < 12 Then
            cuti = 12 - CInt(hasil)
        ElseIf CInt(hasil) > 12 Then
            cuti = 12 + CInt(hasil) Mod 12
        End If
        TextEdit1.Text = cuti.ToString
    End Sub

    Sub countdayoff()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(*) from db_attreclist where reason = 'C ( Cuti )' and year(tgl)  = year(curdate()) and employeecode = '" & TextBox3.Text & "'"
            Dim tempday As Integer = CInt(query.ExecuteScalar)
            'MsgBox(tempday)
            query.CommandText = "select workdate from db_pegawai where employeecode = '" & TextBox3.Text & "'"
            Dim quer11 As Date = CDate(query.ExecuteScalar)
            dt1 = CDate(quer11.ToShortDateString)
            dt2 = Date.Now
            dt3 = (dt2 - dt1)
            diff = dt3.Days
            hasil = CType(Int(diff / 30), String)
            Dim cuti As Integer
            If CInt(hasil) < 6 Then
                cuti = 0
            ElseIf CInt(hasil) < 12 Then
                cuti = 12 - CInt(hasil)
            ElseIf CInt(hasil) > 12 Then
                cuti = 12 + CInt(hasil) Mod 12
            End If
            Dim actualdaysoff As Integer = cuti - tempday
            'MsgBox(actualdaysoff)
            TextEdit1.Text = actualdaysoff.ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub days()
        Dim t As TimeSpan = DateTimePicker3.Value.Date - DateTimePicker2.Value.Date
        Dim tmp As String
        tmp = t.Days.ToString
        Dim hasil As Integer
        hasil = CInt(tmp)
        Dim hasil2 As Integer = hasil + 1
        TextBox4.Text = hasil2.ToString
    End Sub

    Sub rangedate()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandType = CommandType.StoredProcedure
        query.CommandText = "rangedate"
        query.Parameters.AddWithValue("@date1", DateTimePicker3.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim adp As New MySqlDataAdapter(query)
        Dim ds1 As New DataSet
        adp.Fill(ds1)
        For Each dt As DataTable In ds1.Tables
            For Each row As DataRow In dt.Rows
                If Now.Date.DayOfWeek = 7 Then

                End If
            Next
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextBox2.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            TextBox3.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Sub insertlists()
        Dim dtb, dtr, dts As DateTime
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker3.CustomFormat = "yyyy-MM-dd"
        dtr = DateTimePicker2.Value
        dtb = DateTimePicker1.Value
        dts = DateTimePicker3.Value
        Dim d1 As Date = DateTimePicker2.Value.Date
        d1 = d1.AddDays(-1)
        Dim d2 As Date = DateTimePicker3.Value.Date
        For j As Integer = 1 To DateDiff("d", d1, d2)
            d1 = d1.AddDays(1)
            Dim cmd As MySqlCommand = SQLConnection.CreateCommand
            cmd.CommandText = "insert into db_attreclist" +
                          "(Memono, tgl, FullName, Employeecode, Reason)" +
                          "values(@Memono, @tgl, @names, @ec, @reason)"
            cmd.Parameters.AddWithValue("@Memono", txtmemo.Text)
            cmd.Parameters.AddWithValue("@tgl", d1.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@names", TextBox2.Text)
            cmd.Parameters.AddWithValue("@ec", TextBox3.Text)
            cmd.Parameters.AddWithValue("@reason", ComboBox1.Text)
            cmd.ExecuteNonQuery()
        Next
    End Sub

    Sub save()
        Dim dtb, dtr, dts As DateTime
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker3.CustomFormat = "yyyy-MM-dd"
        dtr = DateTimePicker2.Value
        dtb = DateTimePicker1.Value
        dts = DateTimePicker3.Value
        insertlists()
        Dim cmd1 As MySqlCommand = SQLConnection.CreateCommand
        cmd1.CommandType = CommandType.StoredProcedure
        cmd1.CommandText = "rangedate"
        cmd1.Parameters.AddWithValue("@date1", DateTimePicker2.Value.Date)
        cmd1.Parameters.AddWithValue("@date2", DateTimePicker3.Value.Date)
        Dim adp As New MySqlDataAdapter(cmd1)
        Dim ds1 As New DataSet
        adp.Fill(ds1)
        Dim cmd As MySqlCommand = SQLConnection.CreateCommand
        cmd.CommandText = "insert into db_attrec" +
                          "(Memono, tgl, FullName, Employeecode, Reason, StartDate, EndDate, TotalDays, Ket, ApprovedStatus)" +
                          "values(@Memono, @tgl, @names, @ec, @reason, @sd, @ed, @TotalDays, @ket, @stats)"
        cmd.Parameters.AddWithValue("@Memono", txtmemo.Text)
        cmd.Parameters.AddWithValue("@tgl", dtr.ToString("yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("@names", TextBox2.Text)
        cmd.Parameters.AddWithValue("@ec", TextBox3.Text)
        cmd.Parameters.AddWithValue("@reason", ComboBox1.Text)
        cmd.Parameters.AddWithValue("@@sd", dtr.ToString("yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("@ed", dts.ToString("yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("@TotalDays", TextBox4.Text)
        cmd.Parameters.AddWithValue("@ket", RichTextBox1.Text)
        cmd.Parameters.AddWithValue("@stats", "Requested")
        cmd.ExecuteNonQuery()
        MsgBox("Requested")
        ComboBox1.Text = ""
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Timer1.Stop()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select fullname from db_pegawai where employeecode = '" & TextBox2.Text & "'"
            Dim quer As String = CStr(query.ExecuteScalar)
            TextBox3.Text = quer.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        days()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        days()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If ComboBox1.Text = "" Or RichTextBox1.Text = "" OrElse TextBox3.Text = "" Then
            MsgBox("Please fill the blank field")
        ElseIf DateTimePicker2.Value.Date > DateTimePicker3.Value.Date OrElse DateTimePicker2.Value.Date = DateTimePicker3.Value.Date Then
            MsgBox("Total days can't be minus or empty or zero")
        ElseIf ComboBox1.Text = "C ( Cuti )" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select workdate from db_pegawai where employeecode = '" & TextBox3.Text & "'"
            Dim quer11 As Date = CDate(query.ExecuteScalar)
            dt1 = CDate(quer11.ToShortDateString)
            dt2 = Date.Now
            dt3 = (dt2 - dt1)
            diff = dt3.Days
            hasil = CType(Int(diff / 30), String)
            If CInt(hasil) < 6 Then
                MsgBox("Not allowed to do a days off due of an insufficient work duration", MsgBoxStyle.Information)
                'ElseIf textbox4.Text = TextEdit1.text Then
            Else
                save()
            End If
        Else
            save()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        Dim cha As Char = e.KeyChar
        If Char.IsDigit(cha) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        'countdayoff()
        'test()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ComboBox1.Text = "C ( Cuti )" Then
        '    countdayoff()
        'Else
        '    TextEdit1.Text = "0"
        'End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Try
            Dim a As Double = Convert.ToDouble(TextBox4.Text)
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(*) from db_attreclist where reason = 'C ( Cuti )' and year(tgl)  = year(curdate()) and employeecode = '" & TextBox3.Text & "'"
            Dim tempday As Integer = CInt(query.ExecuteScalar)
            'MsgBox(tempday)
            query.CommandText = "select workdate from db_pegawai where employeecode = '" & TextBox3.Text & "'"
            Dim quer11 As Date = CDate(query.ExecuteScalar)
            dt1 = CDate(quer11.ToShortDateString)
            dt2 = Date.Now
            dt3 = (dt2 - dt1)
            diff = dt3.Days
            hasil = CType(Int(diff / 30), String)
            Dim cuti As Integer
            If CInt(hasil) < 6 Then
                cuti = 0
            ElseIf CInt(hasil) < 12 Then
                cuti = 12 - CInt(hasil)
            ElseIf CInt(hasil) > 12 Then
                cuti = 12 + CInt(hasil) Mod 12
            End If
            Dim actualdaysoff As Integer = cuti - tempday
            TextEdit1.Text = CType(actualdaysoff - a, String)
        Catch ex As Exception
        End Try
    End Sub
End Class