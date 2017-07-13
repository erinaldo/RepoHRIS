Imports System.IO
Imports System.Windows.Forms.DataVisualization.Charting
Imports DevExpress.XtraCharts

Public Class Analytical
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

    Sub clea()
        txtnames.Text = ""
        txtid.Text = ""
        txtinterview.Text = ""
        txtpob.Text = ""
        txtage.Text = ""
        txtgender.Text = ""
        txtreligion.Text = ""
        txtidcard.Text = ""
        txtaddress.Text = ""
        txtphone.Text = ""
        txtstatus.Text = ""
        txtposition.Text = ""
        txtexpsal.Text = ""
        label2.Text = ""
        label3.Text = ""
        PictureBox1.Image = Nothing
    End Sub

    Private Sub grafik3()
        ChartControl2.Series.Clear()
        ' Add a radar series to it.
        Dim series1 As New DevExpress.XtraCharts.Series("Series 1", ViewType.RadarArea)
        Dim a As MySqlCommand = SQLConnection.CreateCommand
        a.CommandText = "select skill1 from db_skills where idrec = '" & txtid.Text & "'"
        Dim hasila As Integer = CInt(a.ExecuteScalar)
        Dim b As MySqlCommand = SQLConnection.CreateCommand
        b.CommandText = "select skill2 from db_skills where idrec = '" & txtid.Text & "'"
        Dim hasilb As Integer = CInt(b.ExecuteScalar)
        Dim c As MySqlCommand = SQLConnection.CreateCommand
        c.CommandText = "select skill3 from db_skills where idrec = '" & txtid.Text & "'"
        Dim hasilc As Integer = CInt(c.ExecuteScalar)
        Dim d As MySqlCommand = SQLConnection.CreateCommand
        d.CommandText = "select skill4 from db_skills where idrec = '" & txtid.Text & "'"
        Dim hasild As Integer = CInt(d.ExecuteScalar)
        Dim e As MySqlCommand = SQLConnection.CreateCommand
        e.CommandText = "select skill5 from db_skills where idrec = '" & txtid.Text & "'"
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

        '' Add the chart to the form.
        'ChartControl2.Dock = DockStyle.None
        'Me.Controls.Add(ChartControl2)
    End Sub

    'Dim comp As New Comparison

    'Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
    '    If comp Is Nothing OrElse comp.IsDisposed OrElse comp.MinimizeBox Then
    '        comp.Close()
    '        comp = New Comparison
    '    End If
    '    comp.Show()
    'End Sub

    'Private Sub drawchart()
    '    draw the chart
    '    Chart1.Series.Clear()
    '    Chart1.Series.Add("Recruitment Statistical")
    '    With Chart1.Series(0)
    '        .ChartType = DataVisualization.Charting.SeriesChartType.Line
    '        .BorderWidth = 1
    '        .Color = Color.Red
    '        .BorderDashStyle = ChartDashStyle.Dash
    '        .MarkerStyle = MarkerStyle.Square
    '        .MarkerSize = 4
    '        .IsVisibleInLegend = False
    '        For m = 1 To 12
    '            .Points.AddXY(m, MonthlyData(m))
    '        Next
    '    End With
    'End Sub

    Private Sub Analytical_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        loadinfo()
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

    Private Sub loadinfo()
        Try
            GridControl1.RefreshDataSource()
            Dim table As New DataTable
            Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
            sqlcommand.CommandText = "select count(a.FullName) from db_skills a, db_recruitment b where a.idrec = b.idrec and b.Status = 'In Progress'"
            Dim hasil As Integer = CInt(sqlcommand.ExecuteScalar)
            If hasil = 0 Then
                clea()
            End If
            sqlcommand.CommandText = "select a.FullName, a.IdRec from db_skills a, db_recruitment b where a.idrec = b.idrec and b.Status = 'In Progress'"
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
            GridView1.MoveLast()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Dim tbl_par As New DataTable

    Sub loadadd()
        Dim sqlCommand As New MySqlCommand
        sqlCommand.CommandText = "SELECT Position, ExpectedSalary from db_skills where idrec = '" & txtid.Text & "'"
        sqlCommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par)
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtposition.Text = (tbl_par.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub fillit()
        Dim datat As New DataTable
        Dim sqlcommand As New MySqlCommand
        datat.Clear()
        sqlcommand.CommandText = "select fullname, position, expectedsalary from db_skills where idrec = '" & txtid.Text & "'"
        sqlcommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(datat)
        If datat.Rows.Count > 0 Then
            txtposition.Text = datat.Rows(0).Item(1).ToString
            txtexpsal.Text = datat.Rows(0).Item(2).ToString
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged_1(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "and IdRec='" + GridView1.GetFocusedRowCellValue("IdRec").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT IdRec, InterviewTimes, FullName, PlaceofBirth, DateOfBirth, Address, Gender, Religion, PhoneNumber, IdNumber, Photo, Status, InterviewDate, PhoneNumber, InterviewDates, CreatedDate, Reason FROM db_recruitment WHERE 1=1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If datatabl.Rows.Count > 0 Then
            txtnames.Text = datatabl.Rows(0).Item(2).ToString()
            txtid.Text = datatabl.Rows(0).Item(0).ToString()
            txtinterview.Text = datatabl.Rows(0).Item(1).ToString()
            txtpob.Text = datatabl.Rows(0).Item(3).ToString()
            txtdob.Text = datatabl.Rows(0).Item(4).ToString()
            txtaddress.Text = datatabl.Rows(0).Item(5).ToString()
            txtgender.Text = datatabl.Rows(0).Item(6).ToString()
            Dim filefoto As Byte() = CType(datatabl.Rows(0).Item(10), Byte())
            If filefoto.Length > 0 Then
                PictureBox1.Image = ByteToImage(filefoto)
            Else
                PictureBox1.Image = Nothing
                PictureBox1.Refresh()
            End If
            txtreligion.Text = datatabl.Rows(0).Item(7).ToString()
            txtidcard.Text = datatabl.Rows(0).Item(9).ToString()
            txtstatus.Text = datatabl.Rows(0).Item(11).ToString()
            txtphone.Text = datatabl.Rows(0).Item(13).ToString()
            label2.Text = datatabl.Rows(0).Item(15).ToString
            label3.Text = datatabl.Rows(0).Item(14).ToString
        End If
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select FullName from db_recruitment where FullName Like '%" + TextEdit1.Text + "%'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

    Private Sub TextEdit1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridControl1.RefreshDataSource()
            Dim table As New DataTable
            Dim sqlcommand As New MySqlCommand
            Try
                sqlcommand.CommandText = "select FullName from db_recruitment where FullName Like '%" + TextEdit1.Text + "%'"
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

    Private Sub txtid_EditValueChanged(sender As Object, e As EventArgs) Handles txtid.EditValueChanged
        grafik3()
        fillit()
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        If txtnames.Text = "" OrElse txtid.Text = "" Then
            MsgBox("There's no candidates selected")
        Else
            Dim mess As String
            Dim down As MySqlCommand = SQLConnection.CreateCommand
            down.CommandText = "select fullname from db_recruitment where idrec = '" & txtid.Text & "'"
            Dim downres As String = CStr(down.ExecuteScalar)
            mess = CType(MsgBox("Are you sure to change " & downres & " status to be accepted ?", MsgBoxStyle.YesNo, "Warning"), String)
            If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                Dim up As MySqlCommand = SQLConnection.CreateCommand
                up.CommandText = "update db_recruitment set status = 'Accepted' where idrec = @ic"
                up.Parameters.AddWithValue("@ic", txtid.Text)
                up.ExecuteNonQuery()
                MsgBox("Status from " & downres & " is changed to be accepted")
                clea()
                loadinfo()
            End If
            loadinfo()
            'clea()
            ' fillit()
        End If
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If txtnames.Text = "" OrElse txtid.Text = "" Then
            MsgBox("There's no candidates selected")
        Else
            Dim mess As String
            Dim down As MySqlCommand = SQLConnection.CreateCommand
            down.CommandText = "select fullname from db_recruitment where idrec = '" & txtid.Text & "'"
            Dim downres As String = CStr(down.ExecuteScalar)
            mess = CType(MsgBox("Are you sure to change " & downres & " status to be rejected ?", MsgBoxStyle.YesNo, "Warning"), String)
            If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                Dim up As MySqlCommand = SQLConnection.CreateCommand
                up.CommandText = "update db_recruitment set status = 'Rejected' where idrec = @ic"
                up.Parameters.AddWithValue("@ic", txtid.Text)
                up.ExecuteNonQuery()
                MsgBox("Status from " & downres & " is changed to be rejected")
                clea()
                loadinfo()
            End If
            loadinfo()
            'clea()
            ' fillit()
        End If
    End Sub

    Private Sub txtposition_EditValueChanged(sender As Object, e As EventArgs) Handles txtposition.EditValueChanged
        For indexing As Integer = 0 To tbl_par.Rows.Count - 1
            If txtposition.Text Is tbl_par.Rows(indexing).Item(0).ToString() Then
                txtexpsal.Text = tbl_par.Rows(indexing).Item(1).ToString()
            End If
        Next
    End Sub
End Class