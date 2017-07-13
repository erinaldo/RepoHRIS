Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu

Public Class Addskill
    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            TextEdit1.Text = quer1.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Addskill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select name from db_tmpname"
            Dim quer1 As String = CType(query.ExecuteScalar, String)
            txtname.Text = quer1.ToString
            query.CommandText = "select employeecode from db_tmpname"
            Dim quer2 As String = CType(query.ExecuteScalar, String)
            txtidrecc.Text = quer2.ToString
        Catch ex As Exception
        End Try
    End Sub

    Dim sel As New selectemp

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Timer1.Start()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "truncate db_tmpname"
        query.ExecuteNonQuery()
        If sel Is Nothing OrElse sel.IsDisposed OrElse sel.MinimizeBox Then
            sel.Close()
            sel = selectemp
        End If
        sel.Show()
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        Timer1.Stop()
    End Sub

    Public Sub skills()
        Dim sqlCommand As New MySqlCommand
        Dim str_carSql As String
        Dim sk As MySqlCommand = SQLConnection.CreateCommand
        sk.CommandText = "select idrec from db_skills where idrec = '" & txtidrecc.Text & "'"
        Dim sk1 As String = CStr(sk.ExecuteScalar)
        If sk1 = "" Then
            If skill1.Text = "" OrElse skill2.Text = "" OrElse skill3.Text = "" OrElse skill4.Text = "" OrElse skill5.Text = "" OrElse txtidrecc.Text = "" Then
                MsgBox("Please fill the required fields")
            Else
                Try
                    str_carSql = "INSERT INTO db_skills " +
                           "(IdRec, FullName, Skill1, Skill2, Skill3, Skill4, Skill5, Interviewer) " +
                           "values (@Idrec, @fullname, @skill1, @skill2, @skill3, @skill4, @skill5, @interviewer)"
                    sqlCommand.Connection = SQLConnection
                    sqlCommand.CommandText = str_carSql
                    sqlCommand.Parameters.AddWithValue("@IdRec", txtidrecc.Text)
                    sqlCommand.Parameters.AddWithValue("@FullName", txtname.Text)
                    sqlCommand.Parameters.AddWithValue("@skill1", skill1.Text)
                    sqlCommand.Parameters.AddWithValue("@skill2", skill2.Text)
                    sqlCommand.Parameters.AddWithValue("@skill3", skill3.Text)
                    sqlCommand.Parameters.AddWithValue("@skill4", skill4.Text)
                    sqlCommand.Parameters.AddWithValue("@skill5", skill5.Text)
                    sqlCommand.Parameters.AddWithValue("@interviewer", TextEdit1.Text)
                    sqlCommand.ExecuteNonQuery()
                    MessageBox.Show("Data Succesfully Added!")
                    Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            MsgBox("The data already exists")
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        skills()
    End Sub
End Class