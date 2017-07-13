Imports System.IO

Public Class Circle
    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()
    Dim host As String
    Dim id As String
    Dim password As String
    Dim db As String

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
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

    Private Sub Circle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TransparencyKey = Color.White
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Size = New Size(500, 500)
        Dim path As New Drawing2D.GraphicsPath
        path.AddEllipse(85, 0, 245, 280)
        Region = New Region(path)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        GroupControl1.Visible = True
        GroupControl2.Visible = False
    End Sub

    Private Sub btnProg_Click(sender As Object, e As EventArgs) Handles btnProg.Click
        GroupControl1.Visible = False
        GroupControl2.Visible = True
    End Sub

    Private Sub Circle_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        GroupControl1.Visible = False
        GroupControl2.Visible = False
    End Sub

    Private Sub btnLihat_Click(sender As Object, e As EventArgs) Handles btnLihat.Click
        Dim pesan As String
        pesan = CType(MsgBox("Log Off Application?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Try
                Dim del As MySqlCommand = SQLConnection.CreateCommand
                del.CommandText = "truncate db_hasil"
                del.ExecuteNonQuery()
                Dim dele As MySqlCommand = SQLConnection.CreateCommand
                dele.CommandText = "truncate db_temp"
                dele.Parameters.Clear()
                dele.ExecuteNonQuery()

                dele.CommandText = "truncate db_tmpname"
                dele.Parameters.Clear()
                dele.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            SQLConnection.Close()
            Close()
            Login.Close()
        End If
    End Sub

    Dim table As DataTable
    Dim employees As New NewRec

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

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        importData()
        updatestats()
    End Sub

    'Dim infoForm As New infoReq

    'Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
    '    If infoForm Is Nothing OrElse infoForm.IsDisposed OrElse infoForm.MinimizeBox Then
    '        infoForm.Close()
    '        infoForm = New infoReq
    '    End If
    '    infoForm.Show()
    'End Sub

    'Dim employeenotes As New Notes

    'Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
    '    If employeenotes Is Nothing OrElse employeenotes.IsDisposed OrElse employeenotes.MinimizeBox Then
    '        employeenotes.Close()
    '        employeenotes = New Notes
    '    End If
    '    employeenotes.Show()
    'End Sub

    'Dim spform As New SPForms

    Dim war As New Warning

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If war Is Nothing OrElse war.IsDisposed OrElse war.MinimizeBox Then
            war.Close()
            war = New Warning
        End If
        war.Show()
    End Sub

    'Dim rotasi As New RotasiMutasi

    Dim st As New StatusChange

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        If st Is Nothing OrElse st.IsDisposed OrElse st.MinimizeBox Then
            st.Close()
            st = New StatusChange
        End If
        st.Show()
    End Sub

    Dim proses As New RecProcess
    Dim formed As New ChangeData
    Dim changeem As New ChangeEmp

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        If formed Is Nothing OrElse formed.IsDisposed OrElse formed.MinimizeBox Then
            formed.Close()
            formed = New ChangeData
        End If
        formed.Show()
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        If changeem Is Nothing OrElse changeem.IsDisposed OrElse changeem.MinimizeBox Then
            formed.Close()
            changeem = New ChangeEmp
        End If
        changeem.Show()
    End Sub

    Dim newemps As New NewEmp

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If employees Is Nothing OrElse employees.IsDisposed OrElse employees.MinimizeBox Then
            employees.Close()
            employees = New NewRec
        End If
        employees.Show()
        employees.BarButtonItem1.PerformClick()
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        If newemps Is Nothing OrElse newemps.IsDisposed OrElse newemps.MinimizeBox Then
            newemps.Close()
            newemps = New NewEmp
        End If
        newemps.Show()
        newemps.BarButtonItem1.PerformClick()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If proses Is Nothing OrElse proses.IsDisposed OrElse proses.MinimizeBox Then
            proses.Close()
            proses = New RecProcess
        End If
        proses.Show()
    End Sub

    Private Sub SimpleButton10_Click_1(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        If MainApp Is Nothing OrElse MainApp.MinimizeBox Then
            MainApp.Close()
        End If
        MainApp.Show()
    End Sub

    Sub loaded()
        MainApp.GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlCommand As New MySqlCommand
        Try
            If MainApp.barJudul.Caption = "Module Recruitment" Then
                sqlCommand.CommandText = "select IdRec as IDRecruitment, InterviewTimes as AppliedTimes, FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Status, InterviewDate, Reason, Position, ExpectedSalary, NickName, ApplicationDate, Weight, Height, BloodType, City, ZIP, HomeNumber, RecommendedBy, Martial as MartialStatus, LastSalary, OtherIncome, ExpSalary as ExpectedSalary, ExpFacilities as ExpectedFacilities, FavoriteJob, Reference, CreatedDate from db_recruitment"
            ElseIf mainapp.barJudul.Caption = "Module Employee" Then
                sqlCommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, Email, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, Weight, Height, BloodType, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai"
            End If
            sqlCommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            MainApp.GridControl1.DataSource = table
        Catch ex As Exception
        End Try
    End Sub

    Dim att As New Attendances

    Sub loadattendance()
        att.GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        Try
            sqlcommand.CommandText = "select a.EmployeeCode, a.FullName, a.Tanggal as Date, a.Shift, DATE_FORMAT(a.JamMulai, '%H:%i:%s') as SignIn, DATE_FORMat(a.JamSelesai, '%H:%i:%s') as SignOut, b.EmployeeType From db_absensi a, db_pegawai b where a.EmployeeCode = b.EmployeeCode and a.Tanggal = @date1"
            sqlcommand.Parameters.AddWithValue("@date", att.DateTimePicker2.Value.Date)
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            att.GridControl1.DataSource = table
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        loaded()
        loadattendance()
        GroupControl1.Visible = False
        GroupControl2.Visible = False
    End Sub

    Private Sub btnNew_MouseHover(sender As Object, e As EventArgs) Handles btnNew.MouseHover
        ToolTip1.SetToolTip(btnNew, "Recruitment")
    End Sub

    Private Sub btnProg_MouseHover(sender As Object, e As EventArgs) Handles btnProg.MouseHover
        ToolTip1.SetToolTip(btnProg, "Employee")
    End Sub

    Private Sub SimpleButton2_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton2.MouseHover
        ToolTip1.SetToolTip(SimpleButton2, "Add New Recruitment")
    End Sub

    Private Sub btnChange_MouseHover(sender As Object, e As EventArgs) Handles btnChange.MouseHover
        ToolTip1.SetToolTip(btnChange, "Change Recruitment Data")
    End Sub

    Private Sub SimpleButton1_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton1.MouseHover
        ToolTip1.SetToolTip(SimpleButton3, "See Progress")
    End Sub

    Private Sub SimpleButton4_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton4.MouseHover
        ToolTip1.SetToolTip(SimpleButton4, "See Candidates Details")
    End Sub

    Private Sub SimpleButton6_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton6.MouseHover
        ToolTip1.SetToolTip(SimpleButton6, "Add New Employee")
    End Sub

    Private Sub SimpleButton7_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton7.MouseHover
        ToolTip1.SetToolTip(SimpleButton7, "Change Employee Data")
    End Sub

    Private Sub SimpleButton3_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton3.MouseHover
        ToolTip1.SetToolTip(SimpleButton3, "Warning Notice")
    End Sub

    Private Sub SimpleButton5_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton5.MouseHover
        ToolTip1.SetToolTip(SimpleButton5, "See Employee Details")
    End Sub

    Private Sub SimpleButton8_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton8.MouseHover
        ToolTip1.SetToolTip(SimpleButton8, "Status Change")
    End Sub

    Private Sub SimpleButton9_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton9.MouseHover
        ToolTip1.SetToolTip(SimpleButton9, "Import recruitment data which already accepted to Employee Module")
    End Sub

    Private Sub SimpleButton10_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton10.MouseHover
        ToolTip1.SetToolTip(SimpleButton10, "Go to mainpage")
    End Sub

    Private Sub btnLihat_MouseHover(sender As Object, e As EventArgs) Handles btnLihat.MouseHover
        ToolTip1.SetToolTip(btnLihat, "Log Off from application")
    End Sub

    Private Sub SimpleButton11_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton11.MouseHover
        ToolTip1.SetToolTip(SimpleButton11, "Refresh Recruitment and Employee Data")
    End Sub

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs) Handles SimpleButton12.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub SimpleButton12_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton12.MouseHover
        ToolTip1.SetToolTip(SimpleButton12, "Minimize")
    End Sub

    Private Sub SimpleButton13_MouseHover(sender As Object, e As EventArgs) Handles SimpleButton13.MouseHover
        ToolTip1.SetToolTip(SimpleButton13, "Termination")
    End Sub

    Dim tmnt As New Termination

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        If tmnt Is Nothing OrElse tmnt.IsDisposed OrElse tmnt.MinimizeBox Then
            tmnt.Close()
            tmnt = New Termination
        End If
        tmnt.Show()
    End Sub


    'Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

    'End Sub
End Class