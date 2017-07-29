Imports System.IO
Public Class ChangeEmp

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

    Sub loadalldata()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "SELECT * from db_pegawai where employeecode = '" & TextBox1.Text & "'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par33)
        For index As Integer = 0 To tbl_par33.Rows.Count - 1
        Next
    End Sub

    Sub autochange()
        For index As Integer = 0 To tbl_par33.Rows.Count - 1
            TextBox2.Text = tbl_par33.Rows(index).Item(2).ToString
            txtnick.Text = tbl_par33.Rows(index).Item(18).ToString
            ComboBoxEdit6.Text = tbl_par33.Rows(index).Item(6).ToString
            txtbp.Text = tbl_par33.Rows(index).Item(4).ToString
            txtjoin.Text = tbl_par33.Rows(index).Item(11).ToString
            txtbod.Text = tbl_par33.Rows(index).Item(5).ToString
            txtrel.Text = tbl_par33.Rows(index).Item(7).ToString
            txtidno.Text = tbl_par33.Rows(index).Item(9).ToString
            txtblood.Text = tbl_par33.Rows(index).Item(21).ToString
            txtkg.Text = tbl_par33.Rows(index).Item(19).ToString
            txtcm.Text = tbl_par33.Rows(index).Item(20).ToString
            txtphoneno.Text = tbl_par33.Rows(index).Item(12).ToString
            txtwemail.Text = tbl_par33.Rows(index).Item(22).ToString
            txtpemail.Text = tbl_par33.Rows(index).Item(23).ToString
            txtadd.Text = tbl_par33.Rows(index).Item(8).ToString
            txtrecby.Text = tbl_par33.Rows(index).Item(24).ToString
            txtjob.Text = tbl_par33.Rows(index).Item(3).ToString
            txtcompany.Text = tbl_par33.Rows(index).Item(1).ToString
            txtofloc.Text = tbl_par33.Rows(index).Item(10).ToString
            txtgroup.Text = tbl_par33.Rows(index).Item(25).ToString
            txtdept.Text = tbl_par33.Rows(index).Item(26).ToString
            txttype.Text = tbl_par33.Rows(index).Item(15).ToString
            txtjobdesk.Text = tbl_par33.Rows(index).Item(27).ToString
            ComboBoxEdit2.Text = tbl_par33.Rows(index).Item(28).ToString
            ComboBoxEdit7.Text = tbl_par33.Rows(index).Item(29).ToString
            ComboBoxEdit1.Text = tbl_par33.Rows(index).Item(14).ToString
            CheckEdit1.Checked = CType(tbl_par33.Rows(index).Item(30).ToString, Boolean)
            DateTimePicker2.Text = tbl_par33.Rows(index).Item(31).ToString
            TextBox3.Text = tbl_par33.Rows(index).Item(32).ToString
            CheckEdit2.Checked = CType(tbl_par33.Rows(index).Item(33).ToString, Boolean)
            CheckEdit3.Checked = CType(tbl_par33.Rows(index).Item(34).ToString, Boolean)
            CheckEdit4.Checked = CType(tbl_par33.Rows(index).Item(35).ToString, Boolean)
            CheckEdit5.Checked = CType(tbl_par33.Rows(index).Item(36).ToString, Boolean)
            CheckEdit6.Checked = CType(tbl_par33.Rows(index).Item(37).ToString, Boolean)
            CheckEdit7.Checked = CType(tbl_par33.Rows(index).Item(38).ToString, Boolean)
            ComboBoxEdit9.Text = tbl_par33.Rows(index).Item(39).ToString
            Dim filefoto As Byte() = CType(tbl_par33.Rows(0).Item(13), Byte())
            If filefoto.Length > 0 Then
                PictureBox1.Image = ByteToImage(filefoto)
            Else
                PictureBox1.Image = Nothing
                PictureBox1.Refresh()
            End If
            npwp.Text = tbl_par33.Rows(index).Item(41).ToString
            TextEdit1.Text = tbl_par33.Rows(index).Item(44).ToString
            TextEdit2.Text = tbl_par33.Rows(index).Item(45).ToString
            TextEdit5.Text = tbl_par33.Rows(index).Item(46).ToString
            TextEdit6.Text = tbl_par33.Rows(index).Item(47).ToString
        Next
    End Sub

    Dim tbl_par1, tbl_par2, tbl_par33 As New DataTable

    Sub loaddata1()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "SELECT departmentname from db_departmentmbp where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par2)
        For index As Integer = 0 To tbl_par2.Rows.Count - 1
            txtdept.Properties.Items.Add(tbl_par2.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub loaddata()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select groupname from db_groupmbp where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par1)
        For index As Integer = 0 To tbl_par1.Rows.Count - 1
            txtgroup.Properties.Items.Add(tbl_par1.Rows(index).Item(0).ToString())
        Next
    End Sub

    Dim tbl_par6, tbl_par3, tbl_par4, tbl_par5, tbl_par7 As New DataTable

    Sub loadjob()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select JobTitle from db_jobtitle where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par6)
        For index As Integer = 0 To tbl_par6.Rows.Count - 1
            txtjob.Properties.Items.Add(tbl_par6.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub loadcomp()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select CompanyCode from db_companycode where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par3)
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
            txtcompany.Properties.Items.Add(tbl_par3.Rows(index).Item(0).ToString())
        Next
    End Sub


    Sub loadofloc()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select OfficeLocation from db_officelocation where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par4)
        For index As Integer = 0 To tbl_par4.Rows.Count - 1
            txtofloc.Properties.Items.Add(tbl_par4.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub loadgroup()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select groupname from db_groupmbp where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par5)
        For index As Integer = 0 To tbl_par5.Rows.Count - 1
            txtgroup.Properties.Items.Add(tbl_par5.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub loaddept()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select DepartmentName from db_departmentmbp where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par7)
        For index As Integer = 0 To tbl_par7.Rows.Count - 1
            txtdept.Properties.Items.Add(tbl_par7.Rows(index).Item(0).ToString())
        Next
    End Sub

    Private Sub ChangeEmp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextBox2.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            TextBox1.Text = quer2.ToString
        Catch ex As Exception
        End Try
        loadalldata()
        autochange()
        loaddata()
        loaddata1()
        loadjob()
        loadgroup()
        loadofloc()
        loaddept()
        loadcomp()
        cert()
        benefit()
        skill()
        fam()
        edu()
        exp()
        GridView1.BestFitColumns()
        GridView2.BestFitColumns()
        GridView3.BestFitColumns()
        GridView4.BestFitColumns()
        GridView5.BestFitColumns()
        GridView6.BestFitColumns()
        act = "input"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        Label23.Text = "file.pdf"
    End Sub

    Public Sub empbackup()
        Dim use As MySqlCommand = SQLConnection.CreateCommand
        use.CommandText = "select user from db_temp"
        Dim user As String = CStr(use.ExecuteScalar)

        Dim dtb, dtr, dtu, dta As DateTime
        txtbod.Format = DateTimePickerFormat.Custom
        txtbod.CustomFormat = "yyyy-MM-dd"
        dtb = txtbod.Value
        txtjoin.Format = DateTimePickerFormat.Custom
        txtjoin.CustomFormat = "yyyy-MM-dd"
        dtr = txtjoin.Value
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtu = DateTimePicker2.Value
        txtbod.Format = DateTimePickerFormat.Custom
        txtbod.CustomFormat = "yyyy-MM-dd"
        dta = txtbod.Value
        Dim lastn As Integer
        Dim ynow As String = Format(Now, "yy").ToString
        Dim mnow As String = Month(Now).ToString
        Try
            Dim cmd = SQLConnection.CreateCommand()
            cmd.CommandText = "SELECT last_num FROM view_emp_last_code"
            lastn = DirectCast(cmd.ExecuteScalar(), Integer) + 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim rescode As String = ynow & "-" & mnow & "-" & Strings.Right("00000" & lastn, 5)
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        Try
            sqlcommand.CommandText = "insert into db_historyemp " +
                            "(EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Photo, Status, EmployeeType, TerminateDate, ChangeDate, NickName, Weight, Height, BloodType, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks, MaritalStatus, PphStatus, IsExpiry, ExpiryDates, ApprovedBy, ExcludePayroll, PayCash, ProcessTax, ExcludeThr, ExcludeBonus, PrintSlip, PayrollInterval, ChangeBy, MemilikiNpwp, NoBpjs, NoNpwp, fingerid) " +
                            "values (@EmployeeCode, @CompanyCode, @FullName, @Position, @PlaceOfBirth, @DateOfBirth, @Gender, @Religion, @Address, @IdNumber, @OfficeLocation, @WorkDate, @PhoneNumber, @Photo, @Status, @EmployeeType, @TerminateDate, @ChangeDate, @NickName, @Weight, @Height, @BloodType, @WorkEmail, @PrivateEmail, @RecommendedBy, @Grouping, @Department, @Jobdesks, @MaritalStatus, @PphStatus, @IsExpiry, @ExpiryDates, @ApprovedBy, @ExcludePayroll, @PayCash, @ProcessTax, @ExcludeThr, @ExcludeBonus, @PrintSlip, @PayrollInterval, @ChangeBy, @npwp, @nobpjs, @nonpwp, @fingerid)"
            sqlcommand.Parameters.AddWithValue("@EmployeeCode", TextBox1.Text)
            sqlcommand.Parameters.AddWithValue("@CompanyCode", txtcompany.Text)
            sqlcommand.Parameters.AddWithValue("@FullName", TextBox2.Text)
            sqlcommand.Parameters.AddWithValue("@Position", txtjob.Text)
            sqlcommand.Parameters.AddWithValue("@PlaceOfBirth", txtbp.Text)
            sqlcommand.Parameters.AddWithValue("@DateOfBirth", dta.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@Gender", ComboBoxEdit6.Text)
            sqlcommand.Parameters.AddWithValue("@Religion", txtrel.Text)
            sqlcommand.Parameters.AddWithValue("@Address", txtadd.Text)
            sqlcommand.Parameters.AddWithValue("@IdNumber", txtidno.Text)
            sqlcommand.Parameters.AddWithValue("@OfficeLocation", txtofloc.Text)
            sqlcommand.Parameters.AddWithValue("@WorkDate", txtjoin.Value.Date)
            sqlcommand.Parameters.AddWithValue("@PhoneNumber", txtphoneno.Text)
            If Not ImageEdit1.Text Is Nothing Then
                Dim param As New MySqlParameter("@Photo", ImageToByte(PictureBox1))
                sqlcommand.Parameters.Add(param)
            Else
                sqlcommand.Parameters.AddWithValue("@Photo", "")
            End If
            sqlcommand.Parameters.AddWithValue("@Status", ComboBoxEdit1.Text)
            sqlcommand.Parameters.AddWithValue("@EmployeeType", txttype.Text)
            sqlcommand.Parameters.AddWithValue("@TerminateDate", Nothing)
            sqlcommand.Parameters.AddWithValue("@ChangeDate", Date.Now)
            sqlcommand.Parameters.AddWithValue("@NickName", txtnick.Text)
            sqlcommand.Parameters.AddWithValue("@Weight", txtkg.Text)
            sqlcommand.Parameters.AddWithValue("@Height", txtcm.Text)
            sqlcommand.Parameters.AddWithValue("@BloodType", txtblood.Text)
            sqlcommand.Parameters.AddWithValue("@WorkEmail", txtwemail.Text)
            sqlcommand.Parameters.AddWithValue("@PrivateEmail", txtpemail.Text)
            sqlcommand.Parameters.AddWithValue("@RecommendedBy", txtrecby.Text)
            sqlcommand.Parameters.AddWithValue("@Grouping", txtgroup.Text)
            sqlcommand.Parameters.AddWithValue("@Department", txtdept.Text)
            sqlcommand.Parameters.AddWithValue("@Jobdesks", txtjobdesk.Text)
            sqlcommand.Parameters.AddWithValue("@MaritalStatus", ComboBoxEdit2.Text)
            sqlcommand.Parameters.AddWithValue("@pphStatus", ComboBoxEdit7.Text)
            sqlcommand.Parameters.AddWithValue("@IsExpiry", CheckEdit1.Checked)
            sqlcommand.Parameters.AddWithValue("@ExpiryDates", "")
            sqlcommand.Parameters.AddWithValue("@ApprovedBy", TextBox3.Text)
            sqlcommand.Parameters.AddWithValue("@ExcludePayroll", CheckEdit2.Checked)
            sqlcommand.Parameters.AddWithValue("@PayCash", CheckEdit3.Checked)
            sqlcommand.Parameters.AddWithValue("@ProcessTax", CheckEdit4.Checked)
            sqlcommand.Parameters.AddWithValue("@ExcludeThr", CheckEdit5.Checked)
            sqlcommand.Parameters.AddWithValue("@ExcludeBonus", CheckEdit6.Checked)
            sqlcommand.Parameters.AddWithValue("@PrintSlip", CheckEdit7.Checked)
            sqlcommand.Parameters.AddWithValue("@PayrollInterval", ComboBoxEdit9.Text)
            sqlcommand.Parameters.AddWithValue("@ChangeBy", Label37.Text)
            sqlcommand.Parameters.AddWithValue("@npwp", npwp.Text)
            sqlcommand.Parameters.AddWithValue("@nobpjs", TextEdit3.Text)
            sqlcommand.Parameters.AddWithValue("@nonpwp", TextEdit1.Text)
            sqlcommand.Parameters.AddWithValue("@fingerid", TextEdit2.Text)
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateEmp()
        Dim dtb, dta As Date
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtb = DateTimePicker2.Value

        txtbod.Format = DateTimePickerFormat.Custom
        txtbod.CustomFormat = "yyyy-MM-dd"
        dta = txtbod.Value
        Dim sqlCommand As New MySqlCommand
        Dim str_carSql As String
        ' Try
        Dim hasil As Date
        If CheckEdit1.Checked = True Then
            hasil = CDate(dtb.ToString("yyyy-MM-dd"))
        Else
            hasil = CDate(Nothing)
        End If
        str_carSql = "UPDATE db_pegawai SET" +
                   " CompanyCode = @CompanyCode" +
                   ", FullName = @FullName" +
                   ", Position = @Position" +
                   ", PlaceOfBirth = @PlaceOfBirth" +
                   ", DateOfBirth = @DateOfBirth" +
                   ", Gender = @Gender" +
                   ", Religion = @Religion" +
                   ", Address = @Address" +
                   ", IdNumber = @IdNumber" +
                   ", OfficeLocation = @OfficeLocation" +
                   ", WorkDate = @WorkDate" +
                   ", PhoneNumber = @PhoneNumber" +
                   ", Photo = @Photo" +
                   ", Status = @Status" +
                   ", EmployeeType = @EmployeeType " +
                   ", TerminateDate = @TerminateDate" +
                   ", ChangeDate = @ChangeDate" +
                   ", NickName = @NickName" +
                   ", Weight = @Weight" +
                   ", Height = @height" +
                   ", BloodType = @BloodType" +
                   ", WorkEmail = @WorkEmail" +
                   ", PrivateEmail = @PrivateEmail" +
                   ", RecommendedBy = @RecommendedBy" +
                   ", Grouping = @Grouping" +
                   ", Department = @Department" +
                   ", Jobdesks = @Jobdesks" +
                   ", MaritalStatus = @maritalStatus" +
                   ", PphStatus = @PphStatus" +
                   ", IsExpiry = @IsExpiry" +
                   ", ExpiryDates = @ExpiryDates " +
                   ", ApprovedBy = @ApprovedBy " +
                   ", ExcludePayroll = @ExcludePayroll " +
                   ", PayCash = @PayCash " +
                   ", ProcessTax = @ProcessTax " +
                   ", ExcludeThr = @ExcludeThr " +
                   ", ExcludeBonus = @ExcludeBonus " +
                   ", PrintSlip = @Printslip" +
                   ", PayrollInterval = @payrollInterval " +
                   ", MemilikiNpwp = @npwp" +
                   ", Nobpjs = @nobpjs" +
                   ", Nonpwp = @nonpwp" +
                   ", fingerid = @fingerid" +
                   ", directhead = @directhead" +
                   ", undirecthead = @undirecthead" +
                   " WHERE EmployeeCode = @EmployeeCode"
        sqlCommand.Connection = SQLConnection
        sqlCommand.CommandText = str_carSql
        sqlCommand.Parameters.AddWithValue("@EmployeeCode", TextBox1.Text)
        sqlCommand.Parameters.AddWithValue("@CompanyCode", txtcompany.Text)
        sqlCommand.Parameters.AddWithValue("@FullName", TextBox2.Text)
        sqlCommand.Parameters.AddWithValue("@Position", txtjob.Text)
        sqlCommand.Parameters.AddWithValue("@PlaceOfBirth", txtbp.Text)
        sqlCommand.Parameters.AddWithValue("@DateOfBirth", dta.ToString("yyyy-MM-dd"))
        sqlCommand.Parameters.AddWithValue("@Gender", ComboBoxEdit6.Text)
        sqlCommand.Parameters.AddWithValue("@Religion", txtrel.Text)
        sqlCommand.Parameters.AddWithValue("@Address", txtadd.Text)
        sqlCommand.Parameters.AddWithValue("@IdNumber", txtidno.Text)
        sqlCommand.Parameters.AddWithValue("@OfficeLocation", txtofloc.Text)
        sqlCommand.Parameters.AddWithValue("@WorkDate", txtjoin.Value.Date)
        sqlCommand.Parameters.AddWithValue("@PhoneNumber", txtphoneno.Text)
        If Not ImageEdit1.Text Is Nothing Then
            Dim param As New MySqlParameter("@Photo", ImageToByte(PictureBox1))
            sqlCommand.Parameters.Add(param)
        Else
            sqlCommand.Parameters.AddWithValue("@Photo", "")
        End If
        sqlCommand.Parameters.AddWithValue("@Status", ComboBoxEdit1.Text)
        sqlCommand.Parameters.AddWithValue("@EmployeeType", txttype.Text)
        sqlCommand.Parameters.AddWithValue("@TerminateDate", Nothing)
        sqlCommand.Parameters.AddWithValue("@ChangeDate", Date.Now)
        sqlCommand.Parameters.AddWithValue("@NickName", txtnick.Text)
        sqlCommand.Parameters.AddWithValue("@Weight", txtkg.Text)
        sqlCommand.Parameters.AddWithValue("@Height", txtcm.Text)
        sqlCommand.Parameters.AddWithValue("@BloodType", txtblood.Text)
        sqlCommand.Parameters.AddWithValue("@WorkEmail", txtwemail.Text)
        sqlCommand.Parameters.AddWithValue("@PrivateEmail", txtpemail.Text)
        sqlCommand.Parameters.AddWithValue("@RecommendedBy", txtrecby.Text)
        sqlCommand.Parameters.AddWithValue("@Grouping", txtgroup.Text)
        sqlCommand.Parameters.AddWithValue("@Department", txtdept.Text)
        sqlCommand.Parameters.AddWithValue("@Jobdesks", txtjobdesk.Text)
        sqlCommand.Parameters.AddWithValue("@MaritalStatus", ComboBoxEdit2.Text)
        sqlCommand.Parameters.AddWithValue("@pphStatus", ComboBoxEdit7.Text)
        sqlCommand.Parameters.AddWithValue("@IsExpiry", CheckEdit1.Checked)
        sqlCommand.Parameters.AddWithValue("@ExpiryDates", dtb.ToString("yyyy-MM-dd"))
        sqlCommand.Parameters.AddWithValue("@ApprovedBy", TextBox3.Text)
        sqlCommand.Parameters.AddWithValue("@ExcludePayroll", CheckEdit2.Checked)
        sqlCommand.Parameters.AddWithValue("@PayCash", CheckEdit3.Checked)
        sqlCommand.Parameters.AddWithValue("@ProcessTax", CheckEdit4.Checked)
        sqlCommand.Parameters.AddWithValue("@ExcludeThr", CheckEdit5.Checked)
        sqlCommand.Parameters.AddWithValue("@ExcludeBonus", CheckEdit6.Checked)
        sqlCommand.Parameters.AddWithValue("@PrintSlip", CheckEdit7.Checked)
        sqlCommand.Parameters.AddWithValue("@PayrollInterval", ComboBoxEdit9.Text)
        sqlCommand.Parameters.AddWithValue("@npwp", npwp.Text)
        'Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
        'Dim finfo As New FileInfo(Label23.Text)
        'Dim numBytes As Long = finfo.Length
        'Dim fstream As New FileStream(Label23.Text, FileMode.Open, FileAccess.Read)
        'Dim br As New BinaryReader(fstream)
        'Dim data As Byte() = br.ReadBytes(CInt(numBytes))
        'br.Close()
        'fstream.Close()
        'If Not data Is Nothing Then
        '    sqlCommand.Parameters.AddWithValue("@Jobdesk", data)
        'Else
        '    sqlCommand.Parameters.AddWithValue("@Jobdesk", "")
        'End If
        sqlCommand.Parameters.AddWithValue("@nobpjs", TextEdit3.Text)
        sqlCommand.Parameters.AddWithValue("@nonpwp", TextEdit1.Text)
        sqlCommand.Parameters.AddWithValue("@fingerid", TextEdit2.Text)
        sqlCommand.Parameters.AddWithValue("@directhead", TextEdit5.Text)
        sqlCommand.Parameters.AddWithValue("@undirecthead", TextEdit6.Text)
        sqlCommand.Connection = SQLConnection
        sqlCommand.ExecuteNonQuery()
        MsgBox("Data Successfully Changed", MsgBoxStyle.Information)
        Close()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Dim dt1 As Date
    Dim dt2 As Date
    Dim dt3 As TimeSpan
    Dim diff As Double

    Private Sub txtjoin_ValueChanged(sender As Object, e As EventArgs) Handles txtjoin.ValueChanged
        Dim bulan, tahun As String
        dt1 = CDate(txtjoin.Value.ToShortDateString)
        dt2 = Date.Now
        dt3 = (dt2 - dt1)
        diff = dt3.Days
        tahun = CStr(Int(diff / 365))
        Dim tmpmonth As String = CStr(Int(diff Mod 365))
        bulan = CStr(Int(CInt(tmpmonth) / 30))
        Dim tmpdays As String = CStr(Int(CInt(tmpmonth) Mod 30))
        txtwork.Text = tahun & " Y" & " " & bulan & " M" & " " & tmpdays & " D"
    End Sub

    Private Sub txtbod_ValueChanged(sender As Object, e As EventArgs) Handles txtbod.ValueChanged
        dt1 = CDate(txtbod.Value.ToShortDateString)
        dt2 = Date.Now
        dt3 = (dt2 - dt1)
        diff = dt3.Days
        txtage.Text = Str(Int(diff / 365))
    End Sub

    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        UpdateEmp()
        empbackup()
    End Sub

    Dim act As String = ""

    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs)
        txtschoolname.Text = ""
        txtyears.Text = ""
        Label29.Text = ""
        txtmajor.Text = ""
        act = "input"
    End Sub

    Dim tablecer As New DataTable

    Public Sub cert()
        tablecer.Clear()
        ComboBoxEdit4.Properties.Items.Clear()
        ComboBoxEdit4.Text = ""
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, EmployeeCode, Certificates, Years, Reasons from db_certificates where EmployeeCode = '" & TextBox1.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tablecer)
            GridControl2.DataSource = tablecer
            For index As Integer = 0 To tablecer.Rows.Count - 1
                ComboBoxEdit4.Properties.Items.Add(tablecer.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
        txtcertificate.Text = ""
        txtyears.Text = ""
        txtreason.Text = ""
    End Sub

    Sub certificates()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_certificates set " +
                                    " Certificates = @certificates " +
                                    ", Years = @Years" +
                                    ", Reasons = @reasons" +
                                    " where noid = @noid"
            Else
                cmmd.CommandText = "insert into db_certificates " +
                                    "(EmployeeCode, Certificates, Years, Reasons)" +
                                    " values (@EmployeeCode, @Certificates, @Years, @Reasons)"
            End If
            cmmd.Parameters.AddWithValue("@EmployeeCode", TextBox1.Text)
            cmmd.Parameters.AddWithValue("@noid", Label29.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@Certificates", txtcertificate.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@Certificates", ComboBoxEdit4.Text)
            End If
            cmmd.Parameters.AddWithValue("@Years", txtyear.Text)
            cmmd.Parameters.AddWithValue("@Reasons", txtreason.Text)
            cmmd.ExecuteNonQuery()
            If act = "input" Then
                MsgBox("Added")
            ElseIf act = "edit" Then
                MsgBox("Changed")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cert()
    End Sub

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs)
        txtcertificate.Text = ""
        txtyear.Text = ""
        txtreason.Text = ""
        act = "input"
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        txtmember.Text = ""
        txtrelation.Text = ""
        txtmemgender.Text = ""
        txtmemadd.Text = ""
        txtocc.Text = ""
        txtmemph.Text = ""
        act = "input"
    End Sub

    Private Sub SimpleButton14_Click(sender As Object, e As EventArgs) Handles SimpleButton14.Click
        txtjobtitle.Text = ""
        txtcompanyname.Text = ""
        txtmanagername.Text = ""
        txtcompadd.Text = ""
        txtbasic.Text = ""
        txtaddi.Text = ""
        txttotalsa.Text = ""
        txtreasonquit.Text = ""
        act = "input"
    End Sub

    Private Sub SimpleButton15_Click(sender As Object, e As EventArgs)
        skillname.Text = ""
        skilllevel.Text = ""
        skilldesc.Text = ""
        act = "input"
    End Sub

    Sub school()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_education set" +
                                    " school = @school" +
                                    ", graduatedyear = @graduatedyear" +
                                    ", studyfield = @studyfield" +
                                    " where NoId = @NoId"
            Else
                cmmd.CommandText = "insert into db_education " +
                            "(EmployeeCode, School, GraduatedYear, StudyField) " +
                            "values (@EmployeeCode, @School, @GraduatedYear, @StudyField)"
            End If
            cmmd.Parameters.AddWithValue("@NoId", Label29.Text)
            cmmd.Parameters.AddWithValue("@EmployeeCode", TextBox1.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@School", txtschoolname.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@School", ComboBoxEdit3.Text)
            End If
            cmmd.Parameters.AddWithValue("@GraduatedYear", txtyears.Text)
            cmmd.Parameters.AddWithValue("@StudyField", txtmajor.Text)
            cmmd.ExecuteNonQuery()
            If act = "input" Then
                MsgBox("Added")
            ElseIf act = "edit" Then
                MsgBox("Edit")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        edu()
    End Sub

    Sub family()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_family set " +
                                    " MemberName = @MemberName" +
                                    ", Gender = @gender" +
                                    ", Address = @Address" +
                                    ", Relationship = @Relationship" +
                                    ", Occupation = @Occupation" +
                                    ", PhoneNo = @PhoneNo" +
                                    " where NoId = @Noid"
            Else
                cmmd.CommandText = "insert into db_family " +
                          "(EmployeeCode, Relationship, MemberName, Gender, Address, Occupation, PhoneNo)" +
                          " values (@EmployeeCode,@Relationship, @MemberName, @Gender, @Address, @Occupation, @PhoneNo)"
            End If
            cmmd.Parameters.AddWithValue("@Noid", Label29.Text)
            cmmd.Parameters.AddWithValue("@EmployeeCode", TextBox1.Text)
            cmmd.Parameters.AddWithValue("@Relationship", txtrelation.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@MemberName", txtmember.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@MemberName", ComboBoxEdit5.Text)
            End If
            cmmd.Parameters.AddWithValue("@Gender", txtmemgender.Text)
            cmmd.Parameters.AddWithValue("@Address", txtmemadd.Text)
            cmmd.Parameters.AddWithValue("@Occupation", txtocc.Text)
            cmmd.Parameters.AddWithValue("@PhoneNo", txtmemph.Text)
            cmmd.ExecuteNonQuery()
            If act = "input" Then
                MsgBox("Added")
            ElseIf act = "edit" Then
                MsgBox("Changed")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        fam()
    End Sub

    Dim tablefam As New DataTable

    Public Sub fam()
        tablefam.Clear()
        ComboBoxEdit5.Properties.Items.Clear()
        ComboBoxEdit5.Text = ""
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, EmployeeCode, MemberName, Relationship, Gender, Address, Occupation, PhoneNo from db_family where EmployeeCode = '" & TextBox1.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tablefam)
            GridControl4.DataSource = tablefam
            For index As Integer = 0 To tablefam.Rows.Count - 1
                ComboBoxEdit5.Properties.Items.Add(tablefam.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        txtmember.Text = ""
        txtmemgender.Text = ""
        txtmemadd.Text = ""
        txtocc.Text = ""
        txtmemph.Text = ""
        txtrelation.Text = ""
    End Sub

    Dim tableedu As New DataTable

    Public Sub edu()
        tableedu.Clear()
        ComboBoxEdit3.Properties.Items.Clear()
        ComboBoxEdit3.Text = ""
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, EmployeeCode, School as SchoolOrUniversity, GraduatedYear, StudyField from db_education where EmployeeCode = '" & TextBox1.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tableedu)
            GridControl3.DataSource = tableedu
            For index As Integer = 0 To tableedu.Rows.Count - 1
                ComboBoxEdit3.Properties.Items.Add(tableedu.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
        End Try
        txtschoolname.Text = ""
        Label29.Text = ""
        txtyears.Text = ""
        txtmajor.Text = ""
    End Sub

    Sub insertexp()
        Dim dtb, dtr As Date
        txtperiod.Format = DateTimePickerFormat.Custom
        txtperiod.CustomFormat = "yyyy-MM-dd"
        dtb = txtperiod.Value
        txtuntil.Format = DateTimePickerFormat.Custom
        txtuntil.CustomFormat = "yyyy-MM-dd"
        dtr = txtuntil.Value
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_exp set " +
                                    " Position = @Position" +
                                    ", Company = @Company" +
                                    ", Manager = @Manager" +
                                    ", Address = @Address" +
                                    ", Period = @Period" +
                                    ", Until = @Until" +
                                    ", BasicSalary = @BasicSalary " +
                                    ", AdditionalSalary = @AdditionalSalary" +
                                    ", TotalSalary = @TotalSalary" +
                                    ", QuitReason = @QuitReason " +
                                    " where noid = @NoId"
            Else
                cmmd.CommandText = "insert into db_exp " +
                           "(EmployeeCode, Position, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason)" +
                           " values (@EmployeeCode, @Company, @Position, @Manager, @Address, @Period, @Until, @BasicSalary, @AdditionalSalary, @TotalSalary, @QuitReason)"
            End If
            cmmd.Parameters.AddWithValue("@EmployeeCode", TextBox1.Text)
            cmmd.Parameters.AddWithValue("@NoId", Label29.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@Position", txtjobtitle.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@Position", ComboBoxEdit8.Text)
            End If
            cmmd.Parameters.AddWithValue("@Company", txtcompanyname.Text)
            cmmd.Parameters.AddWithValue("@Manager", txtmanagername.Text)
            cmmd.Parameters.AddWithValue("@Address", txtcompadd.Text)
            cmmd.Parameters.AddWithValue("@Period", dtb.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@Until", dtr.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@BasicSalary", txtbasic.Text)
            cmmd.Parameters.AddWithValue("@AdditionalSalary", txtaddi.Text)
            cmmd.Parameters.AddWithValue("@TotalSalary", txttotalsa.Text)
            cmmd.Parameters.AddWithValue("@QuitReason", txtreasonquit.Text)
            cmmd.ExecuteNonQuery()
            If act = "edit" Then
                MsgBox("Changed")
            Else
                MsgBox("Added")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        txtcompanyname.Text = ""
        txtmanagername.Text = ""
        txtcompadd.Text = ""
        txtbasic.Text = ""
        txtaddi.Text = ""
        txttotalsa.Text = ""
        txtreasonquit.Text = ""
        exp()
    End Sub

    Dim tableexp As New DataTable

    Public Sub exp()
        tableexp.Clear()
        ComboBoxEdit8.Properties.Items.Clear()
        ComboBoxEdit8.Text = ""
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, EmployeeCode, Position, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason from db_exp where EmployeeCode = '" & TextBox1.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tableexp)
            GridControl5.DataSource = tableexp
            For index As Integer = 0 To tableexp.Rows.Count - 1
                ComboBoxEdit8.Properties.Items.Add(tableexp.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
        End Try
        txtjobtitle.Text = ""
        txtcompanyname.Text = ""
        txtmanagername.Text = ""
        txtcompadd.Text = ""
        txtbasic.Text = ""
        txtaddi.Text = ""
        txttotalsa.Text = ""
        txtreasonquit.Text = ""
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        If act = "input" Or act = "edit" Then
            If txtmajor.Text = "" Or txtyears.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                school()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_education where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            edu()
        End If
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        If act = "input" Or act = "edit" Then
            If txtyear.Text = "" Or txtreason.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                certificates()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_certificates where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            cert()
        End If
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        If act = "input" Or act = "edit" Then
            If txtrelation.Text = "" Or txtmemgender.Text = "" Or txtmemadd.Text = "" Or txtocc.Text = "" Or txtmemph.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                family()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_family where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            fam()
        End If
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        If act = "input" Or act = "edit" Then
            If txtcompanyname.Text = "" Or txtmanagername.Text = "" Or txtbasic.Text = "" Or txtaddi.Text = "" Or txtreasonquit.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                insertexp()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_exp where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            exp()
        End If
    End Sub

    Sub insertbenefit()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                query.CommandText = "update db_benefits set benefit = @benefit,  description = @description where noid = @noid"
                query.Parameters.AddWithValue("@noid", Label29.Text)
                query.Parameters.AddWithValue("@benefit", ComboBoxEdit11.Text)
                query.Parameters.AddWithValue("@description", RichTextBox1.Text)
                query.ExecuteNonQuery()
                MsgBox("Changed")
            Else
                query.CommandText = "insert into db_benefits (EmployeeCode, Benefit, description) values (@employeecode, @benefit, @description)"
                query.Parameters.Clear()
                query.Parameters.AddWithValue("@employeecode", TextBox1.Text)
                query.Parameters.AddWithValue("@benefit", TextEdit4.Text)
                query.Parameters.AddWithValue("@description", RichTextBox1.Text)
                query.ExecuteNonQuery()
                MsgBox("Added")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        benefit()
    End Sub

    Sub insertskill()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_empskill set" +
                                   " skillname = @skillname" +
                                   ", skilllevel = @skilllevel" +
                                   ", skilldescription = @skilldescription" +
                                   " where noid = @NoId"
            Else
                cmmd.CommandText = "insert into db_empskill " +
                           "(EmployeeCode, SkillName, SkillLevel, SKillDescription) " +
                           "values (@EmployeeCode, @SkillName, @SkillLevel, @SkillDescription)"
            End If
            cmmd.Parameters.AddWithValue("@NoId", Label29.Text)
            cmmd.Parameters.AddWithValue("@EmployeeCode", TextBox1.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@SkillName", skillname.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@SkillName", ComboBoxEdit10.Text)
            End If
            cmmd.Parameters.AddWithValue("@SkillLevel", skilllevel.Text)
            cmmd.Parameters.AddWithValue("@SkillDescription", skilldesc.Text)
            cmmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
            If act = "input" Then
                MsgBox("Added")
            ElseIf act = "edit" Then
                MsgBox("edit")
            End If
        End Try
        skill()
    End Sub

    Dim tableben As New DataTable

    Public Sub benefit()
        tableben.Clear()
        ComboBoxEdit11.Properties.Items.Clear()
        ComboBoxEdit1.Text = ""
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, EmployeeCode, Benefit, Description from db_benefits where employeecode = '" & TextBox1.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tableben)
            GridControl1.DataSource = tableben
            For index As Integer = 0 To tableben.Rows.Count - 1
                ComboBoxEdit11.Properties.Items.Add(tableben.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
        End Try
        TextEdit4.Text = ""
        RichTextBox1.Text = ""
    End Sub

    Dim tablesk As New DataTable

    Public Sub skill()
        tablesk.Clear()
        ComboBoxEdit10.Properties.Items.Clear()
        ComboBoxEdit10.Text = ""
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, EmployeeCode, SkillName, SkillLevel, SkillDescription from db_empskill where employeecode = '" & TextBox1.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tablesk)
            GridControl6.DataSource = tablesk
            For index As Integer = 0 To tablesk.Rows.Count - 1
                ComboBoxEdit10.Properties.Items.Add(tablesk.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
        End Try
        skillname.Text = ""
        skilllevel.Text = ""
        skilldesc.Text = ""
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        If act = "input" Or act = "edit" Then
            If skilldesc.Text = "" Or skilllevel.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                insertskill()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_empskill where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            skill()
        End If
    End Sub

    Private Sub txtbasic_EditValueChanged(sender As Object, e As EventArgs) Handles txtbasic.EditValueChanged
        Try
            Dim a, b, c As Integer
            a = Convert.ToInt32(txtaddi.Text)
            b = Convert.ToInt32(txtbasic.Text)
            c = a + b
            txttotalsa.Text = c.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtaddi_EditValueChanged(sender As Object, e As EventArgs) Handles txtaddi.EditValueChanged
        Try
            Dim a, b, c As Integer
            a = Convert.ToInt32(txtaddi.Text)
            b = Convert.ToInt32(txtbasic.Text)
            c = a + b
            txttotalsa.Text = c.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub OpenPreviewWindows()
        Try
            Dim iHeight As Integer = PictureBox1.Height
            Dim iWidth As Integer = PictureBox1.Width
            hHwnd = capCreateCaptureWindowA((iDevice), WS_VISIBLE Or WS_CHILD, 0, 0, 640, 480, PictureBox1.Handle.ToInt32, 0)
            Try
                If SendMessage(hHwnd, WM_Cap_Paki_CONNECT, iDevice, 0) Then
                    SendMessage(hHwnd, WM_Cap_SET_SCALE, True, 0)
                    SendMessage(hHwnd, WM_Cap_SET_PREVIEWRATE, 66, 0)
                    SendMessage(hHwnd, WM_Cap_SET_PREVIEW, True, 0)
                    SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, PictureBox1.Width, PictureBox1.Height, SWP_NOMOVE Or SWP_NOZORDER)
                Else
                    DestroyWindow(hHwnd)
                End If
            Catch ex1 As Exception
                MsgBox(ex1.Message)
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ClosePreviewWindow()
        SendMessage(hHwnd, WM_Cap_Paki_DISCONNECT, iDevice, 0)
        DestroyWindow(hHwnd)
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Try
            If btnCapture.Text = "Camera" Then
                Call OpenPreviewWindows()
                btnCapture.Text = "Capture"
            ElseIf btnCapture.Text = "Capture" Then
                Dim data As IDataObject
                Dim Bmap As Image
                SendMessage(hHwnd, WM_Cap_EDIT_COPY, 0, 0)
                data = Clipboard.GetDataObject()
                If data.GetDataPresent(GetType(Bitmap)) Then
                    Bmap = CType(data.GetData(GetType(Bitmap)), Image)
                    PictureBox1.Image = Bmap
                    ClosePreviewWindow()
                End If
                btnCapture.Text = "Camera"
                PictureBox1.Enabled = True
                PictureBox1.Enabled = True
                Call ClosePreviewWindow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit1.CheckedChanged
        If CheckEdit1.Checked = True Then
            DateTimePicker2.Enabled = True
            Dim dtr As DateTime
            DateTimePicker2.Format = DateTimePickerFormat.Custom
            DateTimePicker2.CustomFormat = "yyyy-MM-dd"
            dtr = DateTimePicker2.Value
        ElseIf CheckEdit1.Checked = False Then
            DateTimePicker2.Enabled = False
            Dim dtr As DateTime
            DateTimePicker2.Format = DateTimePickerFormat.Custom
            DateTimePicker2.CustomFormat = "----"
            dtr = DateTimePicker2.Value
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
    End Sub

    Private Sub SimpleButton17_Click(sender As Object, e As EventArgs) Handles SimpleButton17.Click
    End Sub

    Private Sub SimpleButton16_Click(sender As Object, e As EventArgs) Handles SimpleButton16.Click
    End Sub

    Private Sub SimpleButton18_Click(sender As Object, e As EventArgs) Handles SimpleButton18.Click
    End Sub

    Private Sub SimpleButton19_Click(sender As Object, e As EventArgs) Handles SimpleButton19.Click
    End Sub

    Public Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Sub GridView3_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView3.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            'e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf Button1_Click), GetImage))
        End If
    End Sub

    Private Sub GridView2_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView2.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            'e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf Button2_Click), GetImage))
        End If
    End Sub

    Private Sub GridView4_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView4.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            '            e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf Button3_Click), GetImage))
        End If
    End Sub

    Private Sub GridView5_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView5.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            '           e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf Button4_Click), GetImage))
        End If
    End Sub

    Private Sub GridView6_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView6.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            ' e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf Button5_Click), GetImage))
        End If
    End Sub

    Dim openfd As New OpenFileDialog

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            openfd.InitialDirectory = "C:\"
            openfd.Title = "Open a CV FIle"
            openfd.Filter = "PDF Files|*.pdf|Text Files|*.txt|Word FIles|*.docx"
            openfd.ShowDialog()
            Label23.Text = openfd.FileName
            Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
            Dim finfo As New FileInfo(Label23.Text)
            Dim numBytes As Long = finfo.Length
            Dim fstream As New FileStream(Label23.Text, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fstream)
            Dim data As Byte() = br.ReadBytes(CInt(numBytes))
            br.Close()
            fstream.Close()
            sqlcommand.CommandText = "update db_pegawai set jobdesk = @jobdesk where employeecode = @emp"
            sqlcommand.Parameters.AddWithValue("@emp", TextBox1.Text)
            If Not data Is Nothing Then
                sqlcommand.Parameters.AddWithValue("@Jobdesk", data)
            Else
                sqlcommand.Parameters.AddWithValue("@Jobdesk", "")
            End If
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Jobdesk" & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_education where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        edu()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_certificates where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cert()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_family where noid =  '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        fam()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_exp where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        exp()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_empskill where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        skill()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        '  delbenefit()
    End Sub

    Private Sub SimpleButton21_Click(sender As Object, e As EventArgs) Handles SimpleButton21.Click
        If act = "input" Or act = "edit" Then
            If RichTextBox1.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                insertbenefit()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_benefits where noid = '" & Label29.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            benefit()
        End If
    End Sub

    Private Sub SimpleButton20_Click(sender As Object, e As EventArgs)
        TextEdit4.Text = ""
        RichTextBox1.Text = ""
        act = "input"
    End Sub

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            '    e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf Button6_Click)))
        End If
    End Sub

    'Private Sub GridView1_FocusedRowChanged_1(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
    '    Dim datatabl As New DataTable
    '    Dim sqlcommand As New MySqlCommand
    '    datatabl.Clear()
    '    Dim param As String = ""
    '    Try
    '        param = "and NoId ='" + GridView1.GetFocusedRowCellValue("NoId").ToString() + "'"
    '    Catch ex As Exception
    '    End Try
    '    If param > "" Then
    '        act = "edit"
    '    Else
    '        act = "input"
    '    End If
    '    Try
    '        sqlcommand.CommandText = "Select NoId, EmployeeCode, Benefit, Description from db_benefits where 1 = 1 " + param.ToString()
    '        sqlcommand.Connection = SQLConnection
    '        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
    '        Dim cb As New MySqlCommandBuilder(adapter)
    '        adapter.Fill(datatabl)
    '    Catch ex As Exception
    '    End Try
    '    If datatabl.Rows.Count > 0 Then
    '        Label29.Text = datatabl.Rows(0).Item(0).ToString
    '        TextEdit4.Text = datatabl.Rows(0).Item(2).ToString
    '        RichTextBox1.Text = datatabl.Rows(0).Item(3).ToString
    '    End If
    'End Sub

    Private Sub ChangeEmp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = False
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit5.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit5_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit5.EditValueChanged
        Timer2.Stop()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit6.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit6_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit6.EditValueChanged
        Timer3.Stop()
    End Sub

    Private Sub SimpleButton22_Click(sender As Object, e As EventArgs) Handles SimpleButton22.Click
        Timer2.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label37.Text
            .Show()
        End With
    End Sub

    Private Sub SimpleButton23_Click(sender As Object, e As EventArgs) Handles SimpleButton23.Click
        Timer3.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label37.Text
            .Show()
        End With
    End Sub

    Private Sub ComboBoxEdit3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit3.SelectedIndexChanged
        For index As Integer = 0 To tableedu.Rows.Count - 1
            If ComboBoxEdit3.SelectedItem Is tableedu.Rows(index).Item(2).ToString Then
                Label29.Text = tableedu.Rows(index).Item(0).ToString
                txtyears.Text = tableedu.Rows(index).Item(3).ToString
                txtmajor.Text = tableedu.Rows(index).Item(4).ToString
            End If
        Next
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        act = "edit"
        ComboBoxEdit3.Enabled = True
        SimpleButton6.Enabled = True
        ComboBoxEdit3.Visible = True
        txtschoolname.Visible = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        act = "input"
        txtschoolname.Enabled = True
        SimpleButton6.Enabled = True
        txtschoolname.Visible = True
        ComboBoxEdit3.Visible = False
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        act = "delete"
        ComboBoxEdit3.Enabled = True
        SimpleButton6.Enabled = True
        txtschoolname.Visible = False
        ComboBoxEdit3.Visible = True
    End Sub

    Private Sub ComboBoxEdit4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit4.SelectedIndexChanged
        For index As Integer = 0 To tablecer.Rows.Count - 1
            If ComboBoxEdit4.SelectedItem Is tablecer.Rows(index).Item(2).ToString Then
                Label29.Text = tablecer.Rows(index).Item(0).ToString
                txtyear.Text = tablecer.Rows(index).Item(3).ToString
                txtreason.Text = tablecer.Rows(index).Item(4).ToString
            End If
        Next
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        act = "edit"
        ComboBoxEdit4.Enabled = True
        SimpleButton5.Enabled = True
        ComboBoxEdit4.Visible = True
        txtcertificate.Visible = False
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        act = "input"
        txtcertificate.Enabled = True
        SimpleButton5.Enabled = True
        txtcertificate.Visible = True
        ComboBoxEdit4.Visible = False
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        act = "delete"
        ComboBoxEdit4.Enabled = True
        SimpleButton5.Enabled = True
        txtcertificate.Visible = False
        ComboBoxEdit4.Visible = True
    End Sub

    Private Sub ComboBoxEdit5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit5.SelectedIndexChanged
        For index As Integer = 0 To tablefam.Rows.Count - 1
            If ComboBoxEdit5.SelectedItem Is tablefam.Rows(index).Item(2).ToString Then
                Label29.Text = tablefam.Rows(index).Item(0).ToString
                txtrelation.Text = tablefam.Rows(index).Item(3).ToString
                txtmemgender.Text = tablefam.Rows(index).Item(4).ToString
                txtmemadd.Text = tablefam.Rows(index).Item(5).ToString
                txtocc.Text = tablefam.Rows(index).Item(6).ToString
                txtmemph.Text = tablefam.Rows(index).Item(6).ToString
            End If
        Next
    End Sub

    Private Sub RadioButton10_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton10.CheckedChanged
        act = "edit"
        ComboBoxEdit8.Enabled = True
        SimpleButton8.Enabled = True
        ComboBoxEdit8.Visible = True
        txtjobtitle.Visible = False
    End Sub

    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        act = "input"
        txtjobtitle.Enabled = True
        SimpleButton8.Enabled = True
        txtjobtitle.Visible = True
        ComboBoxEdit8.Visible = False
    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        act = "delete"
        ComboBoxEdit8.Enabled = True
        SimpleButton8.Enabled = True
        txtjobtitle.Visible = False
        ComboBoxEdit8.Visible = True
    End Sub

    Private Sub ComboBoxEdit8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit8.SelectedIndexChanged
        For index As Integer = 0 To tableexp.Rows.Count - 1
            If ComboBoxEdit8.SelectedItem Is tableexp.Rows(index).Item(2).ToString Then
                Label29.Text = tableexp.Rows(index).Item(0).ToString
                txtcompanyname.Text = tableexp.Rows(index).Item(3).ToString
                txtmanagername.Text = tableexp.Rows(index).Item(4).ToString
                txtcompadd.Text = tableexp.Rows(index).Item(5).ToString
                txtperiod.Text = tableexp.Rows(index).Item(6).ToString
                txtuntil.Text = tableexp.Rows(index).Item(7).ToString
                txtbasic.Text = tableexp.Rows(index).Item(8).ToString
                txtaddi.Text = tableexp.Rows(index).Item(9).ToString
                txttotalsa.Text = tableexp.Rows(index).Item(10).ToString
                txtreasonquit.Text = tableexp.Rows(index).Item(11).ToString
            End If
        Next
    End Sub

    Private Sub ComboBoxEdit10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit10.SelectedIndexChanged
        For index As Integer = 0 To tablesk.Rows.Count - 1
            If ComboBoxEdit10.SelectedItem Is tablesk.Rows(index).Item(2).ToString Then
                Label29.Text = tablesk.Rows(index).Item(0).ToString
                skilllevel.Text = tablesk.Rows(index).Item(3).ToString
                skilldesc.Text = tablesk.Rows(index).Item(4).ToString
            End If
        Next
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        act = "edit"
        ComboBoxEdit10.Enabled = True
        SimpleButton9.Enabled = True
        ComboBoxEdit10.Visible = True
        skillname.Visible = False
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        act = "input"
        skillname.Enabled = True
        SimpleButton9.Enabled = True
        skillname.Visible = True
        ComboBoxEdit10.Visible = False
    End Sub

    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        act = "delete"
        ComboBoxEdit10.Enabled = True
        SimpleButton13.Enabled = True
        skillname.Visible = False
        ComboBoxEdit10.Visible = True
    End Sub

    Private Sub ComboBoxEdit11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit11.SelectedIndexChanged
        For index As Integer = 0 To tableben.Rows.Count - 1
            If ComboBoxEdit11.SelectedItem Is tableben.Rows(index).Item(2).ToString Then
                Label29.Text = tableben.Rows(index).Item(0).ToString
                RichTextBox1.Text = tableben.Rows(index).Item(3).ToString
            End If
        Next
    End Sub

    Private Sub RadioButton16_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton16.CheckedChanged
        act = "edit"
        ComboBoxEdit11.Enabled = True
        SimpleButton21.Enabled = True
        ComboBoxEdit11.Visible = True
        TextEdit4.Visible = False
    End Sub

    Private Sub RadioButton17_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton17.CheckedChanged
        act = "input"
        TextEdit4.Enabled = True
        SimpleButton21.Enabled = True
        TextEdit4.Visible = True
        ComboBoxEdit11.Visible = False
    End Sub

    Private Sub RadioButton18_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton18.CheckedChanged
        act = "delete"
        ComboBoxEdit11.Enabled = True
        SimpleButton21.Enabled = True
        TextEdit4.Visible = False
        ComboBoxEdit11.Visible = True
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        act = "edit"
        ComboBoxEdit5.Enabled = True
        SimpleButton7.Enabled = True
        ComboBoxEdit5.Visible = True
        txtmember.Visible = False
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        act = "input"
        txtmember.Enabled = True
        SimpleButton7.Enabled = True
        txtmember.Visible = True
        ComboBoxEdit5.Visible = False
    End Sub

    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        act = "delete"
        ComboBoxEdit5.Enabled = True
        SimpleButton7.Enabled = True
        txtmember.Visible = False
        ComboBoxEdit5.Visible = True
    End Sub

    Private Sub ComboBoxEdit6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit6.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub CheckEdit2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            CheckEdit3.Enabled = False
            CheckEdit3.Checked = False
            CheckEdit4.Enabled = False
            CheckEdit4.Checked = False
        Else
            CheckEdit3.Enabled = True
            CheckEdit4.Enabled = True
        End If
    End Sub

    Dim sel As New selectemp

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
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
            TextBox3.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Timer1.Stop()
    End Sub
End Class