Imports System.Data.SqlClient
Public Class SqlDatabase

    Private dbconnection As String


    Public Sub New(connstring As String)
        'Set up the connection string
        dbconnection = connstring
    End Sub

    Public Function getiddatatable(stringquery As String, idholder As Integer) As DataTable
        Dim dtable As New DataTable

        Try
            Dim cnn As New SqlConnection(dbconnection)
            cnn.Open()
            Dim com As New SqlCommand(stringquery, cnn)
            com.Parameters.AddWithValue("@id", idholder)
            Dim reader As SqlDataReader
            reader = com.ExecuteReader
            dtable.Load(reader)
            reader.Close()
            cnn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return dtable


    End Function
    Public Function getdatatable(stringquery As String, Optional parameterquery As Object = Nothing, Optional parameterquery2 As Object = Nothing) As DataTable
        Dim dtable As New DataTable

        Try
            Dim cnn As New SqlConnection(dbconnection)
            cnn.Open()
            Dim com As New SqlCommand(stringquery, cnn)
            If IsNothing(parameterquery) = False Then
                com.Parameters.AddWithValue("@parameter", parameterquery)
            End If
            If IsNothing(parameterquery2) = False Then
                com.Parameters.AddWithValue("@parameter2", parameterquery2)
            End If
            Dim reader As SqlDataReader
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
        Dim conn As New sqlConnection(dbconnection)
        conn.Open()
        Dim comm As New sqlCommand(stringquery, conn)
        Dim rowsupdated As Integer
        rowsupdated = comm.ExecuteNonQuery()

        conn.Close()
        Return rowsupdated

    End Function


    Public Function ExecuteScalar(stringquery As String, Optional parameterquery As Object = Nothing) As Object


        Dim conn As New SqlConnection(dbconnection)
        conn.Open()
        Dim comm As New SqlCommand(stringquery, conn)
        If IsNothing(parameterquery) = False Then
            comm.Parameters.AddWithValue("@parameter", parameterquery)
        End If

        Dim values As Object
        values = comm.ExecuteScalar

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

        Dim columns As String = "("
        Dim vals As String = "("
        Dim returncode As Boolean = True

        For Each Val As KeyValuePair(Of String, String) In data
            columns = columns + String.Format(" {0},", Val.Key.ToString())
            vals = vals + String.Format(" '{0}',", Val.Value)

        Next
        columns = columns.Substring(0, columns.Length - 1)
        columns = columns + ")"
        vals = vals.Substring(0, vals.Length - 1)
        vals = vals + ")"




        Try
            Me.ExecuteNonQuery(String.Format("INSERT INTO {0} {1} VALUES {2};", tablename, columns, vals))
        Catch ex As Exception
            returncode = False
        End Try

        Return returncode
    End Function

End Class
