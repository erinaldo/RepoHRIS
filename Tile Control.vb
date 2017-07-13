Imports System.IO
Imports DevExpress.XtraBars.Alerter

Public Class Tile_Control
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

    Dim main As MainApp

    Private Sub TileItem1_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem1.ItemClick
        If main Is Nothing OrElse main.IsDisposed OrElse main.MinimizeBox Then
            main = New MainApp
        End If
        main.Show()
        Close()
    End Sub

    Public Sub MakeSeeThru()
        Me.Opacity = 0
    End Sub

    Private Sub TileItem5_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem5.ItemClick
        If TileItem5.Text = "Main Application" Then
            MainClone.Close()
            With MainClone
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
        ElseIf TileItem5.Text = "Reports" Then
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem5.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        End If
    End Sub

    Private Sub TileItem6_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem6.ItemClick
        Dim pesan As String
        pesan = CType(MsgBox("Log Off Application?", MsgBoxStyle.YesNo, "Warning"), String)
        If CType(pesan, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Try
                Dim del As MySqlCommand = SQLConnection.CreateCommand
                del.CommandText = "truncate db_hasil"
                del.ExecuteNonQuery()
                Dim dele As MySqlCommand = SQLConnection.CreateCommand
                dele.CommandText = "truncate db_temp"
                dele.ExecuteNonQuery()
                dele.CommandText = "truncate db_tmpname"
                dele.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("tileitem6 " & ex.Message)
            End Try
            SQLConnection.Close()
            Close()
            Login.Close()
            Application.Exit()
        End If
    End Sub

    Private Sub TileItem2_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem2.ItemClick
        MainApp.Show()
        Close()
    End Sub

    Dim loan As New Payments

    Private Sub TileItem3_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem3.ItemClick
        If loan Is Nothing OrElse loan.IsDisposed OrElse loan.MinimizeBox Then
            loan.Close()
            loan = New Payments
        End If
        loan.Show()
        Close()
    End Sub

    Dim att As New Attendances

    Private Sub TileItem4_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem4.ItemClick
        If att Is Nothing OrElse att.IsDisposed OrElse att.MinimizeBox Then
            att.Close()
            att = New Attendances
        End If
        att.Show()
        Close()
    End Sub

    Dim rep As New Reports

    Private Sub TileItem7_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs)
        If rep Is Nothing OrElse rep.IsDisposed OrElse rep.MinimizeBox Then
            rep.Close()
            rep = New Reports
        End If
        rep.Show()
        Close()
    End Sub

    Sub notif()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        With query
            .CommandText = "select status from db_recruitment where status = 'In Progress'"
            Dim rec As String = CStr(query.ExecuteScalar)
            .CommandText = "select status from db_recruitment where status = 'Accept'"
            query.CommandText = "insert into db_notification (notifyRec, NotifyEmp, Notifypay, NotifyMod) values (@noterec, @notifyemp, @notifypay, @notifymod)"
            query.Parameters.AddWithValue("@noterec", rec & "In progress Status")
        End With
    End Sub

    Private Sub Tile_Control_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Azure
        Me.TransparencyKey = Color.Azure
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select leveluser from db_user where username = '" & TextBox1.Text & "'"
            Dim quers As String = CStr(query.ExecuteScalar)
            If quers = "5" Then
                TileItem14.Visible = True
            Else
                TileItem14.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Tile_Control_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub

    Dim cus As New Customize

    Private Sub TileItem8_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem8.ItemClick
        If TileItem8.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        ElseIf TileItem8.Text = "Reports" Then
            'Reports.Close()
            'With Reports
            '    .Label3.Text = TextBox1.Text
            '    .Show()
            'End With
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem8.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        End If
    End Sub

    Dim asp As New AspekKerja

    Private Sub TileItem9_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem9.ItemClick
        If TileItem9.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        ElseIf TileItem9.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        ElseIf TileItem9.Text = "Reports" Then
            'Reports.Close()
            'With Reports
            '    .Label3.Text = TextBox1.Text
            '    .Show()
            'End With
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        End If
    End Sub

    Dim reps As New Reports

    Private Sub TileItem10_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem10.ItemClick
        If TileItem10.Text = "Reports" Then
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem10.Text = "Customize" Then
            If cus Is Nothing OrElse cus.IsDisposed OrElse cus.MinimizeBox Then
                cus.Close()
                cus = New Customize
            End If
            cus.Show()
        ElseIf TileItem10.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        End If
    End Sub

    Private Sub TileItem11_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem11.ItemClick
        If TileItem11.Text = "Approvement" Then
            Approvement.Close()
            With Approvement
                .Label2.Text = TextBox1.Text
                .Show()
            End With
        ElseIf TileItem11.Text = "Aspek Kerja" Then
            If asp Is Nothing OrElse asp.IsDisposed OrElse asp.MinimizeBox Then
                asp.Close()
                asp = New AspekKerja
            End If
            asp.Show()
        ElseIf TileItem11.Text = "Reports" Then
            'Reports.Close()
            'With Reports
            '    .Label3.Text = TextBox1.Text
            '    .Show()
            'End With
            Form2.Close()
            With Form2
                .Label3.Text = TextBox1.Text
                .Show()
            End With
        End If
    End Sub

    Private Sub AlertControl1_AlertClick(sender As Object, e As DevExpress.XtraBars.Alerter.AlertClickEventArgs)

    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Function GetImage1() As Image
        Return ImageCollection1.Images(1)
    End Function

    Private Function GetImage2() As Image
        Return ImageCollection1.Images(3)
    End Function

    Private Function GetImage3() As Image
        Return ImageCollection1.Images(4)
    End Function

    Sub alert()
        Dim btn1 As AlertButton = New AlertButton(GetImage)
        Dim btn2 As AlertButton = New AlertButton(GetImage2)
        Dim btn3 As AlertButton = New AlertButton(GetImage3)
        btn3.Hint = "Erase Notification"
        btn3.Name = "btnerase"
        btn1.Hint = "View Progress"
        btn1.Name = "btnProg"
        btn2.Hint = "View Candidates"
        btn2.Name = "btnview"
        AlertControl1.Buttons.Add(btn1)
        AlertControl1.Buttons.Add(btn2)
        AlertControl1.Buttons.Add(btn3)
        AddHandler AlertControl1.ButtonClick, AddressOf AlertControl1_ButtonClick
        AddHandler AlertControl1.ButtonDownChanged,
        AddressOf AlertControl1_ButtonDownChanged
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "select count(status) from db_recruitment where status = 'In progress'"
            Dim quer2 As Integer = CInt(query.ExecuteScalar)
            query.CommandText = "select count(interviewdate) from db_recruitment where interviewdate = @d1"
            query.Parameters.AddWithValue("@d1", Date.Now)
            Dim quer1 As Integer = CInt(query.ExecuteScalar)
            query.CommandText = "select notifyrec from db_notification"
            Dim note As String = CStr(query.ExecuteScalar)
            query.CommandText = "select count(*) from db_notification"
            Dim hslnote As Integer = CInt(query.ExecuteScalar)
            If hslnote > 1 Then
                AlertControl1.Show(Me, "Notifications", "<b><color=green> " & TextBox1.Text & " you have " & note.ToString & " </color></b>")
            Else
                AlertControl1.Show(Me, "Notifications", "<color=green>" & TextBox1.Text & "</color><br>")
            End If
            'If quer2 > 1 Then
            '    AlertControl1.Show(Me, "Notifications", "<b><color=green> " & TextBox1.Text & " you have " & note.ToString & " </color></b>")
            '    ' AlertControl1.Show(Me, "Notifications", "<b><color=green> " & TextBox1.Text & " you have " & quer2.ToString & " 'In Progress' Status<color=green> and " & quer1.ToString & " interviews scheduled for today </color></b>")
            'Else
            '    AlertControl1.Show(Me, "Notifications", "<color=green>" & TextBox1.Text & "</color><br>")
            'End If
        Catch ex As Exception
            MsgBox("Alert" & ex.Message)
        End Try
    End Sub

    Private Sub AlertControl1_BeforeFormShow(sender As Object, e As AlertFormEventArgs)
        e.AlertForm.OpacityLevel = 1
    End Sub

    Dim prog As New RecProcess

    Private Sub AlertControl1_ButtonClick(sender As Object, e As AlertButtonClickEventArgs) Handles AlertControl1.ButtonClick
        If e.ButtonName = "btnProg" Then
            With Me
                .TextBox1.Text = TextBox1.Text
                .Show()
            End With
        ElseIf e.ButtonName = "btnerase" Then
            With Me
                Dim query As MySqlCommand = SQLConnection.CreateCommand
                query.CommandText = "delete from db_notification where indexs = '1'"
                query.ExecuteNonQuery()
                query.CommandText = "select notify from db_notification"
                Dim note As String = CStr(query.ExecuteScalar)
                'AlertControl1.Show(Me, "Notifications", "<b><color=green> " & TextBox1.Text & " you have " & note.ToString & " </color></b>")
            End With
        End If
    End Sub

    Private Sub AlertControl1_ButtonDownChanged(sender As Object, e As AlertButtonDownChangedEventArgs) Handles AlertControl1.ButtonDownChanged

    End Sub

    Dim us As New NewUser

    Private Sub TileItem13_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem13.ItemClick
        If us Is Nothing OrElse us.IsDisposed OrElse us.MinimizeBox Then
            us.Close()
            us = New NewUser
        End If
        us.Show()
    End Sub

    Private Sub AlertControl1_AlertClick_1(sender As Object, e As AlertClickEventArgs) Handles AlertControl1.AlertClick
        'MsgBox("test3")
    End Sub

    Dim auth As New Authorization

    Private Sub TileItem14_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileItem14.ItemClick
        If auth Is Nothing OrElse auth.IsDisposed OrElse auth.MinimizeBox Then
            auth.Close()
            auth = New Authorization
        End If
        auth.Show()
    End Sub

    Private Sub TileItem15_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs)

    End Sub
End Class
