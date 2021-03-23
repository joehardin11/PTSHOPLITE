<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MachineJobViewer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MachinesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Machineheader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WO1header = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WO1due = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WO2header = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WO2Due = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Employee1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Employee2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Machines_combobox = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpdateStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunningToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AwaitingQCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NeedMaterialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NeedTechSupportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NeedToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MaintenanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WorkOrderInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CompletedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WorkOrder2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewInfoToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CompletedToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MenuStrip1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SplitContainer1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(956, 572)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.MenuStrip1, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.ToolStripComboBox1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(3, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(956, 20)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'DToolStripMenuItem
        '
        Me.DToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MachinesToolStripMenuItem})
        Me.DToolStripMenuItem.Name = "DToolStripMenuItem"
        Me.DToolStripMenuItem.Size = New System.Drawing.Size(37, 18)
        Me.DToolStripMenuItem.Text = "File"
        '
        'MachinesToolStripMenuItem
        '
        Me.MachinesToolStripMenuItem.Name = "MachinesToolStripMenuItem"
        Me.MachinesToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.MachinesToolStripMenuItem.Text = "Machines"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(58, 18)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Items.AddRange(New Object() {"Column View", "Status Box View"})
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(121, 18)
        '
        'SplitContainer1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.SplitContainer1, 2)
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 22)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Machines_combobox)
        Me.TableLayoutPanel1.SetRowSpan(Me.SplitContainer1, 2)
        Me.SplitContainer1.Size = New System.Drawing.Size(952, 548)
        Me.SplitContainer1.SplitterDistance = 461
        Me.SplitContainer1.TabIndex = 1
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(952, 461)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Machineheader, Me.WO1header, Me.WO1due, Me.WO2header, Me.WO2Due, Me.Employee1, Me.Employee2})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(952, 461)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Machineheader
        '
        Me.Machineheader.Text = "Machine"
        Me.Machineheader.Width = 120
        '
        'WO1header
        '
        Me.WO1header.Text = "WO 1"
        Me.WO1header.Width = 210
        '
        'WO1due
        '
        Me.WO1due.Text = "Due Date"
        '
        'WO2header
        '
        Me.WO2header.Text = "WO 2"
        Me.WO2header.Width = 210
        '
        'WO2Due
        '
        Me.WO2Due.Text = "Due Date"
        '
        'Employee1
        '
        Me.Employee1.Text = "Shift 1"
        Me.Employee1.Width = 84
        '
        'Employee2
        '
        Me.Employee2.Text = "Shift 2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Department"
        '
        'Machines_combobox
        '
        Me.Machines_combobox.FormattingEnabled = True
        Me.Machines_combobox.Location = New System.Drawing.Point(3, 18)
        Me.Machines_combobox.Name = "Machines_combobox"
        Me.Machines_combobox.Size = New System.Drawing.Size(121, 21)
        Me.Machines_combobox.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateStatusToolStripMenuItem, Me.WorkOrderInfoToolStripMenuItem, Me.WorkOrder2ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(148, 70)
        '
        'UpdateStatusToolStripMenuItem
        '
        Me.UpdateStatusToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RunningToolStripMenuItem, Me.SetupToolStripMenuItem, Me.AwaitingQCToolStripMenuItem, Me.NeedMaterialToolStripMenuItem, Me.NeedTechSupportToolStripMenuItem, Me.NeedToolToolStripMenuItem, Me.MaintenanceToolStripMenuItem, Me.OffToolStripMenuItem})
        Me.UpdateStatusToolStripMenuItem.Name = "UpdateStatusToolStripMenuItem"
        Me.UpdateStatusToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.UpdateStatusToolStripMenuItem.Text = "Update Status"
        '
        'RunningToolStripMenuItem
        '
        Me.RunningToolStripMenuItem.BackColor = System.Drawing.Color.Green
        Me.RunningToolStripMenuItem.Name = "RunningToolStripMenuItem"
        Me.RunningToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.RunningToolStripMenuItem.Text = "Running"
        '
        'SetupToolStripMenuItem
        '
        Me.SetupToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.SetupToolStripMenuItem.Name = "SetupToolStripMenuItem"
        Me.SetupToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.SetupToolStripMenuItem.Text = "Setup"
        '
        'AwaitingQCToolStripMenuItem
        '
        Me.AwaitingQCToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.AwaitingQCToolStripMenuItem.Name = "AwaitingQCToolStripMenuItem"
        Me.AwaitingQCToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.AwaitingQCToolStripMenuItem.Text = "Awaiting QC"
        '
        'NeedMaterialToolStripMenuItem
        '
        Me.NeedMaterialToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.NeedMaterialToolStripMenuItem.Name = "NeedMaterialToolStripMenuItem"
        Me.NeedMaterialToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.NeedMaterialToolStripMenuItem.Text = "Need Material"
        '
        'NeedTechSupportToolStripMenuItem
        '
        Me.NeedTechSupportToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NeedTechSupportToolStripMenuItem.Name = "NeedTechSupportToolStripMenuItem"
        Me.NeedTechSupportToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.NeedTechSupportToolStripMenuItem.Text = "Need Tech Support"
        '
        'NeedToolToolStripMenuItem
        '
        Me.NeedToolToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NeedToolToolStripMenuItem.Name = "NeedToolToolStripMenuItem"
        Me.NeedToolToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.NeedToolToolStripMenuItem.Text = "Need Tool"
        '
        'MaintenanceToolStripMenuItem
        '
        Me.MaintenanceToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.MaintenanceToolStripMenuItem.Name = "MaintenanceToolStripMenuItem"
        Me.MaintenanceToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.MaintenanceToolStripMenuItem.Text = "Maintenance"
        '
        'OffToolStripMenuItem
        '
        Me.OffToolStripMenuItem.BackColor = System.Drawing.Color.Red
        Me.OffToolStripMenuItem.Name = "OffToolStripMenuItem"
        Me.OffToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.OffToolStripMenuItem.Text = "Off"
        '
        'WorkOrderInfoToolStripMenuItem
        '
        Me.WorkOrderInfoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewInfoToolStripMenuItem, Me.CompletedToolStripMenuItem})
        Me.WorkOrderInfoToolStripMenuItem.Name = "WorkOrderInfoToolStripMenuItem"
        Me.WorkOrderInfoToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.WorkOrderInfoToolStripMenuItem.Text = "Work Order 1"
        '
        'ViewInfoToolStripMenuItem
        '
        Me.ViewInfoToolStripMenuItem.Name = "ViewInfoToolStripMenuItem"
        Me.ViewInfoToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.ViewInfoToolStripMenuItem.Text = "View Info"
        '
        'CompletedToolStripMenuItem
        '
        Me.CompletedToolStripMenuItem.Name = "CompletedToolStripMenuItem"
        Me.CompletedToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.CompletedToolStripMenuItem.Text = "Completed"
        '
        'WorkOrder2ToolStripMenuItem
        '
        Me.WorkOrder2ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewInfoToolStripMenuItem1, Me.CompletedToolStripMenuItem1})
        Me.WorkOrder2ToolStripMenuItem.Name = "WorkOrder2ToolStripMenuItem"
        Me.WorkOrder2ToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.WorkOrder2ToolStripMenuItem.Text = "Work Order 2"
        '
        'ViewInfoToolStripMenuItem1
        '
        Me.ViewInfoToolStripMenuItem1.Name = "ViewInfoToolStripMenuItem1"
        Me.ViewInfoToolStripMenuItem1.Size = New System.Drawing.Size(133, 22)
        Me.ViewInfoToolStripMenuItem1.Text = "View Info"
        '
        'CompletedToolStripMenuItem1
        '
        Me.CompletedToolStripMenuItem1.Name = "CompletedToolStripMenuItem1"
        Me.CompletedToolStripMenuItem1.Size = New System.Drawing.Size(133, 22)
        Me.CompletedToolStripMenuItem1.Text = "Completed"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(130, 3)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(819, 77)
        Me.TextBox1.TabIndex = 4
        '
        'MachineJobViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MachineJobViewer"
        Me.Size = New System.Drawing.Size(956, 572)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MachinesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Machineheader As ColumnHeader
    Friend WithEvents WO1header As ColumnHeader
    Friend WithEvents WO1due As ColumnHeader
    Friend WithEvents WO2header As ColumnHeader
    Friend WithEvents WO2Due As ColumnHeader
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Machines_combobox As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Employee1 As ColumnHeader
    Friend WithEvents Employee2 As ColumnHeader
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents UpdateStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunningToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AwaitingQCToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NeedMaterialToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NeedTechSupportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NeedToolToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MaintenanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WorkOrderInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CompletedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WorkOrder2ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewInfoToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CompletedToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents TextBox1 As TextBox
End Class
