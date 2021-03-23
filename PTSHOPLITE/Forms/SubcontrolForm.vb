Public Class SubcontrolForm
    Private controlid As Integer
    Private partholder As Part
    Private opholder As Operation
    Private userdefault As UserValues
    ''' <summary>
    ''' 1. = Doc, 2. = Setup, 3. = 
    ''' </summary>
    ''' <param name="controltoload"></param>
    ''' <param name="userholder"></param>
    ''' <param name="operationh"></param>
    Public Sub New(controltoload As Integer, userholder As UserValues, Optional operationh As Operation = Nothing)

        controlid = controltoload
        partholder = New Part(operationh.Partno)
        opholder = operationh
        userdefault = userholder
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(controltoload As Integer, userholder As UserValues, parth As Part)

        controlid = controltoload
        partholder = parth
        userdefault = userholder
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub SubcontrolForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If controlid = 1 Then
            'load the document control
            loaddoccontrol()
            Me.Text = "Document Control"
        ElseIf controlid = 2 Then
            'Load the setup control
            loadsetupcontrol()
            Me.Text = "Setup Control"
        ElseIf controlid = 3 Then
            'Load the part control

        ElseIf controlid = 4 Then
            'Load the inspection control
        ElseIf controlid = 5 Then
            'load the machineviewer
            loadmachinejob()
            Me.Text = "Machine Status"


        End If
    End Sub
    Private Sub loadmachinejob()

        Dim machineview As New MachineJobViewer
        machineview.Dock = DockStyle.Fill
        Panel1.Controls.Clear()
        Panel1.Controls.Add(machineview)

    End Sub

    Private Sub loadsetupcontrol()
        Dim setupholder As New SetupControl(userdefault, True, opholder)
        setupholder.Dock = DockStyle.Fill
        Panel1.Controls.Clear()
        Panel1.Controls.Add(setupholder)

    End Sub
    Private Sub loaddoccontrol()
        'Create new document control 
        Dim dockcontrol As New DocumentControl(partholder, Userdefault, True)
        dockcontrol.Dock = DockStyle.Fill
        Panel1.Controls.Clear()
        Panel1.Controls.Add(dockcontrol)


    End Sub

End Class