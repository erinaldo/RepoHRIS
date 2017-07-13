Imports System.IO

Public Class Authorization
    Dim connectionstring As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection

    Public Sub New()
        InitializeComponent()
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
        connectionstring = "Server=" + host + "; User Id=" + id + "; Password=" + password + "; Database=" + db + ""
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Authorization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionstring
        SQLConnection.Open()
        grid()
    End Sub

    Sub grid()
        Dim newtab As New DataTable
        GridControl1.RefreshDataSource()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "select Username from db_user"
        Dim adapter As New MySqlDataAdapter(query.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(newtab)
        GridControl1.DataSource = newtab
    End Sub

    Sub clearance()
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit12.Checked = False
        CheckEdit17.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit20.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit1.Checked = False
        CheckEdit5.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit9.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        CheckEdit18.Checked = False
        CheckEdit42.Checked = False
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        CheckEdit48.Checked = False
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        clearance()
    End Sub

    Sub updation()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        query.CommandText = "update db_user set inputrec = @ir, modrec = @mr, deleterec = @dr, viewrec = @vr, viewprog = @vp, inputemp = @ie, modemp = @me, deleteemp = @de, leaveemp = @le" +
                                            ", terminateemp = @te, statuschemp = @se, inputborongan = @ib, inputovertime = @io, salaryadjust = @sa, inputloans = @il, closepayroll = @cp, leaveapp = @leavea, loansapp = @loansa" +
                                            ", terminateapp = @ta, statuschapp = @sca, otherapp = @op, warnapp = @wa, candidateslists = @cl, employeelists = @el, attendancelists = @al, overtimelists = @ol, warninglists = @wl, leavelists = @ll, otherlists = @othl, loanlists = @lol" +
                                            ", loansumlists = @lsl, payrolllists = @pl, premilists = @prel, latelists = @latel, absencelists = @absel, terminatelists = @tmtl, pajaklists = @pjkl, statuslists = @stal, kim3 = @kim3, kim4 = @kim4, pantailabu = @pantailabu, holidaylists = @holidaylists, warnemp = @wemp, otheremp = @oe,  inputholidays = @ih, sppdapp = @sppd, inputattendance = @inat, sppdlists = @sls where username = @uname"
        query.Parameters.AddWithValue("@uname", LabelControl1.Text)
        query.Parameters.AddWithValue("@ir", CheckEdit2.Checked)
        query.Parameters.AddWithValue("@mr", CheckEdit3.Checked)
        query.Parameters.AddWithValue("@dr", CheckEdit4.Checked)
        query.Parameters.AddWithValue("@vr", CheckEdit12.Checked)
        query.Parameters.AddWithValue("@vp", CheckEdit17.Checked)
        query.Parameters.AddWithValue("@ie", CheckEdit6.Checked)
        query.Parameters.AddWithValue("@me", CheckEdit7.Checked)
        query.Parameters.AddWithValue("@de", CheckEdit8.Checked)
        query.Parameters.AddWithValue("@le", CheckEdit20.Checked)
        query.Parameters.AddWithValue("@te", CheckEdit10.Checked)
        query.Parameters.AddWithValue("@se", CheckEdit11.Checked)
        query.Parameters.AddWithValue("@ib", CheckEdit1.Checked)
        query.Parameters.AddWithValue("@io", CheckEdit5.Checked)
        query.Parameters.AddWithValue("@sa", CheckEdit27.Checked)
        query.Parameters.AddWithValue("@il", CheckEdit28.Checked)
        query.Parameters.AddWithValue("@cp", CheckEdit29.Checked)
        query.Parameters.AddWithValue("@leavea", CheckEdit9.Checked)
        query.Parameters.AddWithValue("@loansa", CheckEdit13.Checked)
        query.Parameters.AddWithValue("@ta", CheckEdit14.Checked)
        query.Parameters.AddWithValue("@sca", CheckEdit15.Checked)
        query.Parameters.AddWithValue("@op", CheckEdit16.Checked)
        query.Parameters.AddWithValue("@wa", CheckEdit18.Checked)
        query.Parameters.AddWithValue("@cl", CheckEdit23.Checked)
        query.Parameters.AddWithValue("@el", CheckEdit24.Checked)
        query.Parameters.AddWithValue("@al", CheckEdit25.Checked)
        query.Parameters.AddWithValue("@ol", CheckEdit26.Checked)
        query.Parameters.AddWithValue("@wl", CheckEdit30.Checked)
        query.Parameters.AddWithValue("@ll", CheckEdit31.Checked)
        query.Parameters.AddWithValue("@othl", CheckEdit32.Checked)
        query.Parameters.AddWithValue("@lol", CheckEdit33.Checked)
        query.Parameters.AddWithValue("@lsl", CheckEdit34.Checked)
        query.Parameters.AddWithValue("@pl", CheckEdit35.Checked)
        query.Parameters.AddWithValue("@prel", CheckEdit36.Checked)
        query.Parameters.AddWithValue("@latel", CheckEdit37.Checked)
        query.Parameters.AddWithValue("@absel", CheckEdit38.Checked)
        query.Parameters.AddWithValue("@tmtl", CheckEdit39.Checked)
        query.Parameters.AddWithValue("@pjkl", CheckEdit40.Checked)
        query.Parameters.AddWithValue("@stal", CheckEdit41.Checked)
        query.Parameters.AddWithValue("@kim3", CheckEdit19.Checked)
        query.Parameters.AddWithValue("@kim4", CheckEdit21.Checked)
        query.Parameters.AddWithValue("@pantailabu", CheckEdit22.Checked)
        query.Parameters.AddWithValue("@holidaylists", CheckEdit42.Checked)
        query.Parameters.AddWithValue("@wemp", CheckEdit43.Checked)
        query.Parameters.AddWithValue("@oe", CheckEdit44.Checked)
        query.Parameters.AddWithValue("@ih", CheckEdit45.Checked)
        query.Parameters.AddWithValue("@sppd", CheckEdit46.Checked)
        query.Parameters.AddWithValue("@inat", CheckEdit47.Checked)
        query.Parameters.AddWithValue("@sls", CheckEdit48.Checked)
        query.ExecuteNonQuery()
        Dim mess2 As String
        mess2 = CType(MsgBox("Authorization Changed, The authorization will take effect after application restarted. You want to restart now ?", MsgBoxStyle.YesNo, "Information"), String)
        If CType(mess2, Global.Microsoft.VisualBasic.MsgBoxResult) = vbYes Then
            Login.Close()
            Application.Exit()
            Process.Start(Application.ExecutablePath)
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatbl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatbl.Clear()
        Dim param As String = ""
        Try
            param = "And Username='" + GridView1.GetFocusedRowCellValue("Username").ToString() + "'"
        Catch ex As Exception
        End Try
        Try
            sqlCommand.CommandText = "SELECT * FROM db_user WHERE 1 = 1 " + param.ToString()
            sqlCommand.Connection = SQLConnection
            Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
            Dim cb As New MySqlCommandBuilder(adapter)
            adapter.Fill(datatbl)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If datatbl.Rows.Count > 0 Then
            LabelControl1.Text = datatbl.Rows(0).Item(1).ToString
            CheckEdit2.Checked = CBool(datatbl.Rows(0).Item(4).ToString)
            CheckEdit3.Checked = CBool(datatbl.Rows(0).Item(5).ToString)
            CheckEdit4.Checked = CBool(datatbl.Rows(0).Item(6).ToString)
            CheckEdit12.Checked = CBool(datatbl.Rows(0).Item(7).ToString)
            CheckEdit17.Checked = CBool(datatbl.Rows(0).Item(8).ToString)
            CheckEdit6.Checked = CBool(datatbl.Rows(0).Item(9).ToString)
            CheckEdit7.Checked = CBool(datatbl.Rows(0).Item(10).ToString)
            CheckEdit8.Checked = CBool(datatbl.Rows(0).Item(11).ToString)
            CheckEdit20.Checked = CBool(datatbl.Rows(0).Item(12).ToString)
            CheckEdit10.Checked = CBool(datatbl.Rows(0).Item(13).ToString)
            CheckEdit11.Checked = CBool(datatbl.Rows(0).Item(14).ToString)
            CheckEdit1.Checked = CBool(datatbl.Rows(0).Item(15).ToString)
            CheckEdit5.Checked = CBool(datatbl.Rows(0).Item(16).ToString)
            CheckEdit27.Checked = CBool(datatbl.Rows(0).Item(17).ToString)
            CheckEdit28.Checked = CBool(datatbl.Rows(0).Item(18).ToString)
            CheckEdit29.Checked = CBool(datatbl.Rows(0).Item(19).ToString)
            CheckEdit9.Checked = CBool(datatbl.Rows(0).Item(20).ToString)
            CheckEdit13.Checked = CBool(datatbl.Rows(0).Item(21).ToString)
            CheckEdit14.Checked = CBool(datatbl.Rows(0).Item(22).ToString)
            CheckEdit15.Checked = CBool(datatbl.Rows(0).Item(23).ToString)
            CheckEdit16.Checked = CBool(datatbl.Rows(0).Item(24).ToString)
            CheckEdit18.Checked = CBool(datatbl.Rows(0).Item(25).ToString)
            CheckEdit23.Checked = CBool(datatbl.Rows(0).Item(28).ToString)
            CheckEdit24.Checked = CBool(datatbl.Rows(0).Item(29).ToString)
            CheckEdit25.Checked = CBool(datatbl.Rows(0).Item(30).ToString)
            CheckEdit26.Checked = CBool(datatbl.Rows(0).Item(31).ToString)
            CheckEdit30.Checked = CBool(datatbl.Rows(0).Item(32).ToString)
            CheckEdit31.Checked = CBool(datatbl.Rows(0).Item(33).ToString)
            CheckEdit32.Checked = CBool(datatbl.Rows(0).Item(34).ToString)
            CheckEdit33.Checked = CBool(datatbl.Rows(0).Item(35).ToString)
            CheckEdit34.Checked = CBool(datatbl.Rows(0).Item(36).ToString)
            CheckEdit35.Checked = CBool(datatbl.Rows(0).Item(37).ToString)
            CheckEdit36.Checked = CBool(datatbl.Rows(0).Item(38).ToString)
            CheckEdit37.Checked = CBool(datatbl.Rows(0).Item(39).ToString)
            CheckEdit38.Checked = CBool(datatbl.Rows(0).Item(40).ToString)
            CheckEdit39.Checked = CBool(datatbl.Rows(0).Item(41).ToString)
            CheckEdit40.Checked = CBool(datatbl.Rows(0).Item(42).ToString)
            CheckEdit41.Checked = CBool(datatbl.Rows(0).Item(43).ToString)
            CheckEdit19.Checked = CBool(datatbl.Rows(0).Item(44).ToString)
            CheckEdit21.Checked = CBool(datatbl.Rows(0).Item(45).ToString)
            CheckEdit22.Checked = CBool(datatbl.Rows(0).Item(46).ToString)
            CheckEdit42.Checked = CBool(datatbl.Rows(0).Item(47).ToString)
            CheckEdit43.Checked = CBool(datatbl.Rows(0).Item(48).ToString)
            CheckEdit44.Checked = CBool(datatbl.Rows(0).Item(26).ToString)
            CheckEdit45.Checked = CBool(datatbl.Rows(0).Item("inputholidays").ToString)
            CheckEdit46.Checked = CBool(datatbl.Rows(0).Item("sppdapp").ToString)
            CheckEdit47.Checked = CBool(datatbl.Rows(0).Item("inputattendance").ToString)
            CheckEdit48.Checked = CBool(datatbl.Rows(0).Item("sppdlists").ToString)
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If CheckEdit19.Checked = False And CheckEdit21.Checked = False And CheckEdit22.Checked = False Then
            MsgBox("Please select at least one office location")
        Else
            updation()
        End If
    End Sub
End Class