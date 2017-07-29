Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Public Class Borongan

    Private Sub Borongan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        loaddata()
        fillcombo()
        machines()
        autofill()
        Dim gridView As GridView = CType(GridControl1.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
   New GridColumnSortInfo(gridView.Columns("EmployeeType"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
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

    Sub premilist()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select ID, EmployeeCode, FullName, CompanyCode, Tasks, Machines, Quantity, Operators, Tanggal, Remarks from db_borongan where tanggal = @tgl"
        query.Parameters.AddWithValue("@tgl", DateTimePicker2.Value.Date)
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Start()
        selectemp.Close()
        With selectemp
            .Label6.Text = Label2.Text
            .Show()
        End With
    End Sub

    Dim tbl_par1, tbl As New DataTable

    Sub fillcombo()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select machine from db_machines where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl)
        For index As Integer = 0 To tbl.Rows.Count - 1
            ComboBoxEdit2.Properties.Items.Add(tbl.Rows(index).Item(0).ToString)
        Next
    End Sub

    Sub loaddata()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        sqlcommand.CommandText = "select tasks, amount from db_calcbor where disabled = '0'"
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par1)
        For index As Integer = 0 To tbl_par1.Rows.Count - 1
            ComboBoxEdit1.Properties.Items.Add(tbl_par1.Rows(index).Item(0).ToString())
            ComboBoxEdit4.Properties.Items.Add(tbl_par1.Rows(index).Item(0).ToString())
        Next
    End Sub

    Dim tbl_par3 As New DataTable

    Sub machines()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select machine from db_machines where isenable = '1'"
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par3)
        For index As Integer = 0 To tbl_par3.Rows.Count - 1
            ComboBoxEdit3.Properties.Items.Add(tbl_par3.Rows(index).Item(0).ToString)
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            textedit2.Text = quer1.ToString

            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            textedit1.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs)
        Timer1.Stop()
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select amount from db_calcbor where tasks = @tasks"
            query.Parameters.AddWithValue("@tasks", ComboBoxEdit1.Text)
            Dim quer As Double = CDbl(query.ExecuteScalar)
            Dim qty As Double = Convert.ToDouble(TextEdit4.Text)
            TextEdit5.Text = CType(quer * qty, String)
            TextEdit4.Text = "1"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextEdit4_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit4.EditValueChanged
    End Sub

    Sub saving()
        Try
            Dim query1 As MySqlCommand = SQLConnection.CreateCommand
            query1.CommandText = "select companycode from db_pegawai where employeecode = '" & textedit1.Text & "'"
            Dim quer As String = CStr(query1.ExecuteScalar)
            query1.CommandText = "select quantity from db_targetbor where code = '" & ComboBoxEdit1.Text & "' and Goals = 'Target 1'"
            Dim queries2 As Integer = CInt(query1.ExecuteScalar)
            Dim queries As MySqlCommand = SQLConnection.CreateCommand
            queries.CommandText = "insert into db_borongan (EmployeeCode, CompanyCode, FullName, Tasks, Machines, Quantity, Target, Amount, Amount1, Amount2, Amount3, Tanggal, Remarks, Operators) " +
                            " values ( @emp, @comp, @fullname, @tasks, @machines, @quantity, @Quantity1, @amount, @amount1, @amount2, @amount3, @date, @remarks, @operators)"
            ''from db_targetbor where code =  '" & ComboBoxEdit1.Text & "' and Goals = 'Target 1'"
            queries.Parameters.AddWithValue("@emp", textedit1.Text)
            queries.Parameters.AddWithValue("@comp", quer)
            queries.Parameters.AddWithValue("@fullname", textedit2.Text)
            queries.Parameters.AddWithValue("@tasks", ComboBoxEdit1.Text)
            queries.Parameters.AddWithValue("@machines", ComboBoxEdit2.Text)
            queries.Parameters.AddWithValue("@quantity", TextEdit4.Text)
            queries.Parameters.AddWithValue("@quantity1", queries2.ToString)
            queries.Parameters.AddWithValue("@amount", TextEdit5.Text)
            queries.Parameters.AddWithValue("@amount1", TextEdit5.Text)
            queries.Parameters.AddWithValue("@amount2", TextEdit5.Text)
            queries.Parameters.AddWithValue("@amount3", TextEdit5.Text)
            queries.Parameters.AddWithValue("@date", Date.Now)
            queries.Parameters.AddWithValue("@remarks", RichTextBox1.Text)
            queries.Parameters.AddWithValue("@Operators", TextEdit3.Text)
            queries.ExecuteNonQuery()
            MsgBox("Saved", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select quantity from db_targetbor where code = '" & ComboBoxEdit1.Text & "' and Goals = 'Target 2'"
        Dim quer1 As Integer = CInt(query.ExecuteScalar)
        query.CommandText = "update db_borongan set" +
                                " target1 = @target2" +
                                " where tasks = @tasks"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@target2", quer1)
        query.Parameters.AddWithValue("@tasks", ComboBoxEdit1.Text)
        query.ExecuteNonQuery()

        query.CommandText = "select quantity from db_targetbor where code = '" & ComboBoxEdit1.Text & "' and Goals = 'Target 3'"
        Dim quer2 As Integer = CInt(query.ExecuteScalar)
        query.CommandText = "update db_borongan set" +
                                " target2 = @target3" +
                                " where tasks = @tasks"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@target3", quer2)
        query.Parameters.AddWithValue("@tasks", ComboBoxEdit1.Text)
        query.ExecuteNonQuery()

        query.CommandText = "select quantity from db_targetbor where code = '" & ComboBoxEdit1.Text & "' and Goals = 'Target 4'"
        Dim quer3 As Integer = CInt(query.ExecuteScalar)
        query.CommandText = "update db_borongan set" +
                                " target3 = @target4" +
                                " where tasks = @tasks"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@target4", quer3)
        query.Parameters.AddWithValue("@tasks", ComboBoxEdit1.Text)
        query.ExecuteNonQuery()
        textedit1.Text = ""
        textedit2.Text = ""
        ComboBoxEdit1.Text = ""
        ComboBoxEdit2.Text = ""
        TextEdit5.Text = ""
        RichTextBox1.Text = ""
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If textedit1.Text = "" OrElse textedit2.Text = "" Then
            MsgBox("Empty field need to fill", MsgBoxStyle.Information, "Required")
        Else
            saving()
        End If
    End Sub

    Private Sub ComboBoxEdit4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit4.SelectedIndexChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select amount from db_calcbor where tasks = @tasks"
            query.Parameters.AddWithValue("@tasks", ComboBoxEdit4.Text)
            Dim quer As Double = CDbl(query.ExecuteScalar)
            Dim qty As Double = Convert.ToDouble(TextEdit7.Text)
            TextEdit6.Text = CType(quer * qty, String)
            TextEdit7.Text = "1"
        Catch ex As Exception
        End Try
    End Sub

    Sub updation()
        Dim dt As Date
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        dt = DateTimePicker1.Value

        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_borongan set " +
                            " Tasks = @tasks" +
                            ", Machines = @machines" +
                            ", Quantity = @qty" +
                            ", Operators = @opt" +
                            ", Tanggal = @tgl" +
                            ", Remarks = @remarks" +
                            ", Amount = @amount" +
                            " where ID = @id"
        query.Parameters.AddWithValue("@tasks", ComboBoxEdit4.Text)
        query.Parameters.AddWithValue("@Machines", ComboBoxEdit3.Text)
        query.Parameters.AddWithValue("@qty", TextEdit7.Text)
        query.Parameters.AddWithValue("@Opt", TextEdit8.Text)
        query.Parameters.AddWithValue("@Tgl", dt.ToString("yyyy-MM-dd"))
        query.Parameters.AddWithValue("@Remarks", RichTextBox2.Text)
        query.Parameters.AddWithValue("@Amount", TextEdit6.Text)
        query.Parameters.AddWithValue("@id", Label1.Text)
        query.ExecuteNonQuery()
        MsgBox("Changed", MsgBoxStyle.Information, "Succes")
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And ID='" + GridView1.GetFocusedRowCellValue("ID").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT * FROM db_borongan WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatabl.Rows.Count > 0 Then
            ComboBoxEdit4.Text = datatabl.Rows(0).Item(4).ToString()
            ComboBoxEdit3.Text = datatabl.Rows(0).Item(5).ToString()
            TextEdit8.Text = datatabl.Rows(0).Item(17).ToString
            Label1.Text = datatabl.Rows(0).Item(0).ToString
            TextEdit7.Text = datatabl.Rows(0).Item(6).ToString
            TextEdit6.Text = datatabl.Rows(0).Item(11).ToString
            DateTimePicker1.Value = CDate(datatabl.Rows(0).Item(15).ToString)
            RichTextBox2.Text = datatabl.Rows(0).Item(16).ToString
        End If
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        updation()
        premilist()
    End Sub

    Private Sub textedit1_TextChanged(sender As Object, e As EventArgs) Handles textedit1.TextChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select fullname from db_pegawai where employeecode = '" & textedit1.Text & "'"
            Dim quer As String = CStr(query.ExecuteScalar)
            textedit2.Text = quer.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        premilist()
    End Sub
End Class