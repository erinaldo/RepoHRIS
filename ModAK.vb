Imports System.IO
Public Class ModAK
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

    Private Sub CheckEdit3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit3.CheckedChanged
        If CheckEdit3.Checked = True Then
            TextEdit4.Enabled = True
            SimpleButton1.Enabled = True
        Else
            TextEdit4.Enabled = False
            SimpleButton1.Enabled = False
        End If
    End Sub

    Private Sub ModAK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        GridView1.BestFitColumns()
    End Sub

    Sub clear()
        TextEdit1.Text = ""
        TextEdit2.Text = ""
        TextEdit3.Text = ""
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
    End Sub

    Sub insertion2()
        Try
            Dim lastm As String
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(goals) from db_targetbor where code = '" & TextEdit1.Text & "'"
            Dim quer As Integer = CInt(query.ExecuteScalar)
            If quer = 0 Then
                lastm = "Target 1"
            Else
                lastm = "Target " & quer + 1 & ""
            End If
            query.CommandText = "insert into db_targetbor" +
                                "(Goals, Quantity, Code, Name)" +
                                " values(@goals, @quantity, @code, @name)"
            query.Parameters.Clear()
            query.Parameters.AddWithValue("@goals", lastm)
            query.Parameters.AddWithValue("@quantity", TextEdit4.Text)
            query.Parameters.AddWithValue("@code", TextEdit1.Text)
            query.Parameters.AddWithValue("@name", TextEdit2.Text)
            query.ExecuteNonQuery()
            MsgBox("Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub insertion()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(tasks) from db_calcbor where tasks = '" & TextEdit1.Text & "'"
            Dim quer As Integer = CInt(query.ExecuteScalar)
            If quer = 0 Then
                query.CommandText = "insert into db_calcbor" +
                            "(tasks, Nama, Amount, Disabled, Periodic)" +
                            " values (@tasks, @Nama, @Amount, @Disabled, @Periodic)"
                query.Parameters.AddWithValue("@tasks", TextEdit1.Text)
                query.Parameters.AddWithValue("@nama", TextEdit2.Text)
                query.Parameters.AddWithValue("@Amount", TextEdit3.Text)
                query.Parameters.AddWithValue("@Disabled", CheckEdit1.Checked)
                query.Parameters.AddWithValue("@Periodic", CheckEdit2.Checked)
                query.ExecuteNonQuery()
                MsgBox("Added")
                ' clear()
                CheckEdit3.Enabled = True
            Else
                MsgBox("There's already exist a tasks with a same name")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If TextEdit1.Text = "" OrElse TextEdit2.Text = "" Then
            MsgBox("Please fill the empty fields")
        Else
            insertion()
        End If
    End Sub

    Sub showtarget()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select * from db_targetbor where code = '" & TextEdit1.Text & "'"
        Dim dt As New DataTable
        dt.Load(query.ExecuteReader)
        GridControl1.DataSource = dt
        GridView1.BestFitColumns()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If TextEdit4.Text = "" Then
            MsgBox("Fill the empty fields")
        Else
            insertion2()
            showtarget()
        End If
    End Sub
End Class