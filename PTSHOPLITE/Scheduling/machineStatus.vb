Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class machineStatus

    Dim machineholder As String
    Dim loading As Boolean
    Dim machineops As DataTable


    Public Sub New(machine As String, Showstatus As Boolean, machineopstable As DataTable)

        machineholder = machine
        machineops = machineopstable
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub loadcomboboxes()
        Dim oleboolean As Boolean = True
        Dim DTvalue As New DataTable
        DTvalue = Nothing

        Dim dtvalue2 As DataTable
        dtvalue2 = DTvalue


    End Sub

    Private Sub machineStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loading = True
        loadcomboboxes()
        'Change size of box
        Me.Width = My.Settings.machineviewerwidth
        Me.Height = My.Settings.machineviewerheight
        Me.Font = My.Settings.machineviewerfont

        'Get the current status of the machine
        Name_label.Text = machineholder
        'Get the machine info

        'Get the current job according to the schedule
        'Get all the machine operations
        Dim PrevJobNo As String
        Dim prevstepno As String


        'Get the jobs according to the schedule
        If machineops.Rows.Count > 0 Then

            PrevJobNo = machineops.Rows(0).Item(0).ToString
            prevstepno = machineops.Rows(0).Item(1).ToString
            WO1_label.Text = machineops.Rows(0).Item(0).ToString & "|" & machineops.Rows(0).Item(1).ToString
            wo1descrip_label.Text = machineops.Rows(0).Item(5)
            duedate1_label.Text = FormatDateTime(machineops.Rows(0).Item(4).ToString, DateFormat.ShortDate)
            If FormatDateTime(machineops.Rows(0).Item(4).ToString, DateFormat.ShortDate) < Today Then
                duedate1_label.BackColor = Color.Red
            End If
            If machineops.Rows.Count > 1 Then
                Dim opscount As Integer = 1
                Dim finishedop As Boolean

                While (machineops.Rows.Count - 1) >= opscount And finishedop = False
                    If PrevJobNo = machineops.Rows(opscount).Item(0).ToString Then
                        'if the step is larger than the previous step then make it go after
                        If prevstepno > machineops.Rows(0).Item(1).ToString Then
                            WO1_label.Text = machineops.Rows(0).Item(0).ToString & "|" & machineops.Rows(0).Item(1).ToString & -machineops.Rows(opscount).Item(1).ToString
                        Else
                            WO1_label.Text = machineops.Rows(0).Item(0).ToString & "|" & machineops.Rows(opscount).Item(1).ToString & -machineops.Rows(0).Item(1).ToString
                        End If
                    Else
                        finishedop = True
                    End If
                    opscount = opscount + 1
                End While


                If machineops.Rows.Count >= opscount Then
                    opscount = opscount - 1
                End If

                WO2_label.Text = machineops.Rows(opscount).Item(0).ToString & "|" & machineops.Rows(opscount).Item(1).ToString
                wo2descrip_label.Text = machineops.Rows(opscount).Item(5)
                duedate2_label.Text = FormatDateTime(machineops.Rows(opscount).Item(4).ToString, DateFormat.ShortDate)
                If FormatDateTime(machineops.Rows(opscount).Item(4).ToString, DateFormat.ShortDate) < Today Then
                    duedate2_label.BackColor = Color.Red
                End If

            Else
                WO2_label.Text = "None"
                wo2descrip_label.Text = "None"
                duedate2_label.Text = "Due:"
            End If

        Else
            WO1_label.Text = "None"
            wo1descrip_label.Text = "None"
            duedate1_label.Text = "Due:"

        End If



        loading = False

    End Sub
    Private Sub StatusColor(Statusval As String)

        If Statusval = "Running" Then
            status_label.BackColor = Color.Green
        ElseIf Statusval = "Awaiting QC" Then
            status_label.BackColor = Color.LightYellow
        ElseIf Statusval = "Maintenance" Then
            status_label.BackColor = Color.MistyRose
        ElseIf Statusval = "Setup" Then
            status_label.BackColor = Color.Lime
        ElseIf Statusval = "Off" Then
            status_label.BackColor = Color.Red
        ElseIf Statusval = "Need Material" Then
            status_label.BackColor = Color.Orange
        ElseIf Statusval = "Need Tech Support" Then
            status_label.BackColor = Color.Pink
        ElseIf Statusval = "Need Tool" Then
            status_label.BackColor = Color.MediumPurple
        End If

    End Sub
    Private Sub WO1_label_Click(sender As Object, e As EventArgs) Handles WO1_label.Click
        'Look up the work order info


    End Sub

    Private Sub WO2_label_Click(sender As Object, e As EventArgs) Handles WO2_label.Click
        'Look up the work order info


    End Sub

End Class
