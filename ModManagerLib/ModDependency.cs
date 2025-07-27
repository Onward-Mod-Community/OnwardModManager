using System.Text.Json.Serialization;

namespace ModManagerLib;

/// <summary>
/// Metadata that defines a mod dependency
/// </summary>
[Serializable]
[JsonSerializable(typeof(ModDependency))]
public class ModDependency
{
    public string ID { get; set; }
    public string MinimumVersion { get; set; }
    public string TargetVersion { get; set; }

    public ModDependency() { }
    public ModDependency(string id, string minimum, string target = null)
    {
        ID = id;
        MinimumVersion = minimum;
        TargetVersion = target ?? minimum;
    }
}
