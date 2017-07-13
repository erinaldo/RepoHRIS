Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports word = Microsoft.Office.Interop.Word
'Imports DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit

Public Class MainApp
    'recruitment
    Dim connectionString1 As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()

    Public conStr As String

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
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
        connectionString1 = "Server=" + host + "; User Id=" + id + "; Password=" + password + "; Database=" + db + ""
    End Sub

#Region "koneksi "

    ' Dim cir As New Circle

    'Sub reprimand()
    '    Dim bs As MySqlCommand = SQLConnection.CreateCommand
    '    bs.CommandText = "select Employeecode from db_pegawai where EmployeeCode = '" & SimpleButton2.Text & "'"
    '    Dim employeecode As String = CStr(bs.ExecuteScalar)

    '    Dim pos As MySqlCommand = SQLConnection.CreateCommand
    '    pos.CommandText = "select FullName from db_pegawai where EmployeeCode = '" & SimpleButton2.Text & "'"
    '    Dim fullname As String = CStr(pos.ExecuteScalar)

    '    Dim objword As word.Application = Nothing
    '    Try
    '        objword = New word.Application
    '        objword.Documents.Open(Application.StartupPath + "\\reprimand.docx")
    '        Dim findobject As word.Find = objword.Selection.Find
    '        With findobject
    '            .ClearFormatting()
    '            .Text = "<Name>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = fullname.ToString
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '        End With
    '        With findobject
    '            .ClearFormatting()
    '            .Text = "<EmployeeCode>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = employeecode.ToString
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    End Try
    'End Sub

    'Sub automove()
    '    Dim Move As String = System.Environment.CurrentDirectory & "\\"
    '    Dim Copy As String = System.Environment.CurrentDirectory & "\\"
    '    Dim location As String = System.Environment.CurrentDirectory & "\\"
    '    System.IO.File.Delete(Move)
    '    System.IO.File.Copy(Copy, location)
    '    MsgBox("Backup")
    'End Sub

#End Region
    Private Sub MainApp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString1
        SQLConnection.Open()
        'Dim lbl As MySqlCommand = SQLConnection.CreateCommand
        'lbl.CommandText = "select user from db_temp"
        'Dim name As String = CStr(lbl.ExecuteScalar)
        'Label1.Text = name
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select leveluser from db_user where binary username = @user"
            query.Parameters.AddWithValue("@user", LabelControl2.Text)
            Dim quer As String = CStr(query.ExecuteScalar)
            If quer = "3" Then
                RibbonPageGroup7.Visible = False
            Else
                RibbonPageGroup7.Visible = True
            End If
        Catch ex As Exception
        End Try
        Timer1.Enabled = True
        BarButtonItem1.PerformClick()
        'act = "input"
        'If barJudul.Caption = "Module Recruitment" Then
        'End If
        'If barJudul.Caption = "Module Employee" Then
        'End If
        'Dim notify As MySqlCommand = SQLConnection.CreateCommand
        'notify.CommandText = "select count(idrec) from db_recruitment where interviewdate = curdate() and status = 'In Progress'"
        'Dim note As Integer = CInt(notify.ExecuteScalar)
    End Sub

    'Sub resetclear()
    '    txtBar1.Text = ""
    '    txtBar2.Text = ""
    '    txtBar3.Text = ""
    '    txtBar4.Text = ""
    '    txtBar5.Text = ""
    '    txtbar6.Text = ""
    '    txtstart.Text = ""
    '    txtfinish.Text = ""
    '    txtlocation.Text = ""
    '    txtphone.Text = ""
    '    compacode.Text = ""
    'End Sub

    'Sub reset()
    '    txtBar1.Text = ""
    '    txtBar2.Text = ""
    '    txtBar3.Text = ""
    '    txtBar4.Text = ""
    '    txtBar5.Text = ""
    '    txtbar6.Text = ""
    '    txtTanggal.Text = ""
    '    txtstart.Text = ""
    '    txtfinish.Text = ""
    '    lcstart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lcfinish.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lcphone.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lclocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lcTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lcJnsShif.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    loadDataKaryawan()
    'End Sub

#Region "menu bar"
    Private Sub BarButtonItem1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        'clearForm()
        '   Reset()
        ' resetclear()
        GridView1.BestFitColumns()
        LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lcForm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        barJudul.Caption = "Module Recruitment"
        ' GridControl1.RefreshDataSource()
        GridView1.Columns.Clear()
        'CardView1.Columns.Clear()
        loadDataReq()
        GridView1.Columns("Blacklist").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        GridView1.MoveLast()
    End Sub

    Private Sub BarButtonItem2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        reset()
        ' resetclear()
        GridView1.BestFitColumns()
        barJudul.Caption = "Module Employee"
        ' GridControl1.RefreshDataSource()
        GridView1.Columns.Clear()
        'CardView1.Columns.Clear()
        loadDataReq()
        GridView1.MoveLast()
    End Sub

    Private Sub BarButtonItem3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        'If loan Is Nothing OrElse loan.IsDisposed OrElse loan.MinimizeBox Then
        '    loan.Close()
        '    loan = New Payments
        'End If
        Payments.Close()
        With Payments
            .Label7.Text = LabelControl2.Text
            .Show()
        End With
        'loan.Show()
        'loan.BarButtonItem1.PerformClick()
        'loan.XtraTabPage5.Show()
    End Sub
    ' Dim showatt As New ShowAtt

    Private Sub BarButtonItem4_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        'If showatt Is Nothing OrElse showatt.IsDisposed OrElse showatt.MinimizeBox Then
        '    showatt.Close()
        '    showatt = New ShowAtt
        'End If
        'showatt.Show()
        ShowAtt.Close()
        With ShowAtt
            .Label2.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub BarButtonItem5_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        Dim pesan As String
        pesan = CType(MsgBox("Log Off Application?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Try
                Dim del As MySqlCommand = SQLConnection.CreateCommand
                del.CommandText = "truncate db_hasil"
                del.ExecuteNonQuery()
                Dim dele As MySqlCommand = SQLConnection.CreateCommand
                dele.CommandText = "truncate db_temp"
                dele.ExecuteNonQuery()
                dele.CommandText = "truncate db_tmpname"
                dele.ExecuteNonQuery()
            Catch ex As Exception

            End Try
            SQLConnection.Close()
            Close()
            Login.Close()
            Application.Exit()
        End If
        'Dim pesan As String
        'pesan = CType(MsgBox("Log Off Application?", MsgBoxStyle.YesNo, "Warning"), String)
        'If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
        '    Dim del As MySqlCommand = SQLConnection.CreateCommand
        '    ' del.CommandText = "delete from db_hasil where EmployeeCode != 'absbahsgedeg'"
        '    del.CommandText = "truncate db_hasil"
        '    del.ExecuteNonQuery()

        '    del.CommandText = "truncate db_tmpname"
        '    del.ExecuteNonQuery()

        '    'Dim dele As MySqlCommand = SQLConnection.CreateCommand
        '    'dele.CommandText = "delete from db_temp where EmployeeCode != 'absbahsgedeg'"
        '    'dele.ExecuteNonQuery()
        '    'Dim delet As MySqlCommand = SQLConnection.CreateCommand
        '    'delet.CommandText = "delete from db_tmpname where EmployeeCode != 'absbahsgedeg'"
        '    'delet.ExecuteNonQuery()
        '    'dele.CommandText = "truncate db_tmpname"
        '    'dele.ExecuteNonQuery()

        '    SQLConnection.Close()
        '    Close()
        '    setting.Close()
        '    rotasi.Close()
        '    employeenotes.Close()
        '    loan.Close()
        '    Login.Close()
        '    Application.Exit()
        'End If
    End Sub

    Dim tbl_par As New DataTable

    'Sub loadDataKaryawan()
    '    Dim sqlCommand As New MySqlCommand
    '    sqlCommand.CommandText = "SELECT EmployeeCode, FullName, Position, CompanyCode FROM db_pegawai WHERE Status!='Fired'"
    '    sqlCommand.Connection = SQLConnection
    '    Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
    '    Dim cb As New MySqlCommandBuilder(adapter)
    '    adapter.Fill(tbl_par)
    '    For index As Integer = 0 To tbl_par.Rows.Count - 1
    '    Next
    'End Sub

#End Region

#Region "button"
    'Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    resetclear()
    '    act = "input"
    'End Sub

    Private Sub btnSegarkan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        loadDataReq()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        'query.CommandText = "select deleterec from db_user where username = '" & LabelControl2.Text & "'"
        'Dim quer2 As String = CStr(query.ExecuteScalar)
        'If quer2 = "True" Then
        '    If barJudul.Caption = "Module Recruitment" Then
        '        DeleteReq()
        '    ElseIf barJudul.Caption = "Module Employee" Then
        '        DeleteEmp()
        '    ElseIf barJudul.Caption = "Module Payroll" Then
        '        DeletePay()
        '    ElseIf barJudul.Caption = "Module Attendance" Then
        '        DeleteAtt()
        '    End If
        'Else
        '    MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        'End If
        'loadDataReq()
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        importData()
        updatestats()
        'CardView1.Focus()
        'CardView1.MoveLast()
    End Sub

    'Dim infoForm As New infoReq

    'Public Sub btnLihat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If infoForm Is Nothing OrElse infoForm.IsDisposed OrElse infoForm.MinimizeBox Then
    '        infoForm.Close()
    '        infoForm = New infoReq
    '    End If
    '    infoForm.Show()
    'End Sub

#End Region

#Region "CRUD database"
    'import to employee
    Dim table As DataTable

    'Private Sub importData1()
    '    Dim maxid As Integer
    '    Dim newid As Integer = maxid + 1
    '    Dim ynow As String = Format(Now, "yy").ToString
    '    Dim mnow As String = Month(Now).ToString
    '    Dim lastn As Integer
    '    Dim updmon As String
    '    Dim lastcode As String = ""
    '    Dim tmp As Integer
    '    Dim numstat As Integer
    '    Try
    '        Dim cmd1 = SQLConnection.CreateCommand
    '        cmd1.CommandText = "select idrec from db_recruitment where status = 'Accepted'"
    '        Dim adp1 As New MySqlDataAdapter(cmd1)
    '        Dim ds1 As New DataSet
    '        adp1.Fill(ds1)
    '        For Each dt As DataTable In ds1.Tables
    '            For Each row As DataRow In dt.Rows
    '                Try
    '                    Dim cmd = SQLConnection.CreateCommand
    '                    cmd.CommandText = "select count(*) from db_recruitment where status = 'Accepted'"
    '                    numstat = CInt(cmd.ExecuteScalar)
    '                Catch ex As Exception
    '                    MsgBox(ex.Message)
    '                End Try
    '                Try
    '                    Dim cmd = SQLConnection.CreateCommand()
    '                    cmd.CommandText = "SELECT last_num FROM view_emp_last_code"
    '                    lastn = DirectCast(cmd.ExecuteScalar(), Integer)
    '                Catch ex As Exception
    '                End Try
    '                Try
    '                    Dim cmd = SQLConnection.CreateCommand
    '                    cmd.CommandText = "SELECT EmployeeCode FROM db_pegawai ORDER BY EmployeeCode DESC LIMIT 1"
    '                    lastcode = DirectCast(cmd.ExecuteScalar(), String)
    '                Catch ex As Exception
    '                End Try
    '                Try
    '                    Dim cmd = SQLConnection.CreateCommand
    '                    cmd.CommandText = "SELECT MID(EmployeeCode, 4, 1) FROM db_pegawai where EmployeeCode = '" & lastcode & "'"
    '                    updmon = DirectCast(cmd.ExecuteScalar(), String)
    '                    If CInt(updmon) <> CInt(mnow) Then
    '                        tmp = 1
    '                    Else
    '                        tmp = lastn + 1
    '                    End If
    '                Catch ex2 As Exception
    '                End Try
    '                Dim actualcode As String = ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
    '                Dim sqlCommand As New MySqlCommand
    '                Try
    '                    sqlCommand.CommandText = "INSERT INTO db_pegawai (FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, IdNumber, Photo, status, CompanyCode, EmployeeCode, OfficeLocation, PhoneNumber, WorkDate, ChangeDate)" +
    '                                                     "SELECT FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, IdNumber, Photo, @status, @CompanyCode, @EmployeeCode, @OfficeLocation, @PhoneNumber, @WorkDate, @ChangeDate FROM db_recruitment WHERE Status='Accepted' AND idrec = @idrec"
    '                    sqlCommand.Parameters.AddWithValue("@Status", "<empty>")
    '                    sqlCommand.Parameters.AddWithValue("@CompanyCode", "<empty>")
    '                    sqlCommand.Parameters.AddWithValue("@EmployeeCode", actualcode)
    '                    sqlCommand.Parameters.AddWithValue("@OfficeLocation", "<empty>")
    '                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", employees.txtphoneno.Text)
    '                    sqlCommand.Parameters.AddWithValue("@WorkDate", Date.Now)
    '                    sqlCommand.Parameters.AddWithValue("@ChangeDate", Date.Now)
    '                    sqlCommand.Parameters.AddWithValue("@idrec", row.Item("idrec"))
    '                    sqlCommand.Connection = SQLConnection
    '                    sqlCommand.ExecuteNonQuery()
    '                    MessageBox.Show("Data Succesfully Imported, Please Click Refresh To Load")
    '                Catch ex As Exception
    '                    MsgBox(ex.Message)
    '                End Try
    '            Next
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub importData()
        Dim maxid As Integer
        Dim newid As Integer = maxid + 1
        Dim ynow As String = Format(Now, "yy").ToString
        Dim mnow As String = Month(Now).ToString
        Dim lastn As Integer
        Dim updmon As String
        Dim lastcode As String = ""
        Dim tmp As Integer
        Dim numstat As Integer
        Dim cd As MySqlCommand = SQLConnection.CreateCommand
        cd.CommandText = "select count(*) from db_recruitment where status = 'Accepted'"
        Dim cdres As Integer = CInt(cd.ExecuteScalar)
        If cdres = 0 Then
            MsgBox("There's no data to be imported")
        Else
            Try
                Dim cmd1 = SQLConnection.CreateCommand
                cmd1.CommandText = "select idrec from db_recruitment where status = 'Accepted'"
                Dim adp1 As New MySqlDataAdapter(cmd1)
                Dim ds1 As New DataSet
                adp1.Fill(ds1)
                For Each dt As DataTable In ds1.Tables
                    For Each row As DataRow In dt.Rows
                        Try
                            Dim cmd = SQLConnection.CreateCommand
                            cmd.CommandText = "select count(*) from db_recruitment where status = 'Accepted'"
                            numstat = CInt(cmd.ExecuteScalar)
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                        Try
                            Dim cmd = SQLConnection.CreateCommand()
                            cmd.CommandText = "SELECT last_num FROM view_emp_last_code"
                            lastn = DirectCast(cmd.ExecuteScalar(), Integer)
                        Catch ex As Exception
                        End Try
                        Try
                            Dim cmd = SQLConnection.CreateCommand
                            cmd.CommandText = "SELECT EmployeeCode FROM db_pegawai ORDER BY EmployeeCode DESC LIMIT 1"
                            lastcode = DirectCast(cmd.ExecuteScalar(), String)
                        Catch ex As Exception
                        End Try
                        Try
                            Dim cmd = SQLConnection.CreateCommand
                            cmd.CommandText = "SELECT MID(EmployeeCode, 4, 1) FROM db_pegawai where EmployeeCode = '" & lastcode & "'"
                            updmon = DirectCast(cmd.ExecuteScalar(), String)
                            If CInt(updmon) <> CInt(mnow) Then
                                tmp = 1
                            Else
                                tmp = lastn + 1
                            End If
                        Catch ex2 As Exception
                        End Try
                        Dim actualcode As String = ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
                        Dim sqlCommand As MySqlCommand = SQLConnection.CreateCommand
                        Try
                            sqlCommand.CommandText = "INSERT INTO db_pegawai (FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, IdNumber, Photo, status, CompanyCode, EmployeeCode, OfficeLocation, PhoneNumber, WorkDate, ChangeDate, NickName, Weight, Height, BloodType, RecommendedBy)" +
                                                             "SELECT FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, IdNumber, Photo, @status, @CompanyCode, @EmployeeCode, @OfficeLocation, PhoneNumber, @WorkDate, @ChangeDate, NickName, Weight, Height, BloodType, RecommendedBy FROM db_recruitment WHERE Status='Accepted' AND idrec = @idrec"
                            sqlCommand.Parameters.AddWithValue("@Status", "Active")
                            sqlCommand.Parameters.AddWithValue("@CompanyCode", "<empty>")
                            sqlCommand.Parameters.AddWithValue("@EmployeeCode", actualcode)
                            sqlCommand.Parameters.AddWithValue("@OfficeLocation", "<empty>")
                            sqlCommand.Parameters.AddWithValue("@WorkDate", Date.Now)
                            sqlCommand.Parameters.AddWithValue("@ChangeDate", Date.Now)
                            sqlCommand.Parameters.AddWithValue("@idrec", row.Item("idrec"))
                            sqlCommand.ExecuteNonQuery()
                            MessageBox.Show("Data Succesfully Imported, Please Click Refresh To Load")
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                        Try
                            sqlCommand.CommandText = "update db_education set " +
                                                        " EmployeeCode = @EmployeeCode" +
                                                        " where idrec = @idrec"
                            sqlCommand.Parameters.Clear()
                            sqlCommand.Parameters.AddWithValue("@EmployeeCode", actualcode)
                            sqlCommand.Parameters.AddWithValue("@idrec", row.Item("idrec"))
                            sqlCommand.ExecuteNonQuery()

                            sqlCommand.CommandText = "update db_certificates set " +
                                                        " EmployeeCode = @EmployeeCode" +
                                                        " where idrec = @idrec"
                            sqlCommand.Parameters.Clear()
                            sqlCommand.Parameters.AddWithValue("@EmployeeCode", actualcode)
                            sqlCommand.Parameters.AddWithValue("@idrec", row.Item("idrec"))
                            sqlCommand.ExecuteNonQuery()

                            sqlCommand.CommandText = "update db_empskill set " +
                                                            " EmployeeCode = @EmployeeCode" +
                                                            " where idrec = @idrec"
                            sqlCommand.Parameters.Clear()
                            sqlCommand.Parameters.AddWithValue("@EmployeeCode", actualcode)
                            sqlCommand.Parameters.AddWithValue("@idrec", row.Item("idrec"))
                            sqlCommand.ExecuteNonQuery()

                            sqlCommand.CommandText = "update db_exp set " +
                                                        " EmployeeCode = @EmployeeCode" +
                                                        " where idrec = @idrec"
                            sqlCommand.Parameters.Clear()
                            sqlCommand.Parameters.AddWithValue("@EmployeeCode", actualcode)
                            sqlCommand.Parameters.AddWithValue("@idrec", row.Item("idrec"))
                            sqlCommand.ExecuteNonQuery()


                            sqlCommand.CommandText = "update db_family set " +
                                                        " EmployeeCode = @EmployeeCode" +
                                                        " where idrec = @idrec"
                            sqlCommand.Parameters.Clear()
                            sqlCommand.Parameters.AddWithValue("@EmployeeCode", actualcode)
                            sqlCommand.Parameters.AddWithValue("@idrec", row.Item("idrec"))
                            sqlCommand.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Next
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    'SELECT CONCAT(DATE_FORMAT(Now(),'%y-%m'),"-", LPAD((RIGHT(MAX(EmployeeCode),4)+1),4,'0')) FROM db_pegawai 
    Public Sub updatestats()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_recruitment SET" +
                                    " Status = @Status" +
                                    " WHERE Status = 'Accepted'"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@Status", "Processed")
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub loadDataReq()
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlCommand As New MySqlCommand
        Try
            If barJudul.Caption = "Module Recruitment" Then
                sqlCommand.CommandText = "select Blacklist, IdRec, InterviewTimes as AppliedTimes, FullName,  PlaceOfBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Status, InterviewDate, Position, NickName, ApplicationDate,  City, ZIP, HomeNumber, RecommendedBy, Martial as MartialStatus, CreatedDate from db_recruitment where status != 'Processed'"
            ElseIf barJudul.Caption = "Module Employee" Then
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                'query.CommandText = "select user from db_temp"
                'Dim querr As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim3 from db_user where username = '" & LabelControl2.Text & "'"
                Dim quer1 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select kim4 from db_user where username= '" & LabelControl2.Text & "'"
                Dim quer22 As String = CStr(query.ExecuteScalar)
                query.CommandText = "select pantailabu from db_user where username = '" & LabelControl2.Text & "'"
                Dim quer3 As String = CStr(query.ExecuteScalar)
                If quer1 = "True" And quer22 = "False" And quer3 = "False" Then
                    sqlCommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation = 'KIM 3'"
                ElseIf quer1 = "True" And quer22 = "True" And quer3 = "False" Then
                    sqlCommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation <> 'Pantai Labu' and officelocation != '<empty>'"
                ElseIf quer1 = "True" And quer22 = "False" And quer3 = "True" Then
                    sqlCommand.CommandText = "Select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation <> 'KIM 4' and officelocation != '<empty>'"
                ElseIf quer1 = "False" And quer22 = "True" And quer3 = "True" Then
                    sqlCommand.CommandText = "Select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation <> 'KIM 3' and officelocation != '<empty>'"
                ElseIf quer1 = "False" And quer22 = "False" And quer3 = "True" Then
                    sqlCommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation = 'Pantai Labu'"
                ElseIf quer1 = "False" And quer22 = "True" And quer3 = "False" Then
                    sqlCommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation = 'KIM 4'"
                Else
                    sqlCommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated'"
                End If
            End If
            sqlCommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
            'Dim repository As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
            'GridView1.Columns(0).ColumnEdit = repository
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridView1.BestFitColumns()
    End Sub

    'Public Function InsertAtt() As Boolean
    '    Dim sqlCommand As New MySqlCommand
    '    Dim str_carSql As String
    '    Try
    '        If act = "edit" Then
    '            str_carSql = "UPDATE db_absensi SET" +
    '                    " FullName = @FullName" +
    '                   ", Tanggal = @Tanggal" +
    '                   ", Shift = @Shift" +
    '                   ", JamMulai = @JamMulai" +
    '                   ", JamSelesai = @JamSelesai" +
    '                   " WHERE EmployeeCode = @EmployeeCode"
    '        Else
    '            str_carSql = "INSERT INTO db_absensi " +
    '                        "(EmployeeCode, FullName, Tanggal, Shift, JamMulai, JamSelesai) " +
    '                        "values (@EmployeeCode ,@FullName, @Tanggal, @Shift, @JamMulai, @JamSelesai)"
    '        End If
    '        sqlCommand.Connection = SQLConnection
    '        sqlCommand.CommandText = str_carSql
    '        sqlCommand.Parameters.AddWithValue("@EmployeeCode", txtbar2.Text)
    '        sqlCommand.Parameters.AddWithValue("@Tanggal", txtTanggal.Text)
    '        sqlCommand.Parameters.AddWithValue("@Shift", txtJnsShift.Text)
    '        sqlCommand.Parameters.AddWithValue("@JamMulai", txtstart.Text)
    '        sqlCommand.Parameters.AddWithValue("@JamSelesai", txtfinish.Text)
    '        sqlCommand.ExecuteNonQuery()
    '        If act = "input" Then
    '            MessageBox.Show("Data Successfully Added!")
    '        ElseIf act = "edit" Then
    '            MessageBox.Show("Data Successfully Changed!")
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '        MessageBox.Show("Process Failed!")
    '    End Try
    'End Function
    '''delete

    'Public Function deletesp() As Boolean
    '    Dim sqlcommand As New MySqlCommand
    '    Dim mess As String
    '    mess = CType(MsgBox("Sure To Delete ?", MsgBoxStyle.YesNo, "Warning"), String)
    '    If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
    '        sqlcommand.Connection = SQLConnection
    '        sqlcommand.CommandType = CommandType.Text
    '        sqlcommand.CommandText = "DELETE FROM db_sp WHERE EmployeeCode = " + txtbar2.Text
    '        sqlcommand.ExecuteNonQuery()
    '        MsgBox("Data Succesfully Removed!", MsgBoxStyle.Information, "Success")
    '        GridControl1.RefreshDataSource()
    '        loadDataReq()
    '    End If
    '    Return Nothing
    'End Function

    'Private movelast As Boolean = True

    'Public Function DeleteReq() As Boolean
    '    Dim sqlCommand As New MySqlCommand
    '    Dim pesan As String
    '    pesan = CType(MsgBox("Sure To Delete ?", MsgBoxStyle.YesNo, "Warning"), String)
    '    If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
    '        sqlCommand.Connection = SQLConnection
    '        sqlCommand.CommandType = CommandType.Text
    '        sqlCommand.CommandText = "DELETE FROM db_recruitment WHERE IdRec = '" & SimpleButton2.Text & "'"
    '        sqlCommand.ExecuteNonQuery()
    '        MsgBox("Data Successfully Removed", MsgBoxStyle.Information, "Success")
    '        GridControl1.RefreshDataSource()
    '        loadDataReq()
    '    End If
    '    Return Nothing
    'End Function

    Private needMoveLastRow As Boolean = True

    'Public Function DeleteEmp() As Boolean
    '    Try
    '        Dim sqlCommand As New MySqlCommand
    '        Dim pesan As String
    '        pesan = CType(MsgBox("Sure To Delete ?", MsgBoxStyle.YesNo, "Warning"), String)
    '        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
    '            sqlCommand.Connection = SQLConnection
    '            sqlCommand.CommandType = CommandType.Text
    '            sqlCommand.CommandText = "DELETE FROM db_pegawai WHERE EmployeeCode = " + txtbar2.Text
    '            sqlCommand.ExecuteNonQuery()
    '            MsgBox("Data Succesfully Removed !", MsgBoxStyle.Information, "Success")
    '            GridControl1.RefreshDataSource()
    '            loadDataReq()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    End Try
    '    Return Nothing
    'End Function

    'Public Function DeletePay() As Boolean
    '    Dim sqlcommand As New MySqlCommand
    '    Dim messages As String
    '    messages = CType(MsgBox("Sure To Delete ?", MsgBoxStyle.YesNo, "Warning"), String)
    '    If CType(messages, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
    '        sqlcommand.Connection = SQLConnection
    '        sqlcommand.CommandType = CommandType.Text
    '        sqlcommand.CommandText = "DELETE FROM db_payroll WHERE EmployeeCode = " + txtbar2.Text
    '        sqlcommand.ExecuteNonQuery()
    '        MsgBox("Data Succesfully Removed!", MsgBoxStyle.Information, "Success")
    '        GridControl1.RefreshDataSource()
    '        loadDataReq()
    '    End If
    '    Return Nothing
    'End Function

    'Public Function DeleteAtt() As Boolean
    '    Dim sqlcommand As New MySqlCommand
    '    Dim pesan As String
    '    pesan = CType(MsgBox("Sure To Delete ?", MsgBoxStyle.YesNo, "Warning"), String)
    '    If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
    '        sqlcommand.Connection = SQLConnection
    '        sqlcommand.CommandType = CommandType.Text
    '        sqlcommand.CommandText = "DELETE FROM db_absensi WHERE EmployeeCode = " + txtbar2.Text
    '        sqlcommand.ExecuteNonQuery()
    '        MsgBox("Data Successfully Removed!", MsgBoxStyle.Information, "Success")
    '        GridControl1.RefreshDataSource()
    '        loadDataReq()
    '    End If
    '    Return Nothing
    'End Function
#End Region

    ''fungsi extras
    'Sub clearForm()
    '    lc3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    lc6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    txtbar1.Text = ""
    '    txtbar2.Text = ""
    '    txtBar3.Text = ""
    '    txtBar4.Text = ""
    '    txtBar5.Text = ""
    '    txtbar6.Text = ""
    '    txtTanggal.Text = ""
    'End Sub

    'Function ImageToByte(ByVal pbImg As PictureBox) As Byte()
    '    If pbImg Is Nothing Then
    '        Return Nothing
    '    End If
    '    Dim ms As New MemoryStream()
    '    pbImg.Image.Save(ms, Imaging.ImageFormat.Jpeg)
    '    Return ms.ToArray()
    'End Function

    'Public Function ByteToImage(ByVal filefoto As Byte()) As Image
    '    Dim pictureBytes As New MemoryStream(filefoto)
    '    Return Image.FromStream(pictureBytes)
    'End Function

    'Dim act As String = ""
    'Dim setting As New setting

    Private Sub BarButtonItem6_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        'If setting Is Nothing OrElse setting.IsDisposed Then
        '    setting = New setting
        'End If
        'setting.Show()
    End Sub

    'Sub ShowGridPreview(ByVal grid As GridControl)
    '    If Not grid.IsPrintingAvailable Then
    '        MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error")
    '        Return
    '    End If
    '    grid.ShowPrintPreview()
    'End Sub

    'Sub PrintGrid(ByVal grid As GridControl)
    '    If Not grid.IsPrintingAvailable Then
    '        MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error")
    '        Return
    '    End If
    '    grid.Print()
    'End Sub

    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ShowGridPreview(GridControl1)
    'End Sub

    'Private Sub txtBar1_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Dim ch As Char = e.KeyChar
    '    If Char.IsLetter(ch) Then
    '        e.Handled = True
    '    End If
    'End Sub

    'Dim nilai As Double
    'Dim value As Long
    'Dim value2 As Long

    'Private Sub rapel_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Dim ch As Char = e.KeyChar
    '    If Char.IsLetter(ch) Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        'clearForm()
        'reset()
        'resetclear()
        'lcForm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'barJudul.Caption = "Module Payment Details"
        'GridControl1.RefreshDataSource()
        'loadDataReq()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        'clearForm()
        'reset()
        'resetclear()
        'lcForm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'barJudul.Caption = "Employee Menus"
        'GridControl1.RefreshDataSource()
        ' loadDataReq()
    End Sub

    'Dim employeenotes As New Notes
    'Private Sub btnNotes_Click(sender As Object, e As EventArgs)
    '    If employeenotes Is Nothing OrElse employeenotes.IsDisposed OrElse employeenotes.MinimizeBox Then
    '        employeenotes.Close()
    '        employeenotes = New Notes
    '    End If
    '    employeenotes.Show()
    'End Sub

    'Dim spforms As New SPForms

    'Private Sub sp1_Click(sender As Object, e As EventArgs)
    '    If spforms Is Nothing OrElse spforms.IsDisposed OrElse spforms.MinimizeBox Then
    '        spform.Close()
    '        spforms = New SPForms
    '    End If
    '    spforms.Show()
    'End Sub

    'Dim rotasi As New RotasiMutasi

    Private Sub GridView1_RowLoaded(sender As Object, e As Views.Base.RowEventArgs)
        Dim view As ColumnView = TryCast(sender, ColumnView)
        If needMoveLastRow = False Then
            view.MoveLast()
        End If
    End Sub

    Dim employees As New NewRec
    Dim newemps As New NewEmp

    Private Sub btnNew_Click(sender As Object, e As EventArgs)
        ''If barJudul.Caption = "Module Recruitment" Then
        ''    If employees Is Nothing OrElse employees.IsDisposed OrElse employees.MinimizeBox Then
        ''        employees.Close()
        ''        employees = New NewRec
        ''    End If
        ''    employees.Show()
        ''    employees.BarButtonItem1.PerformClick()
        ''ElseIf barJudul.Caption = "Module Employee" Then
        ''    If newemps Is Nothing OrElse newemps.IsDisposed OrElse newemps.MinimizeBox Then
        ''        newemps.Close()
        ''        newemps = New NewEmp
        ''    End If
        ''    newemps.Show()
        ''    newemps.BarButtonItem1.PerformClick()
        ''End If
    End Sub

    Dim proses As New RecProcess
    Dim formed As New ChangeData
    Dim changeem As New ChangeEmp

    Private Sub btnChange_Click(sender As Object, e As EventArgs)
        ''If barJudul.Caption = "Module Recruitment" Then
        ''    If formed Is Nothing OrElse formed.IsDisposed OrElse formed.MinimizeBox Then
        ''        formed.Close()
        ''        formed = New ChangeData
        ''    End If
        ''    formed.Show()
        ''ElseIf barJudul.Caption = "Module Employee" Then
        ''    If changeem Is Nothing OrElse changeem.IsDisposed OrElse changeem.MinimizeBox Then
        ''        formed.Close()
        ''        changeem = New ChangeEmp
        ''    End If
        ''    changeem.Show()
        ''End If
    End Sub

    Private Sub btnProg_Click(sender As Object, e As EventArgs)
        ''If proses Is Nothing OrElse proses.IsDisposed OrElse proses.MinimizeBox Then
        ''    proses.Close()
        ''    proses = New RecProcess
        ''End If
        ''proses.Show()
    End Sub

    Dim loan As New Payments

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        If loan Is Nothing OrElse loan.IsDisposed OrElse loan.MinimizeBox Then
            loan.Close()
            loan = New Payments
        End If
        loan.Show()
        loan.BarButtonItem1.PerformClick()
        loan.XtraTabPage5.Show()
    End Sub

    Private Sub GridView1_PopupMenuShowing_1(sender As Object, e As PopupMenuShowingEventArgs)
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
    End Sub

    Dim rep As New Reports

    Private Sub BarButtonItem11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem11.ItemClick
        Reports.Close()
        With Reports
            .Label3.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextEdit1.Text = Format(Now, "dd, MMMM, yyyy hhhh:mm:ss")
    End Sub

    'Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
    '    If rotasi Is Nothing OrElse rotasi.IsDisposed OrElse rotasi.MinimizeBox Then
    '        rotasi.Close()
    '        rotasi = New RotasiMutasi
    '    End If
    '    rotasi.Show()
    'End Sub

    Dim att2 As New Attendances

    Private Sub BarButtonItem12_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem12.ItemClick
        If att2 Is Nothing OrElse att2.IsDisposed OrElse att2.MinimizeBox Then
            att2.Close()
            att2 = New Attendances
        End If
        att2.Show()
    End Sub

    Private Sub GridView1_PopupMenuShowing_2(sender As Object, e As PopupMenuShowingEventArgs)
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
        If barJudul.Caption = "Module Recruitment" Then
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            End If
        ElseIf barJudul.Caption = "Module Employee" Then
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
                e.Menu.Items.Add(New DXMenuItem("Terminate This Employee ?", New EventHandler(AddressOf SimpleButton2_Click_1)))
            End If
        End If
    End Sub

    Private Sub SimpleButton2_Click_1(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim mess As String
        Dim nam As MySqlCommand = SQLConnection.CreateCommand
        nam.CommandText = "select fullname from db_pegawai where employeecode = '" & SimpleButton2.Text & "'"
        Dim nama As String = CStr(nam.ExecuteScalar)
        mess = CType(MsgBox("Are you sure to change " & nama & " status with Employee Code " & SimpleButton2.Text & " to be 'Terminated' ?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Try
                Dim up As MySqlCommand = SQLConnection.CreateCommand
                up.CommandText = "update db_pegawai set" +
                                " Status = @stat" +
                                ", TerminateDate = @tmntdates" +
                                " where Employeecode = @ic"
                up.Parameters.AddWithValue("@ic", SimpleButton2.Text)
                up.Parameters.AddWithValue("@stat", "Terminated")
                up.Parameters.AddWithValue("@tmntdates", Date.Now)
                up.ExecuteNonQuery()

                Dim delpay As MySqlCommand = SQLConnection.CreateCommand
                delpay.CommandText = "delete from db_payrolldata where employeecode = '" & SimpleButton2.Text & "'"
                delpay.ExecuteNonQuery()

                Dim delatt As MySqlCommand = SQLConnection.CreateCommand
                delatt.CommandText = "delete from db_absensi where employeecode = '" & SimpleButton2.Text & "'"
                delatt.ExecuteNonQuery()

                MsgBox("Status from " & nama & " Is changed to be 'Terminated'", MsgBoxStyle.Information)
                Dim delsal As MySqlCommand = SQLConnection.CreateCommand
                delsal.CommandText = "delete from db_payrolldata where EmployeeCode = '" & SimpleButton2.Text & "'"
                delsal.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)
        'If cir Is Nothing Or cir.IsDisposed OrElse cir.MinimizeBox Then
        '    cir.Close()
        '    cir = New Circle
        'End If
        'cir.Show()
    End Sub

    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs)
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub MainApp_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Tile_Control.Close()
        'Tile_Control.Show()
    End Sub

    Private Function GetImage() As System.Drawing.Image
        Return ImageCollection1.Images(0)
    End Function

    Private Function GetImage1() As Image
        Return ImageCollection1.Images(1)
    End Function

    Public Function GetImage2() As Image
        Return ImageCollection1.Images(2)
    End Function

    Private Function GetImage3() As Image
        Return ImageCollection1.Images(3)
    End Function

    Private Function GetImage4() As Image
        Return ImageCollection1.Images(4)
    End Function

    Private Function GetImage5() As Image
        Return ImageCollection1.Images(5)
    End Function

    Private Function GetImage6() As Image
        Return ImageCollection1.Images(6)
    End Function

    Private Function GetImage7() As Image
        Return ImageCollection1.Images(7)
    End Function

    Private Function GetImage8() As Image
        Return ImageCollection1.Images(8)
    End Function

    Private Function GetImage9() As Image
        Return ImageCollection1.Images(9)
    End Function

    Private Function GetImage10() As Image
        Return ImageCollection1.Images(10)
    End Function

    Private Function GetImage11() As Image
        Return ImageCollection1.Images(11)
    End Function

    Private Function GetImage12() As Image
        Return ImageCollection1.Images(12)
    End Function

    Private Function getimage13() As Image
        Return ImageCollection1.Images(13)
    End Function

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If

        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select status from db_recruitment where idrec = '" & SimpleButton2.Text & "'"
        Dim quer As String = CStr(query.ExecuteScalar)

        query.CommandText = "select fullname from db_pegawai where EmployeeCode = '" & SimpleButton2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)

        If barJudul.Caption = "Module Recruitment" Then
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
                If quer = "Pending" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    ' e.Menu.Items.Add(New DXMenuItem("Add Skills", New EventHandler(AddressOf Button12_Click), GetImage12))
                    e.Menu.Items.Add(New DXMenuItem("Change status to be In Progress", New EventHandler(AddressOf Button13_Click), GetImage3()))
                    e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button9_Click), GetImage8))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                ElseIf quer = "In Progress" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    e.Menu.Items.Add(New DXMenuItem("Add Skills", New EventHandler(AddressOf Button11_Click), GetImage12))
                    'e.Menu.Items.Add(New DXMenuItem("Change status to be In Progress", New EventHandler(AddressOf Button12_Click), GetImage3()))
                    e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button9_Click), GetImage8))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                ElseIf quer = "Rejected" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    'e.Menu.Items.Add(New DXMenuItem("Add Skills", New EventHandler(AddressOf Button12_Click), GetImage12))
                    'e.Menu.Items.Add(New DXMenuItem("Change status to be In Progress", New EventHandler(AddressOf Button12_Click), GetImage3))
                    e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button9_Click), GetImage8))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                ElseIf quer = "Processed" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    'e.Menu.Items.Add(New DXMenuItem("Add Skills", New EventHandler(AddressOf Button12_Click), GetImage12))
                    'e.Menu.Items.Add(New DXMenuItem("Change status to be In Progress", New EventHandler(AddressOf Button12_Click), GetImage3))
                    'e.Menu.Items.Add(New DXMenuItem("Change status to be In Progress"))
                    'e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button12_Click), GetImage8))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                End If
            End If
        ElseIf barJudul.Caption = "Module Employee" Then
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
                Dim submenu As New DXSubMenuItem("rows")
                e.Menu.Items.Add(New DXMenuItem("Import Accepted Candidates From Recruitment", New EventHandler(AddressOf btnImport_Click), GetImage5))
                e.Menu.Items.Add(New DXMenuItem("Add Employee", New EventHandler(AddressOf Button3_Click), GetImage1))
                e.Menu.Items.Add(New DXMenuItem("View Employee", New EventHandler(AddressOf Button1_Click), GetImage2))
                e.Menu.Items.Add(New DXMenuItem("Status Change", New EventHandler(AddressOf SimpleButton5_Click), GetImage6))
                e.Menu.Items.Add(New DXMenuItem("Termination", New EventHandler(AddressOf SimpleButton6_Click), GetImage7))
                e.Menu.Items.Add(New DXMenuItem("Warning Notice", New EventHandler(AddressOf SimpleButton4_Click), GetImage4))
                e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button8_Click), GetImage8))
                e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                e.Menu.Items.Add(New DXMenuItem("Terminate This Employee ?", New EventHandler(AddressOf SimpleButton2_Click_1), GetImage))
            End If
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged_1(sender As Object, e As FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If barJudul.Caption = "Module Employee" Then
            Dim datatabl As New DataTable
            Dim sqlCommand As New MySqlCommand
            datatabl.Clear()
            Dim param As String = ""
            Try
                param = "and EmployeeCode='" + GridView1.GetFocusedRowCellValue("EmployeeCode").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT EmployeeCode, FullName FROM db_pegawai WHERE 1=1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            If datatabl.Rows.Count > 0 Then
                SimpleButton2.Text = datatabl.Rows(0).Item(0).ToString
                Button5.Text = datatabl.Rows(0).Item(1).ToString
            End If
        ElseIf barJudul.Caption = "Module Recruitment" Then
            Dim datatabl As New DataTable
            Dim sqlCommand As New MySqlCommand
            datatabl.Clear()
            Dim param As String = ""
            Try
                param = "and IdRec='" + GridView1.GetFocusedRowCellValue("IdRec").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT IdRec , FullName FROM db_recruitment WHERE 1=1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            If datatabl.Rows.Count > 0 Then
                SimpleButton2.Text = datatabl.Rows(0).Item(0).ToString
                Button5.Text = datatabl.Rows(0).Item(1).ToString
            End If
        End If
    End Sub

    Dim emp As New EmployeeDetails

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select inputemp from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
            query.Parameters.AddWithValue("@Name", Button5.Text)
            query.ExecuteNonQuery()
            If emp Is Nothing OrElse emp.IsDisposed OrElse emp.MinimizeBox Then
                emp.Close()
                emp = New EmployeeDetails
            End If
            emp.Show()
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Dim modify As New ChangeEmp

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()
        query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
        query.Parameters.AddWithValue("@Name", Button5.Text)
        query.ExecuteNonQuery()
        If modify Is Nothing OrElse modify.IsDisposed OrElse modify.MinimizeBox Then
            modify.Close()
            modify = New ChangeEmp
        End If
        modify.Show()
    End Sub

    Dim add As New NewEmp

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select InputEmp from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            With NewEmp
                .Label38.Text = LabelControl2.Text
                .Show()
            End With
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Dim add2 As New NewRec

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select inputrec from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            NewRec.Close()
            With NewRec
                .Label40.Text = LabelControl2.Text
                .Show()
            End With
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Dim change As New ChangeData

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()
        query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, name)" +
                            "values (@employeecode, @Name)"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
        query.Parameters.AddWithValue("@Name", Button5.Text)
        query.ExecuteNonQuery()
        If change Is Nothing OrElse change.IsDisposed OrElse change.MinimizeBox Then
            change.Close()
            change = New ChangeData
        End If
        change.Show()
    End Sub

    Dim cand As New CandidatesDetails

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select viewrec from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                                "(idrec)" +
                                "values (@idrec)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@idrec", SimpleButton2.Text)
            query.ExecuteNonQuery()
            If cand Is Nothing OrElse cand.IsDisposed OrElse cand.MinimizeBox Then
                cand.Close()
                cand = New CandidatesDetails
            End If
            cand.Show()
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Dim pro As New RecProcess

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()
        query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
        query.Parameters.AddWithValue("@Name", Button5.Text)
        query.ExecuteNonQuery()
        If pro Is Nothing OrElse pro.IsDisposed OrElse pro.MinimizeBox Then
            pro.Close()
            pro = New RecProcess
        End If
        pro.Show()
    End Sub

    Dim warn As New Warning

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()
        query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, name)" +
                            "values (@employeecode, @name)"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
        query.Parameters.AddWithValue("@Name", Button5.Text)
        query.ExecuteNonQuery()
        If warn Is Nothing OrElse warn.IsDisposed OrElse warn.MinimizeBox Then
            warn.Close()
            warn = New Warning
        End If
        warn.Show()
    End Sub

    Dim status As New StatusChange

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select statuschemp from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
            query.Parameters.AddWithValue("@Name", Button5.Text)
            query.ExecuteNonQuery()
            If status Is Nothing OrElse status.IsDisposed OrElse status.MinimizeBox Then
                status.Close()
                status = New StatusChange
            End If
            status.Show()
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Dim term As New Termination

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select terminateemp from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
            query.Parameters.AddWithValue("@Name", Button5.Text)
            query.ExecuteNonQuery()
            If term Is Nothing OrElse term.IsDisposed OrElse term.MinimizeBox Then
                term.Close()
                term = New Termination
            End If
            term.Show()
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Dim chx As New ChangeEmp

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select modemp from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
            query.Parameters.AddWithValue("@Name", Button5.Text)
            query.ExecuteNonQuery()
            ChangeEmp.Close()
            With ChangeEmp
                .Label37.Text = LabelControl2.Text
                .Show()
            End With
            'If chx Is Nothing OrElse chx.IsDisposed OrElse chx.MinimizeBox Then
            '    chx.Close()
            '    chx = New ChangeEmp
            'End If
            'chx.Show()
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Dim chxx As New ChangeData

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select inputrec from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
            query.Parameters.AddWithValue("@Name", Button5.Text)
            query.ExecuteNonQuery()
            ChangeData.Close()
            With ChangeData
                .Label37.Text = LabelControl2.Text
                .Show()
            End With
            'If chxx Is Nothing OrElse chxx.IsDisposed OrElse chxx.MinimizeBox Then
            '    chxx.Close()
            '    chxx = New ChangeData
            'End If
            'chxx.Show()
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub SimpleButton1_Click_2(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Dim tile As New Tile_Control

    Private Sub BarButtonItem13_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem13.ItemClick
        Tile_Control.Close()
        With Tile_Control
            .TextBox1.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs)
        Close()
        If tile Is Nothing OrElse tile.IsDisposed OrElse tile.MinimizeBox Then
            tile.Close()
            tile = New Tile_Control
        End If
        tile.Show()
    End Sub

    Dim appro As New Approvement

    Private Sub BarButtonItem15_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem15.ItemClick
        Approvement.Close()
        With Approvement
            .Label2.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ' reprimand()
    End Sub
    Dim dtb As New DataTable
    Dim rowint As Integer
    Dim rowfound As Boolean

    Private Function permanent(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Status"))
            Return (val = "Permanent")
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function IsShipToUSCanada(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Status"))
            Return (val = "Processed")
        Catch
            Return False
        End Try
    End Function

    Private Function pending(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Status"))
            Return (val = "Pending")
        Catch
            Return False
        End Try
    End Function

    Private Function inprogress(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Status"))
            Return (val = "In Progress")
        Catch
            Return False
        End Try
    End Function

    Private Function rejected(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Status"))
            Return (val = "Rejected")
        Catch
            Return False
        End Try
    End Function

    Private Function probation(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Status"))
            Return (val = "Probation")
        Catch
            Return False
        End Try
    End Function

    Private Function active(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "Status"))
            Return (val = "Active")
        Catch
            Return False
        End Try
    End Function

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle
        Dim view As GridView = TryCast(sender, GridView)
        e.Appearance.BackColor = Color.FloralWhite
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        If IsShipToUSCanada(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.FloralWhite
            e.Appearance.ForeColor = Color.Black
        ElseIf rejected(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.PaleVioletRed
            e.Appearance.ForeColor = Color.Bisque
        ElseIf inprogress(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.LightCyan
            e.Appearance.ForeColor = Color.Black
        ElseIf pending(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.AntiqueWhite
            e.Appearance.ForeColor = Color.LightSteelBlue
        ElseIf permanent(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.LightCyan
            e.Appearance.ForeColor = Color.Black
        ElseIf probation(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.PaleVioletRed
            e.Appearance.ForeColor = Color.Bisque
        ElseIf active(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.DodgerBlue
            e.Appearance.ForeColor = Color.BlanchedAlmond
        End If
    End Sub

    Dim addsk As New Addskill

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim querr As String = CStr(query.ExecuteScalar)
        query.CommandText = "select viewprog from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "select count(idrec) from db_skills where idrec = '" & SimpleButton2.Text & "'"
            Dim quer As Integer = CInt(query.ExecuteScalar)
            If quer = 1 Then
                MsgBox("data already exists")
            Else
                query.CommandText = "truncate db_tmpname"
                query.ExecuteNonQuery()
                query.CommandText = "insert into db_tmpname " +
                                    "(EmployeeCode, Name)" +
                                    "values (@employeecode, @Name)"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
                query.Parameters.AddWithValue("@Name", Button5.Text)
                query.ExecuteNonQuery()
                If addsk Is Nothing OrElse addsk.IsDisposed OrElse addsk.MinimizeBox Then
                    addsk.Close()
                    addsk = New Addskill
                End If
                addsk.Show()
            End If
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        ' MsgBox("added")
    End Sub

    Dim pen As New Pending

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "select user from db_temp"
        'Dim quer As String = CStr(query.ExecuteScalar)
        query.CommandText = "select viewprog from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                            "(EmployeeCode, Name)" +
                            "values (@employeecode, @Name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
            query.Parameters.AddWithValue("@Name", Button5.Text)
            query.ExecuteNonQuery()
            If pen Is Nothing OrElse pen.IsDisposed OrElse pen.MinimizeBox Then
                pen.Close()
                pen = New Pending
            End If
            pen.Show()
        Else
            MsgBox("ACCESS DENIED", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub AlertControl1_BeforeFormShow(sender As Object, e As DevExpress.XtraBars.Alerter.AlertFormEventArgs)
        e.AlertForm.OpacityLevel = 1
    End Sub

    Sub test()
        Dim View As GridView = CType(GridControl1.MainView,
      DevExpress.XtraGrid.Views.Grid.GridView)
        If Not View Is Nothing Then
            View.OptionsPrint.ExpandAllDetails = True
            View.ExportToPdf("MainViewData.pdf")
        End If
    End Sub

    Private Sub RibbonControl1_Click(sender As Object, e As EventArgs)

    End Sub
End Class