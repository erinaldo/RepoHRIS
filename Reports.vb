Imports System.IO
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
'Imports DevExpress.EasyTest.Framework
Imports DevExpress.Data
Imports DevExpress
Imports DevExpress.XtraEditors

Public Class Reports
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

    Sub ShowGridPreview(ByVal grid As GridControl)
        If Not grid.IsPrintingAvailable Then
            MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error")
            Return
        End If
        grid.ShowPrintPreview()
    End Sub

    Sub PrintGrid(ByVal grid As GridControl)
        If Not grid.IsPrintingAvailable Then
            MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error")
            Return
        End If
        grid.Print()
    End Sub

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs)
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowHandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowHandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ShowGridPreview(GridControl1)
    End Sub

    Dim crit As New Criteria

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Criteria.Close()
        With Criteria
            .Label2.Text = Label3.Text
            .Show()
        End With
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Candidates List")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label2.Text = Label3.Text
            .Label1.Text = "Candidates List"
            .Show()
        End With
        Close()
    End Sub

    Private Function active(ByVal view As GridView, ByVal row As Integer) As Boolean
        Try
            Dim val As String = Convert.ToString(view.GetRowCellValue(row, "StartingHour"))
            Return (val = "Active")
            '"timediff(Date_Format(JamMulai, '%H:%i:%s'), '08:00:00')"
        Catch
            Return False
        End Try
    End Function

    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Dim gridView As GridView = CType(GridControl1.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
            New GridColumnSortInfo(gridView.Columns("PayDate"), ColumnSortOrder.Ascending),
   New GridColumnSortInfo(gridView.Columns("EmployeeCode"), ColumnSortOrder.Ascending),
   New GridColumnSortInfo(gridView.Columns("IdRec"), ColumnSortOrder.Ascending),
   New GridColumnSortInfo(gridView.Columns("WarningLevel"), ColumnSortOrder.Ascending)}, 3)
        GridView1.BestFitColumns()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            'query.CommandText = "select status from db_tmpname"
            'LabelControl2.Text = CStr(query.ExecuteScalar)

            query.CommandText = "select candidateslists from db_user where username = '" & Label3.Text & "'"
            Dim candl As String = CStr(query.ExecuteScalar)
            If candl = "True" Then
                LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            query.CommandText = "select employeelists from db_user where username = '" & Label3.Text & "'"
            Dim empl As String = CStr(query.ExecuteScalar)
            If empl = "True" Then
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            query.CommandText = "select attendancelists from db_user where username = '" & Label3.Text & "'"
            Dim attl As String = CStr(query.ExecuteScalar)
            If attl = "True" Then
                LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select overtimelists from db_user where username = '" & Label3.Text & "'"
            Dim otl As String = CStr(query.ExecuteScalar)
            If otl = "True" Then
                LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select warninglists from db_user where username = '" & Label3.Text & "'"
            Dim warl As String = CStr(query.ExecuteScalar)
            If warl = "True" Then
                LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select leavelists from db_user where username = '" & Label3.Text & "'"
            Dim leal As String = CStr(query.ExecuteScalar)
            If leal = "True" Then
                LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select otherlists from db_user where username = '" & Label3.Text & "'"
            Dim otherl As String = CStr(query.ExecuteScalar)
            If otherl = "True" Then
                LayoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select loanlists from db_user where username = '" & Label3.Text & "'"
            Dim lol As String = CStr(query.ExecuteScalar)
            If lol = "True" Then
                LayoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            query.CommandText = "select loansumlists from db_user where username = '" & Label3.Text & "'"
            Dim lsl As String = CStr(query.ExecuteScalar)
            If lsl = "True" Then
                LayoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select payrolllists from db_user where username = '" & Label3.Text & "'"
            Dim plp As String = CStr(query.ExecuteScalar)
            If plp = "True" Then
                LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select premilists from db_user where username = '" & Label3.Text & "'"
            Dim pls As String = CStr(query.ExecuteScalar)
            If pls = "True" Then
                LayoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select latelists from db_user where username = '" & Label3.Text & "'"
            Dim ltl As String = CStr(query.ExecuteScalar)
            If ltl = "True" Then
                LayoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select absencelists from db_user where username = '" & Label3.Text & "'"
            Dim absl As String = CStr(query.ExecuteScalar)
            If absl = "True" Then
                LayoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select terminatelists from db_user where username = '" & Label3.Text & "'"
            Dim tltl As String = CStr(query.ExecuteScalar)
            If tltl = "True" Then
                LayoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            query.CommandText = "select statuslists from db_user where username = '" & Label3.Text & "'"
            Dim slsl As String = CStr(query.ExecuteScalar)
            If slsl = "True" Then
                LayoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            query.CommandText = "select pajaklists from db_user where username = '" & Label3.Text & "'"
            Dim pjkl As String = CStr(query.ExecuteScalar)
            If pjkl = "True" Then
                LayoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            query.CommandText = "select holidaylists from db_user where username = '" & Label3.Text & "'"
            Dim hol As String = CStr(query.ExecuteScalar)
            If hol = "True" Then
                LayoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Dim navigator As ControlNavigator = GridControl1.EmbeddedNavigator
        navigator.Buttons.Append.Visible = False
        navigator.Buttons.Remove.Visible = False
        GridView1.ExpandAllGroups()
        'GridColumn.SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Count, "{0:n0} rows")
    End Sub

    Private Sub SimpleButton18_Click(sender As Object, e As EventArgs) Handles SimpleButton18.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Employee List")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Employee List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton19_Click(sender As Object, e As EventArgs) Handles SimpleButton19.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Attendance List")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Attendance List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton20_Click(sender As Object, e As EventArgs) Handles SimpleButton20.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Overtime List")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Overtime List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton21_Click(sender As Object, e As EventArgs) Handles SimpleButton21.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Warning Notice")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Warning Notice"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton23_Click(sender As Object, e As EventArgs) Handles SimpleButton23.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Leave Request")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Leave Request"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton22_Click(sender As Object, e As EventArgs) Handles SimpleButton22.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Others Income / Deductions")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Others Income / Deductions"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton24_Click(sender As Object, e As EventArgs) Handles SimpleButton24.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Loan Lists")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Loan Lists"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton25_Click(sender As Object, e As EventArgs) Handles SimpleButton25.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Loan Summary")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Loan Summary"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton26_Click(sender As Object, e As EventArgs) Handles SimpleButton26.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()
        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        ''query.Parameters.AddWithValue("@status", "Payroll Sheet")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Payroll Sheet"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton27_Click(sender As Object, e As EventArgs) Handles SimpleButton27.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()

        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Premi")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Premi"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton28_Click(sender As Object, e As EventArgs) Handles SimpleButton28.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()

        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Lates Or Early Sign In/Out")
        'query.ExecuteNonQuery()

        'If crit Is Nothing OrElse crit.IsDisposed OrElse crit.MinimizeBox Then
        '    crit.Close()
        '    crit = New Criteria
        'End If
        'crit.Show()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Lates Or Early Sign In/Out"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton29_Click(sender As Object, e As EventArgs) Handles SimpleButton29.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()

        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Absences List")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Absences List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton30_Click(sender As Object, e As EventArgs) Handles SimpleButton30.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()

        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Terminate Lists")
        'query.ExecuteNonQuery()

        'If crit Is Nothing OrElse crit.IsDisposed OrElse crit.MinimizeBox Then
        '    crit.Close()
        '    crit = New Criteria
        'End If
        'crit.Show()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Terminate Lists"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()

        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Pajak")
        'query.ExecuteNonQuery()

        'If crit Is Nothing OrElse crit.IsDisposed OrElse crit.MinimizeBox Then
        '    crit.Close()
        '    crit = New Criteria
        'End If
        'crit.Show()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Pajak"
            .Label2.Text = Label3.Text
            .CheckEdit3.Enabled = False
            .DateTimePicker1.Enabled = False
            .DateTimePicker2.Enabled = False
            .Show()
        End With
        Close()
    End Sub

    Private Sub Reports_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Tile_Control.Close()
        'Tile_Control.Show()
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()

        'query.CommandText = "insert into db_tmpname (status) values(@status)"
        'query.Parameters.AddWithValue("@status", "Status Change")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Status Change"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        'Dim query As MySqlCommand = SQLConnection.CreateCommand
        'query.CommandText = "truncate db_tmpname"
        'query.ExecuteNonQuery()

        'query.CommandText = "insert into db_tmpname (status) values (@status)"
        'query.Parameters.AddWithValue("@status", "Holiday Lists")
        'query.ExecuteNonQuery()
        Criteria.Close()
        With Criteria
            .Label1.Text = "Holiday Lists"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Public ReadOnly Property DisplayFormat As DevExpress.Utils.FormatInfo

    Private Sub GridView1_CustomColumnDisplayText(sender As Object, e As Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        If e.Column.FieldName = "SalaryValue" Then
            e.Column.DisplayFormat.FormatType = Utils.FormatType.Numeric
            e.Column.DisplayFormat.FormatString = "n2"
        End If
    End Sub

    Sub download()
        Try
            Dim sFilePath As String
            Dim buffer As Byte()
            Using cmd As New MySqlCommand("select letter from db_warning where memono = '" & LabelControl4.Text & "'", SQLConnection)
                buffer = CType(cmd.ExecuteScalar(), Byte())
            End Using
            sFilePath = Path.GetTempFileName()
            File.Move(sFilePath, Path.ChangeExtension(sFilePath, ".pdf"))
            sFilePath = Path.ChangeExtension(sFilePath, ".pdf")
            File.WriteAllBytes(sFilePath, buffer)
            Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            act.BeginInvoke(sFilePath, Nothing, Nothing)
        Catch ex As Exception
            MsgBox("No document uploaded or document corrupted")
        End Try
    End Sub

    Private Shared Sub OpenPDFFile(ByVal sFilePath As String)
        Using p As New Process
            p.StartInfo = New ProcessStartInfo(CType(sFilePath, String))
            p.Start()
            p.WaitForExit()
            Try
                File.Delete(CType(sFilePath, String))
            Catch
            End Try
        End Using
    End Sub

    Private Function GetImage1() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Sub GridView1_PopupMenuShowing_1(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        Dim view As GridView = CType(sender, GridView)
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            Dim rowhandle As Integer = e.HitInfo.RowHandle
            e.Menu.Items.Clear()
            Dim item As DXMenuItem = CreateMergingEnabledMenuItem(view, rowhandle)
            item.BeginGroup = True
            e.Menu.Items.Add(item)
        End If
        If LabelControl2.Text = "Warning Notice" Then
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
                e.Menu.Items.Add(New DXMenuItem("View Letter", New EventHandler(AddressOf download), GetImage1))
            End If
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "And MemoNo='" + GridView1.GetFocusedRowCellValue("MemoNo").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT MemoNo FROM db_warning WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatabl.Rows.Count > 0 Then
            LabelControl4.Text = datatabl.Rows(0).Item(0).ToString()
        End If
    End Sub
End Class