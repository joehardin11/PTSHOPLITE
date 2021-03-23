Imports System.Data.SqlClient

Module FlaggerModule

    Private Flagstring As String = "PartFlags.Id As FlagID, PartFlags.FlagTypeid, PartFlags.Description, PartFlags.Userholder, PartFlags.DateEntered, PartFlags.Active, PartFlags.PartNumber, FlagTypes.FlagType " &
         "From PartFlags LEFT JOIN FlagTypes ON FlagTypes.Id = PartFlags.Flagtypeid"
    Private Flagstringarea As String = "PartFlags.Id As FlagID, PartFlags.FlagTypeid, PartFlags.Description, PartFlags.Userholder, PartFlags.DateEntered, PartFlags.Active, PartFlags.PartNumber, FlagTypes.FlagType" &
         "((From PartFlags LEFT JOIN Flagtypes ON FlagTypes.Id = PartFlags.Flagtypeid) INNER JOIN FlagAreaConns ON FlagAreaConns.Flagtype = PartFlags.Flagtypeid)"


    '***GET FLAGS
    ''' <summary>
    ''' Gets all the flags that are active
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllActiveFlags() As DataTable

        Dim partflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partflagdt As New DataTable
        partflagdt = Nothing

        Try
            ''Get all the flag information
            partflagdt = partflagdb.getdatatable("SELECT " & Flagstring & " Where Active = @parameter", True)


        Catch ex As Exception

        End Try

        Return partflagdt
    End Function

    ''' <summary>
    ''' Get all the flags for a user;
    ''' Uses the userholder to determine what the person is in charge of
    ''' </summary>
    ''' <param name="userholder"></param>
    ''' <returns></returns>
    Public Function GetFlagsforUser(userholder As UserValues) As DataTable
        'Get all the flags that apply to a user
        Dim Areadt As New DataTable
        Areadt = userholder.GetUserAreas()

        Dim flagtable As New DataTable
        Dim flagdb As New SqlDatabase(My.Settings.PartDatabaseString)


        For Each arearow As DataRow In Areadt.Rows
            'Get all the flags for each area
            Dim temptable As New DataTable

            temptable = flagdb.getdatatable("SELECT " & Flagstring)

            'Merge the temp table with the flagtable
            flagtable.Merge(temptable)
        Next


    End Function
    ''' <summary>
    ''' PartFlags.Id As FlagID, PartFlags.FlagType, PartFlags.Description, PartFlags.User, PartFlags.DateEntered, PartFlags.Active, PartFlags.PartNumber, FlagTypes.FlagType
    ''' </summary>
    ''' <param name="partholder"></param>
    ''' <returns></returns>
    Public Function GetPartFlags(partholder As String) As DataTable
        Dim partflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partflagdt As New DataTable
        partflagdt = Nothing

        Try
            ''Get all the flag information
            partflagdt = partflagdb.getdatatable("SELECT " & Flagstring & " Where PartNumber = @parameter", partholder)

        Catch ex As Exception
            MsgBox("Part Flag Retreival Error: " & ex.Message)
        End Try

        Return partflagdt
    End Function
    ''' <summary>
    ''' PartFlags.Id As FlagID, PartFlags.FlagType, PartFlags.Description, PartFlags.User, PartFlags.DateEntered, PartFlags.Active, PartFlags.PartNumber, FlagTypes.FlagType
    ''' </summary>
    ''' <param name="jobnumber"></param>
    ''' <returns></returns>
    Public Function GetJobFlags(jobnumber As String) As DataTable
        Dim partflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partflagdt As New DataTable
        partflagdt = Nothing

        Try
            ''Get all the flag information
            partflagdt = partflagdb.getdatatable("SELECT " & Flagstring & " Where JobNumber = @parameter", jobnumber)


        Catch ex As Exception

        End Try

        Return partflagdt


    End Function

    '***Flag Functional -Activate, Delete and Deactivate stuff

    Public Function MakeOrderFlag() As Integer


    End Function

    Public Function MakePartFlag() As Integer


    End Function

    '***FLAG TYPES STUFF
    ''' <summary>
    ''' typeval = String that describes the type. Must be less than 50 characters
    ''' </summary>
    ''' <param name="typeval"></param>
    ''' <returns></returns>
    Public Function AddnewFlagtype(typeval As String) As Integer
        Dim flagid As Integer = -1
        typeval = Truncate(typeval, 50)

        Dim partflagconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim partflagcomm As New SqlCommand("INSERT INTO FlagTypes (Flagtype) VALUES (@type); SELECT SCOPE_IDENTITY()", partflagconn)
        partflagcomm.Parameters.AddWithValue("@type", typeval)

        Try
            partflagconn.Open()
            flagid = partflagcomm.ExecuteScalar
            partflagconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            partflagconn.Close()

        End Try

        Return flagid

    End Function

    Public Function Deleteflagtype(typeid As Integer) As Boolean
        ' See if that type of flag is currently active
        Dim flagdt As DataTable
        flagdt = getflagsfromtype(typeid, True)
        If flagdt.Rows.Count > 0 Then
            ' If it is, inform the user, that it can't be deleted until all active flags are resolved. 

            If MsgBox("Active flags have this type of flag. Would you like to delete all of these", vbYesNo) <> vbYes Then
                Return False
            End If
        Else
            Dim flagtypeholder As New Flagtype(typeid)
            If MsgBox("Delete Flag Type: " & flagtypeholder.flagtype & "?", vbYesNo) = vbNo Then Return False

        End If

            'If not, Delete flag type and all flag of that type in partflags

            Dim sqlconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim sqlcomm As New SqlCommand("DELETE FROM PartFlags WHERE Flagtypeid = @id; DELETE FROM FlagTypes WHERE Id = @id; DELETE FROM FlagAreaConns WHERE Flagtypeid=@id", sqlconn)
        sqlcomm.Parameters.AddWithValue("@id", typeid)
        Try
            sqlconn.Open()
            sqlcomm.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            sqlconn.Close()
        End Try

        Return True


    End Function

    Public Function Getflagtypesdt() As DataTable
        'Get all the info about a part

        Dim partflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partflagdt As New DataTable
        partflagdt = Nothing

        Try

            partflagdt = partflagdb.getdatatable("SELECT DISTINCT Id, FlagType, Permanent FROM Flagtypes")


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return partflagdt


    End Function


    ''' <summary>
    ''' Outputs: Id, FlagType, Description, User, DateEntered, Active, PartNumber
    ''' </summary>
    ''' <param name="partnumber"></param>
    ''' <param name="Active"></param>
    ''' <returns></returns>
    Public Function GetPartFlags(partnumber As String, Active As Boolean) As DataTable
        'Get all the info about a part

        Dim partflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partflagdt As New DataTable
        partflagdt = Nothing

        Try

            If Active = True Then
                partflagdt = partflagdb.getdatatable("SELECT " & Flagstring & " Where PartNumber = @parameter AND Active = 'True'", partnumber)
            Else
                partflagdt = partflagdb.getdatatable("SELECT " & FlagString & " Where PartNumber = @parameter", partnumber)
            End If

        Catch ex As Exception

        End Try

        Return partflagdt

    End Function
    Public Function getflagsfromtype(typeid As Integer, Active As Boolean) As DataTable
        'Get all the info about a part

        Dim partflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partflagdt As New DataTable
        partflagdt = Nothing

        Try
            If Active = True Then
                partflagdt = partflagdb.getdatatable("SELECT " & Flagstring & " Where Flagtypeid = @parameter AND Active = 'True'", typeid)
            Else
                partflagdt = partflagdb.getdatatable("SELECT " & FlagString & "  Where Flagtypeid = @parameter", typeid)
            End If


        Catch ex As Exception

        End Try

        Return partflagdt

    End Function

    ''' <summary>
    ''' Get the flags for a specific area of expertise
    ''' Outputs: Id, FlagType, Description, User, DateEntered, Active, PartNumber, Flagname
    ''' </summary>
    ''' <param name="partnumber"></param>
    ''' <param name="Areaid"></param>
    ''' <returns></returns>
    Public Function GetAreaPartFlags(partnumber As String, Areaid As Integer) As DataTable
        'Get all the info about a part

        Dim partflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partflagdt As New DataTable
        partflagdt = Nothing

        Try

            partflagdt = partflagdb.getdatatable("SELECT " & Flagstringarea & " WHERE PartNumber = @parameter AND FlagAreaConns.Area = @parameter2", partnumber, Areaid)

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Return partflagdt

    End Function

    Public Function MakePartFlag(Partnumber As String, Description As String, Typeid As Integer, userholder As UserValues) As Integer
        Dim flagid As Integer = -1

        Dim partflagconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim partflagcomm As New SqlCommand("INSERT INTO Users (FlagType, Description, User, DateEntered, Active, PartNumber) VALUES (@type, @description, @User, @date, @partnumber); SELECT SCOPE_IDENTITY()", partflagconn)
        partflagcomm.Parameters.AddWithValue("@type", Typeid)
        partflagcomm.Parameters.AddWithValue("@description", Description)
        partflagcomm.Parameters.AddWithValue("@User", userholder)
        partflagcomm.Parameters.AddWithValue("@date", DateTime.Now)
        partflagcomm.Parameters.AddWithValue("@partnumber", Partnumber)

        Try
            partflagconn.Open()
            flagid = partflagcomm.ExecuteScalar
            partflagconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            partflagconn.Close()

        End Try

        Return flagid


    End Function

    '***AREA STUFF***
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="AreaDescription"></param>
    ''' <returns></returns>
    Public Function Addarea(AreaDescription As String) As Integer
        Dim areaid As Integer = -1
        AreaDescription = Truncate(AreaDescription, 50)

        Dim partareaconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim partareacomm As New SqlCommand("INSERT INTO Areas (Areaname) VALUES (@type); SELECT SCOPE_IDENTITY()", partareaconn)
        partareacomm.Parameters.AddWithValue("@type", AreaDescription)

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

    Public Function GetAreas() As DataTable
        'Get all the info about a part

        Dim areasdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim areasdt As New DataTable
        areasdt = Nothing

        Try

            areasdt = areasdb.getdatatable("SELECT DISTINCT Id, Areaname FROM Areas")


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return areasdt

    End Function
    Public Sub DeleteAreaConn(Areaid As Integer)

        Dim partareaconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim partareacomm As New SqlCommand("DELETE FROM FlagAreaConns WHERE Id = @areaid", partareaconn)
        partareacomm.Parameters.AddWithValue("@areaid", Areaid)

        Try
            partareaconn.Open()
            Areaid = partareacomm.ExecuteNonQuery
            partareaconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            partareaconn.Close()

        End Try

    End Sub

    Public Sub DeleteArea(Areaid As Integer)

        Dim continueval = MsgBox("Delete area and all connections?", vbYesNo, "DELETE AREA?")

        If continueval = vbNo Then Exit Sub



        Dim partareaconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim partareacomm As New SqlCommand("DELETE FROM Areas WHERE Id = @areaid; DELETE From FlagAreaConns WHERE Area=@areaid; DELETE FROM UserAreas WHERE Area=@areaid ", partareaconn)
        partareacomm.Parameters.AddWithValue("@areaid", Areaid)

        Try
            partareaconn.Open()
            Areaid = partareacomm.ExecuteNonQuery
            partareaconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            partareaconn.Close()

        End Try

    End Sub


End Module
