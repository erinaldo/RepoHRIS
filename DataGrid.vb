Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Views.Grid

Module DataGrid
    ' Create a check menu item that triggers the Boolean AllowCellMerge option.
    Function CreateMergingEnabledMenuItem(ByVal view As GridView,
    ByVal rowHandle As Integer) As DXMenuCheckItem
        Dim checkItem As New DXMenuCheckItem("&Merging Enabled", view.OptionsView.AllowCellMerge, Nothing, AddressOf OnMergingEnabledClick)
        checkItem.Tag = New RowInfo(view, rowHandle)
        Return checkItem
    End Function

    'The handler for the DeleteRow menu item
    Sub OnDeleteRowClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As DXMenuItem = CType(sender, DXMenuItem)
        Dim info As RowInfo = CType(item.Tag, RowInfo)
        info.View.DeleteRow(info.RowHandle)
    End Sub

    'The handler for the MergingEnabled menu item
    Sub OnMergingEnabledClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As DXMenuCheckItem = CType(CType(sender, DXMenuItem), DXMenuCheckItem)
        Dim info As RowInfo = CType(item.Tag, RowInfo)
        info.View.OptionsView.AllowCellMerge = item.Checked
    End Sub
    ' ...
    ' The class that stores menu specific information
    Class RowInfo
        Public Sub New(ByVal view As GridView, ByVal rowHandle As Integer)
            Me.RowHandle = rowHandle
            Me.View = view
        End Sub
        Public View As GridView
        Public RowHandle As Integer
    End Class
End Module
