Imports System.IO
Module ConnectionString
    Public SQLConnection As MySqlConnection = New MySqlConnection
    Public host As String = File.ReadAllText("settinghost.txt")
    Public id As String = File.ReadAllText("settingid.txt")
    Public password As String = File.ReadAllText("settingpass.txt")
    Public db As String = File.ReadAllText("settingdb.txt")
    Public CONSTRING As String = "Server=" + host + "; User Id=" + id + "; Password=" + password + "; Database=" + db + ""
End Module