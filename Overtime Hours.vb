Imports System.IO
Public Class Overtime_Hours
    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()
    Dim tbl_par As New DataTable

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call.
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

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Dim sel As New selectemp

    Sub overtimelist()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select ID, EmployeeCode, FullName, Tanggal, Shift, OvertimeHours, OvertimeType, Remarks from db_absensi where overtimehours != '' and tanggal = @date1"
        query.Parameters.AddWithValue("@date1", DateTimePicker3.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
    End Sub

    Private Sub Overtime_Hours_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        autofill()
    End Sub

    Sub autofill()
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter("select EmployeeCode from db_pegawai", SQLConnection)
        da.Fill(dt)
        Dim r As DataRow
        textedit1.AutoCompleteCustomSource.Clear()
        For Each r In dt.Rows
            textedit1.AutoCompleteCustomSource.Add(r.Item(0).ToString)
        Next
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label2.Text
            .Show()
        End With
        'If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
        '    sel.Close()
        '    sel = New selectemp
        'End If
        'sel.Show()
    End Sub

    Sub insertion()
        Dim dtb As Date
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        dtb = DateTimePicker1.Value
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_absensi set overtimehours = @oth, remarks = @remarks where tanggal = @tgl and employeecode = @emp"
        query.Parameters.AddWithValue("@emp", TextEdit1.Text)
        query.Parameters.AddWithValue("@oth", TextEdit3.Text)
        query.Parameters.AddWithValue("@tgl", dtb.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@Remarks", RichTextBox1.Text)
        query.ExecuteNonQuery()
        MsgBox("Overtime Hours Updated")
        TextEdit1.Text = ""
        TextEdit3.Text = ""
        RichTextBox1.Text = ""
        TextEdit2.Text = ""
        DateTimePicker1.Value = Date.Now
    End Sub

    Sub updateottype()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_absensi set overtimetype = 'HolidayType' where isholiday = '1'"
        query.ExecuteNonQuery()
        query.CommandText = "update db_absensi set overtimetype = 'RegularType' where isholiday = '0'"
        query.ExecuteNonQuery()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select count(*) from db_absensi where tanggal = @tgl and employeecode = @emp"
        query.Parameters.AddWithValue("@tgl", DateTimePicker1.Value.Date)
        query.Parameters.AddWithValue("@emp", textedit1.Text)
        Dim quer As Integer = CInt(query.ExecuteScalar)
        If textedit1.Text = "" OrElse textedit2.Text = "" OrElse TextEdit3.Text = "" Then
            MsgBox("Please fill the empty fields", MsgBoxStyle.Exclamation)
        ElseIf CInt(TextEdit3.Text) > 15 Or CInt(TextEdit3.Text) < 1 Then
            MsgBox("Inputted hours isn't valid", MsgBoxStyle.Exclamation)
        ElseIf quer = 0 Then
            Dim mess2 As String
            mess2 = CType(MsgBox("The date isn't proceeded yet you want to input manually ?", MsgBoxStyle.YesNo, "Warning"), String)
            If CType(mess2, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                AttendanceInput.Close()
                With AttendanceInput
                    .Show()
                    .TextEdit1.Text = textedit1.Text
                    .TextEdit2.Text = textedit2.Text
                    .DateTimePicker1.Value = DateTimePicker1.Value
                    .TimeEdit2.Time = CDate(CType(DateTimePicker1.Value.Date, String))
                    .TimeEdit1.Time = CDate(CType(DateTimePicker1.Value.Date, String))
                End With
            End If
        Else
            insertion()
            updateottype()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit2.Text = quer1.ToString

            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            TextEdit1.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Sub updation()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_absensi set" +
                            " overtimehours = @ot" +
                            ",remarks = @remarks" +
                            " where employeecode = @ec and tanggal = @tgl"
        query.Parameters.AddWithValue("@ot", TextEdit4.Text)
        query.Parameters.AddWithValue("@remarks", RichTextBox2.Text)
        query.Parameters.AddWithValue("@tgl", DateTimePicker2.Value.Date)
        query.Parameters.AddWithValue("@ec", Label1.Text)
        query.ExecuteNonQuery()
        MsgBox("Modified")
        overtimelist()
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs)
        Timer1.Stop()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        overtimelist()
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        updation()
    End Sub

    Private Sub GridView1_FocusedRowChanged_1(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And ID='" + GridView1.GetFocusedRowCellValue("ID").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT * FROM db_absensi WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatabl.Rows.Count > 0 Then
            DateTimePicker2.Value = CDate(datatabl.Rows(0).Item(3).ToString())
            TextEdit4.Text = datatabl.Rows(0).Item(8).ToString()
            RichTextBox2.Text = datatabl.Rows(0).Item(10).ToString
            Label1.Text = datatabl.Rows(0).Item(1).ToString
        End If
    End Sub

    Private Sub textedit1_TextChanged(sender As Object, e As EventArgs) Handles textedit1.TextChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select fullname from db_pegawai set employeecode = '" & textedit1.Text & "'"
            Dim quer As String = CStr(query.ExecuteScalar)
            textedit2.Text = quer.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit4.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub
End Class