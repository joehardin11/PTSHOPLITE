Imports System.Data.OleDb
Public Class SchedulingControl

    Dim controlloaded As Boolean
    Dim scheduletype As Integer = 0
    Dim datelength As Integer = 30
    Dim userholder As UserValues
    Dim departmentholder As String = "Nothing"
    Dim sortColumn As Integer = -1
    Dim unassignedops As DataTable
    Dim unassignedrefresh As Boolean
    Dim listviewcompholders As ListViewItemComparer
    Private unhighlightitems As WeekPlanner.WeekPlannerItem()
    Private unhightlightcolor As Color()


    Dim itemholder As WeekPlanner.WeekPlannerItem

    'Load new
    Public Sub New(userentry As UserValues)
        userholder = userentry
        InitializeComponent()

    End Sub
    Private Sub SchedulingControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Show the control is not loaded
        controlloaded = False

        Dim starttime As DateTime
        Dim endtime As DateTime


        'Get all the machine operations
        starttime = Now

        'Load the comboboxes
        loadcombos()
        endtime = Now
        If My.Settings.scheduledebugging Then
            MsgBox(endtime.Subtract(starttime).TotalSeconds.ToString("0.00000"))
        End If

        'Load Visual
        PlannerViewOptions()

        'Load the unassigned ops
        loadunassigned()
        'JOB PLANNER LOAD
        JobPlanner_Load()

        'Say the control is loaded
        controlloaded = True
        unassignedrefresh = False

        'See how many connections are on a database


    End Sub
    'Job Planner Macros
    Private Sub JobPlanner_Load()


        'Load the Viewing Options
        loadschedule()
        'set userrights
        setuserrights()


    End Sub
    Private Sub setuserrights()

        If userholder.EngRights Then



        End If

    End Sub
    Private Sub JobPlanner_DragDrop(sender As Object, e As DragEventArgs) Handles JobPlanner.DragDrop
        If scheduletype = 1 Then
            Exit Sub
        End If
        If userholder.EngRights = False Then

            Exit Sub
        End If
        'Drag the item

        Dim mouseupposition As New Point(e.X, e.Y)
        mouseupposition = JobPlanner.PointToClient(mouseupposition)
        mouseupposition.Y = mouseupposition.Y - 25

        If mouseupposition.Y < 0 Then mouseupposition.Y = 1

        Dim xpos As Integer = mouseupposition.X
        Dim indexatposition As Integer              'y position 
        Dim total_x, totalcols, indexatpositionX As Integer             'x position

        'totalrows = JobPlanner.RowCount                            'total rows
        totalcols = JobPlanner.DayCount                            'Count the total amount of days

        'total_y = (totalrows * My.Settings.jobplannermachineheight)                              'total rows * the rowheight of 120 pixels
        total_x = JobPlanner.Width - My.Settings.jobplannermachinewidth                    'Subtract the width of the machine column

        'ypos = ypos - 25                                               'Subtract the 40 pixels for the header
        'If ypos < 0 Then                                                'if the 
        '    ypos = 1                                                    'if the user drops it in the header, it goes on the first machine
        'End If

        xpos = xpos - JobPlanner.LeftMargin
        If xpos < 0 Then
            xpos = 1
        End If

        indexatposition = JobPlanner.Mouserow(mouseupposition)      'The position is based on where the user's mouse is when he releases the item
        indexatpositionX = Math.Floor((xpos / total_x) * totalcols)     'The date position is based on the user's mouse location

        'If indexatposition >= totalrows Then                            'If the index position is more than the amount of 
        '    indexatposition = (totalrows)
        'End If


        Dim rows = JobPlanner.Rows
        Dim row = rows.ElementAt(indexatposition)
        If My.Settings.scheduledebugging = True Then
            MsgBox(row.Columns(0).Data(0).Text)
        End If


        Dim dateinsert As Date

        dateinsert = JobPlanner.CurrentDate.AddDays(indexatpositionX)
        dateinsert = New DateTime(dateinsert.Year, dateinsert.Month, dateinsert.Day, 0, 0, 0)
        Dim lvitem As ListViewItem


        For itemid = 0 To ListView1.SelectedItems.Count - 1

            lvitem = ListView1.SelectedItems(0)
            Dim jobno As String
            jobno = lvitem.SubItems(0).Text
            Dim stepno As Integer
            stepno = lvitem.SubItems(1).Text
            Dim Duedateval As Date
            Duedateval = CDate(lvitem.SubItems(5).Text)
            Dim ophrs As Double
            ophrs = Double.Parse(lvitem.SubItems(6).Text)

            If NoMaterialConflict(jobno, dateinsert, stepno) Then
                If RoutingMeetsDuedate(jobno, stepno, dateinsert, Duedateval, ophrs, False) Then
                    addoperation(indexatposition, row.Columns(0).Data(0).Text, dateinsert, jobno, stepno)
                    lvitem.Remove()
                End If
            End If


        Next


    End Sub
    Private Sub JobPlanner_ItemDatesChanged(sender As Object, e As WeekPlanner.WeekPlannerItemEventArgs) Handles JobPlanner.ItemDatesChanged

        If userholder.EngRights = False Then
            'If the user doesn't have the rights to change the schedule
            Dim parsejobstringh As String
            parsejobstringh = JobPlanner.SelectedItem.Subject

            Dim jobnoh As String = Mid(parsejobstringh, 1, InStr(parsejobstringh, "|") - 1)
            Dim stepstringh As String = Mid(parsejobstringh, InStr(parsejobstringh, "|") + 1, Len(parsejobstringh) - InStr(parsejobstringh, "|"))

            Dim stepnoh As Integer = Integer.Parse(stepstringh)
            Dim opchangeh As New Operation(jobnoh, stepnoh, True, False)
            e.Item.StartDate = opchangeh.startdate
            e.Item.EndDate = opchangeh.enddate
            Exit Sub

        Else
            'If its a machine schedule
            If scheduletype = 0 Then


                itemholder = e.Item
                Dim pointm As Point = MousePosition
                Dim machineholder As String
                machineholder = e.Item.Row.Columns(0).Data(0).Text
                Dim parsejobstring As String
                parsejobstring = JobPlanner.SelectedItem.Subject

                Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
                Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

                Dim stepno As Integer = Integer.Parse(stepstring)
                Dim opchange As New Operation(jobno, stepno, True, False)
                If NoMaterialConflict(jobno, e.Item.StartDate, stepno) = False Then


                    opchange.startdate = e.Item.StartDate
                    opchange.enddate = Add_businessdays(opchange.startdate, opchange.totalhoursleft, opchange.Oneshift)
                    opchange.Assignedmachine = machineholder
                    opchange.SetMachineAndDates()


                Else

                    If RoutingMeetsDuedate(jobno, stepno, e.Item.StartDate, opchange.duedate, opchange.totalhoursleft, opchange.Oneshift) Then
                        opchange.startdate = e.Item.StartDate
                        opchange.enddate = Add_businessdays(opchange.startdate, opchange.totalhoursleft, opchange.Oneshift)
                        opchange.Assignedmachine = machineholder
                        opchange.SetMachineAndDates()

                    Else

                        e.Item.StartDate = opchange.startdate
                        e.Item.EndDate = opchange.enddate
                    End If
                End If





                e.Item.EndDate = opchange.enddate
                e.Item.BackColor = getoperationcolorOP(opchange)

            ElseIf scheduletype = 1 Then
                'If its a job schedule

                itemholder = e.Item
                Dim pointm As Point = MousePosition
                Dim parsejobstring As String
                parsejobstring = JobPlanner.SelectedItem.Subject


                Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
                Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))
                Dim stepno As Integer = Integer.Parse(stepstring)
                Dim opchange As New Operation(jobno, stepno, True, False)
                opchange.startdate = e.Item.StartDate
                opchange.enddate = Add_businessdays(opchange.startdate, opchange.totalhoursleft, opchange.Oneshift)
                opchange.SetMachineAndDates()

                e.Item.EndDate = opchange.enddate
                e.Item.BackColor = getoperationcolorOP(opchange)

            End If

        End If


    End Sub
    'JOB PLANNER DOUBLECLICK
    Private Sub JobPlanner_ItemDoubleClick(sender As Object, e As WeekPlanner.WeekPlannerItemEventArgs) Handles JobPlanner.ItemDoubleClick
        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            Dim routeinfo As New RoutingInfo(opview, userholder)
            routeinfo.ShowDialog()
            routeinfo.Close()
            e.Item.BackColor = getoperationcolorOP(opview)
            e.Item.EndDate = opview.enddate

        End If

    End Sub

    Private Sub JobPlanner_KeyUp(sender As Object, e As KeyEventArgs) Handles JobPlanner.KeyUp

        If e.KeyCode = Keys.Right Then

        End If
        If e.KeyCode = Keys.Left Then
            If JobPlanner.SelectedCellDate = JobPlanner.CurrentDate Then
                JobPlanner.CurrentDate = JobPlanner.CurrentDate.AddDays(-1)
            End If
        End If
    End Sub
    Private Sub JobPlanner_KeyDown(sender As Object, e As KeyEventArgs) Handles JobPlanner.KeyDown
        If e.KeyCode = Keys.F1 Then
            If JobPlanner.SelectedCellDate = JobPlanner.CurrentDate.AddDays(JobPlanner.DayCount) Then
                JobPlanner.CurrentDate = JobPlanner.CurrentDate.AddDays(1)
            End If
        End If
        If e.KeyCode = Keys.F2 Then
            If JobPlanner.SelectedCellDate = JobPlanner.CurrentDate Then
                JobPlanner.CurrentDate = JobPlanner.CurrentDate.AddDays(-1)
            End If
        End If
    End Sub

    'Loading Jobs Subroutines
    Private Sub PlannerViewOptions()

        JobPlanner.GridBackgroundColor = My.Settings.ScheduleBackColor
        JobPlanner.Columns.Add("Machine", "Machine", My.Settings.jobplannermachinewidth)
        JobPlanner.LeftMargin = My.Settings.jobplannermachinewidth
        SplitContainer1.SplitterDistance = SplitContainer1.Width - My.Settings.Scheduleunassignedwidth
        TableLayoutPanel1.ColumnStyles(2).Width = My.Settings.Scheduleunassignedwidth
        JobPlanner.ItemTextFont = My.Settings.jobplanneritemfont

        'Show Vendors in Unassigned
        ShowVendorsToolStripMenuItem.Checked = My.Settings.ScheduleShowVendor

        'Add the unassigned operations
        'Get all distinct. 
        Dim opdattable As DataTable
        opdattable = Getavailabloptypes()

        For Each optype As DataRow In opdattable.Rows
            Dim menuitem As New ToolStripMenuItem(optype.Item(0).ToString)
            menuitem.Checked = True
            Dim codeint As Integer = -1
            codeint = My.Settings.unassignedcodes.IndexOf(optype.Item(0).ToString)
            If codeint >= 0 Then
                menuitem.Checked = False
            End If
            menuitem.CheckOnClick =
            OperationsToolStripMenuItem.DropDownItems.Add(menuitem)

        Next

        'Custom Query Change titles
        CustomQuery1ToolStripMenuItem.Text = My.Settings.customreport1name
        CustomQuery2ToolStripMenuItem.Text = My.Settings.customreport2name
        CustomQuery3ToolStripMenuItem.Text = My.Settings.customreport3name
        CustomQuery4ToolStripMenuItem.Text = My.Settings.customreport4name
        CustomQuery5ToolStripMenuItem.Text = My.Settings.customreport5name
        CustomQuery6ToolStripMenuItem.Text = My.Settings.customreport6name
        CustomQuery7ToolStripMenuItem.Text = My.Settings.customreport7name


    End Sub
    Private Sub loadunassigned()
        'Get the info from the checkboxes
        Dim opcodestring As String
        opcodestring = ""
        Dim firststring As Boolean
        firststring = True
        My.Settings.unassignedcodes.Clear()

        'Load each Operation Type
        For Each ophold As ToolStripMenuItem In OperationsToolStripMenuItem.DropDownItems
            If ophold.Checked = True Then
                If firststring = True Then
                    opcodestring = opcodestring & "OrderRouting.OperCode = '" & ophold.Text & "' "
                    firststring = False
                Else
                    opcodestring = opcodestring & "OR OrderRouting.OperCode = '" & ophold.Text & "' "
                End If
            Else
                My.Settings.unassignedcodes.Insert(My.Settings.unassignedcodes.Count, ophold.Text)
            End If

        Next

        My.Settings.Save()
        'Load the unassigned steps
        Dim stepholder As DataTable = GetUnassigned(True, jobno_textbox.Text, opcodestring, My.Settings.ScheduleShowVendor)
        Dim stepviewer As DataView
        stepviewer = stepholder.DefaultView

        '"Select 
        '1. OrderDet.JobNo
        '2. OrderRouting.StepNo
        '3. OrderRouting.PartNo
        '4. Estim.Descrip
        '5. OrderRouting.OperCode
        '6. OrderDet.DueDate
        '7. OrderRouting.TotHrsLeft 

        'Add unactive tasks to the listview on OVerall Page
        ListView1.Items.Clear()
        ListView1.Sorting = SortOrder.None

        For Each seqrow As DataRow In stepholder.Rows
            Dim newlvseq As ListViewItem = ListView1.Items.Add(seqrow.Item(0))  'jobno

            newlvseq.SubItems.Add(seqrow.Item(1))      'stepno
            newlvseq.SubItems.Add(seqrow.Item(2))      'partno
            newlvseq.SubItems.Add(seqrow.Item(3))       'descrip
            newlvseq.SubItems.Add(seqrow.Item(4))       'opercode
            newlvseq.SubItems.Add(seqrow.Item(5))       'duedate
            newlvseq.SubItems.Add(seqrow.Item(6))       'tothrsleft

            If IsDBNull(seqrow.Item(8)) = False Then
                If seqrow.Item(8) = True Then
                    newlvseq.BackColor = Color.Blue
                End If
            ElseIf IsDBNull(seqrow.Item(7)) = False Then
                If seqrow.Item(7) = True Then
                    newlvseq.BackColor = Color.Green
                End If

            End If

            If Checkprogram(seqrow.Item(1), seqrow.Item(2)) = True Then
                newlvseq.BackColor = Color.BlanchedAlmond
            End If


        Next
        '''
        If stepholder.Rows.Count > 0 Then
            ListView1.Columns(0).Width = 60     'jobno
            ListView1.Columns(1).Width = 40     'stepno
            ListView1.Columns(2).Width = 60    'Partno
            ListView1.Columns(3).Width = 150  'Step type
            ListView1.Columns(4).Width = 50    'duedate
            ListView1.Columns(5).Width = 50
        End If

        unassignedrefresh = False
        ListView1.AllowColumnReorder = True

    End Sub
    Private Sub loadschedule()
        If scheduletype = 0 Then
            '  TreeView1.Visible = False
            JobPlanner.Visible = True
            Loadmachines()
        ElseIf scheduletype = 1 Then
            '   TreeView1.Visible = True
            JobPlanner.Visible = True
            loadjobs()
        End If
    End Sub

    Private Function getoperationcolorOP(opholder As Operation) As Color

        If opholder.Completed = True Then
            'If completed = true 
            Return Color.LightBlue
        End If
        If opholder.duedate > opholder.enddate Then
            If opholder.Material = True Then
                If opholder.Tooling = True Then
                    If opholder.Program = True Then
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
    Private Sub addoperation(indexholder As Integer, machineholder As String, startdate As Date, jobno As String, stepno As Integer)
        'Add Operation and new dates
        Dim OptoChange As New Operation(jobno, stepno, True, False)
        Dim partholder As New Part(True, OptoChange.Partno)
        OptoChange.startdate = startdate
        OptoChange.enddate = Add_businessdays(startdate, OptoChange.totalhoursleft, OptoChange.Oneshift)
        OptoChange.Assignedmachine = machineholder



        'Add Operation calendar
        Dim planneritem As New WeekPlanner.WeekPlannerItem
        planneritem.StartDate = OptoChange.startdate
        planneritem.EndDate = OptoChange.enddate
        planneritem.BackColor = getoperationcolorOP(OptoChange)
        planneritem.Subject = OptoChange.JobNo & "|" & OptoChange.Stepno
        planneritem.Name = OptoChange.Partno & "-" & partholder.Description
        planneritem.Row = JobPlanner.Rows(indexholder)

        JobPlanner.Rows(indexholder).Items.Add(planneritem)
        OptoChange.SetMachineAndDates()

    End Sub
    Private Sub Loadmachines()

        'If the combobox is ALL then load all the machines
        Dim machineholdtable As New DataTable
        machineholdtable = GetMachFromDepartment(departmentholder)
        Dim starttime As DateTime
        Dim endtime As DateTime


        'Delete all the rows
        JobPlanner.Rows.Clear()
        If IsNothing(machineholdtable) = False Then
            Dim opconn As New OleDbConnection(My.Settings.E2Database)
            opconn.Open()
            For Each machine As DataRow In machineholdtable.Rows
                Dim columnrows As New WeekPlanner.DataColumns(JobPlanner.Calendar)
                'Machine column to add

                columnrows(0).Data.Add(machine.Item(1).ToString)
                Dim itemcollection As New WeekPlanner.WeekPlannerItemCollection()

                'Get all the machine operations
                starttime = Now

                Dim machineops As DataTable = GetMachineOps(machine.Item(1).ToString, poolconnection:=True, pooledcon:=opconn)

                endtime = Now
                If My.Settings.scheduledebugging Then
                    MsgBox(endtime.Subtract(starttime).TotalSeconds.ToString("0.00000"))
                End If

                starttime = Now


                For Each oprow As DataRow In machineops.Rows

                    'Define each operation
                    'Dim opstarted As New Operation(oprow.Item(0), oprow.Item(1), True, True, opconn)


                    Dim operitem As New WeekPlanner.WeekPlannerItem
                    operitem.StartDate = oprow.Item(2)
                    operitem.EndDate = oprow.Item(3)
                    'operitem.BackColor = Color.LightCoral

                    operitem.BackColor = getoperationcolor(oprow.Item(8), oprow.Item(3), oprow.Item(7), oprow.Item(4), oprow.Item(6), oprow.Item(5))

                    operitem.Subject = oprow.Item(0) & "|" & oprow.Item(1)
                    operitem.Name = oprow.Item(9)
                    itemcollection.Add(operitem)

                Next


                endtime = Now
                If My.Settings.scheduledebugging Then
                    MsgBox("JPtime: " & endtime.Subtract(starttime).TotalSeconds.ToString("0.00000"))
                End If

                JobPlanner.Rows.Add(columnrows, itemcollection)

            Next
            opconn.Close()
        End If





        'For Each machinerow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
        '    Dim machinename As String = machinerow.Columns(0).Data(0).Text
        '    'Get all the machines
        '    Dim machineops As DataTable = GetMachineOps(machinename)
        '    Dim opitems As 

        '    For Each oprow As DataRow In machineops.Rows

        '        Dim opstarted As New Operation(oprow.Item(0), oprow.Item(1), True, False)
        '        Dim operitem As New WeekPlanner.WeekPlannerItem
        '        operitem.StartDate = oprow.Item(2)
        '        operitem.EndDate = oprow.Item(3)
        '        operitem.BackColor = getoperationcolor(opstarted)
        '        operitem.Subject = oprow.Item(0) & "|" & oprow.Item(1)

        '        JobPlanner.Rows(JobPlanner.Rows.IndexOf(machinerow)).Items.Add(operitem)

        '    Next

        'Next
        JobPlanner.ItemHeight = My.Settings.jobplanneritemheight
        JobPlanner.GridCellHeight = My.Settings.jobplannermachineheight
        WorkCentersToolStripMenuItem.DropDownItems.Clear()

        For Each jobrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
            jobrow.IsCollapsible = True
            Dim menuitem As New ToolStripMenuItem(jobrow.Columns(0).Data(0).Text)

            'All Machines start out checked
            menuitem.Checked = True

            menuitem.CheckOnClick = True
            WorkCentersToolStripMenuItem.DropDownItems.Add(menuitem)

        Next

        JobPlanner.IsAllowedDraggingBetweenRows = True


    End Sub
    Private Sub loadjobs()
        'If the combobox is ALL then load all the machines
        Dim jobholdtable As New DataTable
        Dim jobsearchval As String
        jobsearchval = InputBox("Enter Job Info to Search")
        If jobsearchval = "" Then
            Exit Sub
        End If
        jobholdtable = GetJobsFromJobno(jobsearchval)
        Dim starttime As DateTime
        starttime = Now

        'Delete all the rows
        JobPlanner.Rows.Clear()
        If IsNothing(jobholdtable) = False Then
            For Each jobrow As DataRow In jobholdtable.Rows
                Dim columnrows As New WeekPlanner.DataColumns(JobPlanner.Calendar)
                'Machine column to add

                columnrows(0).Data.Add(jobrow.Item(0).ToString & vbNewLine & jobrow.Item(1).ToString) 'job name

                Dim itemcollection As New WeekPlanner.WeekPlannerItemCollection()
                'Get all the machine operations
                Dim jobops As DataTable = GetJOBOps(jobrow.Item(0).ToString)

                Dim opconn As New OleDbConnection(My.Settings.E2Database)
                opconn.Open()

                For Each oprow As DataRow In jobops.Rows

                    'Define each operation
                    'Dim opstarted As New Operation(oprow.Item(0), oprow.Item(1), True, True, opconn)


                    Dim operitem As New WeekPlanner.WeekPlannerItem
                    operitem.StartDate = oprow.Item(2)
                    operitem.EndDate = oprow.Item(3)
                    'operitem.BackColor = Color.LightCoral
                    operitem.BackColor = getoperationcolor(oprow.Item(8), oprow.Item(3), oprow.Item(7), oprow.Item(4), oprow.Item(6), oprow.Item(5))
                    operitem.Subject = oprow.Item(0) & "|" & oprow.Item(1)
                    operitem.Name = oprow.Item(10)
                    itemcollection.Add(operitem)

                Next


                opconn.Close()
                JobPlanner.Rows.Add(columnrows, itemcollection)

            Next
        End If

        Dim endtime As DateTime

        endtime = Now
        If My.Settings.scheduledebugging Then
            MsgBox(endtime.Subtract(starttime).TotalSeconds.ToString("0.00000"))
        End If

        'For Each machinerow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
        '    Dim machinename As String = machinerow.Columns(0).Data(0).Text
        '    'Get all the machines
        '    Dim machineops As DataTable = GetMachineOps(machinename)
        '    Dim opitems As 

        '    For Each oprow As DataRow In machineops.Rows

        '        Dim opstarted As New Operation(oprow.Item(0), oprow.Item(1), True, False)
        '        Dim operitem As New WeekPlanner.WeekPlannerItem
        '        operitem.StartDate = oprow.Item(2)
        '        operitem.EndDate = oprow.Item(3)
        '        operitem.BackColor = getoperationcolor(opstarted)
        '        operitem.Subject = oprow.Item(0) & "|" & oprow.Item(1)

        '        JobPlanner.Rows(JobPlanner.Rows.IndexOf(machinerow)).Items.Add(operitem)

        '    Next

        'Next
        JobPlanner.ItemHeight = My.Settings.jobplanneritemheight
        JobPlanner.GridCellHeight = My.Settings.jobplannermachineheight
        WorkCentersToolStripMenuItem.DropDownItems.Clear()

        For Each jobrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
            jobrow.IsCollapsible = True
            Dim menuitem As New ToolStripMenuItem(jobrow.Columns(0).Data(0).Text)

            'All Machines start out checked
            menuitem.Checked = True

            menuitem.CheckOnClick = True
            WorkCentersToolStripMenuItem.DropDownItems.Add(menuitem)

        Next

        JobPlanner.IsAllowedDraggingBetweenRows = False
    End Sub

    'ToolString Top Subroutines
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        'Routing Settings Page
        Dim settingspageholder As New ScheduleSettings(userholder)
        settingspageholder.ShowDialog()

    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        'Get the partinfo of the selected datagrid item
        If ListView1.SelectedItems.Count > 0 Then
            OpenPartinfo(ListView1.SelectedItems(0).SubItems(2).Text)
        End If
    End Sub
    Private Sub ScheduleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScheduleToolStripMenuItem.Click

    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            Dim routeinfo As New RoutingInfo(opview, userholder)
            routeinfo.ShowDialog()
        End If



    End Sub
    Private Sub SearchToolStripMenuItem1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click
        Dim jobschedulesearch As New OperationSearch_Form(userholder)

        If jobschedulesearch.ShowDialog = DialogResult.OK Then

        End If

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        loadunassigned()
    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles jobno_textbox.KeyDown

        If e.KeyCode = Keys.Enter Then
            loadunassigned()
        End If


    End Sub

    'COMBOBOX Subroutines
    Private Sub Machines_combobox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Machines_combobox.SelectedIndexChanged

        departmentholder = Machines_combobox.Text
        If controlloaded = True Then
            Loadmachines()
        End If
        If controlloaded = True Then
            My.Settings.jobpannermachineview = Machines_combobox.Text
            My.Settings.Save()
        End If



    End Sub

    Private Sub Calendarlength_combo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Calendarlength_combo.SelectedIndexChanged
        If Calendarlength_combo.Text = "Week" Then
            datelength = 6
        ElseIf Calendarlength_combo.Text = "Two Weeks" Then
            datelength = 14
        ElseIf Calendarlength_combo.Text = "Month" Then
            datelength = 30
        ElseIf Calendarlength_combo.Text = "Two Months" Then
            datelength = 60

        End If
        My.Settings.jobplannercalendarview = Calendarlength_combo.Text
        My.Settings.Save()
        JobPlanner.CurrentDate = DateTimePicker1.Value
        JobPlanner.DayCount = datelength

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        'Change the date for the calendar
        JobPlanner.CurrentDate = DateTimePicker1.Value
    End Sub

    Private Sub loadcombos()
        Calendarlength_combo.Items.Add("Two Months")
        Calendarlength_combo.Items.Add("Month")
        Calendarlength_combo.Items.Add("Two Weeks")
        Calendarlength_combo.Items.Add("Week")
        If Calendarlength_combo.FindStringExact(My.Settings.jobplannercalendarview) >= 0 Then
            Calendarlength_combo.SelectedIndex = Calendarlength_combo.FindStringExact(My.Settings.jobplannercalendarview)
        Else
            Calendarlength_combo.SelectedIndex = 0
        End If


        'Get the machine departments
        FillComboFromDB(Machines_combobox.ComboBox, "SELECT DISTINCT Description FROM Department", "Description", False)
        If Machines_combobox.FindStringExact(My.Settings.jobpannermachineview) >= 0 Then
            Machines_combobox.SelectedIndex = Machines_combobox.FindStringExact(My.Settings.jobpannermachineview)
        Else
            Machines_combobox.SelectedIndex = 0
        End If

        'Control the type of calendar--- Machine based scheduling vs Job based Scheduling
        Schedulestyle_combo.Items.Add("Machine")
        Schedulestyle_combo.Items.Add("Job")
        Schedulestyle_combo.SelectedIndex = 0

    End Sub

    'Listview Subroutines
    Private Sub ListView1_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles ListView1.ItemDrag

        Dim unassigneditem As ListViewItem

        unassigneditem = ListView1.SelectedItems(0)
        sender.DoDragDrop(New DataObject("System.Windows.Forms.ListViewItem()", unassigneditem), DragDropEffects.Move)

    End Sub

    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick
        'determine if clicked column is the one that is being sorted
        ' Determine whether the column is the same as the last column clicked.
        If e.Column <> sortColumn Then
            ' Set the sort column to the new column.
            sortColumn = e.Column
            ' Set the sort order to ascending by default.
            ListView1.Sorting = SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If ListView1.Sorting = SortOrder.Ascending Then
                ListView1.Sorting = SortOrder.Descending
            Else
                ListView1.Sorting = SortOrder.Ascending
            End If
        End If
        ' Call the sort method to manually sort.
        ListView1.Sort()
        Dim listviewcomphold As ListViewItemComparer = New ListViewItemComparer(e.Column, ListView1.Sorting)

        Me.ListView1.ListViewItemSorter = listviewcomphold
        listviewcompholders = listviewcomphold

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedItems.Count > 0 Then



            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            Dim routeinfo As New RoutingInfo(opview, userholder)
            routeinfo.ShowDialog()
        End If

    End Sub

    Private Function Getmachine(mouseupposition As Point) As String

        mouseupposition = JobPlanner.PointToClient(mouseupposition)

        Dim ypos As Integer = mouseupposition.Y - JobPlanner.WeekPlannerScrollPosition.Y
        Dim xpos As Integer = mouseupposition.X
        Dim total_y, totalrows, indexatposition As Integer              'y position 
        Dim total_x, totalcols, indexatpositionX As Integer             'x position


        totalrows = JobPlanner.RowCount                            'total rows
        totalcols = JobPlanner.DayCount                            'Count the total amount of days

        total_y = (totalrows * My.Settings.jobplannermachineheight)                                 'total rows * the rowheight of 120 pixels
        total_x = JobPlanner.Width - My.Settings.jobplannermachinewidth                    'Subtract the width of the machine column

        ypos = ypos - 25                                                'Subtract the 40 pixels for the header
        If ypos < 0 Then                                                'if the 
            ypos = 1                                                    'if the user drops it in the header, it goes on the first machine
        End If

        xpos = xpos - JobPlanner.LeftMargin
        If xpos < 0 Then
            xpos = 1
        End If

        indexatposition = Math.Floor((ypos / total_y) * totalrows)      'The position is based on where the user's mouse is when he releases the item
        indexatpositionX = Math.Floor((xpos / total_x) * totalcols)     'The date position is based on the user's mouse location

        If indexatposition >= totalrows Then                            'If the index position is more than the amount of 
            indexatposition = (totalrows)
        End If


        Dim rows = JobPlanner.Rows
        Dim row = rows.ElementAt(indexatposition)

        Return row.Columns(0).Data(0).Text

    End Function

    Private Sub Schedulestyle_combo_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles Schedulestyle_combo.SelectedIndexChanged
        If Schedulestyle_combo.Text = "Job" Then
            scheduletype = 1
        ElseIf Schedulestyle_combo.Text = "Machine" Then
            scheduletype = 0
        End If
        If controlloaded = True Then
            loadschedule()
        End If

    End Sub

    Private Sub SettingsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem1.Click
        'Unassigns ALL

    End Sub
    Private Sub PriorityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PriorityToolStripMenuItem.Click
        'Sorts by hours left AND Due DATE

    End Sub

    '--------------------------REPORTS-----------------------------------------
    Private Sub DueDateToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DueDateToolStripMenuItem1.Click

    End Sub
    Private Sub HourValueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HourValueToolStripMenuItem.Click

    End Sub

    Private Sub SpecificMachineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpecificMachineToolStripMenuItem.Click
        'Sort by specific machine
        ScheduleReportviewer(My.Settings.Machineduedatestring)
    End Sub
    Private Sub MachinePriorityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MachinePriorityToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.Machinepriorityreport)
    End Sub
    Private Sub DepartmentDueDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepartmentDueDateToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.Opertypeduedatereport)
    End Sub
    Private Sub DepartmentPriorityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepartmentPriorityToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.OpertypePriorityReport)
    End Sub

    Private Sub CustomQuery1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomQuery1ToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.customreport1)
    End Sub

    Private Sub CustomQuery2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomQuery2ToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.customreport2)
    End Sub

    Private Sub CustomQuery3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomQuery3ToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.customreport3)
    End Sub

    Private Sub CustomQuery4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomQuery4ToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.customreport4)
    End Sub

    Private Sub CustomQuery5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomQuery5ToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.customreport5)
    End Sub

    Private Sub CustomQuery6ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomQuery6ToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.customreport6)
    End Sub

    Private Sub CustomQuery7ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomQuery7ToolStripMenuItem.Click
        ScheduleReportviewer(My.Settings.customreport7)
    End Sub

    'JOB PLANNER CONTEXT MENU SUBS

    Private Sub ViewPartPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewPartPrintToolStripMenuItem.Click
        If IsNothing(JobPlanner.SelectedItem) = False Then


            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opchange As New Operation(jobno, stepno, True, False)
            OpenPrints(opchange.Partno, userholder)
        End If
    End Sub
    Private Sub UnassignToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UnassignToolStripMenuItem1.Click
        If userholder.EngRights = False Then

            Exit Sub
        End If

        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opchange As New Operation(jobno, stepno, True, False)
            opchange.Assignedmachine = ""
            opchange.SetMachineAndDates()
            loadunassigned()

            JobPlanner.Rows(JobPlanner.SelectedRowIndex).Items.Remove(JobPlanner.SelectedItem)

        End If


    End Sub
    Private Sub RouteStepToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RouteStepToolStripMenuItem.Click

        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            Dim routeinfo As New RoutingInfo(opview, userholder)

            If routeinfo.ShowDialog() = DialogResult.OK Then

            End If
            JobPlanner.SelectedItem.BackColor = getoperationcolor(opview.duedate, opview.enddate, opview.Completed, opview.Material, opview.Tooling, opview.Program)
            JobPlanner.SelectedItem.EndDate = opview.enddate

        End If

    End Sub
    Private Sub ProgrmamingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProgrammingToolStripMenuItem1.Click
        If userholder.SetupRights = False Then

            Exit Sub
        End If

        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)

            If opview.Program = False Then
                opview.Program = True
            Else
                opview.Program = False
            End If
            opview.updatestates()
            JobPlanner.SelectedItem.BackColor = getoperationcolorOP(opview)

        End If
    End Sub
    Private Sub MaterialToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MaterialToolStripMenuItem1.Click
        If userholder.InventoryRights = False Then

            Exit Sub
        End If

        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            If opview.Material = False Then
                opview.Material = True
            Else
                opview.Material = False
            End If
            opview.updatestates()
            JobPlanner.SelectedItem.BackColor = getoperationcolorOP(opview)
        End If
    End Sub
    Private Sub ToolingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolingToolStripMenuItem1.Click
        If userholder.SetupRights = False Then

            Exit Sub
        End If


        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            If opview.Tooling = False Then
                opview.Tooling = True
            Else
                opview.Tooling = False
            End If
            opview.updatestates()
            JobPlanner.SelectedItem.BackColor = getoperationcolorOP(opview)

        End If
    End Sub
    Private Sub CompletedToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CompletedToolStripMenuItem1.Click
        If userholder.SetupRights = False Then

            Exit Sub
        End If


        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            If opview.Completed = False Then
                opview.Completed = True
            Else
                opview.Completed = False
            End If
            opview.updatestates()
            JobPlanner.SelectedItem.BackColor = getoperationcolorOP(opview)
        End If
    End Sub
    Private Sub AllReadyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllReadyToolStripMenuItem.Click
        If userholder.SetupRights = False Then

            Exit Sub
        End If


        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)

            opview.Program = True
            opview.Tooling = True
            opview.Material = True
            opview.updatestates()

            JobPlanner.SelectedItem.BackColor = getoperationcolorOP(opview)
        End If
    End Sub
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If IsNothing(JobPlanner.SelectedItem) = False Then
            'Setup Rights
            If userholder.SetupRights = False Then
                ToolingToolStripMenuItem1.Enabled = False
                ProgrammingToolStripMenuItem1.Enabled = False
                CompletedToolStripMenuItem1.Enabled = False
            Else
                ToolingToolStripMenuItem1.Enabled = True
                ProgrammingToolStripMenuItem1.Enabled = True
                CompletedToolStripMenuItem1.Enabled = True
            End If
            'Inventory Rights
            If userholder.InventoryRights = False Then
                MaterialToolStripMenuItem.Enabled = False
            Else
                MaterialToolStripMenuItem.Enabled = True
            End If
            'Engineering Rights
            If userholder.EngRights = False Then
                UnassignToolStripMenuItem1.Enabled = False
            Else
                UnassignToolStripMenuItem1.Enabled = True

            End If

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            MaterialToolStripMenuItem1.Checked = opview.Material
            ToolingToolStripMenuItem1.Checked = opview.Tooling
            ProgrammingToolStripMenuItem1.Checked = opview.Program
            CompletedToolStripMenuItem1.Checked = opview.Completed
            ViewPartPrintToolStripMenuItem.Checked = hasprints(opview.Partno)
            If opview.Material = True And opview.Tooling = True And opview.Program = True Then
                AllReadyToolStripMenuItem.Checked = True
            Else
                AllReadyToolStripMenuItem.Checked = False
            End If
            e.Cancel = False
        Else
            e.Cancel = True
        End If

    End Sub
    Private Sub SetupInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetupInfoToolStripMenuItem.Click

    End Sub

    'UNASSIGNED LISTVIEW CONTEXT MENU SUBS
    Private Sub MaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialToolStripMenuItem.Click
        If userholder.InventoryRights = False Then

            Exit Sub
        End If


        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            If opview.Material = False Then
                opview.Material = True
            Else
                opview.Material = False
            End If
        End If
    End Sub
    Private Sub ToolingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolingToolStripMenuItem.Click
        If userholder.SetupRights = False Then

            Exit Sub
        End If


        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            If opview.Tooling = False Then
                opview.Tooling = True
            Else
                opview.Tooling = False
            End If
        End If
    End Sub
    Private Sub ProgrammingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProgrammingToolStripMenuItem.Click
        If userholder.SetupRights = False Then

            Exit Sub
        End If


        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            If opview.Program = False Then
                opview.Program = True
            Else
                opview.Program = False
            End If

            opview.updatestates()


        End If
    End Sub
    Private Sub CompletedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompletedToolStripMenuItem.Click
        If userholder.SetupRights = False Then

            Exit Sub
        End If


        If ListView1.SelectedItems.Count > 0 Then
            For Each lvitem As ListViewItem In ListView1.SelectedItems


                Dim jobno As String = lvitem.SubItems(0).Text
                Dim stepstring As String = lvitem.SubItems(1).Text

                Dim stepno As Integer = Integer.Parse(stepstring)

                Dim opview As New Operation(jobno, stepno, True, False)
                If opview.Completed = False Then
                    opview.Completed = True
                Else
                    opview.Completed = False
                End If
                opview.updatestates()
                lvitem.BackColor = Color.Blue

            Next

        End If

    End Sub
    Private Sub UnassignedContext_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles UnassignedContext.Opening

        If ListView1.SelectedItems.Count > 0 Then
            'Setup Rights
            If userholder.SetupRights = False Then
                ToolingToolStripMenuItem.Enabled = False
                ProgrammingToolStripMenuItem.Enabled = False
                CompletedToolStripMenuItem.Enabled = False
            Else
                ToolingToolStripMenuItem.Enabled = True
                ProgrammingToolStripMenuItem.Enabled = True
                CompletedToolStripMenuItem.Enabled = True
            End If
            'Inventory Rights
            If userholder.InventoryRights = False Then
                MaterialToolStripMenuItem.Enabled = False
            Else
                MaterialToolStripMenuItem.Enabled = True
            End If
            'Engineering Rights
            If userholder.EngRights = False Then
                UnassignToolStripMenuItem.Enabled = False
            Else
                UnassignToolStripMenuItem.Enabled = True
            End If


            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)
            MaterialToolStripMenuItem.Checked = opview.Material
            ToolingToolStripMenuItem.Checked = opview.Tooling
            ProgrammingToolStripMenuItem.Checked = opview.Program
            CompletedToolStripMenuItem.Checked = opview.Completed

            e.Cancel = False

        Else

            e.Cancel = True

        End If
    End Sub
    Private Sub PartPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PartPrintToolStripMenuItem.Click
        'open up the multiple fileopen dialog

        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opchange As New Operation(jobno, stepno, True, False)
            OpenPrints(opchange.Partno, userholder)

        End If

    End Sub
    Private Sub UnassignedContext_Closed(sender As Object, e As ToolStripDropDownClosedEventArgs) Handles UnassignedContext.Closed
        For Each ophold As ToolStripMenuItem In OperationsToolStripMenuItem.DropDownItems
            If ophold.Checked = False Then

                My.Settings.unassignedcodes.Insert(My.Settings.unassignedcodes.Count, ophold.Text)
            End If
        Next

    End Sub
    Private Sub GetHrsLeftToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetHrsLeftToolStripMenuItem.Click
        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)
            If My.Settings.scheduledebugging = True Then
                MsgBox(PostOpHrs(jobno, stepno))
            End If


        End If

    End Sub

    Private Sub UnassignedContext_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles UnassignedContext.Closing

    End Sub
    Private Sub OperationsToolStripMenuItem_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles OperationsToolStripMenuItem.DropDownItemClicked
        unassignedrefresh = True
    End Sub
    Private Sub ContextMenuStrip1_Closed(sender As Object, e As ToolStripDropDownClosedEventArgs) Handles ContextMenuStrip1.Closed

    End Sub
    'Quickview Buttons
    Private Sub DocumentControlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DocumentControlToolStripMenuItem.Click
        If IsNothing(JobPlanner.SelectedItem) = False Then


            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opchange As New Operation(jobno, stepno, True, False)
            Dim parth As New Part(True, opchange.Partno)

            QuickviewDocControl(parth, userholder)

        End If



    End Sub

    Private Sub SetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetupToolStripMenuItem.Click
        If IsNothing(JobPlanner.SelectedItem) = False Then


            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opchange As New Operation(jobno, stepno, True, False)


            QuickviewSetupControl(opchange, userholder)

        End If
    End Sub

    Private Sub QualityControlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QualityControlToolStripMenuItem.Click

    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text

            Dim stepno As Integer = Integer.Parse(stepstring)


            Dim parth As New Part(True, ListView1.SelectedItems(0).SubItems(2).Text)
            QuickviewDocControl(parth, userholder)
        End If


    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        If ListView1.SelectedItems.Count > 0 Then

            Dim jobno As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim stepstring As String = ListView1.SelectedItems(0).SubItems(1).Text


            Dim stepno As Integer = Integer.Parse(stepstring)

            Dim opview As New Operation(jobno, stepno, True, False)

            QuickviewSetupControl(opview, userholder)
        End If

    End Sub
    Private Sub clearhighlightitems()
        Dim opconn As New OleDbConnection(My.Settings.E2Database)

        If IsNothing(unhighlightitems) = True Then Exit Sub
        'Exit the sub if there is not list
        For itemindext As Integer = 0 To (unhighlightitems.Count - 1)

            Dim itemholder As New WeekPlanner.WeekPlannerItem
            itemholder = unhighlightitems(itemindext)
            Dim parsejobstring As String
            parsejobstring = itemholder.Subject

            '  Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            ' Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))
            'Dim opholder As New Operation(jobno, stepstring, True, True, opconn)
            itemholder.BackColor = unhightlightcolor(itemindext)

        Next

        unhighlightitems = Nothing
        unhightlightcolor = Nothing


    End Sub
    Private Sub jobnohighlight_textbox_KeyDown(sender As Object, e As KeyEventArgs) Handles jobnohighlight_textbox.KeyDown
        If e.KeyCode = Keys.Enter Then
            clearhighlightitems()

            If jobnohighlight_textbox.Text = "" Then Exit Sub
            'Set the equivalent textbox = so user can reference both
            ToolStripTextBox1.Text = jobnohighlight_textbox.Text

            For Each jbrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
                For Each jbitem As WeekPlanner.WeekPlannerItem In jbrow.Items

                    Dim parsejobstring As String
                    parsejobstring = jbitem.Subject

                    Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
                    Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))
                    Dim stepno As Integer = Integer.Parse(stepstring)
                    If InStr(jobno, jobnohighlight_textbox.Text) > 0 Then
                        unhighlightitems.Add(jbitem)
                        unhightlightcolor.Add(jbitem.BackColor)
                        jbitem.BackColor = My.Settings.schedulehighlightcolor

                    End If

                Next

            Next

        End If

    End Sub

    Private Sub ToolStripTextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles description_textbox.KeyDown
        If e.KeyCode = Keys.Enter Then
            clearhighlightitems()

            If description_textbox.Text = "" Then Exit Sub
            'Set the equivalent textbox = so user can reference both
            ToolStripTextBox2.Text = description_textbox.Text

            Dim opconn As New OleDbConnection(My.Settings.E2Database)
            For Each jbrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
                For Each jbitem As WeekPlanner.WeekPlannerItem In jbrow.Items


                    If InStr(jbitem.Name.ToLower, description_textbox.Text.ToLower) > 0 Then
                        unhighlightitems.Add(jbitem)
                        unhightlightcolor.Add(jbitem.BackColor)
                        jbitem.BackColor = My.Settings.schedulehighlightcolor

                    End If

                Next

            Next

        End If
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        clearhighlightitems()
    End Sub
    Private Sub ToolStripTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            clearhighlightitems()

            If ToolStripTextBox1.Text = "" Then Exit Sub
            'Set the equivalent textbox = so user can reference both
            jobnohighlight_textbox.Text = ToolStripTextBox1.Text
            For Each jbrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
                For Each jbitem As WeekPlanner.WeekPlannerItem In jbrow.Items

                    Dim parsejobstring As String
                    parsejobstring = jbitem.Subject

                    Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
                    Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))
                    Dim stepno As Integer = Integer.Parse(stepstring)
                    If InStr(jobno, ToolStripTextBox1.Text) > 0 Then
                        unhighlightitems.Add(jbitem)
                        unhightlightcolor.Add(jbitem.BackColor)
                        jbitem.BackColor = My.Settings.schedulehighlightcolor

                    End If

                Next

            Next

        End If
    End Sub

    Private Sub ToolStripTextBox2_KeyDown_1(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            clearhighlightitems()

            If ToolStripTextBox2.Text = "" Then Exit Sub
            'Set the equivalent textbox = so user can reference both
            description_textbox.Text = ToolStripTextBox2.Text

            Dim opconn As New OleDbConnection(My.Settings.E2Database)
            For Each jbrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
                For Each jbitem As WeekPlanner.WeekPlannerItem In jbrow.Items


                    If InStr(jbitem.Name.ToLower, ToolStripTextBox2.Text.ToLower) > 0 Then
                        unhighlightitems.Add(jbitem)
                        unhightlightcolor.Add(jbitem.BackColor)
                        jbitem.BackColor = My.Settings.schedulehighlightcolor

                    End If

                Next

            Next
        End If

    End Sub

    Private Sub HighlightThisJobToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HighlightThisJobToolStripMenuItem.Click

        If IsNothing(JobPlanner.SelectedItem) = False Then
            clearhighlightitems()
            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)

            For Each jbrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
                For Each jbitem As WeekPlanner.WeekPlannerItem In jbrow.Items

                    Dim parsejobstring2 As String
                    parsejobstring2 = jbitem.Subject

                    Dim jobno2 As String = Mid(parsejobstring2, 1, InStr(parsejobstring2, "|") - 1)
                    If InStr(jobno2, jobno) > 0 Then
                        unhighlightitems.Add(jbitem)
                        unhightlightcolor.Add(jbitem.BackColor)
                        jbitem.BackColor = My.Settings.schedulehighlightcolor

                    End If

                Next
            Next
            jobnohighlight_textbox.Text = jobno
            ToolStripTextBox1.Text = jobno

        End If

    End Sub

    'Expand and unexpand rows
    Private Sub JobPlanner_RowLeftColumnClick(sender As Object, e As WeekPlanner.RowClickEventArgs, rowNumber As Integer) Handles JobPlanner.RowLeftColumnClick

        e.Row.IsVisible = False

        For Each dropdownitem As ToolStripMenuItem In WorkCentersToolStripMenuItem.DropDownItems
            If dropdownitem.Text = e.Row.Columns(0).Data(0).Text Then
                dropdownitem.Checked = False
            End If
        Next

    End Sub
    Private Sub ClearToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem1.Click
        clearhighlightitems()

    End Sub
    Private Sub ExpandAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpandAllToolStripMenuItem.Click
        For Each machrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
            machrow.IsVisible = True
        Next

        For Each dropdownitem As ToolStripMenuItem In WorkCentersToolStripMenuItem.DropDownItems
            dropdownitem.Checked = True
        Next

    End Sub
    Private Sub WorkCentersToolStripMenuItem_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles WorkCentersToolStripMenuItem.DropDownItemClicked
        For Each machrow As WeekPlanner.WeekPlannerRow In JobPlanner.Rows
            If machrow.Columns(0).Data(0).Text = e.ClickedItem.Text Then
                If machrow.IsVisible = True Then
                    machrow.IsVisible = False
                Else
                    machrow.IsVisible = True
                End If

            End If
        Next
    End Sub
    Private Sub OrderViewerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderViewerToolStripMenuItem.Click

    End Sub
    Private Sub MachineJobViewerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim jobviewer As New MachineJobViewer

        Dim holdform As New HoldingForm(jobviewer, userholder, "Machine Status")
        holdform.Show()


    End Sub
    Private Sub ShowVendorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowVendorsToolStripMenuItem.CheckStateChanged
        My.Settings.ScheduleShowVendor = ShowVendorsToolStripMenuItem.Checked

    End Sub

    Private Sub MenuStrip2_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip2.ItemClicked

    End Sub

    Private Sub JobPlanner_DragEnter(sender As Object, e As DragEventArgs) Handles JobPlanner.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.ListViewItem()") Then
            e.Effect = DragDropEffects.Move
        Else
            If My.Settings.scheduledebugging = True Then
                MsgBox(e.Data.GetType)
            End If
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub JobPlanner_ItemClick(sender As Object, e As WeekPlanner.WeekPlannerItemEventArgs) Handles JobPlanner.ItemClick


        'Gets the mouse positions
        Dim mpos As Point = MousePosition
        mpos = JobPlanner.PointToClient(mpos)
        mpos.Y = mpos.Y - 100

        'op descrip holder as string
        Dim tooltipdesc As String

        'Gets the operation information
        If IsNothing(JobPlanner.SelectedItem) = False Then

            Dim parsejobstring As String
            parsejobstring = JobPlanner.SelectedItem.Subject

            Dim jobno As String = Mid(parsejobstring, 1, InStr(parsejobstring, "|") - 1)
            Dim stepstring As String = Mid(parsejobstring, InStr(parsejobstring, "|") + 1, Len(parsejobstring) - InStr(parsejobstring, "|"))

            Dim stepno As Integer = Integer.Parse(stepstring)

            ' Dim opview As New Operation(jobno, stepno, True, False)
            ' Dim partview As New Part(True, opview.Partno)
            ' tooltipdesc = opview.Partno & "-" & partview.Description
            tooltipdesc = JobPlanner.SelectedItem.Name

        Else
            tooltipdesc = "Unable to find operation"
        End If

        ToolTip1.Show(tooltipdesc, Me, mpos, 2000)

    End Sub
End Class
