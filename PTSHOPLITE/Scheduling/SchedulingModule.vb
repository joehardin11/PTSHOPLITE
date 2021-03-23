Imports System.Data.SqlClient
Imports System.Data.OleDb


Module SchedulingModule
    ''' <summary>
    ''' Provide the sales orders to transfer into a sales order
    ''' </summary>
    ''' <param name="salesordertable"></param>
    ''' <returns></returns>
    Public Function MakeManufOrder(salesordertable As DataTable) As Integer
        'Get the info for each sales order 


        'Get the estimated due date

        'Get the estimated qtys
        Dim stockqty, orderqty, makeqty As Integer


        'Get the routing info
        Dim routingtable As DataTable


        'Get the estimated material list


        'Create summary order form for approval
        Dim order As Integer = -1

        'If the order is approved, create the order and send it to the new database


        'Get the scalar info for the order to get the actual number


        Return order

    End Function


    Public Function RoutingMeetsDuedate(jobno As String, opno As Integer, startdate As Date, duedate As Date, ophrs As Double, oneshift As Boolean) As Boolean

        If My.Settings.DueDateWarnings = False Then
            Return True
        End If

        Dim hrval As Double

        'Job number Search
        Dim opconn As New OleDbConnection(My.Settings.E2Database)

        Dim opcomm As New OleDbCommand("Select SUM(TotHrsLeft) As ophours, SUM(LeadTime) As leaddays FROM OrderRouting WHERE Jobno = @job AND StepNo > @step", opconn)

        opcomm.Parameters.AddWithValue("@job", jobno)
        opcomm.Parameters.AddWithValue("@step", opno)

        Try
            opconn.Open()
            Dim hrtable As New DataTable
            Dim machadapter As New OleDbDataAdapter(opcomm)
            machadapter.Fill(hrtable)
            opconn.Close()
            If hrtable.Rows.Count > 0 Then

                hrval = NotNull(hrtable.Rows(0).Item(0), 0) + ophrs
                hrval = hrval + (NotNull(hrtable.Rows(0).Item(1), 0) * My.Settings.Workdayhours)
            Else
                hrval = 0
            End If

        Catch ex As Exception
            MsgBox("Due Date Check Exception: " & ex.Message)
            opconn.Close()

        End Try



        Dim enddate As Date = Add_businessdays(startdate, hrval, oneshift)

        If enddate >= duedate Then

            If MsgBox("Job: " & jobno & " Step: " & opno.ToString & " is scheduled to have the job complete: " & enddate.ToShortDateString & " AFTER the Due Date: " & duedate.ToShortDateString & ". Continue?", vbYesNo) = vbYes Then
                Return True
            Else
                Return False
            End If

        Else
            Return True
        End If



    End Function
    Public Function NoMaterialConflict(Jobno As String, DateStart As Date, stepno As String) As Boolean

        'Material conflict
        If My.Settings.MaterialWarnings = False Then
            Return True
        End If

        'Job number Search
        Dim opconn As New OleDbConnection(My.Settings.E2Database)

        Dim opcomm As New OleDbCommand("SELECT POReleases.PONum, POReleases.DueDate, POReleases.DateReceived, JobMaterials.Description FROM (JobMaterials INNER JOIN POReleases ON JobMaterials.PONum = POReleases.PONum AND JobMaterials.JobNo = POReleases.JobNo) WHERE JobMaterials.JobNo = @job AND JobMaterials.MainPart = 'N' ORDER BY DueDate DESC", opconn)
        opcomm.Parameters.AddWithValue("@job", Jobno)

        Dim dateholder As Date
        dateholder = Today.AddYears(-20)
        Dim datereceived As Date

        Try
            opconn.Open()
            Dim optable As New DataTable
            Dim machadapter As New OleDbDataAdapter(opcomm)
            machadapter.Fill(optable)
            opconn.Close()

            If optable.Rows.Count > 0 Then

                dateholder = NotNull(optable.Rows(0).Item(1), dateholder)
                datereceived = Nothing
                datereceived = NotNull(optable.Rows(0).Item(2), CDate("1/1/1900"))

                If dateholder > DateStart And datereceived.ToShortDateString = "1/1/1900" Then
                    Dim materialname As String

                    materialname = NotNull(optable.Rows(0).Item(3), "N/a")
                    If MsgBox("Job: " & Jobno & vbNewLine & "Step: " & stepno & vbNewLine & " Material:" & vbNewLine & optable.Rows(0).Item(3).ToString & vbNewLine & "Is due:    " & dateholder.ToShortDateString & " (After the start of this routing step.)" & vbNewLine & "Continue?", vbYesNo) = vbYes Then
                        Return True

                    Else

                        Return False
                    End If

                End If

                Return True

            Else
                Return True

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            opconn.Close()

        End Try





    End Function
    Public Function Getavailabloptypes() As DataTable
        Dim opconn As New OleDbConnection(My.Settings.E2Database)

        Dim opcomm As New OleDbCommand("Select DISTINCT OperCode From OrderRouting", opconn)
        Try
            opconn.Open()
            Dim optable As New DataTable
            Dim machadapter As New OleDbDataAdapter(opcomm)
            machadapter.Fill(optable)
            opconn.Close()
            Return optable

        Catch ex As Exception
            MsgBox(ex.Message)
            opconn.Close()
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Provide rel counter and due date to update the value of the release and order
    ''' </summary>

    Public Sub UpdateRelDueDate(Duedate As DateTime, counter As Integer)

        Dim relduedateconn As New OleDbConnection(My.Settings.E2Database)
        Dim relduedatecomm As New OleDbCommand("UPDATE Releases Set DueDate=@duedate WHERE Counter = @counter", relduedateconn)
        relduedatecomm.Parameters.AddWithValue("@duedate", Duedate)
        relduedatecomm.Parameters.AddWithValue("@counter", counter)

        Try

            relduedateconn.Open()
            relduedatecomm.ExecuteNonQuery()
            relduedateconn.Close()

        Catch ex As Exception
            relduedateconn.Close()
            MsgBox(ex.Message)
        End Try

    End Sub



    ''' <summary>
    ''' Get the info for steps using job number
    ''' ItemOrder: Qty, DueDate, DateComplete, Comments, Counter
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOPReleases(opval As Operation) As DataTable
        Dim releaseconn As New OleDbConnection(My.Settings.E2Database)
        Dim releasequery As New OleDbCommand("Select Qty, DueDate, DateComplete, Comments, Counter FROM Releases WHERE JobNo = @jobno", releaseconn)
        releasequery.Parameters.AddWithValue("@jobno", opval.JobNo)
        Dim reltable As New DataTable

        Try
            releaseconn.Open()
            Dim reldatadaptor As New OleDbDataAdapter(releasequery)
            reldatadaptor.Fill(reltable)
            releaseconn.Close()

        Catch ex As Exception

            releaseconn.Close()
            MsgBox(ex.Message)

        End Try

        Return reltable

    End Function

    ''' <summary>
    ''' Get the info for steps using job number
    ''' ItemOrder: Qty, DueDate, DateComplete, Comments, Counter
    ''' </summary>
    ''' <returns></returns>
    Public Function GetJobReleases(jobno As String) As DataTable
        Dim releaseconn As New OleDbConnection(My.Settings.E2Database)
        Dim releasequery As New OleDbCommand("Select Qty, DueDate, DateComplete, Comments, Counter FROM Releases WHERE JobNo = @jobno", releaseconn)
        releasequery.Parameters.AddWithValue("@jobno", jobno)
        Dim reltable As New DataTable

        Try
            releaseconn.Open()
            Dim reldatadaptor As New OleDbDataAdapter(releasequery)
            reldatadaptor.Fill(reltable)
            releaseconn.Close()

        Catch ex As Exception

            releaseconn.Close()
            MsgBox(ex.Message)

        End Try

        Return reltable

    End Function
    Public Function PriorOpsScheduled() As String

    End Function
    Public Function PostOpHrs(ByVal jobno As String, ByVal opno As Integer) As Double
        Dim opdb As New OLEDatabase(My.Settings.E2Database)

        Dim hrtable As DataTable

        hrtable = opdb.getdatatable("Select SUM(TotHrsLeft) Then As ophours, SUM(LeadTime) As leaddays FROM OrderRouting WHERE Jobno = '" & jobno & "' AND StepNo > " & opno)
        Dim hrval As Integer
        If hrtable.Rows.Count > 0 Then

            hrval = hrtable.Rows(0).Item(0)
            hrval = hrval + (hrtable.Rows(0).Item(1) * My.Settings.Workdayhours)
        Else
            hrval = 0
        End If

        Return hrval


    End Function


    Public Function Setmachemployee(machine As String, shift As Integer, emply As String)
        Dim sqlconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim sqlcomm As SqlCommand
        If shift = 1 Then
            sqlcomm = New SqlCommand("UPDATE Machines SET shift1employee = @emply WHERE Name = @name", sqlconn)

        Else
            sqlcomm = New SqlCommand("UPDATE Machines SET shift2employee = @emply WHERE Name = @name", sqlconn)

        End If
        sqlcomm.Parameters.AddWithValue("@emply", emply)
        sqlcomm.Parameters.AddWithValue("@name", machine)

        Try
            sqlconn.Open()
            sqlcomm.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            sqlconn.Close()
            MsgBox(ex.Message)

        End Try


    End Function
    Public Function getmachineviewerops(machconn As OleDbConnection) As DataTable
        Dim machcomm As New OleDbCommand("SELECT TOP 5 OpScheduling.JobNo, OpScheduling.StepNo, OpScheduling.EstimStartDate, OpScheduling.EstimEndDate, OrderDet.DueDate, Estim.Descrip As PartInfo, CurrentMachine " &
            "From (((OpScheduling INNER JOIN OrderScheduling ON OpScheduling.JobNo = OrderScheduling.JobNo) INNER JOIN OrderDet ON OpScheduling.JobNo = OrderDet.JobNo) INNER JOIN Estim ON OrderDet.PartNo = Estim.PartNo) WHERE  OrderDet.Status = 'OPEN'  AND Completed = 0 ORDER BY OpScheduling.CurrentMachine, OpScheduling.EstimStartDate ASC", machconn)

        ' Dim machparam As New OleDbParameter("@machine", OleDbType.WChar)
        '    machparam.Value = machinename
        '  machcomm.Parameters.Add(machparam)


        Try
            If machconn.State <> ConnectionState.Open Then
                machconn.Open()
            End If
            Dim machoptable As New DataTable
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machoptable)

            Return machoptable

        Catch ex As Exception
            MsgBox(ex.Message)
            machconn.Close()
            Return Nothing
        End Try

    End Function

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
    Public Function getoperationcolor(duedate As Date, enddate As Date, completed As Boolean, material As Boolean, tooling As Boolean, program As Boolean) As Color

        If completed = True Then
            'If completed = true 
            Return Color.LightBlue
        End If
        If duedate > enddate Then
            If material = True Then
                If tooling = True Then
                    If program = True Then
                        Return My.Settings.jobplanneritemgoodcolor
                    Else
                        Return Color.Orange
                    End If
                Else
                    Return Color.Orange
                End If

            Else
                Return Color.Orange
            End If

        Else
            Return My.Settings.jobplanneritembadcolor
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

    Public Function Add_businessdays(ByVal starting_date_holder As Date, ByVal Total_hours As Double, Oneshift As Boolean) As Date
        Dim current_date As Date = starting_date_holder
        'Skip the weekend
        If current_date.DayOfWeek = DayOfWeek.Saturday Then
            current_date = current_date.AddDays(2)
        ElseIf current_date.DayOfWeek = DayOfWeek.Sunday Then
            current_date = current_date.AddDays(1)
        End If


        Dim int_days As Integer
        Dim total_days As Double
        Dim remainder As Double
        If Oneshift = True Then
            total_days = (Total_hours / (My.Settings.Dayworkhours / 2))
        Else
            total_days = (Total_hours / My.Settings.Dayworkhours)
        End If

        int_days = Math.Floor(total_days)
        remainder = total_days - int_days

        If int_days > 0 Then

            For j = 1 To int_days
                current_date = current_date.AddDays(1)

                If current_date.DayOfWeek = DayOfWeek.Saturday Then
                    current_date = current_date.AddDays(2)
                End If

            Next
        End If

        current_date = current_date.AddDays(remainder)
        Return current_date

    End Function

    Public Sub GetMachineHours(ByVal Machine As String)




    End Sub

    Public Sub DetermineMachine(ByVal Operation As String, ByVal SugMachine As String)


    End Sub
    Public Sub InsertOp(Machine As String, Jobno As String, stepno As Integer)



    End Sub

    Public Function DepartmentMachines(Department As String) As DataTable
        'Get the deparmentmachines
        Dim sqlconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim sqlcomm As New SqlCommand("SELECT Name, Id From Machines Where Department = @department", sqlconn)
        sqlcomm.Parameters.AddWithValue("@department", Department)

        Try
            sqlconn.Open()
            Dim machtable As New DataTable
            Dim machadapter As New SqlDataAdapter(sqlcomm)
            machadapter.Fill(machtable)
            sqlconn.Close()

            Return machtable

        Catch ex As Exception
            MsgBox(ex.Message)

            sqlconn.Close()
            Return Nothing

        End Try




    End Function

    Public Sub showopinfo(Operationholder As Operation)


    End Sub
    Public Sub OpenPartinfo(partno As String)
        Dim partinfoform As New PartInfoForm(partno)
        partinfoform.ShowDialog()

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
    Public Function GetAllmachinehours() As DataTable
        Dim machinetable As New DataTable

        Dim sqlconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim sqlcommnd As New SqlCommand("SELECT ")

    End Function
    Public Sub ShuffleUnassigned()



    End Sub

    Public Sub Showmachpriority()

        'Get duedate for machines
        Dim machconn As New OleDbConnection(My.Settings.E2Database)
        Dim machcomm As New OleDbCommand(My.Settings.Machinepriorityreport.ToString, machconn)
        'Dim machcomm As New OleDbCommand("Select OrderDet.JobNo, OrderRouting.StepNo, " & _
        '  "OrderRouting.PartNo, Estim.Descrip, Estim.AltPartNo, OrderRouting.OperCode, OrderDet.DueDate, OrderRouting.TotHrsLeft, OrderRouting.EstimStartDate, OrderRouting.EstimEndDate, OrderRouting.CurrentMachine" & _
        '  " FROM ((OrderRouting" &
        '  " INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo)" & _
        '  " INNER JOIN Estim ON OrderRouting.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'Open' AND (OrderRouting.CurrentMachine IS NOT NULL AND OrderRouting.CurrentMachine <>'')" & _
        '  " ORDER BY OrderRouting.CurrentMachine ASC, OrderRouting.EstimStartDate", machconn)
        Dim machdue_datatable As New DataTable

        Try
            machconn.Open()
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machdue_datatable)
            machconn.Close()
        Catch ex As Exception
            machconn.Close()
            MsgBox("Report Error: " & ex.Message)
        End Try

        OpenCSVFile(machdue_datatable)


    End Sub
    Public Sub showoptypepriority()
        'Get duedate for machines
        Dim machconn As New OleDbConnection(My.Settings.E2Database)
        Dim machcomm As New OleDbCommand(My.Settings.OpertypePriorityReport, machconn)
        '    Dim machcomm As New OleDbCommand("Select OrderDet.JobNo, OrderRouting.StepNo, " & _
        '"OrderRouting.PartNo, Estim.Descrip, Estim.AltPartNo, OrderRouting.OperCode, OrderDet.DueDate, OrderRouting.TotHrsLeft, OrderRouting.EstimStartDate, OrderRouting.EstimEndDate, OrderRouting.CurrentMachine" & _
        '" FROM ((OrderRouting" &
        '" INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo)" & _
        '" INNER JOIN Estim ON OrderRouting.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'Open' AND (OrderRouting.CurrentMachine IS NOT NULL AND OrderRouting.CurrentMachine <>'')" & _
        '" ORDER BY OrderRouting.CurrentMachine ASC, OrderDet.DueDate", machconn)
        Dim machdue_datatable As New DataTable
        Try
            machconn.Open()
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machdue_datatable)
            machconn.Close()
        Catch ex As Exception
            machconn.Close()
            MsgBox("Report Error: " & ex.Message)
        End Try

        OpenCSVFile(machdue_datatable)

        '  Dim reportviewer As New ScheduleReporter(machdue_datatable)
        '  reportviewer.ShowDialog()

    End Sub

    Public Sub showOptypeduedate()
        'Get duedate for machines
        Dim machconn As New OleDbConnection(My.Settings.E2Database)
        Dim machcomm As New OleDbCommand(My.Settings.Opertypeduedatereport, machconn)
        '    Dim machcomm As New OleDbCommand("Select OrderDet.JobNo, OrderRouting.StepNo, " & _
        '"OrderRouting.PartNo, Estim.Descrip, Estim.AltPartNo, OrderRouting.OperCode, OrderDet.DueDate, OrderRouting.TotHrsLeft, OrderRouting.EstimStartDate, OrderRouting.EstimEndDate, OrderRouting.CurrentMachine" & _
        '" FROM ((OrderRouting" &
        '" INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo)" & _
        '" INNER JOIN Estim ON OrderRouting.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'Open' AND (OrderRouting.CurrentMachine IS NOT NULL AND OrderRouting.CurrentMachine <>'')" & _
        '" ORDER BY OrderRouting.CurrentMachine ASC, OrderDet.DueDate", machconn)
        Dim machdue_datatable As New DataTable
        Try
            machconn.Open()
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machdue_datatable)
            machconn.Close()
        Catch ex As Exception
            machconn.Close()
            MsgBox("Report Error: " & ex.Message)
        End Try

        OpenCSVFile(machdue_datatable)


    End Sub

    Public Sub ScheduleReportviewer(reportstring As String)

        If reportstring = "" Then
            MsgBox("No Query Found")
            Exit Sub
        End If

        'Get duedate for machines
        Dim machconn As New OleDbConnection(My.Settings.E2Database)
        Dim machcomm As New OleDbCommand(reportstring, machconn)
        '    Dim machcomm As New OleDbCommand("Select OrderDet.JobNo, OrderRouting.StepNo, " & _
        '"OrderRouting.PartNo, Estim.Descrip, Estim.AltPartNo, OrderRouting.OperCode, OrderDet.DueDate, OrderRouting.TotHrsLeft, OrderRouting.EstimStartDate, OrderRouting.EstimEndDate, OrderRouting.CurrentMachine" & _
        '" FROM ((OrderRouting" &
        '" INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo)" & _
        '" INNER JOIN Estim ON OrderRouting.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'Open' AND (OrderRouting.CurrentMachine IS NOT NULL AND OrderRouting.CurrentMachine <>'')" & _
        '" ORDER BY OrderRouting.CurrentMachine ASC, OrderDet.DueDate", machconn)
        Dim machdue_datatable As New DataTable
        Try
            machconn.Open()
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machdue_datatable)
            machconn.Close()
            OpenCSVFile(machdue_datatable)
        Catch ex As Exception
            machconn.Close()
            MsgBox("Report Error: " & ex.Message)
            Exit Sub
        End Try




    End Sub
    Public Sub Showmachduedate()

        'Get duedate for machines
        Dim machconn As New OleDbConnection(My.Settings.E2Database)
        Dim machcomm As New OleDbCommand(My.Settings.Machineduedatestring, machconn)
        '    Dim machcomm As New OleDbCommand("Select OrderDet.JobNo, OrderRouting.StepNo, " & _
        '"OrderRouting.PartNo, Estim.Descrip, Estim.AltPartNo, OrderRouting.OperCode, OrderDet.DueDate, OrderRouting.TotHrsLeft, OrderRouting.EstimStartDate, OrderRouting.EstimEndDate, OrderRouting.CurrentMachine" & _
        '" FROM ((OrderRouting" &
        '" INNER JOIN OrderDet ON OrderRouting.JobNo = OrderDet.JobNo)" & _
        '" INNER JOIN Estim ON OrderRouting.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'Open' AND (OrderRouting.CurrentMachine IS NOT NULL AND OrderRouting.CurrentMachine <>'')" & _
        '" ORDER BY OrderRouting.CurrentMachine ASC, OrderDet.DueDate", machconn)
        Dim machdue_datatable As New DataTable
        Try
            machconn.Open()
            Dim machadapter As New OleDbDataAdapter(machcomm)
            machadapter.Fill(machdue_datatable)
            machconn.Close()
        Catch ex As Exception
            machconn.Close()
            MsgBox("Report Error: " & ex.Message)
        End Try


        OpenCSVFile(machdue_datatable)




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
                "SetupTime, TimeUnit, WorkOrVend, LeadTime, VendCode, OpScheduling.Completed From OrderRouting INNER JOIN OpScheduling ON OpScheduling.JobNo = OrderRouting.JobNo AND OpScheduling.StepNo = OrderRouting.StepNo WHERE OrderRouting.JobNo = @jobno ORDER BY OrderRouting.StepNo ASC", dbaseconn)

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
            Dim dbasecomm As New SqlCommand("SELECT OpScheduling.JobNo, OpScheduling.StepNo, OperCode, TotHrsLeft, OpScheduling.EstimStartDate, OpScheduling.EstimEndDate, OrderScheduling.Material, Programming, Tooling, Completed From (((OpScheduling INNER JOIN OrderScheduling ON OpScheduling.JobNo = OrderScheduling.JobNo) INNER JOIN OrderDet ON OpScheduling.JobNo = OrderDet.JobNo) INNER JOIN Estim ON OrderDet.PartNo = Estim.PartNo) WHERE OrderDet.Status = 'OPEN'", dbaseconn)

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
        Dim filesconn As New SqlConnection(My.Settings.PartDatabaseString)

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
    Public Function GetMachFromDepartment(ByRef departmentname As String) As DataTable
        Dim sqlconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim sqlcomm As New SqlCommand
        sqlcomm.Connection = sqlconn
        Dim commtext As String

        If departmentname = "ALL" Then

            commtext = "SELECT Id, Name FROM Machines ORDER BY NAME ASC"
            sqlcomm.CommandText = commtext
        Else
            Dim departconn As New SqlConnection(My.Settings.PartDatabaseString)
            Dim departcomm As New SqlCommand("SELECT Id From Department WHERE Description = @descrip", departconn)
            departcomm.Parameters.AddWithValue("@descrip", departmentname)
            Dim departid As Integer

            'Open the connection
            departconn.Open()
            'Get the reader
            Dim departreader As SqlDataReader = departcomm.ExecuteReader
            While departreader.Read
                departid = departreader.GetInt32(0)
            End While

            'Departreader to get integer
            departreader.Close()
            'Close the connection
            departconn.Close()
            commtext = "SELECT Id, Name FROM Machines WHERE Department = @depid ORDER BY NAME ASC"
            sqlcomm.CommandText = commtext
            sqlcomm.Parameters.AddWithValue("@depid", departid)

        End If

        Try
            sqlconn.Open()
            Dim sqladapter As New SqlDataAdapter(sqlcomm)
            Dim machtable As New DataTable
            sqladapter.Fill(machtable)
            sqlconn.Close()
            Return machtable


        Catch ex As Exception

            sqlconn.Close()
            MsgBox(ex.Message)
        End Try

        Return Nothing


    End Function
    Private Sub OpenCSVFile(dtable As DataTable)
        Dim filelocationholder As String
        filelocationholder = My.Settings.TempFileFolder & "\" & DateTime.Now.Ticks.ToString & ".csv"
        ExporttoExcel(dtable, filelocationholder, True)


    End Sub

    Public Function AddScheduleworkcenter(ByRef Workcenter As String, department As Integer) As Boolean

        Dim dptconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim dptcomm As New SqlCommand("SELECT count(1) FROM Machines WHERE Name = @nameid", dptconn)
        dptcomm.Parameters.AddWithValue("@nameid", Workcenter)

        Dim wkcent As New SqlConnection(My.Settings.PartDatabaseString)
        Dim wkcomm As New SqlCommand("INSERT INTO Machines (Name, Department) VALUES (@name, @department)", wkcent)
        wkcomm.Parameters.AddWithValue("@name", Workcenter)
        wkcomm.Parameters.AddWithValue("@department", department)

        Try
            dptconn.Open()
            If dptcomm.ExecuteScalar = 0 Then
                wkcent.Open()
                wkcomm.ExecuteNonQuery()
                wkcent.Close()
            Else
                MsgBox("Work Center Already Exists")
            End If
            dptconn.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            wkcent.Close()
            dptconn.Close()
        End Try


    End Function
    Public Function AddScheduleDepartment(ByRef Newdepartmentname As String) As Boolean

        'Check to see if the department is already entered
        Dim dptconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim dptcomm As New SqlCommand("SELECT count(1) FROM Department WHERE Description=@dept", dptconn)
        dptcomm.Parameters.AddWithValue("@dept", Newdepartmentname)
        Dim insertconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim insertcomm As New SqlCommand("INSERT INTO Department (Description) VALUES (@deptin)", insertconn)
        insertcomm.Parameters.AddWithValue("@deptin", Newdepartmentname)


        Try
            dptconn.Open()
            If dptcomm.ExecuteScalar = 0 Then
                insertconn.Open()
                insertcomm.ExecuteNonQuery()
                insertconn.Close()
            Else
                MsgBox("Department already exists")
            End If

            dptconn.Close()
            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            dptconn.Close()
            insertconn.Close()
            Return False

        End Try


    End Function
    Public Function addScheduleoperationrelationship(operationtoenter As String, machineid As Integer) As Boolean

        Dim addopconn As New SqlConnection(My.Settings.PartDatabaseString)

        Dim addopcommand As New SqlCommand("INSERT INTO Operationtypes (Machineid, Operationcode) Values (@machid, @opcode)", addopconn)
        addopcommand.Parameters.AddWithValue("@machid", machineid)
        addopcommand.Parameters.AddWithValue("@opcode", operationtoenter)

        Try
            addopconn.Open()
            addopcommand.ExecuteNonQuery()
            addopconn.Close()
            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            addopconn.Close()
            Return False

        End Try


    End Function

    Public Function GetoperationsforMachine(machineid As Integer) As DataTable
        Dim routingstring As String = "SELECT OperationCode, Id From Operationtypes Where Machineid = @machid"

        Dim routingconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim routingcommand As New SqlCommand(routingstring, routingconn)

        Try

            'Add the parameter
            routingcommand.Parameters.AddWithValue("@machid", machineid)
            routingconn.Open()
            Dim routingadapter As New SqlDataAdapter(routingcommand)

            Dim dbtable As New DataTable

            routingadapter.Fill(dbtable)
            routingconn.Close()
            Return dbtable

        Catch ex As Exception
            routingconn.Close()
            MsgBox("Error finding routing info: " & ex.Message)
            Return Nothing

        End Try

    End Function
    Public Function GetAllE2Operations() As DataTable

        Dim routingstring As String = "SELECT DISTINCT OperCode From OperCode"

        Dim routingconn As New OleDbConnection(My.Settings.E2Database)
        Dim routingcommand As New OleDbCommand(routingstring, routingconn)
        'Add the parameter

        Try

            routingconn.Open()
            Dim routingadapter As New OleDbDataAdapter(routingcommand)

            Dim dbtable As New DataTable

            routingadapter.Fill(dbtable)
            routingconn.Close()

            Return dbtable

        Catch ex As Exception
            routingconn.Close()

            MsgBox("Error finding routing info: " & ex.Message)
            Return Nothing

        End Try



    End Function

    Public Sub DeleteScheduleWorkCenter(ByRef workcenterdelete As Integer)

        'Delete the specific machine
        Dim wcntconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim wcntcomm As New SqlCommand("DELETE FROM Machines WHERE Id = @id", wcntconn)
        wcntcomm.Parameters.AddWithValue("@id", workcenterdelete)

        'Delete all associated operations
        Dim opconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim opcomm As New SqlCommand("DELETE From Operationtypes WHERE Machineid =@machid", opconn)
        opcomm.Parameters.AddWithValue("@machid", workcenterdelete)
        Try
            wcntconn.Open()
            wcntcomm.ExecuteNonQuery()
            wcntconn.Close()
            opconn.Open()
            opcomm.ExecuteNonQuery()
            opconn.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            wcntconn.Close()
            opconn.Close()

        End Try


    End Sub

    Public Sub DeleteScheduleDepartment(ByRef Departmentid As Integer)
        'Determine if the machines are still in for the department
        Dim deletemachconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim deletemachcomm As New SqlCommand("SELECT 1 From Machines WHERE Id = @department", deletemachconn)
        deletemachcomm.Parameters.AddWithValue("@department", Departmentid)




        Dim dptconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim dptcomm As New SqlCommand("DELETE FROM Department WHERE Id = @id", dptconn)
        dptcomm.Parameters.AddWithValue("@id", Departmentid)

        Try
            deletemachconn.Open()
            If deletemachcomm.ExecuteScalar <> 0 Then
                dptconn.Open()
                dptcomm.ExecuteNonQuery()
                dptconn.Close()
            Else
                MsgBox("Delete All machines associated with department before deleting department")
                deletemachconn.Close()
                Exit Sub
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            dptconn.Close()

        End Try
    End Sub

    Public Sub DeleteOpAssociation(ByRef Machineid As Integer, ByRef Operationid As Integer)



    End Sub

End Module
