using ModManagerLib;
using OnwardModManager.Dialogs;
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
    public partial class MapsPanel : UserControl
    {
        private static MapManager Manager => MainForm.Maps;

        public MapsPanel()
        {
            InitializeComponent();
        }

        public void SetStatus(string status)
        {
            lblStatus.Text = status;
        }

        public void RefreshList()
        {
            Manager.Refresh();
            Manager.FindInstalledMaps();

            HashSet<string> parsedMaps = [];
            foreach (var maps in Manager.AllMaps)
            {
                if (parsedMaps.Contains(maps.ID))
                    continue;

                var latest = Manager.GetLatestVersion(maps.ID);
                AddOrUpdateMapToList(false, latest);
                parsedMaps.Add(maps.ID);
            }

            foreach (var mod in Manager.InstalledMaps)
            {
                AddOrUpdateMapToList(true, mod.Value);
            }

            gvMapsList.Sort(colName, ListSortDirection.Ascending);
            gvMapsList.ClearSelection();
        }

        public void SetRowInstalled(int rowIndex, bool isInstalled)
        {
            var row = gvMapsList.Rows[rowIndex];
            if (isInstalled)
            {
                var map = row.Tag as MapInfo;
                row.Cells[colUninstall.Index] = new DataGridViewButtonCell() { Value = "Uninstall" };
                row.Cells[colInstalled.Index].Value = true;
                row.Cells[colInstalled.Index].ReadOnly = true;
                row.Cells[colVersion.Index].Value = map.Version;
                if (Version.Parse(Manager.GetLatestVersion(map.ID).Version) > Version.Parse(map.Version))
                {
                    row.Cells[colVersion.Index].Style.ForeColor = Color.Red;
                }
                else
                {
                    row.Cells[colVersion.Index].Style.ForeColor = Color.Green;
                }
            }
            else
            {
                row.Cells[colUninstall.Index] = new DataGridViewTextBoxCell();
                row.Cells[colInstalled.Index].Value = false;
                row.Cells[colInstalled.Index].ReadOnly = false;
                row.Cells[colVersion.Index].Value = string.Empty;
                row.Cells[colVersion.Index].Style.ForeColor = SystemColors.ControlText;
            }
        }

        public int AddOrUpdateMapToList(bool isInstalled, MapInfo mapInfo)
        {
            int index = 0;
            var row = GetMapRow(mapInfo.ID);

            var catStr = string.Empty;
            foreach (var category in mapInfo.Categories)
            {
                catStr += $"{category}, ";
            }
            if (catStr == string.Empty)
            {
                catStr = "None";
            }
            catStr = catStr.TrimEnd(',', ' ');

            if (row is not default(DataGridViewRow)) // Update existing row
            {
                index = row.Index;
                row.Cells[colCategory.Index].Value = catStr;
                row.Cells[colInstalled.Index].Value = isInstalled;
                row.Cells[colName.Index].Value = mapInfo.Name;
                row.Cells[colVersion.Index].Value = isInstalled ? mapInfo.Version : string.Empty;
                row.Cells[colLatestVersion.Index].Value = Manager.GetLatestVersion(mapInfo.ID)?.Version;
                row.Cells[colDescription.Index].Value = mapInfo.Description;
                row.Cells[colUninstall.Index].Value = isInstalled ? "Uninstall" : null;
            }
            else // Add it to the list
            {
                index = gvMapsList.Rows.Add(catStr, isInstalled, mapInfo.Name,
                    isInstalled ? mapInfo.Version : string.Empty, Manager.GetLatestVersion(mapInfo.ID)?.Version,
                    mapInfo.Description, isInstalled ? "Uninstall" : null);
            }

            gvMapsList.Rows[index].Tag = mapInfo;
            SetRowInstalled(index, isInstalled);
            return index;
        }

        public DataGridViewRow GetMapRow(string modID) => 
            gvMapsList.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => (r.Tag as MapInfo)?.ID == modID);

        #region Map Grid View Events

        private void gvMapsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex != colUninstall.Index)
            {
                return;
            }

            var clicked = gvMapsList.Rows[e.RowIndex];
            var map = clicked.Tag as MapInfo;
            if (map is null)
                return; // shouldnt happen

            var res = MessageBox.Show($"Are you sure you want to uninstall {map.Name}?", "Uninstall", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res != DialogResult.Yes)
                return;

            if (!Manager.Uninstall(map))
            {
                MessageBox.Show("Failed to uninstall map!", "Uninstall", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetRowInstalled(clicked.Index, false);
        }

        private void gvMapsList_SelectionChanged(object sender, EventArgs e)
        {
            if (gvMapsList.SelectedRows.Count == 0)
            {
                btnMapInfo.Enabled = false;
                return;
            }
            var row = gvMapsList.SelectedRows[0];
            btnMapInfo.Enabled = row.Tag is MapInfo;
        }

        private void gvMapsList_MouseDown(object sender, MouseEventArgs e)
        {
            gvMapsList.ClearSelection();
            btnMapInfo.Enabled = false;
        }

        #endregion

        #region Buttons

        private async void btnInstall_Click(object sender, EventArgs e)
        {
            bool btnMapInfoState = btnMapInfo.Enabled;
            this.btnMapInfo.Enabled = false;
            this.btnInstall.Enabled = false;
            this.btnSelectAll.Enabled = false;
            this.gvMapsList.Enabled = false;
            MainForm.Instance.SetNavigation(false);

            await Task.Run(delegate
            {
                foreach (DataGridViewRow row in gvMapsList.Rows)
                {
                    if (row.Cells[colInstalled.Index].Value is true)
                    {
                        var map = row.Tag as MapInfo;
                        if (Manager.InstalledMaps.ContainsKey(map.ID))
                        {
                            var latest = Manager.GetLatestVersion(map.ID);
                            if (latest.Version != map.Version)
                            {
                                Invoke(() => SetStatus($"Updating map: {map.Name} (0%)"));

                                if (!Manager.Update(map.ID, new Progress<float>(f =>
                                {
                                    Invoke(() => SetStatus($"Updating map: {map.Name} ({(int)(f * 100)}%)"));
                                })))
                                {
                                    MessageBox.Show($"Failed to update map '{map.Name}'", "Update");
                                }
                                else
                                {
                                    row.Tag = latest;
                                    Invoke(() => SetRowInstalled(row.Index, true));
                                }
                            }
                            continue;
                        }

                        Invoke(() => SetStatus($"Downloading map: {map.Name} (0%)"));
                        if (Manager.Install(map, new Progress<float>(f =>
                        {
                            Invoke(() => SetStatus($"Downloading map: {map.Name} ({(int)(f * 100)}%)"));
                        })))
                        {
                            Invoke(() => SetRowInstalled(row.Index, true));
                        }
                    }
                }
            });

            SetStatus("");
            this.btnInstall.Enabled = true;
            this.btnMapInfo.Enabled = btnMapInfoState;
            this.gvMapsList.Enabled = true;
            this.btnSelectAll.Enabled = true;
            MainForm.Instance.SetNavigation(true);
        }

        private void btnMapInfo_Click(object sender, EventArgs e)
        {
            var map = gvMapsList.SelectedRows[0].Tag as MapInfo;
            MapInfoDialog.Show(map);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvMapsList.Rows)
            {
                row.Cells[colInstalled.Index].Value = true;
            }
        }

        #endregion

    }
}
