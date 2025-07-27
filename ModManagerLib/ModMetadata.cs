using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ModManagerLib;

[Serializable]
[JsonSerializable(typeof(ModMetadata))]
public class ModMetadata // for the server's meta.json file
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProjectUrl { get; set; }
    public ModCategory Category { get; set; }
    public List<string> Authors { get; set; } = [];
    public Dictionary<string, List<ModDependency>> Dependencies { get; set; } = []; // Mod Version : Dependencies
}

[Serializable]
public enum ModCategory
{
    None = 0,
    Core,       // EG: BepInEx, OnwardModFramework
    Gameplay,   // Mods that change gameplay
    Cosmetic,   // Mods that mainly make visual changes, typically client side
    Tweaks,     // QOL improvment focused mods
    Library,    // EG: Protobuf.NET, Newtonsoft.Json. Usually not actual plugins or modloaders. these should only be under BepInEx/plugins/libs
    Other,
}