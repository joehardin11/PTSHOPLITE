Public Class ScheduleReporter
    Dim dataholder As DataTable

    Public Sub New(dataentry As DataTable)

        dataholder = dataentry
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub ScheduleReporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        DataGridView1.DataSource = dataholder

        Dim datasrc As New DataSet
        datasrc.Tables.Add(dataholder)
        BindingSource1.DataSource = dataholder




    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        PrintDocument1.DefaultPageSettings.Landscape = True
        Dim datamap As New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
        DataGridView1.DrawToBitmap(datamap, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
        e.Graphics.DrawImage(datamap, 0, 0)
    End Sub

    ' Display a print preview.


    Private Sub PageSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PageSetupToolStripMenuItem.Click
        PageSetupDialog1.PageSettings = PrintDocument1.DefaultPageSettings
        PageSetupDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        If PageSetupDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrintDocument1.DefaultPageSettings = PageSetupDialog1.PageSettings
            PrintDocument1.PrinterSettings = PageSetupDialog1.PrinterSettings
        End If
    End Sub

    Private Sub ExportAscsvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportAscsvToolStripMenuItem.Click
        If DataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0 Then
            Try
                DataGridView1.SuspendLayout()
                DataGridView1.RowHeadersVisible = False
                If DataGridView1.SelectedRows.Count = 0 Then DataGridView1.SelectAll()
                Clipboard.SetDataObject(DataGridView1.GetClipboardContent)
                DataGridView1.ClearSelection()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                DataGridView1.RowHeadersVisible = True
                DataGridView1.ResumeLayout()
            End Try
        End If
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        'Get the cell info




    End Sub
End Class