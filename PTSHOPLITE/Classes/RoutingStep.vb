Imports System.Data.OleDb

Public Class RoutingStep
    Public Partno As String
    Public stepno As Integer
    Public WorkorVend As Boolean
    Public workcenter As String
    Public description As String
    Public setupunit As String
    Public setuptime As Integer
    Public Opercode As String
    Public Cycletime As Integer
    Public cycleunit As String
    Public Vendorcode As String
    Public newstepno As Integer = -1

    Public Sub New(Partnumber As String, StepNumber As Integer)
        Partno = Partnumber
        stepno = StepNumber

        Dim setpartinfoconn As New OleDbConnection(My.Settings.E2Database)
        Dim setpartinfocomm As New OleDbCommand("SELECT PartNo, StepNo, WorkorVend, WorkCntr, VendCode, OperCode, Descrip, SetupTime, TimeUnit, CycleTime, CycleUnit From Routing WHERE PartNo = @partno AND StepNo = @stepno", setpartinfoconn)
        setpartinfocomm.Parameters.AddWithValue("@partno", Partno)
        setpartinfocomm.Parameters.AddWithValue("@stepno", stepno)
        Dim datatb As New DataTable
        Try

            Using dad As New OleDbDataAdapter(setpartinfocomm)
                dad.Fill(datatb)
            End Using

            WorkorVend = NotNull(datatb.Rows(0).Item(2), False)
            Vendorcode = NotNull(datatb.Rows(0).Item(4), "")
            workcenter = NotNull(datatb.Rows(0).Item(3), "")
            Opercode = NotNull(datatb.Rows(0).Item(5), "")
            description = NotNull(datatb.Rows(0).Item(6), "")
            setuptime = NotNull(datatb.Rows(0).Item(7), 0)
            setupunit = NotNull(datatb.Rows(0).Item(8), "M")
            Cycletime = NotNull(datatb.Rows(0).Item(9), 0)
            cycleunit = NotNull(datatb.Rows(0).Item(10), "S")

            setpartinfoconn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            setpartinfoconn.Close()
        End Try

    End Sub

    Public Sub Updaterouting()

        Dim setpartinfoconn As New OleDbConnection(My.Settings.E2Database)
        Dim setpartinfocomm As New OleDbCommand("UPDATE Routing Set StepNo = @newstepno, WorkCntr = @workcntr, OperCode = @opercode, Descrip=@descrip, SetupTime=@setuptime, TimeUnit=@timeunit,  CycleTime = @cycletime, cycleunit = @cycleunit WHERE PartNo = @partno And StepNo = @stepno", setpartinfoconn)
        If newstepno = -1 Then newstepno = stepno
        setpartinfocomm.Parameters.AddWithValue("@newstepno", newstepno)
        setpartinfocomm.Parameters.AddWithValue("@workcntr", workcenter)
        setpartinfocomm.Parameters.AddWithValue("@opercode", Opercode)
        setpartinfocomm.Parameters.AddWithValue("@descrip", description)
        setpartinfocomm.Parameters.AddWithValue("@setuptime", setuptime)
        setpartinfocomm.Parameters.AddWithValue("@timeunit", setupunit)
        setpartinfocomm.Parameters.AddWithValue("@cycletime", Cycletime)
        setpartinfocomm.Parameters.AddWithValue("@cycleunit", cycleunit)
        setpartinfocomm.Parameters.AddWithValue("@partno", Partno)
        setpartinfocomm.Parameters.AddWithValue("@stepno", stepno)

        Try
            setpartinfoconn.Open()
            setpartinfocomm.ExecuteNonQuery()
            setpartinfoconn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            setpartinfoconn.Close()
        End Try



    End Sub

    Public Sub DeleteRoutingStep()
        Dim setpartinfoconn As New OleDbConnection(My.Settings.E2Database)
        Dim setpartinfocomm As New OleDbCommand("Delete Routing WHERE PartNo = @partno And StepNo = @stepno", setpartinfoconn)
        setpartinfocomm.Parameters.AddWithValue("@partno", Partno)
        setpartinfocomm.Parameters.AddWithValue("@stepno", stepno)


        Try
            setpartinfoconn.Open()
            setpartinfocomm.ExecuteNonQuery()
            setpartinfoconn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            setpartinfoconn.Close()
        End Try


    End Sub
End Class
