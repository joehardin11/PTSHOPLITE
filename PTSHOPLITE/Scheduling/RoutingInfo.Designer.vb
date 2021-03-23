<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RoutingInfo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RoutingInfo))
        Me.jobno_textbox = New System.Windows.Forms.TextBox()
        Me.Orderno = New System.Windows.Forms.Label()
        Me.PartNo_label = New System.Windows.Forms.Label()
        Me.partno_textbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Operationcode = New System.Windows.Forms.Label()
        Me.opercode_combo = New System.Windows.Forms.ComboBox()
        Me.workcenter_combo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cycletime_number = New System.Windows.Forms.NumericUpDown()
        Me.cycleunit_combo = New System.Windows.Forms.ComboBox()
        Me.setupunit_combo = New System.Windows.Forms.ComboBox()
        Me.setuptime_number = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.orderqty_textbox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.partdescription_textbox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.opdescription_textbox = New System.Windows.Forms.TextBox()
        Me.hrsleft_textbox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.stepno_textbox = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.QtyMake_textbox = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.altpartno_textbox = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.material_checkbox = New System.Windows.Forms.CheckBox()
        Me.program_checkbox = New System.Windows.Forms.CheckBox()
        Me.tools_checkbox = New System.Windows.Forms.CheckBox()
        Me.completed_checkbox = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Revision_textbox = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Qty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DueDateHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.datecomplete = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Comments = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label13 = New System.Windows.Forms.Label()
        Me.shift_checkbox = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Mat_listview = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Material = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        CType(Me.cycletime_number, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.setuptime_number, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'jobno_textbox
        '
        Me.jobno_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.jobno_textbox.Location = New System.Drawing.Point(14, 38)
        Me.jobno_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.jobno_textbox.Name = "jobno_textbox"
        Me.jobno_textbox.ReadOnly = True
        Me.jobno_textbox.Size = New System.Drawing.Size(137, 25)
        Me.jobno_textbox.TabIndex = 0
        '
        'Orderno
        '
        Me.Orderno.AutoSize = True
        Me.Orderno.Location = New System.Drawing.Point(12, 17)
        Me.Orderno.Name = "Orderno"
        Me.Orderno.Size = New System.Drawing.Size(51, 17)
        Me.Orderno.TabIndex = 1
        Me.Orderno.Text = "Job No"
        '
        'PartNo_label
        '
        Me.PartNo_label.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PartNo_label.AutoSize = True
        Me.PartNo_label.Location = New System.Drawing.Point(181, 305)
        Me.PartNo_label.Name = "PartNo_label"
        Me.PartNo_label.Size = New System.Drawing.Size(56, 17)
        Me.PartNo_label.TabIndex = 3
        Me.PartNo_label.Text = "Part No."
        '
        'partno_textbox
        '
        Me.partno_textbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.partno_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.partno_textbox.Location = New System.Drawing.Point(182, 326)
        Me.partno_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.partno_textbox.Name = "partno_textbox"
        Me.partno_textbox.ReadOnly = True
        Me.partno_textbox.Size = New System.Drawing.Size(202, 25)
        Me.partno_textbox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 407)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Cycle Time"
        '
        'Operationcode
        '
        Me.Operationcode.AutoSize = True
        Me.Operationcode.Location = New System.Drawing.Point(14, 239)
        Me.Operationcode.Name = "Operationcode"
        Me.Operationcode.Size = New System.Drawing.Size(102, 17)
        Me.Operationcode.TabIndex = 7
        Me.Operationcode.Text = "Operation Code"
        '
        'opercode_combo
        '
        Me.opercode_combo.FormattingEnabled = True
        Me.opercode_combo.Location = New System.Drawing.Point(13, 260)
        Me.opercode_combo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.opercode_combo.Name = "opercode_combo"
        Me.opercode_combo.Size = New System.Drawing.Size(140, 25)
        Me.opercode_combo.TabIndex = 8
        '
        'workcenter_combo
        '
        Me.workcenter_combo.FormattingEnabled = True
        Me.workcenter_combo.Location = New System.Drawing.Point(13, 314)
        Me.workcenter_combo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.workcenter_combo.Name = "workcenter_combo"
        Me.workcenter_combo.Size = New System.Drawing.Size(140, 25)
        Me.workcenter_combo.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 293)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 17)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "WorkCenter"
        '
        'cycletime_number
        '
        Me.cycletime_number.Location = New System.Drawing.Point(13, 428)
        Me.cycletime_number.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cycletime_number.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.cycletime_number.Name = "cycletime_number"
        Me.cycletime_number.Size = New System.Drawing.Size(96, 25)
        Me.cycletime_number.TabIndex = 11
        '
        'cycleunit_combo
        '
        Me.cycleunit_combo.FormattingEnabled = True
        Me.cycleunit_combo.Location = New System.Drawing.Point(115, 428)
        Me.cycleunit_combo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cycleunit_combo.Name = "cycleunit_combo"
        Me.cycleunit_combo.Size = New System.Drawing.Size(38, 25)
        Me.cycleunit_combo.TabIndex = 12
        '
        'setupunit_combo
        '
        Me.setupunit_combo.FormattingEnabled = True
        Me.setupunit_combo.Location = New System.Drawing.Point(117, 367)
        Me.setupunit_combo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.setupunit_combo.Name = "setupunit_combo"
        Me.setupunit_combo.Size = New System.Drawing.Size(38, 25)
        Me.setupunit_combo.TabIndex = 15
        '
        'setuptime_number
        '
        Me.setuptime_number.Location = New System.Drawing.Point(14, 366)
        Me.setuptime_number.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.setuptime_number.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.setuptime_number.Name = "setuptime_number"
        Me.setuptime_number.Size = New System.Drawing.Size(96, 25)
        Me.setuptime_number.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 345)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 17)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Setup Time"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 17)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Order Qty."
        '
        'orderqty_textbox
        '
        Me.orderqty_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.orderqty_textbox.Location = New System.Drawing.Point(16, 149)
        Me.orderqty_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.orderqty_textbox.Name = "orderqty_textbox"
        Me.orderqty_textbox.ReadOnly = True
        Me.orderqty_textbox.Size = New System.Drawing.Size(137, 25)
        Me.orderqty_textbox.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(181, 400)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 17)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Part Description"
        '
        'partdescription_textbox
        '
        Me.partdescription_textbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.partdescription_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.partdescription_textbox.Location = New System.Drawing.Point(185, 421)
        Me.partdescription_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.partdescription_textbox.Multiline = True
        Me.partdescription_textbox.Name = "partdescription_textbox"
        Me.partdescription_textbox.ReadOnly = True
        Me.partdescription_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.partdescription_textbox.Size = New System.Drawing.Size(333, 80)
        Me.partdescription_textbox.TabIndex = 18
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(182, 505)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 17)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "OP Description"
        '
        'opdescription_textbox
        '
        Me.opdescription_textbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.opdescription_textbox.Location = New System.Drawing.Point(185, 524)
        Me.opdescription_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.opdescription_textbox.Multiline = True
        Me.opdescription_textbox.Name = "opdescription_textbox"
        Me.opdescription_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.opdescription_textbox.Size = New System.Drawing.Size(333, 79)
        Me.opdescription_textbox.TabIndex = 20
        '
        'hrsleft_textbox
        '
        Me.hrsleft_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.hrsleft_textbox.Location = New System.Drawing.Point(15, 536)
        Me.hrsleft_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hrsleft_textbox.Name = "hrsleft_textbox"
        Me.hrsleft_textbox.ReadOnly = True
        Me.hrsleft_textbox.Size = New System.Drawing.Size(140, 25)
        Me.hrsleft_textbox.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 515)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 17)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "Total Hrs Left"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 71)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 17)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Step No"
        '
        'stepno_textbox
        '
        Me.stepno_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.stepno_textbox.Location = New System.Drawing.Point(15, 92)
        Me.stepno_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.stepno_textbox.Name = "stepno_textbox"
        Me.stepno_textbox.ReadOnly = True
        Me.stepno_textbox.Size = New System.Drawing.Size(137, 25)
        Me.stepno_textbox.TabIndex = 24
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(402, 624)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(129, 39)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(8, 612)
        Me.Button2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(101, 39)
        Me.Button2.TabIndex = 27
        Me.Button2.Text = "Save Changes"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(105, 17)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Qty Left to Make"
        '
        'QtyMake_textbox
        '
        Me.QtyMake_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.QtyMake_textbox.Location = New System.Drawing.Point(16, 208)
        Me.QtyMake_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.QtyMake_textbox.Name = "QtyMake_textbox"
        Me.QtyMake_textbox.ReadOnly = True
        Me.QtyMake_textbox.Size = New System.Drawing.Size(137, 25)
        Me.QtyMake_textbox.TabIndex = 28
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(178, 352)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(116, 17)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "Customer Part No."
        '
        'altpartno_textbox
        '
        Me.altpartno_textbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.altpartno_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.altpartno_textbox.Location = New System.Drawing.Point(181, 371)
        Me.altpartno_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.altpartno_textbox.Name = "altpartno_textbox"
        Me.altpartno_textbox.ReadOnly = True
        Me.altpartno_textbox.Size = New System.Drawing.Size(202, 25)
        Me.altpartno_textbox.TabIndex = 30
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 461)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 17)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "DueDate"
        Me.Label11.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(9, 569)
        Me.Button3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(101, 39)
        Me.Button3.TabIndex = 34
        Me.Button3.Text = "Save to Part"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'material_checkbox
        '
        Me.material_checkbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.material_checkbox.AutoSize = True
        Me.material_checkbox.Location = New System.Drawing.Point(185, 255)
        Me.material_checkbox.Name = "material_checkbox"
        Me.material_checkbox.Size = New System.Drawing.Size(115, 21)
        Me.material_checkbox.TabIndex = 35
        Me.material_checkbox.Text = "Material Ready"
        Me.material_checkbox.UseVisualStyleBackColor = True
        '
        'program_checkbox
        '
        Me.program_checkbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.program_checkbox.AutoSize = True
        Me.program_checkbox.Location = New System.Drawing.Point(185, 282)
        Me.program_checkbox.Name = "program_checkbox"
        Me.program_checkbox.Size = New System.Drawing.Size(118, 21)
        Me.program_checkbox.TabIndex = 36
        Me.program_checkbox.Text = "Program Ready"
        Me.program_checkbox.UseVisualStyleBackColor = True
        '
        'tools_checkbox
        '
        Me.tools_checkbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tools_checkbox.AutoSize = True
        Me.tools_checkbox.Location = New System.Drawing.Point(321, 255)
        Me.tools_checkbox.Name = "tools_checkbox"
        Me.tools_checkbox.Size = New System.Drawing.Size(98, 21)
        Me.tools_checkbox.TabIndex = 37
        Me.tools_checkbox.Text = "Tools Ready"
        Me.tools_checkbox.UseVisualStyleBackColor = True
        '
        'completed_checkbox
        '
        Me.completed_checkbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.completed_checkbox.AutoSize = True
        Me.completed_checkbox.Location = New System.Drawing.Point(321, 282)
        Me.completed_checkbox.Name = "completed_checkbox"
        Me.completed_checkbox.Size = New System.Drawing.Size(91, 21)
        Me.completed_checkbox.TabIndex = 38
        Me.completed_checkbox.Text = "Completed"
        Me.completed_checkbox.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(385, 350)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 17)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "Revision"
        '
        'Revision_textbox
        '
        Me.Revision_textbox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Revision_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Revision_textbox.Location = New System.Drawing.Point(388, 371)
        Me.Revision_textbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Revision_textbox.Name = "Revision_textbox"
        Me.Revision_textbox.ReadOnly = True
        Me.Revision_textbox.Size = New System.Drawing.Size(130, 25)
        Me.Revision_textbox.TabIndex = 39
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(16, 480)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(139, 25)
        Me.DateTimePicker1.TabIndex = 41
        Me.DateTimePicker1.Visible = False
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Qty, Me.DueDateHeader, Me.datecomplete, Me.Comments})
        Me.ListView1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(174, 28)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(360, 89)
        Me.ListView1.TabIndex = 42
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Qty
        '
        Me.Qty.Text = "Qty"
        Me.Qty.Width = 54
        '
        'DueDateHeader
        '
        Me.DueDateHeader.Text = "Due Date"
        Me.DueDateHeader.Width = 71
        '
        'datecomplete
        '
        Me.datecomplete.Text = "Completed"
        Me.datecomplete.Width = 81
        '
        'Comments
        '
        Me.Comments.Text = "Comments"
        Me.Comments.Width = 116
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(173, 8)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 17)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Job No"
        '
        'shift_checkbox
        '
        Me.shift_checkbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.shift_checkbox.AutoSize = True
        Me.shift_checkbox.Location = New System.Drawing.Point(440, 279)
        Me.shift_checkbox.Name = "shift_checkbox"
        Me.shift_checkbox.Size = New System.Drawing.Size(63, 21)
        Me.shift_checkbox.TabIndex = 44
        Me.shift_checkbox.Text = "1 Shift"
        Me.shift_checkbox.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(173, 128)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 17)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "Material"
        '
        'Mat_listview
        '
        Me.Mat_listview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Mat_listview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Material, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.Mat_listview.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Mat_listview.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Mat_listview.FullRowSelect = True
        Me.Mat_listview.HideSelection = False
        Me.Mat_listview.Location = New System.Drawing.Point(174, 144)
        Me.Mat_listview.Name = "Mat_listview"
        Me.Mat_listview.Size = New System.Drawing.Size(360, 101)
        Me.Mat_listview.TabIndex = 46
        Me.Mat_listview.UseCompatibleStateImageBehavior = False
        Me.Mat_listview.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Qty"
        Me.ColumnHeader1.Width = 48
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.DisplayIndex = 3
        Me.ColumnHeader2.Text = "Due Date"
        Me.ColumnHeader2.Width = 77
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.DisplayIndex = 2
        Me.ColumnHeader3.Text = "Received"
        Me.ColumnHeader3.Width = 81
        '
        'Material
        '
        Me.Material.Text = "Material"
        Me.Material.Width = 116
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'RoutingInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 676)
        Me.Controls.Add(Me.Mat_listview)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.shift_checkbox)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Revision_textbox)
        Me.Controls.Add(Me.completed_checkbox)
        Me.Controls.Add(Me.tools_checkbox)
        Me.Controls.Add(Me.program_checkbox)
        Me.Controls.Add(Me.material_checkbox)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.altpartno_textbox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.QtyMake_textbox)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.stepno_textbox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.hrsleft_textbox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.opdescription_textbox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.partdescription_textbox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.orderqty_textbox)
        Me.Controls.Add(Me.setupunit_combo)
        Me.Controls.Add(Me.setuptime_number)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cycleunit_combo)
        Me.Controls.Add(Me.cycletime_number)
        Me.Controls.Add(Me.workcenter_combo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.opercode_combo)
        Me.Controls.Add(Me.Operationcode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PartNo_label)
        Me.Controls.Add(Me.partno_textbox)
        Me.Controls.Add(Me.Orderno)
        Me.Controls.Add(Me.jobno_textbox)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RoutingInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Routing Info"
        CType(Me.cycletime_number, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.setuptime_number, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents jobno_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Orderno As System.Windows.Forms.Label
    Friend WithEvents PartNo_label As System.Windows.Forms.Label
    Friend WithEvents partno_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Operationcode As System.Windows.Forms.Label
    Friend WithEvents opercode_combo As System.Windows.Forms.ComboBox
    Friend WithEvents workcenter_combo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cycletime_number As System.Windows.Forms.NumericUpDown
    Friend WithEvents cycleunit_combo As System.Windows.Forms.ComboBox
    Friend WithEvents setupunit_combo As System.Windows.Forms.ComboBox
    Friend WithEvents setuptime_number As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents orderqty_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents partdescription_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents opdescription_textbox As System.Windows.Forms.TextBox
    Friend WithEvents hrsleft_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents stepno_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents QtyMake_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents altpartno_textbox As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents material_checkbox As System.Windows.Forms.CheckBox
    Friend WithEvents program_checkbox As System.Windows.Forms.CheckBox
    Friend WithEvents tools_checkbox As System.Windows.Forms.CheckBox
    Friend WithEvents completed_checkbox As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Revision_textbox As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ListView1 As ListView
    Friend WithEvents DueDateHeader As ColumnHeader
    Friend WithEvents Label13 As Label
    Friend WithEvents Qty As ColumnHeader
    Friend WithEvents Comments As ColumnHeader
    Friend WithEvents datecomplete As ColumnHeader
    Friend WithEvents shift_checkbox As CheckBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Mat_listview As ListView
    Friend WithEvents Material As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class
