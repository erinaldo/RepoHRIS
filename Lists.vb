Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class Lists
    Sub ShowGridPreview(ByVal grid As GridControl)
        If Not grid.IsPrintingAvailable Then
            MsgBox("The 'DevExpress.XtraPrinting' library is not found", MsgBoxStyle.Information, "Error")
            Return
        End If
        grid.ShowPrintPreview()
    End Sub


    Sub PrintGrid(ByVal grid As GridControl)
        If Not grid.IsPrintingAvailable Then
            MsgBox("The 'DevExpress.XtraPrinting' library is not found", MsgBoxStyle.Information, "Error")
            Return
        End If
        grid.Print()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ShowGridPreview(GridControl1)
    End Sub

    Private Sub Lists_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim del As MySqlCommand = SQLConnection.CreateCommand
        del.CommandText = "delete from db_hasil where EmployeeCode != 'absbahsgedeg'"
        del.ExecuteNonQuery()

        Dim dele As MySqlCommand = SQLConnection.CreateCommand
        dele.CommandText = "delete from db_temp where employeecode != '232v3g2'"
        dele.ExecuteNonQuery()
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

    Private Sub Lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub
End Class