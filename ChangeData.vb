Imports System.IO
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Views.Grid

Public Class ChangeData
    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Application.EnableVisualStyles()
        ' Add any initialization after the InitializeComponent() call.
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetOpenFileDialog() As OpenFileDialog
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.CheckPathExists = True
        openFileDialog.CheckFileExists = True
        openFileDialog.Filter = "Any Files(*.txt)|*.txt"
        openFileDialog.Multiselect = False
        openFileDialog.AddExtension = True
        openFileDialog.ValidateNames = True
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Return openFileDialog
    End Function

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
            ElseIf act = "input" Then
                cmmd.CommandText = "insert into db_exp " +
                           "(idrec, Position, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason)" +
                           " values (@idrec, @Company, @Position, @Manager, @Address, @Period, @Until, @BasicSalary, @AdditionalSalary, @TotalSalary, @QuitReason)"
            Else
                cmmd.CommandText = "delete from db_exp where noid = @noid"
            End If
            cmmd.Parameters.AddWithValue("@Idrec", txtidrec.Text)
            cmmd.Parameters.AddWithValue("@NoId", Label38.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@Position", txtjobtitle.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@Position", ComboBoxEdit5.Text)
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
                MsgBox("Data sucessfully changed")
            ElseIf act = "input" Then
                MsgBox("Data succesfully added")
            ElseIf act = "delete" Then
                MsgBox("Deleted")
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

    Sub certificates()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_certificates set " +
                                    " Certificates = @certificates " +
                                    ", Years = @Years" +
                                    ", Reasons = @reasons" +
                                    " where noid = @noid"
            ElseIf act = "input" Then
                cmmd.CommandText = "insert into db_certificates " +
                                    "(idrec, Certificates, Years, Reasons)" +
                                    " values (@idrec, @Certificates, @Years, @Reasons)"
            Else
                cmmd.CommandText = "delete from db_certificates where noid = @noid"
            End If
            cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
            cmmd.Parameters.AddWithValue("@noid", Label38.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@certificates", txtcertificate.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@certificates", ComboBoxEdit2.Text)
            End If
            cmmd.Parameters.AddWithValue("@Years", txtyear.Text)
            cmmd.Parameters.AddWithValue("@Reasons", txtreason.Text)
            cmmd.ExecuteNonQuery()
            If act = "input" Then
                MsgBox("Added")
            ElseIf act = "edit" Then
                MsgBox("Changed")
            Else
                MsgBox("Deleted")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cert()
    End Sub

    Sub family()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_family set " +
                                    " MemberName = @MemberName" +
                                    ", Relationship = @Relationship " +
                                    ", Gender = @gender" +
                                    ", Address = @Address" +
                                    ", Occupation = @Occupation" +
                                    ", PhoneNo = @PhoneNo" +
                                    " where NoId = @Noid"
            ElseIf act = "input" Then
                cmmd.CommandText = "insert into db_family " +
                          "(idrec, RelationShip, MemberName, Gender, Address, Occupation, PhoneNo)" +
                          " values (@idrec, @Relationship, @MemberName, @Gender, @Address, @Occupation, @PhoneNo)"
            Else
                cmmd.CommandText = "delete from db_family where noid = '" & Label38.Text & "'"
            End If
            cmmd.Parameters.AddWithValue("@Noid", Label38.Text)
            cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
            cmmd.Parameters.AddWithValue("@Relationship", txtrelation.Text)
            If act = "edit" Then
                cmmd.Parameters.AddWithValue("@MemberName", ComboBoxEdit3.Text)
            ElseIf act = "input" Then
                cmmd.Parameters.AddWithValue("@MemberName", txtmember.Text)
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
            Else
                MsgBox("Deleted")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        fam()
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
            ElseIf act = "input" Then
                cmmd.CommandText = "insert into db_empskill " +
                           "(Idrec, SkillName, SkillLevel, SKillDescription) " +
                           "values (@idrec, @SkillName, @SkillLevel, @SkillDescription)"
            Else
                cmmd.CommandText = "delete from db_empskill where noid = @noid"
            End If
            cmmd.Parameters.AddWithValue("@NoId", Label38.Text)
            cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
            If act = "input" Then
                cmmd.Parameters.AddWithValue("@SkillName", skillname.Text)
            ElseIf act = "edit" Then
                cmmd.Parameters.AddWithValue("@SkillName", ComboBoxEdit6.Text)
            End If
            cmmd.Parameters.AddWithValue("@SkillLevel", skilllevel.Text)
            cmmd.Parameters.AddWithValue("@SkillDescription", skilldesc.Text)
            cmmd.ExecuteNonQuery()
            If act = "input" Then
                MsgBox("Added")
            ElseIf act = "edit" Then
                MsgBox("Changed")
            Else
                MsgBox("Deleted")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        skill()
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
            ElseIf act = "input" Then
                cmmd.CommandText = "insert into db_education " +
                            "(IdRec, School, GraduatedYear, StudyField) " +
                            "values (@idrec, @School, @GraduatedYear, @StudyField)"
            Else
                cmmd.CommandText = "delete from db_education where noid = @noid"
            End If
            cmmd.Parameters.AddWithValue("@NoId", Label38.Text)
            cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
            If act = "edit" Then
                cmmd.Parameters.AddWithValue("@school", ComboBoxEdit1.Text)
            ElseIf act = "input" Then
                cmmd.Parameters.AddWithValue("@school", txtschoolname.Text)
            End If
            cmmd.Parameters.AddWithValue("@GraduatedYear", txtyears.Text)
            cmmd.Parameters.AddWithValue("@StudyField", txtmajor.Text)
            cmmd.ExecuteNonQuery()
            If act = "input" Then
                MsgBox("Added")
            ElseIf act = "edit" Then
                MsgBox("Changed")
            Else
                MsgBox("Deleted")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        edu()
    End Sub

    Dim datatable As New DataTable

    Public Overridable Property ShowAutoFilterRow As Boolean

    Dim tableedu As New DataTable

    Public Sub edu()
        tableedu.Clear()
        ComboBoxEdit1.Properties.Items.Clear()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, Idrec as IdRecruitment, School as SchoolOrUniversity, GraduatedYear, StudyField from db_education where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            datatable.Load(sqlcommand.ExecuteReader)
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tableedu)
            GridControl3.DataSource = tableedu
            GridView3.OptionsView.ShowAutoFilterRow = True
            For index As Integer = 0 To tableedu.Rows.Count - 1
                ComboBoxEdit1.Properties.Items.Add(tableedu.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
        End Try
        txtschoolname.Text = ""
        Label38.Text = ""
        txtyears.Text = ""
        txtmajor.Text = ""
    End Sub

    Dim tablecer As New DataTable

    Public Sub cert()
        tablecer.Clear()
        ComboBoxEdit2.Properties.Items.Clear()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, Idrec as IdRecruitment, Certificates, Years, Reasons from db_certificates where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tablecer)
            GridControl2.DataSource = tablecer
            For index As Integer = 0 To tablecer.Rows.Count - 1
                ComboBoxEdit2.Properties.Items.Add(tablecer.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
            MsgBox("Certificates Error  " & ex.Message)
        End Try
        txtcertificate.Text = ""
        txtyear.Text = ""
        txtreason.Text = ""
    End Sub

    Dim tablefam As New DataTable

    Public Sub fam()
        tablefam.Clear()
        ComboBoxEdit3.Properties.Items.Clear()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, Idrec as IdRecruitment, MemberName, Relationship, Gender, Address, Occupation, PhoneNo from db_family where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tablefam)
            GridControl4.DataSource = tablefam
            For index As Integer = 0 To tablefam.Rows.Count - 1
                ComboBoxEdit3.Properties.Items.Add(tablefam.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        txtmember.Text = ""
        txtmemgender.Text = ""
        txtmemadd.Text = ""
        txtocc.Text = ""
        txtmemph.Text = ""
    End Sub

    Dim tableexp As New DataTable

    Public Sub exp()
        tableexp.Clear()
        ComboBoxEdit5.Properties.Items.Clear()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, Idrec as IdRecruitment, Position, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason from db_exp where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tableexp)
            GridControl5.DataSource = tableexp
            'GridView5.OptionsView.ShowAutoFilterRow = True
            For index As Integer = 0 To tableexp.Rows.Count - 1
                ComboBoxEdit5.Properties.Items.Add(tableexp.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
            'MsgBox(ex.Message)
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

    Dim tablesk As New DataTable

    Public Sub skill()
        tablesk.Clear()
        ComboBoxEdit6.Properties.Items.Clear()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select NoId, Idrec as IdRecruitment, SkillName, SkillLevel, SkillDescription from db_empskill where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(tablesk)
            GridControl6.DataSource = tablesk
            'GridView6.OptionsView.ShowAutoFilterRow = True
            For index As Integer = 0 To tablesk.Rows.Count - 1
                ComboBoxEdit6.Properties.Items.Add(tablesk.Rows(index).Item(2).ToString)
            Next
        Catch ex As Exception
        End Try
        skillname.Text = ""
        skilllevel.Text = ""
        skilldesc.Text = ""
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

    'Sub retrieved()
    '    Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
    '    cmmd.CommandText = "select cv from db_recruitment where idrec = '" & txtidrec.Text & "'"
    '    Dim dr As MySqlDataReader
    '    dr = cmmd.ExecuteReader
    '    dr.Read()
    '    Dim filebytes() As Byte = CType(dr(0), Byte())
    '    Dim fstream As New FileStream(dr(0).ToString, FileMode.Create, FileAccess.Write)
    '    fstream.Write(filebytes, 0, filebytes.Length)
    '    File.WriteAllBytes("C:\testpdf.pdf", fstream)
    '    fstream.Close()
    'End Sub

    Sub download()
        Try
            Dim sFilePath As String
            Dim buffer As Byte()
            Using cmd As New MySqlCommand("select cv from db_recruitment where idrec = '" & txtidrec.Text & "'", SQLConnection)
                'Using cmd As New MySqlCommand("Select Top 1 PDF From PDF", SQLConnection)
                buffer = CType(cmd.ExecuteScalar(), Byte())
            End Using
            sFilePath = Path.GetTempFileName()
            File.Move(sFilePath, Path.ChangeExtension(sFilePath, ".pdf"))
            sFilePath = Path.ChangeExtension(sFilePath, ".pdf")
            File.WriteAllBytes(sFilePath, buffer)
            Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            act.BeginInvoke(sFilePath, Nothing, Nothing)
        Catch ex As Exception
            MsgBox("retrieved cv" & ex.Message)
        End Try
    End Sub

    Private Shared Sub OpenPDFFile(ByVal sFilePath As String)
        Using p As New Process
            p.StartInfo = New ProcessStartInfo(sFilePath)
            p.Start()
            p.WaitForExit()
            Try
                File.Delete(sFilePath)
            Catch
            End Try
        End Using
    End Sub

    Public Sub updatechange2()
        Dim dta, dtb, dtr As Date
        txtapplieddate.Format = DateTimePickerFormat.Custom
        txtapplieddate.CustomFormat = "yyyy-MM-dd"
        dta = txtapplieddate.Value

        txtbod.Format = DateTimePickerFormat.Custom
        txtbod.CustomFormat = "yyyy-MM-dd"
        dtb = txtbod.Value
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_recruitment SET" +
                                     " FullName = @FullName" +
                                     ", PlaceOfBirth = @PlaceOfBirth" +
                                     ", DateOfBirth = @DateOfBirth" +
                                     ", Address = @Address" +
                                     ", Gender = @Gender" +
                                     ", Religion = @Religion" +
                                     ", PhoneNumber = @PhoneNumber" +
                                     ", IdNumber = @IdNumber" +
                                     ", Photo = @Photo" +
                                     ", Status = @Status" +
                                     ", InterviewDate = @InterviewDate" +
                                     ", Reason = @Reason" +
                                     ", Position = @Position" +
                                     ", NickName = @NickName" +
                                     ", ApplicationDate = @Applicationdate" +
                                     ", Weight = @Weight " +
                                     ", Height = @Height " +
                                     ", BloodType = @BloodType " +
                                     ", City = @City " +
                                     ", ZIP = @Zip " +
                                     ", HomeNumber = @HomeNumber " +
                                     ", RecommendedBy = @RecommendedBy " +
                                     ", Martial = @Martial " +
                                     ", LastSalary = @LastSalary " +
                                     ", OtherIncome = @OtherIncome " +
                                     ", ExpectedSalary = @ExpectedSalary" +
                                     ", ExpFacilities = @ExpFacilities" +
                                     ", FavoriteJob = @FavoriteJob" +
                                     ", Fwhy = @Fwhy" +
                                     ", AppliedHere = @AppliedHere" +
                                     ", AWhy = @Awhy" +
                                     ", Family = @Family" +
                                     ", Who = @Who " +
                                     ", Strenghts = @Strenghts" +
                                     ", Weakness = @Weakness" +
                                     ", Suitable = @Suitable" +
                                     ", CarrierObject = @CarrierObject" +
                                     ", Reference = @Reference" +
                                     ", PrivateEmail = @Private" +
                                     " WHERE IdRec = @IdRec"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@IdRec", txtidrec.Text)
            sqlcommand.Parameters.AddWithValue("@FullName", txtcandname.Text)
            sqlcommand.Parameters.AddWithValue("@PlaceOfBirth", txtbp.Text)
            sqlcommand.Parameters.AddWithValue("@DateOfBirth", dtb.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@Address", txtadd.Text)
            sqlcommand.Parameters.AddWithValue("@Gender", txtgend.Text)
            sqlcommand.Parameters.AddWithValue("@Religion", txtrel.Text)
            sqlcommand.Parameters.AddWithValue("@PhoneNumber", txtphoneno.Text)
            sqlcommand.Parameters.AddWithValue("@IdNumber", txtidno.Text)
            If Not ImageEdit1.Text Is Nothing Then
                Dim param As New MySqlParameter("@Photo", ImageToByte(pictureEdit))
                sqlcommand.Parameters.Add(param)
            Else
                sqlcommand.Parameters.AddWithValue("@Photo", "")
            End If
            sqlcommand.Parameters.AddWithValue("@Status", txtofloc.Text)
            sqlcommand.Parameters.AddWithValue("@InterviewDate", dtr.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@Reason", txtcandreason.Text)
            sqlcommand.Parameters.AddWithValue("@Position", TextEdit7.Text)
            sqlcommand.Parameters.AddWithValue("@NickName", txtnick.Text)
            sqlcommand.Parameters.AddWithValue("@ApplicationDate", dta.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@Weight", txtkg.Text)
            sqlcommand.Parameters.AddWithValue("@Height", txtcm.Text)
            sqlcommand.Parameters.AddWithValue("@BloodType", txtblood.Text)
            sqlcommand.Parameters.AddWithValue("@City", txtcity.Text)
            sqlcommand.Parameters.AddWithValue("@Zip", txtzip.Text)
            sqlcommand.Parameters.AddWithValue("@HomeNumber", txthome.Text)
            sqlcommand.Parameters.AddWithValue("@RecommendedBy", txtrecby.Text)
            sqlcommand.Parameters.AddWithValue("@Martial", ComboBoxEdit4.Text)
            sqlcommand.Parameters.AddWithValue("@LastSalary", TextBox3.Text)
            sqlcommand.Parameters.AddWithValue("@OtherIncome", TextBox4.Text)
            sqlcommand.Parameters.AddWithValue("@ExpectedSalary", TextBox5.Text)
            sqlcommand.Parameters.AddWithValue("@ExpFacilities", RichTextBox1.Text)
            sqlcommand.Parameters.AddWithValue("@FavoriteJob", TextBox6.Text)
            sqlcommand.Parameters.AddWithValue("@Fwhy", RichTextBox2.Text)
            sqlcommand.Parameters.AddWithValue("@AppliedHere", CheckEdit1.Checked)
            sqlcommand.Parameters.AddWithValue("@AWhy", RichTextBox3.Text)
            sqlcommand.Parameters.AddWithValue("@Family", CheckEdit2.Checked)
            sqlcommand.Parameters.AddWithValue("@Who", RichTextBox4.Text)
            sqlcommand.Parameters.AddWithValue("@Strenghts", RichTextBox5.Text)
            sqlcommand.Parameters.AddWithValue("@Weakness", RichTextBox6.Text)
            sqlcommand.Parameters.AddWithValue("@Suitable", RichTextBox7.Text)
            sqlcommand.Parameters.AddWithValue("@CarrierObject", RichTextBox8.Text)
            sqlcommand.Parameters.AddWithValue("@Reference", RichTextBox9.Text)
            sqlcommand.Parameters.AddWithValue("@private", txtwemail.Text)
            'Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
            'Dim finfo As New FileInfo(Label39.Text)
            'Dim numBytes As Long = finfo.Length
            'Dim fstream As New FileStream(Label39.Text, FileMode.Open, FileAccess.Read)
            'Dim br As New BinaryReader(fstream)
            'Dim data As Byte() = br.ReadBytes(CInt(numBytes))
            'br.Close()
            'fstream.Close()
            'If Not data Is Nothing Then
            '    sqlcommand.Parameters.AddWithValue("@cv", data)
            'Else
            '    sqlcommand.Parameters.AddWithValue("@cv", "")
            'End If
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Data succesfully changed")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
    '    'Dim datatabl As New DataTable
    '    'Dim sqlCommand As New MySqlCommand
    '    'datatabl.Clear()
    '    'Dim param As String = ""
    '    'Try
    '    '    param = "and FullName='" + GridView1.GetFocusedRowCellValue("FullName").ToString() + "'"
    '    'Catch ex As Exception
    '    'End Try
    '    'Try
    '    '    sqlCommand.CommandText = "SELECT * FROM db_recruitment WHERE 1 = 1 " + param.ToString()
    '    '    sqlCommand.Connection = SQLConnection
    '    '    Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
    '    '    Dim cb As New MySqlCommandBuilder(adapter)
    '    '    adapter.Fill(datatabl)
    '    'Catch ex As Exception
    '    '    MsgBox(ex.Message)
    '    'End Try
    '    'If datatabl.Rows.Count > -1 Then
    '    '    txtidrec.Text = datatabl.Rows(0).Item(0).ToString()
    '    '    TextEdit1.Text = datatabl.Rows(0).Item(1).ToString
    '    '    txtcandname.Text = datatabl.Rows(0).Item(2).ToString()
    '    '    txtbp.Text = datatabl.Rows(0).Item(3).ToString
    '    '    txtbod.Text = datatabl.Rows(0).Item(4).ToString
    '    '    txtadd.Text = datatabl.Rows(0).Item(5).ToString
    '    '    txtgend.Text = datatabl.Rows(0).Item(6).ToString()
    '    '    txtrel.Text = datatabl.Rows(0).Item(7).ToString
    '    '    txtphoneno.Text = datatabl.Rows(0).Item(8).ToString
    '    '    txtidno.Text = datatabl.Rows(0).Item(9).ToString()
    '    '    txtofloc.Text = datatabl.Rows(0).Item(11).ToString()
    '    '    ' DateTimePicker2.Value = CDate(datatabl.Rows(0).Item(12).ToString())
    '    '    txtcandreason.Text = datatabl.Rows(0).Item(15).ToString()
    '    '    Dim filefoto As Byte() = CType(datatabl.Rows(0).Item(10), Byte())
    '    '    If filefoto.Length > 0 Then
    '    '        pictureEdit.Image = ByteToImage(filefoto)
    '    '    Else
    '    '        pictureEdit.Image = Nothing
    '    '        pictureEdit.Refresh()
    '    '    End If
    '    '    TextEdit7.Text = datatabl.Rows(0).Item(17).ToString()
    '    '    txtnick.Text = datatabl.Rows(0).Item(19).ToString()
    '    '    txtapplieddate.Text = datatabl.Rows(0).Item(20).ToString()
    '    '    txtkg.Text = datatabl.Rows(0).Item(21).ToString
    '    '    txtcm.Text = datatabl.Rows(0).Item(22).ToString
    '    '    txtblood.Text = datatabl.Rows(0).Item(23).ToString
    '    '    txtcity.Text = datatabl.Rows(0).Item(24).ToString
    '    '    txtzip.Text = datatabl.Rows(0).Item(25).ToString
    '    '    txthome.Text = datatabl.Rows(0).Item(26).ToString
    '    '    txtrecby.Text = datatabl.Rows(0).Item(27).ToString
    '    '    ComboBoxEdit4.Text = datatabl.Rows(0).Item(28).ToString
    '    '    TextBox3.Text = datatabl.Rows(0).Item(29).ToString
    '    '    TextBox4.Text = datatabl.Rows(0).Item(30).ToString
    '    '    TextBox5.Text = datatabl.Rows(0).Item(18).ToString
    '    '    RichTextBox1.Text = datatabl.Rows(0).Item(31).ToString
    '    '    TextBox6.Text = datatabl.Rows(0).Item(32).ToString
    '    '    RichTextBox2.Text = datatabl.Rows(0).Item(33).ToString
    '    '    CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(34).ToString)
    '    '    RichTextBox3.Text = datatabl.Rows(0).Item(35).ToString
    '    '    CheckEdit2.Checked = CBool(datatabl.Rows(0).Item(36).ToString)
    '    '    RichTextBox4.Text = datatabl.Rows(0).Item(37).ToString
    '    '    RichTextBox5.Text = datatabl.Rows(0).Item(38).ToString
    '    '    RichTextBox6.Text = datatabl.Rows(0).Item(39).ToString
    '    '    RichTextBox7.Text = datatabl.Rows(0).Item(40).ToString
    '    '    RichTextBox8.Text = datatabl.Rows(0).Item(41).ToString
    '    '    RichTextBox9.Text = datatabl.Rows(0).Item(42).ToString
    '    '    txtwemail.Text = datatabl.Rows(0).Item(43).ToString
    '    '    edu()
    '    '    cert()
    '    '    skill()
    '    '    fam()
    '    '    exp()
    '    'End If
    'End Sub

    Dim tbl_par2 As New DataTable

    Sub loaddata1()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "SELECT * from db_recruitment where idrec = '" & txtidrec.Text & "'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par2)
        For index As Integer = 0 To tbl_par2.Rows.Count - 1
        Next
    End Sub

    Sub autochange()
        For index As Integer = 0 To tbl_par2.Rows.Count - 1
            txtcandname.Text = tbl_par2.Rows(index).Item(2).ToString
            txtnick.Text = tbl_par2.Rows(index).Item(19).ToString
            txtgend.Text = tbl_par2.Rows(index).Item(6).ToString
            txtapplieddate.Text = tbl_par2.Rows(index).Item(20).ToString
            txtbp.Text = tbl_par2.Rows(index).Item(3).ToString
            txtbod.Text = tbl_par2.Rows(index).Item(4).ToString
            txtidno.Text = tbl_par2.Rows(index).Item(9).ToString
            txtkg.Text = tbl_par2.Rows(index).Item(21).ToString
            txtcm.Text = tbl_par2.Rows(index).Item(22).ToString
            txtrel.Text = tbl_par2.Rows(index).Item(7).ToString
            txtblood.Text = tbl_par2.Rows(index).Item(23).ToString
            txtphoneno.Text = tbl_par2.Rows(index).Item(8).ToString
            txtwemail.Text = tbl_par2.Rows(index).Item(43).ToString
            txtzip.Text = tbl_par2.Rows(index).Item(25).ToString
            txtcity.Text = tbl_par2.Rows(index).Item(24).ToString
            txthome.Text = tbl_par2.Rows(index).Item(26).ToString
            txtadd.Text = tbl_par2.Rows(index).Item(5).ToString
            txtrecby.Text = tbl_par2.Rows(index).Item(27).ToString
            TextEdit7.Text = tbl_par2.Rows(index).Item(17).ToString
            ComboBoxEdit4.Text = tbl_par2.Rows(index).Item(28).ToString
            txtofloc.Text = tbl_par2.Rows(index).Item(11).ToString
            txtcandreason.Text = tbl_par2.Rows(index).Item(15).ToString
            TextBox3.Text = tbl_par2.Rows(index).Item(29).ToString
            TextBox4.Text = tbl_par2.Rows(index).Item(30).ToString
            TextBox5.Text = tbl_par2.Rows(index).Item(18).ToString
            RichTextBox1.Text = tbl_par2.Rows(index).Item(31).ToString
            TextBox6.Text = tbl_par2.Rows(index).Item(32).ToString
            RichTextBox2.Text = tbl_par2.Rows(index).Item(33).ToString
            CheckEdit1.Checked = CType(tbl_par2.Rows(index).Item(34).ToString, Boolean)
            RichTextBox3.Text = tbl_par2.Rows(index).Item(35).ToString
            CheckEdit2.Checked = CType(tbl_par2.Rows(index).Item(36).ToString, Boolean)
            RichTextBox4.Text = tbl_par2.Rows(index).Item(37).ToString
            RichTextBox5.Text = tbl_par2.Rows(index).Item(38).ToString
            RichTextBox6.Text = tbl_par2.Rows(index).Item(39).ToString
            RichTextBox7.Text = tbl_par2.Rows(index).Item(40).ToString
            RichTextBox8.Text = tbl_par2.Rows(index).Item(41).ToString
            RichTextBox9.Text = tbl_par2.Rows(index).Item(42).ToString
            Dim filefoto As Byte() = CType(tbl_par2.Rows(0).Item(10), Byte())
            If filefoto.Length > 0 Then
                pictureEdit.Image = ByteToImage(filefoto)
            Else
                pictureEdit.Image = Nothing
                pictureEdit.Refresh()
            End If
        Next
    End Sub

    Private Sub ChangeData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            txtcandname.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            txtidrec.Text = quer2.ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        loaddata1()
        autochange()
        edu()
        cert()
        skill()
        fam()
        exp()
        GridView2.BestFitColumns()
        GridView3.BestFitColumns()
        GridView4.BestFitColumns()
        GridView5.BestFitColumns()
        GridView6.BestFitColumns()
        act = "input"
        'Label39.Text = "file.pdf"
    End Sub

    Dim openfd As New OpenFileDialog

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        updatechange2()
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

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        updatechange2()
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        If act = "input" Or act = "edit" Then
            If txtmajor.Text = "" OrElse txtyears.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                school()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_education where noid ='" & Label38.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            edu()
        End If
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        If act = "input" Or act = "edit" Then
            If txtyear.Text = "" OrElse txtreason.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                certificates()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_certificates where noid = '" & Label38.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            cert()
        End If
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        If act = "input" Or act = "edit" Then
            If skilldesc.Text = "" Or skilllevel.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                insertskill()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_empskill where noid = '" & Label38.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            skill()
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
            query.CommandText = "delete from db_exp where noid = '" & Label38.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            exp()
        End If
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        If act = "input" Or act = "edit" Then
            If txtrelation.Text = "" Or txtmemgender.Text = "" Or txtmemadd.Text = "" Or txtocc.Text = "" Or txtmemph.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                family()
            End If
        ElseIf act = "delete" Then
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "delete from db_family where noid = '" & Label38.Text & "'"
            query.ExecuteNonQuery()
            MsgBox("Deleted")
            fam()
        End If
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

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Using dialog As New OpenFileDialog
            If dialog.ShowDialog() <> DialogResult.OK Then Return
            ImageEdit1.Image = Image.FromFile(dialog.FileName)
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

    Private Sub SavePdf()
        'Try
        openfd.InitialDirectory = "C:\pdffile"
        openfd.Title = "Open a CV FIle"
        openfd.Filter = "PDF Files|*.pdf|Text Files|*.txt|Word FIles|*.docx"
        openfd.Filter = "PDF Files|*.pdf"
        openfd.ShowDialog()
        Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
        Dim finfo As New FileInfo(Label38.Text)
        Dim numBytes As Long = finfo.Length
        Dim fstream As New FileStream(Label38.Text, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fstream)
        Dim data As Byte() = br.ReadBytes(CInt(numBytes))
        br.Close()
        fstream.Close()
        sqlquery.CommandText = "update db_recruitment set (cv) = (@cv) where idrec = '" & txtidrec.Text & "'"
        sqlquery.Parameters.AddWithValue("@cv", data)
        sqlquery.ExecuteNonQuery()
        MsgBox("Saved")
    End Sub

    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        Try
            openfd.InitialDirectory = "C:\"
            openfd.Title = "Open a CV FIle"
            openfd.Filter = "PDF Files|*.pdf|Text Files|*.txt|Word FIles|*.docx"
            openfd.ShowDialog()
            Label39.Text = openfd.FileName
            Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
            Dim finfo As New FileInfo(Label39.Text)
            Dim numBytes As Long = finfo.Length
            Dim fstream As New FileStream(Label39.Text, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fstream)
            Dim data As Byte() = br.ReadBytes(CInt(numBytes))
            br.Close()
            fstream.Close()
            sqlcommand.CommandText = "update db_recruitment set cv = @cv where idrec = @emp"
            sqlcommand.Parameters.AddWithValue("@emp", txtidrec.Text)
            If Not data Is Nothing Then
                sqlcommand.Parameters.AddWithValue("@cv", data)
            Else
                sqlcommand.Parameters.AddWithValue("@cv", "")
            End If
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("CV" & ex.Message)
        End Try
    End Sub

    Dim act As String = ""

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
        txtschoolname.Visible = True
        ComboBoxEdit1.Visible = False
        txtschoolname.Text = ""
        txtyears.Text = ""
        Label38.Text = ""
        txtmajor.Text = ""
        act = "input"
    End Sub

    'Private Sub GridView2_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
    '    Dim datatabl As New DataTable
    '    Dim sqlcommand As New MySqlCommand
    '    datatabl.Clear()
    '    Dim param As String = ""
    '    Try
    '        param = "and NoId ='" + GridView2.GetFocusedRowCellValue("NoId").ToString() + "'"
    '    Catch ex As Exception
    '    End Try
    '    If param > "" Then
    '        act = "edit"
    '    Else
    '        act = "input"
    '    End If
    '    Try
    '        sqlcommand.CommandText = "Select NoId, Idrec, Certificates, Years, Reasons from db_certificates where 1 = 1 " + param.ToString()
    '        sqlcommand.Connection = SQLConnection
    '        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
    '        Dim cb As New MySqlCommandBuilder(adapter)
    '        adapter.Fill(datatabl)
    '    Catch ex As Exception
    '    End Try
    '    If datatabl.Rows.Count > 0 Then
    '        Label38.Text = datatabl.Rows(0).Item(0).ToString
    '        txtcertificate.Text = datatabl.Rows(0).Item(2).ToString
    '        txtyear.Text = datatabl.Rows(0).Item(3).ToString
    '        txtreason.Text = datatabl.Rows(0).Item(4).ToString
    '    End If
    'End Sub

    'Private Sub GridView4_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView4.FocusedRowChanged
    '    Dim datatabl As New DataTable
    '    Dim sqlcommand As New MySqlCommand
    '    datatabl.Clear()
    '    Dim param As String = ""
    '    Try
    '        param = "and NoId ='" + GridView4.GetFocusedRowCellValue("NoId").ToString() + "'"
    '    Catch ex As Exception
    '    End Try
    '    If param > "" Then
    '        act = "edit"
    '    Else
    '        act = "input"
    '    End If
    '    Try
    '        sqlcommand.CommandText = "Select NoId, Idrec, MemberName, Relationship, Gender, Address, Occupation, PhoneNo from db_family where 1 = 1 " + param.ToString()
    '        sqlcommand.Connection = SQLConnection
    '        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
    '        Dim cb As New MySqlCommandBuilder(adapter)
    '        adapter.Fill(datatabl)
    '    Catch ex As Exception
    '    End Try
    '    If datatabl.Rows.Count > 0 Then
    '        Label38.Text = datatabl.Rows(0).Item(0).ToString
    '        txtmember.Text = datatabl.Rows(0).Item(2).ToString
    '        txtrelation.Text = datatabl.Rows(0).Item(3).ToString
    '        txtmemgender.Text = datatabl.Rows(0).Item(4).ToString
    '        txtmemadd.Text = datatabl.Rows(0).Item(5).ToString
    '        txtocc.Text = datatabl.Rows(0).Item(6).ToString
    '        txtmemph.Text = datatabl.Rows(0).Item(7).ToString
    '    End If
    'End Sub

    'Private Sub GridView5_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView5.FocusedRowChanged
    '    Dim datatabl As New DataTable
    '    Dim sqlcommand As New MySqlCommand
    '    datatabl.Clear()
    '    Dim param As String = ""
    '    Try
    '        param = "and NoId ='" + GridView5.GetFocusedRowCellValue("NoId").ToString() + "'"
    '    Catch ex As Exception
    '    End Try
    '    If param > "" Then
    '        act = "edit"
    '    Else
    '        act = "input"
    '    End If
    '    Try
    '        sqlcommand.CommandText = "Select NoId, Idrec, Position, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason from db_exp where 1 = 1 " + param.ToString()
    '        sqlcommand.Connection = SQLConnection
    '        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
    '        Dim cb As New MySqlCommandBuilder(adapter)
    '        adapter.Fill(datatabl)
    '    Catch ex As Exception
    '    End Try
    '    If datatabl.Rows.Count > 0 Then
    '        Label38.Text = datatabl.Rows(0).Item(0).ToString
    '        txtjobtitle.Text = datatabl.Rows(0).Item(2).ToString
    '        txtcompanyname.Text = datatabl.Rows(0).Item(3).ToString
    '        txtmanagername.Text = datatabl.Rows(0).Item(4).ToString
    '        txtcompadd.Text = datatabl.Rows(0).Item(5).ToString
    '        txtperiod.Text = datatabl.Rows(0).Item(6).ToString
    '        txtuntil.Text = datatabl.Rows(0).Item(7).ToString
    '        txtbasic.Text = datatabl.Rows(0).Item(8).ToString
    '        txtaddi.Text = datatabl.Rows(0).Item(9).ToString
    '        txttotalsa.Text = datatabl.Rows(0).Item(10).ToString
    '        txtreasonquit.Text = datatabl.Rows(0).Item(11).ToString
    '    End If
    'End Sub

    'Private Sub GridView6_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView6.FocusedRowChanged
    '    Dim datatabl As New DataTable
    '    Dim sqlcommand As New MySqlCommand
    '    datatabl.Clear()
    '    Dim param As String = ""
    '    Try
    '        param = "and NoId ='" + GridView6.GetFocusedRowCellValue("NoId").ToString() + "'"
    '    Catch ex As Exception
    '    End Try
    '    If param > "" Then
    '        act = "edit"
    '    Else
    '        act = "input"
    '    End If
    '    Try
    '        sqlcommand.CommandText = "Select NoId, IdRec, SkillName, SkillLevel, SkillDescription from db_empskill where 1 = 1 " + param.ToString()
    '        sqlcommand.Connection = SQLConnection
    '        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
    '        Dim cb As New MySqlCommandBuilder(adapter)
    '        adapter.Fill(datatabl)
    '    Catch ex As Exception
    '        MsgBox("Skill" & ex.Message)
    '    End Try
    '    If datatabl.Rows.Count > 0 Then
    '        Label38.Text = datatabl.Rows(0).Item(0).ToString
    '        skillname.Text = datatabl.Rows(0).Item(2).ToString
    '        skilllevel.Text = datatabl.Rows(0).Item(3).ToString
    '        skilldesc.Text = datatabl.Rows(0).Item(4).ToString
    '    End If
    'End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        txtcertificate.Text = ""
        txtyear.Text = ""
        txtreason.Text = ""
        act = "input"
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)
        txtmember.Text = ""
        txtrelation.Text = ""
        txtmemgender.Text = ""
        txtmemadd.Text = ""
        txtocc.Text = ""
        txtmemph.Text = ""
        act = "input"
    End Sub

    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs)
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

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs)
        skillname.Text = ""
        skilllevel.Text = ""
        skilldesc.Text = ""
        act = "input"
    End Sub

    Private Sub SimpleButton14_Click(sender As Object, e As EventArgs) Handles SimpleButton14.Click
        download()
    End Sub

    Private Sub txtbp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbp.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsDigit(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtidno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtidno.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtrel.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsDigit(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtblood_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtblood.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsDigit(ch) Then
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

    Private Sub txtphoneno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtphoneno.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txthome_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthome.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
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

    Private Sub txtcandname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcandname.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsDigit(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtnick_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnick.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsDigit(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub CheckEdit3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit3.CheckedChanged
        If CheckEdit3.Checked = True Then
            DateTimePicker2.Enabled = True
            Dim dtr As DateTime
            DateTimePicker2.Format = DateTimePickerFormat.Custom
            DateTimePicker2.CustomFormat = "yyyy-MM-dd"
            dtr = DateTimePicker2.Value
        ElseIf CheckEdit3.Checked = False Then
            DateTimePicker2.Enabled = False
            Dim dtr As DateTime
            DateTimePicker2.Format = DateTimePickerFormat.Custom
            DateTimePicker2.CustomFormat = "----"
            dtr = DateTimePicker2.Value
        End If
    End Sub

    Private Sub SimpleButton15_Click(sender As Object, e As EventArgs) Handles SimpleButton15.Click
        edu()
    End Sub

    Private Sub SimpleButton16_Click(sender As Object, e As EventArgs) Handles SimpleButton16.Click
        cert()
    End Sub

    Private Sub SimpleButton17_Click(sender As Object, e As EventArgs) Handles SimpleButton17.Click
        fam()
    End Sub

    Private Sub SimpleButton18_Click(sender As Object, e As EventArgs) Handles SimpleButton18.Click
        skill()
    End Sub

    Private Sub SimpleButton19_Click(sender As Object, e As EventArgs) Handles SimpleButton19.Click
        exp()
    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Sub GridView3_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView3.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            'e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf SimpleButton15_Click), GetImage))
        End If
    End Sub

    Private Sub GridView2_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView2.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            ' e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf SimpleButton16_Click), GetImage))
        End If
    End Sub

    Private Sub GridView4_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView4.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            '   e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf SimpleButton17_Click), GetImage))
        End If
    End Sub

    Private Sub GridView5_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView5.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            ' e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf SimpleButton18_Click), GetImage))
        End If
    End Sub

    Private Sub GridView6_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView6.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            '   e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf SimpleButton19_Click), GetImage))
        End If
    End Sub

    Private Sub txtidrec_TextChanged(sender As Object, e As EventArgs) Handles txtidrec.TextChanged

    End Sub

    'Private Sub GridView3_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
    '    Dim datatabl As New DataTable
    '    Dim sqlcommand As New MySqlCommand
    '    datatabl.Clear()
    '    Dim param As String = ""
    '    Try
    '        param = "and NoId ='" + GridView3.GetFocusedRowCellValue("NoId").ToString() + "'"
    '    Catch ex As Exception
    '    End Try
    '    If param > "" Then
    '        act = "edit"
    '    Else
    '        act = "input"
    '    End If
    '    Try
    '        sqlcommand.CommandText = "Select NoId, Idrec, School, GraduatedYear, StudyField from db_education where 1 = 1 " + param.ToString()
    '        sqlcommand.Connection = SQLConnection
    '        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
    '        Dim cb As New MySqlCommandBuilder(adapter)
    '        adapter.Fill(datatabl)
    '    Catch ex As Exception
    '    End Try

    '    If datatabl.Rows.Count > 0 Then
    '        Label38.Text = datatabl.Rows(0).Item(0).ToString
    '        txtschoolname.Text = datatabl.Rows(0).Item(2).ToString
    '        txtyears.Text = datatabl.Rows(0).Item(3).ToString
    '        txtmajor.Text = datatabl.Rows(0).Item(4).ToString
    '    End If
    'End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        For index As Integer = 0 To tableedu.Rows.Count - 1
            If ComboBoxEdit1.SelectedItem Is tableedu.Rows(index).Item(2).ToString Then
                Label38.Text = tableedu.Rows(index).Item(0).ToString
                txtyears.Text = tableedu.Rows(index).Item(3).ToString
                txtmajor.Text = tableedu.Rows(index).Item(4).ToString
            End If
        Next
    End Sub

    Private Sub ComboBoxEdit2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit2.SelectedIndexChanged
        For index As Integer = 0 To tablecer.Rows.Count - 1
            If ComboBoxEdit2.SelectedItem Is tablecer.Rows(index).Item(2).ToString Then
                Label38.Text = tablecer.Rows(index).Item(0).ToString
                txtyear.Text = tablecer.Rows(index).Item(3).ToString
                txtreason.Text = tablecer.Rows(index).Item(4).ToString
            End If
        Next
    End Sub

    Private Sub ComboBoxEdit3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit3.SelectedIndexChanged
        For index As Integer = 0 To tablefam.Rows.Count - 1
            If ComboBoxEdit3.SelectedItem Is tablefam.Rows(index).Item(2).ToString Then
                Label38.Text = tablefam.Rows(index).Item(0).ToString
                txtrelation.Text = tablefam.Rows(index).Item(3).ToString
                txtmemgender.Text = tablefam.Rows(index).Item(4).ToString
                txtmemadd.Text = tablefam.Rows(index).Item(5).ToString
                txtocc.Text = tablefam.Rows(index).Item(6).ToString
                txtmemph.Text = tablefam.Rows(index).Item(6).ToString
            End If
        Next
    End Sub

    Private Sub ComboBoxEdit5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit5.SelectedIndexChanged
        For index As Integer = 0 To tableexp.Rows.Count - 1
            If ComboBoxEdit5.SelectedItem Is tableexp.Rows(index).Item(2).ToString Then
                Label38.Text = tableexp.Rows(index).Item(0).ToString
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

    Private Sub ComboBoxEdit6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit6.SelectedIndexChanged
        For index As Integer = 0 To tablesk.Rows.Count - 1
            If ComboBoxEdit6.SelectedItem Is tablesk.Rows(index).Item(2).ToString Then
                Label38.Text = tablesk.Rows(index).Item(0).ToString
                skilllevel.Text = tablesk.Rows(index).Item(3).ToString
                skilldesc.Text = tablesk.Rows(index).Item(4).ToString
            End If
        Next
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        act = "edit"
        ComboBoxEdit1.Enabled = True
        SimpleButton7.Enabled = True
        ComboBoxEdit1.Visible = True
        txtschoolname.Visible = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        act = "input"
        txtschoolname.Enabled = True
        SimpleButton7.Enabled = True
        txtschoolname.Visible = True
        ComboBoxEdit1.Visible = False
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        act = "delete"
        ComboBoxEdit1.Enabled = True
        SimpleButton7.Enabled = True
        txtschoolname.Visible = False
        ComboBoxEdit1.Visible = True
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        act = "edit"
        ComboBoxEdit2.Enabled = True
        SimpleButton5.Enabled = True
        ComboBoxEdit2.Visible = True
        txtcertificate.Visible = False
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        act = "input"
        txtcertificate.Enabled = True
        SimpleButton5.Enabled = True
        txtcertificate.Visible = True
        ComboBoxEdit2.Visible = False
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        act = "delete"
        ComboBoxEdit2.Enabled = True
        SimpleButton5.Enabled = True
        txtcertificate.Visible = False
        ComboBoxEdit2.Visible = True
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        act = "edit"
        ComboBoxEdit3.Enabled = True
        SimpleButton6.Enabled = True
        ComboBoxEdit3.Visible = True
        txtmember.Visible = False
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        act = "input"
        txtmember.Enabled = True
        SimpleButton6.Enabled = True
        txtmember.Visible = True
        ComboBoxEdit3.Visible = False
    End Sub

    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        act = "delete"
        ComboBoxEdit3.Enabled = True
        SimpleButton6.Enabled = True
        txtmember.Visible = False
        ComboBoxEdit3.Visible = True
    End Sub

    Private Sub RadioButton10_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton10.CheckedChanged
        act = "edit"
        ComboBoxEdit5.Enabled = True
        SimpleButton8.Enabled = True
        ComboBoxEdit5.Visible = True
        txtjobtitle.Visible = False
    End Sub

    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        act = "input"
        txtjobtitle.Enabled = True
        SimpleButton8.Enabled = True
        txtjobtitle.Visible = True
        ComboBoxEdit5.Visible = False
    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        act = "delete"
        ComboBoxEdit5.Enabled = True
        SimpleButton8.Enabled = True
        txtjobtitle.Visible = False
        ComboBoxEdit5.Visible = True
    End Sub

    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        act = "edit"
        ComboBoxEdit6.Enabled = True
        SimpleButton13.Enabled = True
        ComboBoxEdit6.Visible = True
        skillname.Visible = False
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        act = "input"
        skillname.Enabled = True
        SimpleButton13.Enabled = True
        skillname.Visible = True
        ComboBoxEdit6.Visible = False
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        act = "delete"
        ComboBoxEdit6.Enabled = True
        SimpleButton13.Enabled = True
        skillname.Visible = False
        ComboBoxEdit6.Visible = True
    End Sub
End Class