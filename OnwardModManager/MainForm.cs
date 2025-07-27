using ModManagerLib;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace OnwardModManager
{
    public delegate bool PageChangingEvent(PageType chagingTo);

    public enum PageType 
    {
        None,
        Home,
        Mods,
        Maps,
        Settings
    }

    public partial class MainForm : Form
    {
        public static ModManager Mods { get; set; }
        public static MapManager Maps { get; set; }
        public static Settings Settings { get; set; }
        public static MainForm Instance { get; set; }

        public PageType CurrentPage { get; private set; }
        public event PageChangingEvent CurrentPageChanging = delegate { return true; };

        public MainForm()
        {
            Instance = this;
            Settings ??= Settings.Load();
            Mods ??= new ModManager(Settings.Api);
            Maps ??= new MapManager(Settings.Api);

            InitializeComponent();

            pnlHome.Dock = DockStyle.Fill;
            pnlMods.Dock = DockStyle.Fill;
            pnlMaps.Dock = DockStyle.Fill;
            pnlSettings.Dock = DockStyle.Fill;
            pnlHome.BringToFront();
            SetTitle("Home");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Settings.OnwardPath))
            {
                Settings.OnwardPath = Mods.FindOnwardFolder();
            }

            if (Settings.FirstLoad || string.IsNullOrWhiteSpace(Settings.OnwardPath))
            {
                btnMods.Enabled = false;
                btnMaps.Enabled = false;
                btnHome.Select();
                pnlHome.BringToFront();
                pnlHome.SetupFirstTime();
                SetTitle("Home");
            }
            else if (!Settings.AgreedLicence)
            {
                btnMods.Enabled = false;
                btnMaps.Enabled = false;
                btnHome.Select();
                pnlHome.BringToFront();
                SetTitle("Home");
            }
            else
            {
                Mods.SetOnwardFolder(Settings.OnwardPath);
                Mods.SetInstalledMods(Settings.InstalledMods);
                btnMods.Select();
                pnlMods.RefreshList();
                pnlMods.BringToFront();
                pnlMaps.RefreshList();
                SetTitle("Mods");
            }
        }

        public void AgreedLicence()
        {
            Mods.SetOnwardFolder(Settings.OnwardPath);
            Mods.SetInstalledMods(Settings.InstalledMods);
            pnlMods.RefreshList();
            pnlMaps.RefreshList();
            btnMods.Enabled = true;
            btnMaps.Enabled = true;
        }

        public void SetTitle(string title)
        {
            this.Text = "Onward Mod Manager" + (string.IsNullOrWhiteSpace(title) ? string.Empty : $" - {title}");
        }

        public void SetNavigation(bool enable)
        {
            btnHome.Enabled = enable;
            btnMods.Enabled = enable;
            btnMaps.Enabled = enable;
            btnSettings.Enabled = enable;
        }

        #region Navigation Buttons

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (!CurrentPageChanging(PageType.Home))
                return;

            pnlHome.BringToFront();
            pnlHome.Show();
            SetTitle("Home");
            CurrentPage = PageType.Home;
        }

        private void btnMods_Click(object sender, EventArgs e)
        {
            if (!CurrentPageChanging(PageType.Mods))
                return;

            pnlMods.BringToFront();
            pnlMods.Show();
            SetTitle("Mods");
            CurrentPage = PageType.Mods;
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            if (!CurrentPageChanging(PageType.Maps))
                return;

            pnlMaps.BringToFront();
            pnlMaps.Show();
            SetTitle("Maps");
            CurrentPage = PageType.Maps;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (!CurrentPageChanging(PageType.Settings))
                return;

            pnlSettings.RefreshSettings();
            pnlSettings.BringToFront();
            pnlSettings.Show();
            SetTitle("Settings");
            CurrentPage = PageType.Settings;
        }

        #endregion

        #region Home Panel
        // Moved to Panels/HomePanel.cs
        #endregion

        #region Maps Panel
        // Moved to Panels/MapsPanel.cs
        #endregion

        #region Settings Panel
        // Moved to Panels/SettingsPanel.cs
        #endregion

    }
}
