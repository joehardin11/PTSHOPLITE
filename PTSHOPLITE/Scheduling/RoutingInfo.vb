Public Class RoutingInfo
    Dim opholder As Operation
    Dim partholder As Part
    Dim userholder As UserValues
    Dim loading As Boolean = True


    Public Sub New(openter As Operation, userholder1 As UserValues)
        opholder = openter
        partholder = New Part(True, opholder.Partno)
        userholder = userholder1
        InitializeComponent()

    End Sub


    Private Sub RoutingInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loading = True
        'Load the comboboxes
        loadcomboxes()

        'get the data
        partno_textbox.Text = partholder.Partno
        partdescription_textbox.Text = partholder.Description
        altpartno_textbox.Text = partholder.AltPartno

        'Opinfo
        opdescription_textbox.Text = opholder.description
        jobno_textbox.Text = opholder.JobNo

        stepno_textbox.Text = opholder.Stepno.ToString
        orderqty_textbox.Text = opholder.OrderQty
        cycletime_number.Value = opholder.cycletime
        setuptime_number.Value = opholder.setuptime
        setupunit_combo.SelectedIndex = setupunit_combo.FindStringExact(opholder.setupunit)
        cycleunit_combo.SelectedIndex = cycleunit_combo.FindStringExact(opholder.cycleunit)
        opercode_combo.SelectedIndex = opercode_combo.FindStringExact(opholder.OperationCode)
        workcenter_combo.SelectedIndex = workcenter_combo.FindStringExact(opholder.WorkCenter)
        DateTimePicker1.Value = opholder.duedate
        hrsleft_textbox.Text = opholder.totalhoursleft
        QtyMake_textbox.Text = opholder.MakeQty
        material_checkbox.Checked = opholder.Material
        tools_checkbox.Checked = opholder.Tooling
        completed_checkbox.Checked = opholder.Completed
        program_checkbox.Checked = opholder.Program
        Revision_textbox.Text = opholder.Revision
        shift_checkbox.Checked = opholder.Oneshift

        If userholder.EngRights = False Then
            opercode_combo.Enabled = False
            workcenter_combo.Enabled = False
            setuptime_number.ReadOnly = True
            cycletime_number.ReadOnly = True
            setupunit_combo.Enabled = False
            cycleunit_combo.Enabled = False
            Button3.Visible = False
            Button2.Visible = False
        End If

        'Load the releases
        Loadreleases()
        'Load the materials
        Loadmaterials()

        loading = False

    End Sub
    Private Sub Loadmaterials()
        Dim mattable As DataTable = GetOrderMaterial(opholder.JobNo, False)

        For Each matrow As DataRow In mattable.Rows

            Dim matitem As New ListViewItem
            'PO 
            If NotNull(matrow.Item(6), "None") <> "None" Then
                matitem.Text = "MAT PO: " & matrow.Item(2).ToString & "---" & matrow.Item(3)
                matitem.SubItems.Add(NotNull(matrow.Item("Qty"), 0) & "/" & NotNull(matrow.Item("POQty"), 0))
                Dim Duedate, Receiveddate As Date
                Duedate = NotNull(matrow.Item(7), "1/1/1900")
                Receiveddate = NotNull(matrow.Item(8), "1/1/1900")
                If Duedate.ToShortDateString = "1/1/1900" Then
                    matitem.SubItems.Add("No Due Date")
                Else
                    matitem.SubItems.Add(Duedate.ToShortDateString)
                End If

                If Receiveddate.ToShortDateString = "1/1/1900" Then
                    matitem.SubItems.Add("Not Received")
                    matitem.BackColor = Color.LightSalmon
                Else

                    matitem.SubItems.Add(Receiveddate.ToShortDateString)
                    matitem.BackColor = Color.LightGreen
                End If


            Else
                If matrow.Item(4) = "Y" Then
                    matitem.Text = "MAIN PART: " & matrow.Item(2).ToString & "---" & matrow.Item(3)
                    matitem.SubItems.Add(NotNull(matrow.Item("Qty"), 0))

                    matitem.SubItems.Add(NotNull(matrow.Item(7).ToString, "No Due Date"))

                    matitem.SubItems.Add(NotNull(matrow.Item(8).ToString, "Not Received"))
                    matitem.BackColor = Color.LightCoral

                Else
                    matitem.Text = "ALLOCATED: " & matrow.Item(2).ToString & "---" & matrow.Item(3)
                    matitem.SubItems.Add(NotNull(matrow.Item("Qty"), 0))

                    matitem.SubItems.Add(NotNull(matrow.Item(8).ToString, "No Due Date"))

                    matitem.SubItems.Add(NotNull(matrow.Item(11).ToString, "Not Received"))
                    matitem.BackColor = Color.LightBlue
                End If
            End If
            Mat_listview.Items.Add(matitem)

        Next



    End Sub

    Private Sub Loadreleases()


        Dim reltable As DataTable = GetOPReleases(opholder)
        For Each relval As DataRow In reltable.Rows
            Dim relitem As New ListViewItem
            'Qty
            relitem.Text = relval.Item(0)
            'Due Date
            relitem.SubItems.Add(relval.Item(1))
            'Date Completed 
            relitem.SubItems.Add(NotNull(relval.Item(2), ""))
            'Comments
            relitem.SubItems.Add(relval.Item(3))
            'Counter
            relitem.SubItems.Add(relval.Item(4))

            ListView1.Items.Add(relitem)
        Next

    End Sub

    Private Sub loadcomboxes()
        FillComboFromDB(opercode_combo, "SELECT DISTINCT OperCode FROM OperCode", "OperCode", True)
        FillComboFromDB(workcenter_combo, "SELECT DISTINCT ShortName FROM WorkCntr", "ShortName", True)
        FillComboFromDB(setupunit_combo, "SELECT DISTINCT TimeUnit FROM Routing", "TimeUnit", True)
        FillComboFromDB(cycleunit_combo, "SELECT DISTINCT CycleUnit FROM Routing", "CycleUnit", True)

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Save the changes to the operation info
        If userholder.EngRights = False Then
            MsgBox("Must have engineering rights to change")
            Exit Sub
        End If

        opholder.OperationCode = opercode_combo.Text
        opholder.WorkCenter = workcenter_combo.Text
        opholder.setupunit = setupunit_combo.Text
        opholder.setuptime = setuptime_number.Value
        opholder.cycleunit = cycleunit_combo.Text
        opholder.cycletime = cycletime_number.Value
        opholder.description = opdescription_textbox.Text
        opholder.duedate = DateTimePicker1.Value

        opholder.updateRouteInfo(False)

        hrsleft_textbox.Text = opholder.totalhoursleft

        AddUserTransaction(Truncate(userholder.Username, 11), "Estimate", opholder.Partno, "ROUTING", "Routing Updated")
        MsgBox("Updated")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Change at part level and order level
        If userholder.EngRights = False Then
            MsgBox("Must have engineering rights to change")
            Exit Sub
        End If

        opholder.OperationCode = opercode_combo.Text
        opholder.WorkCenter = workcenter_combo.Text
        opholder.setupunit = setupunit_combo.Text
        opholder.setuptime = setuptime_number.Value
        opholder.cycleunit = cycleunit_combo.Text
        opholder.cycletime = cycletime_number.Value
        opholder.description = opdescription_textbox.Text
        opholder.updatePartRouteInfo(True)
        hrsleft_textbox.Text = opholder.totalhoursleft

        AddUserTransaction(Truncate(userholder.Username, 11), "Estimate", opholder.Partno, "ROUTING", "Routing Updated", True)
        MsgBox("Updated")
    End Sub

    Private Sub Updateoprouting()

    End Sub
    Private Sub updatepartrouting()

    End Sub

    Private Sub material_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles material_checkbox.CheckedChanged
        If loading = False Then
            opholder.Material = material_checkbox.Checked
            opholder.updatestates()
        End If

    End Sub

    Private Sub program_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles program_checkbox.CheckedChanged

        If loading = False Then
            opholder.Program = program_checkbox.Checked
            opholder.updatestates()
        End If

    End Sub

    Private Sub tools_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles tools_checkbox.CheckedChanged

        If loading = False Then
            opholder.Tooling = tools_checkbox.Checked
            opholder.updatestates()
        End If

    End Sub

    Private Sub completed_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles completed_checkbox.CheckedChanged

        If loading = False Then
            opholder.Completed = completed_checkbox.Checked
            opholder.updatestates()
        End If

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        'If listview selected items count is >0

        If ListView1.SelectedItems.Count > 0 Then
            Dim dateval As DateTime
            dateval = DateTime.Parse(ListView1.SelectedItems(0).SubItems(1).Text)
            'Get the amount of listview selected
            Dim dtselect As New DateSelectorform(dateval)
            If dtselect.ShowDialog = DialogResult.OK Then
                UpdateRelDueDate(dtselect.selecteddate, Integer.Parse(ListView1.SelectedItems(0).SubItems(4).Text))
                ListView1.SelectedItems(0).SubItems(1).Text = dtselect.selecteddate.ToShortDateString
            End If


            Dim earliestdate As DateTime = DateTime.Today.AddYears(100)

            For Each relitem As ListViewItem In ListView1.Items
                If DateTime.Parse(relitem.SubItems(1).Text) < earliestdate And relitem.SubItems(2).Text = "" Then
                    earliestdate = DateTime.Parse(relitem.SubItems(1).Text)
                End If
            Next

            opholder.duedate = earliestdate
            opholder.updateRouteInfo(False)
        End If


    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub shift_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles shift_checkbox.CheckedChanged

        If loading = False Then
            opholder.Oneshift = shift_checkbox.Checked
            opholder.updatestates()
            opholder.enddate = Add_businessdays(opholder.startdate, opholder.totalhoursleft, opholder.Oneshift)
        End If


    End Sub
End Class