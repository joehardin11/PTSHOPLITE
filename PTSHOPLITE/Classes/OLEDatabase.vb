Imports System.Data.OleDb
Public Class OLEDatabase

    Private dbconnection As String


    Public Sub New(connstring As String)
        'Set up the connection string
        dbconnection = connstring
    End Sub

    Public Function getdatatable(stringquery) As DataTable
        Dim dtable As New DataTable

        Try
            Dim cnn As New OleDbConnection(dbconnection)
            cnn.Open()
            Dim com As New OleDbCommand(stringquery, cnn)
            Dim reader As OleDbDataReader
            reader = com.ExecuteReader
            dtable.Load(reader)
            reader.Close()
            cnn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return dtable


    End Function

    Public Function ExecuteNonQuery(stringquery As String) As Integer
        Dim conn As New OleDbConnection(dbconnection)
        conn.Open()
        Dim comm As New OleDbCommand(stringquery, conn)
        Dim rowsupdated As Integer
        rowsupdated = comm.ExecuteNonQuery()

        conn.Close()
        Return rowsupdated

    End Function


    Public Function ExecuteScalar(stringquery As String) As String
        Dim conn As New OleDbConnection(dbconnection)
        conn.Open()
        Dim comm As New OleDbCommand(stringquery, conn)
        Dim values As Object
        values = comm.ExecuteNonQuery()
        conn.Close()
        If IsDBNull(values) Or IsNothing(values) Then
            Return ""
        Else
            Return values.ToString
        End If
        Return values

    End Function

    Public Function Update(tablename As String, data As Dictionary(Of String, String), where As String) As Boolean
        Dim vals As String = ""
        Dim returncode As Boolean = True

        If data.Count >= 1 Then
            For Each Val As KeyValuePair(Of String, String) In data
                vals = vals + String.Format(" {0} = '{1}',", Val.Key.ToString(), Val.Value.ToString())

            Next
            vals = vals.Substring(0, vals.Length - 1)



        End If
        Try
            Me.ExecuteNonQuery(String.Format("Update {0} Set {1} where {2};", tablename, vals, where))
        Catch ex As Exception
            returncode = False
        End Try

        Return returncode
    End Function

    Public Function Insert(tablename As String, data As Dictionary(Of String, String), where As String) As Boolean

        Dim columns As String = ""
        Dim vals As String = ""
        Dim returncode As Boolean = True

        For Each Val As KeyValuePair(Of String, String) In data
            columns = columns + String.Format(" {0},", Val.Key.ToString())
            vals = vals + String.Format(" '{0}',", Val.Value)

        Next
        columns = columns.Substring(0, columns.Length - 1)
        vals = vals.Substring(0, vals.Length - 1)




        Try
            Me.ExecuteNonQuery(String.Format("Update {0} Set {1} where {2};", tablename, vals, where))
        Catch ex As Exception
            returncode = False
        End Try

        Return returncode
    End Function

End Class
