Public Class AspekKerja
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Close()
    End Sub

    Private Sub AspekKerja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        aspek()
    End Sub

    Sub aspek()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select Tasks, Nama from db_calcbor"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridControl1.UseEmbeddedNavigator = True
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "and Tasks='" + GridView1.GetFocusedRowCellValue("Tasks").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT * FROM db_calcbor WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
            If datatabl.Rows.Count > -1 Then
                LabelControl1.Text = datatabl.Rows(0).Item(2).ToString
                LabelControl2.Text = datatabl.Rows(0).Item(3).ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Dim modak As New ModAK

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If modak Is Nothing OrElse modak.IsDisposed OrElse modak.MinimizeBox Then
            modak.Close()
            modak = New ModAK
        End If
        modak.Show()
    End Sub

    Dim mods As New ModAKS

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()

        query.CommandText = "insert into db_tmpname (status) values (@status)"
        query.Parameters.Clear()
        query.Parameters.AddWithValue("@status", LabelControl1.Text)
        query.ExecuteNonQuery()
        If mods Is Nothing OrElse mods.IsDisposed OrElse mods.MinimizeBox Then
            mods.Close()
            mods = New ModAKS
        End If
        mods.Show()
    End Sub

    Sub deletion()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "delete from db_calcbor where tasks = '" & LabelControl1.Text & "'"
        query.ExecuteNonQuery()
        MsgBox("Deleted")
        query.CommandText = "delete from db_targetbor where code = '" & LabelControl1.Text & "'"
        query.ExecuteNonQuery()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Dim mess2 As String
        mess2 = CType(MsgBox("Are you sure to delete this data?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(mess2, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            deletion()
        End If
        aspek()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        aspek()
    End Sub
End Class