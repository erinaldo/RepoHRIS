Imports System.IO
Public Class Documents
    Private Sub Documents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
    End Sub

    Private Sub txtchanges()
        'Try
        Dim filepath As String
        Dim buffer As Byte()

        Dim query As MySqlCommand = SQLConnection.CreateCommand
        If ComboBoxEdit1.Text = "Surat Teguran" Then
            query.CommandText = "select count(suratteguran) from db_document"
            Dim hsl As Integer = CInt(query.ExecuteScalar)
            If hsl = 0 Then
                Dim mess As String = CType(MsgBox("you have nothing to view" & vbCrLf & "You want to upload the new one?", MsgBoxStyle.YesNo), String)
                If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                    Dim openfile As New OpenFileDialog
                    openfile.InitialDirectory = "C:\"
                    openfile.Title = "Open a document files"
                    openfile.Filter = "Docx Files|*.docx"
                    openfile.ShowDialog()
                    LabelControl1.Text = openfile.FileName
                    Dim finfo As New FileInfo(LabelControl1.Text)
                    Dim numBytes As Long = finfo.Length
                    Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fstream)
                    Dim data As Byte() = br.ReadBytes(CInt(numBytes))
                    br.Close()
                    fstream.Close()
                    query.CommandText = "insert into db_document (suratteguran) values (@st)"
                    If Not data Is Nothing Then
                        query.Parameters.Clear()
                        query.Parameters.AddWithValue("@st", data)
                    Else
                        query.Parameters.AddWithValue("@st", "")
                    End If
                    query.ExecuteNonQuery()
                    MsgBox("Insert succesful", MsgBoxStyle.Information)
                End If
            Else
                query.CommandText = "select suratteguran from db_document top limit 1"
            End If
        ElseIf ComboBoxEdit1.Text = "Surat Peringatan" Then
            query.CommandText = "select count(suratsp) from db_document"
            Dim hsl As Integer = CInt(query.ExecuteScalar)
            If hsl = 0 Then
                Dim mess As String = CType(MsgBox("You have nothing to be view" & vbCrLf & "You want to upload the new one?", MsgBoxStyle.YesNo), String)
                If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                    Dim openfile As New OpenFileDialog
                    openfile.InitialDirectory = "C:\"
                    openfile.Title = "Open a document files"
                    openfile.Filter = "Docx Files|*.docx"
                    openfile.ShowDialog()
                    LabelControl1.Text = openfile.FileName
                    Dim finfo As New FileInfo(LabelControl1.Text)
                    Dim numBytes As Long = finfo.Length
                    Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fstream)
                    Dim data As Byte() = br.ReadBytes(CInt(numBytes))
                    br.Close()
                    fstream.Close()
                    query.CommandText = "insert into db_document (suratsp) values ( @sp)"
                    If Not data Is Nothing Then
                        query.Parameters.Clear()
                        query.Parameters.AddWithValue("@sp", data)
                    Else
                        query.Parameters.AddWithValue("@sp", "")
                    End If
                    query.ExecuteNonQuery()
                    MsgBox("Insert succesful", MsgBoxStyle.Information)
                End If
            Else
                query.CommandText = "select suratsp from db_document top limit 1"
            End If
        ElseIf ComboBoxEdit1.Text = "Surat Dinas" Then
            query.CommandText = "select count(suratdinas) from db_document"
            Dim hsl As Integer = CInt(query.ExecuteScalar)
            If hsl = 0 Then
                Dim mess As String = CType(MsgBox("You have nothing to view" & vbCrLf & "You want to upload the new one?", MsgBoxStyle.YesNo), String)
                If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
                    Dim openfile As New OpenFileDialog
                    openfile.Title = "Open a document files"
                    openfile.Filter = "Docx Files|*.docx"
                    openfile.ShowDialog()
                    LabelControl1.Text = openfile.FileName
                    Dim finfo As New FileInfo(LabelControl1.Text)
                    Dim numbytes As Long = finfo.Length
                    Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fstream)
                    Dim data As Byte() = br.ReadBytes(CInt(numbytes))
                    br.Close()
                    fstream.Close()
                    query.CommandText = "insert into db_document (suratdinas) values (@sd)"
                    If Not data Is Nothing Then
                        query.Parameters.Clear()
                        query.Parameters.AddWithValue("@sd", data)
                    Else
                        query.Parameters.AddWithValue("@sd", "")
                    End If
                    query.ExecuteNonQuery()
                    MsgBox("Insert succesful", MsgBoxStyle.Information)
                End If
            Else
                query.CommandText = "select suratdinas from db_document top limit 1"
            End If
        End If
        buffer = CType(query.ExecuteScalar(), Byte())
        filepath = Path.GetTempFileName()
        File.Move(filepath, Path.ChangeExtension(filepath, ".docx"))
        filepath = Path.ChangeExtension(filepath, ".docx")
        File.WriteAllBytes(filepath, buffer)
        RichEditControl1.LoadDocument(filepath)
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If ComboBoxEdit1.Text = "" Then
            MsgBox("Please select the document in combo box", MsgBoxStyle.Exclamation)
        Else
            txtchanges()
        End If
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            If ComboBoxEdit1.Text = "Surat Teguran" Then
                query.CommandText = "select count(suratteguran) from db_document"
                Dim hsl As Integer = CInt(query.ExecuteScalar)
                If hsl = 1 Then
                    Dim openfile As New OpenFileDialog
                    openfile.InitialDirectory = "C:\"
                    openfile.Title = "Open a document files"
                    openfile.Filter = "Docx Files|*.docx"
                    openfile.ShowDialog()
                    LabelControl1.Text = openfile.FileName
                    Dim finfo As New FileInfo(LabelControl1.Text)
                    Dim numBytes As Long = finfo.Length
                    Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fstream)
                    Dim data As Byte() = br.ReadBytes(CInt(numBytes))
                    br.Close()
                    fstream.Close()
                    query.CommandText = "update db_document set suratteguran = @st"
                    If Not data Is Nothing Then
                        query.Parameters.Clear()
                        query.Parameters.AddWithValue("@st", data)
                    Else
                        query.Parameters.AddWithValue("@st", "")
                    End If
                    query.ExecuteNonQuery()
                Else
                    MsgBox("Nothing to change", MsgBoxStyle.Exclamation)
                End If
            ElseIf ComboBoxEdit1.Text = "Surat Peringatan" Then
                query.CommandText = "select count(suratsp) from db_document"
                Dim hsl As Integer = CInt(query.ExecuteScalar)
                If hsl = 1 Then
                    Dim openfile As New OpenFileDialog
                    openfile.InitialDirectory = "C:\"
                    openfile.Title = "Open a document files"
                    openfile.Filter = "Docx Files|*.docx"
                    openfile.ShowDialog()
                    LabelControl1.Text = openfile.FileName
                    Dim finfo As New FileInfo(LabelControl1.Text)
                    Dim numBytes As Long = finfo.Length
                    Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fstream)
                    Dim data As Byte() = br.ReadBytes(CInt(numBytes))
                    br.Close()
                    fstream.Close()
                    query.CommandText = "update db_document set suratsp =  @sp"
                    If Not data Is Nothing Then
                        query.Parameters.Clear()
                        query.Parameters.AddWithValue("@sp", data)
                    Else
                        query.Parameters.AddWithValue("@sp", "")
                    End If
                    query.ExecuteNonQuery()
                    MsgBox("Update succesful", MsgBoxStyle.Information)
                Else
                    MsgBox("Nothing to change", MsgBoxStyle.Exclamation)
                End If
            ElseIf ComboBoxEdit1.Text = "Surat Dinas" Then
                query.CommandText = "select count(suratdinas) from db_document"
                Dim hsl As Integer = CInt(query.ExecuteScalar)
                If hsl = 1 Then
                    Dim openfile As New OpenFileDialog
                    openfile.InitialDirectory = "c:\"
                    openfile.Title = "Open a document files"
                    openfile.Filter = "Docx Files|*.docx"
                    openfile.ShowDialog()
                    LabelControl1.Text = openfile.FileName
                    Dim finfo As New FileInfo(LabelControl1.Text)
                    Dim numbytes As Long = finfo.Length
                    Dim fstream As New FileStream(LabelControl1.Text, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fstream)
                    Dim data As Byte() = br.ReadBytes(CInt(numbytes))
                    br.Close()
                    fstream.Close()
                    query.CommandText = "update db_document set suratdinas = @sd"
                    If Not data Is Nothing Then
                        query.Parameters.Clear()
                        query.Parameters.AddWithValue("@sd", data)
                    Else
                        query.Parameters.AddWithValue("@sd", "")
                    End If
                    query.ExecuteNonQuery()
                    MsgBox("Update succesful", MsgBoxStyle.Information)
                Else
                    MsgBox("Nothing to change", MsgBoxStyle.Exclamation)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class