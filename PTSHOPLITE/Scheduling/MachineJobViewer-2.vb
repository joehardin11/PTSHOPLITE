Imports System.Data.OleDb


Public Class MachineJobViewer
    Dim loadingform As Boolean


    Dim employeecombobox As ComboBox

    Private Sub Loadcheckboxes()


    End Sub

    Private Sub MachineJobViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadingform = True
        'Load all Machines in the dropdown menu
        loadcomboboxes()
        'Checkbox all the machines
        Loadmachinesviewer()


        NumericUpDown1.Value = ListView1.Font.Size

        loadingform = False

    End Sub
    Private Sub loadcomboboxes()
        'Get the machine departments
        FillComboFromDB(Machines_combobox, "SELECT DISTINCT Description FROM Department", "Description", False)
        If Machines_combobox.FindStringExact(My.Settings.jobpannermachineview) >= 0 Then
            Machines_combobox.SelectedIndex = Machines_combobox.FindStringExact(My.Settings.jobpannermachineview)
        Else
            Machines_combobox.SelectedIndex = 0
        End If

        FillComboFromDB(Shift1Employee_combo, "SELECT DISTINCT EmplName From EmplCode", "EmplName", True)
        FillComboFromDB(shift2employee_combo, "SELECT DISTINCT EmplName From EmplCode", "EmplName", True)


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
                machineops = getmachineviewerops(machine.Item(1).ToString)

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
                    Machinelvitem.SubItems.Add(NotNull(machine.Item(2).ToString, ""))
                    Machinelvitem.SubItems.Add(NotNull(machine.Item(3).ToString, ""))

                Else
                    Machinelvitem.SubItems.Add("None")
                    Machinelvitem.SubItems.Add("N/A")
                    Machinelvitem.SubItems.Add("None")
                    Machinelvitem.SubItems.Add("N/A")
                    Machinelvitem.SubItems.Add(NotNull(machine.Item(2).ToString, ""))
                    Machinelvitem.SubItems.Add(NotNull(machine.Item(3).ToString, ""))
                End If

                ListView1.Items.Add(Machinelvitem)

            Next
        End If




    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        'Refresh the board
        Loadmachinesviewer()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Machines_combobox.SelectedIndexChanged
        Loadmachinesviewer()


    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        ListView1.Font = New Font(New FontFamily("Segoe UI"), NumericUpDown1.Value, FontStyle.Bold)

    End Sub


    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If loadingform = False Then
            If ListView1.SelectedItems.Count > 0 Then
                Dim shift1empl As String = ListView1.SelectedItems(0).SubItems(5).Text
                Dim shift2empl As String = ListView1.SelectedItems(0).SubItems(6).Text

                Shift1Employee_combo.Items.IndexOf(shift1empl)
                shift2employee_combo.Items.IndexOf(shift2empl)

            End If

        End If
    End Sub

    Private Sub Shift1Employee_combo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Shift1Employee_combo.SelectedIndexChanged
        If loadingform = False Then

            If ListView1.SelectedItems.Count > 0 Then
                ListView1.SelectedItems(0).SubItems(5).Text = Shift1Employee_combo.Text
                UpdateMachineEmployee(1, ListView1.SelectedItems(0).Text, Shift1Employee_combo.Text)
            End If

        End If

    End Sub

    Private Sub shift2employee_combo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shift2employee_combo.SelectedIndexChanged
        If loadingform = False Then
            If ListView1.SelectedItems.Count > 0 Then
                ListView1.SelectedItems(0).SubItems(6).Text = shift2employee_combo.Text
                UpdateMachineEmployee(2, ListView1.SelectedItems(0).Text, shift2employee_combo.Text)
            End If
        End If
    End Sub
End Class
