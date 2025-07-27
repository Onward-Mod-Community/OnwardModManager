using ModManagerLib;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ModManagerServer
{
    internal class Server : IDisposable
    {
        private string ServerRoot { get; }
        private int ListenPort { get; }

        private List<ModInfo> Mods { get; } = [];
        private List<MapInfo> Maps { get; } = [];

        private byte[] CachedFullModsJson = null;
        private byte[] CachedFullMapsJson = null;

        private HttpListener Listener;
        private Thread ListeningThread;
        private string BasePath => $"http://*:{ListenPort}/";

        public Server(int port, string rootPath)
        {
            ListenPort = port;
            ServerRoot = rootPath;
            Listener = new HttpListener();
            Listener.Prefixes.Add(BasePath);
        }

        public void Start()
        {
            Log($"Starting... Listen port: {ListenPort}, Path: {ServerRoot}");
            Listener.Start();
            ListeningThread = new Thread(() => ListenThread().GetAwaiter().GetResult());
            ListeningThread.Start();
        }

        public void Stop()
        {
            try
            {
                Listener?.Stop();
                ListeningThread?.Interrupt();
                ListeningThread = null;
            }
            finally { }
        }

        public void Refresh()
        {
            Mods.Clear();
            Maps.Clear();

            string modsPath = Path.Combine(ServerRoot, "mods");
            var allMods = Directory.GetDirectories(modsPath);
            foreach (var modDir in allMods)
            {
                Mods.AddRange(ParseModFromFilesystem(modDir));
            }

            CachedFullModsJson = Api.SerializeBytes(Mods);
            Log($"Finished refreshing mod list, total mods: {Mods.Count}");

            string mapsPath = Path.Combine(ServerRoot, "maps");
            var allMaps = Directory.GetDirectories(mapsPath);
            foreach (var mapDir in allMaps)
            {
                Maps.AddRange(ParseMapFromFilesystem(mapDir));
            }

            CachedFullMapsJson = Api.SerializeBytes(Maps);
            Log($"Finished refreshing map list, total maps: {Maps.Count}");
        }

        private async Task ListenThread()
        {
            Log("Started listening...");
            while (Listener is not null && Listener.IsListening)
            {
                var context = await Listener.GetContextAsync();
                Task.Run(() =>
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    try
                    {
                        HandleContext(context);
                    }
                    catch (Exception ex)
                    {
                        Log("Error handling context: " + ex);
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }
                    finally
                    {
                        context?.Response?.Close();
                    }
                    stopwatch.Stop();
                    Log($"Api response time: {stopwatch.Elapsed.TotalMilliseconds}ms");

                }).GetAwaiter();
            }
            Log("Stopped listening");
        }

        private void HandleContext(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            switch (request.HttpMethod.ToUpper())
            {
                case "GET":
                    if (request.RawUrl.StartsWith(Api.GET_MODS))
                    {
                        GetMods(context, request, response);
                        return;
                    }
                    if (request.RawUrl.StartsWith(Api.GET_MAPS))
                    {
                        GetMaps(context, request, response);
                        return;
                    }

                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;

                default:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
            }

        }

        private void GetMods(HttpListenerContext context, HttpListenerRequest request, HttpListenerResponse response)
        {
            Log($"[MODS] Raw URL: {request.RawUrl}");

            string requestedModId = request.QueryString[Api.GET_MODS_ID];
            string requestedVersion = request.QueryString[Api.GET_MODS_VERSION];
            string requestedFile = request.QueryString[Api.GET_MODS_FILE];

            bool hasModId = !string.IsNullOrWhiteSpace(requestedModId),
                hasVersion = !string.IsNullOrWhiteSpace(requestedVersion),
                hasFile = !string.IsNullOrWhiteSpace(requestedFile);

            // Get all mods
            if (request.RawUrl.TrimEnd('/') == Api.GET_MODS && !hasModId)
            {
                response.ContentType = "application/json";
                response.OutputStream.Write(CachedFullModsJson);
                return;
            }

            Log($"[MODS] Mod ID: {requestedModId}");

            // All versions of requested mod
            if (hasModId && !hasVersion)
            {
                var requestedMods = Mods.Where((m) => m.ID == requestedModId).ToList();
                response.ContentType = "application/json";
                response.OutputStream.Write(JsonSerializer.SerializeToUtf8Bytes(requestedMods));
                return;
            }

            Log($"[MODS] Mod version: {requestedVersion}");

            // Requested mod version
            if (hasModId && hasVersion && !hasFile)
            {
                var requestedMod = Mods.Where((m) => m.ID == requestedModId && m.Version == requestedVersion).FirstOrDefault();
                if (requestedMod is default(ModInfo))
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }

                response.ContentType = "application/json";
                response.OutputStream.Write(JsonSerializer.SerializeToUtf8Bytes(requestedMod));
                return;
            }

            // Get file
            if (hasModId && hasVersion && hasFile)
            {
                string realPath = Path.Combine(ServerRoot, "mods", requestedModId, requestedVersion, requestedFile);
                Log($"[MODS] File: {requestedFile}, Server path: {realPath}");
                if (File.Exists(realPath))
                {
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", $"attachment; filename=\"{Path.GetFileName(realPath)}\"");
                    response.OutputStream.Write((File.ReadAllBytes(realPath)));
                    return;
                }
            }

            // Fallback
            response.StatusCode = (int)HttpStatusCode.NotFound;
        }

        private void GetMaps(HttpListenerContext context, HttpListenerRequest request, HttpListenerResponse response)
        {
            string requestedMapId = request.QueryString[Api.GET_MAPS_ID];
            string requestedVersion = request.QueryString[Api.GET_MAPS_VERSION];
            string requestedType = request.QueryString[Api.GET_MAPS_TYPE];

            bool hasMapId = !string.IsNullOrWhiteSpace(requestedMapId),
                hasVersion = !string.IsNullOrWhiteSpace(requestedVersion),
                hasType = !string.IsNullOrWhiteSpace(requestedType);

            Log($"[MAPS] Raw URL: {request.RawUrl}");

            // Get all maps
            if (request.RawUrl.TrimEnd('/') == Api.GET_MAPS && !hasMapId)
            {
                response.ContentType = "application/json";
                response.OutputStream.Write(CachedFullMapsJson);
                return;
            }

            Log($"[MAPS] Map ID: {requestedMapId}");

            // Get all versions of a map
            if (hasMapId && !hasVersion)
            {
                response.ContentType = "application/json";
                response.OutputStream.Write(Api.SerializeBytes(Maps.Where(m => m.ID == requestedMapId).ToList()));
                return;
            }

            Log($"[MAPS] Map version: {requestedVersion}");

            // Get map metadata for version
            if (hasMapId && hasVersion && !hasType)
            {
                var map = Maps.FirstOrDefault(m => m.ID == requestedMapId && m.Version == requestedVersion);
                if (map is null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound; // map with version not found
                    return;
                }
                response.ContentType = "application/json";
                response.OutputStream.Write(Api.SerializeBytes(map));
                return;
            }

            if (hasMapId && hasVersion && hasType)
            {
                List<string> supportedTypes = [Api.GET_MAPS_INFO, Api.GET_MAPS_CONTENT];
                if (!supportedTypes.Contains(requestedType)) // 
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest; // Not asking for a valid file type
                    return;
                }

                string filePath = Path.Combine(ServerRoot, "maps", requestedMapId, requestedVersion, $"{requestedMapId}.{requestedType}");
                Log($"[MAPS] Type: {requestedType}, Server path: {filePath}");

                if (File.Exists(filePath))
                {
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", $"attachment; filename=\"{Path.GetFileName(filePath)}\"");
                    using (var fs = File.OpenRead(filePath))
                    {
                        response.AddHeader("Content-Length", fs.Length.ToString());
                        using var bs = new BufferedStream(fs, 50_000_000); // 50MB buffer
                        bs.CopyTo(response.OutputStream);
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    return;
                }
            }

            response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        private List<ModInfo> ParseModFromFilesystem(string path)
        {
            List<ModInfo> mods = [];
            string modId = Path.GetFileName(path);
            ModMetadata metadata = null;

            if (File.Exists(Path.Combine(path, "meta.json")))
            {
                try
                {
                    metadata = Api.Deserialize<ModMetadata>(File.ReadAllText(Path.Combine(path, "meta.json")));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to parse metadata for mod {modId}: " + ex);
                }
            }

            var modVersions = Directory.GetDirectories(path);
            foreach (var modVersion in modVersions)
            {
                string version = Path.GetFileName(modVersion);
                List<ModFileInfo> allFiles = GetModFiles(modVersion);
                List<ModDependency> dependencies = [];
                metadata?.Dependencies.TryGetValue(version, out dependencies);

                var mod = new ModInfo(modId, metadata?.Name ?? modId, version, Api.GetModsUrl(modId, version), allFiles, [])
                {
                    Authors = metadata?.Authors ?? [],
                    ProjectUrl = metadata?.ProjectUrl,
                    Description = metadata?.Description,
                    Dependencies = dependencies ?? [],
                    Category = metadata?.Category ?? ModCategory.None,
                };
                mods.Add(mod);
            }

            return mods;
        }

        private List<MapInfo> ParseMapFromFilesystem(string path)
        {
            List<MapInfo> maps = [];
            MapMetadata mapMeta = null;
            string mapid = Path.GetFileName(path);

            string metaPath = Path.Combine(path, "meta.json");
            if (File.Exists(metaPath))
                mapMeta = Api.Deserialize<MapMetadata>(File.ReadAllText(metaPath));

            var mapVersions = Directory.GetDirectories(path);
            foreach (var mapVersionPath in mapVersions)
            {
                string version = Path.GetFileName(mapVersionPath);
                string infoFilePath = Path.Combine(mapVersionPath, $"{mapid}.info");
                string infoContents = Api.ReadMapInfoJson(File.ReadAllText(infoFilePath));

                var root = JsonDocument.Parse(infoContents).RootElement;
                string title = root.GetProperty("Title").GetString();
                string desc = root.GetProperty("Description").GetString();
                string hash = root.GetProperty("DisplayProperties").GetProperty("hash").GetString();
                string thumbnail = root.GetProperty("ThumbnailUrl").GetString();

                var map = new MapInfo
                {
                    ID = mapid,
                    Name = title ?? string.Empty,
                    Description = desc ?? string.Empty,
                    MetaUrl = Api.GetMapsUrl(mapid, version, MapType.None),
                    SizeBytes = new FileInfo(Path.Combine(mapVersionPath, $"{mapid}.content"))?.Length ?? 0, // could read DisplayProperties.map_filesize too
                    Version = version,
                    Authors = mapMeta?.Authors ?? [],
                    Categories = mapMeta?.Categories ?? [MapCategory.None],
                    Hash = hash,
                    ThumbnailUrl = thumbnail
                };

                maps.Add(map);
            }

            return maps;
        }

        public void Dispose()
        {
            Listener?.Stop();
            Listener?.Close();
            ListeningThread?.Interrupt();
            ListeningThread = null;
            Listener = null;
        }

        private static List<ModFileInfo> GetModFiles(string directoryPath)
        {
            List<ModFileInfo> files = [];
            var dirInfo = new DirectoryInfo(directoryPath);

            foreach (var file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                var fileBytes = File.ReadAllBytes(file.FullName);
                files.Add(new ModFileInfo
                {
                    Path = Path.GetRelativePath(directoryPath, file.FullName),
                    Hash = Api.GetHash(fileBytes),
                    Size = fileBytes.LongLength,
                });
            }

            return files;
        }

        private static void Log(object log)
        {
            Console.WriteLine($"[{DateTime.Now}] [SERVER] {log}");
        }
    }
}
