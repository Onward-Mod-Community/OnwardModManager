using ModManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnwardModManager
{
    public class ModManager
    {
        private string OnwardPath { get; set; } = string.Empty;
        private ClientApi Client { get; set; }

        /// <summary>
        /// List of all mods pulled from the server
        /// </summary>
        public List<ModInfo> AllMods { get; set; } = [];

        /// <summary>
        /// Dictionary that links an installed mod's ID to the instance of the installed ModInfo
        /// </summary>
        public Dictionary<string, ModInfo> InstalledMods { get; set; } = [];

        public ModManager(string host)
        {
            Client = new ClientApi(host);
        }

        /// <summary>
        /// Pulls mod list from server
        /// </summary>
        public bool Refresh()
        {
            try
            {
                AllMods = Client.GetMods();
            } catch { return false; }
            return true;
        }

        /// <summary>
        /// Sets the list of installed mods
        /// </summary>
        /// <param name="mods"></param>
        public void SetInstalledMods(Dictionary<string, ModInfo> mods)
        {
            InstalledMods = mods;
        }

        /// <summary>
        /// Tries to automatically find the onward install location
        /// </summary>
        /// <returns></returns>
        public string FindOnwardFolder()
        {
            string defaultInstall = "C:/Program Files (x86)/Steam/steamapps/common/Onward";
            if (Directory.Exists(defaultInstall))
                return defaultInstall; // That was easy

            // TODO: try parsing steam library folders and search in all steam libraries
            return string.Empty;
        }

        /// <summary>
        /// Sets the onward install location
        /// </summary>
        /// <param name="path"></param>
        public void SetOnwardFolder(string path)
        {
            OnwardPath = path;
        }

        /// <summary>
        /// Installs a mod to the onward install location
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool Install(ModInfo mod)
        {
            var files = Client.GetModFiles(mod);
            foreach (var file in files)
            {
                try
                {
                    if (file.Value is null)
                        continue;

                    string path = Path.Combine(OnwardPath, file.Key);
                    string directory = Path.GetDirectoryName(path);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    if (File.Exists(path))
                        File.Delete(path);

                    if (!mod.InstalledFiles.Contains(file.Key))
                        mod.InstalledFiles.Add(file.Key);

                    File.WriteAllBytes(path, file.Value);
                }
                catch
                {
                    Uninstall(mod);
                    return false;
                }
            }
            InstalledMods.Add(mod.ID, mod);
            return true;
        }

        /// <summary>
        /// Updates a pre-existing mod by removing it and installing the updated files
        /// </summary>
        /// <param name="modid"></param>
        /// <returns></returns>
        public bool Update(string modid)
        {
            if (!InstalledMods.ContainsKey(modid))
                return false; // Not installed

            var installed = InstalledMods[modid];
            var latest = GetLatestVersion(modid);
            if (Version.Parse(latest.Version) > Version.Parse(installed.Version))
            {
                if (!Uninstall(installed))
                    return false; // Couldn't remove old version
                return Install(latest);
            }

            return false; // Already latest or greater version
        }

        /// <summary>
        /// Uninstalls a mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool Uninstall(ModInfo mod)
        {
            if (!InstalledMods.ContainsKey(mod.ID))
                return false; // Not installed

            var installed = InstalledMods[mod.ID];
            if (installed.Version != mod.Version)
                return false; // Not the same version

            foreach (var file in mod.InstalledFiles)
            {
                try
                {
                    string path = Path.Combine(OnwardPath, file);
                    if (!File.Exists(path))
                        continue;

                    File.Delete(path);
                }
                catch
                {
                    return false;
                }
            }
            mod.InstalledFiles.Clear();
            InstalledMods.Remove(mod.ID);
            return true;
        }

        /// <summary>
        /// Finds the latest version of a mod
        /// </summary>
        /// <param name="modid"></param>
        /// <returns></returns>
        public ModInfo GetLatestVersion(string modid)
        {
            var mods = (from mod in AllMods
                       where mod.ID == modid
                       select mod);

            ModInfo latest = null;
            foreach (var mod in mods)
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
            return latest;
        }

        /// <summary>
        /// Makes sure a mod's files are all properly installed
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool VerifyInstalledMod(ModInfo mod)
        {
            if (string.IsNullOrWhiteSpace(OnwardPath))
                return false;

            foreach (var file in mod.InstalledFiles)
            {
                if (!File.Exists(Path.Combine(OnwardPath, file)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Tries to find and parse installed mods (this should be called after <seealso cref="Refresh"/>)
        /// </summary>
        public void FindInstalledMods()
        {
            if (AllMods.Count == 0) return; // Dont have mods anyway

            var pluginsPath = Path.Combine(OnwardPath, "BepInEx/plugins");
            var bepinexPath = Path.Combine(OnwardPath, "BepInEx/core/BepInEx.dll");

            // Try to get BepInEx version
            if (File.Exists(bepinexPath) && !InstalledMods.ContainsKey("BepInEx"))
            {
                var hash = Api.GetHash(File.ReadAllBytes(bepinexPath));
                var bepinex = AllMods.FirstOrDefault(m => m.ID == "BepInEx" && m.Files.Any(f => f.Hash == hash));
                if (bepinex is not null)
                {
                    InstalledMods.Add(bepinex.ID, bepinex);
                }
                else
                {
                    var assembly = Assembly.Load(File.ReadAllBytes(bepinexPath)).GetName();
                    bepinex = AllMods.FirstOrDefault(m => m.ID == "BepInEx" && Version.Parse(m.Version) == assembly.Version);
                    if (bepinex is not null)
                    {
                        // Found it but the files may be corrupt?
                        InstalledMods.Add(bepinex.ID, bepinex);
                    }
                    else
                    {
                        // IDK at this point, could add a fake entry but im not sure
                    }
                }
            }

            var allFiles = Directory.GetFiles(pluginsPath, "*.dll", SearchOption.AllDirectories);
            foreach (var file in allFiles)
            {
                // Try to load the assembly
                try
                {
                    var fileHash = Api.GetHash(File.ReadAllBytes(file));
                    var foundMod = AllMods.FirstOrDefault(m => m.Files.Any(f => f.Hash == fileHash));
                    if (foundMod is not null && !InstalledMods.ContainsKey(foundMod.ID))
                    {
                        foreach (var f in foundMod.Files)
                        {
                            if (File.Exists(Path.Combine(OnwardPath, f.Path)))
                            {
                                foundMod.InstalledFiles.Add(f.Path);
                            }
                        }
                        InstalledMods.Add(foundMod.ID, foundMod);
                    }
                }
                catch {  }
            }
            
        }

    }
}
