using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModManagerLib;

[Serializable]
[JsonSerializable(typeof(MapInfo))]
public class MapInfo
{
    public string ID { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string MetaUrl { get; set; } = string.Empty;
    public long SizeBytes { get; set; } = 0;
    public string Hash {  get; set; } = string.Empty;
    public List<string> Authors { get; set; } = [];
    public HashSet<MapCategory> Categories { get; set; } = [];

    [JsonConstructor]
    public MapInfo() { }

}
