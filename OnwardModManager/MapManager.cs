using ModManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnwardModManager
{
    public class MapManager
    {
        private static string DefaultCustomMapsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", "Downpour Interactive/Onward/CustomContent");

        private ClientApi Client;
        private string CustomMapsPath { get; set; } = DefaultCustomMapsPath;
        private string CutsomMapsTempPath => Path.Combine(CustomMapsPath, "omm-temp");

        public List<MapInfo> AllMaps { get; set; } = [];
        public Dictionary<string, MapInfo> InstalledMaps { get; set; } = [];


        public MapManager(string endpoint)
        {
            Client = new ClientApi(endpoint);
        }

        public bool Refresh()
        {
            try
            {
                AllMaps = Client.GetMaps();
            } catch { return false; }
            return true;
        }

        public bool Install(MapInfo map, IProgress<float> progress = null)
        {
            if (InstalledMaps.ContainsKey(map.ID))
                return false; // Already installed

            if (!Directory.Exists(CutsomMapsTempPath))
                Directory.CreateDirectory(CutsomMapsTempPath);

            var files = Client.GetMapFiles(map, CutsomMapsTempPath, progress);
            if (files is null)
                return false;

            foreach (var file in files)
            {
                File.Move(file, Path.Combine(CustomMapsPath, Path.GetFileName(file))); // remove from temp folder
            }

            if (Directory.GetFiles(CutsomMapsTempPath).Count() == 0)
                Directory.Delete(CutsomMapsTempPath, true);

            InstalledMaps.Add(map.ID, map);
            return true;
        }

        public bool Update(string mapid, IProgress<float> progress = null)
        {
            if (!InstalledMaps.ContainsKey(mapid))
                return false;
            try
            {
                var current = InstalledMaps[mapid];
                var latest = GetLatestVersion(mapid);
                if (latest.Version == current.Version)
                    return false;

                if (!Uninstall(current))
                    return false;

                return Install(latest, progress);
            }
            catch
            {
                return false;
            }
        }

        public bool Uninstall(MapInfo map)
        {
            if (!InstalledMaps.ContainsKey(map.ID))
                return false; // Not installed

            var installed = InstalledMaps[map.ID];
            if (installed.Version != map.Version)
                return false; // Not correct version

            try
            {
                File.Delete(Path.Combine(CustomMapsPath, $"{map.ID}.content"));
                File.Delete(Path.Combine(CustomMapsPath, $"{map.ID}.info"));
            }
            catch
            {
                return false;
            }

            InstalledMaps.Remove(map.ID);
            return true;
        }

        public void FindInstalledMaps()
        {
            var customMaps = Directory.GetFiles(CustomMapsPath, "*.content");
            foreach (var mapPath in customMaps)
            {
                try
                {
                    var mapId = Path.GetFileNameWithoutExtension(mapPath);
                    if (InstalledMaps.ContainsKey(mapId))
                        continue; // Already added

                    if (mapId.EndsWith(".local")) // Testing map
                    {
                        InstalledMaps.Add(mapId, new MapInfo
                        {
                            Name = mapId,
                            ID = mapId,
                            SizeBytes = new FileInfo(mapPath).Length,
                            Version = "0.0.0",
                            Categories = [MapCategory.Testing],
                            Hash = string.Empty
                        });
                        continue;
                    }

                    var infoFilePath = Path.Combine(CustomMapsPath, $"{mapId}.info");
                    if (File.Exists(infoFilePath)) // Valid map
                    {
                        MapInfo map = null;

                        // Parse metadata from .info file
                        var root = JsonDocument.Parse(Api.ReadMapInfoJson(File.ReadAllText(infoFilePath))).RootElement;
                        var title = root.GetProperty("Title").GetString() ?? string.Empty;
                        var desc = root.GetProperty("Description").GetString() ?? string.Empty;
                        var displayProps = root.GetProperty("DisplayProperties");
                        var hash = displayProps.GetProperty("hash").GetString() ?? string.Empty;
                        var thumbnail = root.GetProperty("ThumbnailUrl").GetString() ?? string.Empty;

                        var search = from m in AllMaps where m.ID == mapId select m; // Find all versions of this map
                        if (search.Any())
                        {
                            var final = from m in search where m.Hash == hash select m;
                            if (final.Any()) // Found the correct version
                            {
                                InstalledMaps.Add(mapId, final.First());
                                continue;
                            }

                            // We can still get some metadata from the other versions here
                            MapInfo latest = search.First();
                            foreach (var result in search)
                            {
                                if (Version.Parse(result.Version) > Version.Parse(latest.Version))
                                    latest = result;
                            }

                            // Try our best to make the most accurate map info from what we have
                            map = new MapInfo
                            {
                                ID = mapId,
                                Description = desc,
                                Hash = hash,
                                Name = title,
                                SizeBytes = new FileInfo(mapPath)?.Length ?? 0,
                                Version = "0.0.0", // We dont know so we should probably set it to the lowest
                                Authors = latest?.Authors ?? [],
                                Categories = latest?.Categories ?? [],
                                ThumbnailUrl = latest?.ThumbnailUrl ?? thumbnail
                            };

                            InstalledMaps.Add(mapId, map);
                            continue;
                        }

                        // The server doesn't have this custom map
                        map = new MapInfo
                        {
                            ID = mapId,
                            Description = desc,
                            Hash = hash,
                            Name = title,
                            SizeBytes = new FileInfo(mapPath)?.Length ?? 0,
                            Version = "0.0.0", // We dont know so we should probably set it to the lowest
                            ThumbnailUrl = thumbnail
                        };

                        InstalledMaps.Add(mapId, map);
                    }
                }
                catch { }
            }
        }

        public MapInfo GetLatestVersion(string id)
        {
            var maps = (from map in AllMaps
                        where map.ID == id
                        select map);

            MapInfo latest = null;
            foreach (var mod in maps)
            {
                if (latest is null)
                {
                    latest = mod;
                    continue;
                }
                if (Version.Parse(mod.Version) > Version.Parse(latest.Version))
                {
                    latest = mod;
                }
            }
            if (latest is null)
            {
                if (InstalledMaps.ContainsKey(id))
                    latest = InstalledMaps[id];
            }
            return latest;
        }
    }
}
