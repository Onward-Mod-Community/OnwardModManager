namespace OnwardModManager.Panels
{
    partial class ModsPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            gvModList = new DataGridView();
            colCategory = new DataGridViewTextBoxColumn();
            colModInstalled = new DataGridViewCheckBoxColumn();
            colModName = new DataGridViewTextBoxColumn();
            colModVersion = new DataGridViewTextBoxColumn();
            colLatestVersion = new DataGridViewTextBoxColumn();
            colModDescription = new DataGridViewTextBoxColumn();
            colUninstall = new DataGridViewButtonColumn();
            btnInstall = new Button();
            btnModInfo = new Button();
            btnUninstallAll = new Button();
            ((System.ComponentModel.ISupportInitialize)gvModList).BeginInit();
            SuspendLayout();
            // 
            // gvModList
            // 
            gvModList.AllowUserToAddRows = false;
            gvModList.AllowUserToDeleteRows = false;
            gvModList.AllowUserToResizeRows = false;
            gvModList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gvModList.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gvModList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gvModList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gvModList.Columns.AddRange(new DataGridViewColumn[] { colCategory, colModInstalled, colModName, colModVersion, colLatestVersion, colModDescription, colUninstall });
            gvModList.EditMode = DataGridViewEditMode.EditOnEnter;
            gvModList.Location = new Point(3, 3);
            gvModList.MultiSelect = false;
            gvModList.Name = "gvModList";
            gvModList.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            gvModList.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            gvModList.RowHeadersVisible = false;
            gvModList.RowHeadersWidth = 51;
            gvModList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            gvModList.RowsDefaultCellStyle = dataGridViewCellStyle8;
            gvModList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvModList.Size = new Size(1031, 542);
            gvModList.TabIndex = 4;
            gvModList.CellContentClick += gvModList_CellContentClick;
            gvModList.CellValueChanged += gvModList_CellValueChanged;
            gvModList.SelectionChanged += gvModList_SelectionChanged;
            gvModList.MouseDown += gvModList_MouseDown;
            // 
            // colCategory
            // 
            colCategory.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCategory.DefaultCellStyle = dataGridViewCellStyle2;
            colCategory.HeaderText = "Category";
            colCategory.MinimumWidth = 6;
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            colCategory.Resizable = DataGridViewTriState.False;
            colCategory.Width = 98;
            // 
            // colModInstalled
            // 
            colModInstalled.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colModInstalled.HeaderText = "Install";
            colModInstalled.MinimumWidth = 6;
            colModInstalled.Name = "colModInstalled";
            colModInstalled.Resizable = DataGridViewTriState.False;
            colModInstalled.Width = 54;
            // 
            // colModName
            // 
            colModName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colModName.DefaultCellStyle = dataGridViewCellStyle3;
            colModName.HeaderText = "Name";
            colModName.MinimumWidth = 6;
            colModName.Name = "colModName";
            colModName.ReadOnly = true;
            colModName.Resizable = DataGridViewTriState.False;
            colModName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colModName.Width = 55;
            // 
            // colModVersion
            // 
            colModVersion.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colModVersion.DefaultCellStyle = dataGridViewCellStyle4;
            colModVersion.HeaderText = "Version";
            colModVersion.MinimumWidth = 6;
            colModVersion.Name = "colModVersion";
            colModVersion.ReadOnly = true;
            colModVersion.Resizable = DataGridViewTriState.False;
            colModVersion.SortMode = DataGridViewColumnSortMode.NotSortable;
            colModVersion.Width = 63;
            // 
            // colLatestVersion
            // 
            colLatestVersion.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colLatestVersion.DefaultCellStyle = dataGridViewCellStyle5;
            colLatestVersion.HeaderText = "Latest";
            colLatestVersion.MinimumWidth = 6;
            colLatestVersion.Name = "colLatestVersion";
            colLatestVersion.ReadOnly = true;
            colLatestVersion.Resizable = DataGridViewTriState.False;
            colLatestVersion.SortMode = DataGridViewColumnSortMode.NotSortable;
            colLatestVersion.Width = 54;
            // 
            // colModDescription
            // 
            colModDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colModDescription.DefaultCellStyle = dataGridViewCellStyle6;
            colModDescription.HeaderText = "Description";
            colModDescription.MinimumWidth = 6;
            colModDescription.Name = "colModDescription";
            colModDescription.ReadOnly = true;
            colModDescription.Resizable = DataGridViewTriState.False;
            colModDescription.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colUninstall
            // 
            colUninstall.HeaderText = "Uninstall";
            colUninstall.MinimumWidth = 6;
            colUninstall.Name = "colUninstall";
            colUninstall.ReadOnly = true;
            colUninstall.Resizable = DataGridViewTriState.False;
            colUninstall.Width = 125;
            // 
            // btnInstall
            // 
            btnInstall.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnInstall.Location = new Point(931, 551);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new Size(103, 48);
            btnInstall.TabIndex = 5;
            btnInstall.Text = "Install or Update";
            btnInstall.UseVisualStyleBackColor = true;
            btnInstall.Click += btnInstall_Click;
            // 
            // btnModInfo
            // 
            btnModInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnModInfo.Enabled = false;
            btnModInfo.Location = new Point(822, 551);
            btnModInfo.Name = "btnModInfo";
            btnModInfo.Size = new Size(103, 48);
            btnModInfo.TabIndex = 7;
            btnModInfo.Text = "Mod Info";
            btnModInfo.UseVisualStyleBackColor = true;
            btnModInfo.Click += btnModInfo_Click;
            // 
            // btnUninstallAll
            // 
            btnUninstallAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUninstallAll.ForeColor = Color.Red;
            btnUninstallAll.Location = new Point(3, 551);
            btnUninstallAll.Name = "btnUninstallAll";
            btnUninstallAll.Size = new Size(103, 48);
            btnUninstallAll.TabIndex = 6;
            btnUninstallAll.Text = "Uninstall All";
            btnUninstallAll.UseVisualStyleBackColor = true;
            btnUninstallAll.Click += btnUninstallAll_Click;
            // 
            // ModsPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gvModList);
            Controls.Add(btnInstall);
            Controls.Add(btnModInfo);
            Controls.Add(btnUninstallAll);
            Name = "ModsPanel";
            Size = new Size(1037, 602);
            ((System.ComponentModel.ISupportInitialize)gvModList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gvModList;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewCheckBoxColumn colModInstalled;
        private DataGridViewTextBoxColumn colModName;
        private DataGridViewTextBoxColumn colModVersion;
        private DataGridViewTextBoxColumn colLatestVersion;
        private DataGridViewTextBoxColumn colModDescription;
        private DataGridViewButtonColumn colUninstall;
        private Button btnInstall;
        private Button btnModInfo;
        private Button btnUninstallAll;
    }
}
