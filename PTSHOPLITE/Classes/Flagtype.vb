Imports System.Data.SqlClient

''' <summary>
''' The class for holding the flagtype
''' Areas Datatable has the for 
''' </summary>
Public Class Flagtype
    Public Id As Integer
    Public flagtype As String
    Public Areas As DataTable
    'Areas are all the area that the flag type will be based on. 
    ''' <summary>
    ''' Areas datatable is organized in this order:Flagtype, Area, Areas.Areaname, Flagtypes.Flagtype
    ''' </summary>
    ''' <param name="typeid"></param>
    Public Sub New(typeid As Integer)
        Dim typedb As New SqlDatabase(My.Settings.PartDatabaseString)
        Id = typeid

        Try
            flagtype = typedb.ExecuteScalar("SELECT FlagType From Flagtypes WHERE Id = @parameter", typeid)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
        Areas = Getareas()



    End Sub

    Public Function Getareas() As DataTable
        Dim typedb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim areadtholder As New DataTable

        Try
            areadtholder = typedb.getdatatable("SELECT FlagAreaConns.Flagtypeid, FlagAreaConns.Area, Areas.Areaname, Flagtypes.Flagtype, FlagAreaConns.Id From FlagAreaConns LEFT JOIN Flagtypes on FlagTypes.Id = FlagAreaConns.Flagtypeid LEFT JOIN Areas On Areas.Id = FlagAreaConns.Area WHERE FlagAreaConns.Flagtypeid = @parameter", Id)

        Catch ex As Exception

            MsgBox("Flag Type Class Error: " & ex.Message)

        End Try
        Areas = areadtholder

        Return areadtholder


    End Function

    Public Function Addareaconn(Areaconnid As Integer, Flagid As Integer)

        Dim areaid As Integer = -1

        Dim partareaconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim partareacomm As New SqlCommand("INSERT INTO FlagAreaConns (Area, Flagtypeid) VALUES (@area, @flagid) ; SELECT SCOPE_IDENTITY()", partareaconn)
        partareacomm.Parameters.AddWithValue("area", Areaconnid)
        partareacomm.Parameters.AddWithValue("@flagid", Flagid)
        Try
            partareaconn.Open()
            areaid = partareacomm.ExecuteScalar
            partareaconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            partareaconn.Close()

        End Try

        Return areaid


    End Function




End Class
