Imports System.Data.SqlClient

Public Class ScheduleSettings
    Dim userholder As UserValues
    Dim workcenterholder As Integer
    Dim departmentholder As Integer


    Public Sub New(userentry As UserValues)
        userholder = userentry
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub ScheduleSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Loaddepartmentcombo()
        CalendarDisplaySettings()
        loadgeneralsettings()
        loadstatussettings()
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub
    Private Sub loadstatussettings()

        statuswidth_value.Value = My.Settings.machineviewerwidth
        statusheight_value.Value = My.Settings.machineviewerheight

    End Sub
    Private Sub loadgeneralsettings()
        CheckBox1.Checked = My.Settings.scheduledebugging
        duedatecon_checkbox.Checked = My.Settings.DueDateWarnings
        matcon_checkbox.Checked = My.Settings.MaterialWarnings

    End Sub

    Private Sub Loadlistview()

        DataGridView1.DataSource = Nothing
        'Select all the operations from machine
        Dim optableholder As DataTable = GetoperationsforMachine(workcenterholder)
        DataGridView1.DataSource = optableholder
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 20

    End Sub

    '
    '
    'Combobox Subroutines
    Private Sub Loadworkcentercombo()
        Dim sqlcommand As New SqlCommand("Select Name, Id From Machines Where Department = @department")
        sqlcommand.Parameters.AddWithValue("@department", departmentholder)
        FillComboFromDBsqlCommand(workcentercombobox, sqlcommand, "Name")
        Departments_combobox.ValueMember = "Id"

    End Sub
    Private Sub Loaddepartmentcombo()
        FillComboFromDB(Departments_combobox, "SELECT DISTINCT Description, Id From Department", "Description", False)
        Departments_combobox.ValueMember = "Id"

    End Sub
    Private Sub workcentercombobox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles workcentercombobox.SelectedIndexChanged

        'If the selected value is a machine then get the operations that that machine has assigned
        'Check to see if machine is real
        'Load the listview

        If workcentercombobox.SelectedIndex > -1 Then
            Dim wcint As Integer
            wcint = CType(workcentercombobox.SelectedItem, DataRowView).Row.Item("Id").ToString
            'if the wcint is possible
            If wcint > -1 Then
                workcenterholder = wcint
                Loadlistview()
            End If
            'Add Work Center Button

        End If


    End Sub
    Private Sub Departments_combobox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Departments_combobox.SelectedIndexChanged
        workcentercombobox.SelectedIndex = -1
        If Departments_combobox.SelectedIndex > -1 Then
            Dim dpint As Integer
            dpint = CType(Departments_combobox.SelectedItem, DataRowView).Row.Item("Id").ToString

            If dpint > -1 Then
                departmentholder = dpint
                workcenterholder = -1
                Loadworkcentercombo()
            End If

        End If

    End Sub

    '
    '
    'Button Subroutines

    Private Sub OpCodeadd_button_Click(sender As Object, e As EventArgs) Handles OpCodeadd_button.Click
        If CheckPermissions(userholder, 1) = False Then
            Exit Sub
        End If
        'Add Operation Button
        'Get operations (From E2)
        Dim operationform As New SelectOperationform
        Dim optoenter As String = ""
        If operationform.ShowDialog = Windows.Forms.DialogResult.OK Then
            optoenter = operationform.opselected
            operationform.Close()
            If addScheduleoperationrelationship(optoenter, workcenterholder) = True Then
                'Reload the listview
                Loadlistview()
            End If
        Else
            operationform.Close()
            Exit Sub

        End If

    End Sub

    Private Sub adddepartmentbutton(sender As Object, e As EventArgs) Handles adddepartment_button.Click

        If CheckPermissions(userholder, 1) = False Then
            Exit Sub
        End If
        'Add Department Button
        Dim departmententername As String = InputBox("Enter New Name of Department", "Department Name")

        If AddScheduleDepartment(departmententername) = True Then
            Loaddepartmentcombo()
            Departments_combobox.SelectedIndex = Departments_combobox.FindStringExact(departmententername)
            DataGridView1.DataSource = Nothing

        End If

    End Sub

    Private Sub centerbutton_click(sender As Object, e As EventArgs) Handles Addcenter_button.Click
        'Check if Engineer
        If CheckPermissions(userholder, 1) = False Then
            Exit Sub
        End If



        'If the a department is selected you can addd a workcenter.
        If Departments_combobox.SelectedIndex > -1 Then
            Dim ident As Integer
            ident = CType(Departments_combobox.SelectedItem, DataRowView).Row.Item("Id").ToString
            If ident > -1 Then
                'Add Work Center Button
                Dim machinename As String = InputBox("Please input scheduling name for Machine", "Machine name")
                AddScheduleworkcenter(machinename, departmentholder)
                Loadworkcentercombo()
                workcentercombobox.SelectedIndex = workcentercombobox.FindStringExact(machinename)
            End If
        Else
            MsgBox("Error: A department must be selected prior to adding a workcenter")
        End If



    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles deletedepartment_button.Click
        'Check if Engineer
        If CheckPermissions(userholder, 1) = False Then
            Exit Sub
        End If
        'Delete Department Button
        'Make sure all machines under department are closed or deleted
        'If they aren't, the user is not able to delete the department
        DeleteScheduleDepartment(Departments_combobox.SelectedValue)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles deleteworkcenter_button.Click
        'Check if Engineer
        If CheckPermissions(userholder, 1) = False Then
            Exit Sub
        End If
        'Delete Work Center Button
        If workcenterholder > -1 Then
            DeleteScheduleWorkCenter(workcenterholder)
            Loadworkcentercombo()
        End If

    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        'Check if Engineer
        If CheckPermissions(userholder, 1) = False Then
            Exit Sub
        End If
        If workcenterholder > -1 Then
            'Delete the Operation
            If DataGridView1.SelectedRows.Count > 0 Then
                DeleteOpAssociation(workcenterholder, DataGridView1.SelectedRows(0).Cells(1).Value)
            End If
        Else
            MsgBox("No Work Center Selected")
        End If

    End Sub

    Private Sub OK_button1_Click(sender As Object, e As EventArgs) Handles OK_button1.Click
        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    ''
    '
    '
    '1.
    'VIEW SUBROUTINES
    Private Sub CalendarDisplaySettings()
        PictureBox1.BackColor = My.Settings.ScheduleBackColor
        opfont_textbox.Text = My.Settings.jobplanneritemfont.ToString
        opgoodcolorpicturebox.BackColor = My.Settings.jobplanneritemgoodcolor
        opbadcolorpicturebox.BackColor = My.Settings.jobplanneritembadcolor
        unassignedwidth_number.Value = My.Settings.Scheduleunassignedwidth
        machinecolumnwidth_number.Value = My.Settings.jobplannermachinewidth
        itemheight_number.Value = My.Settings.jobplanneritemheight
        machinerowheight_number.Value = My.Settings.jobplannermachineheight
        highlightcolor.BackColor = My.Settings.schedulehighlightcolor


    End Sub

    Private Sub CalendarColorButton_Click(sender As Object, e As EventArgs) Handles CalendarColorButton.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.BackColor = ColorDialog1.Color
            My.Settings.ScheduleBackColor = ColorDialog1.Color
            My.Settings.Save()
        End If
    End Sub

    Private Sub badcolor_button_Click(sender As Object, e As EventArgs) Handles badcolor_button.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            opbadcolorpicturebox.BackColor = ColorDialog1.Color
            My.Settings.jobplanneritembadcolor = ColorDialog1.Color
            My.Settings.Save()
        End If
    End Sub

    Private Sub goodcolor_button_Click(sender As Object, e As EventArgs) Handles goodcolor_button.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            opgoodcolorpicturebox.BackColor = ColorDialog1.Color
            My.Settings.jobplanneritemgoodcolor = ColorDialog1.Color
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If FontDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            opfont_textbox.Text = FontDialog1.Font.ToString
            My.Settings.jobplanneritemfont = FontDialog1.Font
            My.Settings.Save()
        End If
    End Sub

    Private Sub machinerowheight_number_ValueChanged(sender As Object, e As EventArgs) Handles machinerowheight_number.ValueChanged
        My.Settings.jobplannermachineheight = machinerowheight_number.Value
        My.Settings.Save()

    End Sub

    Private Sub machinecolumnwidth_number_ValueChanged(sender As Object, e As EventArgs) Handles machinecolumnwidth_number.ValueChanged
        My.Settings.jobplannermachinewidth = machinecolumnwidth_number.Value
        My.Settings.Save()

    End Sub

    Private Sub unassignedwidth_number_ValueChanged(sender As Object, e As EventArgs) Handles unassignedwidth_number.ValueChanged
        My.Settings.Scheduleunassignedwidth = unassignedwidth_number.Value
        My.Settings.Save()

    End Sub

    Private Sub itemheight_number_ValueChanged(sender As Object, e As EventArgs) Handles itemheight_number.ValueChanged
        My.Settings.jobplanneritemheight = itemheight_number.Value
        My.Settings.Save()

    End Sub

    Private Sub OKButton2_Click(sender As Object, e As EventArgs) Handles OKButton2.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub opcode_duedate_button_Click(sender As Object, e As EventArgs) Handles opcode_duedate_button.Click
        Dim stringedit As New GetMultilineString("Enter Opcode Due Date Query", My.Settings.Opertypeduedatereport.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.Opertypeduedatereport = stringedit.stringvalue
            My.Settings.Save()
        End If
    End Sub

    Private Sub oppriorityedit_button_Click(sender As Object, e As EventArgs) Handles oppriorityedit_button.Click
        Dim stringedit As New GetMultilineString("Enter Opcode Priority Query", My.Settings.OpertypePriorityReport.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.OpertypePriorityReport = stringedit.stringvalue
            My.Settings.Save()
        End If
    End Sub

    Private Sub machineduedateedit_button_Click(sender As Object, e As EventArgs) Handles machineduedateedit_button.Click
        Dim stringedit As New GetMultilineString("Enter Machine Due Date Query", My.Settings.Machineduedatestring.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.Machineduedatestring = stringedit.stringvalue
            My.Settings.Save()
        End If
    End Sub

    Private Sub machinepriorityedit_button_Click(sender As Object, e As EventArgs) Handles machinepriorityedit_button.Click
        Dim stringedit As New GetMultilineString("Enter Machine Priority Query", My.Settings.Machinepriorityreport.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.Machinepriorityreport = stringedit.stringvalue
            My.Settings.Save()
        End If
    End Sub

    Private Sub allopsduedateedit_button_Click(sender As Object, e As EventArgs) Handles allopsduedateedit_button.Click
        Dim stringedit As New GetMultilineString("Enter Customer Sorted Due Date", My.Settings.allopsduedatereport.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.allopsduedatereport = stringedit.stringvalue
            My.Settings.Save()
        End If
    End Sub

    Private Sub allopspriorityedit_button_Click(sender As Object, e As EventArgs) Handles allopspriorityedit_button.Click
        Dim stringedit As New GetMultilineString("Enter Customer Sorted Priority", My.Settings.allopspriorityreport.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.allopspriorityreport = stringedit.stringvalue
            My.Settings.Save()
        End If
    End Sub

    Private Sub custom1edit_button_Click(sender As Object, e As EventArgs) Handles custom1edit_button.Click
        Dim stringedit As New GetMultilineString("Enter Custom Query 1", My.Settings.customreport1.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.customreport1 = stringedit.stringvalue
            My.Settings.Save()

            My.Settings.customreport1name = InputBox("Enter Custom Query Name", My.Settings.customreport1name)
        End If


    End Sub

    Private Sub custom2edit_button_Click(sender As Object, e As EventArgs) Handles custom2edit_button.Click
        Dim stringedit As New GetMultilineString("Enter Custom Query 2", My.Settings.customreport2.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.customreport2 = stringedit.stringvalue
            My.Settings.Save()

            My.Settings.customreport2name = InputBox("Enter Custom Query Name", My.Settings.customreport2name)
        End If

    End Sub

    Private Sub custom3edit_button_Click(sender As Object, e As EventArgs) Handles custom3edit_button.Click
        Dim stringedit As New GetMultilineString("Enter Custom Query 3", My.Settings.customreport3.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.customreport3 = stringedit.stringvalue
            My.Settings.Save()

            My.Settings.customreport3name = InputBox("Enter Custom Query Name", My.Settings.customreport3name)
        End If

    End Sub

    Private Sub custom4edit_button_Click(sender As Object, e As EventArgs) Handles custom4edit_button.Click
        Dim stringedit As New GetMultilineString("Enter Custom Query 4", My.Settings.customreport4.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.customreport4 = stringedit.stringvalue
            My.Settings.Save()

            My.Settings.customreport4name = InputBox("Enter Custom Query Name", My.Settings.customreport4name)
        End If

    End Sub

    Private Sub custom5edit_button_Click(sender As Object, e As EventArgs) Handles custom5edit_button.Click
        Dim stringedit As New GetMultilineString("Enter Custom Query 5", My.Settings.customreport5.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.customreport5 = stringedit.stringvalue
            My.Settings.Save()

            My.Settings.customreport5name = InputBox("Enter Custom Query Name", My.Settings.customreport5name)
        End If
    End Sub

    Private Sub custom6edit_button_Click(sender As Object, e As EventArgs) Handles custom6edit_button.Click
        Dim stringedit As New GetMultilineString("Enter Custom Query 6", My.Settings.customreport6.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.customreport6 = stringedit.stringvalue
            My.Settings.Save()

            My.Settings.customreport6name = InputBox("Enter Custom Query Name", My.Settings.customreport6name)
        End If

    End Sub

    Private Sub custom7edit_button_Click(sender As Object, e As EventArgs) Handles custom7edit_button.Click
        Dim stringedit As New GetMultilineString("Enter Custom Query 7", My.Settings.customreport7.ToString)

        If stringedit.ShowDialog = DialogResult.OK Then
            My.Settings.customreport7 = stringedit.stringvalue
            My.Settings.Save()

            My.Settings.customreport7name = InputBox("Enter Custom Query Name", My.Settings.customreport7name)
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            My.Settings.scheduledebugging = True
            My.Settings.Save()
        Else
            My.Settings.scheduledebugging = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            highlightcolor.BackColor = ColorDialog1.Color
            My.Settings.schedulehighlightcolor = ColorDialog1.Color
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.machineviewerheight = statusheight_value.Value
        My.Settings.machineviewerwidth = statuswidth_value.Value

    End Sub

    Private Sub matcon_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles matcon_checkbox.CheckedChanged
        If matcon_checkbox.Checked = True Then
            My.Settings.MaterialWarnings = True
            My.Settings.Save()
        Else
            My.Settings.MaterialWarnings = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub duedatecon_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles duedatecon_checkbox.CheckedChanged

        If duedatecon_checkbox.Checked = True Then
            My.Settings.DueDateWarnings = True
            My.Settings.Save()
        Else
            My.Settings.DueDateWarnings = False
            My.Settings.Save()
        End If


    End Sub


End Class
