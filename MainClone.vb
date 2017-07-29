Imports System.IO
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars.Alerter

Public Class MainClone
    Sub autho()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select leveluser from db_user where binary username = @user"
        query.Parameters.AddWithValue("@user", LabelControl2.Text)
        Dim quer As String = CStr(query.ExecuteScalar)
        If quer = "3" Then
            RibbonPageGroup9.Visible = False
        Else
            RibbonPageGroup9.Visible = True
        End If
    End Sub

    Sub mainpageautho()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & LabelControl2.Text & "'"
        Dim tbl As New DataTable
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl)
        For index As Integer = 0 To tbl.Rows.Count - 1
            Dim input As String = tbl.Rows(index).Item(4).ToString
            Dim modify As String = tbl.Rows(index).Item(5).ToString
            Dim delete As String = tbl.Rows(index).Item(6).ToString
            If input = "False" And modify = "False" And delete = "False" Then
                RibbonPageGroup1.Visible = False
            Else
                RibbonPageGroup1.Visible = True
            End If
        Next
    End Sub

    Sub mainpageautho2()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & LabelControl2.Text & "'"
        Dim tbl As New DataTable
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl)
        For index As Integer = 0 To tbl.Rows.Count - 1
            Dim input As String = tbl.Rows(index).Item(9).ToString
            Dim modify As String = tbl.Rows(index).Item(10).ToString
            Dim delete As String = tbl.Rows(index).Item(11).ToString
            Dim leave As String = tbl.Rows(index).Item(12).ToString
            If input = "False" And modify = "False" And delete = "False" And leave = "False" Then
                RibbonPageGroup5.Visible = False
            Else
                RibbonPageGroup5.Visible = True
            End If
        Next
    End Sub

    Sub mainpageautho3()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & LabelControl2.Text & "'"
        Dim tbl As New DataTable
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl)
        For index As Integer = 0 To tbl.Rows.Count - 1
            Dim inputbor As String = tbl.Rows(index).Item(15).ToString
            Dim inputover As String = tbl.Rows(index).Item(16).ToString
            Dim salaryadj As String = tbl.Rows(index).Item(17).ToString
            Dim inputloan As String = tbl.Rows(index).Item(18).ToString
            If inputbor = "False" And inputover = "False" And salaryadj = "False" And inputloan = "False" Then
                RibbonPageGroup6.Visible = False
            Else
                RibbonPageGroup6.Visible = True
            End If
        Next
    End Sub

    Sub mainpageautho6()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & LabelControl2.Text & "'"
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        For i As Integer = 0 To tab.Rows.Count - 1
            If tab.Rows(i).Item("leaveemp").ToString = "True" And tab.Rows(i).Item("inputovertime").ToString = "True" And tab.Rows(i).Item("inputattendance").ToString = "True" And tab.Rows(i).Item("inputholidays").ToString = "True" Then
                RibbonPageGroup7.Visible = True
            Else
                RibbonPageGroup7.Visible = False
            End If
        Next
    End Sub

    Sub mainpageautho4()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & LabelControl2.Text & "'"
        Dim tbl As New DataTable
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl)
        For index As Integer = 0 To tbl.Rows.Count - 1
            Dim candls As String = tbl.Rows(index).Item(28).ToString
            Dim empls As String = tbl.Rows(index).Item(29).ToString
            Dim attls As String = tbl.Rows(index).Item(30).ToString
            Dim otls As String = tbl.Rows(index).Item(31).ToString
            Dim wls As String = tbl.Rows(index).Item(32).ToString
            Dim leavel As String = tbl.Rows(index).Item(33).ToString
            Dim otl As String = tbl.Rows(index).Item(34).ToString
            Dim lol As String = tbl.Rows(index).Item(35).ToString
            Dim lsl As String = tbl.Rows(index).Item(36).ToString
            Dim pls As String = tbl.Rows(index).Item(37).ToString
            Dim premi As String = tbl.Rows(index).Item(38).ToString
            Dim latel As String = tbl.Rows(index).Item(39).ToString
            Dim absl As String = tbl.Rows(index).Item(40).ToString
            Dim ttl As String = tbl.Rows(index).Item(41).ToString
            Dim pjk As String = tbl.Rows(index).Item(42).ToString
            Dim stl As String = tbl.Rows(index).Item(43).ToString
            If candls = "False" And empls = "False" And attls = "False" And otls = "False" And wls = "False" And leavel = "False" And otl = "False" And lol = "False" And lsl = "False" And pls = "False" And premi = "False" And latel = "False" And absl = "False" And ttl = "False" And pjk = "False" And stl = "False" Then
                RibbonPageGroup8.Visible = False
            Else
                RibbonPageGroup8.Visible = True
            End If
        Next
    End Sub

    Sub updateottype()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_absensi set overtimetype = 'Holiday' where isholiday = '1'"
        query.ExecuteNonQuery()
        query.CommandText = "update db_absensi set overtimetype = 'Regular' where isholiday = '0'"
        query.ExecuteNonQuery()
    End Sub

    Sub updateovertime()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_absensi set isholiday = 1 where dayofweek(tanggal) = 7 or tanggal in (select tgl from db_holiday)"
            query.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Overtime Part " & ex.Message)
        End Try
    End Sub

    Sub updatesunday()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_holiday set reason = 'Sunday OFF' where dayofweek(tgl) = 1"
            query.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("OFF DAY " & ex.Message)
        End Try
    End Sub

    Private Function GetImage1o1() As Image
        Return ImageCollection2.Images(0)
    End Function

    Private Function GetImage1o2() As Image
        Return ImageCollection2.Images(2)
    End Function

    Private Function GetImage1o3() As Image
        Return ImageCollection2.Images(3)
    End Function

    Sub alert()
        Dim btn1 As AlertButton = New AlertButton(GetImage1o1)
        Dim btn2 As AlertButton = New AlertButton(GetImage1o2)
        Dim btn3 As AlertButton = New AlertButton(GetImage1o3)
        btn3.Hint = "Erase Notification"
        btn3.Name = "btnerase"
        btn1.Hint = "Test 2"
        btn1.Name = "btnProg"
        btn2.Hint = "Test"
        btn2.Name = "btnview"
        AlertControl1.Buttons.Add(btn1)
        AlertControl1.Buttons.Add(btn2)
        AlertControl1.Buttons.Add(btn3)
        AddHandler AlertControl1.ButtonClick, AddressOf AlertControl1_ButtonClick
        AddHandler AlertControl1.ButtonDownChanged,
        AddressOf AlertControl1_ButtonDownChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(status) from db_recruitment where status = 'In progress'"
            Dim quer2 As Integer = CInt(query.ExecuteScalar)
            query.CommandText = "select count(interviewdate) from db_recruitment where interviewdate = @d1"
            query.Parameters.AddWithValue("@d1", Date.Now)
            Dim quer1 As Integer = CInt(query.ExecuteScalar)
            query.CommandText = "select notifyrec from db_notification"
            Dim note As String = CStr(query.ExecuteScalar)
            query.CommandText = "select count(*) from db_notification"
            Dim hslnote As Integer = CInt(query.ExecuteScalar)
            query.CommandText = "select * from db_notification where unread = '1' and isignored = '0'"
            Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
            Dim tab As New DataTable
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tab)
            For index As Integer = 0 To tab.Rows.Count - 1
                AlertControl1.Show(Me, "Notifications <br>", "<color=green>" & LabelControl2.Text & "<br>" & tab.Rows(index).Item(0).ToString & "<br>" & tab.Rows(index).Item(1).ToString & "<br>" & tab.Rows(index).Item(2).ToString & "<br></color>")
            Next
            'If hslnote > 1 Then
            '    AlertControl1.Show(Me, "Notifications", "<b><color=green> " & LabelControl2.Text & " you have " & note.ToString & " </color></b>")
            'Else
            '    AlertControl1.Show(Me, "Notifications", "<color=green>" & LabelControl2.Text & "</color><br>")
            'End If
            'If quer2 > 1 Then
            '    AlertControl1.Show(Me, "Notifications", "<b><color=green> " & TextBox1.Text & " you have " & note.ToString & " </color></b>")
            '    ' AlertControl1.Show(Me, "Notifications", "<b><color=green> " & TextBox1.Text & " you have " & quer2.ToString & " 'In Progress' Status<color=green> and " & quer1.ToString & " interviews scheduled for today </color></b>")
            'Else
            '    AlertControl1.Show(Me, "Notifications", "<color=green>" & TextBox1.Text & "</color><br>")
            'End If
        Catch ex As Exception
            MsgBox("Alert" & ex.Message)
        End Try
    End Sub

    Private Sub AlertControl1_ButtonDownChanged(sender As Object, e As AlertButtonDownChangedEventArgs)

    End Sub

    Private Sub AlertControl1_ButtonClick(sender As Object, e As AlertButtonClickEventArgs)
        If e.ButtonName = "btnProg" Then
            With Me
                .LabelControl2.Text = LabelControl2.Text
                .Show()
            End With
        ElseIf e.ButtonName = "btnerase" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_notification set unread = '0'"
            query.ExecuteNonQuery()
            alert()
            'query.CommandText = "select notify from db_notification"
            '                Dim note As String = CStr(query.ExecuteScalar)
            '               AlertControl1.Show(Me, "Notifications", "<b><color=green> " & TextBox1.Text & " you have " & note.ToString & " </color></b>")
        End If
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim pesan As String
        pesan = CType(MsgBox("Log Off Application?", MsgBoxStyle.YesNo, "Information"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Try
                Dim del As MySqlCommand = SQLConnection.CreateCommand
                del.CommandText = "truncate db_temp"
                del.ExecuteNonQuery()
                del.CommandText = "truncate db_tmpname"
                del.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Try
                'Dim filetodel As String
                'Dim filedel As String = "E:\Backup\teguran.docx"
                'filetodel = "E:\Backup\sp.docx"
                Dim fileteguran As String = "teguran.pdf"
                Dim filesp As String = "sp.pdf"
                'File.Delete(filetodel)
                'File.Delete(filedel)
                File.Delete(fileteguran)
                File.Delete(filesp)
            Catch ex As Exception
            End Try
            SQLConnection.Close()
            Close()
            Login.Close()
            Application.Exit()
        End If
    End Sub

    Private Sub btnSegarkan_Click(ByVal sender As Object, ByVal e As EventArgs)
        loadDataReq()
    End Sub

    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As EventArgs)
        importData()
        updatestats()
    End Sub

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
            MsgBox("There's no data to be imported", MsgBoxStyle.Information)
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
                            cmd.CommandText = "SELECT MID(EmployeeCode, 4, 1) FROM db_pegawai where EmployeeCode = '" & lastcode.ToString & "'"
                            updmon = DirectCast(cmd.ExecuteScalar(), String)
                            If CInt(updmon) <> CInt(mnow) Then
                                tmp = 1
                            Else
                                tmp = lastn + 1
                            End If
                        Catch ex As Exception
                        End Try
                        '1611-0010
                        'Dim actualcode As String = ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
                        Dim actualcode As String = ynow & mnow & "-" & Strings.Right("0000" & tmp, 5)
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
                            MsgBox("Data Succesfully Imported", MsgBoxStyle.Information, "Information")
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

    Public Overridable Property UseEmbeddedNavigator As Boolean
    Public Overridable Property ShowAutoFilterRow As Boolean

    Private Sub loadDataReq()
        GridView1.Columns.Clear()
        Dim table As New DataTable
        Dim sqlCommand As MySqlCommand = SQLConnection.CreateCommand
        Try
            If BarJudul.Caption = "Module Recruitment" Then
                sqlCommand.CommandText = "select Blacklist, IdRec, InterviewTimes as AppliedTimes, FullName,  PlaceOfBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Status, InterviewDate, Position, NickName, ApplicationDate,  City, ZIP, HomeNumber, RecommendedBy, Martial as MartialStatus, CreatedDate from db_recruitment where status != 'Processed'"
            ElseIf BarJudul.Caption = "Module Employee" Then
                sqlCommand.CommandText = "select kim3 from db_user where username = '" & LabelControl2.Text & "'"
                Dim quer1 As String = CStr(sqlCommand.ExecuteScalar)
                sqlCommand.CommandText = "select kim4 from db_user where username= '" & LabelControl2.Text & "'"
                Dim quer22 As String = CStr(sqlCommand.ExecuteScalar)
                sqlCommand.CommandText = "select pantailabu from db_user where username = '" & LabelControl2.Text & "'"
                Dim quer3 As String = CStr(sqlCommand.ExecuteScalar)
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
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
            GridView1.OptionsView.ShowAutoFilterRow = True
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        GridView1.MoveLast()
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
        Dim navigator As ControlNavigator = GridControl1.EmbeddedNavigator
        Try
            navigator.Buttons.Append.Visible = False
            navigator.Buttons.Remove.Visible = False
        Finally
        End Try
    End Sub

    Private needMoveLastRow As Boolean = True
    Dim employees As New NewRec
    Dim newemps As New NewEmp
    Dim proses As New RecProcess
    Dim formed As New ChangeData
    Dim changeem As New ChangeEmp
    Dim loan As New Payments

    Private Sub GridView1_RowLoaded(sender As Object, e As RowEventArgs)
        'Dim view As ColumnView = TryCast(sender, ColumnView)
        'If needMoveLastRow = False Then
        '    view.MoveLast()
        'End If
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        ''Reports.Close()
        'With Reports
        '    .Label3.Text = LabelControl2.Text
        '    .Show()
        'End With
    End Sub

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs)

    End Sub

    Public Function DeleteReq() As Boolean
        Dim sqlCommand As New MySqlCommand
        Dim pesan As String
        pesan = CType(MsgBox("Sure To Delete ?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            sqlCommand.Connection = SQLConnection
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = "DELETE FROM db_recruitment WHERE IdRec = '" & SimpleButton2.Text & "'"
            sqlCommand.ExecuteNonQuery()
            MsgBox("Removed", MsgBoxStyle.Information, "Success")
            GridControl1.RefreshDataSource()
            loadDataReq()
        End If
        Return Nothing
    End Function

    Public Function DeleteEmp() As Boolean
        Try
            Dim sqlCommand As New MySqlCommand
            Dim pesan As String
            pesan = CType(MsgBox("Sure To Delete ?", MsgBoxStyle.YesNo, "Warning"), String)
            If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                sqlCommand.Connection = SQLConnection
                sqlCommand.CommandType = CommandType.Text
                sqlCommand.CommandText = "DELETE FROM db_recruitment WHERE idrec = '" & SimpleButton2.Text & "'"
                sqlCommand.ExecuteNonQuery()
                MsgBox("Removed", MsgBoxStyle.Information, "Success")
                GridControl1.RefreshDataSource()
                loadDataReq()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Return Nothing
    End Function

    Private Sub btnHapus_Click(ByVal sender As Object, ByVal e As EventArgs)
        DeleteEmp()
    End Sub

    Private Sub SimpleButton2_Click_1(sender As Object, e As EventArgs)
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

    Private Function GetImage() As Image
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

    Dim change As New ChangeData

    Private Sub Button5_Click(sender As Object, e As EventArgs)
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

    Dim emp As New EmployeeDetails

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
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
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Dim modify As New ChangeEmp

    Private Sub Button2_Click(sender As Object, e As EventArgs)
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

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select InputEmp from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            With NewEmp
                .Label38.Text = LabelControl2.Text
                .Show()
            End With
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Dim add2 As New NewRec

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select inputrec from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            NewRec.Close()
            With NewRec
                .Label40.Text = LabelControl2.Text
                .Show()
            End With
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Dim cand As New CandidatesDetails

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
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
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Dim pro As New RecProcess

    Private Sub Button7_Click(sender As Object, e As EventArgs)
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

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select warnemp from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "truncate db_tmpname"
            query.ExecuteNonQuery()
            query.CommandText = "insert into db_tmpname " +
                                "(EmployeeCode, name)" +
                                "values (@employeecode, @name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
            query.Parameters.AddWithValue("@Name", Button5.Text)
            query.ExecuteNonQuery()
            Warning.Close()
            With Warning
                .Label8.Text = LabelControl2.Text
                .Show()
            End With
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Dim status As New StatusChange

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
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
            StatusChange.Close()
            With StatusChange
                .Label16.Text = LabelControl2.Text
                .Show()
            End With
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Dim term As New Termination

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
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
            Termination.Close()
            With Termination
                .Label10.Text = LabelControl2.Text
                .Show()
            End With
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Dim chx As New ChangeEmp

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
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
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    'Private Sub Button9_Click(sender As Object, e As EventArgs)
    '    'Dim query As MySqlCommand = SQLConnection.CreateCommand
    '    'query.CommandText = "select inputrec from db_user where username = '" & LabelControl2.Text & "'"
    '    'Dim quer2 As String = CStr(query.ExecuteScalar)
    '    'If quer2 = "True" Then
    '    '    query.CommandText = "truncate db_tmpname"
    '    '    query.ExecuteNonQuery()
    '    '    query.CommandText = "insert into db_tmpname " +
    '    '                    "(EmployeeCode, Name)" +
    '    '                    "values (@employeecode, @Name)"
    '    '    query.Parameters.Clear()
    '    '    query.Parameters.AddWithValue("@EmployeeCode", SimpleButton2.Text)
    '    '    query.Parameters.AddWithValue("@Name", Button5.Text)
    '    '    query.ExecuteNonQuery()
    '    '    ChangeData.Close()
    '    '    With ChangeData
    '    '        .Label37.Text = LabelControl2.Text
    '    '        .Show()
    '    '    End With
    '    'Else
    '    '    MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
    '    'End If
    'End Sub

    'Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
    '    Tile_Control.Close()
    '    With Tile_Control
    '        .TextBox1.Text = LabelControl2.Text
    '        .Show()
    '    End With
    'End Sub

    'Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
    '    Approvement.Close()
    '    With Approvement
    '        .Label2.Text = LabelControl2.Text
    '        .Show()
    '    End With
    'End Sub

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

    Private Function processed(ByVal view As GridView, ByVal row As Integer) As Boolean
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

    Dim addsk As New Addskill

    Dim pen As New Pending

    Private Sub Button13_Click(sender As Object, e As EventArgs)
        Dim query As MySqlCommand = SQLConnection.CreateCommand
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
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Private Sub AlertControl1_BeforeFormShow(sender As Object, e As AlertFormEventArgs)
        e.AlertForm.OpacityLevel = 1
    End Sub

    Private Sub BarButtonItem8_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
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
                MsgBox(ex.Message)
            End Try
        End If
        SQLConnection.Close()
        Close()
        Login.Close()
        StartPages.Close()
        Application.Exit()
    End Sub

    Private Sub BarButtonItem1_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        BarJudul.Caption = "Module Recruitment"
        loadDataReq()
        GridView1.Columns("Blacklist").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
    End Sub

    Private Sub BarButtonItem10_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        SuratDinas.Close()
        With SuratDinas
            .Show()
        End With
    End Sub

    Private Sub BarButtonItem2_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        BarJudul.Caption = "Module Employee"
        loadDataReq()
    End Sub

    Private Sub BarButtonItem3_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Payments.Close()
        With Payments
            .Label7.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub BarButtonItem4_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        ShowAtt.Close()
        With ShowAtt
            .Label2.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub BarButtonItem5_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        Form2.Close()
        With Form2
            .Label3.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub BarButtonItem6_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        Approvement.Close()
        With Approvement
            .Label2.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Private Sub BarButtonItem7_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        Tiles.Close()
        With Tiles
            .TextBox1.Text = LabelControl2.Text
            .Show()
        End With
    End Sub

    Sub mainauthopage5()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & LabelControl2.Text & "'"
        Dim tab As New DataTable
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tab)
        For index As Integer = 0 To tab.Rows.Count - 1
            If tab.Rows(index).Item(22).ToString = "False" And tab.Rows(index).Item(23).ToString = "False" And tab.Rows(index).Item(24).ToString = "False" And
                tab.Rows(index).Item(25).ToString = "False" And tab.Rows(index).Item(26).ToString = "False" And tab.Rows(index).Item("sppdapp").ToString = "False" Then
                RibbonPageGroup9.Visible = False
            Else
                RibbonPageGroup9.Visible = True
            End If
        Next
    End Sub

    Private Sub MainClone_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        autho()
        mainauthopage5()
        mainpageautho()
        mainpageautho2()
        mainpageautho3()
        mainpageautho6()
        mainpageautho4()
        alert()
        updateovertime()
        updatesunday()
    End Sub

    Private Sub GridView1_FocusedRowChanged_1(sender As Object, e As FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            If BarJudul.Caption = "Module Employee" Then
                Dim datatabl As New DataTable
                Dim sqlCommand As New MySqlCommand
                datatabl.Clear()
                Dim param As String = ""
                param = "and EmployeeCode='" + GridView1.GetFocusedRowCellValue("EmployeeCode").ToString() + "'"
                sqlCommand.CommandText = "SELECT EmployeeCode, FullName FROM db_pegawai WHERE 1=1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
                If datatabl.Rows.Count > 0 Then
                    SimpleButton2.Text = datatabl.Rows(0).Item(0).ToString
                    Button5.Text = datatabl.Rows(0).Item(1).ToString
                End If
            ElseIf BarJudul.Caption = "Module Recruitment" Then
                Dim datatabl As New DataTable
                Dim sqlCommand As New MySqlCommand
                datatabl.Clear()
                Dim param As String = ""
                param = "and IdRec='" + GridView1.GetFocusedRowCellValue("IdRec").ToString() + "'"
                sqlCommand.CommandText = "SELECT IdRec , FullName FROM db_recruitment WHERE 1=1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
                If datatabl.Rows.Count > 0 Then
                    SimpleButton2.Text = datatabl.Rows(0).Item(0).ToString
                    Button5.Text = datatabl.Rows(0).Item(1).ToString
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button11_Click_1(sender As Object, e As EventArgs) Handles Button11.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select viewprog from db_user where username = '" & LabelControl2.Text & "'"
        Dim quer2 As String = CStr(query.ExecuteScalar)
        If quer2 = "True" Then
            query.CommandText = "select count(idrec) from db_skills where idrec = '" & SimpleButton2.Text & "'"
            Dim quer As Integer = CInt(query.ExecuteScalar)
            If quer = 1 Then
                MsgBox("data already exists", MsgBoxStyle.Information)
            Else
                Addskill.Close()
                With Addskill
                    .txtname.Text = Button5.Text
                    .txtidrecc.Text = SimpleButton2.Text
                    .Show()
                End With
            End If
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Private Sub GridView1_PopupMenuShowing_1(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
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
        If BarJudul.Caption = "Module Recruitment" Then
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
                If quer = "Pending" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    e.Menu.Items.Add(New DXMenuItem("Change status to be In Progress", New EventHandler(AddressOf Button13_Click), GetImage3()))
                    e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button9_Click_1), GetImage8))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                ElseIf quer = "In Progress" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    e.Menu.Items.Add(New DXMenuItem("Add Skills", New EventHandler(AddressOf Button11_Click_1), GetImage12))
                    e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button9_Click_1), GetImage8))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                ElseIf quer = "Rejected" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    e.Menu.Items.Add(New DXMenuItem("Modify...", New EventHandler(AddressOf Button9_Click_1), GetImage8))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                ElseIf quer = "Processed" Then
                    e.Menu.Items.Add(New DXMenuItem("Add Candidates", New EventHandler(AddressOf Button4_Click), GetImage1))
                    e.Menu.Items.Add(New DXMenuItem("View Candidates", New EventHandler(AddressOf Button6_Click), GetImage2))
                    e.Menu.Items.Add(New DXMenuItem("Refresh..", New EventHandler(AddressOf btnSegarkan_Click), GetImage9))
                    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf btnHapus_Click), getimage13))
                End If
            End If
        ElseIf BarJudul.Caption = "Module Employee" Then
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
                e.Menu.Items.Add(New DXMenuItem("Terminate This Employee ?", New EventHandler(AddressOf SimpleButton2_Click), GetImage))
            End If
        End If
    End Sub

    Private Sub GridView1_RowLoaded_1(sender As Object, e As RowEventArgs) Handles GridView1.RowLoaded
        Dim view As ColumnView = TryCast(sender, ColumnView)
        If needMoveLastRow = False Then
            view.MoveLast()
        End If
    End Sub

    Private Sub MainClone_FormClosing_1(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'WindowState = FormWindowState.Minimized
        Me.Hide()
        loadDataReq()
        e.Cancel = True
    End Sub

    Private Sub GridView1_RowCellStyle_1(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        If processed(GridView1, e.RowHandle) Then
            e.Appearance.BackColor = Color.FloralWhite
            e.Appearance.ForeColor = Color.Black
        End If
        Dim View As GridView = CType(sender, GridView)
        If e.Column.FieldName = "Status" Or e.Column.FieldName = "Status" Then
            Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Status"))
            If category = "Rejected" Then
                e.Appearance.BackColor = Color.LightCyan
                e.Appearance.BackColor2 = Color.Red
            ElseIf category = "Active" Then
                e.Appearance.BackColor = Color.DeepSkyBlue
                e.Appearance.BackColor2 = Color.LightCyan
            ElseIf category = "InProgress" Then
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.White
            ElseIf category = "Permanent" Then
                e.Appearance.BackColor = Color.LightBlue
                e.Appearance.BackColor = Color.Cyan
            ElseIf category = "Pending" Then
                e.Appearance.BackColor = Color.LightSeaGreen
                e.Appearance.BackColor2 = Color.LightGreen
            ElseIf category = "In Progress" Then
                e.Appearance.BackColor = Color.Cornsilk
                e.Appearance.BackColor2 = Color.Cyan
            End If
        ElseIf e.Column.FieldName = "EmployeeCode" Then
            e.Appearance.BackColor = Color.Yellow
            e.Appearance.BackColor2 = Color.LightYellow
        ElseIf e.Column.FieldName = "IdRec" Then
            e.Appearance.BackColor = Color.Yellow
            e.Appearance.BackColor2 = Color.LightYellow
        ElseIf e.Column.FieldName = "Blacklist" Then
            Dim cek As Boolean = CBool(e.CellValue)
            If cek = True Then
                e.Appearance.BackColor = Color.Red
                e.Appearance.BorderColor = Color.Magenta
            Else
                e.Appearance.BackColor = Color.Wheat
            End If
        End If
    End Sub

    Private Sub GridView1_RowStyle_1(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle
        Dim view As GridView = TryCast(sender, GridView)
        e.Appearance.BackColor = Color.FloralWhite
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles Button9.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
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
        Else
            MsgBox("Forbidden Access", MsgBoxStyle.Exclamation, "DENIED")
        End If
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
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
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Sub BarButtonItem11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem11.ItemClick
        MsgBox("Under Construction", MsgBoxStyle.Information)
    End Sub
End Class