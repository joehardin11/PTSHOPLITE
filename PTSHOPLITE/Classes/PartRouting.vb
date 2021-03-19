Imports System.Data.OleDb

Public Class PartRouting

    Public PartNo As String
    Public routingsteps As DataTable

    Public Sub New(partnoval As String)
        PartNo = partnoval
        Dim setpartinfoconn As New OleDbConnection(My.Settings.E2Database)
        Dim setpartinfocomm As New OleDbCommand("SELECT PartNo, StepNo, WorkorVend, WorkCntr, VendCode, OperCode, Descrip, SetupTime, TimeUnit, CycleTime, CycleUnit From Routing WHERE PartNo = @partno ORDER BY StepNo ", setpartinfoconn)
        setpartinfocomm.Parameters.AddWithValue("@partno", PartNo)

        Dim datatb As New DataTable
        Try

            Using dad As New OleDbDataAdapter(setpartinfocomm)
                dad.Fill(datatb)
            End Using
            setpartinfoconn.Close()

            routingsteps = datatb

        Catch ex As Exception
            MsgBox(ex.Message)
            setpartinfoconn.Close()
        End Try
    End Sub
    Public Sub MovestepEarlier(Stepno As Integer)
        Dim lastrow As Integer = 0
        Dim lastindex As Integer = -1
        Dim foundvalue As Boolean = False

        For Each steprow As DataRow In routingsteps.Rows
            If foundvalue = False Then
                Dim currentrow As Integer
                currentrow = steprow.Item("StepNo")
                If currentrow = Stepno And lastindex <> -1 Then

                    Dim earlystep As New RoutingStep(PartNo, routingsteps.Rows(lastindex).Item("StepNo"))
                    earlystep.newstepno = 1
                    earlystep.Updaterouting()
                    earlystep = New RoutingStep(PartNo, 1)

                    Dim oldstep As New RoutingStep(PartNo, Stepno)
                    oldstep.newstepno = routingsteps.Rows(lastindex).Item("StepNo")
                    oldstep.Updaterouting()

                    earlystep.newstepno = Stepno
                    earlystep.Updaterouting()

                    foundvalue = True

                Else
                    lastindex = lastindex + 1
                End If

            End If

        Next

    End Sub

    Public Sub Movesteplater(stepno As Integer)
        Dim lastrow As Integer = 0
        Dim lastindex As Integer = -1
        Dim foundvalue As Boolean = False

        For i = routingsteps.Rows.Count - 1 To 0 Step -1
            Dim steprow As DataRow
            steprow = routingsteps.Rows(i)

            If foundvalue = False Then
                Dim currentrow As Integer
                currentrow = steprow.Item("StepNo")
                If currentrow = stepno And lastindex <> -1 Then

                    Dim latertep As New RoutingStep(PartNo, routingsteps.Rows(lastindex).Item("StepNo"))
                    latertep.newstepno = 1
                    latertep.Updaterouting()
                    latertep = New RoutingStep(PartNo, 1)

                    Dim oldstep As New RoutingStep(PartNo, stepno)
                    oldstep.newstepno = routingsteps.Rows(lastindex).Item("StepNo")
                    oldstep.Updaterouting()

                    latertep.newstepno = stepno
                    latertep.Updaterouting()

                    foundvalue = True

                Else
                    lastindex = i
                End If

            End If

        Next


    End Sub

    Public Sub UpdateWorkCenter(stepno As Integer, Newworkcenter As String)

        Dim routestep As New RoutingStep(PartNo, stepno)

        routestep.workcenter = Newworkcenter
        routestep.Updaterouting()

    End Sub

    Public Sub UpdateOperCode(stepno As Integer, newopcode As String)

        Dim routestep As New RoutingStep(PartNo, stepno)

        routestep.Opercode = newopcode
        routestep.Updaterouting()

    End Sub

    Public Sub UpdateCycleTime(stepno As Integer, Newcycletime As Integer, newcycleunit As String)
        Dim routestep As New RoutingStep(PartNo, stepno)

        routestep.Cycletime = newcycleunit
        routestep.cycleunit = newcycleunit

        routestep.Updaterouting()

    End Sub

    Public Sub UpdateSetuptime(stepno As Integer, Newsetuptime As Integer, newsetupunit As String)
        Dim routestep As New RoutingStep(PartNo, stepno)


        routestep.setupunit = newsetupunit
        routestep.setupunit = newsetupunit

        routestep.Updaterouting()
    End Sub

End Class
