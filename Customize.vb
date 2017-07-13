Imports System.IO
Imports DevExpress.Utils.Menu

Public Class Customize

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

    Private Sub Customize_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim datatabl As New DataTable
        Dim sqlCommand As New MySqlCommand
        datatabl.Clear()
        If ComboBox1.Text = "Job Title" Then
            Dim param As String = ""
            Try
                param = "And JobTitle='" + GridView1.GetFocusedRowCellValue("JobTitle").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT * FROM db_jobtitle WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            If datatabl.Rows.Count > 0 Then
                SimpleButton3.Text = datatabl.Rows(0).Item(0).ToString()
                TextEdit1.Text = datatabl.Rows(0).Item(0).ToString
                CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(1).ToString)
            End If
        ElseIf ComboBox1.Text = "Company Code" Then
            Dim param As String = ""
            Try
                param = "And CompanyCode='" + GridView1.GetFocusedRowCellValue("CompanyCode").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT * FROM db_companycode WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            If datatabl.Rows.Count > 0 Then
                SimpleButton3.Text = datatabl.Rows(0).Item(0).ToString()
                TextEdit1.Text = datatabl.Rows(0).Item(0).ToString
                CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(1).ToString)
            End If
        ElseIf ComboBox1.Text = "Office Location" Then
            Dim param As String = ""
            Try
                param = "And OfficeLocation='" + GridView1.GetFocusedRowCellValue("OfficeLocation").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT * FROM db_officelocation WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            If datatabl.Rows.Count > 0 Then
                SimpleButton3.Text = datatabl.Rows(0).Item(0).ToString()
                TextEdit1.Text = datatabl.Rows(0).Item(0).ToString
                CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(1).ToString)
            End If
        ElseIf ComboBox1.Text = "Group" Then
            Dim param As String = ""
            Try
                param = "And GroupName='" + GridView1.GetFocusedRowCellValue("GroupName").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT * FROM db_groupmbp WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            If datatabl.Rows.Count > 0 Then
                TextEdit1.Text = datatabl.Rows(0).Item(0).ToString
                CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(1).ToString)
                SimpleButton3.Text = datatabl.Rows(0).Item(0).ToString()
            End If
        ElseIf ComboBox1.Text = "Department" Then
            Dim param As String = ""
            Try
                param = "And DepartmentName='" + GridView1.GetFocusedRowCellValue("DepartmentName").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT * FROM db_departmentmbp WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            If datatabl.Rows.Count > 0 Then
                TextEdit1.Text = datatabl.Rows(0).Item(0).ToString
                CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(1).ToString)
                SimpleButton3.Text = datatabl.Rows(0).Item(0).ToString()
            End If
        ElseIf ComboBox1.Text = "Type Of Offense" Then
            Dim param As String = ""
            Try
                param = "And TypeOfOffense='" + GridView1.GetFocusedRowCellValue("TypeOfOffense").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT * FROM db_offense WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            If datatabl.Rows.Count > 0 Then
                TextEdit1.Text = datatabl.Rows(0).Item(0).ToString
                CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(1).ToString)
                SimpleButton3.Text = datatabl.Rows(0).Item(0).ToString()
            End If
        ElseIf ComboBox1.Text = "Machines" Then
            Dim param As String = ""
            Try
                param = "And Machine='" + GridView1.GetFocusedRowCellValue("Machine").ToString() + "'"
            Catch ex As Exception
            End Try
            Try
                sqlCommand.CommandText = "SELECT * FROM db_machines WHERE 1 = 1 " + param.ToString()
                sqlCommand.Connection = SQLConnection
                Dim adapter As New MySqlDataAdapter(sqlCommand.CommandText, SQLConnection)
                Dim cb As New MySqlCommandBuilder(adapter)
                adapter.Fill(datatabl)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            If datatabl.Rows.Count > 0 Then
                TextEdit1.Text = datatabl.Rows(0).Item(0).ToString
                CheckEdit1.Checked = CBool(datatabl.Rows(0).Item(1).ToString)
                SimpleButton3.Text = datatabl.Rows(0).Item(0).ToString()
            End If
        End If
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Sub insertion()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        If ComboBox1.Text = "Job Title" Then
            query.CommandText = "insert into db_jobtitle (jobtitle, isenable) values (@jobs, @isenable)"
            query.Parameters.AddWithValue("@jobs", TextEdit1.Text)
            query.Parameters.AddWithValue("@isenable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            TextEdit1.Text = ""
            changes()
        ElseIf ComboBox1.Text = "Company Code" Then
            query.CommandText = "insert into db_companycode (companycode, isenable) values (@comp, @isenable)"
            query.Parameters.AddWithValue("@comp", TextEdit1.Text)
            query.Parameters.AddWithValue("@isenable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            TextEdit1.Text = ""
            changes()
        ElseIf ComboBox1.Text = "Office Location" Then
            query.CommandText = "insert into db_officelocation (OfficeLocation, isenable) values (@offloc, @isenable)"
            query.Parameters.AddWithValue("@offloc", TextEdit1.Text)
            query.Parameters.AddWithValue("@isenable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            TextEdit1.Text = ""
            changes()
        ElseIf ComboBox1.Text = "Group" Then
            query.CommandText = "insert into db_groupmbp (groupname, isenable) values (@groupname, @isenable)"
            query.Parameters.AddWithValue("@groupname", TextEdit1.Text)
            query.Parameters.AddWithValue("@isenable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            TextEdit1.Text = ""
            changes()
        ElseIf ComboBox1.Text = "Department" Then
            query.CommandText = "insert into db_departmentmbp (departmentname, isenable) values (@departmentname, @isenable)"
            query.Parameters.AddWithValue("@departmentname", TextEdit1.Text)
            query.Parameters.AddWithValue("@isenable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            TextEdit1.Text = ""
            changes()
        ElseIf ComboBox1.Text = "Type Of Offense" Then
            query.CommandText = "insert into db_offense (typeofoffense, isenable) values (@offensetype, @isenable)"
            query.Parameters.AddWithValue("@offensetype", TextEdit1.Text)
            query.Parameters.AddWithValue("@isenable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            TextEdit1.Text = ""
            changes()
        ElseIf ComboBox1.Text = "Machines" Then
            query.CommandText = "insert into db_machines(machine, isenable) values (@machine, @isenable)"
            query.Parameters.AddWithValue("@machine", TextEdit1.Text)
            query.Parameters.AddWithValue("@isenable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            TextEdit1.Text = ""
            changes()
        End If
    End Sub

    Sub changes()
        GridView1.Columns.Clear()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        If ComboBox1.Text = "Job Title" Then
            Documents.Close()
            query.CommandText = "select * from db_jobtitle"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
            GridView1.BestFitColumns()
            GridView1.MoveLastVisible()
        ElseIf ComboBox1.Text = "Company Code" Then
            Documents.Close()
            query.CommandText = "select * from db_companycode"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
            GridView1.BestFitColumns()
            GridView1.MoveLastVisible()
        ElseIf ComboBox1.Text = "Office Location" Then
            Documents.Close()
            query.CommandText = "select * from db_officelocation"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
            GridView1.BestFitColumns()
            GridView1.MoveLastVisible()
        ElseIf ComboBox1.Text = "Group" Then
            Documents.Close()
            query.CommandText = "select * from db_groupmbp"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
            GridView1.BestFitColumns()
            GridView1.MoveLastVisible()
        ElseIf ComboBox1.Text = "Department" Then
            Documents.Close()
            query.CommandText = "select * from db_departmentmbp"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
            GridView1.BestFitColumns()
            GridView1.MoveLastVisible()
        ElseIf ComboBox1.Text = "Type Of Offense" Then
            Documents.Close()
            query.CommandText = "select * from db_offense"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
            GridView1.BestFitColumns()
            GridView1.MoveLastVisible()
        ElseIf ComboBox1.Text = "Machines" Then
            Documents.Close()
            query.CommandText = "select * from db_machines"
            Dim dt As New DataTable
            dt.Load(query.ExecuteReader)
            GridControl1.DataSource = dt
            GridView1.BestFitColumns()
            GridView1.MoveLastVisible()
        ElseIf ComboBox1.Text = "Documents" Then
            With Documents
                .Show()
            End With
            'query.CommandText = "select * from db_document"
            'Dim dt As New DataTable
            'dt.Load(query.ExecuteReader)
            'GridControl1.DataSource = dt
            'GridView1.BestFitColumns()
            'GridView1.MoveLastVisible()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        changes()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If ComboBox1.Text = "" OrElse TextEdit1.Text = "" Then
            MsgBox("The data can't be empty")
        Else
            insertion()
        End If
    End Sub

    Sub changing()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        If ComboBox1.Text = "Job Title" Then
            query.CommandText = "update db_jobtitle set jobtitle = @job, isenable = @enable where jobtitle = '" & TextEdit1.Text & "'"
            query.Parameters.AddWithValue("@job", TextEdit1.Text)
            query.Parameters.AddWithValue("@enable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Company Code" Then
            query.CommandText = "update db_companycode set companycode = @cc, isenable = @enable where companycode = '" & TextEdit1.Text & "'"
            query.Parameters.AddWithValue("@cc", TextEdit1.Text)
            query.Parameters.AddWithValue("@enable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Office Location" Then
            query.CommandText = "update db_officelocation set officelocation = @ol, isenable = @enable where officelocation = '" & TextEdit1.Text & "'"
            query.Parameters.AddWithValue("@ol", TextEdit1.Text)
            query.Parameters.AddWithValue("@enable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Group" Then
            query.CommandText = "update db_groupmbp set groupname = @gn, isenable = @enable where groupname = '" & TextEdit1.Text & "'"
            query.Parameters.AddWithValue("@gn", TextEdit1.Text)
            query.Parameters.AddWithValue("@enable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Department" Then
            query.CommandText = "update db_departmentmbp set departmentname = @dn, isenable = @enable where departmentname = '" & TextEdit1.Text & "'"
            query.Parameters.AddWithValue("@dn", TextEdit1.Text)
            query.Parameters.AddWithValue("@enable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Type Of Offense" Then
            query.CommandText = "update db_offense set typeofoffense = @tof, isenable = @enable where typeofoffense = '" & TextEdit1.Text & "'"
            query.Parameters.AddWithValue("@tof", TextEdit1.Text)
            query.Parameters.AddWithValue("@enable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Machines" Then
            query.CommandText = "update db_machines set machine = @machines, isenable = @enable where machine = '" & TextEdit1.Text & "'"
            query.Parameters.AddWithValue("@machines", TextEdit1.Text)
            query.Parameters.AddWithValue("@enable", CheckEdit1.Checked)
            query.ExecuteNonQuery()
            changes()
        End If
    End Sub

    Sub deletion()
        Dim query As MySqlCommand = SQLConnection.CreateCommand
        If ComboBox1.Text = "Job Title" Then
            query.CommandText = "delete from db_jobtitle where jobtitle = '" & SimpleButton3.Text & "'"
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Company Code" Then
            query.CommandText = "delete from db_companycode where companycode = '" & SimpleButton3.Text & "'"
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Office Location" Then
            query.CommandText = "delete from db_officelocation where officelocation = '" & SimpleButton3.Text & "'"
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Group" Then
            query.CommandText = "delete from db_groupmbp where groupname = '" & SimpleButton3.Text & "'"
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Department" Then
            query.CommandText = "delete from db_departmentmbp where departmentname= '" & SimpleButton3.Text & "'"
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Type Of Offense" Then
            query.CommandText = "delete from db_offense where TypeOfOffense = '" & SimpleButton3.Text & "'"
            query.ExecuteNonQuery()
            changes()
        ElseIf ComboBox1.Text = "Machines" Then
            query.CommandText = "delete from db_machines where machine = '" & SimpleButton3.Text & "'"
            query.ExecuteNonQuery()
            changes()
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        deletion()
    End Sub

    Private Function GetImage() As Image
        Return ImageCollection1.Images(0)
    End Function

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Add(New DXMenuItem("Delete", New EventHandler(AddressOf SimpleButton3_Click), GetImage))
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        changing()
    End Sub
End Class