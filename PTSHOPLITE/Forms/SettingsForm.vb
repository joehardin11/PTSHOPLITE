Public Class SettingsForm
    Dim userholder As UserValues

    Public Sub New(userinfo As UserValues)
        userholder = userinfo

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadtextboxes()
        loadcheckboxes()
        loadnumbers()
        LoadPartinfo()
        loadflagginginfo

    End Sub
    Private Sub loadflagginginfo()
        flag_listview.Items.Clear()
        areas_listview.Items.Clear()


        'Get the flags
        Dim flagtypedt As DataTable = Getflagtypesdt()

        For Each typerow As DataRow In flagtypedt.Rows

            Dim typeval As New Flagtype(Integer.Parse(typerow.Item(0).ToString))
            Dim typeitem As New ListViewItem(typeval.flagtype)

            Dim areastring As String = ""

            'Get all the area information into a string
            For Each arearow In typeval.Areas.Rows
                areastring = areastring & arearow.item(2)
                areastring = areastring & ", "
            Next

            If areastring.Length > 2 Then
                areastring = Truncate(areastring, areastring.Length - 2)
            End If

            typeitem.SubItems.Add(areastring)
            typeitem.SubItems.Add(typeval.Id)

            flag_listview.Items.Add(typeitem)

        Next

        'Get the Areas
        Dim areadt As DataTable = GetAreas()

        For Each arearow As DataRow In areadt.Rows
            Dim areaitem As New ListViewItem(arearow.Item(1).ToString)
            areaitem.SubItems.Add(arearow.Item(0).ToString)
            areas_listview.Items.Add(areaitem)
        Next

    End Sub


    Private Sub loadtextboxes()
        textbox_partdatabase.Text = My.Settings.E2Database
        textbox_shopdatabase.Text = My.Settings.PartDatabaseString
        textbox_dcfolder.Text = My.Settings.DCFolder
        textbox_PartialInspectionFile.Text = My.Settings.Partial_InspectionFile
        textbox_FullInspectionFile.Text = My.Settings.Full_InspectionFile
        textbox_setupfolder.Text = My.Settings.SetupFolder
        textbox_shopdefault.Text = My.Settings.DefaultProgramLocation
        textbox_partquery.Text = My.Settings.GetPartQuery
        dymotextbox.Text = My.Settings.DYMOPrinter
        tempfiles_textbox.Text = My.Settings.TempFileFolder
        orderfiles_textbox.Text = My.Settings.OrderFileLocation
        Lot_Textbox.Text = My.Settings.DymoMatLotLabel

    End Sub

    Private Sub loadcheckboxes()

        checkbox_partdbe2.Checked = My.Settings.E2Parts
        CheckBox_doctransfer.Checked = My.Settings.E2Document
        CheckBox1.Checked = My.Settings.SQLPT
    End Sub
    Private Sub LoadPartinfo()
        PartLabel_textbox.Text = My.Settings.DymoPartLabel.ToString
        MaterialLabel_Textbox.Text = My.Settings.DYMOMatLabel.ToString
        JobLabel_Textbox.Text = My.Settings.DYMOJobLabel.ToString
        Partdescrip_Value.Value = My.Settings.DymoPartLabeldescriplength
        Materialdescrip_val.Value = My.Settings.dymomatlabeldescriplength
        Jobdescrip_value.Value = My.Settings.dymojoblabelmaxlen

    End Sub

    Private Sub loadnumbers()
        Try
            minpartnumber.Value = My.Settings.MinPartNumber
            minhardwarenumber.Value = My.Settings.MinHardNumber
            minmetalnumber.Value = My.Settings.MinMetNumber
            minmaterialnumber.Value = My.Settings.MinMatNumber

            maxpartnumber.Value = My.Settings.MaxPartNumber
            maxhardwarenumber.Value = My.Settings.MaxHardNumber
            maxmetalnumber.Value = My.Settings.MaxMetNumber
            maxmaterialnumber.Value = My.Settings.MaxMatNumber

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Ask for new value. 
        Dim newvalue As String = InputBox("Edit Setting", "Edit Setting", My.Settings.E2Database)
        'Insert new value
        My.Settings.E2Database = newvalue
        My.Settings.Item("BLSDATAConnectionString") = newvalue

        loadtextboxes()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Ask for new value.
        Dim newvalue As String = InputBox("Edit Setting", "Edit Setting", My.Settings.PartDatabaseString)
        'Insert new value
        My.Settings.PartDatabaseString = newvalue
        My.Settings.Item("SHOPDBConnectionString1") = newvalue


        loadtextboxes()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        'Open file/folder dialog
        FolderBrowserDialog1.SelectedPath = My.Settings.QCFolder

        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'If successful, set the folder to new path
            My.Settings.QCFolder = FolderBrowserDialog1.SelectedPath.ToString
            My.Settings.Save()

        End If

        loadtextboxes()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Open file/folder dialog
        FolderBrowserDialog1.SelectedPath = My.Settings.SetupFolder

        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'If successful, set the folder to new path
            My.Settings.SetupFolder = FolderBrowserDialog1.SelectedPath.ToString
            My.Settings.Save()

        End If

        loadtextboxes()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Open file/folder dialog
        FolderBrowserDialog1.SelectedPath = My.Settings.DCFolder

        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'If successful, set the folder to new path
            My.Settings.DCFolder = FolderBrowserDialog1.SelectedPath.ToString
            My.Settings.Save()
        End If

        loadtextboxes()
    End Sub
    Private Sub orderfiles_Button_Click(sender As Object, e As EventArgs) Handles orderfiles_Button.Click
        'Open file/folder dialog
        FolderBrowserDialog1.SelectedPath = My.Settings.OrderFileLocation

        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'If successful, set the folder to new path
            My.Settings.OrderFileLocation = FolderBrowserDialog1.SelectedPath.ToString
            My.Settings.Save()
        End If

        loadtextboxes()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Open file/folder dialog
        FolderBrowserDialog1.SelectedPath = My.Settings.DefaultProgramLocation

        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'If successful, set the folder to new path
            My.Settings.DefaultProgramLocation = FolderBrowserDialog1.SelectedPath.ToString
            My.Settings.Save()
        End If

        loadtextboxes()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub checkbox_partdbe2_CheckedChanged(sender As Object, e As EventArgs)
        My.Settings.E2Parts = checkbox_partdbe2.Checked
        loadcheckboxes()

    End Sub

    Private Sub CheckBox_doctransfer_CheckedChanged(sender As Object, e As EventArgs)
        My.Settings.E2Document = CheckBox_doctransfer.Checked
        loadcheckboxes()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        My.Settings.SQLPT = CheckBox1.Checked
        loadcheckboxes()
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub tempfiles_button_Click(sender As Object, e As EventArgs) Handles tempfiles_button.Click

        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'If successful, set the folder to new path
            My.Settings.TempFileFolder = FolderBrowserDialog1.SelectedPath.ToString
            My.Settings.Save()
        End If

        loadtextboxes()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        My.Settings.DYMOPrinter = dymotextbox.Text
        My.Settings.Save()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        My.Settings.MinPartNumber = minpartnumber.Value
        My.Settings.MinHardNumber = minhardwarenumber.Value
        My.Settings.MinMetNumber = minmetalnumber.Value
        My.Settings.MinMatNumber = minmaterialnumber.Value

        My.Settings.MaxPartNumber = maxpartnumber.Value
        My.Settings.MaxHardNumber = maxhardwarenumber.Value
        My.Settings.MaxMetNumber = maxmetalnumber.Value
        My.Settings.MaxMatNumber = maxmaterialnumber.Value

        My.Settings.dymojoblabelmaxlen = Jobdescrip_value.Value
        My.Settings.dymomatlabeldescriplength = Materialdescrip_val.Value
        My.Settings.DymoPartLabeldescriplength = Partdescrip_Value.Value

        My.Settings.Save()

        DialogResult = DialogResult.OK

    End Sub

    Private Sub PartLabelBrowse_button_Click(sender As Object, e As EventArgs) Handles PartLabelBrowse_button.Click
        OpenFileDialog1.Multiselect = False


        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.DymoPartLabel = OpenFileDialog1.FileName
            My.Settings.Save()
            PartLabel_textbox.Text = My.Settings.DymoPartLabel

        End If
    End Sub

    Private Sub MaterialLabelBrowse_button_Click(sender As Object, e As EventArgs) Handles MaterialLabelBrowse_button.Click
        OpenFileDialog1.Multiselect = False


        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.DYMOMatLabel = OpenFileDialog1.FileName
            My.Settings.Save()
            MaterialLabel_Textbox.Text = My.Settings.DYMOMatLabel
        End If
    End Sub

    Private Sub JobLabelBrowse_Button_Click(sender As Object, e As EventArgs) Handles JobLabelBrowse_Button.Click
        OpenFileDialog1.Multiselect = False


        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.DYMOJobLabel = OpenFileDialog1.FileName
            My.Settings.Save()
            JobLabel_Textbox.Text = My.Settings.DYMOJobLabel
        End If
    End Sub

    'Private Sub editflagareas_button_Click(sender As Object, e As EventArgs) Handles editflagareas_button.Click
    '    If flag_listview.SelectedItems.Count = 0 Then
    '        MsgBox("Nothing Selected")
    '        Exit Sub

    '    End If
    '    Dim flagid As Integer
    '    flagid = Integer.Parse(flag_listview.SelectedItems(0).SubItems(2).Text)

    '    Dim areaediter As New editareaconnform(userholder, flagid)

    '    areaediter.ShowDialog()

    '    loadflagginginfo()


    'End Sub
    '***FLAG AND AREA BUTTON SUBS
    Private Sub addflag_button_Click(sender As Object, e As EventArgs) Handles addflag_button.Click

        Dim flagtypestring As String
        flagtypestring = InputBox("Enter the new flag type", "New Flag", "")
        AddnewFlagtype(flagtypestring)
        loadflagginginfo()

    End Sub

    Private Sub deleteflag_button_Click(sender As Object, e As EventArgs) Handles deleteflag_button.Click

        Dim flagid As Integer

        If flag_listview.SelectedItems.Count > 0 Then
            flagid = Integer.Parse(flag_listview.SelectedItems(0).SubItems(2).Text)
        Else
            MsgBox("No Flag Selected")

            Exit Sub

        End If

        If Deleteflagtype(flagid) = True Then
            loadflagginginfo()
        End If

    End Sub

    Private Sub editflag_button_Click(sender As Object, e As EventArgs) Handles editflag_button.Click

        'Edit Flagname
        Dim flagid As Integer
        If flag_listview.SelectedItems.Count > 0 Then

            flagid = Integer.Parse(flag_listview.SelectedItems(0).SubItems(2).Text)

        Else
            MsgBox("No Flag Selected")

            Exit Sub

        End If

        'Do something 




    End Sub

    Private Sub addarea_button_Click(sender As Object, e As EventArgs) Handles addarea_button.Click

        'add a new area
        Dim areainput As String
        areainput = InputBox("Add new Area of Focus Name", "New Area of Focus")
        Addarea(areainput)

        loadflagginginfo()

    End Sub

    Private Sub deletearea_button_Click(sender As Object, e As EventArgs) Handles deletearea_button.Click

        If areas_listview.SelectedItems.Count < 0 Then
            MsgBox("No area selected")
            Exit Sub

        End If

        'Delete Area
        Dim areaid As Integer
        areaid = Integer.Parse(areas_listview.SelectedItems.Item(0).SubItems(1).Text)
        Dim areastring As String
        areastring = areas_listview.SelectedItems.Item(0).SubItems(0).Text

        DeleteArea(areaid)


        loadflagginginfo()

    End Sub

    Private Sub Lot_Textbox_button_Click(sender As Object, e As EventArgs) Handles Lot_Textbox_button.Click
        OpenFileDialog1.Multiselect = False


        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.DymoMatLotLabel = OpenFileDialog1.FileName
            My.Settings.Save()
            Lot_Textbox.Text = My.Settings.DymoMatLotLabel
        End If
    End Sub

End Class