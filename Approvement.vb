Imports System.IO
Imports DevExpress.Utils.Menu

Public Class Approvement
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

    Private Sub Approvement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        autho()
        SimpleButton5.Enabled = False
        SimpleButton7.Enabled = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        Timer1.Stop()
    End Sub

    Sub autho()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & Label2.Text & "'"
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        For index As Integer = 0 To tab.Rows.Count - 1
            If tab.Rows(index).Item(22).ToString = "True" Then
                LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If tab.Rows(index).Item(23).ToString = "True" Then
                LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If tab.Rows(index).Item(24).ToString = "True" Then
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If tab.Rows(index).Item(25).ToString = "True" Then
                LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item(26).ToString = "True" Then
                LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If tab.Rows(index).Item("sppdapp").ToString = "True" Then
                LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Next
    End Sub

    Dim sel As New selectemp

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs)
        Timer1.Start()
        If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
            sel.Close()
            sel = New selectemp
        End If
        sel.Show()
    End Sub

    Sub sc()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, EmployeeCode, FullName, tgl as Dates, ChangeType, JobTitle, OfficeLocation, Department, Grouping, ApprovedBy, Status from db_statuschange where employeecode = '" & ComboBoxEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
        GridView1.MoveLastVisible()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        LabelControl5.Text = "Status Change"
        empcode()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_statuschange where status = 'Requested'"
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If quer = 0 Then
            SimpleButton7.Enabled = False
            SimpleButton5.Enabled = False
        Else
            SimpleButton7.Enabled = True
            SimpleButton5.Enabled = True
        End If
    End Sub

    Sub terminate()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, EmployeeCode, FullName, tgl as Dates, JobTitle, Status, Allowance, AllowanceTax, PaymentDate, Reason, ApprovedBy, ApprovedStatus from db_terminate where employeecode = '" & ComboBoxEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
        GridView1.MoveLastVisible()
    End Sub

    Sub leaves()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, EmployeeCode, FullName, tgl as Dates, Reason, StartDate, EndDate, TotalDays, Ket as Reason, ApprovedBy, ApprovedStatus from db_attrec where employeecode = '" & ComboBoxEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
        GridView1.MoveLastVisible()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        LabelControl5.Text = "Termination"
        empcode()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_terminate where approvedstatus = 'Requested'"
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If quer = 0 Then
            SimpleButton5.Enabled = False
            SimpleButton7.Enabled = False
        Else
            SimpleButton5.Enabled = True
            SimpleButton7.Enabled = True
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        LabelControl5.Text = "Leave Request"
        empcode()
        'TextBox3.Text = ""
        'TextBox4.Text = ""
        ''GridView1.Columns.Clear()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_attrec where approvedstatus = 'Requested'"
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If quer = 0 Then
            SimpleButton5.Enabled = False
            SimpleButton7.Enabled = False
        Else
            SimpleButton5.Enabled = True
            SimpleButton7.Enabled = True
        End If
        'leaves()
    End Sub

    Sub updatest2()
        Dim tbl_par As New DataTable
        Dim sqlCommand As MySqlCommand = SQLConnection.CreateCommand
        sqlCommand.CommandText = "Select * from db_statuschange where employeecode = '" & TextBox3.Text & "'"
        Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par)
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
            cmmd.CommandText = "update db_pegawai set Grouping = @grp, Department = @dept, Position = @pos, officelocation = @ofloc " +
                                                ", companycode = @cc, maritalstatus = @maritalstatus, pphstatus = @pphstatus, status = @jobstatus,  excludepayroll = @excludepayroll " +
                                                ", paycash = @paycash, processtax = @processtax, excludethr = @excludethr,  excludebonus = @excludebonus, printslip = @printslip " +
                                                ",  payrollinterval = @payrollinterval where EmployeeCode = @ec"
            cmmd.Parameters.AddWithValue("@grp", tbl_par.Rows(index).Item(8).ToString)
            cmmd.Parameters.AddWithValue("@dept", tbl_par.Rows(index).Item(7).ToString)
            cmmd.Parameters.AddWithValue("@pos", tbl_par.Rows(index).Item(5).ToString)
            cmmd.Parameters.AddWithValue("@ec", TextBox3.Text)
            cmmd.Parameters.AddWithValue("@ofloc", tbl_par.Rows(index).Item(6).ToString)
            cmmd.Parameters.AddWithValue("@cc", tbl_par.Rows(index).Item(12).ToString)
            cmmd.Parameters.AddWithValue("@maritalstatus", tbl_par.Rows(index).Item(13).ToString)
            cmmd.Parameters.AddWithValue("@pphstatus", tbl_par.Rows(index).Item(14).ToString)
            cmmd.Parameters.AddWithValue("@jobstatus", tbl_par.Rows(index).Item(15).ToString)
            cmmd.Parameters.AddWithValue("@excludepayroll", tbl_par.Rows(index).Item(16).ToString)
            cmmd.Parameters.AddWithValue("@paycash", tbl_par.Rows(index).Item(17).ToString)
            cmmd.Parameters.AddWithValue("@processtax", tbl_par.Rows(index).Item(18).ToString)
            cmmd.Parameters.AddWithValue("@excludethr", tbl_par.Rows(index).Item(19).ToString)
            cmmd.Parameters.AddWithValue("@excludebonus", tbl_par.Rows(index).Item(20).ToString)
            cmmd.Parameters.AddWithValue("@printslip", tbl_par.Rows(index).Item(21).ToString)
            cmmd.Parameters.AddWithValue("@payrollinterval", tbl_par.Rows(index).Item(22).ToString)
            cmmd.ExecuteNonQuery()
        Next
    End Sub

    Sub updateterminate()
        Dim cmd As MySqlCommand = SQLConnection.CreateCommand
        cmd.CommandText = "update db_pegawai set status = 'Terminated' where employeecode = '" & TextBox3.Text & "'"
        cmd.ExecuteNonQuery()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select idnumber from db_pegawai where employeecode = '" & TextBox3.Text & "'"
        Dim hsl As String = CStr(query.ExecuteScalar)
        query.CommandText = "select blacklist from db_terminate where employeecode = '" & TextBox3.Text & "'"
        Dim bl As Integer = CInt(query.ExecuteScalar)
        query.CommandText = "update db_recruitment set blacklist = @check where idnumber = @id"
        query.Parameters.AddWithValue("@id", hsl.ToString)
        query.Parameters.AddWithValue("@check", bl)
        query.ExecuteNonQuery()
    End Sub

    Sub approvement()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        Dim fn As String
        fn = InputBox("Reason")
        If LabelControl5.Text = "Status Change" Then
            updatest2()
            query.CommandText = "update db_statuschange set status = 'Approved', reason = @reason, ApprovedBy = @app where MemoNo = '" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@reason", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Approved")
            sc()
        ElseIf LabelControl5.Text = "Termination" Then
            query.CommandText = "update db_terminate set ApprovedStatus = 'Approved', purpose = @purpose,  ApprovedBy = @app where MemoNo = '" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@purpose", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Approved")
            updateterminate()
            terminate()
        ElseIf LabelControl5.Text = "Leave Request" Then
            query.CommandText = "update db_attrec set ApprovedStatus = 'Approved', purpose = @purpose,  ApprovedBy = @app where MemoNo ='" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@purpose", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Approved")
            leaves()
        ElseIf LabelControl5.Text = "Loans" Then
            query.CommandText = "update db_loan set status = 'Approved',  reason = @purpose, ApprovedBy = @app where EmployeeCode = '" & ComboBoxEdit1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@purpose", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Approved")
            loans()
        ElseIf LabelControl5.Text = "Other Income" Then
            query.CommandText = "update db_addition set status = 'Approved', purpose = @purpose, ApprovedBy = @app where MemoNo = '" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@purpose", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Approved")
            others()
        End If
    End Sub

    Sub rejection()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select user from db_temp"
        Dim quer As String = CStr(query.ExecuteScalar)
        Dim fn As String
        fn = InputBox("Reason")
        If LabelControl5.Text = "Status Change" Then
            query.CommandText = "update db_statuschange set status = 'Rejected', Reason = @reason, ApprovedBy = @app where MemoNo = '" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@reason", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Rejected")
            sc()
        ElseIf LabelControl5.Text = "Termination" Then
            query.CommandText = "update db_terminate set ApprovedStatus = 'Rejected', Purpose = @reason, ApprovedBy = @app where MemoNo = '" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@reason", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Rejected")
            terminate()
        ElseIf LabelControl5.Text = "Leave Request" Then
            query.CommandText = "update db_attrec set ApprovedStatus = 'Rejected', Purpose = @reason, ApprovedBy = @app where MemoNo ='" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@reason", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Rejected")
            leaves()
        ElseIf LabelControl5.Text = "Loans" Then
            query.CommandText = "update db_loan set status = 'Rejected', Reason = @purpose, ApprovedBy = @app where EmployeeCode = '" & ComboBoxEdit1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@purpose", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Rejected")
            loans()
        ElseIf LabelControl5.Text = "Other Income" Then
            query.CommandText = "update db_addition set status = 'Rejected', purpose = @purpose, ApprovedBy = @app where MemoNo = '" & Label1.Text & "'"
            query.Parameters.AddWithValue("@app", Label2.Text)
            query.Parameters.AddWithValue("@purpose", fn.ToString)
            query.ExecuteNonQuery()
            MsgBox("Rejected")
            others()
        End If
    End Sub

    Sub trick()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()
        query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name, Initial)" +
                            "values (@employeecode, @Name, @initial)"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@EmployeeCode", Label1.Text)
        query.Parameters.AddWithValue("@Name", TextBox4.Text)
        query.Parameters.AddWithValue("@initial", "1")
        query.ExecuteNonQuery()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            If LabelControl5.Text <> "Loans" Then
                param = "And MemoNo='" + GridView1.GetFocusedRowCellValue("MemoNo").ToString() + "'"
            Else
                param = "And EmployeeCode = '" + GridView1.GetFocusedRowCellValue("EmployeeCode").ToString() + "'"
            End If
        Catch ex As Exception
        End Try
        Try
            If LabelControl5.Text = "Status Change" Then
                sqlCommand.CommandText = "SELECT * FROM db_statuschange WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
                If datatabl.Rows.Count > 0 Then
                    Label1.Text = datatabl.Rows(0).Item(0).ToString
                    TextBox4.Text = datatabl.Rows(0).Item(1).ToString
                    TextBox3.Text = datatabl.Rows(0).Item(2).ToString
                End If
            ElseIf LabelControl5.Text = "Termination" Then
                sqlCommand.CommandText = "SELECT * FROM db_terminate WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
                If datatabl.Rows.Count > 0 Then
                    Label1.Text = datatabl.Rows(0).Item(0).ToString
                    TextBox3.Text = datatabl.Rows(0).Item(3).ToString
                    TextBox4.Text = datatabl.Rows(0).Item(2).ToString
                End If
            ElseIf LabelControl5.Text = "Leave Request" Then
                sqlCommand.CommandText = "SELECT * FROM db_attrec WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
                If datatabl.Rows.Count > 0 Then
                    Label1.Text = datatabl.Rows(0).Item(0).ToString
                    TextBox3.Text = datatabl.Rows(0).Item(3).ToString
                    TextBox4.Text = datatabl.Rows(0).Item(2).ToString
                End If
            ElseIf LabelControl5.Text = "Loans" Then
                sqlCommand.CommandText = "SELECT * FROM db_loan WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
                If datatabl.Rows.Count > 0 Then
                    TextBox4.Text = datatabl.Rows(0).Item(0).ToString
                    TextBox3.Text = datatabl.Rows(0).Item(1).ToString
                End If
            ElseIf LabelControl5.Text = "Other Income" Then
                sqlCommand.CommandText = "SELECT a.MemoNo, a.EmployeeCode, b.FullName FROM db_addition a, db_pegawai b WHERE a.EmployeeCode = b.EmployeeCode " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
                If datatabl.Rows.Count > 0 Then
                    Label1.Text = datatabl.Rows(0).Item(0).ToString
                    TextBox3.Text = datatabl.Rows(0).Item(1).ToString
                    TextBox4.Text = datatabl.Rows(0).Item(2).ToString
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        approvement()
        Close()
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        'If TextBox1.Text = "" OrElse TextBox2.Text = "" Then
        '    MsgBox("Fill Approvement field")
        'Else
        rejection()
        '    TextBox1.Text = ""
        '    TextBox2.Text = ""
        'End If
    End Sub

    Dim sch As New StatusChange
    Dim tmnt As New Termination
    Dim leavex As New LeaveRequest

    Sub filter()
        If LabelControl5.Text = "Status Change" Then
            If sch Is Nothing OrElse sch.IsDisposed OrElse sch.MinimizeBox Then
                sch.Close()
                sch = New StatusChange
            End If
            sch.Show()
        ElseIf LabelControl5.Text = "Termination" Then
            If tmnt Is Nothing OrElse tmnt.IsDisposed OrElse tmnt.MinimizeBox Then
                tmnt.Close()
                tmnt = New Termination
            End If
            tmnt.Show()
        ElseIf LabelControl5.Text = "Leave Request" Then
            If leavex Is Nothing OrElse leavex.IsDisposed OrElse leavex.MinimizeBox Then
                leavex.Close()
                leavex = New LeaveRequest
            End If
            leavex.Show()
        ElseIf LabelControl5.Text = "Loans" Then
        Else
        End If
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs)
        trick()
        filter()
    End Sub

    Sub loans()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select FullName, EmployeeCode, Dates, AmountOfLoan, FromMonths, PaymentPerMonth, CompletedOn, ApprovedBy, Status From db_loan where employeecode = '" & ComboBoxEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
        GridView1.MoveLastVisible()
    End Sub

    Sub others()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select a.MemoNo, a.EmployeeCode, b.FullName, a.Tanggal as Dates, a.Period, a.Until, a.Amount, as1 as Sebagai, a.Reason, a.ApprovedBy, a.Status  from db_addition a, db_pegawai b where a.EmployeeCode = b.EmployeeCode and a.employeecode = '" & ComboBoxEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
        GridView1.MoveLastVisible()
    End Sub

    Dim tab, tab1, tab2, tab3, tab4, tab5 As New DataTable

    Sub empcode()
        Select Case (LabelControl5.Text)
            Case "Loans"
                tab1.Clear()
                ComboBoxEdit1.Properties.Items.Clear()
                ComboBoxEdit2.Properties.Items.Clear()
                ComboBoxEdit1.Text = ""
                ComboBoxEdit2.Text = ""
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select employeecode, fullname from db_loan where status = 'Requested'"
                Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
                'Dim tab As New DataTable
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tab1)
                For index As Integer = 0 To tab1.Rows.Count - 1
                    ComboBoxEdit1.Properties.Items.Add(tab1.Rows(index).Item(0).ToString)
                    ComboBoxEdit2.Properties.Items.Add(tab1.Rows(index).Item(1).ToString)
                Next
            Case "Other Income"
                tab2.Clear()
                ComboBoxEdit1.Properties.Items.Clear()
                ComboBoxEdit2.Properties.Items.Clear()
                ComboBoxEdit1.Text = ""
                ComboBoxEdit2.Text = ""
                Label1.Text = ""
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select a.employeecode, b.Fullname, a.MemoNo from db_addition a, db_pegawai b where a.employeecode = b.employeecode and  a.status = 'Requested'"
                Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
                'Dim tab As New DataTable
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tab2)
                For index As Integer = 0 To tab2.Rows.Count - 1
                    ComboBoxEdit1.Properties.Items.Add(tab2.Rows(index).Item(0).ToString)
                    ComboBoxEdit2.Properties.Items.Add(tab2.Rows(index).Item(1).ToString)
                Next
            Case "Termination"
                tab3.Clear()
                ComboBoxEdit1.Properties.Items.Clear()
                ComboBoxEdit2.Properties.Items.Clear()
                ComboBoxEdit1.Text = ""
                ComboBoxEdit2.Text = ""
                Label1.Text = ""
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select employeecode, fullname, MemoNo from db_terminate where approvedstatus = 'Requested'"
                Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
                'Dim tab As New DataTable
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tab3)
                For index As Integer = 0 To tab3.Rows.Count - 1
                    ComboBoxEdit1.Properties.Items.Add(tab3.Rows(index).Item(0).ToString)
                    ComboBoxEdit2.Properties.Items.Add(tab3.Rows(index).Item(1).ToString)
                Next
            Case "Status Change"
                tab4.Clear()
                ComboBoxEdit1.Properties.Items.Clear()
                ComboBoxEdit2.Properties.Items.Clear()
                ComboBoxEdit1.Text = ""
                ComboBoxEdit2.Text = ""
                Label1.Text = ""
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select employeecode, fullname, MemoNo from db_statuschange where status = 'Requested'"
                Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
                'Dim tab As New DataTable
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tab4)
                For index As Integer = 0 To tab4.Rows.Count - 1
                    ComboBoxEdit1.Properties.Items.Add(tab4.Rows(index).Item(0).ToString)
                    ComboBoxEdit2.Properties.Items.Add(tab4.Rows(index).Item(1).ToString)
                Next
            Case "Leave Request"
                tab.Clear()
                ComboBoxEdit1.Properties.Items.Clear()
                ComboBoxEdit2.Properties.Items.Clear()
                ComboBoxEdit1.Text = ""
                ComboBoxEdit2.Text = ""
                Label1.Text = ""
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select employeecode, fullname, MemoNo from db_attrec where approvedstatus = 'Requested'"
                Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tab)
                For index As Integer = 0 To tab.Rows.Count - 1
                    ComboBoxEdit1.Properties.Items.Add(tab.Rows(index).Item(0).ToString)
                    ComboBoxEdit2.Properties.Items.Add(tab.Rows(index).Item(1).ToString)
                Next
            Case "Perjalanan Dinas"
                tab5.Clear()
                ComboBoxEdit1.Properties.Items.Clear()
                ComboBoxEdit2.Properties.Items.Clear()
                ComboBoxEdit1.Text = ""
                ComboBoxEdit2.Text = ""
                Label1.Text = ""
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select employeecode, fullname, Memono from db_sppd where approvedstatus = 'Requested'"
                Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tab5)
                For index As Integer = 0 To tab5.Rows.Count - 1
                    ComboBoxEdit1.Properties.Items.Add(tab5.Rows(index).Item(0).ToString)
                    ComboBoxEdit2.Properties.Items.Add(tab5.Rows(index).Item(1).ToString)
                Next
        End Select
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        LabelControl5.Text = "Loans"
        empcode()
        'TextBox3.Text = ""
        'TextBox4.Text = ""
        ''GridView1.Columns.Clear()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_loan where status = 'Requested'"
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If quer = 0 Then
            SimpleButton5.Enabled = False
            SimpleButton7.Enabled = False
        Else
            SimpleButton5.Enabled = True
            SimpleButton7.Enabled = True
        End If
        'loans()
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        LabelControl5.Text = "Other Income"
        empcode()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_addition where status = 'Requested'"
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If quer = 0 Then
            SimpleButton5.Enabled = False
            SimpleButton7.Enabled = False
        Else
            SimpleButton5.Enabled = True
            SimpleButton7.Enabled = True
        End If
        'others()
    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function


    Private Sub SimpleButton4_Click_1(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        LabelControl5.Text = "Perjalanan Dinas"
        empcode()
        'TextBox3.Text = ""
        'TextBox4.Text = ""
        ''GridView1.Columns.Clear()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_sppd where approvedstatus = 'Requested'"
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If quer = 0 Then
            SimpleButton5.Enabled = False
            SimpleButton7.Enabled = False
        Else
            SimpleButton5.Enabled = True
            SimpleButton7.Enabled = True
        End If
    End Sub

    Private Function GetImage1() As Image
        Return ImageCollection1.Images(1)
    End Function

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Add(New DXMenuItem("Accept...", New EventHandler(AddressOf SimpleButton5_Click), GetImage))
            e.Menu.Items.Add(New DXMenuItem("Reject...", New EventHandler(AddressOf SimpleButton7_Click), GetImage1))
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        If LabelControl5.Text = "Loans" Then
            loans()
            For index As Integer = 0 To tab1.Rows.Count - 1
                If ComboBoxEdit1.SelectedItem Is tab1.Rows(index).Item(0).ToString Then
                    ComboBoxEdit2.Text = tab1.Rows(index).Item(1).ToString
                    'Label1.Text = tab1.Rows(index).Item(2).ToString
                    'ElseIf ComboBoxEdit2.SelectedItem Is tab1.Rows(index).Item(1).ToString Then
                    '    ComboBoxEdit1.Text = tab1.Rows(index).Item(0).ToString
                End If
            Next
        ElseIf LabelControl5.Text = "Other Income" Then
            others()
            For index As Integer = 0 To tab2.Rows.Count - 1
                If ComboBoxEdit1.SelectedItem Is tab2.Rows(index).Item(0).ToString Then
                    ComboBoxEdit2.Text = tab2.Rows(index).Item(1).ToString
                    Label1.Text = tab2.Rows(index).Item(2).ToString
                    'ElseIf ComboBoxEdit2.SelectedItem Is tab2.Rows(index).Item(1).ToString Then
                    '    ComboBoxEdit1.Text = tab2.Rows(index).Item(0).ToString
                End If
            Next
        ElseIf LabelControl5.Text = "Termination" Then
            terminate()
            For index As Integer = 0 To tab3.Rows.Count - 1
                If ComboBoxEdit1.SelectedItem Is tab3.Rows(index).Item(0).ToString Then
                    ComboBoxEdit2.Text = tab3.Rows(index).Item(1).ToString
                    Label1.Text = tab3.Rows(index).Item(2).ToString
                    'ElseIf ComboBoxEdit2.SelectedItem Is tab3.Rows(index).Item(1).ToString Then
                    '    ComboBoxEdit1.Text = tab3.Rows(index).Item(0).ToString
                End If
            Next
        ElseIf LabelControl5.Text = "Status Change" Then
            sc()
            For index As Integer = 0 To tab4.Rows.Count - 1
                If ComboBoxEdit1.SelectedItem Is tab4.Rows(index).Item(0).ToString Then
                    ComboBoxEdit2.Text = tab4.Rows(index).Item(1).ToString
                    Label1.Text = tab4.Rows(index).Item(2).ToString
                    'ElseIf ComboBoxEdit2.SelectedItem Is tab4.Rows(index).Item(1).ToString Then
                    '    ComboBoxEdit1.Text = tab4.Rows(index).Item(0).ToString
                End If
            Next
        ElseIf LabelControl5.Text = "Leave Request" Then
            leaves()
            For index As Integer = 0 To tab.Rows.Count - 1
                If ComboBoxEdit1.SelectedItem Is tab.Rows(index).Item(0).ToString Then
                    ComboBoxEdit2.Text = tab.Rows(index).Item(1).ToString
                    Label1.Text = tab.Rows(index).Item(2).ToString
                    'ElseIf ComboBoxEdit2.SelectedItem Is tab.Rows(index).Item(1).ToString Then
                    '    ComboBoxEdit1.Text = tab.Rows(index).Item(0).ToString
                End If
            Next
        End If
    End Sub
End Class