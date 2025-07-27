using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModManagerLib;

[Serializable]
[JsonSerializable(typeof(MapMetadata))]
public class MapMetadata
{
    public List<string> Authors { get; set; } = [];
    public HashSet<MapCategory> Categories { get; set; } = [];
}

[Serializable]
public enum MapCategory : ulong
{
    None,
    Competitive,
    Casual,
    Fun,
    Other,
    Testing, // for .local maps
}