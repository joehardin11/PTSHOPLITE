<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobInfoForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobInfoForm))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PartSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.JobNoLabel = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.JobLoading_Progressbar = New System.Windows.Forms.ToolStripProgressBar()
        Me.ReleaseTabControl1 = New System.Windows.Forms.TabControl()
        Me.JobInfoPage = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.makeqty_textbox = New System.Windows.Forms.TextBox()
        Me.orderqty_textbox = New System.Windows.Forms.TextBox()
        Me.dwgno_textbox = New System.Windows.Forms.TextBox()
        Me.Jobnote_textbox = New System.Windows.Forms.TextBox()
        Me.altpartNo_textbox = New System.Windows.Forms.TextBox()
        Me.Revision_textbox = New System.Windows.Forms.TextBox()
        Me.partno_textbox = New System.Windows.Forms.TextBox()
        Me.partdescription_textbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PartNo_label = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ReleasesPage = New System.Windows.Forms.TabPage()
        Me.Release_treeview = New System.Windows.Forms.TreeView()
        Me.OrderDocTab = New System.Windows.Forms.TabPage()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.OrderDocs = New System.Windows.Forms.DataGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RoutingDataGridView = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AxAcroPDF1 = New AxAcroPDFLib.AxAcroPDF()
        Me.ToolStrip1.SuspendLayout()
        Me.ReleaseTabControl1.SuspendLayout()
        Me.JobInfoPage.SuspendLayout()
        Me.ReleasesPage.SuspendLayout()
        Me.OrderDocTab.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        CType(Me.OrderDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RoutingDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxAcroPDF1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripSeparator1, Me.JobNoLabel, Me.ToolStripComboBox1, Me.ToolStripLabel1, Me.JobLoading_Progressbar})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1381, 26)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HomeToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.BackToolStripMenuItem, Me.PartSearchToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(58, 23)
        Me.ToolStripDropDownButton1.Text = "Menu"
        '
        'HomeToolStripMenuItem
        '
        Me.HomeToolStripMenuItem.Name = "HomeToolStripMenuItem"
        Me.HomeToolStripMenuItem.Size = New System.Drawing.Size(190, 24)
        Me.HomeToolStripMenuItem.Text = "Home (Start Over)"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(190, 24)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'BackToolStripMenuItem
        '
        Me.BackToolStripMenuItem.Name = "BackToolStripMenuItem"
        Me.BackToolStripMenuItem.Size = New System.Drawing.Size(190, 24)
        Me.BackToolStripMenuItem.Text = "Back"
        '
        'PartSearchToolStripMenuItem
        '
        Me.PartSearchToolStripMenuItem.Name = "PartSearchToolStripMenuItem"
        Me.PartSearchToolStripMenuItem.Size = New System.Drawing.Size(190, 24)
        Me.PartSearchToolStripMenuItem.Text = "Part Search"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 26)
        '
        'JobNoLabel
        '
        Me.JobNoLabel.Name = "JobNoLabel"
        Me.JobNoLabel.Size = New System.Drawing.Size(59, 23)
        Me.JobNoLabel.Text = "Job No: "
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(160, 26)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(45, 23)
        Me.ToolStripLabel1.Text = "Print: "
        '
        'JobLoading_Progressbar
        '
        Me.JobLoading_Progressbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.JobLoading_Progressbar.Name = "JobLoading_Progressbar"
        Me.JobLoading_Progressbar.Size = New System.Drawing.Size(100, 25)
        Me.JobLoading_Progressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.JobLoading_Progressbar.Visible = False
        '
        'ReleaseTabControl1
        '
        Me.ReleaseTabControl1.Controls.Add(Me.JobInfoPage)
        Me.ReleaseTabControl1.Controls.Add(Me.ReleasesPage)
        Me.ReleaseTabControl1.Controls.Add(Me.OrderDocTab)
        Me.ReleaseTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReleaseTabControl1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReleaseTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.ReleaseTabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.ReleaseTabControl1.Name = "ReleaseTabControl1"
        Me.ReleaseTabControl1.SelectedIndex = 0
        Me.ReleaseTabControl1.Size = New System.Drawing.Size(810, 433)
        Me.ReleaseTabControl1.TabIndex = 79
        '
        'JobInfoPage
        '
        Me.JobInfoPage.Controls.Add(Me.Label3)
        Me.JobInfoPage.Controls.Add(Me.makeqty_textbox)
        Me.JobInfoPage.Controls.Add(Me.orderqty_textbox)
        Me.JobInfoPage.Controls.Add(Me.dwgno_textbox)
        Me.JobInfoPage.Controls.Add(Me.Jobnote_textbox)
        Me.JobInfoPage.Controls.Add(Me.altpartNo_textbox)
        Me.JobInfoPage.Controls.Add(Me.Revision_textbox)
        Me.JobInfoPage.Controls.Add(Me.partno_textbox)
        Me.JobInfoPage.Controls.Add(Me.partdescription_textbox)
        Me.JobInfoPage.Controls.Add(Me.Label1)
        Me.JobInfoPage.Controls.Add(Me.Label4)
        Me.JobInfoPage.Controls.Add(Me.Label13)
        Me.JobInfoPage.Controls.Add(Me.Label10)
        Me.JobInfoPage.Controls.Add(Me.PartNo_label)
        Me.JobInfoPage.Controls.Add(Me.Label5)
        Me.JobInfoPage.Controls.Add(Me.Label12)
        Me.JobInfoPage.Location = New System.Drawing.Point(4, 30)
        Me.JobInfoPage.Margin = New System.Windows.Forms.Padding(4)
        Me.JobInfoPage.Name = "JobInfoPage"
        Me.JobInfoPage.Padding = New System.Windows.Forms.Padding(4)
        Me.JobInfoPage.Size = New System.Drawing.Size(802, 399)
        Me.JobInfoPage.TabIndex = 0
        Me.JobInfoPage.Text = "Job Info"
        Me.JobInfoPage.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(436, 73)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 17)
        Me.Label3.TabIndex = 89
        Me.Label3.Text = "Qty to Make"
        '
        'makeqty_textbox
        '
        Me.makeqty_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.makeqty_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.makeqty_textbox.Location = New System.Drawing.Point(440, 98)
        Me.makeqty_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.makeqty_textbox.Name = "makeqty_textbox"
        Me.makeqty_textbox.ReadOnly = True
        Me.makeqty_textbox.Size = New System.Drawing.Size(135, 25)
        Me.makeqty_textbox.TabIndex = 88
        '
        'orderqty_textbox
        '
        Me.orderqty_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.orderqty_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orderqty_textbox.Location = New System.Drawing.Point(440, 37)
        Me.orderqty_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.orderqty_textbox.Name = "orderqty_textbox"
        Me.orderqty_textbox.ReadOnly = True
        Me.orderqty_textbox.Size = New System.Drawing.Size(135, 25)
        Me.orderqty_textbox.TabIndex = 84
        '
        'dwgno_textbox
        '
        Me.dwgno_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.dwgno_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dwgno_textbox.Location = New System.Drawing.Point(7, 98)
        Me.dwgno_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dwgno_textbox.Name = "dwgno_textbox"
        Me.dwgno_textbox.ReadOnly = True
        Me.dwgno_textbox.Size = New System.Drawing.Size(185, 25)
        Me.dwgno_textbox.TabIndex = 82
        '
        'Jobnote_textbox
        '
        Me.Jobnote_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Jobnote_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Jobnote_textbox.Location = New System.Drawing.Point(4, 261)
        Me.Jobnote_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Jobnote_textbox.Multiline = True
        Me.Jobnote_textbox.Name = "Jobnote_textbox"
        Me.Jobnote_textbox.ReadOnly = True
        Me.Jobnote_textbox.Size = New System.Drawing.Size(791, 123)
        Me.Jobnote_textbox.TabIndex = 73
        '
        'altpartNo_textbox
        '
        Me.altpartNo_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.altpartNo_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.altpartNo_textbox.Location = New System.Drawing.Point(199, 98)
        Me.altpartNo_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.altpartNo_textbox.Name = "altpartNo_textbox"
        Me.altpartNo_textbox.ReadOnly = True
        Me.altpartNo_textbox.Size = New System.Drawing.Size(195, 25)
        Me.altpartNo_textbox.TabIndex = 79
        '
        'Revision_textbox
        '
        Me.Revision_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Revision_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Revision_textbox.Location = New System.Drawing.Point(199, 37)
        Me.Revision_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Revision_textbox.Name = "Revision_textbox"
        Me.Revision_textbox.ReadOnly = True
        Me.Revision_textbox.Size = New System.Drawing.Size(107, 25)
        Me.Revision_textbox.TabIndex = 77
        '
        'partno_textbox
        '
        Me.partno_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.partno_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.partno_textbox.Location = New System.Drawing.Point(8, 37)
        Me.partno_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.partno_textbox.Name = "partno_textbox"
        Me.partno_textbox.ReadOnly = True
        Me.partno_textbox.Size = New System.Drawing.Size(185, 25)
        Me.partno_textbox.TabIndex = 41
        '
        'partdescription_textbox
        '
        Me.partdescription_textbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.partdescription_textbox.BackColor = System.Drawing.SystemColors.ControlLight
        Me.partdescription_textbox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.partdescription_textbox.Location = New System.Drawing.Point(4, 160)
        Me.partdescription_textbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.partdescription_textbox.Multiline = True
        Me.partdescription_textbox.Name = "partdescription_textbox"
        Me.partdescription_textbox.ReadOnly = True
        Me.partdescription_textbox.Size = New System.Drawing.Size(790, 69)
        Me.partdescription_textbox.TabIndex = 75
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(436, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 17)
        Me.Label1.TabIndex = 85
        Me.Label1.Text = "Qty Order"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 73)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 17)
        Me.Label4.TabIndex = 83
        Me.Label4.Text = "Dwg No."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(195, 73)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(112, 17)
        Me.Label13.TabIndex = 80
        Me.Label13.Text = "Alternate Part No."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(195, 16)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 17)
        Me.Label10.TabIndex = 78
        Me.Label10.Text = "Revision"
        '
        'PartNo_label
        '
        Me.PartNo_label.AutoSize = True
        Me.PartNo_label.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PartNo_label.Location = New System.Drawing.Point(7, 16)
        Me.PartNo_label.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PartNo_label.Name = "PartNo_label"
        Me.PartNo_label.Size = New System.Drawing.Size(56, 17)
        Me.PartNo_label.TabIndex = 42
        Me.PartNo_label.Text = "Part No."
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 134)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 17)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "Part Description"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(7, 235)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 17)
        Me.Label12.TabIndex = 74
        Me.Label12.Text = "Notes"
        '
        'ReleasesPage
        '
        Me.ReleasesPage.Controls.Add(Me.Release_treeview)
        Me.ReleasesPage.Location = New System.Drawing.Point(4, 30)
        Me.ReleasesPage.Margin = New System.Windows.Forms.Padding(4)
        Me.ReleasesPage.Name = "ReleasesPage"
        Me.ReleasesPage.Padding = New System.Windows.Forms.Padding(4)
        Me.ReleasesPage.Size = New System.Drawing.Size(802, 398)
        Me.ReleasesPage.TabIndex = 1
        Me.ReleasesPage.Text = "Releases"
        Me.ReleasesPage.UseVisualStyleBackColor = True
        '
        'Release_treeview
        '
        Me.Release_treeview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Release_treeview.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Release_treeview.Location = New System.Drawing.Point(4, 4)
        Me.Release_treeview.Margin = New System.Windows.Forms.Padding(4)
        Me.Release_treeview.Name = "Release_treeview"
        Me.Release_treeview.Size = New System.Drawing.Size(794, 390)
        Me.Release_treeview.TabIndex = 0
        '
        'OrderDocTab
        '
        Me.OrderDocTab.Controls.Add(Me.ToolStrip3)
        Me.OrderDocTab.Controls.Add(Me.OrderDocs)
        Me.OrderDocTab.Location = New System.Drawing.Point(4, 30)
        Me.OrderDocTab.Margin = New System.Windows.Forms.Padding(4)
        Me.OrderDocTab.Name = "OrderDocTab"
        Me.OrderDocTab.Size = New System.Drawing.Size(802, 398)
        Me.OrderDocTab.TabIndex = 2
        Me.OrderDocTab.Text = "Order Documents"
        Me.OrderDocTab.UseVisualStyleBackColor = True
        '
        'ToolStrip3
        '
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton5, Me.ToolStripButton3, Me.ToolStripButton2})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(802, 25)
        Me.ToolStrip3.TabIndex = 1
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(81, 22)
        Me.ToolStripButton5.Text = "Add New FIle"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(57, 22)
        Me.ToolStripButton3.Text = "View File"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(103, 22)
        Me.ToolStripButton2.Text = "Delete Document"
        '
        'OrderDocs
        '
        Me.OrderDocs.AllowDrop = True
        Me.OrderDocs.AllowUserToAddRows = False
        Me.OrderDocs.AllowUserToDeleteRows = False
        Me.OrderDocs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OrderDocs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.OrderDocs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.OrderDocs.Location = New System.Drawing.Point(4, 34)
        Me.OrderDocs.Margin = New System.Windows.Forms.Padding(4)
        Me.OrderDocs.Name = "OrderDocs"
        Me.OrderDocs.Size = New System.Drawing.Size(791, 288)
        Me.OrderDocs.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 26)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.AxAcroPDF1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1381, 658)
        Me.SplitContainer1.SplitterDistance = 810
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 80
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.ReleaseTabControl1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RoutingDataGridView)
        Me.SplitContainer2.Size = New System.Drawing.Size(810, 658)
        Me.SplitContainer2.SplitterDistance = 433
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 90
        '
        'RoutingDataGridView
        '
        Me.RoutingDataGridView.AllowUserToAddRows = False
        Me.RoutingDataGridView.AllowUserToDeleteRows = False
        Me.RoutingDataGridView.AllowUserToOrderColumns = True
        Me.RoutingDataGridView.AllowUserToResizeRows = False
        Me.RoutingDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.RoutingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RoutingDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column3, Me.Column2, Me.Column4, Me.Column5, Me.Column6, Me.TimeUnit, Me.Column7, Me.Column8})
        Me.RoutingDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RoutingDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.RoutingDataGridView.Margin = New System.Windows.Forms.Padding(4)
        Me.RoutingDataGridView.MultiSelect = False
        Me.RoutingDataGridView.Name = "RoutingDataGridView"
        Me.RoutingDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.RoutingDataGridView.Size = New System.Drawing.Size(810, 220)
        Me.RoutingDataGridView.TabIndex = 3
        '
        'Column1
        '
        Me.Column1.HeaderText = "Step No"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 78
        '
        'Column3
        '
        Me.Column3.HeaderText = "Vendor Code"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 106
        '
        'Column2
        '
        Me.Column2.HeaderText = "Work Center"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 103
        '
        'Column4
        '
        Me.Column4.HeaderText = "Operation Code"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 122
        '
        'Column5
        '
        Me.Column5.HeaderText = "Description"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 104
        '
        'Column6
        '
        Me.Column6.HeaderText = "Setup Time"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 96
        '
        'TimeUnit
        '
        Me.TimeUnit.DataPropertyName = "TimeUnit"
        Me.TimeUnit.HeaderText = "TimeUnit"
        Me.TimeUnit.Name = "TimeUnit"
        Me.TimeUnit.Width = 89
        '
        'Column7
        '
        Me.Column7.HeaderText = "Cycle Time"
        Me.Column7.Name = "Column7"
        Me.Column7.Width = 94
        '
        'Column8
        '
        Me.Column8.HeaderText = "Cycle Unit"
        Me.Column8.Name = "Column8"
        Me.Column8.Width = 88
        '
        'AxAcroPDF1
        '
        Me.AxAcroPDF1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxAcroPDF1.Enabled = True
        Me.AxAcroPDF1.Location = New System.Drawing.Point(0, 0)
        Me.AxAcroPDF1.Margin = New System.Windows.Forms.Padding(4)
        Me.AxAcroPDF1.Name = "AxAcroPDF1"
        Me.AxAcroPDF1.OcxState = CType(resources.GetObject("AxAcroPDF1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxAcroPDF1.Size = New System.Drawing.Size(566, 658)
        Me.AxAcroPDF1.TabIndex = 0
        '
        'JobInfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1381, 684)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "JobInfoForm"
        Me.Text = "Job No: "
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ReleaseTabControl1.ResumeLayout(False)
        Me.JobInfoPage.ResumeLayout(False)
        Me.JobInfoPage.PerformLayout()
        Me.ReleasesPage.ResumeLayout(False)
        Me.OrderDocTab.ResumeLayout(False)
        Me.OrderDocTab.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        CType(Me.OrderDocs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RoutingDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxAcroPDF1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As ToolStripDropDownButton
    Friend WithEvents HomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobNoLabel As ToolStripLabel
    Friend WithEvents ReleaseTabControl1 As TabControl
    Friend WithEvents JobInfoPage As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents makeqty_textbox As TextBox
    Friend WithEvents orderqty_textbox As TextBox
    Friend WithEvents dwgno_textbox As TextBox
    Friend WithEvents Jobnote_textbox As TextBox
    Friend WithEvents altpartNo_textbox As TextBox
    Friend WithEvents Revision_textbox As TextBox
    Friend WithEvents partno_textbox As TextBox
    Friend WithEvents partdescription_textbox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents PartNo_label As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ReleasesPage As TabPage
    Friend WithEvents Release_treeview As TreeView
    Friend WithEvents OrderDocTab As TabPage
    Friend WithEvents ToolStrip3 As ToolStrip
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents OrderDocs As DataGridView
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    'Friend WithEvents AxAcroPDF1 As AxAcroPDFLib.AxAcroPDF
    Friend WithEvents BackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PartSearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents RoutingDataGridView As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents TimeUnit As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents AxAcroPDF1 As AxAcroPDFLib.AxAcroPDF
    Friend WithEvents JobLoading_Progressbar As ToolStripProgressBar
End Class
