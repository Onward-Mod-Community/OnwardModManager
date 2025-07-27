using System.Windows.Forms;

namespace OnwardModManager.Panels
{
    partial class MapsPanel
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
            gvMapsList = new DataGridView();
            btnInstall = new Button();
            btnMapInfo = new Button();
            lblStatus = new Label();
            btnSelectAll = new Button();
            colCategory = new DataGridViewTextBoxColumn();
            colInstalled = new DataGridViewCheckBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colVersion = new DataGridViewTextBoxColumn();
            colLatestVersion = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            colUninstall = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)gvMapsList).BeginInit();
            SuspendLayout();
            // 
            // gvMapsList
            // 
            gvMapsList.AllowUserToAddRows = false;
            gvMapsList.AllowUserToDeleteRows = false;
            gvMapsList.AllowUserToResizeRows = false;
            gvMapsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gvMapsList.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gvMapsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gvMapsList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gvMapsList.Columns.AddRange(new DataGridViewColumn[] { colCategory, colInstalled, colName, colVersion, colLatestVersion, colDescription, colUninstall });
            gvMapsList.EditMode = DataGridViewEditMode.EditOnEnter;
            gvMapsList.Location = new Point(3, 3);
            gvMapsList.MultiSelect = false;
            gvMapsList.Name = "gvMapsList";
            gvMapsList.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            gvMapsList.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            gvMapsList.RowHeadersVisible = false;
            gvMapsList.RowHeadersWidth = 51;
            gvMapsList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            gvMapsList.RowsDefaultCellStyle = dataGridViewCellStyle8;
            gvMapsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvMapsList.Size = new Size(981, 535);
            gvMapsList.TabIndex = 10;
            gvMapsList.SelectionChanged += gvMapsList_SelectionChanged;
            gvMapsList.MouseDown += gvMapsList_MouseDown;
            // 
            // btnInstall
            // 
            btnInstall.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnInstall.Location = new Point(881, 544);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new Size(103, 48);
            btnInstall.TabIndex = 6;
            btnInstall.Text = "Install or Update";
            btnInstall.UseVisualStyleBackColor = true;
            btnInstall.Click += btnInstall_Click;
            // 
            // btnMapInfo
            // 
            btnMapInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnMapInfo.Enabled = false;
            btnMapInfo.Location = new Point(772, 544);
            btnMapInfo.Name = "btnMapInfo";
            btnMapInfo.Size = new Size(103, 48);
            btnMapInfo.TabIndex = 8;
            btnMapInfo.Text = "Map Info";
            btnMapInfo.UseVisualStyleBackColor = true;
            btnMapInfo.Click += btnMapInfo_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(112, 544);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(654, 48);
            lblStatus.TabIndex = 9;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSelectAll.Location = new Point(3, 544);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(103, 48);
            btnSelectAll.TabIndex = 11;
            btnSelectAll.Text = "Select All";
            btnSelectAll.UseVisualStyleBackColor = true;
            btnSelectAll.Click += btnSelectAll_Click;
            // 
            // colCategory
            // 
            colCategory.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            colCategory.DefaultCellStyle = dataGridViewCellStyle2;
            colCategory.HeaderText = "Categories";
            colCategory.MinimumWidth = 6;
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            colCategory.Resizable = DataGridViewTriState.False;
            colCategory.Width = 109;
            // 
            // colInstalled
            // 
            colInstalled.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colInstalled.HeaderText = "Install";
            colInstalled.MinimumWidth = 6;
            colInstalled.Name = "colInstalled";
            colInstalled.Resizable = DataGridViewTriState.False;
            colInstalled.Width = 54;
            // 
            // colName
            // 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colName.DefaultCellStyle = dataGridViewCellStyle3;
            colName.HeaderText = "Name";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Resizable = DataGridViewTriState.False;
            colName.Width = 78;
            // 
            // colVersion
            // 
            colVersion.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colVersion.DefaultCellStyle = dataGridViewCellStyle4;
            colVersion.HeaderText = "Version";
            colVersion.MinimumWidth = 6;
            colVersion.Name = "colVersion";
            colVersion.ReadOnly = true;
            colVersion.Resizable = DataGridViewTriState.False;
            colVersion.SortMode = DataGridViewColumnSortMode.NotSortable;
            colVersion.Width = 63;
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
            // colDescription
            // 
            colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDescription.DefaultCellStyle = dataGridViewCellStyle6;
            colDescription.HeaderText = "Description";
            colDescription.MinimumWidth = 6;
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            colDescription.Resizable = DataGridViewTriState.False;
            colDescription.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            // MapsPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSelectAll);
            Controls.Add(lblStatus);
            Controls.Add(btnMapInfo);
            Controls.Add(btnInstall);
            Controls.Add(gvMapsList);
            Name = "MapsPanel";
            Size = new Size(987, 595);
            ((System.ComponentModel.ISupportInitialize)gvMapsList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gvMapsList;
        private Button btnInstall;
        private Button btnMapInfo;
        private Label lblStatus;
        private Button btnSelectAll;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewCheckBoxColumn colInstalled;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colVersion;
        private DataGridViewTextBoxColumn colLatestVersion;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewButtonColumn colUninstall;
    }
}
