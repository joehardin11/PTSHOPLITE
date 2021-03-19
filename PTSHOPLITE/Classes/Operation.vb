Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class Operation

    Public JobNo As String
    Public Stepno As Integer
    Public Partno As String
    Public WorkCenter As String
    Public OrderQty As Integer
    Public MakeQty As Integer
    Public duedate As Date
    Public Assignedmachine As String
    Public totalesthours As Single
    Public totalhoursleft As Single
    Public totalacthours As Single
    Public startdate As Date
    Public enddate As Date
    Public OperationCode As String
    Public description As String
    Public cycletime As Integer
    Public cycleunit As String
    Public setuptime As Integer
    Public setupunit As String
    Public actualpcsgood As Integer
    Public actualpcsscrap As Integer
    Public NumMachforJob As Integer
    Public Material As Boolean
    Public Tooling As Boolean
    Public Completed As Boolean
    Public Program As Boolean
    Public Revision As String
    Public Oneshift As Boolean


    Public Sub New(job As String, stepnum As Integer, oledb As Boolean, poolconnections As Boolean, Optional connectionop As OleDbConnection = Nothing)

        'Look up and add to opscheduling if there isn't a version of that
        Dim checkconn As New OleDbConnection(My.Settings.E2Database)
        Dim checkcomm As New OleDbCommand("SELECT COUNT(*) FROM OpScheduling WHERE JobNo = @jobno AND StepNo=@stepno", checkconn)
        checkcomm.Parameters.AddWithValue("@jobno", job)
        checkcomm.Parameters.AddWithValue("@stepno", stepnum)

        'Dim insertconn As New OleDbConnection(My.Settings.PartHomeDatabaseString)
        'Dim insertcomm As New OleDbCommand("INSERT INTO OpScheduling (JobNo, StepNo) VALUES (@jobno, @stepno)", insertconn)

        ''Look up and add to opscheduling if there isn't a version of that
        'Dim checkorderconn As New OleDbConnection(My.Settings.PartHomeDatabaseString)
        'Dim checkordercomm As New OleDbCommand("SELECT COUNT(*) FROM OrderScheduling WHERE JobNo = @jobno", checkinsertconn)

        'Dim orderinsertconn As New OleDbConnection(My.Settings.PartHomeDatabaseString)
        'Dim orderinsertcomm As New OleDbCommand("INSERT INTO OrderScheduling (JobNo) Values (@jobno)" FROM OrderDet; WHERE NOT EXISTS (SELECT Null FROM OrderScheduling WHERE JobNo = @jobno)", orderinsertconn)
        'orderinsertcomm.Parameters.AddWithValue("@jobno", job)

        If oledb = True Then

            'OLEDB Queries
            Dim opquery As String = "SELECT OrderRouting.JobNo, OrderRouting.StepNo, OrderRouting.PartNo, OrderRouting.WorkCntr, OrderDet.QtyOrdered, OrderDet.QtyToMake, OrderDet.DueDate, " &
            "TotEstHrs, TotHrsLeft, TotActHrs, OrderRouting.EstimStartDate, OrderRouting.EstimEndDate, OperCode, OrderRouting.Descrip, CycleTime, CycleUnit, " &
            " SetupTime, TimeUnit, ActualPcsGood, ActualPcsScrap, OrderDet.Revision FROM (OrderRouting INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo) WHERE OrderRouting.JobNo = @jobno AND OrderRouting.StepNo = @stepno"

            'Add the connection
            Dim selectconn As New OleDbConnection(My.Settings.E2Database)
            Dim selectcomm As OleDbCommand  'Add the command
            If poolconnections = False Then 'Pool the oledb connections
                selectcomm = New OleDbCommand(opquery, selectconn)
            Else
                selectcomm = New OleDbCommand(opquery, connectionop)
            End If
            selectcomm.Parameters.AddWithValue("@jobno", job)
            selectcomm.Parameters.AddWithValue("@stepno", stepnum)
            Dim datatb As New DataTable

            'Scheduling Connection
            Dim opinfoquery As String = "SELECT Programming, Tooling, Completed, OrderScheduling.Material, CurrentMachine, EstimStartDate, EstimEndDate, Oneshift From (OpScheduling INNER JOIN OrderScheduling ON OpScheduling.JobNo = OrderScheduling.JobNo) WHERE OpScheduling.JobNo = @jobno and OpScheduling.StepNo = @stepno"

            Dim infoconn As New OleDbConnection(My.Settings.E2Database)
            Dim infocomm As New OleDbCommand(opinfoquery, infoconn)
            infocomm.Parameters.AddWithValue("@jobno", job)
            infocomm.Parameters.AddWithValue("@stepno", stepnum)

            Dim infodt As New DataTable

            Try
                checkconn.Open()
                Dim Dread As Integer = checkcomm.ExecuteScalar
                If Dread = 0 Then
                    checkcomm.CommandText = "INSERT INTO OpScheduling (JobNo, StepNo) VALUES (@jobno, @stepno)"
                    checkcomm.ExecuteNonQuery()
                End If

                checkcomm.CommandText = "SELECT COUNT(*) FROM OrderScheduling WHERE JobNo = @jobno"

                Dread = checkcomm.ExecuteScalar
                If Dread = 0 Then
                    checkcomm.CommandText = "INSERT INTO OrderScheduling (JobNo) Values (@jobno)"
                    checkcomm.ExecuteNonQuery()
                End If
                checkconn.Close()

                Using dad As New OleDbDataAdapter(selectcomm)
                    dad.Fill(datatb)
                End Using

                Using infodadapt As New OleDbDataAdapter(infocomm)
                    infodadapt.Fill(infodt)
                End Using

            Catch ex As Exception
                checkconn.Close()
                selectconn.Close()
                infoconn.Close()
                'matconn.Close()
                MsgBox("Database Error: " & ex.Message)
                Exit Sub

            End Try
            If datatb.Rows.Count > 0 Then
                JobNo = datatb.Rows(0).Item(0)          'JobNo
                Stepno = datatb.Rows(0).Item(1)         'Stepno
                Partno = datatb.Rows(0).Item(2)         'Partno
                WorkCenter = NotNull(datatb.Rows(0).Item(3), "")     'WorkCenter
                OrderQty = datatb.Rows(0).Item(4)       'Orderqty
                MakeQty = datatb.Rows(0).Item(5)        'makeQty
                duedate = datatb.Rows(0).Item(6)        'DueDate
                totalesthours = datatb.Rows(0).Item(7)      'Esitmateds hours left
                totalhoursleft = datatb.Rows(0).Item(8)     'totalhoursleft
                totalacthours = datatb.Rows(0).Item(9)   'Actual hours

                OperationCode = datatb.Rows(0).Item(12)    'Operationcode
                description = datatb.Rows(0).Item(13)      'Description
                cycletime = datatb.Rows(0).Item(14)        'Cycletime
                cycleunit = datatb.Rows(0).Item(15)        'Cycleunit
                setuptime = datatb.Rows(0).Item(16)        'setuptime
                setupunit = datatb.Rows(0).Item(17)         'Setupunit
                actualpcsgood = NotNull(datatb.Rows(0).Item(18), 0)    'Pieces Good
                actualpcsscrap = NotNull(datatb.Rows(0).Item(19), 0)   'Pieces scrap
                Revision = datatb.Rows(0).Item(20)
                'Revision
            Else
                JobNo = job
                Stepno = stepnum
            End If

            'Program = NotNull(infodt.Rows(0).Item(0), False)   'Program

            Program = Checkprogram(Stepno, Partno)

            Tooling = NotNull(infodt.Rows(0).Item(1), False)    'Tooling
            Completed = NotNull(infodt.Rows(0).Item(2), False)   'Completed
            Material = NotNull(infodt.Rows(0).Item(3), False)       'Material
            Assignedmachine = NotNull(infodt.Rows(0).Item(4), "")
            startdate = NotNull(infodt.Rows(0).Item(5), Today())       'Startdate
            enddate = NotNull(infodt.Rows(0).Item(6), Add_businessdays(startdate, totalhoursleft, Oneshift))         'Enddate
            Oneshift = NotNull(infodt.Rows(0).Item(7), False)

        End If

    End Sub

    Public Function Getsetup() As Setup

    End Function
    Public Sub SetMachineAndDates()

        Dim setdateconn As New OleDbConnection(My.Settings.E2Database)
        Dim setdatecomm As New OleDbCommand("UPDATE OpScheduling Set CurrentMachine=@currentmachine, EstimStartDate=@startdate, EstimEndDate=@enddate WHERE JobNo = @jobno AND StepNo = @stepno", setdateconn)

        Dim machparam As New OleDbParameter("@currentmachine", OleDbType.WChar)
        machparam.Value = Assignedmachine
        setdatecomm.Parameters.Add(machparam)
        Dim startoleparameter As New OleDbParameter("@startdate", OleDbType.Date)
        startoleparameter.Value = startdate
        setdatecomm.Parameters.Add(startoleparameter)
        Dim endoleparameter As New OleDbParameter("@enddate", OleDbType.Date)
        endoleparameter.Value = enddate
        setdatecomm.Parameters.Add(endoleparameter)
        Dim jobparam As New OleDbParameter("@jobno", OleDbType.WChar)
        jobparam.Value = JobNo
        setdatecomm.Parameters.Add(jobparam)
        Dim stepparam As New OleDbParameter("@stepno", OleDbType.Integer)
        stepparam.Value = Stepno
        setdatecomm.Parameters.Add(stepparam)


        Try
            setdateconn.Open()
            setdatecomm.ExecuteNonQuery()
            setdateconn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            setdateconn.Close()
        End Try

    End Sub
    Private Function unitsandtimeHRS(timevalue As Single, unitvalue As String) As Single

        If unitvalue = "H" Then
            Return timevalue
        ElseIf unitvalue = "M" Then
            Return (timevalue / 60)
        Else
            Return timevalue / 3600
        End If


    End Function
    Public Sub updatePartRouteInfo(updatepart As Boolean)

        'Calculate hrsleft
        If actualpcsgood > 0 Then
            totalhoursleft = (MakeQty - actualpcsgood) * unitsandtimeHRS(cycletime, cycleunit)
        Else
            totalhoursleft = ((MakeQty - actualpcsgood) * unitsandtimeHRS(cycletime, cycleunit)) + unitsandtimeHRS(setuptime, setupunit)
        End If

        totalesthours = ((MakeQty - actualpcsgood) * unitsandtimeHRS(cycletime, cycleunit)) + unitsandtimeHRS(setuptime, setupunit)
        Dim setopinfoconn As New OleDbConnection(My.Settings.E2Database)

        Try
            If updatepart = True Then

                Dim setopinfocomm As New OleDbCommand("UPDATE Routing Set OperCode = @opercode, WorkCntr = @workcntr, SetupTime=@setuptime, TimeUnit=@timeunit, Descrip=@descrip, CycleTime = @cycletime, cycleunit = @cycleunit WHERE PartNo = @partno AND StepNo =@stepno", setopinfoconn)

                setopinfocomm.Parameters.AddWithValue("@opercode", OperationCode)
                setopinfocomm.Parameters.AddWithValue("@workcntr", WorkCenter)
                setopinfocomm.Parameters.AddWithValue("@setuptime", setuptime)
                setopinfocomm.Parameters.AddWithValue("@timeunit", setupunit)
                setopinfocomm.Parameters.AddWithValue("@descrip", description)
                setopinfocomm.Parameters.AddWithValue("@cycletime", cycletime)
                setopinfocomm.Parameters.AddWithValue("@cycleunit", cycleunit)
                setopinfocomm.Parameters.AddWithValue("@partno", Partno)
                setopinfocomm.Parameters.AddWithValue("@stepno", Stepno)

                setopinfoconn.Open()
                setopinfocomm.ExecuteNonQuery()
                setopinfoconn.Close()

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            setopinfoconn.Close()
        End Try
    End Sub

    Public Sub updateRouteInfo(updatepart As Boolean)

        'Calculate hrsleft
        If actualpcsgood > 0 Then
            totalhoursleft = (MakeQty - actualpcsgood) * unitsandtimeHRS(cycletime, cycleunit)
        Else
            totalhoursleft = ((MakeQty - actualpcsgood) * unitsandtimeHRS(cycletime, cycleunit)) + unitsandtimeHRS(setuptime, setupunit)
        End If

        totalesthours = ((MakeQty - actualpcsgood) * unitsandtimeHRS(cycletime, cycleunit)) + unitsandtimeHRS(setuptime, setupunit)

        Dim setopinfoconn As New OleDbConnection(My.Settings.E2Database)
        Dim setopinfocomm As New OleDbCommand("UPDATE OrderRouting Set OperCode = @opercode,  WorkCntr = @workcntr, SetupTime=@setuptime, " &
                                              "TimeUnit=@timeunit, Descrip=@descrip, CycleTime = @cycletime, cycleunit = @cycleunit, " &
                                              "TotHrsLeft = @tothrsleft, TotestHrs = @totesthours WHERE JobNo = @jobno And StepNo =@stepno", setopinfoconn)

        setopinfocomm.Parameters.AddWithValue("@opercode", OperationCode)
        setopinfocomm.Parameters.AddWithValue("@workcntr", WorkCenter)
        setopinfocomm.Parameters.AddWithValue("@setuptime", setuptime)
        setopinfocomm.Parameters.AddWithValue("@timeunit", setupunit)
        setopinfocomm.Parameters.AddWithValue("@descrip", description)
        setopinfocomm.Parameters.AddWithValue("@cycletime", cycletime)
        setopinfocomm.Parameters.AddWithValue("@cycleunit", cycleunit)
        setopinfocomm.Parameters.AddWithValue("@totalhrsleft", totalhoursleft)
        setopinfocomm.Parameters.AddWithValue("@totesthrs", totalesthours)
        setopinfocomm.Parameters.AddWithValue("@jobno", JobNo)
        setopinfocomm.Parameters.AddWithValue("@stepno", Stepno)

        Dim setupopconn2 As New OleDbConnection(My.Settings.E2Database)
        Dim setupopcomm2 As New OleDbCommand("UPDATE OrderDet Set DueDate = @duedate WHERE JobNo = @jobno", setupopconn2)
        setupopcomm2.Parameters.AddWithValue("@duedate", duedate)
        setupopcomm2.Parameters.AddWithValue("@jobno", JobNo)


        Try
            setopinfoconn.Open()
            setopinfocomm.ExecuteNonQuery()
            setopinfoconn.Close()
            setupopconn2.Open()
            setupopcomm2.ExecuteNonQuery()
            setupopconn2.Close()

            If updatepart = True Then
                Dim setpartinfoconn As New OleDbConnection(My.Settings.E2Database)
                Dim setpartinfocomm As New OleDbCommand("UPDATE Routing Set OperCode = @opercode, WorkCntr = @workcntr, SetupTime=@setuptime, TimeUnit=@timeunit, Descrip=@descrip, CycleTime = @cycletime, cycleunit = @cycleunit WHERE PartNo = @partno And StepNo =@stepno", setpartinfoconn)

                setpartinfocomm.Parameters.AddWithValue("@opercode", OperationCode)
                setpartinfocomm.Parameters.AddWithValue("@workcntr", WorkCenter)
                setpartinfocomm.Parameters.AddWithValue("@setuptime", setuptime)
                setpartinfocomm.Parameters.AddWithValue("@timeunit", setupunit)
                setpartinfocomm.Parameters.AddWithValue("@descrip", description)
                setpartinfocomm.Parameters.AddWithValue("@cycletime", cycletime)
                setpartinfocomm.Parameters.AddWithValue("@cycleunit", cycleunit)
                setpartinfocomm.Parameters.AddWithValue("@partno", JobNo)
                setpartinfocomm.Parameters.AddWithValue("@stepno", Stepno)

                setpartinfoconn.Open()
                setpartinfocomm.ExecuteNonQuery()
                setpartinfoconn.Close()

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            setopinfoconn.Close()
            setupopconn2.Close()
        End Try
    End Sub

    Public Sub updatestates()

        Dim setconn As New OleDbConnection(My.Settings.E2Database)
        Dim setcomm As New OleDbCommand("UPDATE OpScheduling Set Programming = @program, Tooling = @tooling, Completed = @completed, Oneshift = @shift WHERE JobNo = @jobno AND StepNo = @stepno", setconn)

        setcomm.Parameters.AddWithValue("@program", Program)
        setcomm.Parameters.AddWithValue("@tooling", Tooling)
        setcomm.Parameters.AddWithValue("@completed", Completed)
        setcomm.Parameters.AddWithValue("@shift", Oneshift)
        setcomm.Parameters.AddWithValue("@jobno", JobNo)
        setcomm.Parameters.AddWithValue("@stepno", Stepno)

        Dim setmatconn As New OleDbConnection(My.Settings.E2Database)
        Dim setmatcomm As New OleDbCommand("UPDATE OrderScheduling Set Material=@material WHERE JobNo = @jobno", setmatconn)

        setmatcomm.Parameters.AddWithValue("@material", Material)
        setmatcomm.Parameters.AddWithValue("@jobno", JobNo)

        Try

            setconn.Open()
            setcomm.ExecuteNonQuery()
            setconn.Close()
            setmatconn.Open()
            setmatcomm.ExecuteNonQuery()
            setmatconn.Close()

        Catch ex As Exception

            MsgBox(ex.Message)
            setconn.Close()
            setmatconn.Close()

        End Try

    End Sub
End Class
