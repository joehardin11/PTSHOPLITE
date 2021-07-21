Imports System.Data.SqlClient

Public Class ProductionReporting
    'Dim QtyInput As String
    'Dim PartNoInput As String
    'Dim JobNoInput As String
    'Dim RoutingStepInput As String
    'Dim EmployeeInput As String
    'Dim HoursInput As String
    'Dim SetupInput As Boolean
    'Dim DateProducedInput As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim QtyInput = TextBoxQty.Text
        Dim QtyInputAsInt = 0
        Dim PartNoInput = TextBoxPartNo.Text
        Dim PartNoInputAsInt = 0
        Dim JobNoInput = TextBoxJobNo.Text
        Dim RoutingStepInput = ComboBoxRoutingStep.Text
        Dim EmployeeInput = TextBoxEmployeeNo.Text
        Dim EmployeeInputAsInt = 0
        Dim HoursInput = TextBoxHours.Text
        Dim HoursInputAsInt = 0
        Dim SetupInput = CheckBox1.Checked
        Dim DateProducedInput = DateTimePicker1.Value

        'check if all boxes are filled out correctly
        If QtyInput = "" Or PartNoInput = "" Or JobNoInput = "" Or RoutingStepInput = "" Or EmployeeInput = "" Or HoursInput = "" Then
            MsgBox("All Boxes Must be Filled Out", vbOKOnly)
            TextBoxQty.Select()
            GoTo Line1
        End If

        If Integer.TryParse(QtyInput, QtyInputAsInt) Then
            QtyInputAsInt = QtyInput
        Else
            MsgBox("QTY Must be a Number", vbOKOnly)
            TextBoxQty.Select()
            GoTo Line1
        End If

        If Integer.TryParse(HoursInput, HoursInputAsInt) Then
            HoursInputAsInt = HoursInput
        Else
            MsgBox("HOURS Must be a Number", vbOKOnly)
            TextBoxQty.Select()
            GoTo Line1
        End If

        If Integer.TryParse(EmployeeInput, EmployeeInputAsInt) Then
            EmployeeInputAsInt = EmployeeInput
        Else
            MsgBox("EMPLOYEE NO Must be a Number", vbOKOnly)
            TextBoxQty.Select()
            GoTo Line1
        End If

        If Integer.TryParse(PartNoInput, PartNoInputAsInt) Then
            PartNoInputAsInt = PartNoInput
        Else
            MsgBox("PART NO Must be a Number", vbOKOnly)
            TextBoxQty.Select()
            GoTo Line1
        End If

        If CheckJobNumberFormat(JobNoInput) = False Then
            MsgBox("Please Enter a Valid Job Number", vbOKOnly)
            TextBoxJobNo.Select()
            GoTo Line1
        End If

        'sql write statement
        'AddProductionToSql(QtyInputAsInt, PartNoInputAsInt, JobNoInput, RoutingStepInput, EmployeeInputAsInt, HoursInputAsInt, SetupInput, DateProducedInput)

        'clear boxes
        ClearForm()

Line1:
    End Sub

    Private Sub TextBoxBarCode_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxBarcode.KeyUp
        Dim BarCodeInput = TextBoxBarcode.Text
        Dim BarCodeJobNo As String
        Dim BarCodeRoutStep As String
        Dim BarCodeInfo3 As String
        'Dim BarCodeInfo4 = BarCodeInput

        'if enter is pressed then continue
        If e.KeyCode = Keys.Enter Then
            If BarCodeInput.Length < 16 Then Exit Sub
            'Parse the Input
            BarCodeJobNo = BarCodeInput.Substring(0, 9)
            BarCodeRoutStep = BarCodeInput.Substring(10, 2)
            BarCodeInfo3 = BarCodeInput.Substring(13, 3)

            'Fill the JobNo
            If CheckJobNumberFormat(BarCodeJobNo) = True Then TextBoxJobNo.Text = BarCodeJobNo

            'Fill the PartNo
            Dim PartNo = GetPartNumberFromJobNo(BarCodeJobNo)
            If IsNothing(PartNo) = False Then TextBoxPartNo.Text = BarCodeJobNo

            'fill the routing step combobox and pick it 

            'clear the box 
            TextBoxBarcode.Clear()

            TextBoxQty.Select()
        End If

    End Sub

    Private Sub TextBoxQty_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxQty.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub

    Private Sub TextBoxHours_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxHours.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub

    Private Sub TextBoxEmployeeNo_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxEmployeeNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub

    Private Sub TextBoxJobNo_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxJobNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub

    Private Sub TextBoxPartNo_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxPartNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub

    Private Sub ComboBoxRoutingStep_KeyUp(sender As Object, e As KeyEventArgs) Handles ComboBoxRoutingStep.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub

    Public Function AddProductionToSql(Qty, PartNo, JobNo, RoutingStep, Employee, Hours, Setup, ProdDate)
        Dim sqlconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim commtext As String = "INSERT into ProductionReporting (Qty, PartNo, JobNo, RoutingStep, Employee, Hours, Setup, ProdDate, EntDate) " &
            "VALUES (@Qty, @PartNo, @JobNo, @RoutingStep, @Employee, @Hours, @Setup, @ProdDate, @EntDate); SELECT SCOPE_IDENTITY()"

        Dim EntDate As Date = DateAndTime.Now

        Dim sqlcomm As New SqlCommand(commtext, sqlconn)
        sqlcomm.Parameters.AddWithValue("@Qty", Qty)
        sqlcomm.Parameters.AddWithValue("@PartNo", PartNo)
        sqlcomm.Parameters.AddWithValue("@JobNo", JobNo)
        sqlcomm.Parameters.AddWithValue("@RoutingStep", RoutingStep)
        sqlcomm.Parameters.AddWithValue("@Employee", Employee)
        sqlcomm.Parameters.AddWithValue("@Hours", Hours)
        sqlcomm.Parameters.AddWithValue("@Setup", Setup)
        sqlcomm.Parameters.AddWithValue("@ProdDate", ProdDate)
        sqlcomm.Parameters.AddWithValue("@EntDate", EntDate)
        Dim fileint As Integer = -1

        Try
            sqlconn.Open()
            fileint = sqlcomm.ExecuteScalar
            sqlconn.Close()

        Catch ex As System.Exception
            MsgBox("Document Entry Error: " & ex.Message)
        End Try

        Return fileint

    End Function

    Private Sub TextBoxJobNo_Leave(sender As Object, e As EventArgs) Handles TextBoxJobNo.Leave
        Dim CurrentPartNo As String

        'parse job number and retreve part number
        Dim JobNoInput = TextBoxJobNo.Text
        If CheckJobNumberFormat(JobNoInput) = True Then CurrentPartNo = GetPartNumberFromJobNo(JobNoInput)
        TextBoxPartNo.Text = CurrentPartNo

        'take part number and fill the routing combobox
        If IsNothing(CurrentPartNo) = False Then FillComboFromDB(ComboBoxRoutingStep, "SELECT StepNo, StepNo & ' - ' & OperCode AS OperDesc FROM Routing WHERE PartNo = '" & CurrentPartNo & "' ORDER BY StepNo", "OperDesc", True, "StepNo")

    End Sub

    Private Sub TextBoxPartNo_Leave(sender As Object, e As EventArgs) Handles TextBoxPartNo.Leave

        Dim PartNoInput = TextBoxPartNo.Text
        CheckPartNoFormat(PartNoInput)

    End Sub

    Public Function CheckJobNumberFormat(JobNoToCheck As String)
        If JobNoToCheck.Length() = 9 Then
            If JobNoToCheck.IndexOf("-") = 6 Then
                Dim JobNoHolder = Replace(JobNoToCheck, "-", "")
                Dim JobNoAsInt = 0
                If Integer.TryParse(JobNoHolder, JobNoAsInt) Then

                Else
                    Label_InvalidJob.Visible = True
                    Return False
                End If
            Else
                Label_InvalidJob.Visible = True
                Return False
            End If
        Else
            Label_InvalidJob.Visible = True
            Return False
        End If

        Label_InvalidJob.Visible = False
        Return True
    End Function

    Public Function CheckPartNoFormat(PartNoToCheck As String)
        If PartNoToCheck.Length() = 6 Then
            Dim PartNoAsInt = 0
            If Integer.TryParse(PartNoToCheck, PartNoAsInt) Then

            Else
                Label_InvalidPart.Visible = True
                Return False
            End If
        Else
            Label_InvalidPart.Visible = True
            Return False
        End If

        Label_InvalidPart.Visible = False
        Return True

    End Function

    Public Function GetPartNumberFromJobNo(JobNoInput As String)

        Dim PartNoFromJobNoDT = GetDTFromString("SELECT PartNo FROM OrderDet WHERE JobNo = '" & JobNoInput & "'", True)

        If PartNoFromJobNoDT.Rows.Count = 1 Then
            Dim PartNoFromJobNo = PartNoFromJobNoDT.Rows(0).Item(0)
            Return PartNoFromJobNo
        Else
            MsgBox("No Data Found for JobNo Input, Please Double Check JobNo", vbOKOnly)
            Return Nothing
        End If

        Return Nothing

    End Function

    Private Sub ClearForm() Handles ToolStripButton2.Click

        TextBoxQty.Clear()
        TextBoxHours.Clear()
        TextBoxEmployeeNo.Clear()
        TextBoxJobNo.Clear()
        TextBoxPartNo.Clear()
        ComboBoxRoutingStep.Text = ""
        DateTimePicker1.Value = Now
        CheckBox1.Checked = False
        TextBoxBarcode.Clear()
        TextBoxBarcode.Select()
        Label_InvalidJob.Visible = False
        Label_InvalidPart.Visible = False

    End Sub
End Class