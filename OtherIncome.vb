Imports System.IO
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Views.Grid

Public Class OtherIncome
    'Sub changer()
    '    Dim mnow As String = Month(Now).ToString
    '    Dim yearx As String = Format(Now, "yy").ToString
    '        Dim updmon As String
    '        Dim tmp As Integer
    '        Dim lastcode As String = ""
    '        Dim query As MySqlCommand = SQLConnection.CreateCommand
    '        query.CommandText = "select last_num from lastmemo"
    '        Dim lastn As Integer = DirectCast(query.ExecuteScalar, Integer)
    '        query.CommandText = "SELECT Memono FROM db_addition ORDER BY MemoNo DESC LIMIT 1"
    '        lastcode = DirectCast(query.ExecuteScalar(), String)
    '            Dim cmd = SQLConnection.CreateCommand
    '            cmd.CommandText = "SELECT MID(MemoNo, 9, 1) FROM db_addition where MemoNo = '" & lastcode & "'"
    '            updmon = DirectCast(cmd.ExecuteScalar(), String)
    '            If CInt(updmon) <> CInt(mnow) Then
    '                tmp = 1
    '            Else
    '                tmp = lastn + 1
    '            End If
    '            selectname()
    '            loads()
    '            Dim actualcode As String
    '            actualcode = "Memo" & "-" & yearx & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
    '    txtmemo.Text = actualcode.ToString
    'End Sub

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
            cmd.CommandText = "SELECT last_num FROM lastmemo"
            lastn = DirectCast(cmd.ExecuteScalar(), Integer)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT MemoNo FROM db_addition ORDER BY MemoNo DESC LIMIT 1"
            lastcode = DirectCast(cmd.ExecuteScalar(), String)
        Catch ex As Exception
        End Try
        Try
            Dim cmd = SQLConnection.CreateCommand
            cmd.CommandText = "SELECT MID(MemoNo, 8, 1) FROM db_addition where MemoNo = '" & lastcode & "'"
            updmon = DirectCast(cmd.ExecuteScalar(), String)
            If CInt(updmon) <> CInt(mnow) Then
                tmp = 1
            Else
                tmp = lastn + 1
            End If
        Catch ex2 As Exception
        End Try
        Dim actualcode As String = "Memo" & ynow & "-" & mnow & "-" & Strings.Right("0000" & tmp, 5)
        txtmemo.Text = actualcode.ToString
    End Sub

    Sub autofill()
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter("select EmployeeCode, FullName from db_pegawai", SQLConnection)
        da.Fill(dt)
        Dim r As DataRow
        txtemployeecode.AutoCompleteCustomSource.Clear()
        For Each r In dt.Rows
            txtemployeecode.AutoCompleteCustomSource.Add(r.Item(0).ToString)
        Next
    End Sub

    Sub currencyform()
        txtamount.Properties.DisplayFormat.FormatType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtamount.Properties.DisplayFormat.FormatString = "n"
        txtamount.Properties.Mask.EditMask = "n"
        txtamount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtamount.Properties.Mask.UseMaskAsDisplayFormat = True
    End Sub

    Private Sub OtherIncome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        changer()
        txtperiod.Format = DateTimePickerFormat.Custom
        txtperiod.CustomFormat = "MMMM yyyy"
        txtuntil.Format = DateTimePickerFormat.Custom
        txtuntil.CustomFormat = "MMMM yyyy"
        'loadaddi()
        autofill()
        currencyform()
        'Me.txtamount.Properties.Mask.EditMask = "n0"
        'Me.txtamount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    End Sub

    Sub loads()
        Dim table As New DataTable
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select * from db_addition"
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
        End Try
    End Sub

    Sub loaddummy(ByVal table As DataTable)
        GridControl1.DataSource = DBNull.Value
        Dim tbl_baru As New DataTable
        Dim oldtable As New DataTable
        tbl_baru.Columns.Add("MemoNo")
        tbl_baru.Columns.Add("EmployeeCode")
        tbl_baru.Columns.Add("Period")
        tbl_baru.Columns.Add("Until")
        tbl_baru.Columns.Add("Amount")
        tbl_baru.Columns.Add("Sebagai")
        tbl_baru.Columns.Add("Reason")
        tbl_baru.Columns.Add("Tanggal")
        tbl_baru.Columns.Add("ApprovedBy")
        tbl_baru.Columns.Add("Status")
        tbl_baru.Columns.Add("Purpose")
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_addition where tanggal between @date1 and @date2"
        query.Parameters.AddWithValue("@date1", DateTimePicker2.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker3.Value.Date)
        oldtable.Load(query.ExecuteReader)
        For indeks As Integer = 0 To oldtable.Rows.Count - 1
            tbl_baru.Rows.Add(oldtable.Rows(indeks).Item(0),
            oldtable.Rows(indeks).Item(1),
            (oldtable.Rows(indeks).Item(2)),
            oldtable.Rows(indeks).Item(3),
            Decrypt(CType(oldtable.Rows(indeks).Item(4), String)),
            oldtable.Rows(indeks).Item(5),
            oldtable.Rows(indeks).Item(6),
            oldtable.Rows(indeks).Item(7),
            oldtable.Rows(indeks).Item(8),
            oldtable.Rows(indeks).Item(9),
            oldtable.Rows(indeks).Item(10))
        Next
        GridControl1.DataSource = tbl_baru
        GridView1.BestFitColumns()
    End Sub

    Sub loadadd()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_addition where tanggal between @date1 and @date2"
        query.Parameters.AddWithValue("@date1", DateTimePicker2.Value.Date)
        query.Parameters.AddWithValue("@date2", DateTimePicker3.Value.Date)
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        For index As Integer = 0 To tab.Rows.Count - 1
            Decrypt(tab.Rows(index).Item(4).ToString)
            GridControl1.DataSource = tab
        Next
        GridView1.BestFitColumns()
    End Sub

    Sub insertion()
        Dim dta, dtb, dtc As Date
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        dtb = DateTimePicker1.Value
        txtperiod.Format = DateTimePickerFormat.Custom
        txtperiod.CustomFormat = "MMMM yyyy"
        dta = txtperiod.Value
        txtuntil.Format = DateTimePickerFormat.Custom
        txtuntil.CustomFormat = "MMMM yyyy"
        dtc = txtuntil.Value
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select last_num from lastmemo"
        query.CommandText = "insert into db_addition " +
                                "(Memono, EmployeeCode, Period, Until, Amount, As1, Reason, Tanggal, ApprovedBy, Status) " +
                                 " values (@Memono, @EmployeeCode, @Period, @Until, @Amount, @As1, @Reason, @Date1, @appr, @status)"
        query.Parameters.AddWithValue("@Memono", txtmemo.Text)
        query.Parameters.AddWithValue("@EmployeeCode", txtemployeecode.Text)
        query.Parameters.AddWithValue("@Period", dta.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@Until", dtc.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@Amount", Encryptio(txtamount.Text.Trim))
        query.Parameters.AddWithValue("@As1", txtas.Text)
        query.Parameters.AddWithValue("@Reason", txtreason.Text)
        query.Parameters.AddWithValue("@Date1", dtb.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@appr", "")
        query.Parameters.AddWithValue("@status", "Requested")
        query.ExecuteNonQuery()
        MsgBox("Requested")
        changer()
        txtemployeecode.Text = ""
        txtperiod.Text = ""
        txtuntil.Text = ""
        txtamount.Text = ""
        txtas.Text = ""
        txtreason.Text = ""
        'loadaddi()
        'GridControl1.RefreshDataSource()
        'Dim table As New DataTable
        'Try
        '    query.CommandText = "select * from db_addition"
        '    Dim tbl_par As New DataTable
        '    Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        '    Dim cb As New MySqlCommandBuilder(adapter)
        '    adapter.Fill(table)
        '    GridControl1.DataSource = table
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If txtamount.Text = "" OrElse txtemployeecode.Text = "" OrElse txtas.Text = "" Then
            MsgBox("The required data is still empty, please fill the rest")
        Else
            insertion()
            GridView1.BestFitColumns()
        End If
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

    Private Sub GridView2_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Add(New DXMenuItem("Select Employee", New EventHandler(AddressOf SimpleButton7_Click)))
        End If
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs)

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

    Private Sub txtamount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtamount.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextBox1.Text = quer1.ToString

            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            txtemployeecode.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtemployeecode_TextChanged(sender As Object, e As EventArgs) Handles txtemployeecode.TextChanged
        Timer1.Stop()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select fullname from db_pegawai where employeecode = '" & txtemployeecode.Text & "'"
            Dim quer As String = CStr(query.ExecuteScalar)
            TextBox1.Text = quer.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Add(New DXMenuItem("Delete..", New EventHandler(AddressOf Button1_Click), GetImage))
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "delete from db_addition where memono = @memo"
        query.Parameters.AddWithValue("@memo", Button1.Text)
        query.ExecuteNonQuery()
        MsgBox("Data Removed")
        'loadadd()
        Dim table As New DataTable
        loaddummy(table)
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And MemoNo='" + GridView1.GetFocusedRowCellValue("MemoNo").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT * FROM db_addition WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatabl.Rows.Count > 0 Then
            Button1.Text = datatabl.Rows(0).Item(0).ToString
        End If
    End Sub

    Dim value12 As Integer

    Private Sub txtamount_EditValueChanged(sender As Object, e As EventArgs) Handles txtamount.EditValueChanged
        'TextEdit1.Text = txtamount.Text
        'Try
        '    value12 = CInt(txtamount.Text)
        '    txtamount.Text = Format(value12, "n")
        '    txtamount.SelectionStart = Len(txtamount.Text)\
        'Catch ex As Exception
        'End Try
        TextEdit1.Text = txtamount.Text
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If DateTimePicker2.Value.Date > DateTimePicker3.Value.Date Then
            MsgBox("Invalid date")
        Else
            Dim table As New DataTable
            loaddummy(table)
        End If
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged

    End Sub

    Private Sub TextEdit1_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        Dim allowedchar As String = "0123456789"
        If allowedchar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub
End Class