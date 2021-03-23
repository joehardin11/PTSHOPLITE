Public Class SelectOperationform
    Public opselected As String = ""
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = Windows.Forms.DialogResult.Cancel

    End Sub

    Private Sub LoadOPcombox()
        FillComboFromDB(ComboBox1, "SELECT DISTINCT OperCode From OperCode", "OperCode", True)

        ComboBox1.SelectedIndex = -1

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Make sure the combobox has something in it
        If ComboBox1.Text = "" Or ComboBox1.Text Is Nothing Then
            Exit Sub
        Else
            'Set result as OK
            opselected = ComboBox1.Text
            DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub

    Private Sub SelectOperationform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadOPcombox()
    End Sub
End Class