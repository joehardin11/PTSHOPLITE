Imports System.Data.SqlClient

Public Class UserValues
    Public UserId As Integer
    Public Username As String
    Public EngRights As Boolean
    Public AdminRights As Boolean
    Public QCRights As Boolean
    Public SetupRights As Boolean
    Public InventoryRights As Boolean
    Public Password As String
    Public DocControl As Boolean

    Public Areas As DataTable


    Public Sub New(User As String)
        UserId = -1
        Try
            Dim loginconn As New SqlConnection(My.Settings.PartDatabaseString)
            Dim loginstring As String = "SELECT SetupRights, EngineeringRights, QCRights, AdminRights, InventoryRights, Password, Id, DocConRights FROM Users WHERE Username = @user"
            Dim logincomm As New SqlCommand(loginstring, loginconn)
            logincomm.Parameters.AddWithValue("@user", User)

            Dim logtable As New DataTable

            Dim loginadapter As New SqlDataAdapter(logincomm)

            loginadapter.Fill(logtable)

            If logtable.Rows.Count > 0 Then
                SetupRights = logtable.Rows(0).Item(0)
                EngRights = logtable.Rows(0).Item(1)
                QCRights = logtable.Rows(0).Item(2)
                AdminRights = logtable.Rows(0).Item(3)
                InventoryRights = logtable.Rows(0).Item(4)
                Username = User
                Password = logtable.Rows(0).Item(5)
                UserId = logtable.Rows(0).Item(6)
                DocControl = logtable.Rows(0).Item(7)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub New(UserIdval As Integer)
        UserId = -1

        Try
            Dim loginconn As New SqlConnection(My.Settings.PartDatabaseString)
            Dim loginstring As String = "SELECT SetupRights, EngineeringRights, QCRights, AdminRights, InventoryRights, Username, Id, DocConRights FROM Users WHERE Id = @idval"
            Dim logincomm As New SqlCommand(loginstring, loginconn)
            logincomm.Parameters.AddWithValue("@idval", UserIdval)
            UserId = UserIdval

            Dim logtable As New DataTable

            Dim loginadapter As New SqlDataAdapter(logincomm)

            loginadapter.Fill(logtable)

            If logtable.Rows.Count > 0 Then
                SetupRights = logtable.Rows(0).Item(0)
                EngRights = logtable.Rows(0).Item(1)
                QCRights = logtable.Rows(0).Item(2)
                AdminRights = logtable.Rows(0).Item(3)
                InventoryRights = logtable.Rows(0).Item(4)
                Username = logtable.Rows(0).Item(5)
                UserId = logtable.Rows(0).Item(6)
                DocControl = logtable.Rows(0).Item(7)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Update()
        Dim updateconn As New SqlConnection(My.Settings.PartDatabaseString)
        Try
            Dim updatestring As String = "UPDATE Users SET SetupRights=@setup, EngineeringRights=@eng, QCRights=@qcrights, AdminRights=@admin, InventoryRights=@inventory WHERE Id = @id"

            Dim updatecom As New SqlCommand(updatestring, updateconn)
            updatecom.Parameters.AddWithValue("@setup", SetupRights)
            updatecom.Parameters.AddWithValue("@eng", EngRights)
            updatecom.Parameters.AddWithValue("@qcrights", QCRights)
            updatecom.Parameters.AddWithValue("@admin", AdminRights)
            updatecom.Parameters.AddWithValue("@inventory", InventoryRights)
            updatecom.Parameters.AddWithValue("@id", UserId)
            'Update password
            updateconn.Open()
            updatecom.ExecuteNonQuery()
            updateconn.Close()

        Catch ex As Exception
            updateconn.Close()

            MsgBox(ex.Message)
        End Try


    End Sub
    Public Sub CopyUserRightsandAreas(Copyuser As UserValues)
        SetupRights = Copyuser.SetupRights
        EngRights = Copyuser.EngRights
        QCRights = Copyuser.QCRights
        AdminRights = Copyuser.AdminRights
        InventoryRights = Copyuser.InventoryRights
        Update()
        'For each Area Copy it.

        Dim areadt As New DataTable


        'Delete all current areas
        areadt = GetUserAreas()
        For Each arearow As DataRow In areadt.Rows

            DeleteArea(arearow.Item(0))

        Next

        'Add all copy userrows
        areadt = Copyuser.GetUserAreas
        For Each arearow As DataRow In areadt.Rows
            AddArea(arearow.Item(3), arearow.Item(2))

        Next



    End Sub
    Public Sub UpdatePassword(passwordnew)
        Dim updateconn As New SqlConnection(My.Settings.PartDatabaseString)
        Try
            Dim updatestring As String = "UPDATE Users SET Password = @pass WHERE UserName = @username"

            Dim updatecom As New SqlCommand(updatestring, updateconn)
            updatecom.Parameters.AddWithValue("@pass", passwordnew)
            updatecom.Parameters.AddWithValue("@username", Username)

            'Update password
            updateconn.Open()
            updatecom.ExecuteNonQuery()
            updateconn.Close()

        Catch ex As Exception
            updateconn.Close()

            MsgBox(ex.Message)

        End Try



    End Sub

    Public Function GetUserAreas() As DataTable

        If UserId < 0 Then
            Return Nothing
        End If
        Dim userflagdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim userflagdt As New DataTable
        userflagdt = Nothing

        Try

            userflagdt = userflagdb.getdatatable("SELECT UserAreas.Id, Areas.Areaname, CanResolve, Areas.Id From Userareas LEFT JOIN Areas On Areas.Id = Userareas.Area WHERE UserId = @parameter", UserId)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return userflagdt

    End Function

    Public Function AddArea(areaid As Integer, Optional userresolve As Boolean = False) As Integer

        Dim userareaconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim userareacomm As New SqlCommand("INSERT INTO UserAreas (Area, UserId, CanResolve) VALUES (@area, @user, @resolve); SELECT SCOPE_IDENTITY()", userareaconn)
        userareacomm.Parameters.AddWithValue("@area", areaid)
        userareacomm.Parameters.AddWithValue("@user", UserId)
        userareacomm.Parameters.AddWithValue("@resolve", userresolve)

        Dim areaval As Integer = -1

        Try
            userareaconn.Open()
            areaval = userareacomm.ExecuteScalar
            userareaconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            userareaconn.Close()

        End Try

        Return areaval

    End Function

    Public Sub DeleteArea(areaid As Integer)

        Dim userareaconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim userareacomm As New SqlCommand("DELETE FROM UserAreas WHERE Id = @areaid AND UserId = @userid", userareaconn)
        userareacomm.Parameters.AddWithValue("@areaid", areaid)
        userareacomm.Parameters.AddWithValue("@userid", UserId)

        Try
            userareaconn.Open()
            userareacomm.ExecuteNonQuery()
            userareaconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            userareaconn.Close()

        End Try

    End Sub

    Public Sub UpdateAreaResolveRights(areaid As Integer, resolveright As Boolean)

        Dim userareaconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim userareacomm As New SqlCommand("UPDATE UserAreas SET CanResolve = @resolve WHERE Id = @id", userareaconn)
        userareacomm.Parameters.AddWithValue("@resolve", resolveright)
        userareacomm.Parameters.AddWithValue("@id", areaid)

        Try
            userareaconn.Open()
            userareacomm.ExecuteNonQuery()
            userareaconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            userareaconn.Close()

        End Try

    End Sub

End Class
