<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class machineStatus
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.WO1_label = New System.Windows.Forms.Label()
        Me.WO2_label = New System.Windows.Forms.Label()
        Me.wo1descrip_label = New System.Windows.Forms.Label()
        Me.wo2descrip_label = New System.Windows.Forms.Label()
        Me.duedate1_label = New System.Windows.Forms.Label()
        Me.duedate2_label = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Name_Label = New System.Windows.Forms.Label()
        Me.status_label = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'WO1_label
        '
        Me.WO1_label.AutoSize = True
        Me.WO1_label.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WO1_label.Location = New System.Drawing.Point(3, 0)
        Me.WO1_label.Name = "WO1_label"
        Me.WO1_label.Size = New System.Drawing.Size(35, 20)
        Me.WO1_label.TabIndex = 5
        Me.WO1_label.Text = "WO"
        '
        'WO2_label
        '
        Me.WO2_label.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.WO2_label.AutoSize = True
        Me.WO2_label.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WO2_label.Location = New System.Drawing.Point(2, -2)
        Me.WO2_label.Name = "WO2_label"
        Me.WO2_label.Size = New System.Drawing.Size(35, 20)
        Me.WO2_label.TabIndex = 6
        Me.WO2_label.Text = "WO"
        '
        'wo1descrip_label
        '
        Me.wo1descrip_label.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wo1descrip_label.Location = New System.Drawing.Point(4, 30)
        Me.wo1descrip_label.Name = "wo1descrip_label"
        Me.wo1descrip_label.Size = New System.Drawing.Size(217, 44)
        Me.wo1descrip_label.TabIndex = 8
        Me.wo1descrip_label.Text = "Description"
        '
        'wo2descrip_label
        '
        Me.wo2descrip_label.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wo2descrip_label.Location = New System.Drawing.Point(0, 29)
        Me.wo2descrip_label.Name = "wo2descrip_label"
        Me.wo2descrip_label.Size = New System.Drawing.Size(231, 35)
        Me.wo2descrip_label.TabIndex = 9
        Me.wo2descrip_label.Text = "Description"
        '
        'duedate1_label
        '
        Me.duedate1_label.AutoSize = True
        Me.duedate1_label.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.duedate1_label.Location = New System.Drawing.Point(152, 15)
        Me.duedate1_label.Name = "duedate1_label"
        Me.duedate1_label.Size = New System.Drawing.Size(36, 15)
        Me.duedate1_label.TabIndex = 10
        Me.duedate1_label.Text = "Due: "
        '
        'duedate2_label
        '
        Me.duedate2_label.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.duedate2_label.AutoSize = True
        Me.duedate2_label.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.duedate2_label.Location = New System.Drawing.Point(152, 14)
        Me.duedate2_label.Name = "duedate2_label"
        Me.duedate2_label.Size = New System.Drawing.Size(36, 15)
        Me.duedate2_label.TabIndex = 11
        Me.duedate2_label.Text = "Due: "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(152, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 15)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Due: "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(152, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 15)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Due: "
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.duedate1_label)
        Me.Panel1.Controls.Add(Me.WO1_label)
        Me.Panel1.Controls.Add(Me.wo1descrip_label)
        Me.Panel1.Location = New System.Drawing.Point(6, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(236, 76)
        Me.Panel1.TabIndex = 14
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.wo2descrip_label)
        Me.Panel2.Controls.Add(Me.WO2_label)
        Me.Panel2.Controls.Add(Me.duedate2_label)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Location = New System.Drawing.Point(6, 83)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(236, 69)
        Me.Panel2.TabIndex = 15
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.status_label)
        Me.Panel3.Controls.Add(Me.Name_Label)
        Me.Panel3.Controls.Add(Me.ProgressBar1)
        Me.Panel3.Location = New System.Drawing.Point(244, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(215, 148)
        Me.Panel3.TabIndex = 16
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(3, 29)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 23)
        Me.ProgressBar1.TabIndex = 0
        '
        'Name_Label
        '
        Me.Name_Label.AutoSize = True
        Me.Name_Label.Location = New System.Drawing.Point(3, 6)
        Me.Name_Label.Name = "Name_Label"
        Me.Name_Label.Size = New System.Drawing.Size(40, 13)
        Me.Name_Label.TabIndex = 1
        Me.Name_Label.Text = "Label1"
        '
        'status_label
        '
        Me.status_label.AutoSize = True
        Me.status_label.Location = New System.Drawing.Point(34, 94)
        Me.status_label.Name = "status_label"
        Me.status_label.Size = New System.Drawing.Size(40, 13)
        Me.status_label.TabIndex = 2
        Me.status_label.Text = "Label1"
        '
        'machineStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "machineStatus"
        Me.Size = New System.Drawing.Size(462, 155)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WO1_label As Label
    Friend WithEvents WO2_label As Label
    Friend WithEvents wo1descrip_label As Label
    Friend WithEvents wo2descrip_label As Label
    Friend WithEvents duedate1_label As Label
    Friend WithEvents duedate2_label As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Name_Label As Label
    Friend WithEvents status_label As Label
End Class
