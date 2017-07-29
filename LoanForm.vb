Imports System.IO
Imports System.Globalization
Imports System.Windows.Forms.DataVisualization.Charting
Imports DevExpress
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views
Imports DevExpress.XtraGrid.Views.Grid

Public Class Payments
    Dim tbl_par As New DataTable
    Sub loaddata()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "select fullname, employeecode from db_pegawai a where a.employeecode not in (select b.employeecode from db_payrolldata b )"
        sqlcommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par)
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtname1.Properties.Items.Add(tbl_par.Rows(index).Item(0).ToString())
        Next
    End Sub

    Public ReadOnly Property DisplayFormat As Utils.FormatInfo

    Sub viewthousand()
        GridControl2.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select kim3 from db_user where username = '" & Label7.Text & "'"
            Dim quer1 As String = CStr(query.ExecuteScalar)
            query.CommandText = "select kim4 from db_user where username= '" & Label7.Text & "'"
            Dim quer22 As String = CStr(query.ExecuteScalar)
            query.CommandText = "select pantailabu from db_user where username = '" & Label7.Text & "'"
            Dim quer3 As String = CStr(query.ExecuteScalar)
            If quer1 = "True" And quer22 = "False" And quer3 = "False" Then
                sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
            ElseIf quer1 = "True" And quer22 = "True" And quer3 = "False" Then
                sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
            ElseIf quer1 = "True" And quer22 = "False" And quer3 = "True" Then
                sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
            ElseIf quer1 = "False" And quer22 = "True" And quer3 = "True" Then
                sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
            ElseIf quer1 = "False" And quer22 = "False" And quer3 = "True" Then
                sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
            ElseIf quer1 = "False" And quer22 = "True" And quer3 = "False" Then
                sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
            Else
                sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode"
            End If
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl2.DataSource = table
            GridControl2.UseEmbeddedNavigator = True
            Dim navigator As ControlNavigator = GridControl2.EmbeddedNavigator
            navigator.Buttons.Append.Visible = False
            navigator.Buttons.Remove.Visible = False
        Catch ex As Exception
            MsgBox("Filter OfficeLocation  " & ex.Message)
        End Try
    End Sub

    Sub viewthousand1()
        Dim newtab As New DataTable
        GridControl2.RefreshDataSource()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = ''"
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(newtab)
        GridControl2.DataSource = newtab
        GridControl2.UseEmbeddedNavigator = True
        Dim navigator As ControlNavigator = GridControl2.EmbeddedNavigator
        navigator.Buttons.Append.Visible = False
        navigator.Buttons.Remove.Visible = False
    End Sub

    Dim tbl_par3 As New DataTable
    Dim log As Login

    Sub loanlists()
        tbl_par3.Columns.Clear()
        txtloanname.Properties.Items.Clear()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "SELECT distinct FullName, EmployeeCode from db_loan where status = 'Approved' and issettle = '0'"
        sqlcommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par3)
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
            txtloanname.Properties.Items.Add(tbl_par3.Rows(index).Item(0).ToString())
            txtnameloan.Properties.Items.Add(tbl_par3.Rows(index).Item(1).ToString())
        Next
    End Sub

    Dim tbl_par2 As New DataTable

    Sub loaddata1()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "SELECT FullName, EmployeeCode, StatusWajibPajak, MemilikiNpwp, BasicRate as BasicSalary, Allowance, Incentives, MealRate, Transport, JaminanKesehatan, Bpjs, JaminanKecelakaanKerja, JaminanKematian, JaminanHariTua, IuranPensiun, BiayaJabatan, Rapel, Loan From db_payrolldata"
        sqlcommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par2)
        For index As Integer = 0 To tbl_par2.Rows.Count - 1
        Next
    End Sub

    'Sub selectname()
    '    GridControl8.RefreshDataSource()
    '    Dim table As New DataTable
    '    Dim sqlCommand As New MySqlCommand
    '    Try
    '        sqlCommand.CommandText = "select a.FullName, a.EmployeeCode, a.BasicRate as BasicSalary, b.Gender, b.Status, b.IdNumber, b.Religion from db_payrolldata a, db_pegawai b where a.EMployeeCode = b.EmployeeCode"
    '        sqlCommand.Connection = SQLConnection
    '        Dim tbl_par As New DataTable
    '        Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
    '        Dim cb As New MySqlCommandBuilder(adapter)
    '        adapter.Fill(table)
    '        GridControl8.DataSource = table
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Public Sub Insertpart()
        Dim sqlCommand As MySqlCommand = SQLConnection.CreateCommand
        Try
            sqlCommand.CommandText = "select user from db_temp"
            Dim sql As String = CType(sqlCommand.ExecuteScalar, String)
            sqlCommand.CommandText = "INSERT INTO db_salarychange " +
                                "(FullName, EmployeeCode, StatusWajibPajak, MemilikiNpwp, BasicRate, Allowance, Incentives, MealRate, Transport, JaminanKesehatan, Bpjs, JaminanKecelakaanKerja, JaminanKematian, JaminanHariTua, IuranPensiun, BiayaJabatan, Rapel, Loan, ChangeDate, ChangeBy, Golongan) " +
                                "values (@FullName, @EmployeeCode, @StatusWajibPajak, @MemilikiNpwp, @BasicRate, @Allowance, @Incentives, @MealRate, @Transport, @JaminanKesehatan, @Bpjs, @JaminanKecelakaanKerja, @JaminanKematian, @JaminanHariTua, @IuranPensiun, @BiayaJabatan, @Rapel, @Loan, @ChangeDate, @ChangeBy, @golongan)"
            sqlCommand.Parameters.AddWithValue("@FullName", txtname1.Text)
            sqlCommand.Parameters.AddWithValue("@EmployeeCode", txtempcode1.Text)
            sqlCommand.Parameters.AddWithValue("@StatusWajibPajak", txtwajibpajak.Text)
            sqlCommand.Parameters.AddWithValue("@MemilikiNpwp", txtnpwp.Text)
            sqlCommand.Parameters.AddWithValue("@BasicRate", Encryptio(TextBox1.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@Allowance", Encryptio(TextBox2.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@Incentives", Encryptio(TextBox3.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@MealRate", Encryptio(TextBox4.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@Transport", Encryptio(TextBox5.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@JaminanKesehatan", cjk1.Checked)
            sqlCommand.Parameters.AddWithValue("@Bpjs", cbpjs1.Checked)
            sqlCommand.Parameters.AddWithValue("@JaminanKecelakaanKerja", cjkk1.Checked)
            sqlCommand.Parameters.AddWithValue("@JaminanKematian", cjamkem1.Checked)
            sqlCommand.Parameters.AddWithValue("@JaminanHariTua", cjht1.Checked)
            sqlCommand.Parameters.AddWithValue("@IuranPensiun", ciupe1.Checked)
            sqlCommand.Parameters.AddWithValue("@BiayaJabatan", cbj1.Checked)
            sqlCommand.Parameters.AddWithValue("@Rapel", crapel1.Checked)
            sqlCommand.Parameters.AddWithValue("@Loan", cloan1.Checked)
            sqlCommand.Parameters.AddWithValue("@ChangeDate", Date.Now)
            sqlCommand.Parameters.AddWithValue("@ChangeBy", Label7.Text)
            sqlCommand.Parameters.AddWithValue("@golongan", txtgol1.Text)
            sqlCommand.ExecuteNonQuery()
            MsgBox("Data Succesfully Added", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatepart()
        Dim sqlCommand As MySqlCommand = SQLConnection.CreateCommand
        Try
            Dim a As MySqlCommand = SQLConnection.CreateCommand
            a.CommandText = "select JaminanKesehatan from db_payrolldata where employeecode = '" & txtempcode2.Text & "'"

            Dim use As MySqlCommand = SQLConnection.CreateCommand
            use.CommandText = "select user from db_temp"
            Dim user As String = CStr(use.ExecuteScalar)

            sqlCommand.CommandText = "INSERT INTO db_salarychange " +
                                "(FullName, EmployeeCode, StatusWajibPajak, MemilikiNpwp, BasicRate, Allowance, Incentives, MealRate, Transport, JaminanKesehatan, Bpjs, JaminanKecelakaanKerja, JaminanKematian, JaminanHariTua, IuranPensiun, BiayaJabatan, Rapel, Loan, ChangeDate, ChangeBy) " +
                                "values (@FullName, @EmployeeCode, @StatusWajibPajak, @MemilikiNpwp, @BasicRate, @Allowance, @Incentives, @MealRate, @Transport, @JaminanKesehatan, @Bpjs, @JaminanKecelakaanKerja, @JaminanKematian, @JaminanHariTua, @IuranPensiun, @BiayaJabatan, @Rapel, @Loan, @ChangeDate, @ChangeBy)"
            sqlCommand.Parameters.AddWithValue("@FullName", txtname2.Text)
            sqlCommand.Parameters.AddWithValue("@EmployeeCode", txtempcode2.Text)
            sqlCommand.Parameters.AddWithValue("@StatusWajibPajak", txtwp1.Text)
            sqlCommand.Parameters.AddWithValue("@MemilikiNpwp", txtnpwp1.Text)
            sqlCommand.Parameters.AddWithValue("@BasicRate", Encryptio(TextBox6.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@Allowance", Encryptio(TextBox7.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@Incentives", Encryptio(TextBox8.Text))
            sqlCommand.Parameters.AddWithValue("@MealRate", Encryptio(TextBox9.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@Transport", Encryptio(TextBox10.Text.Trim))
            sqlCommand.Parameters.AddWithValue("@JaminanKesehatan", cjk1.Checked)
            sqlCommand.Parameters.AddWithValue("@Bpjs", cbpjs1.Checked)
            sqlCommand.Parameters.AddWithValue("@JaminanKecelakaanKerja", cjkk1.Checked)
            sqlCommand.Parameters.AddWithValue("@JaminanKematian", cjamkem1.Checked)
            sqlCommand.Parameters.AddWithValue("@JaminanHariTua", cjht1.Checked)
            sqlCommand.Parameters.AddWithValue("@IuranPensiun", ciupe1.Checked)
            sqlCommand.Parameters.AddWithValue("@BiayaJabatan", cbj1.Checked)
            sqlCommand.Parameters.AddWithValue("@Rapel", crapel1.Checked)
            sqlCommand.Parameters.AddWithValue("@Loan", cloan1.Checked)
            sqlCommand.Parameters.AddWithValue("@ChangeDate", Date.Now)
            sqlCommand.Parameters.AddWithValue("@ChangeBy", Label7.Text)
            sqlCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatechange()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_payrolldata SET" +
                    " FullName = @FullName" +
                    ", EmployeeCode = @EmployeeCode" +
                    ", StatusWajibPajak = @StatusWajibPajak" +
                    ", MemilikiNpwp = @MemilikiNpwp" +
                    ", BasicRate = @BasicRate" +
                    ", Allowance = @Allowance" +
                    ", Incentives = @Incentives" +
                    ", MealRate = @MealRate" +
                    ", Transport = @Transport" +
                    ", JaminanKesehatan = @JaminanKesehatan" +
                    ", Bpjs = @Bpjs" +
                    ", JaminanKecelakaanKerja = @JaminanKecelakaanKerja" +
                    ", JaminanKematian = @JaminanKematian" +
                    ", JaminanHariTua = @JaminanHariTua" +
                    ", IuranPensiun = @IuranPensiun" +
                    ", BiayaJabatan = @BiayaJabatan" +
                    ", Rapel = @Rapel" +
                    ", Loan = @Loan" +
                    ", Golongan = @Golongan" +
                    ", MealTidakTetap = @MealTidakTetap" +
                    ", TransportTidakTetap = @TransportTidakTetap" +
                    ", Deductions = @Deductions" +
                    " WHERE EmployeeCode = @EmployeeCode"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@FullName", txtname2.Text)
            sqlcommand.Parameters.AddWithValue("@EmployeeCode", txtempcode2.Text)
            sqlcommand.Parameters.AddWithValue("@StatusWajibPajak", txtwp1.Text)
            sqlcommand.Parameters.AddWithValue("@MemilikiNpwp", txtnpwp1.Text)
            sqlcommand.Parameters.AddWithValue("@BasicRate", Encryptio(txtbasicrate1.Text.Trim))
            sqlcommand.Parameters.AddWithValue("@Allowance", Encryptio(txtallowance1.Text.Trim))
            sqlcommand.Parameters.AddWithValue("@Incentives", Encryptio(txtincentives1.Text.Trim))
            sqlcommand.Parameters.AddWithValue("@MealRate", Encryptio(txtmealrate1.Text.Trim))
            sqlcommand.Parameters.AddWithValue("@Transport", Encryptio(txttransport1.Text.Trim))
            sqlcommand.Parameters.AddWithValue("@JaminanKesehatan", cjk1.Checked)
            sqlcommand.Parameters.AddWithValue("@Bpjs", cbpjs1.Checked)
            sqlcommand.Parameters.AddWithValue("@JaminanKecelakaanKerja", cjkk1.Checked)
            sqlcommand.Parameters.AddWithValue("@JaminanKematian", cjamkem1.Checked)
            sqlcommand.Parameters.AddWithValue("@JaminanHariTua", cjht1.Checked)
            sqlcommand.Parameters.AddWithValue("@IuranPensiun", ciupe1.Checked)
            sqlcommand.Parameters.AddWithValue("@BiayaJabatan", cbj1.Checked)
            sqlcommand.Parameters.AddWithValue("@Rapel", crapel1.Checked)
            sqlcommand.Parameters.AddWithValue("@Loan", cloan1.Checked)
            sqlcommand.Parameters.AddWithValue("@Golongan", txtgol1.Text)
            sqlcommand.Parameters.AddWithValue("@MealTidakTetap", CheckEdit4.Checked)
            sqlcommand.Parameters.AddWithValue("@TransportTidakTetap", CheckEdit5.Checked)
            sqlcommand.Parameters.AddWithValue("@Deductions", TextEdit4.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MessageBox.Show("Data Successfully Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Sub insertlists()
    'Dim lesser, greater As Date
    'If txtmonth.Value < txtcompletedon1.Value Then
    '    lesser = txtmonth.Value
    '    greater = txtcompletedon1.Value
    'Else
    '    lesser = txtcompletedon1.Value
    '    greater = txtmonth.Value
    'End If
    'While lesser <= greater
    '    Dim nilai As String = ""
    '    If lesser.Month = 1 Then
    '        nilai = "January"
    '    ElseIf lesser.Month = 2 Then
    '        nilai = "February"
    '    ElseIf lesser.Month = 3 Then
    '        nilai = "March"
    '    ElseIf lesser.Month = 4 Then
    '        nilai = "April"
    '    ElseIf lesser.Month = 5 Then
    '        nilai = "May"
    '    ElseIf lesser.Month = 6 Then
    '        nilai = "June"
    '    ElseIf lesser.Month = 7 Then
    '        nilai = "July"
    '    ElseIf lesser.Month = 8 Then
    '        nilai = "August"
    '    ElseIf lesser.Month = 9 Then
    '        nilai = "September"
    '    ElseIf lesser.Month = 10 Then
    '        nilai = "October"
    '    ElseIf lesser.Month = 11 Then
    '        nilai = "November"
    '    ElseIf lesser.Month = 12 Then
    '        nilai = "December"
    '    End If
    ' Try       

    Public Sub insertlists()
        Dim d1 As Date = txtmonth.Value.Date
        Dim d2 As Date = txtcompletedon1.Value.Date
        For j As Integer = 1 To DateDiff("M", d1, d2)
            d1 = d1.AddMonths(1)
            Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
            sqlcommand.CommandText = "insert into db_loanlist " +
                                        "(FullName, EmployeeCode, AmountOfLoan, Monthx, Realisasi, MemoNo)" +
                                        " Values (@FullName, @EmployeeCode, @AmountOfLoan, @Monthx, @Realisasi, @Memono)"
            sqlcommand.Parameters.AddWithValue("@FullName", txtname.Text)
            sqlcommand.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
            sqlcommand.Parameters.AddWithValue("@AmountOfLoan", txtloan.Text)
            sqlcommand.Parameters.AddWithValue("@Monthx", d1.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@Realisasi", "")
            sqlcommand.Parameters.AddWithValue("@memono", TextBox13.Text)
            sqlcommand.ExecuteNonQuery()
        Next
    End Sub

    Dim dt1 As Date
    Dim dt2 As Date
    Dim dt3 As TimeSpan
    Dim diff As Double
    Dim hasil As String

    Public Sub InsertLoan()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select Workdate from db_pegawai where employeecode = @emp"
        query.Parameters.AddWithValue("@emp", txtempcode.Text)
        Dim quer11 As Date = CDate(query.ExecuteScalar)
        dt1 = CDate(quer11.ToShortDateString)
        dt2 = Date.Now
        dt3 = (dt2 - dt1)
        diff = dt3.Days
        hasil = CType(Int(diff / 365), String)
        If CInt(hasil) < 1 Then
            MsgBox("This employee isn't allowed to do a loan due of an insufficient work duration", MsgBoxStyle.Information)
        Else
            Dim dtr, dts, dtk As Date
            txtmonth.Format = DateTimePickerFormat.Custom
            txtdates.Format = DateTimePickerFormat.Custom
            txtdates.CustomFormat = "yyyy-MM-dd"
            dtk = txtdates.Value
            txtcompletedon1.Format = DateTimePickerFormat.Custom
            txtcompletedon1.CustomFormat = "MMMM yyyy"
            dtr = txtcompletedon1.Value
            txtmonth.CustomFormat = "yyyy-MM-dd"
            dts = txtmonth.Value
            Dim quer As Integer
            Try
                Dim cmd = SQLConnection.CreateCommand
                cmd.CommandText = "select count(*) as numRows from db_loan where EmployeeCode = '" & txtempcode.Text & "' and issettle = '0'"
                quer = CInt(cmd.ExecuteScalar)
            Catch ex As Exception
                MsgBox("count " & ex.Message)
            End Try
            If quer = 0 Then
                insertlists()
                Dim sqlcommand As New MySqlCommand
                Dim str_carsql As String
                str_carsql = "INSERT INTO db_loan " +
                                    "(FullName, EmployeeCode, Status, Reason, Dates, AmountOfLoan, Months, SalaryInclude, FromMonths, PaymentPerMonth, CompletedOn, Guaranteedoc, MemoNo) " +
                                    " Values (@FullName, @EmployeeCode, @Status, @Reason, @Dates, @AmountOfLoan, @Months, @SalaryInclude, @FromMonths, @PaymentPerMonth, @CompletedOn, @guaranteedoc, @MemoNo)"
                sqlcommand.Connection = SQLConnection
                sqlcommand.CommandText = str_carsql
                sqlcommand.Parameters.AddWithValue("@FullName", txtname.Text)
                sqlcommand.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
                sqlcommand.Parameters.AddWithValue("@Status", "Requested")
                sqlcommand.Parameters.AddWithValue("@Reason", txtreason.Text)
                sqlcommand.Parameters.AddWithValue("@Dates", dtk.ToString("yyyy-MM-dd"))
                sqlcommand.Parameters.AddWithValue("@AmountOfLoan", txtloan.Text)
                sqlcommand.Parameters.AddWithValue("@Months", txtrangemon.Text)
                sqlcommand.Parameters.AddWithValue("@SalaryInclude", CheckEdit2.Checked)
                sqlcommand.Parameters.AddWithValue("@FromMonths", dts.ToString("yyyy-MM-dd"))
                sqlcommand.Parameters.AddWithValue("@PaymentPerMonth", lcpayment.Text)
                sqlcommand.Parameters.AddWithValue("@CompletedOn", dtr.ToString("yyyy-MM-dd"))
                sqlcommand.Parameters.AddWithValue("@MemoNo", TextBox13.Text)
                Try
                    Dim finfo As New FileInfo(LabelControl6.Text)
                    Dim numBytes As Long = finfo.Length
                    Dim fstream As New FileStream(LabelControl6.Text, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fstream)
                    Dim data As Byte() = br.ReadBytes(CInt(numBytes))
                    br.Close()
                    fstream.Close()
                    sqlcommand.Parameters.AddWithValue("@emp", txtempcode.Text)
                    If Not data Is Nothing Then
                        sqlcommand.Parameters.AddWithValue("@guaranteedoc", data)
                    Else
                        sqlcommand.Parameters.AddWithValue("@guaranteedoc", "")
                    End If
                Catch ex As Exception
                    MsgBox("Guaranteedoc" & ex.Message)
                End Try
                sqlcommand.Connection = SQLConnection
                sqlcommand.ExecuteNonQuery()
                MsgBox("Requested", MsgBoxStyle.Information)
                Close()
            Else
                MsgBox("This Employee still have a loan progress in the lists", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Public Sub UpdateLoan()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_payrolldata SET" +
                                    " Loan = @Loan" +
                                    " Where EmployeeCode = @EmployeeCode"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("EmployeeCode", txtempcode.Text)
            sqlcommand.Parameters.AddWithValue("@Loan", "1")
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateRapel()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "Update db_payrolldata SET" +
                                    " Rapel = @Rapel" +
                                    " Where EmployeeCode = @EmployeeCode"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("EmployeeCode", txtempcode.Text)
            sqlcommand.Parameters.AddWithValue("@Rapel", "1")
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function InsertPayroll() As Boolean
        Dim quer As Integer
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "select count(*) as numRows from db_payrolldata where EmployeeCode = '" & txtempcode1.Text & "'"
            quer = CInt(cmd.ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If quer = 0 Then
            Dim sqlCommand As New MySqlCommand
            Dim str_carSql As String
            Try
                str_carSql = "INSERT INTO db_payrolldata " +
                                "(FullName, EmployeeCode, StatusWajibPajak, MemilikiNpwp, BasicRate, Allowance, Incentives, MealRate, Transport, JaminanKesehatan, Bpjs, JaminanKecelakaanKerja, JaminanKematian, JaminanHariTua, IuranPensiun, BiayaJabatan, Rapel, Loan, Golongan, MealTidakTetap, TransportTidakTetap, Deductions) " +
                                "values (@FullName, @EmployeeCode, @StatusWajibPajak, @MemilikiNpwp, @BasicRate, @Allowance, @Incentives, @MealRate, @Transport, @JaminanKesehatan, @Bpjs, @JaminanKecelakaanKerja, @JaminanKematian, @JaminanHariTua, @IuranPensiun, @BiayaJabatan, @Rapel, @Loan, @gol, @MealTidakTetap, @TransportTidakTetap, @Deductions)"
                sqlCommand.Connection = SQLConnection
                sqlCommand.CommandText = str_carSql
                sqlCommand.Parameters.AddWithValue("@FullName", txtname1.Text)
                sqlCommand.Parameters.AddWithValue("@EmployeeCode", txtempcode1.Text)
                sqlCommand.Parameters.AddWithValue("@StatusWajibPajak", txtwajibpajak.Text)
                sqlCommand.Parameters.AddWithValue("@MemilikiNpwp", txtnpwp.Text)
                sqlCommand.Parameters.AddWithValue("@BasicRate", Encryptio(txtbasicrate.Text.Trim))
                sqlCommand.Parameters.AddWithValue("@Allowance", Encryptio(txtallowance.Text.Trim))
                sqlCommand.Parameters.AddWithValue("@Incentives", Encryptio(txtincentives.Text.Trim))
                sqlCommand.Parameters.AddWithValue("@MealRate", Encryptio(txtmealrate.Text.Trim))
                sqlCommand.Parameters.AddWithValue("@Transport", Encryptio(txttransport.Text.Trim))
                sqlCommand.Parameters.AddWithValue("@JaminanKesehatan", cjk.Checked)
                sqlCommand.Parameters.AddWithValue("@Bpjs", cbpjs.Checked)
                sqlCommand.Parameters.AddWithValue("@JaminanKecelakaanKerja", cjkk.Checked)
                sqlCommand.Parameters.AddWithValue("@JaminanKematian", cjamkem.Checked)
                sqlCommand.Parameters.AddWithValue("@JaminanHariTua", cjht.Checked)
                sqlCommand.Parameters.AddWithValue("@IuranPensiun", ciupe.Checked)
                sqlCommand.Parameters.AddWithValue("@BiayaJabatan", cbj.Checked)
                sqlCommand.Parameters.AddWithValue("@Rapel", crapel.Checked)
                sqlCommand.Parameters.AddWithValue("@Loan", cloan.Checked)
                sqlCommand.Parameters.AddWithValue("@gol", txtgol.Text)
                sqlCommand.Parameters.AddWithValue("@MealTidakTetap", CheckEdit1.Checked)
                sqlCommand.Parameters.AddWithValue("@TransportTidakTetap", CheckEdit3.Checked)
                sqlCommand.Parameters.AddWithValue("@Deductions", Encryptio(TextEdit3.Text.Trim))
                sqlCommand.Connection = SQLConnection
                sqlCommand.ExecuteNonQuery()
                MsgBox("Added!", MsgBoxStyle.Information)
                Return True
            Catch ex As Exception
                Return False
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("The Employee are Already on the lists!", MsgBoxStyle.Critical)
        End If
        Return False
    End Function

    Sub loanmemo()
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
            'cmd.CommandText = "(select cast(right(max(`db_hris`.`db_loan`.`MemoNo`),4) as signed) AS `last_num` from `db_hris`.`db_loan`)"
            cmd.CommandText = "select last_num from loan_memo"
            lastn = DirectCast(cmd.ExecuteScalar(), Integer)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "Select MemoNo FROM db_loan ORDER BY MemoNo DESC LIMIT 1"
            lastcode = DirectCast(cmd.ExecuteScalar(), String)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "Select MID(MemoNo, 9, 1) FROM db_loan where MemoNo = '" & lastcode & "'"
            updmon = DirectCast(cmd.ExecuteScalar(), String)
            If CInt(updmon) <> CInt(mnow) Then
                tmp = 1
            Else
                tmp = lastn + 1
            End If
            Dim actualcode As String = "Loan-" & ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
            TextBox13.Text = actualcode.ToString
        Catch ex2 As Exception
        End Try
    End Sub

    Sub Process()
        Dim dtb, dtr As DateTime
        date1.Format = DateTimePickerFormat.Custom
        date1.CustomFormat = "yyyy-MM-dd"
        dtb = date1.Value
        date2.Format = DateTimePickerFormat.Custom
        date2.CustomFormat = "yyyy-MM-dd"
        dtr = date2.Value
        Dim sqlcommand As New MySqlCommand
        Dim str_carsql As String
        Dim d1 As Date = date1.Value.Date
        d1 = d1.AddDays(-1)
        Dim d2 As Date = date2.Value.Date
        For j As Integer = 1 To DateDiff("d", d1, d2)
            d1 = d1.AddDays(1)
            str_carsql = "INSERT INTO db_holiday " +
                            "(tgl, StartDate, EndDate, TotalDays, Reason) " +
                                    " Values (@tgl, @StartDate, @EndDate, @TotalDays, @Reason)"
            sqlcommand.Connection = SQLConnection
            sqlcommand.CommandText = str_carsql
            sqlcommand.Parameters.Clear()
            sqlcommand.Parameters.AddWithValue("@tgl", d1.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@StartDate", dtb.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@EndDate", dtr.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@TotalDays", txtdays.Text)
            sqlcommand.Parameters.AddWithValue("@Reason", textreason.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
        Next
        updateovertime()
        updatesunday()
        MsgBox("Holiday added", MsgBoxStyle.Information)
    End Sub

    Sub updatesunday()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_holiday set reason = 'Sunday OFF' where dayofweek(tgl) = 1"
            query.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("OFF DAY " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Sub updateovertime()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_absensi set isholiday = 1 where dayofweek(tanggal) = 1 or tanggal in (select tgl from db_holiday)"
            query.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Overtime Part " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Function updateholiday() As Boolean
        Dim dtb, dtr As DateTime
        date1.Format = DateTimePickerFormat.Custom
        date1.CustomFormat = "yyyy-MM-dd"
        dtb = date1.Value
        date2.Format = DateTimePickerFormat.Custom
        date2.CustomFormat = "yyyy-MM-dd"
        dtr = date2.Value
        Dim sqlcommand As New MySqlCommand
        Dim str_carsql As String
        Try
            str_carsql = "Update db_holiday SET" +
                          "StartDate = @StartDate" +
                          "EndDate = @EndDate" +
                          "Reason = @Reason" +
                          "TotalDays = @TotalDays"
            sqlcommand.Connection = SQLConnection
            sqlcommand.CommandText = str_carsql
            sqlcommand.Parameters.AddWithValue("@StartDate", dtb.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@EndDate", dtr.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@Reason", txtreason.Text)
            sqlcommand.Parameters.AddWithValue("@TotalDays", txtdays.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MessageBox.Show("Data Successfully Added!")
            Return True
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub loanpay()
        Try
            Dim a, b, res As Double
            a = Convert.ToDouble(txtloan.Text)
            b = Convert.ToDouble(txtrangemon.Text)
            res = a / b
            lcpayment.Text = res.ToString
        Catch ex As Exception
        End Try
    End Sub

    Sub loaddummy(ByVal table As DataTable)
        GridControl4.DataSource = DBNull.Value
        Dim tbl_baru As New DataTable
        Dim oldtable As New DataTable
        tbl_baru.Columns.Add("FullName")
        tbl_baru.Columns.Add("EmployeeCode")
        tbl_baru.Columns.Add("BasicRate")
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select FullName, EmployeeCode, BasicRate from db_payrolldata"
        'query.CommandText = "select kim3 from db_user where username = '" & Label7.Text & "'"
        'Dim quer1 As String = CStr(query.ExecuteScalar)
        'query.CommandText = "select kim4 from db_user where username= '" & Label7.Text & "'"
        'Dim quer22 As String = CStr(query.ExecuteScalar)
        'query.CommandText = "select pantailabu from db_user where username = '" & Label7.Text & "'"
        'Dim quer3 As String = CStr(query.ExecuteScalar)
        'If quer1 = "True" And quer22 = "False" And quer3 = "False" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
        'ElseIf quer1 = "True" And quer22 = "True" And quer3 = "False" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
        'ElseIf quer1 = "True" And quer22 = "False" And quer3 = "True" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
        'ElseIf quer1 = "False" And quer22 = "True" And quer3 = "True" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
        'ElseIf quer1 = "False" And quer22 = "False" And quer3 = "True" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
        'ElseIf quer1 = "False" And quer22 = "True" And quer3 = "False" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
        'Else
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode"
        'End If
        oldtable.Load(query.ExecuteReader)
        For indeks As Integer = 0 To oldtable.Rows.Count - 1
            tbl_baru.Rows.Add(oldtable.Rows(indeks).Item(0),
            oldtable.Rows(indeks).Item(1),
            Decrypt(CType(oldtable.Rows(indeks).Item(2), String)))
        Next
        GridControl4.DataSource = tbl_baru
        GridView4.BestFitColumns()
        'Dim navigator As ControlNavigator = GridControl2.EmbeddedNavigator
        'navigator.Buttons.Append.Visible = False
        'navigator.Buttons.Remove.Visible = False
    End Sub

    Private Sub loadpayroll()
        Dim customCurrencyInfo As CultureInfo = CultureInfo.CreateSpecificCulture("id-ID")
        customCurrencyInfo.NumberFormat.CurrencyNegativePattern = 8
        GridControl2.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlCommand As New MySqlCommand
        Try
            sqlCommand.CommandText = "Select FullName, EmployeeCode, BasicRate as BasicSalary FROM db_payrolldata"
            sqlCommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl2.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim navigator As ControlNavigator = GridControl2.EmbeddedNavigator
        navigator.Buttons.Append.Visible = False
        navigator.Buttons.Remove.Visible = False
    End Sub

    Private Sub loadloan()
        GridControl7.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select MemoNo, FullName, EmployeeCode, AmountOfLoan, FromMonths, PaymentPerMonth, CompletedOn From db_loan where Status = 'Approved' and issettle = '0'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl7.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridControl7.UseEmbeddedNavigator = True
        Dim navigator As ControlNavigator = GridControl7.EmbeddedNavigator
        navigator.Buttons.Remove.Visible = False
        navigator.Buttons.Append.Visible = False
    End Sub

    Public Overridable Property UseEmbeddedNavigator As Boolean

    Private Sub loadloanname()
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select distinct a.FullName, a.EmployeeCode, a.AmountOfLoan, Date_Format(a.Monthx, '%M-%Y') as Month, a.Realisasi from db_loanlist a, db_loan b where a.FullName Like '%" + txtloanname.Text + "%' and b.status = 'Approved' and b.issettle = '0'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim gridView As GridView = CType(GridControl1.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
   New GridColumnSortInfo(gridView.Columns("AmountOfLoan"), Data.ColumnSortOrder.Descending)}, 1)
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
        Dim navigator As ControlNavigator = GridControl1.EmbeddedNavigator
        navigator.Buttons.Remove.Visible = False
        navigator.Buttons.Append.Visible = False
    End Sub

    Private Sub loadpayroll1()
        GridControl4.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "Select FullName, EmployeeCode, BasicRate as BasicSalary FROM db_payrolldata"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl4.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridControl4.UseEmbeddedNavigator = True
        Dim navigstor As ControlNavigator = GridControl4.EmbeddedNavigator
        navigstor.Buttons.Remove.Visible = False
        navigstor.Buttons.Append.Visible = False
    End Sub

    Sub loaddummy2(ByVal table As DataTable)
        GridControl2.DataSource = DBNull.Value
        Dim tbl_baru As New DataTable
        Dim oldtable As New DataTable
        tbl_baru.Columns.Add("FullName")
        tbl_baru.Columns.Add("EmployeeCode")
        tbl_baru.Columns.Add("BasicRate")
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select FullName, EmployeeCode, BasicRate from db_payrolldata"
        'query.CommandText = "select kim3 from db_user where username = '" & Label7.Text & "'"
        'Dim quer1 As String = CStr(query.ExecuteScalar)
        'query.CommandText = "select kim4 from db_user where username= '" & Label7.Text & "'"
        'Dim quer22 As String = CStr(query.ExecuteScalar)
        'query.CommandText = "select pantailabu from db_user where username = '" & Label7.Text & "'"
        'Dim quer3 As String = CStr(query.ExecuteScalar)
        'If quer1 = "True" And quer22 = "False" And quer3 = "False" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
        'ElseIf quer1 = "True" And quer22 = "True" And quer3 = "False" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
        'ElseIf quer1 = "True" And quer22 = "False" And quer3 = "True" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
        'ElseIf quer1 = "False" And quer22 = "True" And quer3 = "True" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
        'ElseIf quer1 = "False" And quer22 = "False" And quer3 = "True" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
        'ElseIf quer1 = "False" And quer22 = "True" And quer3 = "False" Then
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
        'Else
        '    query.CommandText = "select a.EmployeeCode, a.FullName, a.BasicRate as BasicSalary from db_payrolldata a, db_pegawai b where a.employeecode = b.employeecode"
        'End If
        oldtable.Load(query.ExecuteReader)
        For indeks As Integer = 0 To oldtable.Rows.Count - 1
            tbl_baru.Rows.Add(oldtable.Rows(indeks).Item(0),
            oldtable.Rows(indeks).Item(1),
            Decrypt(CType(oldtable.Rows(indeks).Item(2), String)))
        Next
        GridControl2.DataSource = tbl_baru
        GridView2.BestFitColumns()
        'Dim navigator As ControlNavigator = GridControl2.EmbeddedNavigator
        'navigator.Buttons.Append.Visible = False
        'navigator.Buttons.Remove.Visible = False
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        barJudul.Caption = "Loan"
        XtraTabPage3.Show()
        XtraTabPage2.Show()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        barJudul.Caption = "Rapel"
    End Sub

    Private Sub holidays()
        GridControl6.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "Select tgl as Dates, Reason as HolidaysReason, TotalDays from db_holiday where year(tgl) = year(curdate()) "
            'select * from db_holiday where month(tgl) = month(CURDATE()).
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl6.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridControl6.UseEmbeddedNavigator = True
        Dim navigator As ControlNavigator = GridControl6.EmbeddedNavigator
        navigator.Buttons.Append.Visible = False
        navigator.Buttons.Remove.Visible = False
        Dim gridView As GridView = CType(GridControl6.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
   New GridColumnSortInfo(gridView.Columns("HolidaysReason"), Data.ColumnSortOrder.Descending)}, 1)
    End Sub

    'Sub countdate()
    '    Dim dtr As DateTime
    '    dtr = CDate(DateEdit1.EditValue)
    '    DateEdit1.Properties.Mask.EditMask = CType(Date.Now, String)
    '    dtr = CDate(DateEdit1.EditValue.ToString)
    '    MsgBox(dtr)
    '    Try
    '        Dim tes As MySqlCommand = SQLConnection.CreateCommand
    '        tes.CommandText = "INSERT INTO test " +
    '                            "(datedd) " +
    '                            "values (@datedd)"
    '        tes.Parameters.AddWithValue("@datedd", dtr.ToString("yyyy-MM-dd HH:mm:ss"))
    '        tes.ExecuteNonQuery()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub    

    Sub autofill()
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter("select EmployeeCode from db_pegawai", SQLConnection)
        da.Fill(dt)
        Dim r As DataRow
        txtempcode.AutoCompleteCustomSource.Clear()
        For Each r In dt.Rows
            txtempcode.AutoCompleteCustomSource.Add(r.Item(0).ToString)
        Next
    End Sub

    Private Function inprogress(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Realisasi"))
            Return (val = "PAID")
        Catch
            Return False
        End Try
    End Function

    Sub currencyform()
        txtbasicrate.Properties.DisplayFormat.FormatType = Mask.MaskType.Numeric
        txtbasicrate.Properties.DisplayFormat.FormatString = "n"
        txtbasicrate.Properties.Mask.EditMask = "n"
        txtbasicrate.Properties.Mask.MaskType = Mask.MaskType.Numeric
        txtbasicrate.Properties.Mask.UseMaskAsDisplayFormat = True

        txtallowance.Properties.DisplayFormat.FormatType = Mask.MaskType.Numeric
        txtallowance.Properties.DisplayFormat.FormatString = "n"
        txtallowance.Properties.Mask.EditMask = "n"
        txtallowance.Properties.Mask.MaskType = Mask.MaskType.Numeric
        txtallowance.Properties.Mask.UseMaskAsDisplayFormat = True

        txtincentives.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtincentives.Properties.DisplayFormat.FormatString = "n"
        txtincentives.Properties.Mask.EditMask = "n"
        txtincentives.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtincentives.Properties.Mask.UseMaskAsDisplayFormat = True

        txtmealrate.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtmealrate.Properties.DisplayFormat.FormatString = "n"
        txtmealrate.Properties.Mask.EditMask = "n"
        txtmealrate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtmealrate.Properties.Mask.UseMaskAsDisplayFormat = True

        txttransport.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txttransport.Properties.DisplayFormat.FormatString = "n"
        txttransport.Properties.Mask.EditMask = "n"
        txttransport.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txttransport.Properties.Mask.UseMaskAsDisplayFormat = True

        txtbasicrate1.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtbasicrate1.Properties.DisplayFormat.FormatString = "n"
        txtbasicrate1.Properties.Mask.EditMask = "n"
        txtbasicrate1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtbasicrate1.Properties.Mask.UseMaskAsDisplayFormat = True

        txtallowance1.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtallowance1.Properties.DisplayFormat.FormatString = "n"
        txtallowance1.Properties.Mask.EditMask = "n"
        txtallowance1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtallowance1.Properties.Mask.UseMaskAsDisplayFormat = True

        txtincentives1.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtincentives1.Properties.DisplayFormat.FormatString = "n"
        txtincentives1.Properties.Mask.EditMask = "n"
        txtincentives1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtincentives1.Properties.Mask.UseMaskAsDisplayFormat = True

        txtmealrate1.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtmealrate1.Properties.DisplayFormat.FormatString = "n"
        txtmealrate1.Properties.Mask.EditMask = "n"
        txtmealrate1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtmealrate1.Properties.Mask.UseMaskAsDisplayFormat = True

        txttransport1.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txttransport1.Properties.DisplayFormat.FormatString = "n"
        txttransport1.Properties.Mask.EditMask = "n"
        txttransport1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txttransport1.Properties.Mask.UseMaskAsDisplayFormat = True

        txtloan.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtloan.Properties.DisplayFormat.FormatString = "n"
        txtloan.Properties.Mask.EditMask = "n"
        txtloan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtloan.Properties.Mask.UseMaskAsDisplayFormat = True

        lcpayment.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        lcpayment.Properties.DisplayFormat.FormatString = "n"
        lcpayment.Properties.Mask.EditMask = "n"
        lcpayment.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        lcpayment.Properties.Mask.UseMaskAsDisplayFormat = True

        TextEdit3.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        TextEdit3.Properties.DisplayFormat.FormatString = "n"
        TextEdit3.Properties.Mask.EditMask = "n"
        TextEdit3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        TextEdit3.Properties.Mask.UseMaskAsDisplayFormat = True
    End Sub

    Private Sub LoanForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        Dim table As New DataTable
        loaddata()
        loaddata1()
        loadloan()
        loanlists()
        txtmonth.Format = DateTimePickerFormat.Custom
        txtcompletedon1.Format = DateTimePickerFormat.Custom
        txtcompletedon1.CustomFormat = "MMMM yyyy"
        txtmonth.CustomFormat = "MMMM yyyy"
        GridView1.BestFitColumns()
        GridView4.BestFitColumns()
        GridView6.BestFitColumns()
        GridView7.BestFitColumns()
        GridView2.BestFitColumns()
        autofill()
        LabelControl6.Text = "file.pdf"
        showbor()
        showclose()
        showloan()
        showsalary()
        showotherincome()
        BarButtonItem3.PerformClick()
        currencyform()
        loanmemo()
    End Sub

    Public Overridable Property PageVisible As Boolean

    Sub showotherincome()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select otheremp from db_user where username = '" & Label7.Text & "'"
            Dim queras As String = CStr(query.ExecuteScalar)
            If queras = "True" Then
                RibbonPageGroup4.Visible = True
            Else
                RibbonPageGroup4.Visible = False
            End If
        Catch ex As Exception
            MsgBox("otherincome" & ex.Message)
        End Try
    End Sub

    Sub showloan()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select inputloans from db_user where username = '" & Label7.Text & "'"
            Dim queras As String = CStr(query.ExecuteScalar)
            If queras = "True" Then
                XtraTabPage3.PageVisible = True
            Else
                XtraTabPage3.PageVisible = False
            End If
        Catch ex As Exception
            MsgBox("Loan " & ex.Message)
        End Try
    End Sub

    Sub showsalary()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select SalaryAdjust from db_user where username = '" & Label7.Text & "'"
            Dim queras As String = CStr(query.ExecuteScalar)
            If queras = "True" Then
                XtraTabPage5.PageVisible = True
            Else
                XtraTabPage5.PageVisible = False
            End If
        Catch ex As Exception
            MsgBox("Adjust " & ex.Message)
        End Try
    End Sub

    Sub showclose()
        Try
            Dim quer As MySqlCommand = SQLConnection.CreateCommand
            quer.CommandText = "select closepayroll from db_user where username = '" & Label7.Text & "'"
            Dim queras As String = CStr(quer.ExecuteScalar)
            If queras = "True" Then
                RibbonPageGroup6.Visible = True
                RibbonPageGroup8.Visible = True
            Else
                RibbonPageGroup6.Visible = False
                RibbonPageGroup8.Visible = False
            End If
        Catch ex As Exception
            MsgBox("ClosePayroll" & ex.Message)
        End Try
    End Sub

    Sub showbor()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select inputborongan from db_user where username = '" & Label7.Text & "'"
            Dim quers As String = CStr(query.ExecuteScalar)
            If quers = "True" Then
                RibbonPageGroup9.Visible = True
            Else
                RibbonPageGroup9.Visible = False
            End If
        Catch ex As Exception
            MsgBox("Borongan " & ex.Message)
        End Try
    End Sub

    Private Sub txtname_SelectedIndexChanged(sender As Object, e As EventArgs)
        Timer1.Stop()
    End Sub

    Private Sub txtmonths_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Dim value As Integer = 0

    Sub dateyear()
        Try
            Dim Msg, Number, StartDate As String
            Dim Months As Double
            Dim SecondDate As Date
            Dim IntervalType As DateInterval
            IntervalType = DateInterval.Month
            StartDate = CType((txtmonth.Value), String)
            SecondDate = CDate(StartDate)
            Number = (txtrangemon.Text)
            Months = Val(Number)
            Msg = CType(DateAdd(IntervalType, Months, SecondDate), String)
            txtcompletedon1.Text = Msg
        Catch ex As Exception
            MsgBox("dateyear" & ex.Message)
        End Try
    End Sub

    Private Sub txtamount_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        barJudul.Caption = "Salary Adjustment"
        XtraTabPage5.Show()
    End Sub

    Private Sub ComboBoxEdit5_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' txtyears.Text = Year(Now).ToString
    End Sub

    Private Sub txtname1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtname1.SelectedIndexChanged
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            If txtname1.SelectedItem Is tbl_par.Rows(index).Item(0).ToString Then
                txtempcode1.Text = tbl_par.Rows(index).Item(1).ToString
            End If
        Next
    End Sub

    Sub cleartxt()
        txtname1.Text = ""
        txtempcode1.Text = ""
        txtwajibpajak.Text = ""
        txtnpwp.Text = ""
        txtbasicrate.Text = ""
        txtallowance.Text = ""
        txtincentives.Text = ""
        txtmealrate.Text = ""
        txttransport.Text = ""
        cjk.Checked = False
        cbpjs.Checked = False
        cjkk.Checked = False
        cjamkem.Checked = False
        cjht.Checked = False
        ciupe.Checked = False
        cbj.Checked = False
        crapel.Checked = False
        cloan.Checked = False
    End Sub

    Sub cleartxt2()
        txtname2.Text = ""
        txtempcode2.Text = ""
        txtwp1.Text = ""
        txtnpwp1.Text = ""
        txtbasicrate1.Text = ""
        txtallowance1.Text = ""
        txtincentives1.Text = ""
        txtmealrate1.Text = ""
        txttransport1.Text = ""
        cjk1.Checked = False
        cbpjs1.Checked = False
        cjkk1.Checked = False
        cjamkem1.Checked = False
        cjht1.Checked = False
        ciupe1.Checked = False
        cbj1.Checked = False
        crapel1.Checked = False
        cloan1.Checked = False
    End Sub

    Private Sub btnapp1_Click(sender As Object, e As EventArgs) Handles btnapp1.Click
        If txtname1.Text = "" OrElse txtempcode1.Text = "" Then
            MsgBox("Please Insert Employee Name Or Employee Code")
        Else
            Insertpart()
            InsertPayroll()
            cleartxt()
        End If
        'loadpayroll()
        Dim table As New DataTable
        loaddummy(table)
        'loadpayroll1()
        loaddummy2(table)
    End Sub

    Private Sub ComboBoxEdit6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtname2.SelectedIndexChanged
        For index As Integer = 0 To tbl_par2.Rows.Count - 1
            If txtname2.SelectedItem Is tbl_par2.Rows(index).Item(0).ToString Then
                txtempcode2.Text = tbl_par2.Rows(index).Item(1).ToString
                txtwp1.Text = tbl_par2.Rows(index).Item(2).ToString
                txtnpwp1.Text = tbl_par2.Rows(index).Item(3).ToString
                txtbasicrate1.Text = tbl_par2.Rows(index).Item(4).ToString
                txtallowance1.Text = tbl_par2.Rows(index).Item(5).ToString
                txtincentives1.Text = tbl_par2.Rows(index).Item(6).ToString
                txtmealrate1.Text = tbl_par2.Rows(index).Item(7).ToString
                txttransport1.Text = tbl_par2.Rows(index).Item(8).ToString
                cjk1.Checked = CBool(tbl_par2.Rows(index).Item(9).ToString)
                cbpjs1.Checked = CBool(tbl_par2.Rows(index).Item(10).ToString)
                cjkk1.Checked = CBool(tbl_par2.Rows(index).Item(11).ToString)
                cjamkem1.Checked = CBool(tbl_par2.Rows(index).Item(12).ToString)
                cjht1.Checked = CBool(tbl_par2.Rows(index).Item(13).ToString)
                ciupe1.Checked = CBool(tbl_par2.Rows(index).Item(14).ToString)
                cbj1.Checked = CBool(tbl_par2.Rows(index).Item(15).ToString)
                crapel1.Checked = CBool(tbl_par2.Rows(index).Item(16).ToString)
                cloan1.Checked = CBool(tbl_par2.Rows(index).Item(17).ToString)
            End If
        Next
    End Sub


    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If txtname2.Text = "" OrElse txtempcode2.Text = "" Then
            MsgBox("Please Input Employee Name Or Employee Code!")
        Else
            Dim mess2 As String
            mess2 = CType(MsgBox("Are you sure to change this employee data?", MsgBoxStyle.YesNo, "Warning"), String)
            If CType(mess2, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                updatechange()
                updatepart()
                cleartxt2()
            End If
        End If
        'loadpayroll1()
        'loadpayroll()
        Dim table As New DataTable
        loaddummy(table)
        loaddummy2(table)
    End Sub

    Private Sub CheckEdit2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            txtmonth.Enabled = True
            txtcompletedon1.Enabled = True
            txtmonth.Text = ""
            txtcompletedon1.Text = ""
        Else
            txtmonth.Enabled = False
            txtcompletedon1.Enabled = False
            txtmonth.Text = ""
            txtcompletedon1.Text = ""
        End If
    End Sub

    Private Sub btnApp_Click(sender As Object, e As EventArgs) Handles btnApp.Click
        If txtname.Text = "" OrElse txtempcode.Text = "" Then
            MsgBox("Please Insert Employee Name Or Employee Code!")
        Else
            InsertLoan()
            UpdateLoan()
        End If
        loadloan()
        'loadloanname()
    End Sub

    Private Sub txtrangemon_EditValueChanged(sender As Object, e As EventArgs) Handles txtrangemon.EditValueChanged
        loanpay()
        dateyear()
    End Sub

    Dim closer As New ClosePayroll

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        If closer Is Nothing OrElse closer.IsDisposed OrElse closer.MinimizeBox Then
            closer.Close()
            closer = New ClosePayroll
        End If
        closer.Show()
    End Sub

    Dim proses As New PayrollSet

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        If proses Is Nothing OrElse proses.IsDisposed OrElse proses.MinimizeBox Then
            proses.Close()
            proses = New PayrollSet
        End If
        proses.Show()
    End Sub

    Private Sub txtname3_SelectedIndexChanged(sender As Object, e As EventArgs)
        'For index As Integer = 0 To tbl_par2.Rows.Count - 1
        '    If  .SelectedItem Is tbl_par2.Rows(index).Item(0).ToString Then
        '        txtempcode3.Text = tbl_par2.Rows(index).Item(1).ToString
        '    End If
        'Next
    End Sub

    Dim overtime As New Overtime_Hours

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        If overtime Is Nothing OrElse overtime.IsDisposed OrElse overtime.MinimizeBox Then
            overtime.Close()
            overtime = New Overtime_Hours
        End If
        overtime.Show()
    End Sub

    Dim value1, value2 As Integer

    Private Sub txtloanname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtloanname.SelectedIndexChanged
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
            If txtloanname.SelectedItem Is tbl_par3.Rows(index).Item(0).ToString Then
            End If
        Next
    End Sub

    Private Sub btnLookup_Click(sender As Object, e As EventArgs) Handles btnLookup.Click
        If txtloanname.Text = "" Then
            MsgBox("Please insert the employee name", MsgBoxStyle.Exclamation)
        Else
            loadloanname()
        End If
    End Sub

    Dim act As String = ""

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And EmployeeCode='" + GridView1.GetFocusedRowCellValue("EmployeeCode").ToString() + "'"
        Catch ex As Exception
        End Try
        If param > "" Then
            act = "edit"
        Else
            act = "input"
        End If
        Try
            sqlCommand.CommandText = "SELECT FullName, EmployeeCode FROM db_loan WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatabl.Rows.Count > 0 Then
            txtloanname.Text = datatabl.Rows(0).Item(0).ToString()
            txtempcode.Text = datatabl.Rows(0).Item(1).ToString()
        End If
    End Sub

    Dim pay As New Payslip

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        If pay Is Nothing OrElse pay.IsDisposed OrElse pay.MinimizeBox Then
            pay.Close()
            pay = New Payslip
        End If
        pay.Show()
    End Sub

    Private Sub txtbasicrate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbasicrate.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtallowance_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtallowance.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtincentives_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtincentives.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtmealrate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmealrate.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txttransport_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttransport.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Sub completedloan()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_payrolldata SET" +
                                    " Loan = @Loan" +
                                    "WHERE EmployeeCode = @EmployeeCode "
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
            sqlcommand.Parameters.AddWithValue("@Loan", "1")
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub paid()
        Dim cmd As MySqlCommand = SQLConnection.CreateCommand
        cmd.CommandText = "update db_loanlist set realisasi = 'PAID' where memono = @ec"
        'cmd.Parameters.AddWithValue("@ec", txtnameloan.Text)
        cmd.Parameters.AddWithValue("@ec", Label9.Text)
        cmd.ExecuteNonQuery()
    End Sub

    Sub settling()
        Dim cmd As MySqlCommand = SQLConnection.CreateCommand
        Dim mess As String
        Dim name As MySqlCommand = SQLConnection.CreateCommand
        name.CommandText = "select fullname from db_loan where employeecode = @ec and issettle = '0'"
        name.Parameters.AddWithValue("@ec", txtnameloan.Text)
        Dim rname As String = CStr(name.ExecuteScalar)
        mess = CType(MsgBox("Sure to settle this employee named  " & rname & " with employee code " & txtnameloan.Text & "?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            paid()
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.CommandText = "update db_loan set issettle = '1' where employeecode = '" & txtnameloan.Text & "'"
            cmd.CommandText = "update db_loan set issettle = '1' where memono = '" & Label9.Text & "'"
            cmd.ExecuteNonQuery()
            MsgBox("Settled", MsgBoxStyle.Information, "Success")
        End If
    End Sub

    Sub updatedloan()
        Dim cmd As MySqlCommand = SQLConnection.CreateCommand
        'conn.Open()
        cmd.CommandText = "update db_payrolldata set loan = 0 where employeecode = @emp"
        cmd.Parameters.AddWithValue("@emp", txtnameloan.Text)
        cmd.ExecuteNonQuery()
        'conn.Close()
    End Sub

    Private Sub txtnameloan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtnameloan.SelectedIndexChanged
        loanlists()
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
            If txtnameloan.SelectedItem Is tbl_par3.Rows(index).Item(1).ToString Then
            End If
        Next
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call complete()
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        'update db_absensi set isholiday = 1 where dayofweek(tanggal) = 7 or tanggal in (select tgl from db_holiday)
        days()
        If date1.Text = "" Then
            MsgBox("Please Input The Start Date")
        ElseIf date1.Value > date2.Value Then
            MsgBox("Total days can't be zero or less than zero days")
        ElseIf txtreason.Text = "" Then
            MsgBox("Please insert the reason of the holiday")
        Else
            Process()
            holidays()
        End If
    End Sub

    Sub days()
        Dim t As TimeSpan = date2.Value - date1.Value
        Dim tmp As String
        tmp = t.Days.ToString
        Dim hasil As Integer
        hasil = CInt(tmp)
        Dim hasil2 As Integer = hasil + 1
        txtdays.Text = hasil2.ToString
    End Sub

    Private Sub date2_ValueChanged(sender As Object, e As EventArgs) Handles date2.ValueChanged
        days()
    End Sub

    Private Sub SimpleButton3_Click_1(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If txtnameloan.Text = "" Then
            MsgBox("Please Input The EmployeeCode To The Field!")
        Else
            Call settling()
            updatedloan()
            loadloan()
            txtnameloan.Text = ""
        End If
    End Sub

    Private Sub txtname3_SelectedIndexChanged_1(sender As Object, e As EventArgs)
        Timer2.Stop()
    End Sub

    Private Sub GridView4_FocusedRowChanged(sender As Object, e As Base.FocusedRowChangedEventArgs) Handles GridView4.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "and EmployeeCode='" + GridView4.GetFocusedRowCellValue("EmployeeCode").ToString() + "'"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            sqlCommand.CommandText = "SELECT FullName, EmployeeCode, StatusWajibPajak, MemilikiNpwp, BasicRate, Allowance, Incentives, MealRate, Transport, JaminanKesehatan, Bpjs, JaminanKecelakaanKerja, JaminanKematian, JaminanHariTua, IuranPensiun, BiayaJabatan, Rapel, Loan, Golongan, MealTidakTetap, TransportTidakTetap, Deductions from db_payrolldata WHERE 1= 1 " + param.ToString
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If datatabl.Rows.Count > 0 Then
            txtname2.Text = datatabl.Rows(0).Item(0).ToString()
            txtempcode2.Text = datatabl.Rows(0).Item(1).ToString()
            txtwp1.Text = datatabl.Rows(0).Item(2).ToString()
            txtnpwp1.Text = datatabl.Rows(0).Item(3).ToString()
            txtbasicrate1.Text = Decrypt(datatabl.Rows(0).Item(4).ToString())
            TextBox6.Text = datatabl.Rows(0).Item(4).ToString
            txtallowance1.Text = Decrypt(datatabl.Rows(0).Item(5).ToString())
            TextBox7.Text = datatabl.Rows(0).Item(5).ToString
            txtincentives1.Text = Decrypt(datatabl.Rows(0).Item(6).ToString())
            TextBox8.Text = datatabl.Rows(0).Item(6).ToString
            txtmealrate1.Text = Decrypt(datatabl.Rows(0).Item(7).ToString())
            TextBox9.Text = datatabl.Rows(0).Item(8).ToString
            txttransport1.Text = Decrypt(datatabl.Rows(0).Item(8).ToString())
            TextBox10.Text = datatabl.Rows(0).Item(8).ToString
            cjk1.Checked = CBool(datatabl.Rows(0).Item(9).ToString)
            cbpjs1.Checked = CBool(datatabl.Rows(0).Item(10).ToString)
            cjkk1.Checked = CBool(datatabl.Rows(0).Item(11).ToString)
            cjamkem1.Checked = CBool(datatabl.Rows(0).Item(12).ToString)
            cjht1.Checked = CBool(datatabl.Rows(0).Item(13).ToString)
            ciupe1.Checked = CBool(datatabl.Rows(0).Item(14).ToString)
            cbj1.Checked = CBool(datatabl.Rows(0).Item(15).ToString)
            crapel1.Checked = CBool(datatabl.Rows(0).Item(16).ToString)
            cloan1.Checked = CBool(datatabl.Rows(0).Item(17).ToString)
            txtgol1.Text = datatabl.Rows(0).Item(18).ToString
            CheckEdit4.Checked = CBool(datatabl.Rows(0).Item(19).ToString)
            CheckEdit5.Checked = CBool(datatabl.Rows(0).Item(20).ToString)
            TextEdit4.Text = Decrypt(datatabl.Rows(0).Item(21).ToString)
        End If
    End Sub

    Private Sub GridView6_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView6.PopupMenuShowing
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
    End Sub

    Private Sub GridView2_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView2.PopupMenuShowing
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
    End Sub

    Private Sub GridView4_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView4.PopupMenuShowing
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
    End Sub

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
    End Sub

    Sub download()
        Try
            Dim sFilePath As String
            Dim buffer As Byte()
            Using cmd As New MySqlCommand("select guaranteedoc from db_loan where employeecode = '" & Label6.Text & "'", SQLConnection)
                buffer = CType(cmd.ExecuteScalar(), Byte())
            End Using
            sFilePath = Path.GetTempFileName()
            File.Move(sFilePath, Path.ChangeExtension(sFilePath, ".pdf"))
            sFilePath = Path.ChangeExtension(sFilePath, ".pdf")
            File.WriteAllBytes(sFilePath, buffer)
            pdfviewer.PdfViewer1.LoadDocument(sFilePath)
            pdfviewer.Show()
            'Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            'act.BeginInvoke(sFilePath, Nothing, Nothing)
        Catch ex As Exception
            MsgBox("No document uploaded or document corrupted")
        End Try
    End Sub

    Private Shared Sub OpenPDFFile(ByVal sFilePath As String)
        Using p As New Process
            p.StartInfo = New ProcessStartInfo(CType(sFilePath, String))
            p.Start()
            p.WaitForExit()
            Try
                File.Delete(CType(sFilePath, String))
            Catch
            End Try
        End Using
    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Function GetImage1() As Image
        Return ImageCollection1.Images(1)
    End Function

    Private Sub GridView7_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView7.PopupMenuShowing
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Add(New DXMenuItem("View Guarantee Documents ", AddressOf download, GetImage))
            e.Menu.Items.Add(New DXMenuItem("Settle Loans", New EventHandler(AddressOf SimpleButton3_Click_1), GetImage1))
        End If
    End Sub

    Private Sub GridView5_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs)
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
    End Sub

    Dim sel As New selectemp

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Timer1.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label7.Text
            .Show()
        End With
    End Sub

    Function ImageToByte(ByVal pbImg As PictureBox) As Byte()
        If pbImg Is Nothing Then
            Return Nothing
        End If
        Dim ms As New MemoryStream()
        pbImg.Image.Save(ms, Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Public Function ByteToImage(ByVal filefoto As Byte()) As Image
        Dim pictureBytes As New MemoryStream(filefoto)
        Return Image.FromStream(pictureBytes)
    End Function

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs)

    End Sub

    Dim form As New Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If form Is Nothing OrElse form.IsDisposed OrElse form.MinimizeBox Then
            form.Close()
            form = New Form1
        End If
        form.Show()
    End Sub

    Private Sub txtmonth_ValueChanged(sender As Object, e As EventArgs) Handles txtmonth.ValueChanged
        dateyear()
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs)
        Timer2.Start()
        If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
            sel.Close()
            sel = New selectemp
        End If
        sel.Show()
    End Sub

    Private Sub txtcompletedon1_ValueChanged(sender As Object, e As EventArgs) Handles txtcompletedon1.ValueChanged
        dateyear()
    End Sub

    Public value19, value18 As Long

    Private Sub txtloan_EditValueChanged(sender As Object, e As EventArgs) Handles txtloan.EditValueChanged
        'Try
        '    value19 = CInt(txtloan.Text)
        '    txtloan.Text = Format(value19, "##,##0")
        '    txtloan.SelectionStart = Len(txtloan.Text)
        'Catch ex As Exception
        'End Try
        'TextBox11.Text = txtloan.Text
        loanpay()
        dateyear()
    End Sub

    Private Sub date1_ValueChanged(sender As Object, e As EventArgs) Handles date1.ValueChanged
        days()
    End Sub

    Dim other As New OtherIncome

    Private Sub BarButtonItem9_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        'If other Is Nothing OrElse other.IsDisposed OrElse other.MinimizeBox Then
        '    other.Close()
        '    other = New OtherIncome
        'End If
        'other.Show()
        OtherIncome.Close()
        With OtherIncome
            .Label3.Text = Label7.Text
            .Show()
        End With
    End Sub

    Private Sub txtloan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtloan.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtbasicrate1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbasicrate1.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtallowance1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtallowance1.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtincentives1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtincentives1.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtmealrate1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmealrate1.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txttransport1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttransport1.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            txtname.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            txtempcode.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtempcode_TextChanged(sender As Object, e As EventArgs) Handles txtempcode.TextChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select fullname from db_pegawai where employeecode = '" & txtempcode.Text & "'"
            Dim quer As String = CStr(query.ExecuteScalar)
            txtname.Text = quer.ToString
        Catch ex As Exception
        End Try
    End Sub

    Dim bor As New Borongan

    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        Borongan.Close()
        With Borongan
            .Label2.Text = Label7.Text
            .Show()
        End With
    End Sub

    Public value11, value12, value3, value4, value5 As Long

    'Private Sub txtmealrate_EditValueChanged(sender As Object, e As EventArgs) Handles txtmealrate.EditValueChanged
    '    Try
    '        value4 = CInt(txtmealrate.Text)
    '        txtmealrate.Text = Format(value4, "##,##0")
    '        txtmealrate.SelectionStart = Len(txtmealrate.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox4.Text = txtmealrate.Text
    'End Sub

    'Private Sub txttransport_EditValueChanged(sender As Object, e As EventArgs) Handles txttransport.EditValueChanged
    '    Try
    '        value5 = CInt(txttransport.Text)
    '        txttransport.Text = Format(value5, "##,##0")
    '        txttransport.SelectionStart = Len(txttransport.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox5.Text = txttransport.Text
    'End Sub

    Public value13, value14, value15, value16, value17 As Long

    Dim fm1 As New Form1

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If fm1 Is Nothing OrElse fm1.IsDisposed OrElse fm1.MinimizeBox Then
            fm1.Close()
            fm1 = New Form1
        End If
        fm1.Show()
    End Sub

    Private Sub XtraTabControl3_Click(sender As Object, e As EventArgs) Handles XtraTabControl3.Click

    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        'If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
        '    '  MessageBox.Show("Please enter numbers only")
        '    e.Handled = True
        'End If
    End Sub

    Dim openfd As New OpenFileDialog

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        openfd.InitialDirectory = "C:\"
        openfd.Title = "Open a document files"
        openfd.Filter = "PDF Files|*.pdf"
        'Text Files|*.txt|Word FIles|*.docx"
        openfd.ShowDialog()
        LabelControl6.Text = openfd.FileName
    End Sub

    Private Sub GridView7_FocusedRowChanged(sender As Object, e As Base.FocusedRowChangedEventArgs) Handles GridView7.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And EmployeeCode='" + GridView7.GetFocusedRowCellValue("EmployeeCode").ToString() + "'"
            sqlCommand.CommandText = "SELECT FullName, EmployeeCode, MemoNo FROM db_loan WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatabl.Rows.Count > 0 Then
            Label6.Text = datatabl.Rows(0).Item(1).ToString()
            txtnameloan.Text = datatabl.Rows(0).Item(1).ToString
            Label9.Text = datatabl.Rows(0).Item(2).ToString

        End If
    End Sub

    Private Sub GridView2_CustomColumnDisplayText(sender As Object, e As Base.CustomColumnDisplayTextEventArgs) Handles GridView2.CustomColumnDisplayText
        If e.Column.FieldName = "BasicSalary" Then
            e.Column.DisplayFormat.FormatType = Utils.FormatType.Numeric
            e.Column.DisplayFormat.FormatString = "n2"
        End If
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        If inprogress(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.Silver
        End If
    End Sub

    Private Sub RibbonControl1_Click(sender As Object, e As EventArgs) Handles RibbonControl1.Click

    End Sub

    Private Sub GridView4_CustomColumnDisplayText(sender As Object, e As Base.CustomColumnDisplayTextEventArgs) Handles GridView4.CustomColumnDisplayText
        If e.Column.FieldName = "BasicSalary" Then
            e.Column.DisplayFormat.FormatType = Utils.FormatType.Numeric
            e.Column.DisplayFormat.FormatString = "n2"
        End If
    End Sub

    Private Sub GridView7_CustomColumnDisplayText(sender As Object, e As Base.CustomColumnDisplayTextEventArgs) Handles GridView7.CustomColumnDisplayText
        If e.Column.FieldName = "AmountOfLoan" OrElse e.Column.FieldName = "PaymentPerMonth" Then
            e.Column.DisplayFormat.FormatType = Utils.FormatType.Numeric
            e.Column.DisplayFormat.FormatString = "n2"
        End If
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged

    End Sub

    Private Sub SimpleButton4_Click_1(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Dim table As New DataTable
        loaddummy2(table)
        loaddummy(table)
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        GridControl2.DataSource = Nothing
        GridControl4.DataSource = Nothing
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        Dim table As New DataTable
        loaddummy2(table)
        loaddummy(table)
    End Sub

    Private Sub SimpleButton8_Click_1(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        GridControl2.DataSource = Nothing
        GridControl4.DataSource = Nothing
        txtbasicrate1.Text = ""
        txtallowance1.Text = ""
        txtincentives1.Text = ""
        txtmealrate1.Text = ""
        txttransport1.Text = ""
        txtgol1.Text = ""
        txtempcode2.Text = ""
        txtname2.Text = ""
    End Sub

    Private Sub txtdays_TextChanged(sender As Object, e As EventArgs) Handles txtdays.TextChanged

    End Sub

    Private Sub lcpayment_EditValueChanged(sender As Object, e As EventArgs) Handles lcpayment.EditValueChanged
        'Try
        '    value18 = CInt(lcpayment.Text)
        '    lcpayment.Text = Format(value18, "##,##0")
        '    lcpayment.SelectionStart = Len(lcpayment.Text)
        'Catch ex As Exception
        'End Try
        'TextEdit2.Text = lcpayment.Text
    End Sub

    'Private Sub txttransport1_EditValueChanged(sender As Object, e As EventArgs) Handles txttransport1.EditValueChanged
    '    Try
    '        value16 = CInt(txttransport1.Text)
    '        txttransport1.Text = Format(value16, "##,##0")
    '        txttransport.SelectionStart = Len(txttransport1.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox10.Text = txttransport1.Text
    'End Sub

    'Private Sub txtmealrate1_EditValueChanged(sender As Object, e As EventArgs) Handles txtmealrate1.EditValueChanged
    '    Try
    '        value15 = CInt(txtmealrate1.Text)
    '        txtmealrate1.Text = Format(value15, "##,##0")
    '        txtmealrate1.SelectionStart = Len(txtmealrate1.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox9.Text = txtmealrate1.Text
    'End Sub

    'Private Sub txtincentives1_EditValueChanged(sender As Object, e As EventArgs) Handles txtincentives1.EditValueChanged
    '    Try
    '        value14 = CInt(txtincentives1.Text)
    '        txtincentives1.Text = Format(value14, "##,##0")
    '        txtincentives1.SelectionStart = Len(txtincentives1.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox8.Text = txtincentives1.Text
    'End Sub

    'Private Sub txtallowance1_EditValueChanged(sender As Object, e As EventArgs) Handles txtallowance1.EditValueChanged
    '    Try
    '        value13 = CInt(txtallowance1.Text)
    '        txtallowance1.Text = Format(value13, "##,##0")
    '        txtallowance1.SelectionStart = Len(txtallowance1.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox7.Text = txtallowance1.Text
    'End Sub

    'Private Sub txtincentives_EditValueChanged(sender As Object, e As EventArgs) Handles txtincentives.EditValueChanged
    '    Try
    '        value3 = CInt(txtincentives.Text)
    '        txtincentives.Text = Format(value3, "##,##0")
    '        txtincentives.SelectionStart = Len(txtincentives.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox3.Text = txtincentives.Text
    'End Sub

    'Private Sub txtallowance_EditValueChanged(sender As Object, e As EventArgs) Handles txtallowance.EditValueChanged
    '    Try
    '        value12 = CInt(txtallowance.Text)
    '        txtallowance.Text = Format(value12, "##,##0")
    '        txtallowance.SelectionStart = Len(txtbasicrate.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox2.Text = txtallowance.Text
    'End Sub

    'Private Sub txtbasicrate_EditValueChanged(sender As Object, e As EventArgs) Handles txtbasicrate.EditValueChanged
    '    Try
    '        value11 = CInt(txtbasicrate.Text)
    '        txtbasicrate.Text = Format(value11, "##,##0")
    '        txtbasicrate.SelectionStart = Len(txtbasicrate.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox1.Text = txtbasicrate.Text
    'End Sub

    'Private Sub txtbasicrate1_EditValueChanged(sender As Object, e As EventArgs) Handles txtbasicrate1.EditValueChanged
    '    Try
    '        value1 = CInt(txtbasicrate1.Text)
    '        txtbasicrate1.Text = Format(value1, "##,##0")
    '        txtbasicrate1.SelectionStart = Len(txtbasicrate1.Text)
    '    Catch ex As Exception
    '    End Try
    '    TextBox6.Text = txtbasicrate1.Text
    'End Sub

    Sub complete()
        Dim count As MySqlCommand = SQLConnection.CreateCommand
        count.CommandText = "select count(*) from db_loanlist where realisasi = curdate()"
        Dim xcount As Integer = CInt(count.ExecuteScalar)
        If xcount = 0 Then
            MsgBox("There's no data")
        Else
            Dim sqlcommand As New MySqlCommand
            sqlcommand.CommandType = CommandType.StoredProcedure
            sqlcommand.CommandText = "completedloan"
            Dim p1 As New MySqlParameter
            p1.ParameterName = "@empcode"
            p1.Value = txtnameloan.Text
            sqlcommand.Parameters.Add(p1)
            sqlcommand.Connection = SQLConnection
        End If
    End Sub
End Class