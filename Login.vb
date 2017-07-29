Imports System.IO
Imports System.Security.Cryptography
Public Class Login

    Sub autoupdate()
        Do While True
            Dim client As New Net.WebClient
            Dim newversion As String = client.DownloadString("ftp://amos.makmur.id/updates/latestversion.txt")
            If newversion <> File.ReadAllText("program file location") Then
                For Each p As Process In Process.GetProcesses
                    If p.ProcessName = "vshosts32.exe" Then
                        p.Kill()
                    End If
                Next
                File.Delete("\\C:programfiles86\makmurgroup")
                client.DownloadFile("http://blbbal/com", "programfile")
                client.Dispose()
            End If
            Threading.Thread.Sleep(300000)
        Loop
    End Sub

    Sub visiting() 'store last visit to track version of HRIS    
        Dim hostname As String = Net.Dns.GetHostName
        Dim ipadd As String = Net.Dns.GetHostEntry(hostname).AddressList(0).ToString
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        With query
            .CommandText = "update db_user set lastvisit = ?, hostname = ?, ipaddress = ?, hrversion = '10.0.12' where username = ?"
            .Parameters.AddWithValue("lastvisit", Date.Now)
            .Parameters.AddWithValue("hostname", hostname)
            .Parameters.AddWithValue("ipaddress", ipadd)
            .Parameters.AddWithValue("username", teUsername.Text)
            .ExecuteNonQuery()
        End With
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AutomaticUpdater1.Show()
        CheckForUpdatesToolStripMenuItem.PerformClick()
        '//Uncomment below line to see Russian version

        '  //AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");

        '  //If you want to open download page when user click on download button uncomment below line.

        '    AutoUpdater.OpenDownloadPage = True

        '    '  //Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

        '    AutoUpdater.LetUserSelectRemindLater = False
        '    AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days
        '    AutoUpdater.RemindLaterAt = 2
        '    AutoUpdater.Start("http://rbsoft.org/updates/right-click-enhancer.xml")
        ' AutoUpdater.Start("ftp://amos:t4m4g0@ftp1.makmurgroup.id/repo/hris/%file%")
    End Sub

    'Public Sub CheckForUpdates()
    '    Try
    '        Timer1.Enabled = False
    '        ProgressBar1.Visible = True
    '        WebBrowser1.Visible = False
    '        Dim request As Net.HttpWebRequest = CType(Net.WebRequest.Create("https://dl.dropbox.com/s/f3hbcpzffkdg5y0/version.txt?dl=0"), Net.HttpWebRequest)
    '        Dim response As Net.HttpWebResponse = CType(request.GetResponse(), Net.HttpWebResponse)
    '        Dim sr As StreamReader = New StreamReader(response.GetResponseStream())
    '        Dim newestversion As String = sr.ReadToEnd()
    '        Dim currentversion As String = Application.ProductVersion
    '        If newestversion.Contains(currentversion) Then
    '            Timer1.Enabled = True
    '        Else
    '            Dim mess As String
    '            mess = CType(MsgBox("There's a new update, Update ?", MsgBoxStyle.YesNo, "Information"), String)
    '            If CType(mess, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
    '                WebBrowser1.Navigate("https://dl.dropbox.com/s/axtsp9oq8pez6u6/HRIS.exe?dl=0")
    '            Else
    '                Timer1.Enabled = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Public Function Hash512(password As String, salt As String) As String
        Dim convertedToBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(password & salt)
        Dim hashType As HashAlgorithm = New SHA512Managed()
        Dim hashBytes As Byte() = hashType.ComputeHash(convertedToBytes)
        Dim hashedResult As String = Convert.ToBase64String(hashBytes)
        Return hashedResult
    End Function

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        If teUsername.Text = "" OrElse tePassword.Text = "" Then
            MsgBox("Username and pasword can't be empty", MsgBoxStyle.Exclamation)
        Else
            SQLConnection.Close()
            SQLConnection.ConnectionString = CONSTRING
            If SQLConnection.State = ConnectionState.Closed Then
                SQLConnection.Open()
            End If
            Dim m5 As New MD5CryptoServiceProvider
            Dim bytestring() As Byte = System.Text.Encoding.ASCII.GetBytes(tePassword.Text)
            bytestring = m5.ComputeHash(bytestring)
            Dim finalstring As String = Nothing
            For Each bt As Byte In bytestring
                finalstring &= bt.ToString("x2")
            Next
            Try
                Dim tbl_par As New DataTable
                Dim sqlCommand As New MySqlCommand
                sqlCommand.CommandText = "Select username, password FROM db_user WHERE Binary username ='" + teUsername.Text + "' and Binary password ='" + finalstring + "'"
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tbl_par)
                If tbl_par.Rows.Count > 0 Then
                    ProgressBar1.Visible = True
                    Timer1.Enabled = True
                Else
                    MsgBox("Username and password didn't match", MsgBoxStyle.Critical)
                    teUsername.Enabled = True
                    tePassword.Enabled = True
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Sub tePassword_KeyDown(sender As Object, e As KeyEventArgs) Handles tePassword.KeyDown
        Dim m5 As New MD5CryptoServiceProvider
        Dim bytestring() As Byte = System.Text.Encoding.ASCII.GetBytes(tePassword.Text)
        bytestring = m5.ComputeHash(bytestring)
        Dim finalstring As String = Nothing
        For Each bt As Byte In bytestring
            finalstring &= bt.ToString("x2")
        Next
        If e.KeyCode = Keys.Enter Then
            If teUsername.Text = "" OrElse tePassword.Text = "" Then
                MsgBox("Username and password can't be empty", MsgBoxStyle.Exclamation)
            Else
                SQLConnection.Close()
                SQLConnection.ConnectionString = CONSTRING
                If SQLConnection.State = ConnectionState.Closed Then
                    SQLConnection.Open()
                End If
                Try
                    Dim tbl_par As New DataTable
                    Dim sqlCommand As New MySqlCommand
                    sqlCommand.CommandText = "Select username, password FROM db_user WHERE BINARY username ='" + teUsername.Text + "' and BINARY password='" + finalstring + "'"
                    sqlCommand.Connection = SQLConnection
                    Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                    Dim cb As New MySqlCommandBuilder(adapter)
                    adapter.Fill(tbl_par)
                    If tbl_par.Rows.Count > 0 Then
                        ProgressBar1.Visible = True
                        Timer1.Enabled = True
                    Else
                        MsgBox("Username and password didn't match", MsgBoxStyle.Critical)
                        teUsername.Enabled = True
                        tePassword.Enabled = True
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        SQLConnection.Close()
        SQLConnection.ConnectionString = CONSTRING
        If SQLConnection.State = ConnectionState.Closed Then
            SQLConnection.Open()
        End If
        Dim m5 As New MD5CryptoServiceProvider
        Dim bytestring() As Byte = System.Text.Encoding.ASCII.GetBytes(tePassword.Text)
        bytestring = m5.ComputeHash(bytestring)
        Dim finalstring As String = Nothing
        For Each bt As Byte In bytestring
            finalstring &= bt.ToString("x2")
        Next
        Try
            teUsername.Enabled = False
            tePassword.Enabled = False
            If ProgressBar1.Value < 100 Then
                ProgressBar1.Value += 10
            ElseIf ProgressBar1.Value = 100 Then
                Timer1.Stop()
                Dim tbl_par As New DataTable
                Dim sqlCommand As New MySqlCommand
                sqlCommand.CommandText = "Select username, password FROM db_user WHERE BINARY username ='" + teUsername.Text + "' and BINARY password='" + finalstring + "'"
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(tbl_par)
                If tbl_par.Rows.Count > 0 Then
                    visiting()
                    With Tiles
                        .TextBox1.Text = teUsername.Text
                        .Show()
                    End With
                    Hide()
                Else
                    MsgBox("Username and password didn't match", MsgBoxStyle.Critical)
                    teUsername.Enabled = True
                    tePassword.Enabled = True
                End If
                Timer1.Stop()
            End If
            If ProgressBar1.Value = 10 Then
                Label1.Text = "Preparing"
            ElseIf ProgressBar1.Value = 50 Then
                Label1.Text = "Initializing"
                With MainClone
                    .LabelControl2.Text = teUsername.Text
                    .Show()
                    .Hide()
                End With
            ElseIf ProgressBar1.Value = 60 Then
                'CheckForUpdates()                
            ElseIf ProgressBar1.Value = 96 Then
                Label1.Text = "Ready"
            ElseIf ProgressBar1.Value = 97 Then
                Label1.Text = "Ready."
            ElseIf ProgressBar1.Value = 98 Then
                Label1.Text = "Ready.."
            ElseIf ProgressBar1.Value = 99 Then
                Label1.Text = "Ready..."
            ElseIf ProgressBar1.Value = 100 Then
                Label1.Text = "Ready...."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub AutomaticUpdater1_ClosingAborted(sender As Object, e As EventArgs) Handles AutomaticUpdater1.ClosingAborted
        'your app was preparing to close
        ' however the update wasn't ready so your app is going to show itself
        'LoadFilesEtc()  
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub teUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles teUsername.KeyDown
        Dim m5 As New MD5CryptoServiceProvider
        Dim bytestring() As Byte = System.Text.Encoding.ASCII.GetBytes(tePassword.Text)
        bytestring = m5.ComputeHash(bytestring)
        Dim finalstring As String = Nothing
        For Each bt As Byte In bytestring
            finalstring &= bt.ToString("x2")
        Next
        If e.KeyCode = Keys.Enter Then
            If teUsername.Text = "" OrElse tePassword.Text = "" Then
                MsgBox("Username and password can't be empty", MsgBoxStyle.Exclamation)
            Else
                SQLConnection.Close()
                SQLConnection.ConnectionString = CONSTRING
                If SQLConnection.State = ConnectionState.Closed Then
                    SQLConnection.Open()
                End If
                Try
                    Dim tbl_par As New DataTable
                    Dim sqlCommand As New MySqlCommand
                    sqlCommand.CommandText = "Select username, password FROM db_user WHERE BINARY username ='" + teUsername.Text + "' and BINARY password='" + finalstring + "'"
                    sqlCommand.Connection = SQLConnection
                    Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                    Dim cb As New MySqlCommandBuilder(adapter)
                    adapter.Fill(tbl_par)
                    If tbl_par.Rows.Count > 0 Then
                        ProgressBar1.Visible = True
                        Timer1.Enabled = True
                    Else
                        MsgBox("Username and password didn't match", MsgBoxStyle.Critical)
                        teUsername.Enabled = True
                        tePassword.Enabled = True
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try
            End If
        End If
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Close()
        Application.Exit()
    End Sub

    Private Sub Login_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        AutomaticUpdater1.Show()
        CheckForUpdatesToolStripMenuItem.PerformClick()
    End Sub

    Private Sub Login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        StartPages.Close()
    End Sub
End Class