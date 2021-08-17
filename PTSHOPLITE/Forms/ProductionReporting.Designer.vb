<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProductionReporting
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductionReporting))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QtyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartNoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.JobNoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoutingStepDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HoursDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SetupDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ProdDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EntDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Notes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductionReportingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SHOPDBDataSetProdReport = New PTSHOPLITE.SHOPDBDataSetProdReport()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxNotes = New System.Windows.Forms.TextBox()
        Me.Label_InvalidPart = New System.Windows.Forms.Label()
        Me.Label_InvalidJob = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.TextBoxEmployeeNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ComboBoxRoutingStep = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxPartNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxBarcode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxJobNo = New System.Windows.Forms.TextBox()
        Me.TextBoxHours = New System.Windows.Forms.TextBox()
        Me.TextBoxQty = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ProductionReportingTableAdapter = New PTSHOPLITE.SHOPDBDataSetProdReportTableAdapters.ProductionReportingTableAdapter()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProductionReportingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SHOPDBDataSetProdReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Location = New System.Drawing.Point(0, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1011, 553)
        Me.Panel1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ButtonCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ButtonUpdate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label11)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxNotes)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label_InvalidPart)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label_InvalidJob)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label10)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateTimePicker1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxEmployeeNo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label9)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboBoxRoutingStep)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxPartNo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxBarcode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxJobNo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxHours)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxQty)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.Size = New System.Drawing.Size(1011, 553)
        Me.SplitContainer1.SplitterDistance = 400
        Me.SplitContainer1.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn, Me.QtyDataGridViewTextBoxColumn, Me.PartNoDataGridViewTextBoxColumn, Me.JobNoDataGridViewTextBoxColumn, Me.RoutingStepDataGridViewTextBoxColumn, Me.EmployeeDataGridViewTextBoxColumn, Me.HoursDataGridViewTextBoxColumn, Me.SetupDataGridViewCheckBoxColumn, Me.ProdDateDataGridViewTextBoxColumn, Me.EntDate, Me.Notes})
        Me.DataGridView1.DataSource = Me.ProductionReportingBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(400, 553)
        Me.DataGridView1.TabIndex = 0
        Me.DataGridView1.TabStop = False
        '
        'IdDataGridViewTextBoxColumn
        '
        Me.IdDataGridViewTextBoxColumn.DataPropertyName = "Id"
        Me.IdDataGridViewTextBoxColumn.HeaderText = "Id"
        Me.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn"
        Me.IdDataGridViewTextBoxColumn.ReadOnly = True
        Me.IdDataGridViewTextBoxColumn.Visible = False
        Me.IdDataGridViewTextBoxColumn.Width = 25
        '
        'QtyDataGridViewTextBoxColumn
        '
        Me.QtyDataGridViewTextBoxColumn.DataPropertyName = "Qty"
        Me.QtyDataGridViewTextBoxColumn.HeaderText = "Qty"
        Me.QtyDataGridViewTextBoxColumn.Name = "QtyDataGridViewTextBoxColumn"
        Me.QtyDataGridViewTextBoxColumn.ReadOnly = True
        Me.QtyDataGridViewTextBoxColumn.Width = 55
        '
        'PartNoDataGridViewTextBoxColumn
        '
        Me.PartNoDataGridViewTextBoxColumn.DataPropertyName = "PartNo"
        Me.PartNoDataGridViewTextBoxColumn.HeaderText = "PartNo"
        Me.PartNoDataGridViewTextBoxColumn.Name = "PartNoDataGridViewTextBoxColumn"
        Me.PartNoDataGridViewTextBoxColumn.ReadOnly = True
        Me.PartNoDataGridViewTextBoxColumn.Width = 77
        '
        'JobNoDataGridViewTextBoxColumn
        '
        Me.JobNoDataGridViewTextBoxColumn.DataPropertyName = "JobNo"
        Me.JobNoDataGridViewTextBoxColumn.HeaderText = "JobNo"
        Me.JobNoDataGridViewTextBoxColumn.Name = "JobNoDataGridViewTextBoxColumn"
        Me.JobNoDataGridViewTextBoxColumn.ReadOnly = True
        Me.JobNoDataGridViewTextBoxColumn.Width = 74
        '
        'RoutingStepDataGridViewTextBoxColumn
        '
        Me.RoutingStepDataGridViewTextBoxColumn.DataPropertyName = "RoutingStep"
        Me.RoutingStepDataGridViewTextBoxColumn.HeaderText = "RoutingStep"
        Me.RoutingStepDataGridViewTextBoxColumn.Name = "RoutingStepDataGridViewTextBoxColumn"
        Me.RoutingStepDataGridViewTextBoxColumn.ReadOnly = True
        Me.RoutingStepDataGridViewTextBoxColumn.Width = 111
        '
        'EmployeeDataGridViewTextBoxColumn
        '
        Me.EmployeeDataGridViewTextBoxColumn.DataPropertyName = "Employee"
        Me.EmployeeDataGridViewTextBoxColumn.HeaderText = "Employee"
        Me.EmployeeDataGridViewTextBoxColumn.Name = "EmployeeDataGridViewTextBoxColumn"
        Me.EmployeeDataGridViewTextBoxColumn.ReadOnly = True
        Me.EmployeeDataGridViewTextBoxColumn.Width = 95
        '
        'HoursDataGridViewTextBoxColumn
        '
        Me.HoursDataGridViewTextBoxColumn.DataPropertyName = "Hours"
        Me.HoursDataGridViewTextBoxColumn.HeaderText = "Hours"
        Me.HoursDataGridViewTextBoxColumn.Name = "HoursDataGridViewTextBoxColumn"
        Me.HoursDataGridViewTextBoxColumn.ReadOnly = True
        Me.HoursDataGridViewTextBoxColumn.Width = 71
        '
        'SetupDataGridViewCheckBoxColumn
        '
        Me.SetupDataGridViewCheckBoxColumn.DataPropertyName = "Setup"
        Me.SetupDataGridViewCheckBoxColumn.HeaderText = "Setup"
        Me.SetupDataGridViewCheckBoxColumn.Name = "SetupDataGridViewCheckBoxColumn"
        Me.SetupDataGridViewCheckBoxColumn.ReadOnly = True
        Me.SetupDataGridViewCheckBoxColumn.Width = 51
        '
        'ProdDateDataGridViewTextBoxColumn
        '
        Me.ProdDateDataGridViewTextBoxColumn.DataPropertyName = "ProdDate"
        Me.ProdDateDataGridViewTextBoxColumn.HeaderText = "ProdDate"
        Me.ProdDateDataGridViewTextBoxColumn.Name = "ProdDateDataGridViewTextBoxColumn"
        Me.ProdDateDataGridViewTextBoxColumn.ReadOnly = True
        Me.ProdDateDataGridViewTextBoxColumn.Width = 93
        '
        'EntDate
        '
        Me.EntDate.DataPropertyName = "EntDate"
        Me.EntDate.HeaderText = "EntDate"
        Me.EntDate.Name = "EntDate"
        Me.EntDate.ReadOnly = True
        Me.EntDate.Width = 84
        '
        'Notes
        '
        Me.Notes.DataPropertyName = "Notes"
        Me.Notes.HeaderText = "Notes"
        Me.Notes.Name = "Notes"
        Me.Notes.ReadOnly = True
        Me.Notes.Width = 70
        '
        'ProductionReportingBindingSource
        '
        Me.ProductionReportingBindingSource.DataMember = "ProductionReporting"
        Me.ProductionReportingBindingSource.DataSource = Me.SHOPDBDataSetProdReport
        '
        'SHOPDBDataSetProdReport
        '
        Me.SHOPDBDataSetProdReport.DataSetName = "SHOPDBDataSetProdReport"
        Me.SHOPDBDataSetProdReport.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCancel.Location = New System.Drawing.Point(469, 349)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(103, 45)
        Me.ButtonCancel.TabIndex = 25
        Me.ButtonCancel.Text = "CANCEL"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        Me.ButtonCancel.Visible = False
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonUpdate.Location = New System.Drawing.Point(360, 349)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(103, 45)
        Me.ButtonUpdate.TabIndex = 24
        Me.ButtonUpdate.Text = "UPDATE"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        Me.ButtonUpdate.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(29, 416)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 17)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "NOTES:"
        '
        'TextBoxNotes
        '
        Me.TextBoxNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxNotes.Location = New System.Drawing.Point(32, 436)
        Me.TextBoxNotes.Multiline = True
        Me.TextBoxNotes.Name = "TextBoxNotes"
        Me.TextBoxNotes.Size = New System.Drawing.Size(527, 78)
        Me.TextBoxNotes.TabIndex = 22
        '
        'Label_InvalidPart
        '
        Me.Label_InvalidPart.AutoSize = True
        Me.Label_InvalidPart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_InvalidPart.ForeColor = System.Drawing.Color.Red
        Me.Label_InvalidPart.Location = New System.Drawing.Point(261, 260)
        Me.Label_InvalidPart.Name = "Label_InvalidPart"
        Me.Label_InvalidPart.Size = New System.Drawing.Size(81, 13)
        Me.Label_InvalidPart.TabIndex = 21
        Me.Label_InvalidPart.Text = "*Invalid Part-No"
        Me.Label_InvalidPart.Visible = False
        '
        'Label_InvalidJob
        '
        Me.Label_InvalidJob.AutoSize = True
        Me.Label_InvalidJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_InvalidJob.ForeColor = System.Drawing.Color.Red
        Me.Label_InvalidJob.Location = New System.Drawing.Point(90, 260)
        Me.Label_InvalidJob.Name = "Label_InvalidJob"
        Me.Label_InvalidJob.Size = New System.Drawing.Size(79, 13)
        Me.Label_InvalidJob.TabIndex = 20
        Me.Label_InvalidJob.Text = "*Invalid Job-No"
        Me.Label_InvalidJob.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(258, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 20)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "(Optional)"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(32, 349)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(115, 23)
        Me.DateTimePicker1.TabIndex = 18
        Me.DateTimePicker1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(29, 329)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 17)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "DATE PRODUCED:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(360, 349)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 45)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "SUBMIT"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(196, 349)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(77, 21)
        Me.CheckBox1.TabIndex = 15
        Me.CheckBox1.TabStop = False
        Me.CheckBox1.Text = "SETUP:"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBoxEmployeeNo
        '
        Me.TextBoxEmployeeNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxEmployeeNo.Location = New System.Drawing.Point(360, 207)
        Me.TextBoxEmployeeNo.Name = "TextBoxEmployeeNo"
        Me.TextBoxEmployeeNo.Size = New System.Drawing.Size(115, 23)
        Me.TextBoxEmployeeNo.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(357, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 17)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "EMPLOYEE NO:"
        '
        'ComboBoxRoutingStep
        '
        Me.ComboBoxRoutingStep.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxRoutingStep.FormattingEnabled = True
        Me.ComboBoxRoutingStep.Location = New System.Drawing.Point(360, 276)
        Me.ComboBoxRoutingStep.Name = "ComboBoxRoutingStep"
        Me.ComboBoxRoutingStep.Size = New System.Drawing.Size(199, 24)
        Me.ComboBoxRoutingStep.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(357, 256)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 17)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "ROUTING STEP"
        '
        'TextBoxPartNo
        '
        Me.TextBoxPartNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPartNo.Location = New System.Drawing.Point(196, 276)
        Me.TextBoxPartNo.Name = "TextBoxPartNo"
        Me.TextBoxPartNo.Size = New System.Drawing.Size(115, 23)
        Me.TextBoxPartNo.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(193, 256)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 17)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "PART NO:"
        '
        'TextBoxBarcode
        '
        Me.TextBoxBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxBarcode.Location = New System.Drawing.Point(32, 56)
        Me.TextBoxBarcode.Name = "TextBoxBarcode"
        Me.TextBoxBarcode.Size = New System.Drawing.Size(351, 26)
        Me.TextBoxBarcode.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(28, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(261, 20)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "(Job-No or Routing Step for Autofill)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(25, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(227, 37)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Scan Barcode:"
        '
        'TextBoxJobNo
        '
        Me.TextBoxJobNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxJobNo.Location = New System.Drawing.Point(32, 276)
        Me.TextBoxJobNo.Name = "TextBoxJobNo"
        Me.TextBoxJobNo.Size = New System.Drawing.Size(115, 23)
        Me.TextBoxJobNo.TabIndex = 4
        '
        'TextBoxHours
        '
        Me.TextBoxHours.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxHours.Location = New System.Drawing.Point(196, 207)
        Me.TextBoxHours.Name = "TextBoxHours"
        Me.TextBoxHours.Size = New System.Drawing.Size(115, 23)
        Me.TextBoxHours.TabIndex = 2
        '
        'TextBoxQty
        '
        Me.TextBoxQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxQty.Location = New System.Drawing.Point(32, 207)
        Me.TextBoxQty.Name = "TextBoxQty"
        Me.TextBoxQty.Size = New System.Drawing.Size(115, 23)
        Me.TextBoxQty.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(29, 256)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "JOB-NO:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(193, 187)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "HOURS:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(29, 187)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "QTY:"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripSeparator1, Me.ToolStripButton2, Me.ToolStripSeparator2, Me.ToolStripButton3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1011, 27)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(102, 24)
        Me.ToolStripButton1.Text = "Force Refresh"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(85, 24)
        Me.ToolStripButton2.Text = "Clear Form"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(76, 24)
        Me.ToolStripButton3.Text = "Edit Entry"
        '
        'ProductionReportingTableAdapter
        '
        Me.ProductionReportingTableAdapter.ClearBeforeFill = True
        '
        'ProductionReporting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 582)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.MinimumSize = New System.Drawing.Size(1027, 621)
        Me.Name = "ProductionReporting"
        Me.Text = "Production Reporting"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProductionReportingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SHOPDBDataSetProdReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxJobNo As TextBox
    Friend WithEvents TextBoxHours As TextBox
    Friend WithEvents TextBoxQty As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxBarcode As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBoxRoutingStep As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxPartNo As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents TextBoxEmployeeNo As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label_InvalidPart As Label
    Friend WithEvents Label_InvalidJob As Label
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents SHOPDBDataSetProdReport As SHOPDBDataSetProdReport
    Friend WithEvents ProductionReportingBindingSource As BindingSource
    Friend WithEvents ProductionReportingTableAdapter As SHOPDBDataSetProdReportTableAdapters.ProductionReportingTableAdapter
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBoxNotes As TextBox
    Friend WithEvents IdDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents QtyDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PartNoDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents JobNoDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents RoutingStepDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents HoursDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SetupDataGridViewCheckBoxColumn As DataGridViewCheckBoxColumn
    Friend WithEvents ProdDateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EntDate As DataGridViewTextBoxColumn
    Friend WithEvents Notes As DataGridViewTextBoxColumn
    Friend WithEvents ButtonUpdate As Button
    Friend WithEvents ButtonCancel As Button
End Class
