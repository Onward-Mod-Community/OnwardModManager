using ModManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnwardModManager
{
    [Serializable]
    [JsonSerializable(typeof(Settings))]
    public class Settings
    {
        [JsonIgnore]
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
        };

        [JsonIgnore]
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OnwardModManager", "settings.json");

        [JsonIgnore]
        public bool FirstLoad { get; set; } = false;

        public string Api { get; set; } = "onward.glowie.club";
        public string OnwardPath { get; set; } = string.Empty;
        public bool AgreedLicence { get; set; } = false;
        public Dictionary<string, ModInfo> InstalledMods { get; set; } = [];


        [JsonConstructor]
        public Settings() { }

        public static Settings Load()
        {
            if (File.Exists(FilePath))
            {
                return JsonSerializer.Deserialize<Settings>(File.ReadAllBytes(FilePath), SerializerOptions);
            }
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            
            var settings = new Settings();
            File.WriteAllBytes(FilePath, JsonSerializer.SerializeToUtf8Bytes(settings, SerializerOptions));
            settings.FirstLoad = true;
            return settings;
        }

        public void Save()
        {
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }

            File.WriteAllBytes(FilePath, JsonSerializer.SerializeToUtf8Bytes(this, SerializerOptions));
        }

    }
}
