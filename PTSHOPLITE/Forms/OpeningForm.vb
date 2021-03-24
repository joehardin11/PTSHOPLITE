Public Class OpeningForm
    Private Sub GoButton_Click(sender As Object, e As EventArgs) Handles GoButton.Click
        JobInfoForm.Show()

    End Sub

    Private Sub OpeningForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class