using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModManagerLib
{
    [Serializable]
    [JsonSerializable(typeof(ModFileInfo))]
    public class ModFileInfo
    {
        public string Path { get; set; } = string.Empty; // relative to onward install
        public string Hash { get; set; } = string.Empty; // sha-256 string
        public long Size { get; set; } = 0;
    }

    /// <summary>
    /// Full metadata for a mod
    /// </summary>
    [Serializable]
    [JsonSerializable(typeof(ModInfo))]
    public class ModInfo
    {
        public string ID { get; set; } // unique id for this mod
        public string Name { get; set; } // display name
        public string Version { get; set; } // current version
        public string Description { get; set; }
        public string Url { get; set; } // where to pull mod info from (should be pulled from Api.GetModsUrl(id, version))
        public string ProjectUrl { get; set; } 
        public ModCategory Category { get; set; } = ModCategory.None;
        public List<string> Authors { get; set; } = [];
        public List<ModFileInfo> Files { get; set; } = [];
        public List<string> InstalledFiles { get; set; } = [];
        public List<ModDependency> Dependencies { get; set; } = [];

        public ModInfo() { }

        public ModInfo(string modId, string name, string version, string url, List<ModFileInfo> files, List<string> installed)
        {
            ID = modId;
            Name = name;
            Version = version;
            Url = url;
            Files = files ?? [];
            InstalledFiles = installed ?? [];
        }

    }

}
