using ModManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnwardModManager.Panels
{
    public partial class ModsPanel : UserControl
    {
        private static ModManager Manager => MainForm.Mods;
        private static Settings Settings => MainForm.Settings;

        public ModsPanel()
        {
            InitializeComponent();
        }

        public void RefreshList()
        {
            if (!Manager.Refresh())
            {
                MessageBox.Show("Failed to connect to server! \nPlease check your internet / make sure your API endpoint is valid", "Failed to connect");
                Application.Exit(); // Considering an offline mode for uninstalling mods
            }
            Manager.FindInstalledMods();

            HashSet<string> parsedMods = [];
            foreach (var mod in Manager.AllMods)
            {
                if (parsedMods.Contains(mod.ID))
                    continue;

                var latest = Manager.GetLatestVersion(mod.ID);
                AddOrUpdateModToList(false, latest);
                parsedMods.Add(mod.ID);
            }

            foreach (var mod in Manager.InstalledMods)
            {
                AddOrUpdateModToList(true, mod.Value);
            }

            foreach(var mod in Manager.InstalledMods.Values)
            {
                if (GetModDependents(mod.ID).Count > 0)
                    SetModLocked(mod.ID, true);
            }

            gvModList.Sort(colCategory, ListSortDirection.Ascending);
            gvModList.ClearSelection();
        }

        private bool CheckDependencies(ModInfo mod)
        {
            foreach (var dep in mod.Dependencies)
            {
                var latestDep = Manager.GetLatestVersion(dep.ID);
                if (latestDep is null)
                    return false;
                if (Version.Parse(latestDep.Version) < Version.Parse(dep.MinimumVersion))
                    return false;
                if (Manager.InstalledMods.ContainsKey(latestDep.ID))
                    continue;

                SetModLocked(dep.ID, true);
                var row = GetModRow(dep.ID);
                row.Cells[colModInstalled.Index].Value = true;
            }
            return true;
        }

        private void SetModLocked(string modId, bool locked)
        {
            var row = GetModRow(modId);
            row.ReadOnly = locked;
            if (locked)
            {
                row.Cells[colUninstall.Index] = new DataGridViewTextBoxCell();
            }
            else
            {
                if (Manager.InstalledMods.ContainsKey(modId))
                {
                    row.Cells[colUninstall.Index] = new DataGridViewButtonCell { Value = "Uninstall" };
                    row.Cells[colModInstalled.Index].ReadOnly = true;
                }
            }

            gvModList.Refresh();
        }

        private List<string> GetModDependents(string modId)
        {
            List<string> results = [];
            foreach (DataGridViewRow row in gvModList.Rows)
            {
                if (row.Cells[colModInstalled.Index].Value is false)
                    continue;
                var mod = row.Tag as ModInfo;
                if (mod.Dependencies.Any(m => m.ID == modId))
                    results.Add(mod.ID);
            }
            return results;
        }

        public int AddOrUpdateModToList(bool isInstalled, ModInfo modInfo)
        {
            int index = 0;
            var row = GetModRow(modInfo.ID);

            if (row is not default(DataGridViewRow)) // Update existing row
            {
                index = row.Index;
                row.Cells[colCategory.Index].Value = modInfo.Category;
                row.Cells[colModInstalled.Index].Value = isInstalled;
                row.Cells[colModName.Index].Value = modInfo.Name;
                row.Cells[colModVersion.Index].Value = isInstalled ? modInfo.Version : string.Empty;
                row.Cells[colLatestVersion.Index].Value = Manager.GetLatestVersion(modInfo.ID)?.Version;
                row.Cells[colModDescription.Index].Value = modInfo.Description;
                row.Cells[colUninstall.Index].Value = isInstalled ? "Uninstall" : null;
            }
            else // Add it to the list
            {
                index = gvModList.Rows.Add(modInfo.Category, isInstalled, modInfo.Name,
                    isInstalled ? modInfo.Version : string.Empty, Manager.GetLatestVersion(modInfo.ID)?.Version,
                    modInfo.Description, isInstalled ? "Uninstall" : null);
            }

            gvModList.Rows[index].Tag = modInfo;
            SetRowInstalled(index, isInstalled);
            return index;
        }

        public void SetRowInstalled(int rowIndex, bool isInstalled)
        {
            var row = gvModList.Rows[rowIndex];
            if (isInstalled)
            {
                var mod = row.Tag as ModInfo;
                row.Cells[colUninstall.Index] = new DataGridViewButtonCell() { Value = "Uninstall" };
                row.Cells[colModInstalled.Index].Value = true;
                row.Cells[colModInstalled.Index].ReadOnly = true;
                row.Cells[colModVersion.Index].Value = mod.Version;
                if (Version.Parse(Manager.GetLatestVersion(mod.ID)?.Version ?? "0.0.0") > Version.Parse(mod.Version))
                {
                    row.Cells[colModVersion.Index].Style.ForeColor = Color.Red;
                }
                else
                {
                    row.Cells[colModVersion.Index].Style.ForeColor = Color.Green;
                }
            }
            else
            {
                row.Cells[colUninstall.Index] = new DataGridViewTextBoxCell();
                row.Cells[colModInstalled.Index].Value = false;
                row.Cells[colModInstalled.Index].ReadOnly = false;
                row.Cells[colModVersion.Index].Value = string.Empty;
                row.Cells[colModVersion.Index].Style = row.DefaultCellStyle.Clone();
            }
        }

        public DataGridViewRow GetModRow(string modID)
        {
            return gvModList.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => (r.Tag as ModInfo)?.ID == modID);
        }

        #region Mods Grid View Events

        private void gvModList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gvModList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            gvModList.Refresh();
            if (e.RowIndex < 0 || e.RowIndex > gvModList.Rows.Count)
                return;
            if (e.ColumnIndex != colUninstall.Index)
            {
                return;
            }

            var clicked = gvModList.Rows[e.RowIndex];
            var mod = clicked.Tag as ModInfo;
            if (mod is null)
                return; // shouldnt happen

            var res = MessageBox.Show($"Are you sure you want to uninstall {mod.Name}?", "Uninstall", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res != DialogResult.Yes)
                return;

            if (!Manager.Uninstall(mod))
            {
                MessageBox.Show("Failed to uninstall mod!", "Uninstall", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetRowInstalled(clicked.Index, false);

            Settings.InstalledMods = Manager.InstalledMods; // not really needed since these should be the same
            Settings.Save();
        }

        private void gvModList_MouseDown(object sender, MouseEventArgs e)
        {
            gvModList.ClearSelection();
            btnModInfo.Enabled = false;
        }

        private void gvModList_SelectionChanged(object sender, EventArgs e)
        {
            btnModInfo.Enabled = gvModList.SelectedRows.Count > 0;
        }

        private void gvModList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > gvModList.Rows.Count)
                return;
            var mod = gvModList.Rows[e.RowIndex].Tag as ModInfo;
            if (e.ColumnIndex == colModInstalled.Index)
            {
                if (gvModList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value is true)
                {
                    var valid = CheckDependencies(mod);
                    if (!valid)
                    {
                        MessageBox.Show("Warning: failed to find depencencies for selected mod!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvModList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    }
                    else
                    {
                        foreach (var dep in mod.Dependencies)
                        {
                            SetModLocked(dep.ID, true);
                        }
                    }
                }
                else // We need to unlock any dependencies that were locked by this mod
                {
                    foreach (var dep in mod.Dependencies)
                    {
                        var usedBy = GetModDependents(dep.ID);
                        if (usedBy.Count == 0 || usedBy.Count == 1 && usedBy[0] == mod.ID)
                        {
                            SetModLocked(dep.ID, false);
                        }
                    }
                }
                return;
            }
        }

        #endregion

        #region Button Events

        private void btnInstall_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvModList.Rows)
            {
                if (row.Cells[colModInstalled.Index].Value is true)
                {
                    var mod = row.Tag as ModInfo;
                    if (Manager.InstalledMods.ContainsKey(mod.ID))
                    {
                        var latest = Manager.GetLatestVersion(mod.ID);
                        if (latest.Version != mod.Version)
                        {
                            if (!Manager.Update(mod.ID))
                            {
                                MessageBox.Show($"Failed to update mod '{mod.Name}'");
                            }
                            else
                            {
                                row.Tag = latest;
                                SetRowInstalled(row.Index, true);
                            }
                        }
                        continue;
                    }

                    if (!Manager.Install(mod))
                    {
                        MessageBox.Show($"Failed to install mod '{mod.Name}'");
                    }
                    else
                    {
                        SetRowInstalled(row.Index, true);
                    }
                }
            }
        }

        private void btnModInfo_Click(object sender, EventArgs e)
        {
            // TODO: this works for now but maybe a mod info dialog?
            if (gvModList.SelectedRows.Count == 0)
                return;

            var mod = gvModList.SelectedRows[0].Tag as ModInfo;
            if (mod is null) return;

            if (string.IsNullOrWhiteSpace(mod.ProjectUrl))
                return;

            Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = mod.ProjectUrl }).Dispose();
        }

        private void btnUninstallAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to uninstall ALL mods?", "Uninstall", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            foreach (DataGridViewRow row in gvModList.Rows)
            {
                if (row.Cells[colModInstalled.Index].Value is true)
                {
                    var mod = row.Tag as ModInfo;
                    if (!Manager.Uninstall(mod))
                    {
                        MessageBox.Show($"Failed to uninstall mod '{mod.Name}'");
                        return;
                    }
                    SetRowInstalled(row.Index, false);
                }
            }
        }

        #endregion

    }
}
