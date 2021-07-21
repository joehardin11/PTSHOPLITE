Public Class JobInfoForm



    Private Sub JobInfoForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

    End Sub
    'Private Sub LoadSalesorders(e As TreeViewEventArgs)
    '    JobLoading_Progressbar.Visible = True
    '    JobLoading_Progressbar.Value = 0
    '    JobLoading_Progressbar.Maximum = 100

    '    'treeview1change = True
    '    'If e.Node.Text = "Open Jobs" Then
    '    '    Exit Sub
    '    'End If

    '    Dim jobno As String
    '    If e.Node.Level > 0 Then
    '        'get the parent level 
    '        jobno = Mid(e.Node.Parent.Text, 1, InStr(e.Node.Parent.Text, "||") - 1)

    '    Else
    '        'Get the same level since anything not greater than 0 is going to be the parent
    '        jobno = Mid(e.Node.Text, 1, InStr(e.Node.Text, "||") - 1)
    '    End If

    '    'Get all the job numbers for that part

    '    clearorderform()
    '    Dim optable As DataTable = GetJobSteps(True, jobno)
    '    Ops_Treeview.Nodes.Clear()
    '    JobLoading_Progressbar.Value = 10

    '    For Each opr As DataRow In optable.Rows

    '        Dim timestring, workcntr As String
    '        If opr.Item(7) = 1 Then
    '            'If its a vendor
    '            workcntr = opr.Item(9)
    '            timestring = " LeadTime: " & opr.Item(8) & " Days"
    '        Else
    '            workcntr = opr.Item(2) & "-" & opr.Item(0)
    '            timestring = " S/U: " & opr.Item(5) & " " & opr.Item(6) & " C/T: " & opr.Item(3) & " " & opr.Item(4)
    '        End If
    '        Dim newnode As New TreeNode(opr.Item(1) & "|" & workcntr & timestring)

    '        If opr.Item(10) = True Then
    '            newnode.BackColor = Color.LightBlue
    '        End If

    '        Ops_Treeview.Nodes.Add(newnode)

    '    Next
    '    JobLoading_Progressbar.Value = 30
    '    'Get the releases

    '    Dim releasetable As DataTable = GetJobReleases(jobno)
    '    For Each relrow As DataRow In releasetable.Rows
    '        Dim reltreeitem As New TreeNode

    '        reltreeitem.Text = "Qty: " & relrow.Item(0) & "---" & NotNull(relrow.Item(3), "")
    '        reltreeitem.Nodes.Add(" Due:" & relrow.Item(1))
    '        reltreeitem.Nodes.Add("Completed: " & NotNull(relrow.Item(2), "Not Completed"))
    '        Release_treeview.Nodes.Add(reltreeitem)

    '    Next
    '    JobLoading_Progressbar.Value = 40
    '    Release_treeview.ExpandAll()


    '    '***Load the job information
    '    Dim jobval As New Order(jobno, True, False)

    '    'Load Job info
    '    Dim loadpartholder As New Part(True, jobval.Partno)
    '    partholder = loadpartholder

    '    partno_textbox.Text = partholder.Partno
    '    Jobnote_textbox.Text = partholder.Comments
    '    partdescription_textbox.Text = partholder.Description
    '    altpartNo_textbox.Text = partholder.AltPartno
    '    Revision_textbox.Text = partholder.Revision
    '    dwgno_textbox.Text = partholder.Drawing
    '    treeview1change = False
    '    orderholder = jobval
    '    orderqty_textbox.Text = orderholder.OrderQty
    '    stockqty_textbox.Text = orderholder.StockQty
    '    makeqty_textbox.Text = orderholder.MakeQty

    '    Top_toolstrip.Text = "Job No:" & jobno

    '    '*** LOad Documents
    '    Loaddocuments()
    '    JobLoading_Progressbar.Value = 50
    '    '***LOAD Material
    '    LoadOrdermaterial(jobno)
    '    JobLoading_Progressbar.Value = 60
    '    '***LOAD BLANKS
    '    Loadblankinfo()
    '    JobLoading_Progressbar.Value = 70
    '    '***Load OrderDocs
    '    Loadorderdocuments()
    '    '*** Load Job
    '    JobLoading_Progressbar.Value = 80
    '    LoadJobSchedule(jobno)

    '    LoadInspections

    '    JobLoading_Progressbar.Value = 100
    '    JobLoading_Progressbar.Visible = False


    '    Job_label.Text = "Job No: " & jobno

    'End Sub

    'Private Sub clearorderform()

    '    partno_textbox.Text = ""
    '    altpartNo_textbox.Text = ""
    '    dwgno_textbox.Text = ""
    '    Revision_textbox.Text = ""
    '    Jobnote_textbox.Text = ""
    '    partdescription_textbox.Text = ""
    '    orderholder = Nothing
    '    partholder = Nothing
    '    orderqty_textbox.Text = ""
    '    stockqty_textbox.Text = ""
    '    makeqty_textbox.Text = ""

    '    Mat_Treeview.Nodes.Clear()
    '    Ops_Treeview.Nodes.Clear()
    '    Release_treeview.Nodes.Clear()

    '    BindingSource1.DataSource = Nothing

    '    Job_label.Text = "Job No: None"
    'End Sub

End Class
