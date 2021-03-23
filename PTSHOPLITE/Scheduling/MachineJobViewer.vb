Imports System.Data.OleDb

Public Class MachineJobViewer
    Dim loadingform As Boolean
    Dim starttime As DateTime
    Dim endtime As DateTime
    Dim employeecombobox As ComboBox

    Private Sub Loadcheckboxes()



    End Sub

    Private Sub MachineJobViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadingform = True

        'Load all Machines in the dropdown menu
        loadcomboboxes()
        'Checkbox all the machines
        'Loadmachinesviewer()
        LoadStatusviewer()
        loadingform = False

    End Sub
    Private Sub loadcomboboxes()
        'Get the machine departments
        FillComboFromDB(Machines_combobox, "SELECT DISTINCT Description FROM Department", "Description", False)
        If Machines_combobox.FindStringExact(My.Settings.machineviewerlast) >= 0 Then
            Machines_combobox.SelectedIndex = Machines_combobox.FindStringExact(My.Settings.machineviewerlast)
        Else
            Machines_combobox.SelectedIndex = 0
        End If

    End Sub
    ''' <summary>
    ''' Pulls up the status control of the machine instead of just a listview of the machines. 
    ''' </summary>
    Private Sub LoadStatusviewer()

        FlowLayoutPanel1.Controls.Clear()
        Dim machineholdtable As DataTable
        machineholdtable = GetMachFromDepartment(Machines_combobox.Text)

        'Get all the machine operations
        Dim machineops As New DataTable
        Dim machconn As New OleDbConnection(My.Settings.E2Database)
        machconn.Open()
        machineops = getmachineviewerops(machconn)


        If IsNothing(machineholdtable) = False Then

            Dim machsqlconn As New SqlClient.SqlConnection(My.Settings.PartDatabaseString)
            machsqlconn.Open()
            Dim machcontrols(machineholdtable.Rows.Count - 1) As machineStatus
            Dim machid As Integer = 0


            MsgBox(machineops.Rows.Count)
            For Each machine As DataRow In machineholdtable.Rows
                Dim machineview As New DataView(machineops)
                machineview.RowFilter = "CurrentMachine = '" & machine.Item(1).ToString & "'"

                Dim machstatus As New machineStatus(machine.Item(1).ToString, True, machineops)
                machstatus.Tag = machine.Item(1).ToString
                ' MsgBox(machstatus.Name)
                machstatus.ContextMenuStrip = ContextMenuStrip1
                machcontrols(machid) = machstatus

                machid = machid + 1
            Next

            starttime = Now
            FlowLayoutPanel1.Controls.AddRange(machcontrols)
            endtime = Now
            If My.Settings.scheduledebugging = True Then MsgBox(endtime.Subtract(starttime).TotalSeconds.ToString("0.00000"))

        End If

    End Sub
    Private Sub Loadmachinesviewer()
        ListView1.Items.Clear()

        'Get all the machines
        Dim machineholdtable As DataTable
        machineholdtable = GetMachFromDepartment(Machines_combobox.Text)
        'for each machine, get the earliest two operations that hasn't been completed

        If IsNothing(machineholdtable) = False Then
            For Each machine As DataRow In machineholdtable.Rows

                'Get all the machine operations
                Dim machineops As New DataTable
                Dim machconn As New OleDb.OleDbConnection(My.Settings.E2Database)
                machconn.Open()
                machineops = getmachineviewerops(machconn)
                machconn.Close()


                'Filter the machineops datatable

                Dim Machinelvitem As New ListViewItem(machine.Item(1).ToString)
                If machineops.Rows.Count > 0 Then

                    Machinelvitem.SubItems.Add(machineops.Rows(0).Item(0).ToString & "|" & machineops.Rows(0).Item(1).ToString & "-" & machineops.Rows(0).Item(5))
                    Machinelvitem.SubItems.Add(FormatDateTime(machineops.Rows(0).Item(4).ToString, DateFormat.ShortDate))

                    If machineops.Rows.Count > 1 Then
                        Machinelvitem.SubItems.Add(machineops.Rows(1).Item(0).ToString & "|" & machineops.Rows(1).Item(1).ToString & "-" & machineops.Rows(1).Item(5))
                        Machinelvitem.SubItems.Add(FormatDateTime(machineops.Rows(1).Item(4).ToString, DateFormat.ShortDate))
                    Else
                        Machinelvitem.SubItems.Add("None")
                        Machinelvitem.SubItems.Add("N/A")
                    End If
                    Machinelvitem.SubItems.Add("")
                    Machinelvitem.SubItems.Add("")
                Else
                    Machinelvitem.SubItems.Add("None")
                    Machinelvitem.SubItems.Add("N/A")
                    Machinelvitem.SubItems.Add("None")
                    Machinelvitem.SubItems.Add("N/A")
                    Machinelvitem.SubItems.Add("")
                    Machinelvitem.SubItems.Add("")
                End If

                ListView1.Items.Add(Machinelvitem)




            Next
        End If

    End Sub

    Private Sub Refreshmachines()


    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        'Refresh the board
        Loadmachinesviewer()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Machines_combobox.SelectedIndexChanged
        If loadingform = False Then
            LoadStatusviewer()
            My.Settings.machineviewerlast = Machines_combobox.Text
        End If

    End Sub


    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If loadingform = False Then
            If ListView1.SelectedItems.Count > 0 Then
                Dim shift1empl As String = ListView1.SelectedItems(0).SubItems(5).Text
                Dim shift2empl As String = ListView1.SelectedItems(0).SubItems(6).Text

            End If

        End If
    End Sub


    Private Sub WorkOrderInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkOrderInfoToolStripMenuItem.Click

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        'Get the information from the parent.
        Dim context_menu As ContextMenuStrip
        context_menu = sender

        If IsNothing(context_menu) = False Then
            Dim machcontrol As Control
            machcontrol = context_menu.SourceControl
            context_menu.Tag = machcontrol.Tag
            Dim machtable As DataTable = GetMachInfo(machcontrol.Tag)
            If machtable.Rows.Count > 0 Then

                For Each statusvalue As ToolStripMenuItem In UpdateStatusToolStripMenuItem.DropDownItems
                    If statusvalue.Text = NotNull(machtable.Rows(0).Item(2).ToString, "NONE") Then
                        statusvalue.Checked = True
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub RunningToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunningToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Running"
        updatestatus(strip_string, sender)

    End Sub

    Private Sub SetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetupToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Setup"
        updatestatus(strip_string, sender)

    End Sub

    Private Sub AwaitingQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AwaitingQCToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Awaiting QC"
        updatestatus(strip_string, sender)
    End Sub

    Private Sub NeedMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NeedMaterialToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Need Material"
        updatestatus(strip_string, sender)
    End Sub

    Private Sub NeedTechSupportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NeedTechSupportToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Need Tech Support"
        updatestatus(strip_string, sender)
    End Sub

    Private Sub NeedToolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NeedToolToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Need Tool"
        updatestatus(strip_string, sender)
    End Sub

    Private Sub MaintenanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaintenanceToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Maitenance"
        updatestatus(strip_string, sender)
    End Sub

    Private Sub OffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OffToolStripMenuItem.Click
        'Get the information from the parent.
        Dim strip_string As String
        strip_string = "Off"
        updatestatus(strip_string, sender)
    End Sub

    Private Sub updatestatus(strip_string As String, sender As Object)
        'Get the context menu of the string

        Dim context_menu As ContextMenuStrip
        context_menu = CType(sender, ToolStripMenuItem).OwnerItem.Owner

        Dim machstatcontrol As Control
        machstatcontrol = context_menu.SourceControl
        MsgBox(machstatcontrol.Tag)
        If IsNothing(context_menu) = False Then
            Dim machname As String = context_menu.Tag

            MachineStatusModule.ChangeMachstatus(sender, strip_string, machname)
            Dim stripval As ToolStripMenuItem = sender
            stripval.Checked = True
            For Each tstrips As ToolStripMenuItem In stripval.Owner.Items.OfType(Of ToolStripMenuItem)
                If tstrips.Checked = True And tstrips.Text <> strip_string Then
                    tstrips.Checked = False
                End If
            Next

        End If

    End Sub
End Class
