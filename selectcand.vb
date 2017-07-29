Imports System.IO
Imports DevExpress.Utils.Menu

Public Class selectcand

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

    Sub loaddata()
        GridControl1.RefreshDataSource()
        Dim table As New DataTable
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "select Idrec, FullName, Position, PlaceOfBirth, DateOfBirth, Gender, Religion, Address, IdNumber, PhoneNumber, Status, Position as ExpectedPosition, ApplicationDate, BlackList from db_recruitment where status != 'Pending'"
            sqlcommand.Connection = SQLConnection
            Dim tbl_par As New DataTable
            Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(table)
            GridControl1.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overridable Property UseEmbeddedNavigator As Boolean

    Private Sub selectcand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        loaddata()
        GridView1.BestFitColumns()
        GridControl1.UseEmbeddedNavigator = True
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "and Idrec='" + GridView1.GetFocusedRowCellValue("Idrec").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT FullName, Idrec, Photo, Position, Status FROM db_recruitment WHERE 1=1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If datatabl.Rows.Count > 0 Then
            Label2.Text = datatabl.Rows(0).Item(0).ToString()
            Label3.Text = datatabl.Rows(0).Item(1).ToString
            Dim filefoto As Byte() = CType(datatabl.Rows(0).Item(2), Byte())
            If filefoto.Length > 0 Then
                PictureBox1.Image = ByteToImage(filefoto)
            Else
                PictureBox1.Image = Nothing
                PictureBox1.Refresh()
            End If
            Label4.Text = datatabl.Rows(0).Item(3).ToString
            Label5.Text = datatabl.Rows(0).Item(4).ToString
        End If
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        loaddata()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Dim del As MySqlCommand = SQLConnection.CreateCommand
        del.CommandText = "truncate db_tmpname"
        del.ExecuteNonQuery()
        Dim name As MySqlCommand = SQLConnection.CreateCommand
        name.CommandText = "insert into db_tmpname" +
                            "(name, idrec, jobtitle, status)" +
                            "Values(@name, @idrec, @jobtitle, @status)"
        name.Parameters.AddWithValue("@name", Label2.Text)
        name.Parameters.AddWithValue("@idrec", Label3.Text)
        name.Parameters.AddWithValue("@jobtitle", Label4.Text)
        name.Parameters.AddWithValue("@status", Label5.Text)
        name.ExecuteNonQuery()
        Close()
    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Add(New DXMenuItem("Select Candidates", New EventHandler(AddressOf SimpleButton3_Click), GetImage))
        End If
    End Sub
End Class