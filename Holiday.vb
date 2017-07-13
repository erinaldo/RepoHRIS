Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class Holiday
    Dim connectionString As String
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim oDt_sched As New DataTable
    Dim tbl_par As New DataTable

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
        connectionString = "Server=" + host + "; User Id=" + id + "; Password=" + password + "; Database=" + db + ""
    End Sub

    Private Sub holidays()
        GridControl6.RefreshDataSource()
        Dim sqlcommand As MySqlCommand = SQLConnection.CreateCommand
        Try
            'sqlcommand.CommandText = "Select tgl as Dates, Reason as HolidaysReason, TotalDays from db_holiday where year(tgl) = year(curdate()) "
            sqlcommand.CommandText = "select tgl as Dates, Reason as HolidaysReason, TotalDays From db_holiday where tgl between @date1 and @date2"
            sqlcommand.Parameters.AddWithValue("@date1", DateTimePicker1.Value.Date)
            sqlcommand.Parameters.AddWithValue("@date2", DateTimePicker2.Value.Date)
            'select * from db_holiday where month(tgl) = month(CURDATE()).       
            Dim table As New DataTable
            table.Load(sqlcommand.ExecuteReader)
            GridControl6.DataSource = table
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        GridControl6.UseEmbeddedNavigator = True
        Dim navigator As ControlNavigator = GridControl6.EmbeddedNavigator
        navigator.Buttons.Append.Visible = False
        navigator.Buttons.Remove.Visible = False
        Dim gridView As GridView = CType(GridControl6.FocusedView, GridView)
        gridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
   New GridColumnSortInfo(gridView.Columns("HolidaysReason"), DevExpress.Data.ColumnSortOrder.Descending)}, 1)
    End Sub

    Sub Process()
        Dim dtb, dtr As DateTime
        date1.Format = DateTimePickerFormat.Custom
        date1.CustomFormat = "yyyy-MM-dd"
        dtb = date1.Value
        date2.Format = DateTimePickerFormat.Custom
        date2.CustomFormat = "yyyy-MM-dd"
        dtr = date2.Value
        Dim sqlcommand As New MySqlCommand
        Dim str_carsql As String
        Dim d1 As Date = date1.Value.Date
        d1 = d1.AddDays(-1)
        Dim d2 As Date = date2.Value.Date
#Disable Warning BC42016 ' Implicit conversion
        For j As Integer = 1 To DateDiff("d", d1, d2)
#Enable Warning BC42016 ' Implicit conversion
            d1 = d1.AddDays(1)
            str_carsql = "INSERT INTO db_holiday " +
                            "(tgl, StartDate, EndDate, TotalDays, Reason) " +
                                    " Values (@tgl, @StartDate, @EndDate, @TotalDays, @Reason)"
            sqlcommand.Connection = SQLConnection
            sqlcommand.CommandText = str_carsql
            sqlcommand.Parameters.Clear()
            sqlcommand.Parameters.AddWithValue("@tgl", d1.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@StartDate", dtb.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@EndDate", dtr.ToString("yyyy-MM-dd"))
            sqlcommand.Parameters.AddWithValue("@TotalDays", txtdays.Text)
            sqlcommand.Parameters.AddWithValue("@Reason", textreason.Text)
            sqlcommand.Connection = SQLConnection
            sqlcommand.ExecuteNonQuery()
        Next
        updateovertime()
        updatesunday()
        MsgBox("Holiday added")
    End Sub

    Sub updatesunday()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_holiday set reason = 'Sunday OFF' where dayofweek(tgl) = 1"
            query.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("OFF DAY " & ex.Message)
        End Try
    End Sub

    Sub updateovertime()
        Try
            Dim query As MySqlCommand = SQLConnection.CreateCommand
            query.CommandText = "update db_absensi set isholiday = 1 where dayofweek(tanggal) = 1 or tanggal in (select tgl from db_holiday)"
            query.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Overtime Part " & ex.Message)
        End Try
    End Sub

    Sub days()
        Dim t As TimeSpan = date2.Value - date1.Value
        Dim tmp As String
        tmp = t.Days.ToString
        Dim hasil As Integer
        hasil = CInt(tmp)
        Dim hasil2 As Integer = hasil + 1
        txtdays.Text = hasil2.ToString
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        days()
        If date1.Text = "" Then
            MsgBox("Please Input The Start Date")
        ElseIf date1.Value > date2.Value Then
            MsgBox("Total days can't be zero or less than zero days")
        ElseIf textreason.Text = "" Then
            MsgBox("Please insert the reason of the holiday")
        Else
            Process()
            holidays()
        End If
    End Sub

    Private Sub Holiday_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLConnection.ConnectionString = connectionString
        SQLConnection.Open()
        days()
    End Sub

    Private Sub date1_ValueChanged(sender As Object, e As EventArgs) Handles date1.ValueChanged
        days()
    End Sub

    Private Sub date2_ValueChanged(sender As Object, e As EventArgs) Handles date2.ValueChanged
        days()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        holidays()
    End Sub
End Class