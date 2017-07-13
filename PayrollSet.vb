Imports System.IO

Public Class PayrollSet
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

    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable()
    Dim tbl_par As New DataTable

    Sub loaddata()
        Dim sqlcommand As New MySqlCommand
        sqlcommand.CommandText = "SELECT Bpjs, JamKecelakaanKerja, JaminanKesehatan, IuranPensiun, JaminanHariTua, BiayaJabatan, Lates, JaminanKematian, TK0, TK1, TK2, TK3, K0, K1, K2, K3, KI0, KI1, KI2 From db_setpayroll"
        sqlcommand.Connection = SQLConnection
        Dim adapter As New MySqlDataAdapter(sqlcommand.CommandText, SQLConnection)
        Dim cb As New MySqlCommandBuilder(adapter)
        adapter.Fill(tbl_par)
        For index As Integer = 0 To tbl_par.Rows.Count - 1
        Next
    End Sub

    Sub reset()
        txtnew.Text = ""
    End Sub

    Public Function InsertPer() As Boolean
        Dim sqlcommand As New MySqlCommand
        Dim str_carsql As String
        Try
            str_carsql = "Insert into db_setpayroll  " +
                        "(Bpjs, JamkecelakaanKerja, JaminanKesehatan, IuranPensiun, JaminanHariTua, BiayaJabatan, Lates, JaminanKematian )" +
                        "values (@Bpjs, @JamKecelakaanKerja, @JaminanKesehatan, @IuranPensiun, @JaminanHariTua, @BiayaJabatan, @Lates, @JaminanKematian)"
            sqlcommand.Connection = SQLConnection
            sqlcommand.CommandText = str_carsql
            sqlcommand.Parameters.AddWithValue("@Bpjs", "1")
            sqlcommand.Parameters.AddWithValue("@JamKecelakaanKerja", "1")
            sqlcommand.Parameters.AddWithValue("@JaminanKesehatan", "1")
            sqlcommand.Parameters.AddWithValue("@IuranPensiun", "1")
            sqlcommand.Parameters.AddWithValue("@JaminanHariTua", "1")
            sqlcommand.Parameters.AddWithValue("@BiayaJabatan", "1")
            sqlcommand.Parameters.AddWithValue("@Lates", "1")
            sqlcommand.Parameters.AddWithValue("@JaminanKematian", "1")
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MessageBox.Show("Data Succesfully Added!")
            Return True
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try
    End Function

    Public Sub updatebpjs()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " Bpjs = @Bpjs"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@Bpjs", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatejamkk()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " JamKecelakaanKerja = @JamKecelakaanKerja"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@JamKecelakaanKerja", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatejht2()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll Set" +
                                    " JaminanHariTua = @JaminanHariTua"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@JaminanHariTua", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatejamkes()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " JaminanKesehatan = @JaminanKesehatan"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@JaminanKesehatan", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updateiupe()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " IuranPensiun = @IuranPensiun"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@IuranPensiun", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatejht()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " JaminanHariTua = @JaminanHariTua"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@JaminanHariTua", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatebj()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " BiayaJabatan = @BiayaJabatan"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@BiayaJabatan", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatelates()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " Lates = @Lates"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@Lates", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatejamkem()
        Dim sqlcommand As New MySqlCommand
        Try
            sqlcommand.CommandText = "UPDATE db_setpayroll SET" +
                                    " JaminanKematian = @JaminanKematian"
            sqlcommand.Connection = SQLConnection
            sqlcommand.Parameters.AddWithValue("@JaminanKematian", txtnew.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
            MsgBox("Changed!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub updatetk0()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " TK0 = @tk0"
        cmmd.Parameters.AddWithValue("@tk0", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updatetk1()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                           " TK1 = @tk1"
        cmmd.Parameters.AddWithValue("@tk1", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updatetk2()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " TK2 = @tk2"
        cmmd.Parameters.AddWithValue("@tk2", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updatetk3()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " TK3 = @tk3"
        cmmd.Parameters.AddWithValue("@tk3", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updatek0()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " K0 = @k0"
        cmmd.Parameters.AddWithValue("@k0", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updatek1()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " K1 = @k1"
        cmmd.Parameters.AddWithValue("@K1", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updatek2()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                           " k2 = @k2"
        cmmd.Parameters.AddWithValue("@k2", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updatek3()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " K3 = @K3"
        cmmd.Parameters.AddWithValue("@K3", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updateki0()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " KI0 = @KI0"
        cmmd.Parameters.AddWithValue("@KI0", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updateki1()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " KI1 = @KI1"
        cmmd.Parameters.AddWithValue("@KI1", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Public Sub updateKi2()
        Dim cmmd As MySqlCommand = SQLConnection.CreateCommand
        cmmd.CommandText = "update db_setpayroll set" +
                            " KI2 = @KI2"
        cmmd.Parameters.AddWithValue("@KI2", txtnew.Text)
        cmmd.ExecuteNonQuery()
        MsgBox("Changed")
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If radiobpjs.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updatebpjs()
            End If
        ElseIf radiojkk.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updatejamkk()
            End If
        ElseIf radiojk.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updatejamkes()
            End If
        ElseIf radioiupe.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updateiupe()
            End If
        ElseIf radiojht.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updatejht()
            End If
        ElseIf radiobj.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updatebj()
            End If
        ElseIf radiolates.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updatelates()
            End If
        ElseIf radiojamkem.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value!")
            Else
                updatejamkem()
            End If
        ElseIf tk0.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatetk0()
            End If
        ElseIf tk1.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatetk1()
            End If
        ElseIf tk2.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatetk2()
            End If
        ElseIf tk3.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatetk3()
            End If
        ElseIf k0.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatek0()
            End If
        ElseIf k1.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatek1()
            End If
        ElseIf k2.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatek2()
            End If
        ElseIf k3.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updatek3()
            End If
        ElseIf ki0.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updateki0()
            End If
        ElseIf ki1.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updateki1()
            End If
        ElseIf ki2.Checked = True Then
            If txtnew.Text = "" Then
                MsgBox("Please input the new value")
            Else
                updateki2()
            End If
        End If
    End Sub

    Private Sub radiobpjs_CheckedChanged(sender As Object, e As EventArgs) Handles radiobpjs.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(0).ToString
        Next
    End Sub

    Private Sub PayrollSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        loaddata()
    End Sub

    Private Sub radiojkk_CheckedChanged(sender As Object, e As EventArgs) Handles radiojkk.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(1).ToString
        Next
        loaddata()
    End Sub

    Private Sub radiojk_CheckedChanged(sender As Object, e As EventArgs) Handles radiojk.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(2).ToString
        Next
        loaddata()
    End Sub

    Private Sub radioiupe_CheckedChanged(sender As Object, e As EventArgs) Handles radioiupe.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(3).ToString
        Next
        loaddata()
    End Sub

    Private Sub radiojht_CheckedChanged(sender As Object, e As EventArgs) Handles radiojht.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(4).ToString
        Next
        loaddata()
    End Sub

    Private Sub radiobj_CheckedChanged(sender As Object, e As EventArgs) Handles radiobj.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(5).ToString
        Next
        loaddata()
    End Sub

    Private Sub radiolates_CheckedChanged(sender As Object, e As EventArgs) Handles radiolates.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(6).ToString
        Next
        loaddata()
    End Sub

    Private Sub radiojamkem_CheckedChanged(sender As Object, e As EventArgs) Handles radiojamkem.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(7).ToString
        Next
        loaddata()
    End Sub

    Private Sub tk0_CheckedChanged(sender As Object, e As EventArgs) Handles tk0.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(8).ToString
        Next
        loaddata()
    End Sub

    Private Sub tk1_CheckedChanged(sender As Object, e As EventArgs) Handles tk1.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(9).ToString
        Next
        loaddata()
    End Sub

    Private Sub tk2_CheckedChanged(sender As Object, e As EventArgs) Handles tk2.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(10).ToString
        Next
        loaddata()
    End Sub

    Private Sub tk3_CheckedChanged(sender As Object, e As EventArgs) Handles tk3.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(11).ToString
        Next
        loaddata()
    End Sub

    Private Sub k0_CheckedChanged(sender As Object, e As EventArgs) Handles k0.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(12).ToString
        Next
    End Sub

    Private Sub k1_CheckedChanged(sender As Object, e As EventArgs) Handles k1.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(13).ToString
        Next
        loaddata()
    End Sub

    Private Sub k2_CheckedChanged(sender As Object, e As EventArgs) Handles k2.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(14).ToString
        Next
        loaddata()
    End Sub

    Private Sub k3_CheckedChanged(sender As Object, e As EventArgs) Handles k3.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(15).ToString
        Next
        loaddata()
    End Sub

    Private Sub ki0_CheckedChanged(sender As Object, e As EventArgs) Handles ki0.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(16).ToString
        Next
        loaddata()
    End Sub

    Private Sub ki1_CheckedChanged(sender As Object, e As EventArgs) Handles ki1.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(17).ToString
        Next
        loaddata()
    End Sub

    Private Sub ki2_CheckedChanged(sender As Object, e As EventArgs) Handles ki2.CheckedChanged
        reset()
        For index As Integer = 0 To tbl_par.Rows.Count - 1
            txtcurrent.Text = tbl_par.Rows(index).Item(18).ToString
        Next
        loaddata()
    End Sub
End Class