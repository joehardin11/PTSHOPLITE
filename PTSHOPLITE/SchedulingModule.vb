Imports System.Data.SqlClient
Imports System.Data.OleDb


Module SchedulingModule
    ''' <summary>
    ''' Provide the sales orders to transfer into a sales order
    ''' </summary>
    ''' <param name="salesordertable"></param>
    ''' <returns></returns>

    Public Function GetLateMaterialJobs()

    End Function

    Public Function GetDueMaterialjobs()


    End Function


    ''' <summary>
    ''' Get the material info using a jobnumber
    ''' ItemOrder: 1: JobMaterials.QtyPosted1, 2:JobMaterials.BinLoc1, 3. JobMaterials.PartNo, 4.JobMaterials.Description, 55.JobMaterials.MainPart,
    ''' 6.JobMaterials.StockUnit, 7.JobMaterials.PONum, 8.POReleases.DueDate, 9.POReleases.DateReceived, 10.POReleaess.Comment, 11.JobMaterials.Counter
    ''' 
    ''' </summary>
    ''' <param name="jobname"></param>
    ''' <param name="sqldatabase"></param>
    ''' <returns></returns>
    Public Function GetOrderMaterial(jobname As String, sqldatabase As Boolean)
        If sqldatabase = True Then



        Else
            Dim machconn As New OleDbConnection(My.Settings.E2Database)

            Dim machcomm As New OleDbCommand("SELECT DISTINCT JobMaterials.Counter, JobMaterials.BinLoc1, JobMaterials.PartNo, JobMaterials.Description, JobMaterials.MainPart, JobMaterials.StockUnit, JobMaterials.PONum As PoNum, POReleases.DueDate, POReleases.DateReceived, POReleases.Comments, JobMaterials.DatePosted, JobMaterials.ReceiverNo As Received, JobMaterials.Bin1Lot, JobMaterials.QtyPosted1 As Qty, POReleases.Qty As POQty FROM (JobMaterials LEFT JOIN POReleases ON (JobMaterials.JobNo = POReleases.JobNo AND JobMaterials.PONum = POReleases.PONum)) " &
                                             "WHERE JobMaterials.JobNo = @job AND JobMaterials.OutsideService = 'N'", machconn)
            ' 
            ' Dim machcomm As New OleDbCommand("SELECT JobNo, StepNo, EstimStartDate, EstimEndDate, Material FROM OpScheduling WHERE CurrentMachine = @machine", machconn)
            Dim machparam As New OleDbParameter("@job", OleDbType.WChar)
            machparam.Value = jobname
            machcomm.Parameters.Add(machparam)


            Try
                machconn.Open()
                Dim machoptable As New DataTable
                Dim machadapter As New OleDbDataAdapter(machcomm)
                machadapter.Fill(machoptable)
                machconn.Close()
                Return machoptable

            Catch ex As Exception
                MsgBox(ex.Message)
                machconn.Close()
                Return Nothing
            End Try


        End If




    End Function

    Public Function GetMachineOps(ByVal machinename As String, Optional jobnoonly As String = "", Optional Sqldatabase As Boolean = False, Optional poolconnection As Boolean = False, Optional pooledcon As OleDbConnection = Nothing) As DataTable

        If Sqldatabase = True Then



        Else
            Dim machconn As OleDbConnection
            If poolconnection = True Then
                machconn = pooledcon
            Else
                machconn = New OleDbConnection(My.Settings.E2Database)
            End If


            Dim machcomm As New OleDbCommand("SELECT DISTINCT OpScheduling.JobNo, OpScheduling.StepNo, OpScheduling.EstimStartDate, OpScheduling.EstimEndDate, " &
                                             "OrderScheduling.Material, Programming, Tooling, Completed, OrderDet.DueDate, Estim.PartNo + '-' + Estim.Descrip " &
                                             "As PartInfo From (((OpScheduling INNER JOIN OrderScheduling ON OpScheduling.JobNo = OrderScheduling.JobNo) INNER JOIN " &
                                             "OrderDet ON OpScheduling.JobNo = OrderDet.JobNo) INNER JOIN Estim ON OrderDet.PartNo = Estim.PartNo) " &
                                             "WHERE OpScheduling.CurrentMachine = @machine And OrderDet.Status = 'OPEN' ", machconn)

            ' Dim machcomm As New OleDbCommand("SELECT JobNo, StepNo, EstimStartDate, EstimEndDate, Material FROM OpScheduling WHERE CurrentMachine = @machine", machconn)
            Dim machparam As New OleDbParameter("@machine", OleDbType.WChar)
            machparam.Value = machinename
            machcomm.Parameters.Add(machparam)


            Try

                If poolconnection <> True Then

                    machconn.Open()
                End If
                Dim machoptable As New DataTable
                Dim machadapter As New OleDbDataAdapter(machcomm)
                machadapter.Fill(machoptable)

                If poolconnection <> True Then

                    machconn.Close()
                End If

                Return machoptable

            Catch ex As Exception
                MsgBox(ex.Message)
                machconn.Close()
                Return Nothing
            End Try


        End If



    End Function
    ''' <summary>
    ''' Gets the jobs according to a search value that are open and have a machine assigned. 
    ''' </summary>
    ''' <param name="jobno"></param>
    ''' <returns></returns>
    Public Function GetJobsFromJobno(jobno As String)
        Dim machconn As New OleDbConnection(My.Settings.E2Database)

        Dim machcomm As New OleDbCommand("SELECT DISTINCT OrderDet.JobNo, Estim.PartNo + '-' + Estim.Descrip As PartInfo " &
                                         "From (((OrderDet LEFT JOIN OrderScheduling ON OrderDet.JobNo = OrderScheduling.JobNo) LEFT JOIN " &
                                         "OpScheduling ON OpScheduling.JobNo = OrderDet.JobNo) LEFT JOIN Estim ON OrderDet.PartNo = Estim.PartNo) " &
                                         "WHERE OrderDet.Status = 'OPEN' AND (OrderDet.JobNo Like ('%' & @jobno & '%') OR OrderDet.PartDesc LIKE ('%' & @jobno & '%') OR OrderDet.PartNo LIKE ('%' & @jobno & '%'))", machconn)

        machcomm.Parameters.AddWithValue("@jobno", jobno)

        Try
            machconn.Open()
            Dim machoptable As New DataTable
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machoptable)
            machconn.Close()
            Return machoptable

        Catch ex As Exception
            MsgBox(ex.Message)
            machconn.Close()
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get the info for steps using job number
    ''' ItemOrder: 0. Jobno, 1.Step, 2.StartDate, 3.EndDate, 4.Material, 5.Programming, 6.Tooling, 7.Completed, 
    ''' 8.DueDate, 9.PartInfo, 10. Partno-Descrip, 11.Machine
    ''' </summary>
    ''' <param name="machinename"></param>
    ''' <param name="jobnoonly"></param>
    ''' <returns></returns>
    Public Function GetJOBOps(jobno As String) As DataTable
        Dim machconn As New OleDbConnection(My.Settings.E2Database)
        Dim machcomm As New OleDbCommand("SELECT DISTINCT OpScheduling.JobNo, OpScheduling.StepNo, OpScheduling.EstimStartDate, OpScheduling.EstimEndDate, " &
                                         "OrderScheduling.Material, Programming, Tooling, Completed, OrderDet.DueDate, Estim.PartNo + '-' + Estim.Descrip " &
                                         "As PartInfo, OpScheduling.CurrentMachine FROM (((OpScheduling INNER JOIN OrderScheduling ON OpScheduling.JobNo = OrderScheduling.JobNo) INNER JOIN " &
                                         "OrderDet ON OpScheduling.JobNo = OrderDet.JobNo) INNER JOIN Estim ON OrderDet.PartNo = Estim.PartNo) " &
                                         "WHERE OpScheduling.JobNo = @jobno And OrderDet.Status = 'OPEN' AND (OpScheduling.CurrentMachine <> '' AND OpScheduling.CurrentMachine <> ' ' AND OpScheduling.CurrentMachine IS NOT NULL) ORDER BY OpScheduling.StepNo ", machconn)

        machcomm.Parameters.AddWithValue("@jobno", jobno)


        Try
            machconn.Open()
            Dim machoptable As New DataTable
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machoptable)
            machconn.Close()
            Return machoptable

        Catch ex As Exception
            MsgBox(ex.Message)
            machconn.Close()
            Return Nothing
        End Try


    End Function
    Public Function getHolidayList(ByVal vYear As Integer) As List(Of Date)

        Dim FirstWeek As Integer = 1
        Dim SecondWeek As Integer = 2
        Dim ThirdWeek As Integer = 3
        Dim FourthWeek As Integer = 4
        Dim LastWeek As Integer = 5

        Dim HolidayList As New List(Of Date)

        '   http://www.usa.gov/citizens/holidays.shtml      
        '   http://archive.opm.gov/operating_status_schedules/fedhol/2013.asp

        ' New Year's Day            Jan 1
        HolidayList.Add(DateSerial(vYear, 1, 1))
        'Good Friday
        Dim goodfr As DateTime = EasterSunday(vYear).AddDays(-2)
        HolidayList.Add(DateSerial(vYear, goodfr.Month, goodfr.Day))


        ' Memorial Day          last Mon in May
        HolidayList.Add(GetNthDayOfNthWeek(DateSerial(vYear, 5, 1), DayOfWeek.Monday, LastWeek))

        ' Independence Day      July 4
        HolidayList.Add(DateSerial(vYear, 7, 4))

        ' Labor Day             first Mon in Sept
        HolidayList.Add(GetNthDayOfNthWeek(DateSerial(vYear, 9, 1), DayOfWeek.Monday, FirstWeek))

        ' Thanksgiving Day      fourth Thur in Nov
        HolidayList.Add(GetNthDayOfNthWeek(DateSerial(vYear, 11, 1), DayOfWeek.Friday, FourthWeek))
        ' Thanksgiving Day      fourth Thur in Nov
        HolidayList.Add(GetNthDayOfNthWeek(DateSerial(vYear, 11, 1), DayOfWeek.Thursday, FourthWeek))

        ' Christmas Day         Dec 25
        HolidayList.Add(DateSerial(vYear, 12, 24))
        ' Christmas Day         Dec 25
        HolidayList.Add(DateSerial(vYear, 12, 25))

        'saturday holidays are moved to Fri; Sun to Mon
        For i As Integer = 0 To HolidayList.Count - 1
            Dim dt As Date = HolidayList(i)
            If dt.DayOfWeek = DayOfWeek.Saturday Then
                HolidayList(i) = dt.AddDays(-1)
            End If
            If dt.DayOfWeek = DayOfWeek.Sunday Then
                HolidayList(i) = dt.AddDays(1)
            End If
        Next

        'return
        Return HolidayList

    End Function
    Private Function EasterSunday(ByVal year As Integer) As DateTime


        Dim day As Integer = 0
        Dim month As Integer = 0

        Dim g As Integer = year Mod 19
        Dim c As Integer = year / 100
        Dim h As Integer = (c - Int((c / 4)) - Int((8 * c + 13) / 25) + 19 * g + 15) Mod 30
        Dim i As Integer = h - Int((h / 28)) * 1 - (Int((h / 28)) * Int((29 / (h + 1))) * Int(((21 - g) / 11)))

        day = i - year + (Int((year / 4)) + i + 2 - c + Int((c / 4)) Mod 7) + 28
        month = 3

        If (day > 31) Then

            month = month + 1
            day = day - 31
        End If



        Return New DateTime(month, day, year)
    End Function

    Private Function GetNthDayOfNthWeek(ByVal dt As Date, ByVal DayofWeek As Integer, ByVal WhichWeek As Integer) As Date
        'specify which day of which week of a month and this function will get the date
        'this function uses the month and year of the date provided

        'get first day of the given date
        Dim dtFirst As Date = DateSerial(dt.Year, dt.Month, 1)

        'get first DayOfWeek of the month
        Dim dtRet As Date = dtFirst.AddDays(6 - dtFirst.AddDays(-(DayofWeek + 1)).DayOfWeek)

        'get which week
        dtRet = dtRet.AddDays((WhichWeek - 1) * 7)

        'if day is past end of month then adjust backwards a week
        If dtRet >= dtFirst.AddMonths(1) Then
            dtRet = dtRet.AddDays(-7)
        End If

        'return
        Return dtRet

    End Function


    Public Sub GetMachineHours(ByVal Machine As String)




    End Sub

    Public Sub DetermineMachine(ByVal Operation As String, ByVal SugMachine As String)


    End Sub
    Public Sub InsertOp(Machine As String, Jobno As String, stepno As Integer)



    End Sub


    Public Sub showopinfo(Operationholder As Operation)


    End Sub

    Public Sub AssignMachineWizardSLACKTIME(Generations As Integer)
        'Reference :https://developers.google.com/optimization/scheduling/job_shop
        'Reference :http://courses.washington.edu/ie337/Job%20Shop%20Scheduling.pdf

        'Please see python solution 
        'Start time of job is tdate (Unit is date
        'Processing time of job is the ptime (Unit is hrs) 
        '   To convert to date you simply divide by 16 hrs
        '
        '
        'The objective of the solver is to minimize slacktime overage

        '0. a. Get ALL THE SEQUENCES (Except for those that are locked)
        Dim MasterSeqTable As DataTable
        'MasterSeqTable = GetUnassigned(True, )
        '0. b. Get ALL THE MACHINES (AND assigned hour value based only on locked sequences)
        Dim MachineTable As DataTable

        '0. c. GET ALL THE MACHINE CAPABILITIES

        '1. a. Get Slack time for each Job

        '1. b. Assign to sequences in the datatable

        '2.  a. Sort Sequences based on slack time and step Number

        '3. ASSIGN TOP SEQUENCE

        '3. a. i. Get the best machine based on lowest assigned hours

        '3. a. ii. Assign machine to sequence

        '3. b. Update Machine Hours

        '3. c. OPTIONAL: Update SLACK JOB requirements

        '3. d. DELETE ROW FROM datatable

        'REPEAT STEP 3.

    End Sub



    ''' <summary>
    ''' "SELECT OrderDet.JobNo, OrderDet.PartNo, OrderDet.PartDesc, OrderScheduling.Material, OrderDet.QtyToMake, OrderDet.DueDate, 
    ''' </summary>
    ''' <param name="olevalue"></param>
    ''' <returns></returns>
    Public Function Getjobview(olevalue As Boolean) As DataTable

        Dim jobtable As New DataTable
        Dim jobstring As String

        If olevalue = True Then


            '  jobstring = "SELECT OrderDet.JobNo As JobNo, OrderDet.PartNo As PartNo, OrderDet.PartDesc As PartDesc, OrderScheduling.Material,OrderDet.QtyToMake, OrderDet.DueDate, OrderScheduling.MaterialPrepped, OrderDet.ProdCode, OrderDet.TravPrinted FROM (OrderDet LEFT JOIN OrderScheduling ON OrderDet.JobNo = OrderScheduling.JobNo) WHERE OrderDet.Status = 'Open' AND OrderDet.OrderNo <> '' " &
            '          "And EXISTS (SELECT 1 FROM OrderRouting WHERE OrderRouting.Jobno = OrderDet.JobNo) AND OrderDet.User_Text1 <> 'N' ORDER BY OrderDet.DueDate ASC, OrderDet.OrderNo ASC"

            jobstring = "SELECT OrderDet.JobNo As JobNo, OrderDet.PartNo As PartNo, OrderDet.PartDesc As PartDesc, OrderScheduling.Material,OrderDet.QtyToMake, OrderDet.DueDate As OrderDueDate, OrderScheduling.MaterialPrepped, OrderDet.ProdCode As ProductCode, OrderDet.TravPrinted, OrderDet.User_Text2 As ShellOrder FROM (OrderDet LEFT JOIN OrderScheduling ON OrderDet.JobNo = OrderScheduling.JobNo) WHERE OrderDet.Status = 'Open' AND OrderDet.OrderNo <> '' " &
                    "AND OrderDet.User_Text1 <> 'N' ORDER BY OrderDet.DueDate ASC, OrderDet.OrderNo ASC"

            Dim dbaseconn As New OleDbConnection(My.Settings.E2Database)
            Dim dbasecomm As New OleDbCommand(jobstring, dbaseconn)

            Try
                Dim dbaseadapter As New OleDbDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)
            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try

        Else
            Dim dbaseconn As New SqlConnection(My.Settings.E2Database)
            Dim dbasecomm As New SqlCommand("Select JobNo, PartNo, PartDesc FROM OrderDet", dbaseconn)

            Try
                Dim dbaseadapter As New SqlDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)

            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try
        End If

        Return jobtable

    End Function
    ''' <summary>
    '''     ''' </summary>
    ''' <param name="olevalue">OLE database or not</param>
    ''' <param name="Startdate"></param>
    ''' <param name="enddate"></param>
    ''' <returns></returns>
    Public Function Getclosedjobs(olevalue As Boolean, Startdate As DateTime, enddate As DateTime)
        Dim jobtable As New DataTable
        Dim jobstring As String

        If olevalue = True Then


            '  jobstring = "SELECT OrderDet.JobNo As JobNo, OrderDet.PartNo As PartNo, OrderDet.PartDesc As PartDesc, OrderScheduling.Material,OrderDet.QtyToMake, OrderDet.DueDate, OrderScheduling.MaterialPrepped, OrderDet.ProdCode, OrderDet.TravPrinted FROM (OrderDet LEFT JOIN OrderScheduling ON OrderDet.JobNo = OrderScheduling.JobNo) WHERE OrderDet.Status = 'Open' AND OrderDet.OrderNo <> '' " &
            '          "And EXISTS (SELECT 1 FROM OrderRouting WHERE OrderRouting.Jobno = OrderDet.JobNo) AND OrderDet.User_Text1 <> 'N' ORDER BY OrderDet.DueDate ASC, OrderDet.OrderNo ASC"

            jobstring = "SELECT OrderDet.JobNo As JobNo, OrderDet.PartNo As PartNo, OrderDet.PartDesc As PartDesc, OrderScheduling.Material,OrderDet.QtyToMake, OrderDet.DueDate As OrderDueDate, OrderScheduling.MaterialPrepped, OrderDet.ProdCode As ProductCode, OrderDet.TravPrinted, OrderDet.User_Text2 As ShellOrder FROM (OrderDet LEFT JOIN OrderScheduling ON OrderDet.JobNo = OrderScheduling.JobNo) WHERE OrderDet.Status = 'Closed' AND OrderDet.OrderNo <> '' " &
                    "AND OrderDet.User_Text1 <> 'N' AND (OrderDet.DateFinished >= @startdate AND OrderDet.DateFinished<=@enddate) ORDER BY OrderDet.DueDate ASC, OrderDet.OrderNo ASC"

            Dim dbaseconn As New OleDbConnection(My.Settings.E2Database)
            Dim dbasecomm As New OleDbCommand(jobstring, dbaseconn)
            dbasecomm.Parameters.AddWithValue("@startdate", Startdate)
            dbasecomm.Parameters.AddWithValue("@enddate", enddate)
            Try
                Dim dbaseadapter As New OleDbDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)
            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try

        Else
            Dim dbaseconn As New SqlConnection(My.Settings.E2Database)
            Dim dbasecomm As New SqlCommand("Select JobNo, PartNo, PartDesc FROM OrderDet", dbaseconn)

            Try
                Dim dbaseadapter As New SqlDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)

            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try
        End If

        Return jobtable


    End Function

    Public Function Getmatdatejobs(Startdate As DateTime, enddate As DateTime)

        Dim jobtable As New DataTable
        Dim jobstring As String

        jobstring = "SELECT DISTINCT OrderDet.JobNo As JobNo, OrderDet.PartNo As PartNo, OrderDet.PartDesc As PartDesc, OrderScheduling.Material, OrderDet.QtyToMake, OrderDet.DueDate As OrderDueDate, OrderScheduling.MaterialPrepped, OrderDet.ProdCode As ProductCode, OrderDet.TravPrinted, OrderDet.User_Text2 As ShellOrder FROM (((POReleases LEFT JOIN OrderDet On POReleases.JobNo=OrderDet.JobNo) LEFT JOIN OrderScheduling ON POReleases.JobNo = OrderScheduling.JobNo) LEFT JOIN PODet ON POReleases.PONum = PODet.PONum) WHERE OrderDet.Status = 'Open' AND OrderDet.OrderNo <> '' " &
                    "AND OrderDet.User_Text1 <> 'N' AND (POReleases.DueDate > @startdate AND POReleases.DueDate<@enddate) AND ISNULL(POReleases.DateReceived) AND PODet.OutsideService = 'N'"


        Dim dbaseconn As New OleDbConnection(My.Settings.E2Database)
        Dim dbasecomm As New OleDbCommand(jobstring, dbaseconn)
        dbasecomm.Parameters.AddWithValue("@startdate", Startdate)
        dbasecomm.Parameters.AddWithValue("@enddate", enddate)

        Try
            Dim dbaseadapter As New OleDbDataAdapter(dbasecomm)
            dbaseadapter.Fill(jobtable)
        Catch ex As Exception
            dbaseconn.Close()
            MsgBox(ex.Message)
        End Try

        Return jobtable


    End Function
    ''' <summary>
    ''' 0:WorkCntr, 1:StepNo, 2:OperCode, 3:CycleTime, 4:CycleUnit, " 
    ''' "5:SetupTime, 6:SetupUnit, 7:WorkOrVend, 8:LeadTime , 9: Vwndor
    ''' </summary>
    ''' <param name="olevalue"></param>
    ''' <param name="partno"></param>
    ''' <returns></returns>
    Public Function GetPartSteps(olevalue As Boolean, partno As String) As DataTable
        Dim jobtable As New DataTable


        If olevalue = True Then
            Dim dbaseconn As New OleDbConnection(My.Settings.E2Database)
            ' Dim dbasecomm As New OleDbCommand("SELECT JobNo, StepNo, OperCode, TotHrsLeft,  FROM OrderRouting WHERE JobNo = @jobno", dbaseconn)
            Dim dbasecomm As New OleDbCommand("SELECT WorkCntr, StepNo, OperCode, CycleTime, CycleUnit, " &
                "SetupTime, TimeUnit, WorkOrVend, LeadTime, VendCode From Routing WHERE PartNo = @partno ORDER BY StepNo ASC", dbaseconn)

            dbasecomm.Parameters.AddWithValue("@partno", partno)

            Try
                Dim dbaseadapter As New OleDbDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)
            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try

        Else
            Dim dbaseconn As New SqlConnection(My.Settings.E2Database)
            'Dim dbasecomm As New SqlCommand("SELECT JobNo, StepNo,  OperCode, TotHrsLeft FROM OrderRouting WHERE JobNo = @jobno", dbaseconn)
            Dim dbasecomm As New SqlCommand("SELECT OpScheduling.JobNo, OpScheduling.StepNo, OperCode, TotHrsLeft, OpScheduling.EstimStartDate, OpScheduling.EstimEndDate, OrderScheduling.Material, Programming, Tooling, Completed From (((OpScheduling INNER JOIN OrderScheduling ON OpScheduling.JobNo = OrderScheduling.JobNo) INNER JOIN OrderDet ON OpScheduling.JobNo = OrderDet.JobNo) INNER JOIN Estim ON OrderDet.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'OPEN'", dbaseconn)

            dbasecomm.Parameters.AddWithValue("@jobno", partno)
            Try
                Dim dbaseadapter As New SqlDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)

            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try
        End If

        Return jobtable
    End Function
    ''' <summary>
    ''' 0:jobno, 1:StepNo, 2:OperCode, 3:HrsLeft, 4:CycleUnit, " 
    ''' "5:SetupTime, 6:SetupUnit, 7:WorkOrVend, 8:LeadTime , 9: Vwndor
    ''' </summary>
    ''' <param name="olevalue"></param>
    ''' <param name="jobno"></param>
    ''' <returns></returns>
    Public Function GetJobSteps(olevalue As Boolean, jobno As String) As DataTable
        Dim jobtable As New DataTable
        If olevalue = True Then
            Dim dbaseconn As New OleDbConnection(My.Settings.E2Database)
            ' Dim dbasecomm As New OleDbCommand("SELECT JobNo, StepNo, OperCode, TotHrsLeft,  FROM OrderRouting WHERE JobNo = @jobno", dbaseconn)
            ' Dim dbasecomm As New OleDbCommand("SELECT OrderRouting.JobNo, OrderRouting.StepNo, OrderRouting.OperCode, OrderRouting.TotHrsLeft, OrderRouting.WorkOrVend, OpScheduling.EstimStartDate, OpScheduling.EstimEndDate From ((OrderRouting LEFT JOIN OpScheduling ON OrderRouting.JobNo =OpScheduling.JobNo) INNER JOIN OrderDet On OrderRouting.Jobno = OrderDet.Jobno )  WHERE OrderDet.Status = 'OPEN' And OrderRouting.JobNo = @jobno", dbaseconn)
            Dim dbasecomm As New OleDbCommand("SELECT OrderRouting.WorkCntr, OrderRouting.StepNo, OrderRouting.OperCode, CycleTime, CycleUnit, " &
                "SetupTime, TimeUnit, WorkOrVend, LeadTime, VendCode, OpScheduling.Completed From OrderRouting LEFT JOIN OpScheduling ON OpScheduling.JobNo = OrderRouting.JobNo AND OpScheduling.StepNo = OrderRouting.StepNo WHERE OrderRouting.JobNo = @jobno ORDER BY OrderRouting.StepNo ASC", dbaseconn)

            dbasecomm.Parameters.AddWithValue("@jobno", jobno)

            Try
                Dim dbaseadapter As New OleDbDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)
            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try

        Else
            Dim dbaseconn As New SqlConnection(My.Settings.E2Database)
            'Dim dbasecomm As New SqlCommand("SELECT JobNo, StepNo,  OperCode, TotHrsLeft FROM OrderRouting WHERE JobNo = @jobno", dbaseconn)
            Dim dbasecomm As New SqlCommand("SELECT OpScheduling.JobNo, OpScheduling.StepNo, OperCode, TotHrsLeft, OpScheduling.EstimStartDate, OpScheduling.EstimEndDate, OrderScheduling.Material, Programming, Tooling, Completed From (((OpScheduling LEFT JOIN OrderScheduling ON OpScheduling.JobNo = OrderScheduling.JobNo) INNER JOIN OrderDet ON OpScheduling.JobNo = OrderDet.JobNo) INNER JOIN Estim ON OrderDet.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'OPEN'", dbaseconn)

            dbasecomm.Parameters.AddWithValue("@jobno", jobno)
            Try
                Dim dbaseadapter As New SqlDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)

            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try
        End If

        Return jobtable
    End Function

    Public Function PartJobMaterial(olevalue As Boolean, Jobno As String)

        Dim jobtable As New DataTable
        If olevalue = True Then
            Dim dbaseconn As New OleDbConnection(My.Settings.E2Database)
            Dim dbasecomm As New OleDbCommand("SELECT JobNo, PartNo, Description, BinLoc1, QtyPosted1 From JobMaterials WHERE OrderDet.Status = 'OPEN' And OrderRouting.JobNo = @jobno", dbaseconn)
            dbasecomm.Parameters.AddWithValue("@jobno", Jobno)

            Try
                Dim dbaseadapter As New OleDbDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)
            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try

        Else
            Dim dbaseconn As New SqlConnection(My.Settings.E2Database)
            'Dim dbasecomm As New SqlCommand("SELECT JobNo, StepNo,  OperCode, TotHrsLeft FROM OrderRouting WHERE JobNo = @jobno", dbaseconn)
            Dim dbasecomm As New SqlCommand("SELECT JobNo, PartNo, Description, BinLoc1, QtyPosted1 From JobMaterials WHERE OrderDet.Status = 'OPEN' And OrderRouting.JobNo = @jobno", dbaseconn)

            dbasecomm.Parameters.AddWithValue("@jobno", Jobno)
            Try
                Dim dbaseadapter As New SqlDataAdapter(dbasecomm)
                dbaseadapter.Fill(jobtable)

            Catch ex As Exception
                dbaseconn.Close()
                MsgBox(ex.Message)
            End Try
        End If

        Return jobtable

    End Function
    ''' <summary>
    ''' Gets all the unnassgned operations that aren't closed and on the schedule yet
    ''' </summary>
    ''' <param name="olevalue">Say whether to use the E2 access database</param>
    ''' <param name="searchval">Search value for the job no</param>
    ''' <param name="opcodestring"></param>
    ''' <param name="Showvendors"></param>
    ''' <returns></returns>
    Public Function GetUnassigned(olevalue As Boolean, searchval As String, opcodestring As String, Showvendors As Boolean) As DataTable

        Dim unassignedtable As New DataTable
        Dim Vendorstring As String
        If Showvendors = True Then
            Vendorstring = ""
        Else
            Vendorstring = " AND OrderRouting.WorkOrVend = 0"
        End If
        If olevalue = True Then



            Dim databaseconn As New OleDbConnection(My.Settings.E2Database)
            Dim dbcomm As New OleDbCommand()
            dbcomm.Connection = databaseconn

            If searchval = "" Then
                dbcomm.CommandText = "Select OrderDet.JobNo, OrderRouting.StepNo, OrderRouting.PartNo," &
                " OrderDet.PartDesc, IIf(OrderRouting.WorkOrVend = 1, OrderRouting.VendCode, OrderRouting.OperCode), OrderDet.DueDate, IIf(OrderRouting.WorkOrVend = 1, OrderRouting.LeadTime*24, OrderRouting.TotHrsLeft) , OrderScheduling.Material, OpScheduling.Completed" &
                " FROM (((OrderRouting" &
                " INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo) LEFT JOIN OrderScheduling ON OrderRouting.JobNo = OrderScheduling.JobNo)" &
                " LEFT JOIN OpScheduling ON (OrderRouting.JobNo = OpScheduling.JobNo AND OrderRouting.StepNo = OpScheduling.StepNo))" &
                "WHERE OrderDet.Status = 'Open' AND (" & opcodestring & ") AND OrderDet.JobOnHold = 'N'" & Vendorstring & " AND OrderDet.User_Text1 <> 'N' AND OrderRouting.JobNo <> '' AND (OpScheduling.CurrentMachine ='' OR OpScheduling.CurrentMachine = ' ' Or OpScheduling.CurrentMachine IS NULL)"
                '

            Else
                dbcomm.CommandText = "Select OrderDet.JobNo, OrderRouting.StepNo, OrderRouting.PartNo," &
             " OrderDet.PartDesc, IIf(OrderRouting.WorkOrVend = 1, OrderRouting.VendCode, OrderRouting.OperCode), OrderDet.DueDate, OrderRouting.TotHrsLeft, OrderScheduling.Material, OpScheduling.Completed" &
             " FROM (((OrderRouting" &
             " INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo) LEFT JOIN OrderScheduling ON OrderRouting.JobNo = OrderScheduling.JobNo)" &
             " LEFT JOIN OpScheduling ON (OrderRouting.JobNo = OpScheduling.JobNo AND OrderRouting.StepNo = OpScheduling.StepNo))" &
             "WHERE OrderDet.Status = 'Open' AND (" & opcodestring & ") AND OrderDet.JobOnHold = 'N'" & Vendorstring & "AND OrderDet.User_Text1 <> 'N' AND (OrderRouting.JobNo LIKE ('%' & @jobno & '%') OR OrderDet.PartDesc LIKE ('%' & @jobno & '%') OR OrderRouting.PartNo LIKE ('%' & @jobno & '%')) AND (OpScheduling.CurrentMachine ='' OR OpScheduling.CurrentMachine = ' ' Or OpScheduling.CurrentMachine IS NULL)"
                'AND OrderDet.User_Text1 <> 'N'


                dbcomm.Parameters.AddWithValue("@jobno", searchval)
            End If
            Try
                Dim scheduleadapt As New OleDbDataAdapter(dbcomm)
                scheduleadapt.Fill(unassignedtable)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Else

        End If

        Return unassignedtable




    End Function

    Public Function Checkprogram(stepno As Integer, partno As String) As Boolean

        'Get and bind all the files that are associated with the setup
        Dim filesquery As String
        filesquery = "SELECT 1 From (Operation INNER JOIN setupfiles ON Operation.ID = setupfiles.OperationID) WHERE Operation.PartID = @part AND Operation.PartOpNo = @opid AND setupfiles.Program = 'TRUE'"

        'Define the connection to the shopdb
        Dim filesconn As New SqlConnection(My.Settings.SHOPDB)

        'Define the comm
        Dim filescomm As New SqlCommand(filesquery, filesconn)

        'Define the parameters
        filescomm.Parameters.AddWithValue("@opid", stepno)
        filescomm.Parameters.AddWithValue("@part", partno)
        Dim filesadapter As New SqlDataAdapter(filescomm)

        'file table for data
        Dim filetable As New DataTable

        Try
            filesadapter.Fill(filetable)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If filetable.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If



    End Function
    Public Function GetMachFromJob(ByRef JobNo As String) As DataTable
        Dim sqlconn As New OleDbConnection(My.Settings.E2Database)
        Dim sqlcomm As New OleDbCommand
        sqlcomm.Connection = sqlconn
        Dim commtext As String
        If JobNo = "" Or IsNothing(JobNo) Then
            Return Nothing

        End If
        commtext = "SELECT DISTINCT CurrentMachine FROM (((OrderRouting" &
             " INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo) LEFT JOIN OrderScheduling ON OrderRouting.JobNo = OrderScheduling.JobNo)" &
             " LEFT JOIN OpScheduling ON (OrderRouting.JobNo = OpScheduling.JobNo AND OrderRouting.StepNo = OpScheduling.StepNo))" &
             " WHERE (OrderRouting.JobNo Like ('%' & @jobno & '%') OR OrderDet.PartDesc LIKE ('%' & @jobno & '%') OR OrderRouting.PartNo LIKE ('%' & @jobno & '%'))" &
             " And CurrentMachine Is Not NULL And CurrentMachine <> '' AND CurrentMachine <>' '"
        sqlcomm.CommandText = commtext
        sqlcomm.Parameters.AddWithValue("@jobno", JobNo)

        Try
            sqlconn.Open()
            Dim sqladapter As New OleDbDataAdapter(sqlcomm)
            Dim machtable As New DataTable
            sqladapter.Fill(machtable)
            sqlconn.Close()
            Return machtable


        Catch ex As Exception

            sqlconn.Close()
            MsgBox(ex.Message)

        End Try



    End Function



    Public Sub DeleteOpAssociation(ByRef Machineid As Integer, ByRef Operationid As Integer)



    End Sub
    Public Function GetE2Operations(partholder As Part) As DataTable

        Try
            Dim routingstring As String = "SELECT StepNo, WorkCntr, OperCode, Descrip FROM Routing WHERE PartNo = @partno and WorkOrVend = 0"

            Dim routingconn As New OleDbConnection(My.Settings.E2Database)
            Dim routingcommand As New OleDbCommand(routingstring, routingconn)
            Dim partnoparameter As New OleDbParameter("@partno", OleDbType.WChar, 30)
            partnoparameter.Value = partholder.Partno

            'Add the parameter
            routingcommand.Parameters.Add(partnoparameter)

            Dim routingadapter As New OleDbDataAdapter(routingcommand)


            Dim dbtable As New DataTable

            routingadapter.Fill(dbtable)

            Return dbtable

        Catch ex As Exception
            MsgBox("Error finding routing info: " & ex.Message)
            Return Nothing

        End Try



    End Function
    Public Sub AddAlle2Operations(partid As Part)
        'Adds all the operations from E2
        Dim optable As New DataTable
        optable = GetE2Operations(partid)

        If IsNothing(optable) = False Then
            If optable.Rows.Count > 0 Then
                For Each operationrow As DataRow In optable.Rows
                    'Add each operation

                    'Order of datatable---0 StepNo, 1 WorkCntr, 2 OperCode, 3 Descrip
                    Dim OPint As Integer = operationrow.Item(0)
                    Dim opidvalue As Integer = 0
                    Dim description As String = operationrow.Item(3)


                    'insertion string
                    Dim insertstring As String
                    insertstring = "INSERT INTO Operation (PartId, Description, PartOpNo) VALUES (@partid, @description, @opno) SELECT SCOPE_IDENTITY()"
                    Dim opconn As New SqlConnection(My.Settings.SHOPDB)
                    Dim opcomm As New SqlCommand(insertstring, opconn)
                    'Define the parameters
                    opcomm.Parameters.AddWithValue("@partid", partid.Partno)
                    opcomm.Parameters.AddWithValue("@description", description)
                    opcomm.Parameters.AddWithValue("@opno", OPint)

                    'Call the scalar query
                    opidvalue = ScalarSelect(opcomm)

                    '****Add setup for operation
                    AddSetup(opidvalue, partid.Partno, description)

                Next
            End If
        End If

    End Sub

End Module
