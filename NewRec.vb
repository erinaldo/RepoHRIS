Imports System.IO
Public Class NewRec
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

    Sub insertion()
        Dim dtr, dtb, dta As DateTime
        txtbod.Format = DateTimePickerFormat.Custom
        txtbod.CustomFormat = "yyyy-MM-dd"
        dtr = txtbod.Value
        txtapplieddate.Format = DateTimePickerFormat.Custom
        txtapplieddate.CustomFormat = "yyyy-MM-dd"
        dtb = txtapplieddate.Value
        Dim hasil, lastres As Integer
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "Select count(*) from db_recruitment where IdNumber = '" & txtidno.Text & "'"
            hasil = CInt(cmd.ExecuteScalar)
            If hasil = 0 Then
                lastres = 1
            ElseIf hasil = 1 OrElse hasil > 1 Then
                lastres = hasil + 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        TextEdit1.Text = lastres.ToString
        If lastres = 1 OrElse lastres = 2 Then
            txtofloc.Text = "Pending"
        ElseIf lastres = 4 OrElse lastres > 4 Then
            txtofloc.Text = "Blocked"
        End If
        Try
            Dim use As MySqlCommand = SQLConnection.CreateCommand
            use.CommandText = "select user from db_temp"
            Dim user As String = CStr(use.ExecuteScalar)
            Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
            cmmd.CommandText = "insert into db_recruitment " +
                            " (IdRec, InterviewTimes, FullName, PlaceOfBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Photo, Status, InterviewDate, Interviewdates, CV, Reason, CreatedDate, Position, ExpectedSalary, NickName, ApplicationDate, Weight, Height, BloodType, City, ZIP, HomeNumber, RecommendedBy, Martial, LastSalary, OtherIncome, ExpFacilities, FavoriteJob, FWhy, AppliedHere, AWhy, Family, Who, Strenghts, Weakness, Suitable, CarrierObject, Reference, PrivateEmail, ChangeBy, ChangeDates)" +
                            " values (@IdRec, @InterviewTimes, @FullName, @PlaceOfBirth, @DateOfBirth, @Address, @Gender, @Religion, @PhoneNumber, @IdNumber, @Photo, @Status, @InterviewDate, @Interviewdates, @CV, @Reason, @CreatedDate, @Position, @ExpectedSalary, @NickName, @ApplicationDate, @Weight, @Height, @BloodType, @City, @ZIP, @HomeNumber, @RecommendedBy, @Martial, @LastSalary, @OtherIncome, @ExpFacilities, @FavoriteJob, @FWhy, @AppliedHere, @AWhy, @Family, @Who, @Strenghts, @Weakness, @Suitable, @CarrierObject, @Reference, @PrivateEmail, @ChangeBy, @ChangeDates)"
            cmmd.Parameters.AddWithValue("@IdRec", txtidrec.Text)
            cmmd.Parameters.AddWithValue("@Interviewtimes", TextEdit1.Text)
            cmmd.Parameters.AddWithValue("@FullName", txtcandname.Text)
            cmmd.Parameters.AddWithValue("@PlaceOfBirth", txtbp.Text)
            cmmd.Parameters.AddWithValue("@DateOfBirth", dtr.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@Address", txtadd.Text)
            cmmd.Parameters.AddWithValue("@Gender", txtgend.Text)
            cmmd.Parameters.AddWithValue("@Religion", txtrel.Text)
            cmmd.Parameters.AddWithValue("@PhoneNumber", txtphoneno.Text)
            cmmd.Parameters.AddWithValue("@IdNumber", txtidno.Text)
            If Not ImageEdit1.Text Is Nothing Then
                Dim param As New MySqlParameter("@Photo", ImageToByte(pictureEdit))
                cmmd.Parameters.Add(param)
            Else
                cmmd.Parameters.AddWithValue("@Photo", "")
            End If
            cmmd.Parameters.AddWithValue("@Status", txtofloc.Text)
            If CheckEdit3.Checked = True Then
                DateTimePicker2.Format = DateTimePickerFormat.Custom
                DateTimePicker2.CustomFormat = "yyyy-MM-dd"
                dta = DateTimePicker2.Value
                dta = CDate(dta.ToString("yyyy-MM-dd"))
                cmmd.Parameters.AddWithValue("@interviewdate", dta.ToString("yyyy-MM-dd"))
            ElseIf CheckEdit3.Checked = False Then
                cmmd.Parameters.AddWithValue("@interviewdate", Nothing)
            End If
            cmmd.Parameters.AddWithValue("@InterviewDates", Nothing)
            Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
            Dim finfo As New FileInfo(Label38.Text)
            Dim numBytes As Long = finfo.Length
            Dim fstream As New FileStream(Label38.Text, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fstream)
            Dim data As Byte() = br.ReadBytes(CInt(numBytes))
            br.Close()
            fstream.Close()
            If Not data Is Nothing Then
                cmmd.Parameters.AddWithValue("@cv", data)
            Else
                cmmd.Parameters.AddWithValue("@cv", "")
            End If
            cmmd.Parameters.AddWithValue("@Reason", txtcandreason.Text)
            cmmd.Parameters.AddWithValue("@CreatedDate", Date.Now)
            cmmd.Parameters.AddWithValue("@Position", TextEdit7.Text)
            cmmd.Parameters.AddWithValue("@ExpectedSalary", TextBox5.Text)
            cmmd.Parameters.AddWithValue("@NickName", txtnick.Text)
            cmmd.Parameters.AddWithValue("@ApplicationDate", dtb.ToString("yyyy-MM-dd"))
            cmmd.Parameters.AddWithValue("@Weight", txtkg.Text)
            cmmd.Parameters.AddWithValue("@Height", txtcm.Text)
            cmmd.Parameters.AddWithValue("@BloodType", txtblood.Text)
            cmmd.Parameters.AddWithValue("@City", txtcity.Text)
            cmmd.Parameters.AddWithValue("@Zip", txtzip.Text)
            cmmd.Parameters.AddWithValue("@HomeNumber", txthome.Text)
            cmmd.Parameters.AddWithValue("@RecommendedBy", txtrecby.Text)
            cmmd.Parameters.AddWithValue("@Martial", ComboBoxEdit4.Text)
            cmmd.Parameters.AddWithValue("@LastSalary", TextBox3.Text)
            cmmd.Parameters.AddWithValue("@otherIncome", TextBox4.Text)
            cmmd.Parameters.AddWithValue("@ExpFacilities", RichTextBox1.Text)
            cmmd.Parameters.AddWithValue("@FavoriteJob", TextBox6.Text)
            cmmd.Parameters.AddWithValue("@FWhy", RichTextBox2.Text)
            cmmd.Parameters.AddWithValue("@AppliedHere", CheckEdit1.Checked)
            cmmd.Parameters.AddWithValue("@AWhy", RichTextBox3.Text)
            cmmd.Parameters.AddWithValue("@Family", CheckEdit2.Checked)
            cmmd.Parameters.AddWithValue("@Who", RichTextBox4.Text)
            cmmd.Parameters.AddWithValue("@Strenghts", RichTextBox5.Text)
            cmmd.Parameters.AddWithValue("@Weakness", RichTextBox6.Text)
            cmmd.Parameters.AddWithValue("@Suitable", RichTextBox7.Text)
            cmmd.Parameters.AddWithValue("@CarrierObject", RichTextBox8.Text)
            cmmd.Parameters.AddWithValue("@Reference", RichTextBox9.Text)
            cmmd.Parameters.AddWithValue("@PrivateEmail", txtwemail.Text)
            cmmd.Parameters.AddWithValue("@ChangeBy", Label40.Text)
            cmmd.Parameters.AddWithValue("@ChangeDates", Date.Now)
            cmmd.ExecuteNonQuery()
            MsgBox("Saved")
            cleartxt()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub enable2d()
        txtcandname.Enabled = True
        txtbp.Enabled = True
        txtadd.Enabled = True
        txtgend.Enabled = True
        txtrel.Enabled = True
        txtphoneno.Enabled = True
        txtcandreason.Enabled = True
        TextEdit7.Enabled = True
        TextBox5.Enabled = True
        txtnick.Enabled = True
        txtkg.Enabled = True
        txtcm.Enabled = True
        txtblood.Enabled = True
        txtcity.Enabled = True
        txtzip.Enabled = True
        txthome.Enabled = True
        txtrecby.Enabled = True
        ComboBoxEdit4.Enabled = True
        txtwemail.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        RichTextBox1.Enabled = True
        TextBox6.Enabled = True
        RichTextBox2.Enabled = True
        CheckEdit1.Checked = True
        RichTextBox3.Enabled = True
        CheckEdit2.Checked = True
        RichTextBox4.Enabled = True
        RichTextBox5.Enabled = True
        RichTextBox6.Enabled = True
        RichTextBox7.Enabled = True
        RichTextBox8.Enabled = True
        RichTextBox9.Enabled = True
    End Sub

    Sub enable()
        txtcandname.Enabled = False
        txtbp.Enabled = False
        txtadd.Enabled = False
        txtgend.Enabled = False
        txtrel.Enabled = False
        txtphoneno.Enabled = False
        txtcandreason.Enabled = False
        TextEdit7.Enabled = False
        TextBox5.Enabled = False
        txtnick.Enabled = False
        txtkg.Enabled = False
        txtcm.Enabled = False
        txtblood.Enabled = False
        txtcity.Enabled = False
        txtzip.Enabled = False
        txthome.Enabled = False
        txtrecby.Enabled = False
        ComboBoxEdit4.Enabled = False
        txtwemail.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        RichTextBox1.Enabled = False
        TextBox6.Enabled = False
        RichTextBox2.Enabled = False
        CheckEdit1.Checked = False
        RichTextBox3.Enabled = False
        CheckEdit2.Checked = False
        RichTextBox4.Enabled = False
        RichTextBox5.Enabled = False
        RichTextBox6.Enabled = False
        RichTextBox7.Enabled = False
        RichTextBox8.Enabled = False
        RichTextBox9.Enabled = False
    End Sub

    Sub cleartxt()
        txtcandname.Text = ""
        txtbp.Text = ""
        txtadd.Text = ""
        txtgend.Text = ""
        txtrel.Text = ""
        txtphoneno.Text = ""
        txtidno.Text = ""
        txtcandreason.Text = ""
        TextEdit7.Text = ""
        TextBox5.Text = ""
        txtnick.Text = ""
        txtkg.Text = ""
        txtcm.Text = ""
        txtblood.Text = ""
        txtcity.Text = ""
        txtzip.Text = ""
        txthome.Text = ""
        txtrecby.Text = ""
        ComboBoxEdit4.Text = ""
        txtwemail.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        RichTextBox1.Text = ""
        TextBox6.Text = ""
        RichTextBox2.Text = ""
        CheckEdit1.Checked = False
        RichTextBox3.Text = ""
        CheckEdit2.Checked = False
        RichTextBox4.Text = ""
        RichTextBox5.Text = ""
        RichTextBox6.Text = ""
        RichTextBox7.Text = ""
        RichTextBox8.Text = ""
        RichTextBox9.Text = ""
    End Sub

    Dim tbl_par As New DataTable

    Sub changer()
        Dim ynow As String = Format(Now, "yy").ToString
        Dim mnow As String = Month(Now).ToString
        Dim lastn As Integer
        Dim maxid As Integer
        Dim newid As Integer = maxid + 1
        Dim updmon As String
        Dim lastcode As String = ""
        Dim tmp As Integer
        Try
            Dim cmd = SQLConnection.CreateCommand()
            cmd.CommandText = "SELECT IdRec FROM id_last_num"
            lastn = DirectCast(cmd.ExecuteScalar(), Integer)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT Idrec FROM db_recruitment ORDER BY IdRec DESC LIMIT 1"
            lastcode = DirectCast(cmd.ExecuteScalar(), String)
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT MID(Idrec, 8, 1) FROM db_recruitment where idrec = '" & lastcode & "'"
            updmon = DirectCast(cmd.ExecuteScalar(), String)
            If CInt(updmon) <> CInt(mnow) Then
                tmp = 1
            Else
                tmp = lastn + 1
            End If
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
        Dim actualcode As String = "REQ" & "-" & ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
        txtidrec.Text = actualcode.ToString
    End Sub

    Private Sub NewEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        txtapplieddate.Format = DateTimePickerFormat.Custom
        txtapplieddate.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = Nothing
        changer()
        Label38.Text = "file.pdf"
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        ' cleartxt()
        Reset()
        barJudul.Caption = "Add Recruitment"
    End Sub

    Private Sub NewEmployee_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        'Location = New Point(500, 200)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        cleartxt()
    End Sub

    Private Sub btnPhoto_Click(sender As Object, e As EventArgs)
        Using dialog As New OpenFileDialog
            If dialog.ShowDialog() <> DialogResult.OK Then Return
            pictureEdit.Image = Image.FromFile(dialog.FileName)
        End Using
    End Sub

    Private Sub BarButtonItem2_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        cleartxt()
        barJudul.Caption = "Change Data"
        Reset()
    End Sub

    Dim openfd As New OpenFileDialog

    Private Sub btnCV_Click(sender As Object, e As EventArgs)
        openfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
        openfd.Title = "Open a CV File"
        openfd.Filter = "Word Files|*.docx|Text Files|*.txt"
        openfd.ShowDialog()
    End Sub

    Private Sub OpenPreviewWindows()
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

    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        openfd.InitialDirectory = "C:\"
        openfd.Title = "Open a CV FIle"
        openfd.Filter = "PDF Files|*.pdf|Text Files|*.txt|Word FIles|*.docx"
        openfd.ShowDialog()
        Label38.Text = openfd.FileName
    End Sub

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Using dialog As New OpenFileDialog
            If dialog.ShowDialog() <> DialogResult.OK Then Return
            pictureEdit.Image = Image.FromFile(dialog.FileName)
        End Using
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        If txtcandname.Text = "" OrElse TextEdit7.Text = "" OrElse txtidno.Text = "" Then
            MsgBox("Please fill the empty fields", MsgBoxStyle.Information)
        Else
            insertion()
            changer()
        End If
    End Sub

    Private Sub CheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit1.CheckedChanged
        If CheckEdit1.Checked = True Then
            RichTextBox3.Enabled = True
        Else
            RichTextBox3.Enabled = False
        End If
    End Sub

    Private Sub CheckEdit2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit1.Checked = True Then
            RichTextBox4.Enabled = True
        Else
            RichTextBox4.Enabled = False
        End If
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

    Sub loadschool()
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlCommand As New MySqlCommand
        Try
            sqlCommand.CommandText = "select Idrec as IdRecruitment, School, GraduatedYear, StudyField from db_education where idrec = '" & txtidrec.Text & "'"
            sqlCommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridView1.BestFitColumns()
    End Sub

    Sub school()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_education " +
                           "(IdRec, School, GraduatedYear, StudyField)" +
                            "values (@IdRec, @School, @GraduatedYear, @StudyField)"
        cmmd.Parameters.AddWithValue("@Idrec", txtidrec.Text)
        cmmd.Parameters.AddWithValue("@School", txtschoolname.Text)
        cmmd.Parameters.AddWithValue("@GraduatedYear", txtyears.Text)
        cmmd.Parameters.AddWithValue("@StudyField", txtmajor.Text)
        cmmd.ExecuteNonQuery()
        loadschool()
        txtschoolname.Text = ""
        txtyears.Text = ""
        txtmajor.Text = ""
    End Sub

    Sub loadcertification()
        GridControl2.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select Idrec as IdRecruitment, Certificates, Years, Reasons from db_certificates where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl2.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridView2.BestFitColumns()
    End Sub

    Sub certificates()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_certificates " +
                            "(idrec, certificates, years, reasons)" +
                            "values(@idrec, @certificates, @years, @reasons)"
        cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
        cmmd.Parameters.AddWithValue("@certificates", txtcertificate.Text)
        cmmd.Parameters.AddWithValue("@years", txtyear.Text)
        cmmd.Parameters.AddWithValue("@reasons", txtreason.Text)
        cmmd.ExecuteNonQuery()
        loadcertification()
        txtcertificate.Text = ""
        txtyear.Text = ""
        txtreason.Text = ""
    End Sub

    Sub loadfamily()
        GridControl3.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select Idrec, MemberName, Gender, Address, occupation, PhoneNo from db_family where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl3.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridView3.BestFitColumns()
    End Sub

    Sub family()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_family " +
                            " (idrec, MemberName, Gender, Address, Occupation, PhoneNo)" +
                            "values (@idrec, @MemberName, @Gender, @Address, @Occupation, @PhoneNo)"
        cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
        cmmd.Parameters.AddWithValue("@MemberName", txtmember.Text)
        cmmd.Parameters.AddWithValue("@Gender", txtmemgender.Text)
        cmmd.Parameters.AddWithValue("@Address", txtmemadd.Text)
        cmmd.Parameters.AddWithValue("@Occupation", txtocc.Text)
        cmmd.Parameters.AddWithValue("@PhoneNo", txtmemph.Text)
        cmmd.ExecuteNonQuery()
        loadfamily()
        txtmember.Text = ""
        txtmemgender.Text = ""
        txtmemadd.Text = ""
        txtocc.Text = ""
        txtmemph.Text = ""
    End Sub

    Sub loadexp()
        GridControl4.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select Idrec, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason from db_exp where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl4.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridView4.BestFitColumns()
    End Sub

    Sub exp()
        Dim dtr, dtb As DateTime
        txtperiod.Format = DateTimePickerFormat.Custom
        txtperiod.CustomFormat = "yyyy-MM-dd"
        dtr = txtperiod.Value
        txtuntil.Format = DateTimePickerFormat.Custom
        txtuntil.CustomFormat = "yyyy-MM-dd"
        dtb = txtuntil.Value

        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_exp " +
                            "(idrec, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason) " +
                            "values(@idrec, @Company, @Manager, @Address, @Period, @Until, @BasicSalary, @AdditionalSalary, @TotalSalary, @QuitReason)"
        cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
        cmmd.Parameters.AddWithValue("@company", txtcompanyname.Text)
        cmmd.Parameters.AddWithValue("@Manager", txtmanagername.Text)
        cmmd.Parameters.AddWithValue("@Address", txtcompadd.Text)
        cmmd.Parameters.AddWithValue("@Period", dtr.ToString("yyyy-MM-dd"))
        cmmd.Parameters.AddWithValue("@Until", dtb.ToString("yyyy-MM-dd"))
        cmmd.Parameters.AddWithValue("@BasicSalary", txtbasic.Text)
        cmmd.Parameters.AddWithValue("@AdditionalSalary", txtaddi.Text)
        cmmd.Parameters.AddWithValue("@Totalsalary", txttotalsa.Text)
        cmmd.Parameters.AddWithValue("@QuitReason", txtreasonquit.Text)
        cmmd.ExecuteNonQuery()
        loadexp()
        txtcompanyname.Text = ""
        txtmanagername.Text = ""
        txtcompadd.Text = ""
        txtbasic.Text = ""
        txtaddi.Text = ""
        txttotalsa.Text = ""
        txtreasonquit.Text = ""
    End Sub

    Sub loadskill()
        GridControl5.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select Idrec as IdRecruitment, SkillName, SkillLevel, SkillDescription from db_empskill where idrec = '" & txtidrec.Text & "'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl5.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridView5.BestFitColumns()
    End Sub

    Sub skill()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "insert into db_empskill " +
                            "(idrec, skillname, skilllevel, skilldescription)" +
                            "values(@idrec, @skillname, @skilllevel, @skilldescription)"
        cmmd.Parameters.AddWithValue("@idrec", txtidrec.Text)
        cmmd.Parameters.AddWithValue("@skillname", skillname.Text)
        cmmd.Parameters.AddWithValue("@skilllevel", skilllevel.Text)
        cmmd.Parameters.AddWithValue("@skilldescription", skilldesc.Text)
        cmmd.ExecuteNonQuery()
        loadskill()
        skillname.Text = ""
        skilllevel.Text = ""
        skilldesc.Text = ""
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

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        school()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        certificates()
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        family()
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        exp()
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        skill()
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

    Private Sub txtidno_EditValueChanged(sender As Object, e As EventArgs) Handles txtidno.EditValueChanged
        Dim hasil, lastres, hsl As Integer
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "Select count(*) from db_recruitment where IdNumber = '" & txtidno.Text & "'"
            hasil = CInt(cmd.ExecuteScalar)
            If hasil = 0 Then
                lastres = 1
            ElseIf hasil = 1 OrElse hasil > 1 Then
                lastres = hasil + 1
            End If
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select applicationdate from db_recruitment where idnumber = '" & txtidno.Text & "'"
            Dim quer11 As Date = CDate(query.ExecuteScalar)
            dt1 = CDate(quer11.ToShortDateString)
            dt2 = Date.Now
            dt3 = dt2 - dt1
            diff = dt3.Days
            Dim hasilx As String = CType(Int(diff / 365), String)
            If CInt(hasil) > 2 Then
                lastres = 1
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Dim cmd2 As MySqlCommand = SQLConnection.CreateCommand
        cmd2.CommandText = "select count(blacklist) from db_recruitment where IdNumber = '" & txtidno.Text & "' and blacklist = '1'"
        hsl = CInt(cmd2.ExecuteScalar)
        If hsl = 1 Then
            MsgBox("This Candidates is already on the 'BLACKLISTS ' list for infraction", MsgBoxStyle.Exclamation)
            Label39.Text = "BLACKLISTED"
        ElseIf hsl <> 1 Then
            Label39.Text = ""
        End If
        TextEdit1.Text = lastres.ToString
        If lastres = 1 OrElse lastres = 2 Then
            txtofloc.Text = "Pending"
        ElseIf lastres > 2 Then
            txtofloc.Text = "Blocked"
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

    Private Sub txtgend_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtgend.KeyPress
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

    Private Sub txtmemph_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmemph.KeyPress
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
            txtrecby.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtrecby_EditValueChanged(sender As Object, e As EventArgs) Handles txtrecby.EditValueChanged
        Timer1.Stop()
    End Sub

    Private Sub txtofloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtofloc.SelectedIndexChanged
        'If txtofloc.Text = "Blocked" Then
        '    enable()
        'End If
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        If TextEdit1.Text <> "4" Then
            enable2d()
        Else
            enable()
        End If
    End Sub
End Class