Imports System.Data.SqlClient
Imports System.Data.OleDb

Module MachineStatusModule
    Sub ChangeMachstatus(sender As Object, Tagdescription As String, machname As String)
        'Get the information from the parent.
        Dim context_menu As ContextMenuStrip
        context_menu = CType(sender, ToolStripMenuItem).OwnerItem.Owner

        If IsNothing(context_menu) = False Then
            'Update the status
            SqlMachstatus(machname, Tagdescription)

        End If


    End Sub

    Public Function GetMachInfo(machine As String, Optional sqlconn As SqlConnection = Nothing) As DataTable
        Dim singleconn As Boolean
        singleconn = False
        If IsNothing(sqlconn) Then
            sqlconn = New SqlConnection(My.Settings.PartDatabaseString)
            singleconn = True
        End If
        Dim sqlcomm As New SqlCommand("SELECT shift1employee, shift2employee, Status, Alarm FROM Machines WHERE Name = @name", sqlconn)
        sqlcomm.Parameters.AddWithValue("@name", machine)
        Dim sqladap As New SqlDataAdapter(sqlcomm)
        Dim emplytable As New DataTable
        Try
            If sqlconn.State = ConnectionState.Closed Then
                sqlconn.Open()
            End If

            sqladap.Fill(emplytable)
            If singleconn = True Then
                sqlconn.Close()
            End If
            Return emplytable
        Catch ex As Exception
            sqlconn.Close()
            MsgBox(ex.Message)
        End Try

    End Function


    Private Sub SqlMachstatus(machine As String, status As String)

        Dim sqlconn As SqlConnection
        sqlconn = New SqlConnection(My.Settings.PartDatabaseString)

        Dim sqlcomm As New SqlCommand("Update Machines SET Status = @status WHERE Name = @name", sqlconn)
        sqlcomm.Parameters.AddWithValue("@name", machine)
        sqlcomm.Parameters.AddWithValue("@status", status)
        Try
            If sqlconn.State = ConnectionState.Closed Then
                sqlconn.Open()
            End If

            sqlcomm.ExecuteNonQuery()
            sqlconn.Close()
        Catch ex As Exception
            sqlconn.Close()
            MsgBox(ex.Message)
        End Try



    End Sub

End Module
