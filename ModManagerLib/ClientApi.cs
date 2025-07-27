using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModManagerLib
{
    public class ClientApi
    {
        private string Host { get; }

        public ClientApi(string host)
        {
            Host = host;
        }

        #region Mods

        /// <summary>
        /// Pulls full mod list from the server
        /// </summary>
        /// <returns></returns>
        public List<ModInfo> GetMods()
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            var response = client.GetAsync(Api.GetModsUrl()).Result;
            response.EnsureSuccessStatusCode();

            string jsonString = response.Content.ReadAsStringAsync().Result;
            return Api.Deserialize<List<ModInfo>>(jsonString);
        }

        /// <summary>
        /// Pulls all versions of a mod from the server
        /// </summary>
        /// <param name="modID">The ID of the mod to get</param>
        /// <returns></returns>
        public List<ModInfo> GetMods(string modID)
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            var response = client.GetAsync(Api.GetModsUrl(modID)).Result;
            response.EnsureSuccessStatusCode();

            string jsonString = response.Content.ReadAsStringAsync().Result;
            return Api.Deserialize<List<ModInfo>>(jsonString);
        }

        /// <summary>
        /// Pulls the mod with the specified version from the server
        /// </summary>
        /// <param name="modID">The ID of the mod to get</param>
        /// <param name="version">The version of the mod to get</param>
        /// <returns></returns>
        public ModInfo GetMod(string modID, string version)
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            var response = client.GetAsync(Api.GetModsUrl(modID, version)).Result;
            response.EnsureSuccessStatusCode();

            string jsonString = response.Content.ReadAsStringAsync().Result;
            return Api.Deserialize<ModInfo>(jsonString);
        }

        /// <summary>
        /// Downloads all mod files from the server
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public Dictionary<string, byte[]> GetModFiles(ModInfo mod)
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            Dictionary<string, byte[]> files = [];

            foreach (var file in mod.Files)
            {
                var response = client.GetAsync(Api.GetModsUrl(mod.ID, mod.Version, file.Path)).Result;
                if (!response.IsSuccessStatusCode)
                {
                    files.Add(file.Path, null);
                    continue;
                }
                byte[] fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                files[file.Path] = fileBytes ?? [];
            }

            return files;
        }

        #endregion

        #region Maps

        /// <summary>
        /// Pulls full map list from the server
        /// </summary>
        /// <returns></returns>
        public List<MapInfo> GetMaps()
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            var response = client.GetAsync(Api.GetMapsUrl()).Result;
            response.EnsureSuccessStatusCode();

            string jsonString = response.Content.ReadAsStringAsync().Result;
            return Api.Deserialize<List<MapInfo>>(jsonString);
        }

        /// <summary>
        /// Pulls all versions of a map from the server
        /// </summary>
        /// <param name="mapId">The ID of the map to get</param>
        /// <returns></returns>
        public List<MapInfo> GetMaps(string mapId)
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            var response = client.GetAsync(Api.GetMapsUrl(mapId)).Result;
            response.EnsureSuccessStatusCode();

            string jsonString = response.Content.ReadAsStringAsync().Result;
            return Api.Deserialize<List<MapInfo>>(jsonString);
        }

        /// <summary>
        /// Pulls the map info with the specified version from the server
        /// </summary>
        /// <param name="mapId">The ID of the map to get</param>
        /// <param name="version">The version of the map to get</param>
        /// <returns></returns>
        public MapInfo GetMap(string mapId, string version)
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            var response = client.GetAsync(Api.GetMapsUrl(mapId, version)).Result;
            response.EnsureSuccessStatusCode();

            string jsonString = response.Content.ReadAsStringAsync().Result;
            return Api.Deserialize<MapInfo>(jsonString);
        }

        /// <summary>
        /// Downloads all map files from the server to the specified directory
        /// </summary>
        /// <param name="map"></param>
        /// <param name="folderPath"></param>
        /// <param name="progress">Progress updater</param>
        /// <returns></returns>
        public List<string> GetMapFiles(MapInfo map, string folderPath, IProgress<float> progress = null)
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"https://{Host}")
            };

            List<string> files = [];
            string infoPath = Path.Combine(folderPath, $"{map.ID}.info");
            string contentPath = Path.Combine(folderPath, $"{map.ID}.content");

            var response = client.GetAsync(Api.GetMapsUrl(map.ID, map.Version, MapType.Info));
            response.Result.EnsureSuccessStatusCode();
            File.WriteAllBytes(infoPath, response.Result.Content.ReadAsByteArrayAsync().Result);
            files.Add(infoPath);
            
            using var fs = File.OpenWrite(contentPath);
            client.DownloadAsync(Api.GetMapsUrl(map.ID, map.Version, MapType.Content), fs, progress).GetAwaiter().GetResult();
            files.Add(contentPath);

            return files;
        }

        #endregion

    }

    internal static class HttpClientExtensions
    {
        public static async Task DownloadAsync(this HttpClient client, string requestUri, Stream destination, IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            // Get the http headers first to examine the content length
            using (var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var contentLength = response.Content.Headers.ContentLength;

                using (var download = await response.Content.ReadAsStreamAsync(cancellationToken))
                {
                    // Ignore progress reporting when no progress reporter was 
                    // passed or when the content length is unknown
                    if (progress == null || !contentLength.HasValue)
                    {
                        await download.CopyToAsync(destination);
                        return;
                    }

                    // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
                    var relativeProgress = new Progress<long>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));
                    // Use extension method to report progress while downloading
                    await download.CopyToAsync(destination, 81920, relativeProgress, cancellationToken);
                    progress.Report(1);
                }
            }
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            }
            buffer = null;
        }
    }
}
