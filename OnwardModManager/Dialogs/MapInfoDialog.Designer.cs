namespace OnwardModManager.Dialogs
{
    partial class MapInfoDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapInfoDialog));
            imgMapThumbnail = new PictureBox();
            lblName = new Label();
            txtDescription = new TextBox();
            lblCategories = new Label();
            lblVersion = new Label();
            lblSize = new Label();
            lblAuthors = new Label();
            lblID = new Label();
            ((System.ComponentModel.ISupportInitialize)imgMapThumbnail).BeginInit();
            SuspendLayout();
            // 
            // imgMapThumbnail
            // 
            imgMapThumbnail.Location = new Point(12, 12);
            imgMapThumbnail.Name = "imgMapThumbnail";
            imgMapThumbnail.Size = new Size(505, 256);
            imgMapThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            imgMapThumbnail.TabIndex = 0;
            imgMapThumbnail.TabStop = false;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblName.Font = new Font("Segoe UI", 9F);
            lblName.Location = new Point(523, 3);
            lblName.Name = "lblName";
            lblName.Size = new Size(265, 34);
            lblName.TabIndex = 1;
            lblName.Text = "Name: ";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(12, 284);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.Size = new Size(505, 154);
            txtDescription.TabIndex = 4;
            txtDescription.TextAlign = HorizontalAlignment.Center;
            // 
            // lblCategories
            // 
            lblCategories.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblCategories.Font = new Font("Segoe UI", 9F);
            lblCategories.Location = new Point(523, 37);
            lblCategories.Name = "lblCategories";
            lblCategories.Size = new Size(265, 34);
            lblCategories.TabIndex = 5;
            lblCategories.Text = "Categories: ";
            lblCategories.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            lblVersion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblVersion.Font = new Font("Segoe UI", 9F);
            lblVersion.Location = new Point(523, 71);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(265, 34);
            lblVersion.TabIndex = 6;
            lblVersion.Text = "Version: ";
            lblVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSize
            // 
            lblSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSize.Font = new Font("Segoe UI", 9F);
            lblSize.Location = new Point(523, 105);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(265, 34);
            lblSize.TabIndex = 7;
            lblSize.Text = "File Size: ";
            lblSize.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAuthors
            // 
            lblAuthors.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAuthors.Font = new Font("Segoe UI", 9F);
            lblAuthors.Location = new Point(523, 139);
            lblAuthors.Name = "lblAuthors";
            lblAuthors.Size = new Size(265, 34);
            lblAuthors.TabIndex = 8;
            lblAuthors.Text = "Authors: ";
            lblAuthors.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Font = new Font("Segoe UI", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblID.Location = new Point(523, 421);
            lblID.Name = "lblID";
            lblID.Size = new Size(27, 17);
            lblID.TabIndex = 9;
            lblID.Text = "ID: ";
            // 
            // MapInfoDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblID);
            Controls.Add(lblAuthors);
            Controls.Add(lblSize);
            Controls.Add(lblVersion);
            Controls.Add(lblCategories);
            Controls.Add(txtDescription);
            Controls.Add(lblName);
            Controls.Add(imgMapThumbnail);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MapInfoDialog";
            Text = "MapInfoDialog";
            ((System.ComponentModel.ISupportInitialize)imgMapThumbnail).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imgMapThumbnail;
        private Label lblName;
        private TextBox txtDescription;
        private Label lblCategories;
        private Label lblVersion;
        private Label lblSize;
        private Label lblAuthors;
        private Label lblID;
    }
}