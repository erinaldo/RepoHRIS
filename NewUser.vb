Imports System.IO
Public Class NewUser
    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()

    Public conStr As String

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

    Private Sub NewUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
    End Sub

    Sub reg()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "insert into db_user (username, password, leveluser) values (@username, MD5(@password), @leveluser)"
        query.Parameters.AddWithValue("@username", TextEdit1.Text)
        query.Parameters.AddWithValue("@password", TextEdit2.Text)
        query.Parameters.AddWithValue("@leveluser", "1")
        query.ExecuteNonQuery()
        MsgBox("Registered!", MsgBoxStyle.Information)
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If TextEdit1.Text = "" OrElse TextEdit2.Text = "" Then
            MsgBox("Please fill the empty fields", MsgBoxStyle.Information)
        ElseIf TextEdit2.Text <> TextEdit3.Text Then
            MsgBox("Password not match!", MsgBoxStyle.Exclamation)
        Else
            Try
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "select count(username) from db_user where username = '" & TextEdit1.Text & "'"
                Dim quer As Integer = CInt(query.ExecuteScalar)
                If quer > 0 Then
                    MsgBox("Username already exist, Please use another one")
                Else
                    reg()
                    Close()
                End If
            Catch ex As Exception
                MsgBox("SignUp Part--> " & ex.Message)
            End Try
        End If
    End Sub
End Class