Imports System.IO
Imports DevExpress.Data
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
'Imports word = Microsoft.Office.Interop.Word
Imports DevExpress.XtraEditors

Public Class Warning
    Dim connectionstring As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection

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
            cmd.CommandText = "Select last_num FROM lastmemo4"
            lastn = DirectCast(cmd.ExecuteScalar(), Integer)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "Select MemoNo FROM db_warning ORDER BY MemoNo DESC LIMIT 1"
            lastcode = DirectCast(cmd.ExecuteScalar(), String)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "Select MID(MemoNo, 8, 1) FROM db_warning where MemoNo = '" & lastcode & "'"
            updmon = DirectCast(cmd.ExecuteScalar(), String)
            If CInt(updmon) <> CInt(mnow) Then
                tmp = 1
            Else
                tmp = lastn + 1
            End If
        Catch ex2 As Exception
        End Try
        Dim actualcode As String = "WRG-" & ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
        txtmemo.Text = actualcode.ToString
    End Sub

    Sub wordviewer()
        Dim filepath As String
        Dim buffer As Byte()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select suratsp from db_document"
        buffer = CType(query.ExecuteScalar(), Byte())
        filepath = Path.GetTempFileName()
        File.Move(filepath, Path.ChangeExtension(filepath, ".docx"))
        filepath = Path.ChangeExtension(filepath, ".docx")
        File.WriteAllBytes(filepath, buffer)
    End Sub

    Sub download1()
        Try
            Dim sFilePath As String
            Dim buffer As Byte()
            Using cmd As New MySqlCommand("select letter from db_warning where memono = '" & txtmemo.Text & "'", SQLConnection)
                buffer = CType(cmd.ExecuteScalar(), Byte())
            End Using
            sFilePath = Path.GetTempFileName()
            File.Move(sFilePath, Path.ChangeExtension(sFilePath, ".pdf"))
            sFilePath = Path.ChangeExtension(sFilePath, ".pdf")
            File.WriteAllBytes(sFilePath, buffer)
            pdfviewer.PdfViewer1.LoadDocument(sFilePath)
            pdfviewer.Show()
            '  Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            ' act.BeginInvoke(sFilePath, Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("No document uploaded or document corrupted")
        End Try
    End Sub

    Private Shared Sub OpenPDFFile(ByVal sFilePath As String)
        Using p As New Process
            p.StartInfo = New ProcessStartInfo(CType(sFilePath, String))
            p.Start()
            p.WaitForExit()
            Try
                File.Delete(CType(sFilePath, String))
            Catch ex As DirectoryNotFoundException
                msgbox(ex)
            End Try
        End Using
    End Sub

    Sub save()
        Dim dtb, dtr As DateTime
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        dtr = DateTimePicker2.Value
        dtb = DateTimePicker1.Value
        Dim ltype As String = ""
        If RadioButton1.Checked = True Then
            ltype = "Teguran"
        ElseIf RadioButton2.Checked = True Then
            ltype = "Peringatan"
        End If
        Dim cmd As MySqlCommand = SQLConnection.CreateCommand
        cmd.CommandText = "insert into db_warning" +
                          "(Memono, tgl, FullName, Employeecode, WarningLevel, OffenseType, DescriptionOfInfraction, Plan, Consequences, IsPenalty, PaymentDate, Amount, LetterType)" +
                          "values(@Memono, @tgl, @names, @ec, @warninglevel, @offensetype, @descriptionofinfraction, @plan, @consequences, @ispenalty, @paymentdate, @amount, @LetterType)"
        cmd.Parameters.AddWithValue("@Memono", txtmemo.Text)
        cmd.Parameters.AddWithValue("@tgl", dtb.ToString("yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("@names", TextBox3.Text)
        cmd.Parameters.AddWithValue("@ec", TextBox2.Text)
        cmd.Parameters.AddWithValue("@WarningLevel", ComboBox1.Text)
        cmd.Parameters.AddWithValue("@OffenseType", ComboBox2.Text)
        cmd.Parameters.AddWithValue("@descriptionofinfraction", RichTextBox1.Text)
        cmd.Parameters.AddWithValue("@plan", RichTextBox2.Text)
        cmd.Parameters.AddWithValue("@consequences", RichTextBox3.Text)
        cmd.Parameters.AddWithValue("@ispenalty", CheckEdit1.Checked)
        cmd.Parameters.AddWithValue("@paymentdate", dtr.ToString("yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("@amount", Encryptio(textbox4.Text.Trim))
        cmd.Parameters.AddWithValue("@LetterType", ltype)
        cmd.ExecuteNonQuery()
    End Sub

    Sub autofill()
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter("select EmployeeCode from db_pegawai", SQLConnection)
        da.Fill(dt)
        Dim r As DataRow
        TextBox2.AutoCompleteCustomSource.Clear()
        For Each r In dt.Rows
            TextBox2.AutoCompleteCustomSource.Add(r.Item(0).ToString)
        Next
    End Sub

    Dim tbl_par7 As New DataTable

    Sub loaddept()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select typeofoffense from db_offense where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par7)
        For index As Integer = 0 To tbl_par7.Rows.Count - 1
            ComboBox2.Properties.Items.Add(tbl_par7.Rows(index).Item(0).ToString())
        Next
    End Sub

    Sub currencyformay()
        textbox4.Properties.DisplayFormat.FormatType = Mask.MaskType.Numeric
        textbox4.Properties.DisplayFormat.FormatString = "n"
        textbox4.Properties.Mask.EditMask = "n"
        textbox4.Properties.Mask.MaskType = Mask.MaskType.Numeric
        textbox4.Properties.Mask.UseMaskAsDisplayFormat = True
    End Sub

    Private Sub Warning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionstring
        SQLConnection.Open()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select employeecode from db_tmpname where 1 = 1"
        Dim quer As String = CType(query.ExecuteScalar, String)
        TextBox2.Text = quer.ToString
        query.CommandText = "select Name from db_tmpname where 1 = 1"
        Dim quer1 As String = CType(query.ExecuteScalar, String)
        TextBox3.Text = quer1.ToString
        changer()
        autofill()
        loaddept()
        currencyformay()
    End Sub

    Private Sub CheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit1.CheckedChanged
        If CheckEdit1.Checked = True Then
            DateTimePicker2.Enabled = True
            textbox4.Enabled = True
        Else
            DateTimePicker2.Enabled = False
            textbox4.Enabled = False
        End If
    End Sub

    Dim sel As New selectemp

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label8.Text
            .Show()
        End With
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextBox3.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            TextBox2.Text = quer2.ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Timer1.Stop()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select fullname from db_pegawai where employeecode = '" & TextBox2.Text & "'"
            TextBox3.Text = CStr(query.ExecuteScalar)
        Catch ex As Exception
        End Try
    End Sub

    Sub saveteguran()
        Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
        Dim finfo As New FileInfo(LabelControl1.Text)
        Dim numBytes As Long = finfo.Length
        Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fstream)
        Dim data As Byte() = br.ReadBytes(CInt(numBytes))
        br.Close()
        fstream.Close()
        sqlquery.CommandText = "update db_warning set letter = @cv where memono = @memono"
        sqlquery.Parameters.AddWithValue("@cv", data)
        sqlquery.Parameters.AddWithValue("@memono", txtmemo.Text)
        sqlquery.ExecuteNonQuery()
        Dim mess2 As String = CType(MsgBox("Proccessed..." & vbCrLf & "Do you want to view the letter ?", MsgBoxStyle.YesNoCancel, "Information"), String)
        If CType(mess2, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            download1()
        Else
            Close()
        End If
    End Sub

    Sub savesp()
        Dim sqlquery As MySqlCommand = SQLConnection.CreateCommand
        Dim finfo As New FileInfo(LabelControl1.Text)
        Dim numBytes As Long = finfo.Length
        Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fstream)
        Dim data As Byte() = br.ReadBytes(CInt(numBytes))
        br.Close()
        fstream.Close()
        sqlquery.CommandText = "update db_warning set letter = @cv where memono = @memono"
        sqlquery.Parameters.AddWithValue("@cv", data)
        sqlquery.Parameters.AddWithValue("@memono", txtmemo.Text)
        sqlquery.ExecuteNonQuery()
        Dim mess2 As String = CType(MsgBox("Proccessed, do you want to view the letter ?", MsgBoxStyle.YesNoCancel, "Warning"), String)
        If CType(mess2, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            download1()
        Else
            Close()
        End If
        'MsgBox("Saved")
    End Sub

    'Sub download()
    '    Dim sFilePath As String
    '    Dim buffer As Byte()
    '    Using cmd As New MySqlCommand("select jobdesk from db_pegawai where employeecode = '17-4-00003'", SQLConnection)
    '        buffer = CType(cmd.ExecuteScalar(), Byte())
    '    End Using
    '    sFilePath = Path.GetTempFileName()
    '    File.Move(sFilePath, Path.ChangeExtension(sFilePath, ".docx"))
    '    sFilePath = Path.ChangeExtension(sFilePath, ".docx")
    '    File.WriteAllBytes(sFilePath, buffer)
    '    Dim bs As MySqlCommand = SQLConnection.CreateCommand
    '    bs.CommandText = "select EmployeeCode from db_pegawai where employeecode = '" & TextBox2.Text & "'"
    '    Dim employeecode As String = CStr(bs.ExecuteScalar)
    '    bs.CommandText = "select fullname from db_pegawai where employeecode = '" & TextBox2.Text & "'"
    '    Dim pos As String = CStr(bs.ExecuteScalar)
    '    Dim objword As word.Application = Nothing
    '    objword = New word.Application
    '    objword.Documents.Open(sFilePath)
    '    Dim findobject As word.Find = objword.Selection.Find
    '    With findobject
    '        .ClearFormatting()
    '        .Text = "<Name>"
    '        .Replacement.ClearFormatting()
    '        .Replacement.Text = pos.ToString
    '        .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '    End With
    '    With findobject
    '        .ClearFormatting()
    '        .Text = "<EmployeeCode>"
    '        .Replacement.ClearFormatting()
    '        .Replacement.Text = employeecode.ToString
    '        .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '    End With
    'End Sub

    'Sub sp()
    '    Dim query As MySqlCommand = SQLConnection.CreateCommand
    '    query.CommandText = "select position from db_pegawai where employeecode = '" & TextBox2.Text & "'"
    '    Dim jobtitle As String = CStr(query.ExecuteScalar)
    '    query.CommandText = "select department from db_pegawai where employeecode = '" & TextBox2.Text & "'"
    '    Dim dept As String = CStr(query.ExecuteScalar)
    '    Dim ynow As String = Format(Now, "yyyy-MMMM-dd").ToString
    '    Dim level As String
    '    If ComboBox1.Text = "Level 1" Then
    '        level = "1"
    '    ElseIf ComboBox1.Text = "Level 2" Then
    '        level = "2"
    '    Else
    '        level = "3"
    '    End If
    '    Dim objword As word.Application = Nothing
    '    Try
    '        objword = New word.Application
    '        objword.Documents.Open("E:\Backup\sp.docx")
    '        Dim findobject As word.Find = objword.Selection.Find
    '        With findobject
    '            .ClearFormatting()
    '            .Text = "<EmployeeName>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = TextBox3.Text
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '            .Text = "<number>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = level
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '            .Text = "<JobTitle>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = jobtitle
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '            .Text = "<Department>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = dept
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '            .Text = "<TypeOfInfraction>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = RichTextBox1.Text
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '            .Text = "<TypeOfInfraction>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = RichTextBox1.Text
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '            .Text = "<Dates>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = ynow
    '            .Execute(Replace:=word.WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '            .Text = "<unknown>"
    '            .Replacement.ClearFormatting()
    '            .Replacement.Text = "unknwn"
    '            .Execute(Replace:=WdReplace.wdReplaceAll)
    '            .ClearFormatting()
    '        End With
    '        'objword.Documents.Save("E:\Backup\sp.docx")
    '        'objword.Documents.Save("E:\Backup\sp.pdf")
    '        '            objword.Documents.Open("E:\Backup\sp.docx")
    '        'objword.Activate()
    '        'objword.SaveAs2("E:\Backup\sp.pdf", WdSaveFormat.wdFormatPDF)
    '        'objword.Close()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Sub copy()
        Dim FileToMove As String
        Dim MoveLocation As String
        Dim FileToDel As String
        Try
            Dim Xcel() As Process = Process.GetProcessesByName("WINWORD.EXE")
            For Each Process As Process In Xcel
                Process.Kill()
            Next
            FileToDel = "E:\Backup\sp.docx"
            FileToMove = "" & System.Environment.CurrentDirectory & "\sp.docx"
            MoveLocation = "E:\Backup\sp.docx"
            File.Delete(FileToDel)
            File.Copy(FileToMove, MoveLocation)
            'MsgBox("Copied")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub sp()
        Dim sp1 As Boolean = False
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select Position from db_pegawai where employeecode = '" & TextBox2.Text & "'"
        Dim jobtitle As String = CStr(query.ExecuteScalar)
        query.CommandText = "select department from db_pegawai where employeecode = '" & TextBox2.Text & "'"
        Dim dept As String = CStr(query.ExecuteScalar)
        query.CommandText = "select companycode from db_pegawai where employeecode = '" & TextBox2.Text & "'"
        Dim kompeni As String = CStr(query.ExecuteScalar)
        Dim ynow As String = Format(Now, "yyyy-MMMM-dd").ToString
        Dim level As String
        If ComboBox1.Text = "Level 1" Then
            level = "1"
        ElseIf ComboBox1.Text = "Level 2" Then
            level = "2"
        Else
            level = "3"
        End If
        Try
            'document extraction part
            Dim filepath As String
            Dim buffer As Byte()
            query.CommandText = "select suratsp from db_document"
            buffer = CType(query.ExecuteScalar(), Byte())
            filepath = Path.GetTempFileName()
            File.Move(filepath, Path.ChangeExtension(filepath, ".docx"))
            filepath = Path.ChangeExtension(filepath, ".docx")
            File.WriteAllBytes(filepath, buffer)
            'end
            RichEditControl1.LoadDocument(filepath, DocumentFormat.OpenXml)
            RichEditControl1.LayoutUnit = DocumentLayoutUnit.Document
            RichEditControl1.Document.BeginUpdate()
            Dim searchResult As ISearchResult = RichEditControl1.Document.StartSearch("<EmployeeName>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult.FindNext()
                searchResult.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult.CurrentResult.Start, TextBox3.Text)
            Loop
            Dim searchResult1 As ISearchResult = RichEditControl1.Document.StartSearch("<JobTitle>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult1.FindNext()
                searchResult1.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult1.CurrentResult.Start, jobtitle)
            Loop
            Dim searchResult2 As ISearchResult = RichEditControl1.Document.StartSearch("<Department>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult2.FindNext()
                searchResult2.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult2.CurrentResult.Start, dept)
            Loop
            Dim searchResult3 As ISearchResult = RichEditControl1.Document.StartSearch("<TypeOfInfraction>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult3.FindNext()
                searchResult3.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult3.CurrentResult.Start, RichTextBox1.Text)
            Loop
            Dim searchResult4 As ISearchResult = RichEditControl1.Document.StartSearch("<Dates>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult4.FindNext()
                searchResult4.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult4.CurrentResult.Start, ynow)
            Loop
            Dim searchResult5 As ISearchResult = RichEditControl1.Document.StartSearch("<number>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult5.FindNext()
                searchResult5.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult5.CurrentResult.Start, level)
            Loop
            Dim searchResult6 As ISearchResult = RichEditControl1.Document.StartSearch("<Kompeni>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult6.FindNext()
                searchResult6.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchResult6.CurrentResult.Start, kompeni)
            Loop
            RichEditControl1.Document.EndUpdate()
            Using ps As New PrintingSystem
                Dim pc1 As New PrintableComponentLink(ps)
                pc1.Component = RichEditControl1
                pc1.CreateDocument()
                pc1.PrintingSystem.ExportOptions.Pdf.NeverEmbeddedFonts = "IDAutomationHC39M"
                pc1.PrintingSystem.ExportToPdf("sp.pdf")
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub teguran()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select Position from db_pegawai where employeecode = '" & TextBox2.Text & "'"
        Dim jobtitle As String = CStr(query.ExecuteScalar)
        query.CommandText = "select department from db_pegawai where employeecode = '" & TextBox2.Text & "'"
        Dim dept As String = CStr(query.ExecuteScalar)
        query.CommandText = "select companycode from db_pegawai where employeecode = '" & TextBox2.Text & "'"
        Dim kompeni As String = CStr(query.ExecuteScalar)
        Dim ynow As String = Format(Now, "yyyy-MMMM-dd").ToString
        Try
            'document extraction part
            Dim filepath As String
            Dim buffer As Byte()
            query.CommandText = "select suratteguran from db_document"
            buffer = CType(query.ExecuteScalar(), Byte())
            filepath = Path.GetTempFileName()
            File.Move(filepath, Path.ChangeExtension(filepath, ".docx"))
            filepath = Path.ChangeExtension(filepath, ".docx")
            File.WriteAllBytes(filepath, buffer)
            'end extraction       
            RichEditControl1.LoadDocument(filepath, DocumentFormat.OpenXml)
            RichEditControl1.LayoutUnit = DocumentLayoutUnit.Document
            RichEditControl1.Document.BeginUpdate()
            Dim searchResult As ISearchResult = RichEditControl1.Document.StartSearch("<EmployeeName>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult.FindNext()
                searchResult.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult.CurrentResult.Start, TextBox3.Text)
            Loop
            Dim searchResult1 As ISearchResult = RichEditControl1.Document.StartSearch("<JobTitle>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult1.FindNext()
                searchResult1.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult1.CurrentResult.Start, jobtitle)
            Loop
            Dim searchResult2 As ISearchResult = RichEditControl1.Document.StartSearch("<Department>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult2.FindNext()
                searchResult2.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult2.CurrentResult.Start, dept)
            Loop
            Dim searchResult3 As ISearchResult = RichEditControl1.Document.StartSearch("<TypeOfInfraction>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult3.FindNext()
                searchResult3.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult3.CurrentResult.Start, RichTextBox1.Text)
            Loop
            Dim searchResult4 As ISearchResult = RichEditControl1.Document.StartSearch("<Dates>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchResult4.FindNext()
                searchResult4.Replace(String.Empty)
                Dim insertRange As DocumentRange = RichEditControl1.Document.InsertText(searchResult4.CurrentResult.Start, ynow)
            Loop
            Dim searchresult5 As ISearchResult = RichEditControl1.Document.StartSearch("<Kompeni>", SearchOptions.CaseSensitive, SearchDirection.Forward, RichEditControl1.Document.Range)
            Do While searchresult5.FindNext
                searchresult5.Replace(String.Empty)
                Dim insertrange As DocumentRange = RichEditControl1.Document.InsertText(searchresult5.CurrentResult.Start, kompeni)
            Loop
            RichEditControl1.Document.EndUpdate()
            Using ps As New PrintingSystem
                Dim pc1 As New PrintableComponentLink(ps)
                pc1.Component = RichEditControl1
                pc1.CreateDocument()
                pc1.PrintingSystem.ExportOptions.Pdf.NeverEmbeddedFonts = "IDAutomationHC39M"
                pc1.PrintingSystem.ExportToPdf("teguran.pdf")
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        teguran()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        'RichEdit.Show()
        'sp()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If TextBox2.Text = "" OrElse TextBox3.Text = "" OrElse ComboBox1.Text = "" Then
            MsgBox("Please fill the empty fields or select the radio button type")
        Else
            If RadioButton1.Checked = True Then
                teguran()
                save()
                saveteguran()
                TextBox3.Text = ""
                TextBox2.Text = ""
                txtmemo.Text = ""
                ComboBox1.Text = ""
                ComboBox2.Text = ""
                RichTextBox1.Text = ""
                CheckEdit1.Checked = False
                RichTextBox2.Text = ""
                textbox4.Text = ""
                RichTextBox3.Text = ""
                changer()
                Close()
            ElseIf RadioButton2.Checked = True Then
                sp()
                save()
                savesp()
                    TextBox3.Text = ""
                    TextBox2.Text = ""
                    txtmemo.Text = ""
                    ComboBox1.Text = ""
                    ComboBox2.Text = ""
                    RichTextBox1.Text = ""
                    CheckEdit1.Checked = False
                    RichTextBox2.Text = ""
                    textbox4.Text = ""
                    RichTextBox3.Text = ""
                    changer()
                    Close()
                Else
                    Exit Sub
                End If
            End If
        If RadioButton1.Checked = True Then
            SimpleButton4.Enabled = True

        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Warning_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Shell("Taskkill /IM WINWORD.exe /F")
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            LabelControl1.Text = "teguran.pdf"
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            LabelControl1.Text = "sp.pdf"
        End If
    End Sub
End Class