Public Class OpeningForm
    Dim orderholder As Order = Nothing
    Dim opholder As Operation
    Dim partholder As Part

    Private Sub GoButton_Click(sender As Object, e As EventArgs) Handles GoButton.Click

        'Dim InputType = ParseInput(TextBox1.Text)
        'JobInfoForm.Show()

    End Sub

    Private Sub OpeningForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then GoButton_Click(sender, e)
    End Sub

    Public Function ParseInput(Input As String)

        Dim InputInt As Integer = 0

        Integer.TryParse(Input, InputInt)

        If InputInt = 0 Then Return Nothing

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ProductionReporting.Show()
    End Sub
End Class