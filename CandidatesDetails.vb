Imports System.IO
Imports DevExpress.XtraCharts

Public Class CandidatesDetails
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

    Private Sub CandidatesDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select idrec from db_tmpname where 1 = 1"
        Dim quer As String = CType(query.ExecuteScalar, String)
        txtidrec.Text = quer.ToString
        loaddata1()
        loaddata()
        autochange()
        autochx()
        loadschool()
        loadcert()
        loadexp()
        loadskill()
        loadfamily()
        grafik2()
        GridView1.BestFitColumns()
        GridView2.BestFitColumns()
        GridView3.BestFitColumns()
        GridView4.BestFitColumns()
        GridView5.BestFitColumns()
        GridView6.BestFitColumns()
        GridView7.BestFitColumns()
        GridView8.BestFitColumns()
        Dim query1 As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select status from db_recruitment where idrec = '" & txtidrec.Text & "'"
        Dim quer1 As String = CStr(query.ExecuteScalar)
        If quer1 = "In progress" Then
            SimpleButton5.Enabled = True
            SimpleButton6.Enabled = True
        ElseIf quer1 = "Rejected" OrElse quer1 = "Processed" OrElse quer1 = "Pending" Then
            SimpleButton5.Enabled = False
            SimpleButton6.Enabled = False
        End If
    End Sub

    Private Sub grafik3()
        ' ChartControl2.Series.Clear()
        ' Add a radar series to it.
        Dim series1 As New Series("Series 1", ViewType.RadarArea)
        Dim a As MySqlCommand = SQLConnection.CreateCommand
        a.CommandText = "select skill1 from db_skills where idrec = '" & txtidrec.Text & "'"
        Dim hasila As Integer = CInt(a.ExecuteScalar)
        Dim b As MySqlCommand = SQLConnection.CreateCommand
        b.CommandText = "select skill2 from db_skills where idrec = '" & txtidrec.Text & "'"
        Dim hasilb As Integer = CInt(b.ExecuteScalar)
        Dim c As MySqlCommand = SQLConnection.CreateCommand
        c.CommandText = "select skill3 from db_skills where idrec = '" & txtidrec.Text & "'"
        Dim hasilc As Integer = CInt(c.ExecuteScalar)
        Dim d As MySqlCommand = SQLConnection.CreateCommand
        d.CommandText = "select skill4 from db_skills where idrec = '" & txtidrec.Text & "'"
        Dim hasild As Integer = CInt(d.ExecuteScalar)
        Dim e As MySqlCommand = SQLConnection.CreateCommand
        e.CommandText = "select skill5 from db_skills where idrec = '" & txtidrec.Text & "'"
        Dim hasile As Integer = CInt(e.ExecuteScalar)
        ' Populate the series with points.
        series1.Points.Add(New SeriesPoint("Leadership", hasila))
        series1.Points.Add(New SeriesPoint("Knowledge", hasilb))
        series1.Points.Add(New SeriesPoint("Confidence", hasilc))
        series1.Points.Add(New SeriesPoint("Teamwork", hasild))
        series1.Points.Add(New SeriesPoint("Individual", hasile))
        ' Add the series to the chart.
        ChartControl2.Series.Add(series1)
        ' Flip the diagram (if necessary).
        CType(ChartControl2.Diagram, RadarDiagram).StartAngleInDegrees = 180
        CType(ChartControl2.Diagram, RadarDiagram).RotationDirection =
            RadarDiagramRotationDirection.Counterclockwise
        ' Add a title to the chart and hide the legend.
        Dim chartTitle1 As New ChartTitle()
        chartTitle1.Text = "Radar Area Chart"
        'ChartControl2.Titles.Add(chartTitle1)
        ChartControl2.Legend.Visible = False
    End Sub

    Private Sub grafik2()
        Dim series1 As New Series("Series 1", ViewType.RadarArea)
        Dim a As MySqlCommand = SQLConnection.CreateCommand
        a.CommandText = "select skill1 from db_skills where idrec = '" & Label25.Text & "'"
        Dim hasila As Integer = CInt(a.ExecuteScalar)
        Dim b As MySqlCommand = SQLConnection.CreateCommand
        b.CommandText = "select skill2 from db_skills where idrec = '" & Label25.Text & "'"
        Dim hasilb As Integer = CInt(b.ExecuteScalar)
        Dim c As MySqlCommand = SQLConnection.CreateCommand
        c.CommandText = "select skill3 from db_skills where idrec = '" & Label25.Text & "'"
        Dim hasilc As Integer = CInt(c.ExecuteScalar)
        Dim d As MySqlCommand = SQLConnection.CreateCommand
        d.CommandText = "select skill4 from db_skills where idrec = '" & Label25.Text & "'"
        Dim hasild As Integer = CInt(d.ExecuteScalar)
        Dim e As MySqlCommand = SQLConnection.CreateCommand
        e.CommandText = "select skill5 from db_skills where idrec = '" & Label25.Text & "'"
        Dim hasile As Integer = CInt(e.ExecuteScalar)
        series1.Points.Add(New SeriesPoint("Leadership", hasila))
        series1.Points.Add(New SeriesPoint("Knowledge", hasilb))
        series1.Points.Add(New SeriesPoint("Confidence", hasilc))
        series1.Points.Add(New SeriesPoint("Teamwork", hasild))
        series1.Points.Add(New SeriesPoint("Individual", hasile))
        ChartControl2.Series.Add(series1)
        CType(ChartControl2.Diagram, RadarDiagram).StartAngleInDegrees = 180
        CType(ChartControl2.Diagram, RadarDiagram).RotationDirection =
            RadarDiagramRotationDirection.Counterclockwise
        Dim chartTitle1 As New ChartTitle()
        chartTitle1.Text = "Radar Area Chart"
        ChartControl2.Legend.Visible = False
    End Sub

    Dim tbl_par2, tbl_par3 As New DataTable

    Sub loadschool()
        Dim sqlCommand As New MySqlCommand
        sqlCommand.CommandText = "Select IdRec as IdRecruitment, School as SchoolOrUniversity, GraduatedYear, StudyField from db_education where IdRec = '" & txtidrec.Text & "'"
        sqlCommand.Connection = SQLConnection
        Dim dt As New DataTable
        dt.Load(sqlCommand.ExecuteReader)
        GridControl3.DataSource = dt
    End Sub

    Sub loadcert()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "Select Idrec as IdRecruitment, Certificates, Years, Reasons from db_certificates where idrec = '" & txtidrec.Text & "'"
        sqlcommand.Connection = SQLConnection
        Dim dt As New DataTable
        dt.Load(sqlcommand.ExecuteReader)
        GridControl2.DataSource = dt
    End Sub

    Sub loadexp()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "Select IdRec as IdRecruitment, Company, Manager, Address, Period, Until, BasicSalary, AdditionalSalary, TotalSalary, QuitReason from db_exp where idrec = '" & txtidrec.Text & "'"
        sqlcommand.Connection = SQLConnection
        Dim dt As New DataTable
        dt.Load(sqlcommand.ExecuteReader)
        GridControl5.DataSource = dt
    End Sub

    Sub loadskill()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "Select IdRec as IdRecruitment, SkillName, SkillLevel, SkillDescription from db_empskill where IdRec = '" & txtidrec.Text & "'"
        sqlcommand.Connection = SQLConnection
        Dim dt As New DataTable
        dt.Load(sqlcommand.ExecuteReader)
        GridControl6.DataSource = dt
    End Sub

    Sub loadfamily()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "Select idrec as Idrecruitment, MemberName, Relationship, Gender, Address, Occupation, PhoneNo from db_family where idrec = '" & txtidrec.Text & "'"
        sqlcommand.Connection = SQLConnection
        Dim dt As New DataTable
        dt.Load(sqlcommand.ExecuteReader)
        GridControl4.DataSource = dt
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

    Sub loaddata1()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "SELECT * from db_recruitment where idrec = '" & txtidrec.Text & "'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par2)
        For index As Integer = 0 To tbl_par2.Rows.Count - 1
        Next
    End Sub

    Sub loaddata()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select * from db_skills where idrec = '" & txtidrec.Text & "'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par3)
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
        Next
    End Sub

    Sub autochx()
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
            TextBox1.Text = tbl_par3.Rows(index).Item(2).ToString
            TextBox2.Text = tbl_par3.Rows(index).Item(3).ToString
            TextBox7.Text = tbl_par3.Rows(index).Item(4).ToString
            TextBox8.Text = tbl_par3.Rows(index).Item(5).ToString
            TextBox9.Text = tbl_par3.Rows(index).Item(6).ToString
            TextEdit2.Text = tbl_par3.Rows(index).Item(9).ToString
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextBox10.Text = quer1.ToString

            query.CommandText = "select idrec from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            Label25.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Dim sel As New selectcand

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Start()
        If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
            sel.Close()
            sel = New selectcand
        End If
        sel.Show()
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        Timer1.Stop()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If TextBox10.Text = "" Then
            MsgBox("Insert the name to compare with")
        Else
            grafik2()
            Label27.Text = TextBox10.Text.ToString
            Label27.Visible = True
            SimpleButton2.Enabled = False
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        ChartControl2.Series.Clear()
        SimpleButton2.Enabled = True
        grafik3()
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

    Sub download()
        Try
            Dim sFilePath As String
            Dim buffer As Byte()
            Using cmd As New MySqlCommand("select cv from db_recruitment where idrec = '" & txtidrec.Text & "'", SQLConnection)
                buffer = CType(cmd.ExecuteScalar(), Byte())
            End Using
            sFilePath = Path.GetTempFileName()
            File.Move(sFilePath, Path.ChangeExtension(sFilePath, ".pdf"))
            sFilePath = Path.ChangeExtension(sFilePath, ".pdf")
            File.WriteAllBytes(sFilePath, buffer)
            Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            act.BeginInvoke(sFilePath, Nothing, Nothing)
        Catch ex As Exception
            MsgBox("View CV error  " & ex.Message)
        End Try
    End Sub

    Sub autoswitch()
        Try
            Dim sfilePath As String
            Dim buffer As Byte()
            Using cmd As MySqlCommand = SQLConnection.CreateCommand
                cmd.CommandText = "select cv from db_recruitment where idrec = '" & txtidrec.Text & "'"
                buffer = CType(cmd.ExecuteScalar(), Byte())
            End Using
            sfilePath = Path.GetTempFileName()
            File.Move(sfilePath, Path.ChangeExtension(sfilePath, ".xls"))
            sfilePath = Path.ChangeExtension(sfilePath, ".xls")
            File.WriteAllBytes(sfilePath, buffer)
            Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            act.BeginInvoke(sfilePath, Nothing, Nothing)
        Catch ex As Exception
            EndInvoke(Nothing)
        End Try
    End Sub

    Private Shared Sub OpenPDFFile(ByVal sFilePath As String)
        Using p As New Process
            p.StartInfo = New ProcessStartInfo(sFilePath)
            p.Start()
            p.WaitForExit()
            Try
                File.Delete(sFilePath)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
    End Sub

    Private Sub SimpleButton14_Click(sender As Object, e As EventArgs) Handles SimpleButton14.Click
        download()
    End Sub

    Public Function AcceptReq() As Boolean
        Dim sqlCommand As New MySqlCommand
        Dim pesan As String
        pesan = CType(MsgBox("Are You Sure To Change This Employee Status To Be Accepted ?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            sqlCommand.Connection = SQLConnection
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = "update db_recruitment set status = 'Accepted' WHERE IdRec = '" & txtidrec.Text & "'"
            sqlCommand.ExecuteNonQuery()
            MsgBox("Status Change To Be Accepted", MsgBoxStyle.Information, "Success")
            Me.Close()
        End If
        Return Nothing
    End Function

    Public Function RejectReq() As Boolean
        Dim sqlCommand As New MySqlCommand
        Dim pesan As String
        pesan = CType(MsgBox("Are You Sure To Change This Employee Status To Be Rejected ?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            sqlCommand.Connection = SQLConnection
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = "update db_recruitment set status = 'Rejected' WHERE IdRec = '" & txtidrec.Text & "'"
            sqlCommand.ExecuteNonQuery()
            MsgBox("Status Change To Be Rejected", MsgBoxStyle.Information, "Success")
            Close()
        End If
        Return Nothing
    End Function

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        AcceptReq()
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        RejectReq()
    End Sub

    Sub autochange()
        For index As Integer = 0 To tbl_par2.Rows.Count - 1
            Label25.Text = tbl_par2.Rows(index).Item(0).ToString
            TextEdit1.Text = tbl_par2.Rows(index).Item(1).ToString
            Label26.Text = tbl_par2.Rows(index).Item(2).ToString
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
End Class