Public Class OrderFormOutline
    Public orderid As Integer = -1

    Private Sub OrderFormOutline_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Cancel_button_Click(sender As Object, e As EventArgs) Handles Cancel_button.Click

        orderid = -1

        DialogResult = DialogResult.Cancel



    End Sub

    Private Sub Enter_button_Click(sender As Object, e As EventArgs) Handles Enter_button.Click

        'Add the order


        'Add Order Relationships


        'Add the ordersteps




        DialogResult = DialogResult.OK

    End Sub
End Class