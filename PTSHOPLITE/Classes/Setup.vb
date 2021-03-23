Imports System.Data.SqlClient


Public Class Setup
    Public SetupID As Integer = 0
    Public PartID As String = ""
    Public Operation As Integer = 0
    Public Opdescription As String = ""
    Public Machine As String = ""
    Public Material As String = ""
    Public Fixture As String = ""
    Public Cycletime As String = ""
    Public Note As String = ""
    Public Program As String = ""

    Public Sub New(OpID As Integer)
        'Look up the setup who matches the desired operation
        GetSetupData(OpID)

    End Sub



    Public Sub GetSetupData(OPval As Integer)
        Dim SELECTString As String

        SELECTString = "SELECT * From SETUP WHERE Operation = @op"

        'Connection string
        Dim setupconn As New SqlConnection(My.Settings.PartDatabaseString)

        'setup comm
        Dim setupcomm As New SqlCommand(SELECTString, setupconn)
        setupcomm.Parameters.AddWithValue("@op", OPval)
        Dim setupdatatable As New DataTable

        Try
            'Try to update the datatable with the adapter
            Dim setupadapter As New SqlDataAdapter(setupcomm)
            setupadapter.Fill(setupdatatable)

        Catch ex As Exception
            MsgBox("Error getting setup data: " & ex.Message)
            setupconn.Close()
            Exit Sub
        End Try

        'Get the information into the individual ids
        If setupdatatable.Rows.Count > 0 Then
            With setupdatatable.Rows(0)
                SetupID = .Item(0)
                PartID = IsStringNull(.Item(1).ToString)
                Operation = .Item(2)
                Opdescription = IsStringNull(.Item(3).ToString)
                Machine = IsStringNull(.Item(4).ToString)
                Material = IsStringNull(.Item(5).ToString)
                Fixture = IsStringNull(.Item(6).ToString)
                Note = IsStringNull(.Item(8).ToString)
                Program = IsStringNull(.Item(9).ToString)
            End With
        End If

    End Sub

    Public Sub UpdateDB()

        'Update the database to include any changes
        Dim updatestring As String
        updatestring = "UPDATE SETUP SET Machine = @machine, Material = @material, OpDescription =@description, FixtureLocation = @fixture, " &
            "CycleTime = @cycle, Notes = @notes, Program = @program " &
            "WHERE Id = @setupid"

        Dim updateconn As New SqlConnection(My.Settings.PartDatabaseString.ToString)
        Dim updatecomm As New SqlCommand(updatestring, updateconn)
        'Install parameters
        updatecomm.Parameters.AddWithValue("@machine", Machine)
        updatecomm.Parameters.AddWithValue("@material", Material)
        updatecomm.Parameters.AddWithValue("@fixture", Fixture)
        updatecomm.Parameters.AddWithValue("@cycle", Cycletime)
        updatecomm.Parameters.AddWithValue("@notes", Note)
        updatecomm.Parameters.AddWithValue("@program", Program)
        updatecomm.Parameters.AddWithValue("@setupid", SetupID)
        updatecomm.Parameters.AddWithValue("@description", Opdescription)

        Try
            'Begin the sql query to update
            updateconn.Open()
            updatecomm.ExecuteNonQuery()
            updateconn.Close()

        Catch ex As Exception
            updateconn.Close()
            MsgBox("Error Updating Setup Database: " & ex.Message)

        End Try
    End Sub

End Class
