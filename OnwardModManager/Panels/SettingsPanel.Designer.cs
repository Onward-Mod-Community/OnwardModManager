
namespace OnwardModManager.Panels
{
    partial class SettingsPanel
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
            lblOnwardPath = new Label();
            txtOnwardPath = new TextBox();
            lblSettings = new Label();
            btnBrowse = new Button();
            txtApi = new TextBox();
            lblApi = new Label();
            SuspendLayout();
            // 
            // lblOnwardPath
            // 
            lblOnwardPath.Anchor = AnchorStyles.Top;
            lblOnwardPath.AutoSize = true;
            lblOnwardPath.Location = new Point(312, 185);
            lblOnwardPath.Name = "lblOnwardPath";
            lblOnwardPath.Size = new Size(93, 20);
            lblOnwardPath.TabIndex = 0;
            lblOnwardPath.Text = "Onward Path";
            // 
            // txtOnwardPath
            // 
            txtOnwardPath.Anchor = AnchorStyles.Top;
            txtOnwardPath.Location = new Point(312, 208);
            txtOnwardPath.Name = "txtOnwardPath";
            txtOnwardPath.Size = new Size(413, 27);
            txtOnwardPath.TabIndex = 1;
            txtOnwardPath.TextAlign = HorizontalAlignment.Center;
            txtOnwardPath.TextChanged += txtOnwardPath_TextChanged;
            // 
            // lblSettings
            // 
            lblSettings.AutoSize = true;
            lblSettings.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSettings.Location = new Point(13, 12);
            lblSettings.Name = "lblSettings";
            lblSettings.Size = new Size(141, 46);
            lblSettings.TabIndex = 2;
            lblSettings.Text = "Settings";
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top;
            btnBrowse.Location = new Point(312, 241);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(413, 45);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtApi
            // 
            txtApi.Anchor = AnchorStyles.Top;
            txtApi.Location = new Point(312, 136);
            txtApi.Name = "txtApi";
            txtApi.Size = new Size(413, 27);
            txtApi.TabIndex = 5;
            txtApi.TextAlign = HorizontalAlignment.Center;
            txtApi.TextChanged += txtApi_TextChanged;
            // 
            // lblApi
            // 
            lblApi.Anchor = AnchorStyles.Top;
            lblApi.AutoSize = true;
            lblApi.Location = new Point(312, 113);
            lblApi.Name = "lblApi";
            lblApi.Size = new Size(95, 20);
            lblApi.TabIndex = 4;
            lblApi.Text = "API Endpoint";
            // 
            // SettingsPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtApi);
            Controls.Add(lblApi);
            Controls.Add(btnBrowse);
            Controls.Add(lblSettings);
            Controls.Add(txtOnwardPath);
            Controls.Add(lblOnwardPath);
            Name = "SettingsPanel";
            Size = new Size(1057, 589);
            Load += SettingsPanel_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblOnwardPath;
        private TextBox txtOnwardPath;
        private Label lblSettings;
        private Button btnBrowse;
        private TextBox txtApi;
        private Label lblApi;
    }
}
