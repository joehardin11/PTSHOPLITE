Imports System.Data.SqlClient

Public Class AddOperationForm
    Dim partId As Part
    Dim userholder As UserValues
    Public operationid As Integer

    Public Sub New(User As UserValues, Parth As Part)
        userholder = User
        partId = Parth
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Add Button

        'Description
        Dim description As String = TextBox1.Text

        Dim maxOPint As Integer = 1 'Maximum integer
        'Define the query
        Dim maxOpselect As String
        maxOpselect = "SELECT MAX(PartOpNo) FROM Operation WHERE PartId = @partid"
        Dim maxcomm As New SqlCommand(maxOpselect)
        maxcomm.Parameters.AddWithValue("@partid", partId.Partno)
        'Get the integer using a scalar command. 
        Dim intholder As Object = ScalarSelect(maxcomm)
        If IsNothing(intholder) = False And IsDBNull(intholder) = False Then
            maxOPint = intholder + 10
        Else
            intholder = 10
        End If

        '****Insert new operation into database
        'Get the new ID that was created for the new operation
        'Default is 0. This will be used as error recognition
        Dim opIDvalue As Integer = 0

        'insertion string
        Dim insertstring As String
        insertstring = "INSERT INTO Operation (PartId, Description, PartOpNo) VALUES (@partid, @description, @opno) SELECT SCOPE_IDENTITY()"
        Dim opcomm As New SqlCommand(insertstring)

        'Define the parameters
        opcomm.Parameters.AddWithValue("@partid", partId.Partno)
        opcomm.Parameters.AddWithValue("@description", description)
        opcomm.Parameters.AddWithValue("@opno", maxOPint)

        'Call the scalar query
        opIDvalue = ScalarSelect(opcomm)
        operationid = opIDvalue
        '****Add setup for operation
        AddSetup(opIDvalue, partId.Partno, description)


        '****Add inspection for operation
        If inspection_checkbox.Checked = True Then
            'add the inspection of the checkbox is checked
            AddInspection(opIDvalue, partId, description, userholder)
        End If

        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub AddOperationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load the partids
        Partid_label.Text = partId.Partno
        descriptionlabel.Text = partId.Description
        inspection_checkbox.Checked = True

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Copy Operation from another part

    End Sub

    Private Sub E2OPS_button_Click(sender As Object, e As EventArgs) Handles E2OPS_button.Click
        'Test e2 connection
        If Teste2connections() = False Then
            Exit Sub
        End If

        'e2 operations added
        AddAlle2Operations(partId)

        'for each operation in the table
        DialogResult = DialogResult.OK



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Cancel the adding
        DialogResult = Windows.Forms.DialogResult.Cancel

    End Sub
End Class