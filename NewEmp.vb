Imports System.IO
Public Class NewEmp
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

    Sub loadexp()
        GridControl4.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select EmployeeCode, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason from db_exp where employeecode = '" & txtempcode.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl4.DataSource = table
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
        GridView4.BestFitColumns()
    End Sub

    Sub exp()
        Dim dtb, dtr As Date
        txtperiod.Format = DateTimePickerFormat.Custom
        txtperiod.CustomFormat = "yyyy-MM-dd"
        dtb = txtperiod.Value
        txtuntil.Format = DateTimePickerFormat.Custom
        txtuntil.CustomFormat = "yyyy-MM-dd"
        dtr = txtuntil.Value
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_exp " +
                            "(EmployeeCode, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason)" +
                            " values (@EmployeeCode, @Company, @Manager, @Address, @Period, @Until, @BasicSalary, @AdditionalSalary, @TotalSalary, @QuitReason)"
        cmmd.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
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
        txtcompanyname.Text = ""
        txtmanagername.Text = ""
        txtcompadd.Text = ""
        txtbasic.Text = ""
        txtaddi.Text = ""
        txttotalsa.Text = ""
        txtreasonquit.Text = ""
        loadexp()
    End Sub

    Sub loadfamily()
        GridControl3.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select EmployeeCode, MemberName, RelationShip, Gender, Address, Occupation, PhoneNo from db_family where employeecode = '" & txtempcode.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl3.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        txtmember.Text = ""
        txtmemgender.Text = ""
        txtmemadd.Text = ""
        txtocc.Text = ""
        txtmemph.Text = ""
        txtrelation.Text = ""
        GridView3.BestFitColumns()
    End Sub

    Sub family()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_family " +
                            "(EmployeeCode, MemberName, RelationShip, Gender, Address, Occupation, PhoneNo)" +
                            " values (@EmployeeCode, @MemberName, @RelationShip @Gender, @Address, @Occupation, @PhoneNo)"
        cmmd.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
        cmmd.Parameters.AddWithValue("@MemberName", txtmember.Text)
        cmmd.Parameters.AddWithValue("@RelationShip", txtrelation.Text)
        cmmd.Parameters.AddWithValue("@Gender", txtmemgender.Text)
        cmmd.Parameters.AddWithValue("@Address", txtmemadd.Text)
        cmmd.Parameters.AddWithValue("@Occupation", txtocc.Text)
        cmmd.Parameters.AddWithValue("@PhoneNo", txtmemph.Text)
        cmmd.ExecuteNonQuery()
        loadfamily()
    End Sub

    Sub loadcertificate()
        GridControl2.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select EmployeeCode, Certificates, Years, Reasons from db_certificates where employeecode = '" & txtempcode.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl2.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        txtcertificate.Text = ""
        txtyear.Text = ""
        txtreason.Text = ""
        GridView2.BestFitColumns()
    End Sub

    Sub certificates()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_certificates " +
                            "(EmployeeCode, Certificates, Years, Reasons)" +
                            " values (@EmployeeCode, @Certificates, @Years, @Reasons)"
        cmmd.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
        cmmd.Parameters.AddWithValue("@Certificates", txtcertificate.Text)
        cmmd.Parameters.AddWithValue("@Years", txtyear.Text)
        cmmd.Parameters.AddWithValue("@Reasons", txtreason.Text)
        cmmd.ExecuteNonQuery()
        loadcertificate()
    End Sub

    Sub loadskill()
        GridControl5.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlCommand As New MySqlCommand
        Try
            sqlCommand.CommandText = "select EmployeeCode, SkillName, SkillLevel, SkillDescription from db_empskill where EmployeeCode = '" & txtempcode.Text & "'"
            sqlCommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl5.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        skillname.Text = ""
        skilllevel.Text = ""
        skilldesc.Text = ""
        GridView5.BestFitColumns()
    End Sub

    Sub skill()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_empskill " +
                            "(EmployeeCode, SkillName, SkillLevel, SKillDescription) " +
                            "values (@EmployeeCode, @SkillName, @SkillLevel, @SkillDescription)"
        cmmd.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
        cmmd.Parameters.AddWithValue("@SkillName", skillname.Text)
        cmmd.Parameters.AddWithValue("@SkillLevel", skilllevel.Text)
        cmmd.Parameters.AddWithValue("@SkillDescription", skilldesc.Text)
        cmmd.ExecuteNonQuery()
        loadskill()
    End Sub

    Sub loadschool()
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlCommand As New MySqlCommand
        Try
            sqlCommand.CommandText = "select EmployeeCode, School as SchoolOrUniversity, GraduatedYear, StudyField from db_education where EmployeeCode = '" & txtempcode.Text & "'"
            sqlCommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        txtschoolname.Text = ""
        txtyears.Text = ""
        txtmajor.Text = ""
        GridView1.BestFitColumns()
    End Sub

    Sub school()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_education " +
                            "(EmployeeCode, School, GraduatedYear, StudyField) " +
                            "values (@EmployeeCode, @School, @GraduatedYear, @StudyField)"
        cmmd.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
        cmmd.Parameters.AddWithValue("@School", txtschoolname.Text)
        cmmd.Parameters.AddWithValue("@GraduatedYear", txtyears.Text)
        cmmd.Parameters.AddWithValue("@StudyField", txtmajor.Text)
        cmmd.ExecuteNonQuery()
        loadschool()
    End Sub

    Sub cleartxt1()
        txtcompcode.Text = ""
        txtnames.Text = ""
        txtposition.Text = ""
        txtbp.Text = ""
        txtgender.Text = ""
        txtrel.Text = ""
        txtadd.Text = ""
        txtidno.Text = ""
        txtoffloc.Text = ""
        txtphoneno.Text = ""
        txtempstat.Text = ""
        txttype.Text = ""
        txtnick.Text = ""
        txtkg.Text = ""
        txtcm.Text = ""
        txtblood.Text = ""
        txtwemail.Text = ""
        txtpemail.Text = ""
        txtrecby.Text = ""
        txtgroup.Text = ""
        txtdept.Text = ""
        txtjobdesk.Text = ""
        ComboBoxEdit6.Text = ""
        ComboBoxEdit7.Text = ""
        CheckEdit1.Checked = False
        TextBox3.Text = ""
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        ComboBoxEdit9.Text = ""
    End Sub

    Public Sub empbackup()
        Dim dtb, dtr, dtu As DateTime
        txtbod.Format = DateTimePickerFormat.Custom
        txtbod.CustomFormat = "yyyy-MM-dd"
        dtb = txtbod.Value
        txtjoin.Format = DateTimePickerFormat.Custom
        txtjoin.CustomFormat = "yyyy-MM-dd"
        dtr = txtjoin.Value
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtu = DateTimePicker2.Value
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
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Dim use As MySqlCommand = SQLConnection.CreateCommand
        use.CommandText = "select user from db_temp"
        Dim user As String = CStr(use.ExecuteScalar)
        Try
            cmmd.CommandText = "insert into db_historyemp " +
                            "(EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Photo, Status, EmployeeType, TerminateDate, ChangeDate, NickName, Weight, Height, BloodType, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks, MaritalStatus, PphStatus, IsExpiry, ExpiryDates, ApprovedBy, ExcludePayroll, PayCash, ProcessTax, ExcludeThr, ExcludeBonus, PrintSlip, PayrollInterval, MemilikiNpwp, Jobdesk, NoBpjs, NoNpwp, fingerid) " +
                            "values (@EmployeeCode, @CompanyCode, @FullName, @Position, @PlaceOfBirth, @DateOfBirth, @Gender, @Religion, @Address, @IdNumber, @OfficeLocation, @WorkDate, @PhoneNumber, @Photo, @Status, @EmployeeType, @TerminateDate, @ChangeDate, @NickName, @Weight, @Height, @BloodType, @WorkEmail, @PrivateEmail, @RecommendedBy, @Grouping, @Department, @Jobdesks, @MaritalStatus, @PphStatus, @IsExpiry, @ExpiryDates, @ApprovedBy, @ExcludePayroll, @PayCash, @ProcessTax, @ExcludeThr, @ExcludeBonus, @PrintSlip, @PayrollInterval, @npwp, @Jobdesk, @NoBpjs, @NoNpwp, @fingerid)"
            cmmd.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
            cmmd.Parameters.AddWithValue("@CompanyCode", txtcompcode.Text)
            cmmd.Parameters.AddWithValue("@FullName", txtnames.Text)
            cmmd.Parameters.AddWithValue("@Position", txtposition.Text)
            cmmd.Parameters.AddWithValue("@PlaceOfBirth", txtbp.Text)
            cmmd.Parameters.AddWithValue("@DateOfBirth", dtb.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@Gender", txtgender.Text)
            cmmd.Parameters.AddWithValue("@Religion", txtrel.Text)
            cmmd.Parameters.AddWithValue("@Address", txtadd.Text)
            cmmd.Parameters.AddWithValue("@IdNumber", txtidno.Text)
            cmmd.Parameters.AddWithValue("@OfficeLocation", txtoffloc.Text)
            cmmd.Parameters.AddWithValue("@WorkDate", dtr.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@PhoneNumber", txtphoneno.Text)
            If Not txtfoto.Text Is Nothing Then
                Dim param As New MySqlParameter("@Photo", ImageToByte(pictureEdit))
                cmmd.Parameters.Add(param)
            Else
                cmmd.Parameters.AddWithValue("@Photo", "")
            End If
            cmmd.Parameters.AddWithValue("@Status", txtempstat.Text)
            cmmd.Parameters.AddWithValue("@EmployeeType", txttype.Text)
            cmmd.Parameters.AddWithValue("@TerminateDate", Nothing)
            cmmd.Parameters.AddWithValue("@ChangeDate", Date.Now)
            cmmd.Parameters.AddWithValue("@NickName", txtnick.Text)
            cmmd.Parameters.AddWithValue("@Weight", txtkg.Text)
            cmmd.Parameters.AddWithValue("@Height", txtcm.Text)
            cmmd.Parameters.AddWithValue("@BloodType", txtblood.Text)
            cmmd.Parameters.AddWithValue("@WorkEmail", txtwemail.Text)
            cmmd.Parameters.AddWithValue("@PrivateEmail", txtpemail.Text)
            cmmd.Parameters.AddWithValue("@RecommendedBy", txtrecby.Text)
            cmmd.Parameters.AddWithValue("@Grouping", txtgroup.Text)
            cmmd.Parameters.AddWithValue("@Department", txtdept.Text)
            cmmd.Parameters.AddWithValue("@Jobdesks", txtjobdesk.Text)
            cmmd.Parameters.AddWithValue("@MaritalStatus", ComboBoxEdit6.Text)
            cmmd.Parameters.AddWithValue("@PphStatus", ComboBoxEdit7.Text)
            cmmd.Parameters.AddWithValue("@IsExpiry", CheckEdit1.Checked)
            cmmd.Parameters.AddWithValue("@ExpiryDates", dtu.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@ApprovedBy", TextBox3.Text)
            cmmd.Parameters.AddWithValue("@ExcludePayroll", CheckEdit2.Checked)
            cmmd.Parameters.AddWithValue("@PayCash", CheckEdit3.Checked)
            cmmd.Parameters.AddWithValue("@ProcessTax", CheckEdit4.Checked)
            cmmd.Parameters.AddWithValue("@ExcludeThr", CheckEdit5.Checked)
            cmmd.Parameters.AddWithValue("@ExcludeBonus", CheckEdit6.Checked)
            cmmd.Parameters.AddWithValue("@PrintSlip", CheckEdit7.Checked)
            cmmd.Parameters.AddWithValue("@PayrollInterval", ComboBoxEdit9.Text)
            cmmd.Parameters.AddWithValue("@ChangeBy", Label38.Text)
            cmmd.Parameters.AddWithValue("@ChangeDate", Date.Now)
            cmmd.Parameters.AddWithValue("@npwp", npwp.Text)
            Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
            Dim finfo As New FileInfo(Label23.Text)
            Dim numBytes As Long = finfo.Length
            Dim fstream As New FileStream(Label23.Text, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fstream)
            Dim data As Byte() = br.ReadBytes(CInt(numBytes))
            br.Close()
            fstream.Close()
            If Not data Is Nothing Then
                cmmd.Parameters.AddWithValue("@Jobdesk", data)
            Else
                cmmd.Parameters.AddWithValue("@Jobdesk", "")
            End If
            cmmd.Parameters.AddWithValue("@NoBpjs", TextEdit4.Text)
            cmmd.Parameters.AddWithValue("@NoNpwp", TextEdit3.Text)
            cmmd.Parameters.AddWithValue("@fingerid", TextEdit5.Text)
            cmmd.ExecuteNonQuery()
            '            MsgBox("Added")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cleartxt1()
    End Sub

    Public Sub insertion()
        Dim dtb, dtr, dtu As DateTime
        txtbod.Format = DateTimePickerFormat.Custom
        txtbod.CustomFormat = "yyyy-MM-dd"
        dtb = txtbod.Value
        txtjoin.Format = DateTimePickerFormat.Custom
        txtjoin.CustomFormat = "yyyy-MM-dd"
        dtr = txtjoin.Value
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtu = DateTimePicker2.Value
        Dim use As MySqlCommand = SQLConnection.CreateCommand
        use.CommandText = "select user from db_temp"
        Dim user As String = CStr(use.ExecuteScalar)
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
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            cmmd.CommandText = "insert into db_pegawai " +
                            "(EmployeeCode, CompanyCode, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, OfficeLocation, WorkDate, PhoneNumber, Photo, Status, EmployeeType, TerminateDate, ChangeDate, NickName, Weight, Height, BloodType, WorkEmail, PrivateEmail, RecommendedBy, Grouping, Department, Jobdesks, MaritalStatus, PphStatus, IsExpiry, ExpiryDates, ApprovedBy, ExcludePayroll, PayCash, ProcessTax, ExcludeThr, ExcludeBonus, PrintSlip, PayrollInterval, CreatedBy, MemilikiNpwp, jobdesk, NoBpjs, NoNpwp, fingerid, directhead, undirecthead) " +
                            "values (@EmployeeCode, @CompanyCode, @FullName, @Position, @PlaceOfBirth, @DateOfBirth, @Gender, @Religion, @Address, @IdNumber, @OfficeLocation, @WorkDate, @PhoneNumber, @Photo, @Status, @EmployeeType, @TerminateDate, @ChangeDate, @NickName, @Weight, @Height, @BloodType, @WorkEmail, @PrivateEmail, @RecommendedBy, @Grouping, @Department, @Jobdesks, @MaritalStatus, @PphStatus, @IsExpiry, @ExpiryDates, @ApprovedBy, @ExcludePayroll, @PayCash, @ProcessTax, @ExcludeThr, @ExcludeBonus, @PrintSlip, @PayrollInterval, @CreatedBy, @npwp, @jobdesk, @NoBpjs, @NoNpwp, @fingerid, @directhead, @undirecthead)"
            cmmd.Parameters.AddWithValue("@EmployeeCode", txtempcode.Text)
            cmmd.Parameters.AddWithValue("@CompanyCode", txtcompcode.Text)
            cmmd.Parameters.AddWithValue("@FullName", txtnames.Text)
            cmmd.Parameters.AddWithValue("@Position", txtposition.Text)
            cmmd.Parameters.AddWithValue("@PlaceOfBirth", txtbp.Text)
            cmmd.Parameters.AddWithValue("@DateOfBirth", dtb.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@Gender", txtgender.Text)
            cmmd.Parameters.AddWithValue("@Religion", txtrel.Text)
            cmmd.Parameters.AddWithValue("@Address", txtadd.Text)
            cmmd.Parameters.AddWithValue("@IdNumber", txtidno.Text)
            cmmd.Parameters.AddWithValue("@OfficeLocation", txtoffloc.Text)
            cmmd.Parameters.AddWithValue("@WorkDate", dtr.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@PhoneNumber", txtphoneno.Text)
            If Not txtfoto.Text Is Nothing Then
                Dim param As New MySqlParameter("@Photo", ImageToByte(pictureEdit))
                cmmd.Parameters.Add(param)
            Else
                cmmd.Parameters.AddWithValue("@Photo", "")
            End If
            cmmd.Parameters.AddWithValue("@Status", txtempstat.Text)
            cmmd.Parameters.AddWithValue("@EmployeeType", txttype.Text)
            cmmd.Parameters.AddWithValue("@TerminateDate", Nothing)
            cmmd.Parameters.AddWithValue("@ChangeDate", Date.Now)
            cmmd.Parameters.AddWithValue("@NickName", txtnick.Text)
            cmmd.Parameters.AddWithValue("@Weight", txtkg.Text)
            cmmd.Parameters.AddWithValue("@Height", txtcm.Text)
            cmmd.Parameters.AddWithValue("@BloodType", txtblood.Text)
            cmmd.Parameters.AddWithValue("@WorkEmail", txtwemail.Text)
            cmmd.Parameters.AddWithValue("@PrivateEmail", txtpemail.Text)
            cmmd.Parameters.AddWithValue("@RecommendedBy", txtrecby.Text)
            cmmd.Parameters.AddWithValue("@Grouping", txtgroup.Text)
            cmmd.Parameters.AddWithValue("@Department", txtdept.Text)
            cmmd.Parameters.AddWithValue("@Jobdesks", txtjobdesk.Text)
            cmmd.Parameters.AddWithValue("@MaritalStatus", ComboBoxEdit6.Text)
            cmmd.Parameters.AddWithValue("@PphStatus", ComboBoxEdit7.Text)
            cmmd.Parameters.AddWithValue("@IsExpiry", CheckEdit1.Checked)
            cmmd.Parameters.AddWithValue("@ExpiryDates", dtu.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@ApprovedBy", TextBox3.Text)
            cmmd.Parameters.AddWithValue("@ExcludePayroll", CheckEdit2.Checked)
            cmmd.Parameters.AddWithValue("@PayCash", CheckEdit3.Checked)
            cmmd.Parameters.AddWithValue("@ProcessTax", CheckEdit4.Checked)
            cmmd.Parameters.AddWithValue("@ExcludeThr", CheckEdit5.Checked)
            cmmd.Parameters.AddWithValue("@ExcludeBonus", CheckEdit6.Checked)
            cmmd.Parameters.AddWithValue("@PrintSlip", CheckEdit7.Checked)
            cmmd.Parameters.AddWithValue("@PayrollInterval", ComboBoxEdit9.Text)
            cmmd.Parameters.AddWithValue("@CreatedBy", Label38.Text)
            cmmd.Parameters.AddWithValue("@npwp", npwp.Text)
            Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
            Dim finfo As New FileInfo(Label23.Text)
            Dim numBytes As Long = finfo.Length
            Dim fstream As New FileStream(Label23.Text, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fstream)
            Dim data As Byte() = br.ReadBytes(CInt(numBytes))
            br.Close()
            fstream.Close()
            If Not data Is Nothing Then
                cmmd.Parameters.AddWithValue("@Jobdesk", data)
            Else
                cmmd.Parameters.AddWithValue("@Jobdesk", "")
            End If
            cmmd.Parameters.AddWithValue("@NoBpjs", TextEdit4.Text)
            cmmd.Parameters.AddWithValue("@NoNpwp", TextEdit3.Text)
            cmmd.Parameters.AddWithValue("@fingerid", TextEdit5.Text)
            cmmd.Parameters.AddWithValue("@directhead", TextEdit6.Text)
            cmmd.Parameters.AddWithValue("@undirecthead", TextEdit7.Text)
            cmmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cleartxt1()
    End Sub

    Dim tbl_par As New DataTable

    Sub changer()
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
        txtempcode.Text = rescode.ToString
    End Sub

    Dim tbl_par6, tbl_par3, tbl_par4, tbl_par5, tbl_par7 As New DataTable

    Sub loadjob()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select JobTitle from db_jobtitle where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par6)
        For index As Integer = 0 To tbl_par6.Rows.Count - 1
            txtposition.Properties.Items.Add(tbl_par6.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub loadcomp()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select CompanyCode from db_companycode where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par3)
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
            txtcompcode.Properties.Items.Add(tbl_par3.Rows(index).Item(0).ToString())
        Next
    End Sub


    Sub loadofloc()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select OfficeLocation from db_officelocation where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par4)
        For index As Integer = 0 To tbl_par4.Rows.Count - 1
            txtoffloc.Properties.Items.Add(tbl_par4.Rows(index).Item(0).ToString())
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

    Private Sub NewEmp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "update db_pegawai set photo = @photo"
        Dim param As New MySqlParameter("@Photo", ImageToByte(pictureEdit))
        sqlcommand.Parameters.Add(param)
        sqlcommand.ExecuteNonQuery()
        changer()
        Reset()
        loaddata1()
        loaddata()
        loadjob()
        loadcomp()
        loadofloc()
        loadgroup()
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        Label23.Text = "C:\pdffile\file.pdf"
    End Sub
    Dim tbl_par2, tbl_par1 As New DataTable

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

    Private Sub btnPhoto_Click(sender As Object, e As EventArgs)
        Using dialog As New OpenFileDialog
            If dialog.ShowDialog() <> DialogResult.OK Then Return
            txtfoto.Image = Image.FromFile(dialog.FileName)
            pictureEdit.Image = Image.FromFile(dialog.FileName)
        End Using
    End Sub

    Private Sub OpenPreviewWindows()
        Try
            Dim iHeight As Integer = pictureEdit.Height
            Dim iWidth As Integer = pictureEdit.Width
            hHwnd = capCreateCaptureWindowA((iDevice), WS_VISIBLE Or WS_CHILD, 0, 0, 640, 480, pictureEdit.Handle.ToInt32, 0)
            Try
                If SendMessage(hHwnd, WM_Cap_Paki_CONNECT, iDevice, 0) Then
                    SendMessage(hHwnd, WM_Cap_SET_SCALE, True, 0)
                    SendMessage(hHwnd, WM_Cap_SET_PREVIEWRATE, 66, 0)
                    SendMessage(hHwnd, WM_Cap_SET_PREVIEW, True, 0)
                    SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, pictureEdit.Width, pictureEdit.Height, SWP_NOMOVE Or SWP_NOZORDER)
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
                    pictureEdit.Image = Bmap
                    ClosePreviewWindow()
                End If
                btnCapture.Text = "Camera"
                pictureEdit.Enabled = True
                pictureEdit.Enabled = True
                Call ClosePreviewWindow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        If txtnames.Text = "" OrElse txtgender.Text = "" OrElse txtidno.Text = "" OrElse txtrel.Text = "" OrElse txtblood.Text = "" OrElse txtposition.Text = "" OrElse txtcompcode.Text = "" OrElse txtadd.Text = "" Then
            MsgBox("Please fill the required blank fields", MsgBoxStyle.Information)
        Else
            insertion()
            empbackup()
            changer()
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Using dialog As New OpenFileDialog
            If dialog.ShowDialog() <> DialogResult.OK Then Return
            txtfoto.Image = Image.FromFile(dialog.FileName)
            pictureEdit.Image = Image.FromFile(dialog.FileName)
        End Using
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        school()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        certificates()
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        family()
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

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        skill()
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        exp()
    End Sub

    Dim dt1 As Date
    Dim dt2 As Date
    Dim dt3 As TimeSpan
    Dim diff As Double

    Private Sub txtbod_ValueChanged(sender As Object, e As EventArgs) Handles txtbod.ValueChanged
        dt1 = CDate(txtbod.Value.ToShortDateString)
        dt2 = Date.Now
        dt3 = (dt2 - dt1)
        diff = dt3.Days
        txtage.Text = Str(Int(diff / 365))
    End Sub

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

    Private Sub txtidno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtidno.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtkg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkg.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcm.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtrel.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtblood_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtblood.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtphoneno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtphoneno.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtbasic_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbasic.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtaddi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtaddi.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txttotalsa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttotalsa.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub CheckEdit2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            CheckEdit3.Checked = False
            CheckEdit4.Checked = False
            CheckEdit3.Enabled = False
            CheckEdit4.Enabled = False
        Else
            CheckEdit3.Enabled = True
            CheckEdit4.Enabled = True
        End If
    End Sub
    Dim sel As New selectemp

    Dim openfd As New OpenFileDialog

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit6.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit7.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit6_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit6.EditValueChanged
        Timer2.Stop()
    End Sub

    Private Sub TextEdit7_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit7.EditValueChanged
        Timer3.Stop()
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        Timer2.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label38.Text
            .Show()
        End With
        'If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
        '    sel.Close()
        '    sel = New selectemp
        'End If
        'sel.Show()
    End Sub

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs) Handles SimpleButton12.Click
        Timer3.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label38.Text
            .Show()
        End With
    End Sub

    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        openfd.InitialDirectory = "C:\"
        openfd.Title = "Open a CV FIle"
        openfd.Filter = "PDF Files|*.pdf|Text Files|*.txt|Word FIles|*.docx"
        openfd.ShowDialog()
        Label23.Text = openfd.FileName
    End Sub

    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
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
End Class