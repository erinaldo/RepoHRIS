Imports System.IO
Public Class ModAKS
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

    Private Sub ModAKS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        act = "input"
        'showtarget()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select status from db_tmpname"
        Dim quer As String = CStr(query.ExecuteScalar)
        TextEdit1.Text = quer.ToString
        TextEdit4.Text = ""
    End Sub

    Sub updation()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_calcbor set" +
                                " Nama = @nama" +
                                ", amount = @amount" +
                                ", disabled = @disabled" +
                                ", periodic = @periodic" +
                                " where tasks = @tasks"
            query.Parameters.AddWithValue("@tasks", TextEdit1.Text)
            query.Parameters.AddWithValue("@nama", TextEdit2.Text)
            query.Parameters.AddWithValue("@amount", TextEdit3.Text)
            query.Parameters.AddWithValue("@disabled", CheckEdit1.Checked)
            query.Parameters.AddWithValue("@Periodic", CheckEdit2.Checked)
            query.ExecuteNonQuery()
            MsgBox("Updation Succesful")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select Nama from db_calcbor where tasks = '" & TextEdit1.Text & "'"
            Dim quer1 As String = CStr(query.ExecuteScalar)
            TextEdit2.Text = quer1.ToString

            query.CommandText = "select amount from db_calcbor where tasks = '" & TextEdit1.Text & "'"
            Dim quer2 As String = CStr(query.ExecuteScalar)
            TextEdit3.Text = quer2.ToString

            query.CommandText = "select disabled from db_calcbor where tasks = '" & TextEdit1.Text & "'"
            Dim quer3 As Integer = CInt(query.ExecuteScalar)
            CheckEdit1.Checked = CBool(quer3.ToString)

            query.CommandText = "select periodic from db_calcbor where tasks = '" & TextEdit1.Text & "'"
            Dim quer4 As Integer = CInt(query.ExecuteScalar)
            CheckEdit2.Checked = CBool(quer4.ToString)

            query.CommandText = "select No, Goals as Target, Quantity, Code, Name from db_targetbor where code = '" & TextEdit1.Text & "'"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        updation()
    End Sub

    Sub showtarget()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select No, Goals as Target, Quantity, Code, Name from db_targetbor where code = '" & TextEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
    End Sub

    Dim act As String = ""

    Sub target()
        Dim lastm As String
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "select count(goals) from db_targetbor where code = '" & TextEdit1.Text & "'"
        Dim quer As Integer = CInt(cmmd.ExecuteScalar)
        If quer = 0 Then
            lastm = "Target 1"
        Else
            lastm = "Target " & quer + 1 & ""
        End If
        Try
            If act = "edit" Then
                cmmd.CommandText = "update db_targetbor set" +
                                    " Name = @Name" +
                                    ", Quantity = @Quantity" +
                                    " where no = @no"
            Else
                cmmd.CommandText = "insert into db_targetbor " +
                            "(Goals, Name, Quantity, Code) " +
                            "values (@Goals, @Name, @Quantity, @Code)"
            End If
            cmmd.Parameters.AddWithValue("@no", Label1.Text)
            cmmd.Parameters.AddWithValue("@Code", TextEdit1.Text)
            cmmd.Parameters.AddWithValue("@Goals", lastm)
            cmmd.Parameters.AddWithValue("@Quantity", TextEdit4.Text)
            cmmd.Parameters.AddWithValue("@Name", TextEdit2.Text)
            cmmd.ExecuteNonQuery()
            If act = "input" Then
                MsgBox("Data succesfully added")
            ElseIf act = "edit" Then
                MsgBox("Data changed")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        showtarget()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        Dim param As String = ""
        Try
            param = "and No='" + GridView1.GetFocusedRowCellValue("No").ToString() + "'"
        Catch ex As Exception
        End Try
        If param > "" Then
            act = "edit"
        Else
            act = "input"
        End If
        Try
            sqlCommand.CommandText = "SELECT * FROM db_targetbor WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatabl)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If datatabl.Rows.Count > -1 Then
            TextEdit4.Text = datatabl.Rows(0).Item(1).ToString
            Label1.Text = datatabl.Rows(0).Item(4).ToString
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        target()
        act = "input"
        TextEdit4.Text = ""
    End Sub

    Private Sub CheckEdit3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit3.CheckedChanged
        If CheckEdit3.Checked = True Then
            TextEdit4.Enabled = True
            SimpleButton1.Enabled = True
        Else
            TextEdit4.Enabled = False
            SimpleButton1.Enabled = False
        End If
    End Sub
End Class