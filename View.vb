Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress

Public Class View
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

    Private Sub View_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mth As String = ""
        If Now.Date.Month = 1 Then
            mth = "January"
        ElseIf Now.Date.Month = 2 Then
            mth = "February"
        ElseIf Now.Date.Month = 3 Then
            mth = "March"
        ElseIf Now.Date.Month = 4 Then
            mth = "April"
        ElseIf Now.Date.Month = 5 Then
            mth = "May"
        ElseIf Now.Date.Month = 6 Then
            mth = "June"
        ElseIf Now.Date.Month = 7 Then
            mth = "July"
        ElseIf Now.Date.Month = 8 Then
            mth = "August"
        ElseIf Now.Date.Month = 9 Then
            mth = "September"
        ElseIf Now.Date.Month = 10 Then
            mth = "October"
        ElseIf Now.Date.Month = 11 Then
            mth = "November"
        ElseIf Now.Date.Month = 12 Then
            mth = "December"
        End If
        CardView1.CardCaptionFormat = "PaySlip " & mth & " " & Now.Date.Year & ""
    End Sub

    Private Sub CardView1_CustomColumnDisplayText(sender As Object, e As Views.Base.CustomColumnDisplayTextEventArgs) Handles CardView1.CustomColumnDisplayText
        If e.Column.FieldName = "Salary" OrElse e.Column.FieldName = "Deductions" OrElse e.Column.FieldName = "MealRate" OrElse e.Column.FieldName = "Transport" OrElse e.Column.FieldName = "Allowance" OrElse e.Column.FieldName = "Incentives" OrElse e.Column.FieldName = "FixedSalary" Then
            e.Column.DisplayFormat.FormatType = Utils.FormatType.Numeric
            e.Column.DisplayFormat.FormatString = "n2"
        End If
    End Sub
End Class