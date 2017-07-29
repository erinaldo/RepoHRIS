Imports System.IO
Imports DevExpress.Utils.Menu

Public Class selectemp
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

    'Dim att As New Attendance

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "and EmployeeCode='" + GridView1.GetFocusedRowCellValue("EmployeeCode").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT FUllName, EmployeeCode, Photo, Position, Status FROM db_pegawai WHERE 1=1 " + param.ToString()
            sqlCommand.Connection = SQLConnection

            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If datatabl.Rows.Count > 0 Then
            Label2.Text = datatabl.Rows(0).Item(0).ToString()
            Label3.Text = datatabl.Rows(0).Item(1).ToString
            Dim filefoto As Byte() = CType(datatabl.Rows(0).Item(2), Byte())
            If filefoto.Length > 0 Then
                PictureBox1.Image = ByteToImage(filefoto)
            Else
                PictureBox1.Image = Nothing
                PictureBox1.Refresh()
            End If
            Label4.Text = datatabl.Rows(0).Item(3).ToString
            Label5.Text = datatabl.Rows(0).Item(4).ToString
        End If
    End Sub

    'Sub recordss()
    '    Dim rec As MySqlCommand = SQLConnection.CreateCommand
    '    rec.CommandText = "select count(*) from db_pegawai"
    '    txtrec1.Text = CStr(rec.ExecuteScalar)
    'End Sub

    Sub loaddata()
        GridControl1.RefreshDataSource()
        GridControl1.UseEmbeddedNavigator = True
        Dim table As New DataTable
        Dim str As String = "EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated'"
        Dim sqlcommand As New MySqlCommand
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select kim3 from db_user where username = '" & Label6.Text & "'"
            Dim quer1 As String = CStr(query.ExecuteScalar)
            query.CommandText = "select kim4 from db_user where username= '" & Label6.Text & "'"
            Dim quer22 As String = CStr(query.ExecuteScalar)
            query.CommandText = "select pantailabu from db_user where username = '" & Label6.Text & "'"
            Dim quer3 As String = CStr(query.ExecuteScalar)
            If quer1 = "True" And quer22 = "False" And quer3 = "False" Then
                sqlcommand.CommandText = "select " & str & " And officelocation = 'KIM 3'"
            ElseIf quer1 = "True" And quer22 = "True" And quer3 = "False" Then
                sqlcommand.CommandText = "select " & str & " and officelocation <> 'Pantai Labu' and officelocation != '<empty>'"
            ElseIf quer1 = "True" And quer22 = "False" And quer3 = "True" Then
                sqlcommand.CommandText = "select " & str & " and officelocation <> 'KIM 4' and officelocation != '<empty>'"
            ElseIf quer1 = "False" And quer22 = "True" And quer3 = "True" Then
                sqlcommand.CommandText = "select " & str & " and officelocation <> 'KIM 3' and officelocation != '<empty>'"
            ElseIf quer1 = "False" And quer22 = "False" And quer3 = "True" Then
                sqlcommand.CommandText = "select " & str & " and officelocation = 'Pantai Labu'"
            ElseIf quer1 = "False" And quer22 = "True" And quer3 = "False" Then
                sqlcommand.CommandText = "select " & str & " and officelocation = 'KIM 4'"
            Else
                sqlcommand.CommandText = "select " & str
            End If
            'If quer1 = "True" And quer22 = "False" And quer3 = "False" Then
            '    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation = 'KIM 3'"
            'ElseIf quer1 = "True" And quer22 = "True" And quer3 = "False" Then
            '    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation <> 'Pantai Labu' and officelocation != '<empty>'"
            'ElseIf quer1 = "True" And quer22 = "False" And quer3 = "True" Then
            '    sqlcommand.CommandText = "Select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation <> 'KIM 4' and officelocation != '<empty>'"
            'ElseIf quer1 = "False" And quer22 = "True" And quer3 = "True" Then
            '    sqlcommand.CommandText = "Select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation <> 'KIM 3' and officelocation != '<empty>'"
            'ElseIf quer1 = "False" And quer22 = "False" And quer3 = "True" Then
            '    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation = 'Pantai Labu'"
            'ElseIf quer1 = "False" And quer22 = "True" And quer3 = "False" Then
            '    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated' and officelocation = 'KIM 4'"
            'Else
            '    sqlcommand.CommandText = "select EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Status, EmployeeType, NickName, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks from db_pegawai where status != 'Terminate' and  status != 'Terminated'"
            'End If
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
            MsgBox("Filter OfficeLocation  " & ex.Message)
        End Try
    End Sub

    Public Overridable Property UseEmbeddedNavigator As Boolean

    Private Sub selectemp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        loaddata()
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        loaddata()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Dim del As MySqlCommand = SQLConnection.CreateCommand
        del.CommandText = "truncate db_tmpname"
        del.ExecuteNonQuery()
        Dim name As MySqlCommand = SQLConnection.CreateCommand
        name.CommandText = "insert into db_tmpname" +
                            "(name, employeecode, jobtitle, status)" +
                            "Values(@name, @employeecode, @jobs, @stats)"
        name.Parameters.AddWithValue("@name", Label2.Text)
        name.Parameters.AddWithValue("@Employeecode", Label3.Text)
        name.Parameters.AddWithValue("@jobs", Label4.Text)
        name.Parameters.AddWithValue("@stats", Label5.Text)
        name.ExecuteNonQuery()
        Close()
    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Add(New DXMenuItem("Select Employee", New EventHandler(AddressOf SimpleButton3_Click), GetImage))
        End If
    End Sub
End Class