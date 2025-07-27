using ModManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnwardModManager.Dialogs
{
    public partial class MapInfoDialog : Form
    {
        private MapInfo Map { get; set; }

        public MapInfoDialog(MapInfo map)
        {
            Map = map;
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            if (!string.IsNullOrWhiteSpace(Map.ThumbnailUrl))
            {
                try
                {
                    using var client = new HttpClient();
                    var imgBytes = client.GetByteArrayAsync(Map.ThumbnailUrl).Result;
                    using var ms = new MemoryStream(imgBytes);
                    imgMapThumbnail.Image = Image.FromStream(ms);
                }
                catch { }
            }

            this.Text = $"Map Info - {Map.Name}";
            lblName.Text = $"Name: {Map.Name}";

            string tmp = string.Empty;
            foreach (var cat in Map.Categories)
            {
                tmp += $"{cat}, ";
            }

            tmp = tmp.TrimEnd(',', ' ');

            lblCategories.Text = $"Categories: {tmp}";
            lblVersion.Text = $"Version: {Map.Version}";
            
            lblSize.Text = $"File Size: {Map.SizeBytes/1_024_000} MB";

            tmp = string.Empty;
            foreach (var author in Map.Authors)
                tmp += $"{author}, ";
            tmp = tmp.TrimEnd(',', ' ');

            lblAuthors.Text = $"Authors: {tmp}";
            lblID.Text = $"ID: {Map.ID}";

            txtDescription.Text = Map.Description;
            lblName.Select(); // so the text isn't selected
        }

        public static DialogResult Show(MapInfo map)
        {
            using var dialog = new MapInfoDialog(map);
            return dialog.ShowDialog();
        }

    }
}
