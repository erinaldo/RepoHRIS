Imports System.IO

Public Class Criteria
    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()
    Dim tbl_par As New DataTable

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call.
        Dim host As String
        Dim id As String
        Dim password As String
        Dim db As String
        If File.Exists("settinghost.txt") Then
            host = File.ReadAllText("settinghost.txt")
        Else
            host = "localhost"
        End If
        If File.Exists("settingid.txt") Then
            id = File.ReadAllText("settingid.txt")
        Else
            id = "root"
        End If
        If File.Exists("settingpass.txt") Then
            password = File.ReadAllText("settingpass.txt")
        Else
            password = ""
        End If
        If File.Exists("settingdb.txt") Then
            db = File.ReadAllText("settingdb.txt")
        Else
            db = "db_hris"
        End If
        connectionString = "Server=" + host + "; User Id=" + id + "; Password=" + password + "; Database=" + db + ""
    End Sub

    Private Sub Criteria_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Try
            'Dim query As MySqlCommand = SQLConnection.CreateCommand
            'query.CommandText = "select status from db_tmpname"
            'Dim quer2 As String = CType(query.ExecuteScalar, String)
            'Label1.Text = quer2.ToString
            If Label1.Text = "Candidates List" Then
                CheckEdit1.Text = "Id Recruitment"
            Else
                CheckEdit1.Text = "Employee Code"
            End If
            If Label1.Text = "Holiday Lists" Then
                CheckEdit1.Enabled = False
                TextEdit1.Enabled = False
                SimpleButton1.Enabled = False
                CheckEdit2.Enabled = False
                textedit2.Enabled = False
                SimpleButton2.Enabled = False
            Else
                CheckEdit1.Enabled = True
                TextEdit1.Enabled = True
                SimpleButton1.Enabled = True
                CheckEdit2.Enabled = True
                textedit2.Enabled = True
                SimpleButton2.Enabled = True
            End If
        Catch ex As Exception
        End Try
        autofill()
        DateTimePicker1.Enabled = True
        DateTimePicker2.Enabled = True
        fill()
    End Sub

    Sub fill()
        If Label1.Text = "Candidates List" OrElse Label1.Text = "Employee List" OrElse Label1.Text = "Loan Summary" OrElse Label1.Text = "Pajak" Then
            CheckEdit3.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        Else
            CheckEdit3.Enabled = True
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        End If
    End Sub

    Sub autofill()
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter("select EmployeeCode from db_pegawai", SQLConnection)
        da.Fill(dt)
        Dim r As DataRow
        textedit2.AutoCompleteCustomSource.Clear()
        For Each r In dt.Rows
            textedit2.AutoCompleteCustomSource.Add(r.Item(0).ToString)
        Next
    End Sub

    Private Sub CheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit1.CheckedChanged
        If CheckEdit1.Checked = True Then
            CheckEdit2.Checked = False
            'If Label1.Text = "Candidates List" Then
            '    If CheckEdit1.Checked = True Then
            '        TextEdit1.Enabled = True
            '        TextEdit1.Text = ""
            '        SimpleButton1.Enabled = True
            '        CheckEdit2.Checked = False
            '        CheckEdit3.Checked = False
            '        CheckEdit3.Enabled = False
            '    Else
            '        CheckEdit2.Enabled = True
            '        TextEdit1.Enabled = False
            '        SimpleButton1.Enabled = False
            '        CheckEdit3.Enabled = True
            '    End If
            'Else
            '    If CheckEdit1.Checked = True Then
            '        TextEdit1.Enabled = True
            '        TextEdit1.Text = ""
            '        SimpleButton1.Enabled = True
            '        CheckEdit2.Checked = False
            '    Else
            '        TextEdit1.Enabled = False
            '        CheckEdit2.Enabled = False
            '        SimpleButton1.Enabled = False
            '    End If
        End If
    End Sub

    Private Sub CheckEdit2_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            textedit2.Enabled = False
            SimpleButton2.Enabled = False
            CheckEdit1.Checked = False
        Else
            textedit2.Enabled = True
            SimpleButton2.Enabled = True
        End If
    End Sub

    Dim sel1 As New selectcand

    Dim sel2 As New selectemp

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Start()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()

        If Label1.Text <> "Candidates List" Then
            selectemp.Close()
            With selectemp
                .Label6.Text = Label2.Text
                .Show()
            End With
            'If sel2 Is Nothing OrElse sel2.IsDisposed OrElse sel2.MinimizeBox Then
            '    sel2.Close()
            '    sel2 = New selectemp
            'End If
            'sel2.Show()
        Else
            'CheckEdit1.Text = "Id Recruitment"
            If sel1 Is Nothing OrElse sel1.IsDisposed OrElse sel1.MinimizeBox Then
                sel1.Close()
                sel1 = New selectcand
            End If
            sel1.Show()
        End If
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Close()
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select idrec, employeecode from db_tmpname"
            Dim tab As New DataTable
            tab.Load(query.ExecuteReader)
            For index As Integer = 0 To tab.Rows.Count - 1
                If Label1.Text = "Candidates List" Then
                    CheckEdit1.Text = "Id Recruitment"
                    TextEdit1.Text = tab.Rows(index).Item(0).ToString
                Else
                    TextEdit1.Text = tab.Rows(index).Item(1).ToString
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CheckEdit3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit3.CheckedChanged
        If CheckEdit3.Checked = True Then
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If
    End Sub

    Dim rep As Reports

    Public Overridable Property UseEmbeddedNavigator As Boolean

    Sub candidates(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select IdRec, InterviewTImes as AppliedTimes, FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Status, Reason, CreatedDate, ExpectedSalary from db_recruitment where idrec = @emp"
        query.Parameters.AddWithValue("@emp", emp)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub employee(emp As String)
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai where employeecode = @emp"
        sqlcommand.Parameters.AddWithValue("@emp", emp)
        Dim dt As New DataTable
        dt.Load(sqlcommand.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub attendance(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, FullName, Tanggal as Date, Shift, JamMulai as StartingHour, JamSelesai as EndedHour from db_absensi where EmployeeCode = @emp And tanggal between @date1 And @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        With Form2
            .GridView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
        End With
    End Sub

    Sub dinas(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        With query
            .CommandText = "select MemoNo, EmployeeCode, FullName, IssuesDates, Department, FromDates, ToDates, DestinationCity, Agenda, ApprovedBy, ApprovedStatus from db_sppd where issuesdates between @date1 and @date2"
            .Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
            .Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        End With
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        With Form2
            .GridView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
        End With
    End Sub

    Sub overtime(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, FullName, Tanggal as Date, Shift, OvertimeType, OvertimeHours from db_absensi where EmployeeCode = @emp And tanggal between @date1 And @date2 and overtimehours <> 0"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        With Form2
            .GridView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
        End With
    End Sub

    Sub warning(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, tgl as Date, EmployeeCode, FullName, WarningLevel, OffenseType, DescriptionOfInfraction as Description, Plan, Consequences from db_warning where employeecode = @emp And tgl between @date1 And @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        With Form2
            .GridView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridView1.BestFitColumns()
            .GridControl2.UseEmbeddedNavigator = True
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
        End With
    End Sub

    Sub leavereq(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, tgl as Date, EmployeeCode, FullName, Reason, StartDate, EndDate, TotalDays from db_attrec where employeecode = @emp And tgl between @date1 And @date2 and approvedstatus = 'Approved'"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub others(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, Tanggal as Date, EmployeeCode, Period, Until, Amount, As1 as AsAn, Reason from db_addition where employeecode = @emp And tanggal between @date1 And @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub loanlist(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, FullName, ApprovedBy, Reason, Dates as Date, Date_format(FromMonths, '%M-%Y') as FromMonth, AmountOfLoan, Date_Format(CompletedOn, '%M-%Y') as CompletedPayment from db_loan where EmployeeCode = @emp and dates between @date1 and @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub loansummary(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, FullName, AmountOfLoan, Monthx, Realisasi from db_loanlist where EmployeeCode = @emp"
        query.Parameters.AddWithValue("@emp", emp)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub payroll(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select a.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.employee_type as EmployeeType from payroll2 a, db_pegawai b where a.employee_code = @emp and b.EmployeeCode = @emp and a.pay_date between @date1 and @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub premi(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, CompanyCode, FullName, Tanggal as Date, Tasks, Machines, Quantity from db_borongan where employeecode = @emp and tanggal between @date1 and @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub absence(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, FullName, Tanggal as Date, Shift, JamMulai as StartingHour, JamSelesai as EndedHour from db_absensi where EmployeeCode = @emp and JamMulai is null and JamSelesai is null and tanggal between @date1 and @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub terminate(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, FullName, EmployeeCode, tgl as Dates, ApprovedBy, Reason, JobTitle, Status, Explanation from db_terminate where EmployeeCode = @emp and tgl between @date1 and @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub latesin(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, FullName, Tanggal as Dates, Shift, timediff(Date_Format(JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut from db_absensi where employeecode = @emp and tanggal between @date1 and @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub statuschange(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select EmployeeCode, FullName, tgl as ChangeDate, ChangeType, JobTitle, OfficeLocation, Department, Grouping as Groups from db_statuschange where status = 'Approved' and employeecode = @emp and tgl between @date1 and @date2"
        query.Parameters.AddWithValue("@emp", emp)
        query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        'With Reports
        '    .GridControl1.DataSource = dt
        '    .GridControl1.UseEmbeddedNavigator = True
        '    .Label3.Text = Label2.Text
        '    '.Show()
        'End With
        With Form2
            .GridView1.Columns.Clear()
            '.CardView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            '.Show()
        End With
    End Sub

    Sub pajak(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select a.basicrate, a.allowance, b.pphstatus, c.jamkecelakaankerja, c.jaminankematian, c.biayajabatan, c.jaminanharitua, c.iuranpensiun, b.memilikinpwp from db_payrolldata a, db_pegawai b, db_setpayroll c where a.employeecode = '" & emp & "'"
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        For i As Integer = 0 To tab.Rows.Count - 1
            Dim basicrate As Integer = CInt(Decrypt(tab.Rows(i).Item(0).ToString))
            Dim allowance As Integer = CInt(Decrypt(tab.Rows(i).Item(1).ToString))
            Dim pphstats As String = tab.Rows(i).Item(2).ToString
            Dim jkk As String = tab.Rows(i).Item(3).ToString
            Dim jamkem As String = tab.Rows(i).Item(4).ToString
            Dim bj As String = tab.Rows(i).Item(5).ToString
            Dim jht As String = tab.Rows(i).Item(6).ToString
            Dim ip As String = tab.Rows(i).Item(7).ToString
            query.CommandText = "select PphStatus from db_pegawai where employeecode = '" & emp & "'"
            Dim quer2 As String = CStr(query.ExecuteScalar)
            query.CommandText = CType("select " & quer2 & " from db_setpayroll", String)
            Dim ptkp As String = CStr(query.ExecuteScalar)
            query.CommandText = "insert into db_hasil (EmployeeCode, GajiPokok, TunjanganLain, JKK, Jk, Bruto, BiayaJabatan, JHT, JaminanPensiun, NettoBulan, NettoTahun, Ptkp)  values (@Emp, @Gapok, @tunjanganlain, @jkk, @jk, @bruto, @biayajabatan, @jht, @jaminanpensiun, @nettobulan, @nettotahun, @ptkp)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            query.Parameters.AddWithValue("@gapok", basicrate)
            query.Parameters.AddWithValue("@tunjanganlain", allowance)
            query.Parameters.AddWithValue("@jkk", basicrate * CInt(jkk) / 100)
            query.Parameters.AddWithValue("@jk", basicrate * CInt(jamkem) / 100)
            query.Parameters.AddWithValue("@bruto", basicrate + allowance + (basicrate * CInt(jkk) / 100) + (basicrate + CInt(jamkem) / 100))
            query.Parameters.AddWithValue("@biayajabatan", basicrate + allowance + (basicrate * CInt(jkk) / 100) + (basicrate + CInt(jamkem) / 100) * CInt(bj) / 100)
            query.Parameters.AddWithValue("@jht", basicrate * CInt(jht) / 100)
            query.Parameters.AddWithValue("@jaminanpensiun", basicrate * CInt(ip) / 100)
            query.Parameters.AddWithValue("@nettobulan", (basicrate + allowance + (basicrate * CInt(jkk) / 100) + (basicrate + CInt(jamkem) / 100)) - (basicrate * CInt(ip)) / 100)
            query.Parameters.AddWithValue("@nettotahun", (basicrate + allowance + (basicrate * CInt(jkk) / 100) + (basicrate + CInt(jamkem) / 100)) - (basicrate * CInt(ip)) / 100 * 12)
            query.Parameters.AddWithValue("@ptkp", ptkp)
            query.ExecuteNonQuery()
        Next
        query.CommandText = "Select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk As JaminanKecelakaanKerja, a.Jk As JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT As JaminanHariTua, a.JaminanPensiun, a.NettoBulan As NettoPerBulan, a.NettoTahun As NettoPerTahun, a.Ptkp, a.pphbulan As PphPerBulan from db_hasil a, db_payrolldata b where b.employeecode = @emp"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@emp", emp)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        With Form2
            .GridView1.Columns.Clear()
            .LayoutView1.Columns.Clear()
            .GridControl1.DataSource = dt
            .GridControl2.DataSource = dt
            .GridControl1.UseEmbeddedNavigator = True
            .Label3.Text = Label2.Text
            .LabelControl2.Text = Label1.Text
            .Show()
        End With
    End Sub

    Sub pajaks(emp As String)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "Select basicrate from db_payrolldata where employeecode = '" & emp & "'"
        Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select allowance from db_payrolldata where employeecode = '" & emp & "'"
        Dim quer1 As String = CStr(query.ExecuteScalar)
        query.CommandText = "select PphStatus from db_pegawai where employeecode = '" & emp & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        Try
            query.CommandText = CType("select " & quer2 & " from db_setpayroll", String)
            Dim quer3 As String = CStr(query.ExecuteScalar)
            query.CommandText = "select a.Basicrate * b.jamkecelakaankerja / 100 from db_payrolldata a, db_setpayroll b where Employeecode = @emp"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            Dim quer5 As Integer = CInt(query.ExecuteScalar)

            query.CommandText = "select a.Basicrate * b.JaminanKematian / 100 from db_payrolldata a, db_setpayroll b where EmployeeCode = @emp"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            Dim quer6 As Int64 = CInt(query.ExecuteScalar)

            query.CommandText = "select basicrate + allowance from db_payrolldata where employeecode = @emp"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            Dim quer7 As String = CStr(query.ExecuteScalar)
            Dim quer77 As Integer = (CInt(quer7) + quer5 + quer6)

            query.CommandText = "select a.basicrate + a.allowance + (a.basicrate * b.jamkecelakaankerja / 100) + (a.basicrate * b.jaminankematian / 100) * b.biayajabatan / 100 from db_payrolldata a, db_setpayroll b where employeecode = @emp"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            Dim quer8 As String = CStr(query.ExecuteScalar)

            query.CommandText = "select a.basicrate * b.jaminanharitua / 100 from db_payrolldata a, db_setpayroll b where employeecode = @emp"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            Dim quer9 As String = CStr(query.ExecuteScalar)

            query.CommandText = "select a.basicrate * b.iuranpensiun / 100 from db_payrolldata a, db_setpayroll b where employeecode = @emp"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            Dim quer10 As String = CStr(query.ExecuteScalar)

            Dim nettobulan As Integer = CInt(quer77 - CInt(quer10))

            query.CommandText = "insert into db_hasil (EmployeeCode, GajiPokok, TunjanganLain, JKK, Jk, Bruto, BiayaJabatan, JHT, JaminanPensiun, NettoBulan, NettoTahun, Ptkp, pphbulan) " +
                                " select @Emp, @Gapok, @tunjanganlain, @jkk, @jk, @bruto, @biayajabatan, @jht, @jaminanpensiun, @nettobulan, @nettotahun, @ptkp, calc_pajak(a.BasicRate, b.JamKecelakaanKerja, b.JaminanKematian, a.Allowance, b.BiayaJabatan, a.MemilikiNpwp, b.JaminanHariTua, b.IuranPensiun, a.StatusWajibPajak) from db_payrolldata a, db_setpayroll b  where a.EmployeeCode = '" & emp & "'"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            query.Parameters.AddWithValue("@gapok", quer)
            query.Parameters.AddWithValue("@tunjanganlain", quer1)
            query.Parameters.AddWithValue("@jkk", quer5)
            query.Parameters.AddWithValue("@jk", quer6)
            query.Parameters.AddWithValue("@bruto", quer77)
            query.Parameters.AddWithValue("@biayajabatan", quer8)
            query.Parameters.AddWithValue("@jht", quer9)
            query.Parameters.AddWithValue("@jaminanpensiun", quer10)
            query.Parameters.AddWithValue("@nettobulan", nettobulan)
            query.Parameters.AddWithValue("@nettotahun", nettobulan * 12)
            query.Parameters.AddWithValue("@ptkp", quer3)
            query.ExecuteNonQuery()
            query.CommandText = "select a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk as JaminanKecelakaanKerja, a.Jk as JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT as JaminanHariTua, a.JaminanPensiun, a.NettoBulan as NettoPerBulan, a.NettoTahun as NettoPerTahun, a.Ptkp, a.pphbulan as PphPerBulan from db_hasil a, db_payrolldata b where a.employeecode = @emp and b.employeecode = @emp"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@emp", emp)
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            'With Reports
            '    .GridControl1.DataSource = dt
            '    .GridControl1.UseEmbeddedNavigator = True
            '    .Label3.Text = Label2.Text
            '    '.Show()
            'End With
            With Form2
                .GridView1.Columns.Clear()
                '.CardView1.Columns.Clear()
                .LayoutView1.Columns.Clear()
                .GridControl1.DataSource = dt
                .GridControl2.DataSource = dt
                .GridControl1.UseEmbeddedNavigator = True
                .Label3.Text = Label2.Text
                .LabelControl2.Text = Label1.Text
                '.Show()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Sub proceed()
        If Label1.Text = "Candidates List" Then
            If CheckEdit1.Checked = True Then
                candidates(TextEdit1.Text)
            Else
                Dim cmd As MySqlCommand = SQLConnection.CreateCommand()
                cmd.CommandText = "Select IdRec, InterviewTImes As AppliedTimes, FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Status, Reason, CreatedDate, ExpectedSalary from db_recruitment where CreatedDate between @date1 And @date2"
                cmd.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                cmd.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Dim dt As New DataTable
                dt.Load(cmd.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                    '.Show()
                End With
            End If
        ElseIf Label1.Text = "Employee List" Then
            If CheckEdit1.Checked = True Then
                employee(TextEdit1.Text)
            Else
                Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai where officelocation = 'KIM 3'"
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai where officelocation <> 'Pantai Labu' and officelocation != <empty>"
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai where officelocation <> 'KIM 4'  and officelocation != <empty>"
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai where officelocation <> 'KIM 3'  and officelocation != <empty>"
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai where officelocation = 'Pantai Labu'"
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai where officelocation = 'KIM 4'"
                Else
                    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position as JobTitle, WorkDate as JoinDate, Gender, Religion, PhoneNumber from db_pegawai"
                End If
                Dim dt As New DataTable
                dt.Load(sqlcommand.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                    '.Show()
                End With
            End If
        ElseIf Label1.Text = "Attendance List" Then
            If CheckEdit1.Checked = True Then
                attendance(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    'Date_Format(JamMulai, '%H:%i:%s')
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, Date_format(a.JamMulai, '%H:%i:%s') As StartingHour, date_format(a.JamSelesai, '%H:%i:%s') As EndedHour, b.OfficeLocation " +
                                                        "from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, Date_format(a.JamMulai, '%H:%i:%s') As StartingHour, date_format(a.JamSelesai, '%H:%i:%s') As EndedHour, b.OfficeLocation " +
                                                        "from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation <> 'Pantai Labu' and officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, Date_format(a.JamMulai, '%H:%i:%s') As StartingHour, date_format(a.JamSelesai, '%H:%i:%s') As EndedHour, b.OfficeLocation " +
                                                        "from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation <> 'KIM 4' and officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, Date_format(a.JamMulai, '%H:%i:%s') As StartingHour, date_format(a.JamSelesai, '%H:%i:%s') As EndedHour, b.OfficeLocation " +
                                                        "from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation <> 'KIM 3' and officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, Date_format(a.JamMulai, '%H:%i:%s') As StartingHour, date_format(a.JamSelesai, '%H:%i:%s') As EndedHour, b.OfficeLocation " +
                                                        "from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, Date_format(a.JamMulai, '%H:%i:%s') As StartingHour, date_format(a.JamSelesai, '%H:%i:%s') As EndedHour, b.OfficeLocation " +
                                                        "from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, Date_format(a.JamMulai, '%H:%i:%s') As StartingHour, date_format(a.JamSelesai, '%H:%i:%s') As EndedHour, b.OfficeLocation " +
                                                        "from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode "
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                    '.Show()
                End With
            End If
        ElseIf Label1.Text = "Overtime List" Then
            If CheckEdit1.Checked = True Then
                overtime(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, a.OvertimeType, a.OvertimeHours, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.Employeecode = b.employeecode and b.officelocation = 'KIM 3' and overtimehours <> 0"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, a.OvertimeType, a.OvertimeHours, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.Employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>' and a.overtimehours <> 0"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, a.OvertimeType, a.OvertimeHours, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.Employeecode = b.employeecode and  b.officelocation <> 'KIM 4' and b.officelocation != '<empty>' and a.overtimehours <> 0"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, a.OvertimeType, a.OvertimeHours, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.Employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>' and a.overtimehours <> 0"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, a.OvertimeType, a.OvertimeHours, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.Employeecode = b.employeecode and  b.officelocation = 'Pantai Labu' and a.overtimehours <> 0"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, a.OvertimeType, a.OvertimeHours, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.Employeecode = b.employeecode and  b.officelocation = 'KIM 4' and a.overtimehours <> 0"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Date, a.Shift, a.OvertimeType, a.OvertimeHours, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 And @date2 and a.Employeecode = b.employeecode and a.overtimehours <> 0"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Warning Notice" Then
            If CheckEdit1.Checked = True Then
                warning(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.WarningLevel, a.OffenseType, a.DescriptionOfInfraction As Description, a.Plan, a.Consequences, b.OfficeLocation from db_warning a, db_pegawai b where a.tgl between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.WarningLevel, a.OffenseType, a.DescriptionOfInfraction As Description, a.Plan, a.Consequences, b.OfficeLocation from db_warning a, db_pegawai b where a.tgl between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.WarningLevel, a.OffenseType, a.DescriptionOfInfraction As Description, a.Plan, a.Consequences, b.OfficeLocation from db_warning a, db_pegawai b where a.tgl between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.WarningLevel, a.OffenseType, a.DescriptionOfInfraction As Description, a.Plan, a.Consequences, b.OfficeLocation from db_warning a, db_pegawai b where a.tgl between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and  b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.WarningLevel, a.OffenseType, a.DescriptionOfInfraction As Description, a.Plan, a.Consequences, b.OfficeLocation from db_warning a, db_pegawai b where a.tgl between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.WarningLevel, a.OffenseType, a.DescriptionOfInfraction As Description, a.Plan, a.Consequences, b.OfficeLocation from db_warning a, db_pegawai b where a.tgl between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.WarningLevel, a.OffenseType, a.DescriptionOfInfraction As Description, a.Plan, a.Consequences, b.OfficeLocation from db_warning a, db_pegawai b where a.tgl between @date1 And @date2 and a.EmployeeCode = b.EmployeeCode"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Leave Request" Then
            If CheckEdit1.Checked = True Then
                leavereq(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username = '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.Reason, a.StartDate, a.EndDate, a.TotalDays from db_attrec a, db_pegawai b where a.tgl between @date1 And @date2 and a.Approvedstatus = 'Approved' and a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.Reason, a.StartDate, a.EndDate, a.TotalDays from db_attrec a, db_pegawai b where a.tgl between @date1 And @date2 and a.Approvedstatus = 'Approved' and a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.Reason, a.StartDate, a.EndDate, a.TotalDays from db_attrec a, db_pegawai b where a.tgl between @date1 And @date2 and a.Approvedstatus = 'Approved' and a.employeecode = b.employeecode and  b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.Reason, a.StartDate, a.EndDate, a.TotalDays from db_attrec a, db_pegawai b where a.tgl between @date1 And @date2 and a.Approvedstatus = 'Approved' and a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.Reason, a.StartDate, a.EndDate, a.TotalDays from db_attrec a, db_pegawai b where a.tgl between @date1 And @date2 and a.Approvedstatus = 'Approved' and a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.tgl As Date, a.EmployeeCode, a.FullName, a.Reason, a.StartDate, a.EndDate, a.TotalDays from db_attrec a, db_pegawai b where a.tgl between @date1 And @date2 and a.Approvedstatus = 'Approved' and a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select MemoNo, tgl As Date, EmployeeCode, FullName, Reason, StartDate, EndDate, TotalDays from db_attrec where tgl between @date1 And @date2 and Approvedstatus = 'Approved'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Others Income / Deductions" Then
            If CheckEdit1.Checked = True Then
                leavereq(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.Tanggal As Date, a.EmployeeCode, a.Period, a.Until, a.Amount, a.As1 As AsA, a.Reason from db_addition a, db_pegawai b where a.tanggal between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.Tanggal As Date, a.EmployeeCode, a.Period, a.Until, a.Amount, a.As1 As AsA, a.Reason from db_addition a, db_pegawai b where a.tanggal between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.Tanggal As Date, a.EmployeeCode, a.Period, a.Until, a.Amount, a.As1 As AsA, a.Reason from db_addition a, db_pegawai b where a.tanggal between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.Tanggal As Date, a.EmployeeCode, a.Period, a.Until, a.Amount, a.As1 As AsA, a.Reason from db_addition a, db_pegawai b where a.tanggal between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.MemoNo, a.Tanggal As Date, a.EmployeeCode, a.Period, a.Until, a.Amount, a.As1 As AsA, a.Reason from db_addition a, db_pegawai b where a.tanggal between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.MemoNo, a.Tanggal As Date, a.EmployeeCode, a.Period, a.Until, a.Amount, a.As1 As AsA, a.Reason from db_addition a, db_pegawai b where a.tanggal between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select MemoNo, Tanggal As Date, EmployeeCode, Period, Until, Amount, As1 As AsA, Reason from db_addition where tanggal between @date1 And @date2"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Loan Lists" Then
            If CheckEdit1.Checked = True Then
                loanlist(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.ApprovedBy, a.Reason, a.Dates As Date, Date_Format(a.FromMonths, '%M-%Y') as FromMonth, a.AmountOfLoan, Date_Format(a.CompletedOn, '%M-%Y') As CompletedPayment from db_loan a, db_pegawai b where a.dates between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.ApprovedBy, a.Reason, a.Dates As Date, Date_Format(a.FromMonths, '%M-%Y') as FromMonth, a.AmountOfLoan, Date_Format(a.CompletedOn, '%M-%Y') As CompletedPayment from db_loan a, db_pegawai b where a.dates between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.ApprovedBy, a.Reason, a.Dates As Date, Date_Format(a.FromMonths, '%M-%Y') as FromMonth, a.AmountOfLoan, Date_Format(a.CompletedOn, '%M-%Y') As CompletedPayment from db_loan a, db_pegawai b where a.dates between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.ApprovedBy, a.Reason, a.Dates As Date, Date_Format(a.FromMonths, '%M-%Y') as FromMonth, a.AmountOfLoan, Date_Format(a.CompletedOn, '%M-%Y') As CompletedPayment from db_loan a, db_pegawai b where a.dates between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.ApprovedBy, a.Reason, a.Dates As Date, Date_Format(a.FromMonths, '%M-%Y') as FromMonth, a.AmountOfLoan, Date_Format(a.CompletedOn, '%M-%Y') As CompletedPayment from db_loan a, db_pegawai b where a.dates between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.ApprovedBy, a.Reason, a.Dates As Date, Date_Format(a.FromMonths, '%M-%Y') as FromMonth, a.AmountOfLoan, Date_Format(a.CompletedOn, '%M-%Y') As CompletedPayment from db_loan a, db_pegawai b where a.dates between @date1 And @date2 and a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select EmployeeCode, FullName, ApprovedBy, Reason, Dates As Date, Date_Format(FromMonths, '%M-%Y') as FromMonth, AmountOfLoan, Date_Format(CompletedOn, '%M-%Y') As CompletedPayment from db_loan where dates between @date1 And @date2"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Loan Summary" Then
            If CheckEdit1.Checked = True Then
                loansummary(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.AmountOfLoan, Date_Format(a.Monthx, '%M-%Y') as Month, a.Realisasi b.OfficeLocation from db_loanlist a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.AmountOfLoan, Date_Format(a.Monthx, '%M-%Y') as Month, a.Realisasi b.OfficeLocation from db_loanlist a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.AmountOfLoan, Date_Format(a.Monthx, '%M-%Y') as Month, a.Realisasi b.OfficeLocation from db_loanlist a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.AmountOfLoan, Date_Format(a.Monthx, '%M-%Y') as Month, a.Realisasi b.OfficeLocation from db_loanlist a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.AmountOfLoan, Date_Format(a.Monthx, '%M-%Y') as Month, a.Realisasi b.OfficeLocation from db_loanlist a, db_pegawai b where a.employeecode = b.employeecode and a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.AmountOfLoan, Date_Format(a.Monthx, '%M-%Y') as Month, a.Realisasi b.OfficeLocation from db_loanlist a, db_pegawai b where a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
                Else
                    query.CommandText = "Select EmployeeCode, FullName, AmountOfLoan, Date_Format(Monthx, '%M-%Y') as Month, Realisasi from db_loanlist"
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Payroll Sheet" Then
            If CheckEdit1.Checked = True Then
                payroll(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.Employee_type as EmployeeType, b.OfficeLocation from payroll2 a, db_pegawai b where a.pay_date between @date1 And @date2 and a.employee_code = b.EmployeeCode and b.OfficeLocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.Employee_type as EmployeeType, b.OfficeLocation from payroll2 a, db_pegawai b where a.pay_date between @date1 And @date2 and a.employee_code = b.EmployeeCode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.Employee_type as EmployeeType, b.OfficeLocation from payroll2 a, db_pegawai b where a.pay_date between @date1 And @date2 and a.employee_code = b.EmployeeCode and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.Employee_type as EmployeeType, b.OfficeLocation from payroll2 a, db_pegawai b where a.pay_date between @date1 And @date2 and a.employee_code = b.EmployeeCode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Selecta.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.Employee_type as EmployeeType, b.OfficeLocation from payroll2 a, db_pegawai b where a.pay_date between @date1 And @date2 and a.employee_code = b.EmployeeCode and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Selecta.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.Employee_type as EmployeeType, b.OfficeLocation from payroll2 a, db_pegawai b where a.pay_date between @date1 And @date2 and a.employee_code = b.EmployeeCode and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select a.pay_date as PayDate, a.employee_code as EmployeeCode, b.FullName, a.salary_component as SalaryComponent, a.salary_value as SalaryValue, a.Employee_type as EmployeeType from payroll2 a, db_pegawai b where a.pay_date between @date1 And @date2 and a.employee_code = b.EmployeeCode"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Premi" Then
            If CheckEdit1.Checked = True Then
                premi(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.CompanyCode, a.FullName, a.Tanggal As Date, a.Tasks, a.Machines, a.Quantity, b.OfficeLocation from db_borongan a, db_pegawai b where a.tanggal between @date1 And @date2 and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.CompanyCode, a.FullName, a.Tanggal As Date, a.Tasks, a.Machines, a.Quantity, b.OfficeLocation from db_borongan a, db_pegawai b where a.tanggal between @date1 And @date2 and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.CompanyCode, a.FullName, a.Tanggal As Date, a.Tasks, a.Machines, a.Quantity, b.OfficeLocation from db_borongan a, db_pegawai b where a.tanggal between @date1 And @date2 and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.CompanyCode, a.FullName, a.Tanggal As Date, a.Tasks, a.Machines, a.Quantity, b.OfficeLocation from db_borongan a, db_pegawai b where a.tanggal between @date1 And @date2 and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.CompanyCode, a.FullName, a.Tanggal As Date, a.Tasks, a.Machines, a.Quantity, b.OfficeLocation from db_borongan a, db_pegawai b where a.tanggal between @date1 And @date2 and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.CompanyCode, a.FullName, a.Tanggal As Date, a.Tasks, a.Machines, a.Quantity, b.OfficeLocation from db_borongan a, db_pegawai b where a.tanggal between @date1 And @date2 and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select EmployeeCode, CompanyCode, FullName, Tanggal As Date, Tasks, Machines, Quantity from db_borongan where tanggal between @date1 And @date2"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Lates Or Early Sign In/Out" Then
            If CheckEdit1.Checked = True Then
                latesin(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.Tanggal as Dates, a.Shift, timediff(Date_Format(a.JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(a.JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 and @date2 and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.Tanggal as Dates, a.Shift, timediff(Date_Format(a.JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(a.JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 and @date2 and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.Tanggal as Dates, a.Shift, timediff(Date_Format(a.JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(a.JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 and @date2 and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.Tanggal as Dates, a.Shift, timediff(Date_Format(a.JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(a.JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 and @date2 and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.Tanggal as Dates, a.Shift, timediff(Date_Format(a.JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(a.JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 and @date2 and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.Tanggal as Dates, a.Shift, timediff(Date_Format(a.JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(a.JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut, b.OfficeLocation from db_absensi a, db_pegawai b where a.tanggal between @date1 and @date2 and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "select EmployeeCode, FullName, Tanggal as Dates, Shift, timediff(Date_Format(JamMulai, '%H:%i:%s'), '08:00:00') as LateSignIn, timediff(Date_Format(JamSelesai, '%H:%i:%s'), '17:00:00') as LateSignOut from db_absensi where tanggal between @date1 and @date2"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Absences List" Then
            If CheckEdit1.Checked = True Then
                absence(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Dates, a.Shift, a.JamMulai as StartingHour, a.JamSelesai as EndedHour, b.OfficeLocation from db_absensi a, db_pegawai b where a.JamMulai is null and a.JamSelesai is null and a.tanggal between @date1 and @date2 and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Dates, a.Shift, a.JamMulai as StartingHour, a.JamSelesai as EndedHour, b.OfficeLocation from db_absensi a, db_pegawai b where a.JamMulai is null and a.JamSelesai is null and a.tanggal between @date1 and @date2 and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Dates, a.Shift, a.JamMulai as StartingHour, a.JamSelesai as EndedHour, b.OfficeLocation from db_absensi a, db_pegawai b where a.JamMulai is null and a.JamSelesai is null and a.tanggal between @date1 and @date2 and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Dates, a.Shift, a.JamMulai as StartingHour, a.JamSelesai as EndedHour, b.OfficeLocation from db_absensi a, db_pegawai b where a.JamMulai is null and a.JamSelesai is null and a.tanggal between @date1 and @date2 and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Dates, a.Shift, a.JamMulai as StartingHour, a.JamSelesai as EndedHour, b.OfficeLocation from db_absensi a, db_pegawai b where a.JamMulai is null and a.JamSelesai is null and a.tanggal between @date1 and @date2 and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "Select a.EmployeeCode, a.FullName, a.Tanggal As Dates, a.Shift, a.JamMulai as StartingHour, a.JamSelesai as EndedHour, b.OfficeLocation from db_absensi a, db_pegawai b where a.JamMulai is null and a.JamSelesai is null and a.tanggal between @date1 and @date2 and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "Select EmployeeCode, FullName, Tanggal As Dates, Shift, JamMulai as StartingHour, JamSelesai as EndedHour from db_absensi where JamMulai is null and JamSelesai is null and tanggal between @date1 and @date2"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Terminate Lists" Then
            If CheckEdit1.Checked = True Then
                terminate(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "select a.MemoNo, a.FullName, a.EmployeeCode, a.tgl as Dates, a.ApprovedBy, a.Reason, a.JobTitle, a.Status, a.Explanation, b.OfficeLocation from db_terminate a, db_pegawai b where a.tgl between @date1 and @date2 and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.MemoNo, a.FullName, a.EmployeeCode, a.tgl as Dates, a.ApprovedBy, a.Reason, a.JobTitle, a.Status, a.Explanation, b.OfficeLocation from db_terminate a, db_pegawai b where a.tgl between @date1 and @date2 and b.officelocation <> 'Pantai Labu' and b.OfficeLocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.MemoNo, a.FullName, a.EmployeeCode, a.tgl as Dates, a.ApprovedBy, a.Reason, a.JobTitle, a.Status, a.Explanation, b.OfficeLocation from db_terminate a, db_pegawai b where a.tgl between @date1 and @date2 and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "select a.MemoNo, a.FullName, a.EmployeeCode, a.tgl as Dates, a.ApprovedBy, a.Reason, a.JobTitle, a.Status, a.Explanation, b.OfficeLocation from db_terminate a, db_pegawai b where a.tgl between @date1 and @date2 and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.MemoNo, a.FullName, a.EmployeeCode, a.tgl as Dates, a.ApprovedBy, a.Reason, a.JobTitle, a.Status, a.Explanation, b.OfficeLocation from db_terminate a, db_pegawai b where a.tgl between @date1 and @date2 and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.MemoNo, a.FullName, a.EmployeeCode, a.tgl as Dates, a.ApprovedBy, a.Reason, a.JobTitle, a.Status, a.Explanation, b.OfficeLocation from db_terminate a, db_pegawai b where a.tgl between @date1 and @date2 and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "select MemoNo, FullName, EmployeeCode, tgl as Dates, ApprovedBy, Reason, JobTitle, Status, Explanation from db_terminate where tgl between @date1 and @date2"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Pajak" Then
            If CheckEdit1.Checked = True Then
                pajak(TextEdit1.Text)
            Else
                Dim cmd As MySqlCommand = SQLConnection.CreateCommand()
                cmd.CommandText = "select employeecode from db_payrolldata"
                Dim adp As New MySqlDataAdapter(cmd)
                Dim ds As New DataSet
                adp.Fill(ds)
                For Each tbl As DataTable In ds.Tables
                    For Each row As DataRow In tbl.Rows
                        pajak(CType(row.Item("EmployeeCode"), String))
                    Next
                Next
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "Select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk As JaminanKecelakaanKerja, a.Jk As JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT As JaminanHariTua, a.JaminanPensiun, a.NettoBulan As NettoPerBulan, a.NettoTahun As NettoPerTahun, a.Ptkp, a.pphbulan As PphPerBulan from db_hasil a, db_payrolldata b, db_pegawai c where a.EmployeeCode = b.EmployeeCode And a.EmployeeCode = c.EmployeeCode And c.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk as JaminanKecelakaanKerja, a.Jk as JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT as JaminanHariTua, a.JaminanPensiun, a.NettoBulan as NettoPerBulan, a.NettoTahun as NettoPerTahun, a.Ptkp, a.pphbulan as PphPerBulan from db_hasil a, db_payrolldata b, db_pegawai c where a.EmployeeCode = b.EmployeeCode and a.EmployeeCode = c.EmployeeCode and c.officelocation <> 'Pantai Labu' and c.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk as JaminanKecelakaanKerja, a.Jk as JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT as JaminanHariTua, a.JaminanPensiun, a.NettoBulan as NettoPerBulan, a.NettoTahun as NettoPerTahun, a.Ptkp, a.pphbulan as PphPerBulan from db_hasil a, db_payrolldata b, db_pegawai c where a.EmployeeCode = b.EmployeeCode and a.EmployeeCode = c.EmployeeCode and c.officelocation <> 'KIM 4' and c.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk as JaminanKecelakaanKerja, a.Jk as JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT as JaminanHariTua, a.JaminanPensiun, a.NettoBulan as NettoPerBulan, a.NettoTahun as NettoPerTahun, a.Ptkp, a.pphbulan as PphPerBulan from db_hasil a, db_payrolldata b, db_pegawai c where a.EmployeeCode = b.EmployeeCode and a.EmployeeCode = c.EmployeeCode and c.officelocation <> 'KIM 3' and c.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk as JaminanKecelakaanKerja, a.Jk as JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT as JaminanHariTua, a.JaminanPensiun, a.NettoBulan as NettoPerBulan, a.NettoTahun as NettoPerTahun, a.Ptkp, a.pphbulan as PphPerBulan from db_hasil a, db_payrolldata b, db_pegawai c where a.EmployeeCode = b.EmployeeCode and a.EmployeeCode = c.EmployeeCode and c.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk as JaminanKecelakaanKerja, a.Jk as JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT as JaminanHariTua, a.JaminanPensiun, a.NettoBulan as NettoPerBulan, a.NettoTahun as NettoPerTahun, a.Ptkp, a.pphbulan as PphPerBulan from db_hasil a, db_payrolldata b, db_pegawai c where a.EmployeeCode = b.EmployeeCode and a.EmployeeCode = c.EmployeeCode and c.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "select distinct a.EmployeeCode, b.FullName, a.GajiPokok, a.TunjanganLain, a.Jkk as JaminanKecelakaanKerja, a.Jk as JaminanKematian, a.Bruto, a.BiayaJabatan, a.JHT as JaminanHariTua, a.JaminanPensiun, a.NettoBulan as NettoPerBulan, a.NettoTahun as NettoPerTahun, a.Ptkp, a.pphbulan as PphPerBulan from db_hasil a, db_payrolldata b where a.EmployeeCode = b.EmployeeCode"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                With Form2
                    .GridView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Status Change" Then
            If CheckEdit1.Checked = True Then
                statuschange(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select user from db_temp"
                Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.tgl as ChangeDate, a.ChangeType, a.JobTitle, a.OfficeLocation, a.Department, a.Grouping as Groups, a.ApprovedBy, b.OfficeLocation from db_statuschange a, db_pegawai b where a.status = 'Approved' and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.tgl as ChangeDate, a.ChangeType, a.JobTitle, a.OfficeLocation, a.Department, a.Grouping as Groups, a.ApprovedBy, b.OfficeLocation from db_statuschange a, db_pegawai b where a.status = 'Approved' and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.tgl as ChangeDate, a.ChangeType, a.JobTitle, a.OfficeLocation, a.Department, a.Grouping as Groups, a.ApprovedBy, b.OfficeLocation from db_statuschange a, db_pegawai b where a.status = 'Approved' and b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.tgl as ChangeDate, a.ChangeType, a.JobTitle, a.OfficeLocation, a.Department, a.Grouping as Groups, a.ApprovedBy, b.OfficeLocation from db_statuschange a, db_pegawai b where a.status = 'Approved' and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.tgl as ChangeDate, a.ChangeType, a.JobTitle, a.OfficeLocation, a.Department, a.Grouping as Groups, a.ApprovedBy, b.OfficeLocation from db_statuschange a, db_pegawai b where a.status = 'Approved' and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.EmployeeCode, a.FullName, a.tgl as ChangeDate, a.ChangeType, a.JobTitle, a.OfficeLocation, a.Department, a.Grouping as Groups, a.ApprovedBy, b.OfficeLocation from db_statuschange a, db_pegawai b where a.status = 'Approved' and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "select EmployeeCode, FullName, tgl as ChangeDate, ChangeType, JobTitle, OfficeLocation, Department, Grouping as Groups, ApprovedBy from db_statuschange where status = 'Approved'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                'With Reports
                '    .GridControl1.DataSource = dt
                '    .GridControl1.UseEmbeddedNavigator = True
                '    .Label3.Text = Label2.Text
                '    .LabelControl2.Text = Label1.Text
                '    '.Show()
                'End With
                With Form2
                    .GridView1.Columns.Clear()
                    '.CardView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        ElseIf Label1.Text = "Holiday Lists" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select tgl as Dates, Reason from db_holiday where tgl between @date1 and @date2"
            query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
            query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            With Form2
                .GridView1.Columns.Clear()
                .LayoutView1.Columns.Clear()
                .GridControl1.DataSource = dt
                .GridControl2.DataSource = dt
                .GridView1.BestFitColumns()
                .GridControl2.UseEmbeddedNavigator = True
                .GridControl1.UseEmbeddedNavigator = True
                .Label3.Text = Label2.Text
                .LabelControl2.Text = Label1.Text
            End With
        ElseIf Label1.Text = "Surat Dinas" Then
            If CheckEdit1.Checked = True Then
                dinas(TextEdit1.Text)
            Else
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select kim3 from db_user where username = '" & Label2.Text & "'"
                Dim kim3 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & Label2.Text & "'"
                Dim kim4 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & Label2.Text & "'"
                Dim plabu As String = CStr(query.ExecuteScalar)
                If kim3 = "True" And kim4 = "False" And plabu = "False" Then
                    query.CommandText = "select a.MemoNo, a.EmployeeCode, a.FullName, a.IssuesDates, a.Department, a.FromDates, a.ToDates, a.DestinationCity, a.Agenda, a.ApprovedBy, a.ApprovedStatus from db_sppd a, db_pegawai b where a.issuesdates between @date1 and @date2 and a.employeecode = b.employeecode and b.officelocation = 'KIM 3'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.MemoNo, a.EmployeeCode, a.FullName, a.IssuesDates, a.Department, a.FromDates, a.ToDates, a.DestinationCity, a.Agenda, a.ApprovedBy, a.ApprovedStatus from db_sppd a, db_pegawai b where a.issuesdates between @date1 and @date2 and a.employeecode = b.employeecode and b.officelocation <> 'Pantai Labu' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "True" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.MemoNo, a.EmployeeCode, a.FullName, a.IssuesDates, a.Department, a.FromDates, a.ToDates, a.DestinationCity, a.Agenda, a.ApprovedBy, a.ApprovedStatus from db_sppd a, db_pegawai b where a.issuesdates between @date1 and @date2 and a.employeecode = b.employeecode and  b.officelocation <> 'KIM 4' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "True" Then
                    query.CommandText = "select a.MemoNo, a.EmployeeCode, a.FullName, a.IssuesDates, a.Department, a.FromDates, a.ToDates, a.DestinationCity, a.Agenda, a.ApprovedBy, a.ApprovedStatus from db_sppd a, db_pegawai b where a.issuesdates between @date1 and @date2 and a.employeecode = b.employeecode and b.officelocation <> 'KIM 3' and b.officelocation != '<empty>'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "False" And plabu = "True" Then
                    query.CommandText = "select a.MemoNo, a.EmployeeCode, a.FullName, a.IssuesDates, a.Department, a.FromDates, a.ToDates, a.DestinationCity, a.Agenda, a.ApprovedBy, a.ApprovedStatus from db_sppd a, db_pegawai b where a.issuesdates between @date1 and @date2 and a.employeecode = b.employeecode and b.officelocation = 'Pantai Labu'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                ElseIf kim3 = "False" And kim4 = "True" And plabu = "False" Then
                    query.CommandText = "select a.MemoNo, a.EmployeeCode, a.FullName, a.IssuesDates, a.Department, a.FromDates, a.ToDates, a.DestinationCity, a.Agenda, a.ApprovedBy, a.ApprovedStatus from db_sppd a, db_pegawai b where a.issuesdates between @date1 and @date2 and a.employeecode = b.employeecode and b.officelocation = 'KIM 4'"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                Else
                    query.CommandText = "select a.MemoNo, a.EmployeeCode, a.FullName, a.IssuesDates, a.Department, a.FromDates, a.ToDates, a.DestinationCity, a.Agenda, a.ApprovedBy, a.ApprovedStatus from db_sppd a, db_pegawai b where a.issuesdates between @date1 and @date2 and a.employeecode = b.employeecode"
                    query.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
                    query.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
                End If
                Dim dt As New DataTable
                dt.Load(query.ExecuteReader)
                With Form2
                    .GridView1.Columns.Clear()
                    .LayoutView1.Columns.Clear()
                    .GridControl1.DataSource = dt
                    .GridControl2.DataSource = dt
                    .GridView1.BestFitColumns()
                    .GridControl2.UseEmbeddedNavigator = True
                    .GridControl1.UseEmbeddedNavigator = True
                    .Label3.Text = Label2.Text
                    .LabelControl2.Text = Label1.Text
                End With
            End If
        End If
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Timer3.Start()
       Timer3.Enabled = True
        'proceed()
        'Close()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        ProgressBar1.Visible = True
        ProgressPanel1.Visible = True
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 10
        ElseIf ProgressBar1.Value = 100 Then
            Timer1.Stop()
        End If
        If ProgressBar1.Value = 40 Then
            proceed()
        ElseIf ProgressBar1.Value = 100 Then
            'Reports.Show()
            Form2.Show()
            Close()
        End If
    End Sub
End Class