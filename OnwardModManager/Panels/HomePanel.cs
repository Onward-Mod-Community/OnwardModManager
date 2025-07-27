using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnwardModManager.Panels
{
    public partial class HomePanel : UserControl
    {
        public Settings Settings => MainForm.Settings;

        const string Licence = @"Copyright © 2025 Onward Mod Community

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";

        public HomePanel()
        {
            InitializeComponent();

            pnlSetup.Hide();
            pnlSetup.SendToBack();
            txtLicence.Text = Licence;
        }

        public void SetupFirstTime()
        {
            txtOnwardLocation.Text = Settings.OnwardPath;
            pnlSetup.Show();
            pnlSetup.BringToFront();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "Onward|Onward.exe",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = txtOnwardLocation.Text
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            txtOnwardLocation.Text = Path.GetDirectoryName(ofd.FileName);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Settings.OnwardPath = txtOnwardLocation.Text;
            pnlSetup.Hide();
            pnlSetup.SendToBack();
        }

        private void txtOnwardLocation_TextChanged(object sender, EventArgs e)
        {
            btnContinue.Enabled = (Directory.Exists(txtOnwardLocation.Text) && File.Exists(Path.Combine(txtOnwardLocation.Text, "Onward.exe")));
        }

        private void btnAgree_Click(object sender, EventArgs e)
        {
            if (Settings.AgreedLicence)
            {
                MessageBox.Show("Already agreed!");
                return;
            }
            Settings.AgreedLicence = true;
            MainForm.Instance.AgreedLicence();
            MessageBox.Show("You can now use the mod manager!");
        }

        private void btnDisagree_Click(object sender, EventArgs e)
        {
            Settings.AgreedLicence = false;
            Application.Exit();
        }
    }
}
