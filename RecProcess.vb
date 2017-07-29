Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu

Public Class RecProcess

    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()

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
        connectionString = "Server=" + host + "; User Id=" + id + "; Password=" + password + "; Database=" + db + ""
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

    Sub clearal()
        txtid.Text = ""
        txtinterview.Text = ""
        txtaddress.Text = ""
        txtphone.Text = ""
    End Sub

    Sub cleartxt()
        txtid.Text = ""
        txtinterview.Text = ""
        txtaddress.Text = ""
        txtphone.Text = ""
        txtidcard.Text = ""
        txtstatus.Text = ""
        txtinterviewdate.Text = ""
    End Sub

    Public Sub skills()
        Dim sqlCommand As New MySqlCommand
        Dim str_carSql As String
        Dim sk As MySqlCommand = SQLConnection.CreateCommand
        sk.CommandText = "select idrec from db_skills where idrec = '" & txtidrecc.Text & "'"
        Dim sk1 As String = CStr(sk.ExecuteScalar)
        If sk1 = "" Then
            If skill1.Text = "" OrElse skill2.Text = "" OrElse skill3.Text = "" OrElse skill4.Text = "" OrElse skill5.Text = "" OrElse txtidrecc.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                Try
                    str_carSql = "INSERT INTO db_skills " +
                           "(IdRec, FullName, Skill1, Skill2, Skill3, Skill4, Skill5, Interviewer) " +
                           "values (@Idrec, @fullname, @skill1, @skill2, @skill3, @skill4, @skill5, @interviewer)"
                    sqlCommand.Connection = SQLConnection
                    sqlCommand.CommandText = str_carSql
                    sqlCommand.Parameters.AddWithValue("@IdRec", txtidrecc.Text)
                    sqlCommand.Parameters.AddWithValue("@FullName", txtname.Text)
                    sqlCommand.Parameters.AddWithValue("@skill1", skill1.Text)
                    sqlCommand.Parameters.AddWithValue("@skill2", skill2.Text)
                    sqlCommand.Parameters.AddWithValue("@skill3", skill3.Text)
                    sqlCommand.Parameters.AddWithValue("@skill4", skill4.Text)
                    sqlCommand.Parameters.AddWithValue("@skill5", skill5.Text)
                    sqlCommand.Parameters.AddWithValue("@interviewer", TextEdit1.Text)
                    sqlCommand.ExecuteNonQuery()
                    MessageBox.Show("Data Succesfully Added!")
                    txtname.Text = ""
                    txtidrecc.Text = ""
                    txtstat.Text = ""
                    skill1.Text = ""
                    skill2.Text = ""
                    skill3.Text = ""
                    skill4.Text = ""
                    skill5.Text = ""
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            MsgBox("The data already exists")
        End If
    End Sub

    Public Sub updatechange()
        Dim dtb As DateTime
        txtinterviewdate.Format = DateTimePickerFormat.Custom
        txtinterviewdate.CustomFormat = "yyyy-MM-dd"
        dtb = txtinterviewdate.Value
        Dim cmd As MySqlCommand = SQLConnection.CreateCommand
        cmd.CommandText = "Update db_recruitment set interviewtimes = @it, FullName = @fn, Address = @address, PhoneNumber = @pn, IdNumber = @in, status = @status, interviewdates = @id where idrec = '" & txtid.Text & "'"
        cmd.Parameters.AddWithValue("@it", txtinterview.Text)
        cmd.Parameters.AddWithValue("@fn", txtfullname.Text)
        cmd.Parameters.AddWithValue("@address", txtaddress.Text)
        cmd.Parameters.AddWithValue("@pn", txtphone.Text)
        cmd.Parameters.AddWithValue("@in", txtidcard.Text)
        cmd.Parameters.AddWithValue("@status", txtstatus.Text)
        cmd.Parameters.AddWithValue("@id", dtb.ToString("yyyy-MM-dd"))
        cmd.ExecuteNonQuery()
        NotifyIcon1.Visible = True
        NotifyIcon1.Icon = SystemIcons.Information
        NotifyIcon1.BalloonTipTitle = "Recruitment Process"
        NotifyIcon1.BalloonTipText = "Data Changed"
        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        NotifyIcon1.ShowBalloonTip(9000000)
    End Sub

    Public Sub updatechange2()
        Dim dtb As DateTime
        txtinterviewdate.Format = DateTimePickerFormat.Custom
        txtinterviewdate.CustomFormat = "yyyy-MM-dd"
        dtb = txtinterviewdate.Value
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_recruitment SET" +
                                     " InterviewTimes = @InterviewTimes" +
                                     ", FullName = @FullName" +
                                     ", Address = @Address" +
                                     ", PhoneNumber = @PhoneNumber" +
                                     ", IdNumber = @IdNumber" +
                                     ", Status = @Status" +
                                     ", InterviewDate = @InterviewDate" +
                                     " WHERE IdRec = @IdRec"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@IdRec", txtid.Text)
            sqlcommand.Parameters.AddWithValue("@InterviewTimes", txtinterview.Text)
            sqlcommand.Parameters.AddWithValue("@FullName", txtfullname.Text)
            sqlcommand.Parameters.AddWithValue("@Address", txtaddress.Text)
            sqlcommand.Parameters.AddWithValue("@PhoneNumber", txtphone.Text)
            sqlcommand.Parameters.AddWithValue("@IdNumber", txtidcard.Text)
            sqlcommand.Parameters.AddWithValue("@Status", txtstatus.Text)
            sqlcommand.Parameters.AddWithValue("@InterviewDate", dtb.ToString("yyyy-MM-dd"))
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MessageBox.Show("Data Successfully Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Dim tbl_par As New DataTable

    Sub loadDataKaryawan()
        Dim sqlCommand As New MySqlCommand
        sqlCommand.CommandText = "SELECT IdRec, InterviewTimes, FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Photo, status, InterviewDate, Cv, Reason FROM db_recruitment where status = 'Pending'"
        sqlCommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par)
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtfullname.Properties.Items.Add(tbl_par.Rows(index).Item(2).ToString())
        Next
    End Sub

    Dim tbl_par12 As New DataTable

    Private Sub txtfullname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtfullname.SelectedIndexChanged
        For indexing As Integer = 0 To tbl_par.Rows.Count - 1
            If txtfullname.SelectedItem Is tbl_par.Rows(indexing).Item(2).ToString() Then
                txtid.Text = tbl_par.Rows(indexing).Item(0).ToString()
                txtinterview.Text = tbl_par.Rows(indexing).Item(1).ToString()
                txtaddress.Text = tbl_par.Rows(indexing).Item(5).ToString()
                txtphone.Text = tbl_par.Rows(indexing).Item(8).ToString()
                txtidcard.Text = tbl_par.Rows(indexing).Item(9).ToString()
                txtstatus.Text = tbl_par.Rows(indexing).Item(11).ToString
            End If
        Next
    End Sub

    Private Sub loadinfo()
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select FullName from db_recruitment where status = 'Pending'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
        End Try
    End Sub

    Private Sub loadinfo2()
        GridControl2.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select a.FullName from db_recruitment a where status = 'In Progress' and a.idrec not in(SELECT b.idrec from db_skills b)"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl2.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RecProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        loadinfo()
        loadinfo2()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        cleartxt()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        cleartxt()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)

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

    Dim view As New pdfviewer

    Private Sub btnView_Click(sender As Object, e As EventArgs)
        If view Is Nothing OrElse view.IsDisposed Then
            view = New pdfviewer
        End If
        view.Show()
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs)
        For indexing As Integer = 0 To tbl_par12.Rows.Count - 1
            If txtfullname.SelectedItem Is tbl_par12.Rows(indexing).Item(2).ToString() Then
                txtid.Text = tbl_par12.Rows(indexing).Item(0).ToString()
                txtinterview.Text = tbl_par12.Rows(indexing).Item(1).ToString()
                txtphone.Text = tbl_par12.Rows(indexing).Item(3).ToString()
                txtidcard.Text = tbl_par12.Rows(indexing).Item(4).ToString()
                txtstatus.Text = tbl_par12.Rows(indexing).Item(5).ToString
            End If
        Next
    End Sub

    Private Sub SimpleButton2_Click_1(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If txtfullname.Text = "" OrElse txtid.Text = "" Then
            MsgBox("there's no candidate selected")
        Else
            GroupControl2.Visible = True
        End If
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Dim mess As String
        Dim down As MySqlCommand = SQLConnection.CreateCommand
        Dim dtb As DateTime
        txtinterviewdate.Format = DateTimePickerFormat.Custom
        txtinterviewdate.CustomFormat = "yyyy-MM-dd"
        dtb = txtinterviewdate.Value
        down.CommandText = "select fullname from db_recruitment where idrec = '" & txtid.Text & "'"
        Dim downres As String = CStr(down.ExecuteScalar)
        mess = CType(MsgBox("Are you sure to change " & downres & " to be In Progress ?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Dim up As MySqlCommand = SQLConnection.CreateCommand
            up.CommandText = "update db_recruitment set" +
                                " status = @stat" +
                                ", InterviewDates = @idates" +
                                " where idrec = @ic"
            up.Parameters.AddWithValue("@ic", txtid.Text)
            up.Parameters.AddWithValue("@stat", "In Progress")
            up.Parameters.AddWithValue("@idates", dtb.ToString("yyyy-MM-dd"))
            up.ExecuteNonQuery()
            MsgBox("Status from " & downres & " Is changed to be In progress")
            GroupControl2.Visible = False
            clear()
        End If
        loadinfo()
        loadinfo2()
    End Sub

    Sub clear()
        txtfullname.Text = ""
        txtid.Text = ""
        txtidcard.Text = ""
        txtinterview.Text = ""
        txtaddress.Text = ""
        txtphone.Text = ""
        txtpob.Text = ""
        txtage.Text = ""
        txtgender.Text = ""
        txtstatus.Text = ""
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And FullName='" + GridView1.GetFocusedRowCellValue("FullName").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT IdRec, InterviewTimes, FullName, PlaceofBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Photo, Status, InterviewDate, PhoneNumber, Reason FROM db_recruitment WHERE 1=1 " + param.ToString()
            sqlCommand.Connection = SQLConnection

            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If datatabl.Rows.Count > 0 Then
            txtfullname.Text = datatabl.Rows(0).Item(2).ToString()
            txtid.Text = datatabl.Rows(0).Item(0).ToString()
            txtinterview.Text = datatabl.Rows(0).Item(1).ToString()
            txtidcard.Text = datatabl.Rows(0).Item(9).ToString()
            txtaddress.Text = datatabl.Rows(0).Item(5).ToString()
            txtdob.Text = datatabl.Rows(0).Item(4).ToString
            txtphone.Text = datatabl.Rows(0).Item(8).ToString()
            txtinterviewdate.Text = datatabl.Rows(0).Item(12).ToString()
            txtgender.Text = datatabl.Rows(0).Item(6).ToString
            txtpob.Text = datatabl.Rows(0).Item(3).ToString
            txtstatus.Text = datatabl.Rows(0).Item(11).ToString()
        End If
    End Sub


    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick

    End Sub

    Dim dt1 As Date
    Dim dt2 As Date
    Dim dt3 As TimeSpan
    Dim diff As Double

    Private Sub txtdob_ValueChanged(sender As Object, e As EventArgs) Handles txtdob.ValueChanged
        dt1 = CDate(txtdob.Value.ToShortDateString)
        dt2 = Date.Now
        dt3 = (dt2 - dt1)
        diff = dt3.Days
        txtage.Text = Str(Int(diff / 365))
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridControl1.RefreshDataSource()
            Dim table As New DataTable
            Dim sqlcommand As New MySqlCommand
            Try
                sqlcommand.CommandText = "select FullName from db_recruitment where FullName Like '%" + TextBox1.Text + "%' and status = 'Pending'"
                sqlcommand.Connection = SQLConnection
                Dim tbl_par As New DataTable
                Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(table)
                GridControl1.DataSource = table
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub SimpleButton3_Click_1(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        GroupControl2.Visible = False
    End Sub

    Private Sub GridView2_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And FullName='" + GridView2.GetFocusedRowCellValue("FullName").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT FullName, IdRec, Status FROM db_recruitment WHERE 1=1 " + param.ToString()
            sqlCommand.Connection = SQLConnection

            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox("4" & ex.Message & "'")
        End Try
        If datatabl.Rows.Count > 0 Then
            txtname.Text = datatabl.Rows(0).Item(0).ToString()
            txtidrecc.Text = datatabl.Rows(0).Item(1).ToString()
            txtstat.Text = datatabl.Rows(0).Item(2).ToString()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridControl2.RefreshDataSource()
            Dim table As New DataTable
            Dim sqlcommand As New MySqlCommand
            Try
                sqlcommand.CommandText = "select a.FullName from db_recruitment a where a.FullName Like '%" + TextBox2.Text + "%' and a.status = 'In Progress' and a.idrec not in(SELECT b.idrec from db_skills b)"
                sqlcommand.Connection = SQLConnection
                Dim tbl_par As New DataTable
                Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(table)
                GridControl2.DataSource = table
            Catch ex As Exception
                MsgBox("5" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        skills()
        loadinfo2()
        loadinfo()
    End Sub

    Private Sub button2_Click_1(sender As Object, e As EventArgs) Handles button2.Click
        GridControl2.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select a.FullName from db_recruitment a where a.FullName Like '%" + TextBox2.Text + "%' and a.status = 'In Progress' and a.idrec not in(SELECT b.idrec from db_skills b)"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl2.DataSource = table
        Catch ex As Exception
            MsgBox("6")
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub button1_Click_1(sender As Object, e As EventArgs) Handles button1.Click
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select FullName from db_recruitment where FullName Like '%" + TextBox1.Text + "%' and status = 'Pending'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
            MsgBox("7")
            'MsgBox(ex.Message)
        End Try
    End Sub

    Dim sel As New selectemp

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Timer1.Start()
        If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
            sel.Close()
            sel = New selectemp
        End If
        sel.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit1.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        Timer1.Stop()
    End Sub
End Class