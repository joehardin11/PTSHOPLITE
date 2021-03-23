Public Class SelectComboForm
    Public ReturnedValue As String
    Private datacombotable As DataTable
    Private valuecol As String
    Private viewcol As String
    Private defaultvalue As String
    Private forcedtext As Boolean


    ''' <summary>
    ''' Custom Combobox Holder that uses a datatable to fill the form. 
    ''' </summary>
    ''' <param name="Dataholder"></param>
    ''' <param name="formtitle"></param>
    ''' <param name="valuecolumn"></param>
    ''' <param name="viewcolumn"></param>
    Public Sub New(Dataholder As DataTable, formtitle As String, valuecolumn As String, viewcolumn As String, Optional Defaultval As String = "", Optional Forcetext As Boolean = False)

        datacombotable = Dataholder
        valuecol = valuecolumn
        viewcol = viewcolumn

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Text = formtitle
        forcedtext = Forcetext


        defaultvalue = Defaultval
    End Sub

    Private Sub SelectComboForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.DataSource = datacombotable
        ComboBox1.ValueMember = valuecol
        ComboBox1.DisplayMember = viewcol
        If defaultvalue <> "" Then
            ComboBox1.SelectedIndex() = ComboBox1.FindString(defaultvalue)
        Else
            ComboBox1.SelectedIndex() = 0
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If forcedtext = True Then
            ReturnedValue = ComboBox1.Text
        Else

            If IsNothing(ComboBox1.SelectedValue) = True Then

                MsgBox("Please Choose an Option From the Drop Down List", vbOKOnly)
                DialogResult = Windows.Forms.DialogResult.Cancel
                Exit Sub
            Else
                ReturnedValue = ComboBox1.SelectedValue.ToString

            End If

        End If

        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub SelectComboForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown

        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If

    End Sub


End Class