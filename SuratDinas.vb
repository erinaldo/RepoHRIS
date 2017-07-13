Imports System.IO
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid

Public Class SuratDinas
    Dim connectionstring As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim act As String = "input"

    Public Sub New()
        InitializeComponent()
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
        connectionstring = "Server=" + host + "; User Id=" + id + "; Password=" + password + "; Database=" + db + ""
    End Sub

    Private Sub SuratDinas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionstring
        SQLConnection.Open()
        'changer()
        combo()
        Timer2.Start()
    End Sub

    Sub viewtravel()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select MemoNo, TravelDates, TimeTravel, Descript as Description, MobilDinas, Bus, KeretaApi, Pesawat, Hotel, Mess, Others from db_detailsppd where memono = '" & ComboBoxEdit1.Text & "'"
        Dim tab As New DataTable
        GridControl3.DataSource = tab
    End Sub

    Sub viewcosts()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select Expenses, Costs, Description from db_detailsppd where memono '" & ComboBoxEdit1.Text & "'"
        Dim tab As New DataTable
        GridControl4.DataSource = tab
    End Sub

    Sub counttotal()
        Try
            Dim a, b, c, e, f, g, h As Integer
            a = CInt(Convert.ToDouble(TextEdit12.Text))
            b = CInt(Convert.ToDouble(TextEdit13.Text))
            c = CInt(Convert.ToDouble(TextEdit16.Text))
            e = CInt(Convert.ToDouble(TextEdit19.Text))
            f = CInt(Convert.ToDouble(TextEdit20.Text))
            g = CInt(Convert.ToDouble(TextEdit21.Text))
            h = a + b + c + e + f + g
            TextEdit22.Text = h.ToString
        Catch ex As Exception
        End Try
    End Sub

    Dim dt1 As Date
    Dim dt2 As Date
    Dim dt3 As TimeSpan
    Dim diff As Double
    Dim hasil As String

    Sub days()
        Dim t As TimeSpan = DateTimePicker2.Value.Date - DateTimePicker1.Value.Date
        Dim tmp As String
        tmp = t.Days.ToString
        Dim hasil As Integer
        hasil = CInt(tmp)
        Dim hasil2 As Integer = hasil + 1
        TextEdit5.Text = hasil2.ToString
    End Sub

    Sub changer()
        Dim maxid As Integer
        Dim newid As Integer = maxid + 1
        Dim ynow As String = Format(Now, "yy").ToString
        Dim mnow As String = Month(Now).ToString
        Dim lastn As Integer
        Dim updmon As String
        Dim lastcode As String = ""
        Dim tmp As Integer
        Dim cd As MySqlCommand = SQLConnection.CreateCommand
        Try
            Dim cmd = SQLConnection.CreateCommand()
            cmd.CommandText = "SELECT memo_sppd FROM memo_sppd"
            lastn = DirectCast(cmd.ExecuteScalar(), Integer)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT MemoNo FROM db_sppd ORDER BY MemoNo DESC LIMIT 1"
            lastcode = DirectCast(cmd.ExecuteScalar(), String)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT MID(MemoNo, 8, 1) FROM db_sppd where MemoNo = '" & lastcode & "'"
            updmon = DirectCast(cmd.ExecuteScalar(), String)
            If CInt(updmon) <> CInt(mnow) Then
                tmp = 1
            Else
                tmp = lastn + 1
            End If
        Catch ex2 As Exception
        End Try
        Dim actualcode As String = "Memo" & ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
        TextEdit1.Text = actualcode.ToString
    End Sub

    Sub showgrid()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select TravelDates, TimeTravel as Jam, Descript, MobilDinas, Bus, KeretaApi, Pesawat, Hotel, Mess, Others from db_detailsppd where memono = '" & TextEdit1.Text & "' and descript = ''"
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        GridControl1.DataSource = tab
        GridView1.BestFitColumns()
        Dim gridView As GridView = CType(GridControl1.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
            New GridColumnSortInfo(gridView.Columns("TravelDates"), DevExpress.Data.ColumnSortOrder.Ascending)}, 0)
    End Sub

    Sub showgrid2()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select TravelDates, TimeTravel as Jam, Descript, MobilDinas, Bus, KeretaApi, Pesawat, Hotel, Mess, Others from db_detailsppd where memono = '" & ComboBoxEdit2.Text & "' and expenses = ''"
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        GridControl1.DataSource = tab
        GridView1.BestFitColumns()
        Dim gridView As GridView = CType(GridControl1.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
            New GridColumnSortInfo(gridView.Columns("TravelDates"), DevExpress.Data.ColumnSortOrder.Ascending)}, 0)
    End Sub

    Sub insertdetail()
        DateTimePicker4.Format = DateTimePickerFormat.Custom
        DateTimePicker4.CustomFormat = "yyyy-MM-dd"
        Dim dtd As DateTime = DateTimePicker4.Value
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "insert into db_detailsppd (MemoNo, Traveldates, Timetravel, Descript, MobilDinas, Bus, KeretaApi, Pesawat, Hotel, Mess, others) values (@memono, @traveldates, @timetravel, @descript, @mobildinas, @bus, @keretaapi, @pesawat, @hotel, @mess, @others)"
        query.Parameters.AddWithValue("@memono", ComboBoxEdit2.Text)
        query.Parameters.AddWithValue("@TravelDates", dtd.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@TimeTravel", TimeEdit1.Text)
        query.Parameters.AddWithValue("@Descript", RichTextBoxEx1.Text)
        query.Parameters.AddWithValue("@MobilDinas", CheckEdit1.Checked)
        query.Parameters.AddWithValue("@Bus", CheckEdit2.Checked)
        query.Parameters.AddWithValue("@KeretaApi", CheckEdit3.Checked)
        query.Parameters.AddWithValue("@Pesawat", CheckEdit4.Checked)
        query.Parameters.AddWithValue("@Hotel", CheckEdit5.Checked)
        query.Parameters.AddWithValue("@Mess", CheckEdit6.Checked)
        query.Parameters.AddWithValue("@Others", CheckEdit7.Checked)
        query.ExecuteNonQuery()
        RichTextBoxEx1.Text = ""
        TimeEdit1.Text = ""
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
    End Sub

    Sub edit()
        Dim dta, dtb, dtc, dtd As New DateTime
        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker3.CustomFormat = "yyyy-MM-dd"
        dta = DateTimePicker3.Value
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        dtb = DateTimePicker1.Value
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtc = DateTimePicker2.Value
        DateTimePicker4.Format = DateTimePickerFormat.Custom
        DateTimePicker4.CustomFormat = "yyyy-MM-dd"
        dtd = DateTimePicker4.Value
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_sppd set IssuesDates = @idates, Fromdates = @fdates, Todates = @tdates, destinationcity = @dcity, agenda = @agenda where memono = '" & ComboBoxEdit2.Text & "'"
        query.Parameters.AddWithValue("@idates", dta.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@fdates", dtb.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@tdates", dtc.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@dcity", TextEdit6.Text)
        query.Parameters.AddWithValue("@agenda", TextEdit7.Text)
        query.ExecuteNonQuery()
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Sub insertion()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        Dim dta, dtb, dtc, dtd As New DateTime
        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker3.CustomFormat = "yyyy-MM-dd"
        dta = DateTimePicker3.Value
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        dtb = DateTimePicker1.Value
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtc = DateTimePicker2.Value
        DateTimePicker4.Format = DateTimePickerFormat.Custom
        DateTimePicker4.CustomFormat = "yyyy-MM-dd"
        dtd = DateTimePicker4.Value
        query.CommandText = "insert into db_sppd (MemoNo, EmployeeCode, FullName, IssuesDates, Department, FromDates, ToDates, DestinationCity, Agenda, TravelDates, JamWaktu, Descript, MobilDinas, Bus, KeretaApi, PesawatTerbang, Hotel, Mess, Others, PremiumLiter, SolarLiter, UangSaku, brpmalam, biayamalam, biayatransportasi, biayalainlain, approvedby)" +
                                        "Values (@MemoNo, @EmployeeCode, @FullName, @IssuesDates, @Department, @FromDates, @ToDates, @DestinationCity, @Agenda, @TravelDates, @JamWaktu, @Descript, @MobilDinas, @Bus, @KeretaApi, @PesawatTerbang, @Hotel, @Mess, @Others, @PremiumLiter, @SolarLiter, @UangSaku, @brpmalam, @biayamalam, @biayatransportasi, @biayalainlain, @approvedby)"
        query.Parameters.AddWithValue("@MemoNo", TextEdit1.Text)
        query.Parameters.AddWithValue("@EmployeeCode", TextEdit2.Text)
        query.Parameters.AddWithValue("@FullName", TextEdit3.Text)
        query.Parameters.AddWithValue("@IssuesDates", dta.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@Department", TextEdit4.Text)
        query.Parameters.AddWithValue("@FromDates", dtb.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@ToDates", dtc.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@DestinationCity", TextEdit6.Text)
        query.Parameters.AddWithValue("@Agenda", TextEdit7.Text)
        query.Parameters.AddWithValue("@TravelDates", dtd.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@JamWaktu", TimeEdit1.Text)
        query.Parameters.AddWithValue("@Descript", RichTextBoxEx1.Text)
        query.Parameters.AddWithValue("@MobilDinas", CheckEdit1.Checked)
        query.Parameters.AddWithValue("@Bus", CheckEdit2.Checked)
        query.Parameters.AddWithValue("@KeretaApi", CheckEdit3.Checked)
        query.Parameters.AddWithValue("@PesawatTerbang", CheckEdit4.Checked)
        query.Parameters.AddWithValue("@Hotel", CheckEdit5.Checked)
        query.Parameters.AddWithValue("@Mess", CheckEdit6.Checked)
        query.Parameters.AddWithValue("@Others", CheckEdit7.Checked)
        query.Parameters.AddWithValue("@premiumliter", TextEdit8.Text)
        query.Parameters.AddWithValue("@solarliter", TextEdit9.Text)
        query.Parameters.AddWithValue("@uangsaku", TextEdit15.Text)
        query.Parameters.AddWithValue("@brpmalam", TextEdit17.Text)
        query.Parameters.AddWithValue("@biayamalam", TextEdit18.Text)
        query.Parameters.AddWithValue("@biayatransportasi", TextEdit20.Text)
        query.Parameters.AddWithValue("@biayalainlain", TextEdit21.Text)
        query.Parameters.AddWithValue("@approvedby", "")
        query.ExecuteNonQuery()
        MsgBox("Requested!", MsgBoxStyle.Information)
    End Sub

    Sub sppd()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select Position from db_pegawai where employeecode = '" & TextEdit3.Text & "'"
        Dim jobtitle As String = CStr(query.ExecuteScalar)
        query.CommandText = "select department from db_pegawai where employeecode = '" & TextEdit3.Text & "'"
        Dim dept As String = CStr(query.ExecuteScalar)
        query.CommandText = "select companycode from db_pegawai where employeecode = '" & TextEdit3.Text & "'"
        Dim kompeni As String = CStr(query.ExecuteScalar)
        Dim ynow As String = Format(Now, "yyyy-MMMM-dd").ToString
        Dim a, b, hsl As Double
        a = Convert.ToDouble(TextEdit12.Text)
        b = Convert.ToDouble(TextEdit13.Text)
        hsl = a + b
        query.CommandText = "select directhead from db_pegawai where employeecode = '" & TextEdit3.Text & "'"
        Dim directhead As String = CStr(query.ExecuteScalar)
        query.CommandText = "select undirecthead from db_pegawai where employeecode = '" & TextEdit3.Text & "'"
        Dim undirecthead As String = CStr(query.ExecuteScalar)
        Try
            RichEditControl1.LoadDocument("SPPD.docx", DocumentFormat.OpenXml)
            RichEditControl1.LayoutUnit = DocumentLayoutUnit.Document
            RichEditControl1.Document.BeginUpdate()
            Dim searchResult As ISearchResult = RichEditControl1.Document.StartSearch("<employeename>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult.FindNext()
                searchResult.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult.CurrentResult.Start, TextEdit2.Text)
            Loop
            Dim searchResult1 As ISearchResult = RichEditControl1.Document.StartSearch("<employeedept>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult1.FindNext()
                searchResult1.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult1.CurrentResult.Start, dept)
            Loop
            Dim searchResult2 As ISearchResult = RichEditControl1.Document.StartSearch("<traveldates>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult2.FindNext()
                searchResult2.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult2.CurrentResult.Start, CStr(DateTimePicker1.Value.Date))
            Loop
            Dim searchResult3 As ISearchResult = RichEditControl1.Document.StartSearch("<traveldates2>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult3.FindNext()
                searchResult3.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult3.CurrentResult.Start, CStr(DateTimePicker2.Value.Date))
            Loop
            Dim searchResult4 As ISearchResult = RichEditControl1.Document.StartSearch("<tdhr>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult4.FindNext()
                searchResult4.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult4.CurrentResult.Start, TextEdit5.Text)
            Loop
            Dim searchResult5 As ISearchResult = RichEditControl1.Document.StartSearch("<tgl>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult5.FindNext()
                searchResult5.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult5.CurrentResult.Start, CStr(DateTimePicker3.Value.Date))
            Loop
            Dim searchResult6 As ISearchResult = RichEditControl1.Document.StartSearch("<pre>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult6.FindNext()
                searchResult6.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult6.CurrentResult.Start, CStr(CheckEdit8.Checked))
            Loop
            Dim searchResult7 As ISearchResult = RichEditControl1.Document.StartSearch("<so>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult7.FindNext()
                searchResult7.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult7.CurrentResult.Start, CStr(CheckEdit9.Checked))
            Loop
            Dim searchResult8 As ISearchResult = RichEditControl1.Document.StartSearch("<oil>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult8.FindNext()
                searchResult8.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult8.CurrentResult.Start, hsl.ToString)
            Loop
            Dim searchResult9 As ISearchResult = RichEditControl1.Document.StartSearch("<rpsaku>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult9.FindNext()
                searchResult9.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult9.CurrentResult.Start, TextEdit15.Text)
            Loop
            Dim searchResult10 As ISearchResult = RichEditControl1.Document.StartSearch("<rpinap>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult10.FindNext()
                searchResult10.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult10.CurrentResult.Start, TextEdit18.Text)
            Loop
            Dim searchResult11 As ISearchResult = RichEditControl1.Document.StartSearch("<usaku>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult11.FindNext()
                searchResult11.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult11.CurrentResult.Start, TextEdit16.Text)
            Loop
            Dim searchResult12 As ISearchResult = RichEditControl1.Document.StartSearch("<uinap>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult12.FindNext()
                searchResult12.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult12.CurrentResult.Start, TextEdit19.Text)
            Loop
            Dim searchResult13 As ISearchResult = RichEditControl1.Document.StartSearch("<temp>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult13.FindNext()
                searchResult13.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult13.CurrentResult.Start, TextEdit22.Text)
            Loop
            Dim searchResult14 As ISearchResult = RichEditControl1.Document.StartSearch("<destinationcity>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult14.FindNext()
                searchResult14.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult14.CurrentResult.Start, TextEdit6.Text)
            Loop
            Dim searchResult15 As ISearchResult = RichEditControl1.Document.StartSearch("<agenda>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult15.FindNext()
                searchResult15.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult15.CurrentResult.Start, TextEdit7.Text)
            Loop
            Dim searchResult16 As ISearchResult = RichEditControl1.Document.StartSearch("<mlm>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult16.FindNext()
                searchResult16.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult16.CurrentResult.Start, TextEdit17.Text)
            Loop
            Dim searchResult17 As ISearchResult = RichEditControl1.Document.StartSearch("<terbilang>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult17.FindNext()
                searchResult17.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult17.CurrentResult.Start, Label4.Text)
            Loop
            Dim searchResult18 As ISearchResult = RichEditControl1.Document.StartSearch("<Atasan Langsung>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult18.FindNext
                searchResult18.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult18.CurrentResult.Start, directhead)
            Loop
            Dim searchResult19 As ISearchResult = RichEditControl1.Document.StartSearch("<Atasan Tidak Langsung>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult19.FindNext
                searchResult19.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult19.CurrentResult.Start, undirecthead)
            Loop
            RichEditControl1.Document.EndUpdate()
            Using ps As New PrintingSystem
                Dim pc1 As New PrintableComponentLink(ps)
                pc1.Component = RichEditControl1
                pc1.CreateDocument()
                pc1.PrintingSystem.ExportOptions.Pdf.NeverEmbeddedFonts = "IDAutomationHC39M"
                pc1.PrintingSystem.ExportToPdf("sppd.pdf")
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'insertion()
        If TextEdit1.Enabled = False And TextEdit2.Text IsNot "" And TextEdit3.Text IsNot "" And TextEdit4.Text IsNot "" And TextEdit6.Text IsNot "" And TextEdit7.Text IsNot "" Then
            newinsert()
            SimpleButton3.Enabled = False
        End If
        'updation()
    End Sub

    Dim emp As New selectemp

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Start()
        If emp Is Nothing OrElse emp.IsDisposed OrElse emp.MinimizeBox Then
            emp.Close()
            emp = New selectemp
        End If
        emp.Show()
    End Sub

    Sub updation()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        DateTimePicker4.Format = DateTimePickerFormat.Custom
        DateTimePicker4.CustomFormat = "yyyy-MM-dd"
        Dim dtd As DateTime = DateTimePicker4.Value
        query.CommandText = "update db_sppd set traveldates = @traveldates, jamwaktu = @jamwaktu, descript = @descript, mobildinas = @mobildinas, bus = @bus, keretaapi = @keretaapi, pesawatterbang = @pesawatterbang, hotel = @hotel, mess = @mess, others = @others, premiumliter = @premiumliter, solarliter = @solarliter, uangsaku = @uangsaku, brpmalam = @brpmalam, biayamalam = @biayamalam, biayatransportasi = @biayatransportasi, biayalainlain = @biayalainlain, approvedby = @approvedby where memono = '" & TextEdit1.Text & "'"
        query.Parameters.AddWithValue("@TravelDates", dtd.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@JamWaktu", TimeEdit1.Text)
        query.Parameters.AddWithValue("@Descript", RichTextBoxEx1.Text)
        query.Parameters.AddWithValue("@MobilDinas", CheckEdit1.Checked)
        query.Parameters.AddWithValue("@Bus", CheckEdit2.Checked)
        query.Parameters.AddWithValue("@KeretaApi", CheckEdit3.Checked)
        query.Parameters.AddWithValue("@PesawatTerbang", CheckEdit4.Checked)
        query.Parameters.AddWithValue("@Hotel", CheckEdit5.Checked)
        query.Parameters.AddWithValue("@Mess", CheckEdit6.Checked)
        query.Parameters.AddWithValue("@Others", CheckEdit7.Checked)
        query.Parameters.AddWithValue("@premiumliter", TextEdit8.Text)
        query.Parameters.AddWithValue("@solarliter", TextEdit9.Text)
        query.Parameters.AddWithValue("@uangsaku", TextEdit15.Text)
        query.Parameters.AddWithValue("@brpmalam", TextEdit17.Text)
        query.Parameters.AddWithValue("@biayamalam", TextEdit18.Text)
        query.Parameters.AddWithValue("@biayatransportasi", TextEdit20.Text)
        query.Parameters.AddWithValue("@biayalainlain", TextEdit21.Text)
        query.Parameters.AddWithValue("@approvedby", "")
        query.ExecuteNonQuery()
        MsgBox("Requested")
    End Sub

    ', TravelDates, JamWaktu, Descript, MobilDinas, Bus, KeretaApi, PesawatTerbang, Hotel, Mess, Others, PremiumLiter, SolarLiter, UangSaku, brpmalam, biayamalam, biayatransportasi, biayalainlain, approvedby
    ', @TravelDates, @JamWaktu, @Descript, @MobilDinas, @Bus, @KeretaApi, @PesawatTerbang, @Hotel, @Mess, @Others, @PremiumLiter, @SolarLiter, @UangSaku, @brpmalam, @biayamalam, @biayatransportasi, @biayalainlain, @approvedby
    Sub newinsert()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        Dim dta, dtb, dtc, dtd As New DateTime
        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker3.CustomFormat = "yyyy-MM-dd"
        dta = DateTimePicker3.Value
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        dtb = DateTimePicker1.Value
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtc = DateTimePicker2.Value
        DateTimePicker4.Format = DateTimePickerFormat.Custom
        DateTimePicker4.CustomFormat = "yyyy-MM-dd"
        dtd = DateTimePicker4.Value
        query.CommandText = "insert into db_sppd (MemoNo, EmployeeCode, FullName, IssuesDates, Department, FromDates, ToDates, DestinationCity, Agenda)" +
                                        "Values (@MemoNo, @EmployeeCode, @FullName, @IssuesDates, @Department, @FromDates, @ToDates, @DestinationCity, @Agenda)"
        query.Parameters.AddWithValue("@MemoNo", TextEdit1.Text)
        query.Parameters.AddWithValue("@EmployeeCode", TextEdit2.Text)
        query.Parameters.AddWithValue("@FullName", TextEdit3.Text)
        query.Parameters.AddWithValue("@IssuesDates", dta.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@Department", TextEdit4.Text)
        query.Parameters.AddWithValue("@FromDates", dtb.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@ToDates", dtc.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@DestinationCity", TextEdit6.Text)
        query.Parameters.AddWithValue("@Agenda", TextEdit7.Text)
        query.ExecuteNonQuery()
        TextEdit6.Enabled = False
        TextEdit7.Enabled = False
        TextEdit4.Enabled = False
        DateTimePicker1.Enabled = False
        DateTimePicker2.Enabled = False
        DateTimePicker3.Enabled = False
        MsgBox("Requested!", MsgBoxStyle.Information)
        fucn()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit3.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            TextEdit2.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Sub fucn()
        DateTimePicker4.Enabled = True
        TimeEdit1.Enabled = True
        RichTextBoxEx1.Enabled = True
        CheckEdit1.Enabled = True
        CheckEdit2.Enabled = True
        CheckEdit3.Enabled = True
        CheckEdit4.Enabled = True
        CheckEdit5.Enabled = True
        CheckEdit6.Enabled = True
        CheckEdit7.Enabled = True
        SimpleButton2.Enabled = True
    End Sub

    Private Sub TextEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit2.EditValueChanged
        Timer1.Stop()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select department from db_pegawai where employeecode = '" & TextEdit2.Text & "'"
        TextEdit4.Text = CStr(query.ExecuteScalar)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        days()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        days()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    End Sub

    Private Sub CheckEdit8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit8.CheckedChanged
        If CheckEdit8.Checked = True Then
            TextEdit8.Enabled = True
            TextEdit10.Enabled = True
        ElseIf CheckEdit8.Checked = False Then
            TextEdit8.Enabled = False
            TextEdit10.Enabled = False
        End If
    End Sub

    Private Sub CheckEdit9_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit9.CheckedChanged
        If CheckEdit9.Checked = True Then
            TextEdit9.Enabled = True
            TextEdit11.Enabled = True
        ElseIf CheckEdit9.Checked = False Then
            TextEdit9.Enabled = False
            TextEdit11.Enabled = False
        End If
    End Sub

    Private Sub TextEdit12_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit12.EditValueChanged
        counttotal()
    End Sub

    Private Sub TextEdit13_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit13.EditValueChanged
        counttotal()
    End Sub

    Private Sub TextEdit16_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit16.EditValueChanged
        counttotal()
    End Sub

    Private Sub TextEdit19_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit19.EditValueChanged
        counttotal()
    End Sub

    Private Sub TextEdit20_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit20.EditValueChanged
        counttotal()
    End Sub

    Private Sub TextEdit21_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit21.EditValueChanged
        counttotal()
    End Sub

    Sub premium()
        Try
            Dim a, b, c As Double
            a = Convert.ToDouble(TextEdit8.Text)
            b = Convert.ToDouble(TextEdit10.Text)
            c = a * b
            TextEdit12.Text = c.ToString
        Catch ex As Exception
        End Try
    End Sub

    Sub solar()
        Try
            Dim a, b, c As Double
            a = Convert.ToDouble(TextEdit9.Text)
            b = Convert.ToDouble(TextEdit11.Text)
            c = a * b
            TextEdit13.Text = c.ToString
        Catch ex As Exception
        End Try
    End Sub

    Sub saku()
        Try
            Dim a, b, c As Double
            a = Convert.ToDouble(TextEdit14.Text)
            b = Convert.ToDouble(TextEdit15.Text)
            c = a * b
            TextEdit16.Text = c.ToString
        Catch ex As Exception
        End Try
    End Sub

    Sub inap()
        Try
            Dim a, b, c As Double
            a = Convert.ToDouble(TextEdit17.Text)
            b = Convert.ToDouble(TextEdit18.Text)
            c = a * b
            TextEdit19.Text = c.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit8_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit8.EditValueChanged
        premium()
    End Sub

    Private Sub TextEdit10_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit10.EditValueChanged
        premium()
    End Sub

    Private Sub TextEdit9_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit9.EditValueChanged
        solar()
    End Sub

    Private Sub TextEdit18_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit18.EditValueChanged
        inap()
    End Sub

    Private Sub TextEdit11_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit11.EditValueChanged
        solar()
    End Sub

    Private Sub TextEdit14_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit14.EditValueChanged
        saku()
    End Sub

    Private Sub TextEdit15_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit15.EditValueChanged
        saku()
    End Sub

    Private Sub TextEdit17_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit17.EditValueChanged
        inap()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        insertdetail()
        'insertdetail()
        'showgrid()
        showgrid2()
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        sppd()
    End Sub

    Private Sub TextEdit22_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit22.EditValueChanged
        If TextEdit22.Text <> "" Then
            Label4.Text = TERBILANG(Val(TextEdit22.Text))
        End If
    End Sub

    Sub editable()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select EmployeeCode, FullName, IssuesDates, Department, FromDates, ToDates, Destinationcity, agenda from db_sppd where memono = '" & TextEdit1.Text & "'"
            Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            Dim tab As New DataTable
            adapter.Fill(tab)
            For index As Integer = 0 To tab.Rows.Count - 1
                TextEdit2.Text = tab.Rows(index).Item(0).ToString
                TextEdit3.Text = tab.Rows(index).Item(1).ToString
                DateTimePicker3.Text = tab.Rows(index).Item(2).ToString
                TextEdit4.Text = tab.Rows(index).Item(3).ToString
                DateTimePicker1.Text = tab.Rows(index).Item(4).ToString
                DateTimePicker2.Text = tab.Rows(index).Item(5).ToString
                TextEdit6.Text = tab.Rows(index).Item(6).ToString
                TextEdit7.Text = tab.Rows(index).Item(7).ToString
            Next
        Catch ex As Exception
        End Try
    End Sub

    Sub editable2()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select EmployeeCode, FullName, IssuesDates, Department, FromDates, ToDates, Destinationcity, agenda from db_sppd where memono = '" & ComboBoxEdit2.Text & "'"
            Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            Dim tab As New DataTable
            adapter.Fill(tab)
            For index As Integer = 0 To tab.Rows.Count - 1
                TextEdit2.Text = tab.Rows(index).Item(0).ToString
                TextEdit3.Text = tab.Rows(index).Item(1).ToString
                DateTimePicker3.Text = tab.Rows(index).Item(2).ToString
                TextEdit4.Text = tab.Rows(index).Item(3).ToString
                DateTimePicker1.Text = tab.Rows(index).Item(4).ToString
                DateTimePicker2.Text = tab.Rows(index).Item(5).ToString
                TextEdit6.Text = tab.Rows(index).Item(6).ToString
                TextEdit7.Text = tab.Rows(index).Item(7).ToString
            Next
        Catch ex As Exception
        End Try
    End Sub

    Sub clearance()
        TextEdit2.Text = ""
        TextEdit3.Text = ""
        TextEdit4.Text = ""
        TextEdit6.Text = ""
        TextEdit7.Text = ""
    End Sub

    Sub query()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_detailsppd where memono = '" & TextEdit3.Text & "'"
        Dim tab As New DataTable
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            act = "input"
            clearance()
            SimpleButton3.Text = "Request"
            TextEdit1.Visible = True
            ComboBoxEdit2.Visible = False
            XtraTabControl2.Enabled = False
        End If
        changer()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            act = "edit"
            SimpleButton3.Text = "Edit"
            ComboBoxEdit2.Visible = True
            TextEdit1.Visible = False
            XtraTabControl2.Enabled = True
        End If
        TextEdit1.Text = ""
    End Sub

    Dim ta As New DataTable

    Sub combo()
        Dim qery As MySqlCommand = SQLConnection.CreateCommand
        qery.CommandText = "select Memono from db_sppd"
        Dim adapter As New MySqlDataAdapter(qery.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(ta)
        For index As Integer = 0 To ta.Rows.Count - 1
            ComboBoxEdit2.Properties.Items.Add(ta.Rows(index).Item(0).ToString())
        Next
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        editable()
    End Sub

    Private Sub ComboBoxEdit2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit2.SelectedIndexChanged
        For index As Integer = 0 To ta.Rows.Count - 1
        Next
        editable2()
        showgrid2()
        showcosts()
    End Sub

    Sub view()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select memono from db_sppd where issuesdates = @date1"
        query.Parameters.AddWithValue("@date1", DateTimePicker9.Value.Date)
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        For index As Integer = 0 To tab.Rows.Count - 1
            ComboBoxEdit1.Properties.Items.Add(tab.Rows(index).Item(0).ToString)
        Next
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        ComboBoxEdit1.Properties.Items.Clear()
        view()
    End Sub

    Private Sub ComboBoxEdit3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit3.SelectedIndexChanged
        If ComboBoxEdit3.Text = "Lain-lain" Then
            TextEdit44.Enabled = True
        Else
            TextEdit44.Enabled = False
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And Descript='" + GridView1.GetFocusedRowCellValue("Descript").ToString() + "'"
        Catch ex As Exception
        End Try
        If param > "" Then
            act = "edit"
        Else
            act = "input"
        End If
        Try
            sqlCommand.CommandText = "SELECT TravelDates, TimeTravel, Descript, MobilDinas, Bus, KeretaApi, Pesawat, Hotel, Mess, Others from db_detailsppd WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatabl.Rows.Count > 0 Then
            DateTimePicker4.Text = CStr(datatabl.Rows(0).Item(0).ToString())
            TimeEdit1.Text = datatabl.Rows(0).Item(1).ToString()
            RichTextBoxEx1.Text = datatabl.Rows(0).Item(2).ToString
            CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(3).ToString)
            CheckEdit2.Checked = CBool(datatabl.Rows(0).Item(4).ToString)
            CheckEdit3.Checked = CBool(datatabl.Rows(0).Item(5).ToString)
            CheckEdit4.Checked = CBool(datatabl.Rows(0).Item(6).ToString)
            CheckEdit5.Checked = CBool(datatabl.Rows(0).Item(7).ToString)
            CheckEdit6.Checked = CBool(datatabl.Rows(0).Item(8).ToString)
        End If
    End Sub

    Sub insertcost()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "insert into db_detailsppd (Memono, Expenses, Costs, Description) values (@MemoNo, @expenses, @costs, @description)"
        If RadioButton1.Checked = True Then
            query.Parameters.AddWithValue("@memono", TextEdit1.Text)
        ElseIf RadioButton2.Checked = True Then
            query.Parameters.AddWithValue("@memono", ComboBoxEdit2.Text)
        End If
        '  query.Parameters.AddWithValue("@memono", TextEdit1.Text)
        If ComboBoxEdit3.Text = "Lain-lain" Then
            query.Parameters.AddWithValue("@expenses", TextEdit44.Text)
        Else
            query.Parameters.AddWithValue("@expenses", ComboBoxEdit3.Text)
        End If
        query.Parameters.AddWithValue("@costs", TextEdit45.Text)
        query.Parameters.AddWithValue("@description", RichTextBoxEx3.Text)
        query.ExecuteNonQuery()
    End Sub

    Sub showcosts()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            If RadioButton1.Checked = True Then
                query.CommandText = "select MemoNo, Expenses, Costs, Description from db_detailsppd where memono ='" & TextEdit1.Text & "' and timetravel = ''"
            ElseIf RadioButton2.Checked = True Then
                query.CommandText = "select MemoNo, Expenses, Costs, Description from db_detailsppd where memono ='" & ComboBoxEdit2.Text & "' and timetravel = ''"
            End If
            Dim tab As New DataTable
            tab.Load(query.ExecuteReader)
            GridControl4.DataSource = tab
            Dim item As GridGroupSummaryItem = New GridGroupSummaryItem()
            item.FieldName = "Costs"
            item.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item.DisplayFormat = "Total n2"
            item.ShowInGroupColumnFooter = GridView4.Columns("Costs")
            GridView4.GroupSummary.Add(item)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        insertcost()
        showcosts()
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        viewtravel()
        viewcosts()
    End Sub
End Class