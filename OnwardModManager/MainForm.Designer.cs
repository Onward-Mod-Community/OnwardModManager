namespace OnwardModManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnlSplit = new SplitContainer();
            btnHome = new Button();
            btnMaps = new Button();
            btnSettings = new Button();
            btnMods = new Button();
            pnlMaps = new OnwardModManager.Panels.MapsPanel();
            pnlHome = new OnwardModManager.Panels.HomePanel();
            pnlMods = new OnwardModManager.Panels.ModsPanel();
            pnlSettings = new OnwardModManager.Panels.SettingsPanel();
            ((System.ComponentModel.ISupportInitialize)pnlSplit).BeginInit();
            pnlSplit.Panel1.SuspendLayout();
            pnlSplit.Panel2.SuspendLayout();
            pnlSplit.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSplit
            // 
            pnlSplit.Dock = DockStyle.Fill;
            pnlSplit.FixedPanel = FixedPanel.Panel1;
            pnlSplit.IsSplitterFixed = true;
            pnlSplit.Location = new Point(0, 0);
            pnlSplit.Name = "pnlSplit";
            // 
            // pnlSplit.Panel1
            // 
            pnlSplit.Panel1.BackColor = Color.WhiteSmoke;
            pnlSplit.Panel1.Controls.Add(btnHome);
            pnlSplit.Panel1.Controls.Add(btnMaps);
            pnlSplit.Panel1.Controls.Add(btnSettings);
            pnlSplit.Panel1.Controls.Add(btnMods);
            // 
            // pnlSplit.Panel2
            // 
            pnlSplit.Panel2.Controls.Add(pnlMaps);
            pnlSplit.Panel2.Controls.Add(pnlHome);
            pnlSplit.Panel2.Controls.Add(pnlMods);
            pnlSplit.Panel2.Controls.Add(pnlSettings);
            pnlSplit.Size = new Size(1182, 653);
            pnlSplit.SplitterDistance = 104;
            pnlSplit.TabIndex = 1;
            // 
            // btnHome
            // 
            btnHome.Location = new Point(12, 12);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(80, 80);
            btnHome.TabIndex = 0;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // btnMaps
            // 
            btnMaps.Location = new Point(12, 184);
            btnMaps.Name = "btnMaps";
            btnMaps.Size = new Size(80, 80);
            btnMaps.TabIndex = 2;
            btnMaps.Text = "Maps";
            btnMaps.UseVisualStyleBackColor = true;
            btnMaps.Click += btnMaps_Click;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(12, 270);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(80, 80);
            btnSettings.TabIndex = 3;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnMods
            // 
            btnMods.Location = new Point(12, 98);
            btnMods.Name = "btnMods";
            btnMods.Size = new Size(80, 80);
            btnMods.TabIndex = 1;
            btnMods.Text = "Mods";
            btnMods.UseVisualStyleBackColor = true;
            btnMods.Click += btnMods_Click;
            // 
            // pnlMaps
            // 
            pnlMaps.Location = new Point(3, 322);
            pnlMaps.Name = "pnlMaps";
            pnlMaps.Size = new Size(507, 328);
            pnlMaps.TabIndex = 2;
            // 
            // pnlHome
            // 
            pnlHome.Location = new Point(3, 3);
            pnlHome.Name = "pnlHome";
            pnlHome.Size = new Size(408, 261);
            pnlHome.TabIndex = 1;
            // 
            // pnlMods
            // 
            pnlMods.Location = new Point(663, 322);
            pnlMods.Name = "pnlMods";
            pnlMods.Size = new Size(399, 268);
            pnlMods.TabIndex = 0;
            // 
            // pnlSettings
            // 
            pnlSettings.Location = new Point(663, 35);
            pnlSettings.Name = "pnlSettings";
            pnlSettings.Size = new Size(399, 219);
            pnlSettings.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 653);
            Controls.Add(pnlSplit);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            Text = "Onward Mod Manager";
            Load += MainForm_Load;
            pnlSplit.Panel1.ResumeLayout(false);
            pnlSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlSplit).EndInit();
            pnlSplit.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        public SplitContainer pnlSplit;
        private Button btnMods;
        private Button btnSettings;
        private Button btnHome;
        private Button btnMaps;
        public Panels.ModsPanel pnlMods;
        public Panels.HomePanel pnlHome;
        public Panels.SettingsPanel pnlSettings;
        public Panels.MapsPanel pnlMaps;
    }
}
