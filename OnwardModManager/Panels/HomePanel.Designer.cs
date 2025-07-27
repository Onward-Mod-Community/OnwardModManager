namespace OnwardModManager.Panels
{
    partial class HomePanel
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
            lblTitle = new Label();
            pnlSetup = new Panel();
            lblInstall = new Label();
            btnBrowse = new Button();
            txtOnwardLocation = new TextBox();
            btnContinue = new Button();
            lblWelcomeInstall = new Label();
            lblWelcome = new Label();
            txtLicence = new TextBox();
            btnDisagree = new Button();
            btnAgree = new Button();
            pnlSetup.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(3, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(920, 61);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Onward Mod Manager";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlSetup
            // 
            pnlSetup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlSetup.Controls.Add(lblInstall);
            pnlSetup.Controls.Add(btnBrowse);
            pnlSetup.Controls.Add(txtOnwardLocation);
            pnlSetup.Controls.Add(btnContinue);
            pnlSetup.Controls.Add(lblWelcomeInstall);
            pnlSetup.Location = new Point(3, 74);
            pnlSetup.Name = "pnlSetup";
            pnlSetup.Size = new Size(917, 481);
            pnlSetup.TabIndex = 1;
            // 
            // lblInstall
            // 
            lblInstall.Anchor = AnchorStyles.Top;
            lblInstall.AutoSize = true;
            lblInstall.Location = new Point(410, 184);
            lblInstall.Name = "lblInstall";
            lblInstall.Size = new Size(109, 20);
            lblInstall.TabIndex = 4;
            lblInstall.Text = "Install location:";
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top;
            btnBrowse.Location = new Point(408, 240);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(111, 39);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtOnwardLocation
            // 
            txtOnwardLocation.Anchor = AnchorStyles.Top;
            txtOnwardLocation.Location = new Point(161, 207);
            txtOnwardLocation.Name = "txtOnwardLocation";
            txtOnwardLocation.Size = new Size(592, 27);
            txtOnwardLocation.TabIndex = 2;
            txtOnwardLocation.TextChanged += txtOnwardLocation_TextChanged;
            // 
            // btnContinue
            // 
            btnContinue.Anchor = AnchorStyles.Bottom;
            btnContinue.Enabled = false;
            btnContinue.Location = new Point(161, 397);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(592, 65);
            btnContinue.TabIndex = 1;
            btnContinue.Text = "Continue";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            // 
            // lblWelcomeInstall
            // 
            lblWelcomeInstall.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblWelcomeInstall.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWelcomeInstall.Location = new Point(3, 27);
            lblWelcomeInstall.Name = "lblWelcomeInstall";
            lblWelcomeInstall.Size = new Size(911, 79);
            lblWelcomeInstall.TabIndex = 0;
            lblWelcomeInstall.Text = "Welcome!\r\nPlease select / verify your installation of Onward";
            lblWelcomeInstall.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            lblWelcome.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblWelcome.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(3, 74);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(917, 82);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "Welcome!\r\nPlease read and agree to the licence to continue\r\n";
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtLicence
            // 
            txtLicence.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLicence.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLicence.Location = new Point(155, 159);
            txtLicence.Multiline = true;
            txtLicence.Name = "txtLicence";
            txtLicence.ReadOnly = true;
            txtLicence.ScrollBars = ScrollBars.Vertical;
            txtLicence.Size = new Size(613, 306);
            txtLicence.TabIndex = 3;
            // 
            // btnDisagree
            // 
            btnDisagree.Anchor = AnchorStyles.Bottom;
            btnDisagree.Location = new Point(341, 471);
            btnDisagree.Name = "btnDisagree";
            btnDisagree.Size = new Size(94, 52);
            btnDisagree.TabIndex = 4;
            btnDisagree.Text = "Disagree";
            btnDisagree.UseVisualStyleBackColor = true;
            btnDisagree.Click += btnDisagree_Click;
            // 
            // btnAgree
            // 
            btnAgree.Anchor = AnchorStyles.Bottom;
            btnAgree.Location = new Point(493, 471);
            btnAgree.Name = "btnAgree";
            btnAgree.Size = new Size(94, 52);
            btnAgree.TabIndex = 5;
            btnAgree.Text = "Agree";
            btnAgree.UseVisualStyleBackColor = true;
            btnAgree.Click += btnAgree_Click;
            // 
            // HomePanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlSetup);
            Controls.Add(btnAgree);
            Controls.Add(btnDisagree);
            Controls.Add(txtLicence);
            Controls.Add(lblWelcome);
            Controls.Add(lblTitle);
            Name = "HomePanel";
            Size = new Size(926, 558);
            pnlSetup.ResumeLayout(false);
            pnlSetup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlSetup;
        private Label lblWelcomeInstall;
        private Button btnContinue;
        private TextBox txtOnwardLocation;
        private Button btnBrowse;
        private Label lblInstall;
        private Label lblWelcome;
        private TextBox txtLicence;
        private Button btnDisagree;
        private Button btnAgree;
    }
}
