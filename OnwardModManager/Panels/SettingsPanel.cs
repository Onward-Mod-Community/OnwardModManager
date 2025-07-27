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
    public partial class SettingsPanel : UserControl
    {
        private Settings Settings => MainForm.Settings;
        private bool ChangesPending { get; set; } = false;

        public SettingsPanel()
        {
            InitializeComponent();
        }

        private void SettingsPanel_Load(object sender, EventArgs e)
        {
            MainForm.Instance.CurrentPageChanging += Instance_CurrentPageChanging;
            RefreshSettings();
        }

        public void RefreshSettings()
        {
            txtApi.Text = Settings.Api;
            txtOnwardPath.Text = Settings.OnwardPath;
            ChangesPending = false;
        }

        private bool Instance_CurrentPageChanging(PageType chagingTo)
        {
            if (chagingTo == PageType.Settings && MainForm.Instance.CurrentPage == PageType.Settings)
                return false;

            if (!ChangesPending)
                return true;

            var res = MessageBox.Show("Would you like to save your changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.No)
            {
                RefreshSettings();
                return true;
            }
            if (res == DialogResult.Cancel)
                return false;

            // Validate settings
            string api = txtApi.Text.Trim().ToLower();
            string path = txtOnwardPath.Text.Trim();
            if (api.StartsWith("https://"))
            {
                api = api.Remove(0, "https://".Length);
            }

            if (Uri.CheckHostName(api) == UriHostNameType.Unknown)
            {
                MessageBox.Show("Invalid API endpoint");
                return false;
            }

            if (!Directory.Exists(path) || !File.Exists(Path.Combine(path, "Onward.exe")))
            {
                MessageBox.Show("Invalid onward path!");
                return false;
            }

            Settings.Api = api;
            Settings.OnwardPath = path;
            Settings.Save();
            ChangesPending = false;
            return true;
        }

        private void txtOnwardPath_TextChanged(object sender, EventArgs e)
        {
            ChangesPending = true;
        }
        private void txtApi_TextChanged(object sender, EventArgs e)
        {
            ChangesPending = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "Onward|Onward.exe",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = txtOnwardPath.Text
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            txtOnwardPath.Text = Path.GetDirectoryName(ofd.FileName);
            ChangesPending = true;
        }
    }
}
