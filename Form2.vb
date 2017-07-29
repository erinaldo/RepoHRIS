Imports DevExpress.XtraGrid
Imports System.IO
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data
Imports DevExpress

Public Class Form2

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

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ShowGridPreview(GridControl1)
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Criteria.Close()
        With Criteria
            .Label2.Text = Label3.Text
            .Label1.Text = "Candidates List"
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton18_Click(sender As Object, e As EventArgs) Handles SimpleButton18.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Employee List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton20_Click(sender As Object, e As EventArgs) Handles SimpleButton20.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Overtime List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton21_Click(sender As Object, e As EventArgs) Handles SimpleButton21.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Warning Notice"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton23_Click(sender As Object, e As EventArgs) Handles SimpleButton23.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Leave Request"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton22_Click(sender As Object, e As EventArgs) Handles SimpleButton22.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Others Income / Deductions"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton24_Click(sender As Object, e As EventArgs) Handles SimpleButton24.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Loan Lists"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Holiday Lists"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton26_Click(sender As Object, e As EventArgs) Handles SimpleButton26.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Payroll Sheet"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton27_Click(sender As Object, e As EventArgs) Handles SimpleButton27.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Premi"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton25_Click(sender As Object, e As EventArgs) Handles SimpleButton25.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Loan Summary"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton28_Click(sender As Object, e As EventArgs) Handles SimpleButton28.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Lates Or Early Sign In/Out"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton30_Click(sender As Object, e As EventArgs) Handles SimpleButton30.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Terminate Lists"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
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

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Status Change"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton29_Click(sender As Object, e As EventArgs) Handles SimpleButton29.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Absences List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Private Sub SimpleButton19_Click(sender As Object, e As EventArgs) Handles SimpleButton19.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Attendance List"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub

    Sub autho()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_user where username = '" & Label3.Text & "'"
        Dim tab As New DataTable
        tab.Load(query.ExecuteReader)
        For index As Integer = 0 To tab.Rows.Count - 1
            If tab.Rows(index).Item("Candidateslists").ToString = "True" Then
                LayoutControlItem5.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem5.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("employeelists").ToString = "True" Then
                LayoutControlItem6.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem6.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("attendancelists").ToString = "True" Then
                LayoutControlItem21.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem21.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("overtimelists").ToString = "True" Then
                LayoutControlItem7.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem7.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("warninglists").ToString = "True" Then
                LayoutControlItem9.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem9.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("leavelists").ToString = "True" Then
                LayoutControlItem8.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem8.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("otherlists").ToString = "True" Then
                LayoutControlItem10.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem10.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("loanlists").ToString = "True" Then
                LayoutControlItem11.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem11.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("loansumlists").ToString = "True" Then
                LayoutControlItem13.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem13.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("payrolllists").ToString = "True" Then
                LayoutControlItem15.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem15.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("premilists").ToString = "True" Then
                LayoutControlItem14.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem14.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("latelists").ToString = "True" Then
                LayoutControlItem16.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem16.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("absencelists").ToString = "True" Then
                LayoutControlItem20.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem20.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("terminatelists").ToString = "True" Then
                LayoutControlItem17.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem17.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("statuslists").ToString = "True" Then
                LayoutControlItem19.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem19.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("pajaklists").ToString = "True" Then
                LayoutControlItem18.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem18.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("holidaylists").ToString = "True" Then
                LayoutControlItem12.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem12.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
            If tab.Rows(index).Item("sppdlists").ToString = "True" Then
                LayoutControlItem4.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                LayoutControlItem4.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
        Next
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        autho()
        Dim gridView As GridView = CType(GridControl1.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
            New GridColumnSortInfo(gridView.Columns("PayDate"), ColumnSortOrder.Ascending),
   New GridColumnSortInfo(gridView.Columns("EmployeeCode"), ColumnSortOrder.Ascending),
   New GridColumnSortInfo(gridView.Columns("IdRec"), ColumnSortOrder.Ascending),
   New GridColumnSortInfo(gridView.Columns("IssuesDates"), ColumnSortOrder.Ascending),
   New GridColumnSortInfo(gridView.Columns("WarningLevel"), ColumnSortOrder.Ascending)}, 4)
        GridView1.BestFitColumns()
        Dim navigator As ControlNavigator = GridControl1.EmbeddedNavigator
        navigator.Buttons.Append.Visible = False
        navigator.Buttons.Remove.Visible = False
        GridView1.ExpandAllGroups()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        LayoutView1.CardCaptionFormat = ""
    End Sub

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
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
                e.Menu.Items.Add(New DXMenuItem("Print Letter", New EventHandler(AddressOf download1), GetImage2))
            End If
        End If
    End Sub

    Public Function GetImage2() As Image
        Return ImageCollection1.Images(1)
    End Function

    Public ReadOnly Property DisplayFormat As DevExpress.Utils.FormatInfo

    Private Sub GridView1_CustomColumnDisplayText(sender As Object, e As Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        If e.Column.FieldName = "GajiPokok" OrElse e.Column.FieldName = "TunjanganLain" OrElse e.Column.FieldName = "JaminanKecelakaanKerja" OrElse e.Column.FieldName = "JaminanKematian" OrElse e.Column.FieldName = "Bruto" OrElse e.Column.FieldName = "BiayaJabatan" OrElse e.Column.FieldName = "JaminanHariTua" OrElse e.Column.FieldName = "JaminanPensiun" OrElse e.Column.FieldName = "NettoPerBulan" OrElse e.Column.FieldName = "NettoPerTahun" OrElse e.Column.FieldName = "Ptkp" OrElse e.Column.FieldName = "PphPerBulan" OrElse e.Column.FieldName = "Amount" OrElse e.Column.FieldName = "AmountOfLoan" OrElse e.Column.FieldName = "SalaryValue" OrElse e.Column.FieldName = "" Then
            e.Column.DisplayFormat.FormatType = Utils.FormatType.Numeric
            e.Column.DisplayFormat.FormatString = "n2"
        End If
    End Sub

    Sub download1()
        Try
            Dim sfilepath As String
            Dim buffer As Byte()
            Using cmd As New MySqlCommand("select letter from db_warning where memono = '" & LabelControl4.Text & "'", SQLConnection)
                buffer = CType(cmd.ExecuteScalar(), Byte())
            End Using
            sfilepath = Path.GetTempFileName()
            File.Move(sfilepath, Path.ChangeExtension(sfilepath, "pdf"))
            sfilepath = Path.ChangeExtension(sfilepath, ".pdf")
            File.WriteAllBytes(sfilepath, buffer)
            Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            act.BeginInvoke(sfilepath, Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            pdfviewer.PdfViewer1.LoadDocument(sFilePath)
            pdfviewer.Show()
            'Dim act As Action(Of String) = New Action(Of String)(AddressOf OpenPDFFile)
            'act.BeginInvoke(sFilePath, Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message)
            '            MsgBox("No document uploaded or document corrupted")
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

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            GridControl1.Visible = True
            SimpleButton3.Visible = False
        Else
            GridControl1.Visible = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            SimpleButton3.Visible = True
            GridControl2.Visible = True
        Else
            GridControl2.Visible = False
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        ShowGridPreview(GridControl2)
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle
        Dim view As GridView = TryCast(sender, GridView)
        e.Appearance.BackColor = Color.FloralWhite
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Dim View As GridView = CType(sender, GridView)
        If e.Column.FieldName = "Description" Or e.Column.FieldName = "Description" Then
            Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Description"))
            'e.Appearance.BackColor = Color.LightCyan
            'e.Appearance.BackColor2 = Color.Orange
        End If
    End Sub

    Private Sub LayoutView1_CustomColumnDisplayText(sender As Object, e As Views.Base.CustomColumnDisplayTextEventArgs) Handles LayoutView1.CustomColumnDisplayText
        If e.Column.FieldName = "GajiPokok" OrElse e.Column.FieldName = "TunjanganLain" OrElse e.Column.FieldName = "JaminanKecelakaanKerja" OrElse e.Column.FieldName = "JaminanKematian" OrElse e.Column.FieldName = "Bruto" OrElse e.Column.FieldName = "BiayaJabatan" OrElse e.Column.FieldName = "JaminanHariTua" OrElse e.Column.FieldName = "JaminanPensiun" OrElse e.Column.FieldName = "NettoPerBulan" OrElse e.Column.FieldName = "NettoPerTahun" OrElse e.Column.FieldName = "Ptkp" OrElse e.Column.FieldName = "PphPerBulan" OrElse e.Column.FieldName = "Amount" OrElse e.Column.FieldName = "AmountOfLoan" OrElse e.Column.FieldName = "SalaryValue" OrElse e.Column.FieldName = "" Then
            e.Column.DisplayFormat.FormatType = Utils.FormatType.Numeric
            e.Column.DisplayFormat.FormatString = "n2"
        End If
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        Criteria.Close()
        With Criteria
            .Label1.Text = "Surat Dinas"
            .Label2.Text = Label3.Text
            .Show()
        End With
        Close()
    End Sub
End Class