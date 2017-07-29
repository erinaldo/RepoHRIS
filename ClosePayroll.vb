Public Class ClosePayroll
    Dim tbl_par As New DataTable

    Sub loaddata()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "SELECT FullName, EmployeeCode From db_payrolldata"
        sqlcommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par)
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtname.Properties.Items.Add(tbl_par.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub computesalary()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandType = CommandType.StoredProcedure
        sqlcommand.CommandText = "test"
        Dim p1 As New MySqlParameter
        p1.ParameterName = "@empcode"
        p1.Value = txtempcode.Text
        sqlcommand.Parameters.Add(p1)
        sqlcommand.Connection = SQLConnection
        Dim dt As New DataTable
        dt.Load(sqlcommand.ExecuteReader)
        list.GridControl1.DataSource = dt
    End Sub

    Dim list As New Lists

    Private Sub ClosePayroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        loaddata()
    End Sub

    Private Sub radiochoose_CheckedChanged(sender As Object, e As EventArgs) Handles radiochoose.CheckedChanged
        If radiochoose.Checked = True Then
            txtname.Enabled = True
            txtempcode.Enabled = False
        Else
            txtname.Enabled = False
            txtempcode.Enabled = False
        End If
    End Sub

    Private Sub txtname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtname.SelectedIndexChanged
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            If txtname.SelectedItem Is tbl_par.Rows(index).Item(0).ToString Then
                txtempcode.Text = tbl_par.Rows(index).Item(1).ToString
            End If
        Next
    End Sub

    Dim lis As New Lists
    Dim basicsalary As Decimal
    Dim pphutang As Decimal

    ''find rangedate between start and end holiday
    'Dim dates1 As MySqlCommand = SQLConnection.CreateCommand
    'dates1.CommandText = "select startdate from db_holiday where startdate between @date1 and @date2"
    'dates1.Parameters.AddWithValue("@date1", date1.Value.Date)
    'dates1.Parameters.AddWithValue("@date2", date2.Value)
    'Dim realdate1 As String = CStr(dates1.ExecuteScalar)

    'Dim dates2 As MySqlCommand = SQLConnection.CreateCommand
    'dates2.CommandText = "select enddate from db_holiday where startdate between @date1 and @date2"
    'dates2.Parameters.AddWithValue("@date1", date1.Value.Date)
    'dates2.Parameters.AddWithValue("@date2", date2.Value)
    'Dim realdate2 As String = CStr(dates2.ExecuteScalar)

    'Dim sqlcommand0 As New MySqlCommand
    'sqlcommand0.CommandType = CommandType.StoredProcedure
    'sqlcommand0.CommandText = "rangedate"
    'Dim p1, p2 As New MySqlParameter
    'p1.ParameterName = "@date1"
    'p2.ParameterName = "@date2"
    'p1.Value = realdate1
    'p2.Value = realdate2
    'sqlcommand0.Parameters.Add(p1)
    'sqlcommand0.Parameters.Add(p2)
    'sqlcommand0.Connection = SQLConnection
    'Dim rangedate As String = CStr(sqlcommand0.ExecuteScalar)
    ''end

    'Dim aabsen As MySqlCommand = SQLConnection.CreateCommand
    'aabsen.CommandText = "select startdate from db_attrec where startdate between @date1 and @date2 and reason = 'Sakit' or reason = 'Izin'"
    'aabsen.Parameters.AddWithValue("@date1", date1.Value.Date)
    'aabsen.Parameters.AddWithValue("@date2", date2.Value)
    'Dim reala As String = CStr(aabsen.ExecuteScalar)

    'Dim babsen As MySqlCommand = SQLConnection.CreateCommand
    'babsen.CommandText = "Select enddate from db_attrec where enddate between @date1 And @date2 and reason = 'Sakit' or reason = 'Izin'"
    'babsen.Parameters.AddWithValue("@date1", date1.Value.Date)
    'babsen.Parameters.AddWithValue("@date2", date2.Value)
    'Dim realb As String = CStr(babsen.ExecuteScalar)

    'Dim sqlcommand1 As New MySqlCommand
    'sqlcommand1.CommandType = CommandType.StoredProcedure
    'sqlcommand1.CommandText = "rangedate"
    'Dim d1, d2 As New MySqlParameter
    'd1.ParameterName = "@date1"
    'd2.ParameterName = "@date2"
    'd1.Value = date1.Value
    'd2.Value = date2.Value
    'sqlcommand1.Parameters.Add(d1)
    'sqlcommand1.Parameters.Add(d2)
    'sqlcommand1.Connection = SQLConnection

    'Dim days As New DataSet
    'Dim daysAdp As New MySqlDataAdapter(sqlcommand1)

    'daysAdp.Fill(days)

    'Dim hslabsen As String = CStr(sqlcommand1.ExecuteScalar)
    'end

    ''find HK dates 
    'Dim jlhhk As MySqlCommand = SQLConnection.CreateCommand
    'jlhhk.CommandText = "Select tanggal from db_absensi where employeecode = @ec And tanggal between @date1 And @date2"
    'jlhhk.Parameters.AddWithValue("@ec", emp)
    'jlhhk.Parameters.AddWithValue("@date1", date1.Value.Date)
    'jlhhk.Parameters.AddWithValue("@date2", date2.Value)
    'Dim tggl As String = CStr(jlhhk.ExecuteScalar)
    ''end

    'Dim dt1 As Date
    'Dim dt2 As Date
    'Dim dt3 As TimeSpan
    'Dim diff As Double
    'Dim hasil As String

    'Sub days()
    '    Dim t As TimeSpan = DateTimePicker2.Value.Date - DateTimePicker1.Value.Date
    '    Dim tmp As String
    '    tmp = t.Days.ToString
    '    Dim hasil As Integer
    '    hasil = CInt(tmp)
    '    Dim hasil2 As Integer = hasil + 1
    '    TextEdit40.Text = hasil2.ToString
    'End Sub

    Sub latesdeduction(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select a.officelocation, count(minute(b.jamMulai)) from db_absensi b, db_pegawai a where b.tanggal between @date1 and @date2 and minute(b.jammulai) > 10 and a.employeecode = @emp and hour(b.jammulai) = 8 and a.employeecode = b.employeecode"
        query.Parameters.AddWithValue("@date1", date1.Value.Date)
        query.Parameters.AddWithValue("@date2", date2.Value.Date)
        query.Parameters.AddWithValue("@emp", emp)
        Dim tab As New DataTable
        For index As Integer = 0 To tab.Rows.Count - 1
            MsgBox(tab.Rows(index).Item(2).ToString())
            tab.Rows(index).Item(1).ToString()
        Next
    End Sub

    Sub computecomponent(emp As String, tipe As String, office As String)
        'Try
        'find employeetype
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select employeetype from db_pegawai where employeecode = '" & emp & "'"
        Dim emptype As String = CStr(query.ExecuteScalar)
        ' end

        'find hk
        query.CommandText = "select count(*) from db_absensi where tanggal between @date1 and @date2 and employeecode = '" & emp & "'"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@date1", date1.Value.Date)
        query.Parameters.AddWithValue("@date2", date2.Value.Date)
        Dim hk As Integer = CInt(query.ExecuteScalar)
        'end

        query.CommandText = "select basicrate, allowance, incentives, mealrate, transport from db_payrolldata where employeecode ='" & emp & "'"
        Dim table As New DataTable
        table.Load(query.ExecuteReader)
        For index As Integer = 0 To table.Rows.Count - 1
            Dim gapok As Integer = CInt(Decrypt(CType(table.Rows(index).Item(0), String)))

            query.CommandText = "select count(minute(b.jamMulai)) from db_absensi b, db_pegawai a where b.tanggal between @date1 and @date2 and minute(b.jammulai) > 10 and a.employeecode = @emp and hour(b.jammulai) = 8 and a.employeecode = b.employeecode"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@date1", date1.Value.Date)
            query.Parameters.AddWithValue("@date2", date2.Value.Date)
            query.Parameters.AddWithValue("@emp", emp)
            Dim x As Integer = CInt(query.ExecuteScalar)

            If LCase(tipe) = "bulanan" Then
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Lates', @total"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@total", "Total Telat = " & x & " Potongan = " & x * 7000 & "")
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Gapok', @basicrate"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "select biayajabatan from db_setpayroll"
                Dim bj As Integer = CInt(query.ExecuteScalar)
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Biaya Jabatan', @biayajabatan"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@biayajabatan", gapok * bj / 100)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Kompetensi', @basicrate"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@basicrate", gapok * 0.3 / 100)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Transportasi', @transport"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@transport", Decrypt(CType(table.Rows(index).Item(4), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Makan', @makan"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@makan", Decrypt(CType(table.Rows(index).Item(3), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'On Call', @oncall"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@oncall", "0")
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Kost', @kost"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Bulanan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@kost", "0")
                query.ExecuteNonQuery()

                query.CommandText = "select as1 from db_addition where employeecode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@emp", emp)
                Dim quers As String = CStr(query.ExecuteScalar)
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, @types2, calc_addsubx(amount, as1, @basicrate) from db_addition where EmployeeCode = @emp and period between @d1 and until"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@types2", quers)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

            ElseIf LCase(tipe) = "borongan" Then
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Lates', @total"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@total", "Total Telat = " & x & " Potongan = " & x * 5000 & "")
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Total Borongan',  sum(quantity) from db_borongan where employeecode = '" & emp & "'"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Total Gapok',  sum(calc_borongan(b.Quantity, a.periodic, b.Target, b.Target1, b.Target2, b.Target3, b.Amount, b.Amount1, b.Amount2, b.Amount3, b.Operators)) from db_calcbor a, db_borongan b where b.Employeecode = @emp and b.tanggal between @d1 and @d2 and a.tasks = b.tasks"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Premi', '0'"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Overtime', sum(calc_overtime(@basicrate, a.OvertimeHours, a.isHoliday, @offloc)) from db_absensi a  where a.EmployeeCode = @emp and a.OvertimeHours > 0 and a.Tanggal between @d1 and @d2"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.Parameters.AddWithValue("@offloc", office)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Tunjangan Kost', '0'"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) Select @Date, @emp, @types, 'Loans', @basicrate - paymentpermonth from db_loan b where EmployeeCode = @emp and frommonths between @d1 and @d2"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "select as1 from db_addition where employeecode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@emp", emp)
                Dim quers As String = CStr(query.ExecuteScalar)
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, @types2, calc_addsubx(amount, as1, @basicrate) from db_addition where EmployeeCode = @emp and period between @d1 and until"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Borongan")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@types2", quers)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

            ElseIf LCase(tipe) = "harian" Then
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Lates', @total"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@total", "Total Telat = " & x & " Potongan = " & x * 1000 & "")
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Gapok', @basicrate"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)) & " " & "Hari Kerja : " & hk)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Uang Shift', @shift"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@shift", "0")
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Overtime', sum(calc_overtime(@basicrate, a.OvertimeHours, a.isHoliday, @offloc)) from db_absensi a  where a.EmployeeCode = @emp and a.OvertimeHours > 0 and a.Tanggal between @d1 and @d2"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.Parameters.AddWithValue("@offloc", office)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Makan', @makan"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@makan", Decrypt(CType(table.Rows(index).Item(3), String)))
                query.ExecuteNonQuery()

                query.CommandText = "select biayajabatan from db_setpayroll"
                Dim bj As Integer = CInt(query.ExecuteScalar)
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Biaya Jabatan', @biayajabatan"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@biayajabatan", gapok * bj / 100)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Uang Kerajinan', @uk"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@uk", "0")
                query.ExecuteNonQuery()

                query.CommandText = "select as1 from db_addition where employeecode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@emp", emp)
                Dim quers As String = CStr(query.ExecuteScalar)
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, @types2, calc_addsubx(amount, as1, @basicrate) from db_addition where EmployeeCode = @emp and period between @d1 and until"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@types2", quers)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) Select @Date, @emp, @types, 'Loans', @basicrate - paymentpermonth from db_loan b where EmployeeCode = @emp and frommonths between @d1 and @d2"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Total Gaji Harian', @total"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", "Harian")
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@total", gapok / 30 * hk)
                query.ExecuteNonQuery()
            End If
        Next
        Dim sqlCommand As New MySqlCommand
        sqlCommand.CommandText = "Select distinct employee_code, salary_component, salary_value, employee_type from payroll2"
        sqlCommand.Connection = SQLConnection
        Dim dt As New DataTable
        dt.Load(sqlCommand.ExecuteReader)
        lis.GridControl1.DataSource = dt
    End Sub

    Sub computingsalary(emp As String, office As String, tipe As String)
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select employeetype from db_pegawai where EmployeeCode = '" & emp & "'"
            Dim emptype As String = CStr(query.ExecuteScalar)

            Dim q As MySqlCommand = SQLConnection.CreateCommand()
            q.CommandText = "select * from db_employeetype where emptype = '" & emptype & "'"

            query.CommandText = "select count(*) from db_absensi where Tanggal between @date1 and @date2 and EmployeeCode = '" & emp & "'"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@date1", date1.Value.Date)
            query.Parameters.AddWithValue("@date2", date2.Value)
            Dim hk As Integer = CInt(query.ExecuteScalar)

            query.CommandText = "select count(*) from db_absensi where Tanggal between @date1 and @date2 and EmployeeCode = '" & emp & "' and isHoliday = 1"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@date1", date1.Value.Date)
            query.Parameters.AddWithValue("@date2", date2.Value)
            Dim hkholiday As Integer = CInt(query.ExecuteScalar)

            query.CommandText = "select basicrate, allowance, incentives, mealrate, transport from db_payrolldata where employeecode ='" & emp & "'"
            Dim table As New DataTable
            table.Load(query.ExecuteReader)
            For index As Integer = 0 To table.Rows.Count - 1
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Gapok', @basicrate" ' where EmployeeCode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Overtime', calc_overtime(@basicrate, a.OvertimeHours, a.isHoliday, @offloc) from db_absensi a  where a.EmployeeCode = @emp and a.OvertimeHours > 0 and a.Tanggal between @d1 and @d2"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.Parameters.AddWithValue("@offloc", office)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "select as1 from db_addition where employeecode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@emp", emp)
                Dim quers As String = CStr(query.ExecuteScalar)

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, @types2, calc_addsubx(amount, as1, @basicrate) from db_addition where EmployeeCode = @emp and period between @d1 and until"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@types2", quers)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "select amount from db_warning where employeecode = '" & emp & "'"
                Dim amount As String = Decrypt(CStr(query.ExecuteScalar))
                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Warning Fine', @basicrate - @amount from db_warning  where employeecode = @emp And month(paymentdate) = month(@date)" ' year(tgl) = year(curdate())
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@Date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.Parameters.AddWithValue("@amount", amount)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) Select @Date, @emp, @types, 'Loans', @basicrate - paymentpermonth from db_loan b where EmployeeCode = @emp and frommonths between @d1 and @d2"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.Parameters.AddWithValue("@basicrate", Decrypt(CType(table.Rows(index).Item(0), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Borong', calc_borongan(Quantity, Target, Target1, Target2, Target3, Amount, Amount1, Amount2, Amount3, Operators) from db_borongan where Employeecode = @emp and tanggal between @d1 and @d2"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@d1", date1.Value.Date)
                query.Parameters.AddWithValue("@d2", date2.Value.Date)
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Tunjangan', @allowance" 'from db_payrolldata where EmployeeCode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@allowance", Decrypt(CType(table.Rows(index).Item(1), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Uang Makan', @mealrate" 'from db_payrolldata where EmployeeCode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@mealrate", Decrypt(CType(table.Rows(index).Item(3), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Transportasi', @transport" 'from db_payrolldata where EmployeeCode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.Parameters.AddWithValue("@transport", Decrypt(CType(table.Rows(index).Item(4), String)))
                query.ExecuteNonQuery()

                query.CommandText = "insert into payroll2(pay_date, employee_code, employee_type, salary_component, salary_value) select @date, @emp, @types, 'Others', amount from db_addition where EmployeeCode = @emp"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@types", tipe)
                query.Parameters.AddWithValue("@date", DateTimePicker1.Value.Date)
                query.Parameters.AddWithValue("@emp", emp)
                query.ExecuteNonQuery()

            Next
            Dim sqlCommand As New MySqlCommand
            sqlCommand.CommandText = "Select distinct employee_code, salary_component, salary_value, employee_type from payroll2"
            sqlCommand.Connection = SQLConnection
            Dim dt As New DataTable
            dt.Load(sqlCommand.ExecuteReader)
            lis.GridControl1.DataSource = dt
        Catch ex As Exception
            ';MsgBox(ex.Message)
        End Try
    End Sub

    Sub checkloan()
        Try
            Dim dtb As Date
            DateTimePicker1.Format = DateTimePickerFormat.Custom
            DateTimePicker1.CustomFormat = "yyyy-MM-dd"
            dtb = DateTimePicker1.Value
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select monthx from db_loanlist where monthx = @date1"
            query.Parameters.AddWithValue("@date1", dtb.ToString("yyyy-MM-dd"))
            Dim quer As Date = CDate(query.ExecuteScalar)
            If CDate(dtb.ToString("yyyy-MM-dd")) = quer.Date Then
                Dim cmd As MySqlCommand = SQLConnection.CreateCommand
                cmd.CommandText = "update db_loanlist set Realisasi = 'PAID' where monthx = '" & dtb.ToString("yyyy-MM-dd") & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Loan subtracted for this month")
            End If
        Catch ex As Exception
            MsgBox("loan " & ex.Message)
        End Try
    End Sub

    Private Sub processpayroll()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select officelocation from db_pegawai where employeecode = '" & txtempcode.Text & "'"
        Dim ofloc As String = CStr(query.ExecuteScalar)
        query.CommandText = "select employeetype from db_pegawai where employeecode ='" & txtempcode.Text & "'"
        Dim tipe As String = CStr(query.ExecuteScalar)

        If payrollcheck.Checked = True Then
            If radiochoose.Checked = True Then
                computecomponent(txtempcode.Text, tipe, ofloc)
            Else
                Dim cmd As MySqlCommand = SQLConnection.CreateCommand()
                cmd.CommandText = "select a.employeecode, b.officelocation, b.employeetype from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode"
                Dim adp As New MySqlDataAdapter(cmd)
                Dim ds As New DataSet
                adp.Fill(ds)
                For Each tbl As DataTable In ds.Tables
                    For Each row As DataRow In tbl.Rows
                        computecomponent(CType(row.Item(0), String), CType(row.Item(2), String), CType(row.Item(1), String))
                    Next
                Next
            End If
        ElseIf thrcheck.Checked = True Then
        ElseIf bonuscheck.Checked = True Then
            loaddata()
        ElseIf checkovertime.Checked = True Then
        ElseIf payrollcheck.Checked = True And thrcheck.Checked = True And bonuscheck.Checked = True Then
            loaddata()
        ElseIf payrollcheck.Checked = True And thrcheck.Checked = True And bonuscheck.Checked = False Then
        ElseIf payrollcheck.Checked = True And thrcheck.Checked = False And bonuscheck.Checked = True Then
            loaddata()
        ElseIf payrollcheck.Checked = False And thrcheck.Checked = True And bonuscheck.Checked = True Then
            loaddata()
        ElseIf payrollcheck.Checked = True And thrcheck.Checked = False And bonuscheck.Checked = True Then
        End If
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select pay_date from payroll2"
        Dim pay As Date = CDate(query.ExecuteScalar)
        If DateTimePicker1.Value.Date = pay Then
            MsgBox("The payment date have already processed", MsgBoxStyle.Information)
        Else

            Timer1.Enabled = True
            If lis Is Nothing OrElse lis.IsDisposed OrElse lis.MinimizeBox Then
                lis.Close()
                lis = New Lists
            End If
            Timer1.Start()
        End If
    End Sub

    Private Sub radioloadall_CheckedChanged(sender As Object, e As EventArgs) Handles radioloadall.CheckedChanged
        If radioloadall.Checked = True Then
            txtname.Text = ""
            txtempcode.Text = ""
        End If
    End Sub

    Friend Delegate Sub SetDataSourceDelegate(table As DataTable)

    Private Sub setDataSource(table As DataTable)
        If InvokeRequired Then
            Invoke(New SetDataSourceDelegate(AddressOf setDataSource), table)
        Else
            lis.GridControl1.DataSource = table
            ProgressBar1.Visible = False
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select pay_date from payroll2"
        Dim pay As Date = CDate(query.ExecuteScalar)
        If radiochoose.Checked = True And txtname.Text = "" Then
            Timer1.Enabled = False
            MsgBox("Please insert the employee name you wanted to display", MsgBoxStyle.Exclamation)
        ElseIf date1.Value.Date > date2.Value.Date Then
            MsgBox("Wrong Dates, please do check again", MsgBoxStyle.Information)
        Else
            ProgressBar1.Visible = True
            If ProgressBar1.Value < 100 Then
                ProgressBar1.Value += 10
            ElseIf ProgressBar1.Value = 100 Then
                Timer1.Stop()
            End If
            If ProgressBar1.Value = 40 Then
                processpayroll()
            ElseIf ProgressBar1.Value = 100 Then
                lis.Show()
                Close()
            End If
        End If
    End Sub
End Class