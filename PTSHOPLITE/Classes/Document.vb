Imports System.Data.SqlClient

Public Class Document

    Public Fileid As Integer
    Public Partnumber As String = ""
    Public Revision = ""
    Public path As String = ""
    Public Lastdate As Date
    Public Type As String
    Public State As String
    Public Checkedoutby As String
    Public Filepath As String
    Public DrawingNumber As String
    Public Description As String
    Public TempPath As String

    Public Sub New(file As Integer)
        'Open the part information

        Dim sqlconn As New SqlConnection(My.Settings.SHOPDB)
        Dim sqlcomm As New SqlCommand("SELECT * From Documents WHERE Id = @fileid", sqlconn)
        sqlcomm.Parameters.AddWithValue("@fileid", file)
        Fileid = file

        Try
            Dim filetable As New DataTable

            Dim sqladapt As New SqlDataAdapter(sqlcomm)
            'Fill the table with the adapter
            sqladapt.Fill(filetable)

            'Give parameters the data
            Partnumber = filetable.Rows(0).Item(1).ToString
            Description = filetable.Rows(0).Item(2).ToString
            Revision = filetable.Rows(0).Item(3).ToString
            path = filetable.Rows(0).Item(4).ToString
            Lastdate = filetable.Rows(0).Item(5)
            Type = filetable.Rows(0).Item(6).ToString
            DrawingNumber = filetable.Rows(0).Item(7).ToString
            State = filetable.Rows(0).Item(8).ToString
            Checkedoutby = filetable.Rows(0).Item(9).ToString
            TempPath = filetable.Rows(0).Item(11).ToString
            Filepath = My.Settings.DCFolder & path

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub SaveValues()
        'Save the document information
        Dim sqlconn As New SqlConnection(My.Settings.SHOPDB)

        Dim sqlcomm As New SqlCommand("UPDATE Documents Set Description = @description, Revision = @revision, " &
                                      "Path = @path, LastDate = @lastdate, Type = @type, DrawingNumber = @drawingnumber, " &
                                      "State = @state, CheckedOutBy = @checkedoutby, TempPath = @temppath WHERE Id = @fileid", sqlconn)

        'Add the parameters for the comm
        sqlcomm.Parameters.AddWithValue("@description", Description)
        sqlcomm.Parameters.AddWithValue("@revision", Revision)
        sqlcomm.Parameters.AddWithValue("@path", path)
        sqlcomm.Parameters.AddWithValue("@lastdate", Lastdate)
        sqlcomm.Parameters.AddWithValue("@type", Type)
        sqlcomm.Parameters.AddWithValue("@drawingnumber", DrawingNumber)
        sqlcomm.Parameters.AddWithValue("@state", State)
        sqlcomm.Parameters.AddWithValue("@checkedoutby", Checkedoutby)
        sqlcomm.Parameters.AddWithValue("@fileid", Fileid)
        sqlcomm.Parameters.AddWithValue("@temppath", TempPath)

        Try
            sqlconn.Open()
            sqlcomm.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            sqlconn.Close()

        End Try

    End Sub


    Sub Deletedocument()
        'Delete the file from the face of the earth
        Dim sqlconn As New SqlConnection(My.Settings.SHOPDB)
        Dim sqlcomm As New SqlCommand("UPDATE Documents Set State = 'Obsolete' WHERE Id = @id", sqlconn)
        'Add parameters to the command
        sqlcomm.Parameters.AddWithValue("@id", Fileid)

        Try
            'Open the connection. delete the file. close the connection
            sqlconn.Open()
            sqlcomm.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            sqlconn.Close()
        End Try



    End Sub
End Class
