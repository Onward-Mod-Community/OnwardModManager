using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModManagerLib;

public enum MapType
{
    None,
    Info,
    Content,
}

public static class Api
{
    private static readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions();

    public static T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, serializerOptions);
    public static T Deserialize<T>(byte[] json) => JsonSerializer.Deserialize<T>(json, serializerOptions);

    public static string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, serializerOptions);
    public static byte[] SerializeBytes<T>(T obj) => JsonSerializer.SerializeToUtf8Bytes(obj, serializerOptions);

    public static string GetHash(byte[] fileBytes)
    {
        var hash = SHA256.HashData(fileBytes);
        string strHash = string.Empty;
        foreach (var b in hash)
        {
            strHash += b.ToString("x2");
        }
        return strHash;
    }

    /// <summary>
    /// Decodes the map's .info file and returns the decoded json string
    /// </summary>
    /// <param name="infoFileText">The encoded .info contents</param>
    /// <returns>The decoded .info file json</returns>
    public static string ReadMapInfoJson(string infoFileText)
    {
        static string Rot13(string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int num = array[i];
                switch (num)
                {
                    case 110:
                    case 111:
                    case 112:
                    case 113:
                    case 114:
                    case 115:
                    case 116:
                    case 117:
                    case 118:
                    case 119:
                    case 120:
                    case 121:
                    case 122:
                        num -= 13;
                        break;
                    case 97:
                    case 98:
                    case 99:
                    case 100:
                    case 101:
                    case 102:
                    case 103:
                    case 104:
                    case 105:
                    case 106:
                    case 107:
                    case 108:
                    case 109:
                        num += 13;
                        break;
                    default:
                        switch (num)
                        {
                            case 78:
                            case 79:
                            case 80:
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                                num -= 13;
                                break;
                            case 65:
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                                num += 13;
                                break;
                        }
                        break;
                }
                array[i] = (char)num;
            }
            return new string(array);
        }

        if (infoFileText[0] == '\0')
        {
            string value = infoFileText.Substring(1);
            value = Rot13(value);
            infoFileText = Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
        return infoFileText;
    }

    public const string API_PREFIX = "/api/v1";

    static Api()
    {
        serializerOptions.Converters.Add(new JsonStringEnumConverter());
        serializerOptions.AllowTrailingCommas = true;
    }

    public const string GET_MODS = $"{API_PREFIX}/mods";
    public const string GET_MODS_ID = "id";
    public const string GET_MODS_VERSION = "version";
    public const string GET_MODS_FILE = "file";

    public static string GetModsUrl(string modID = null, string version = null, string file = null)
    {
        if (modID is null)
            return GET_MODS;
        if (version is null)
            return $"{GET_MODS}?{GET_MODS_ID}={modID}";
        if (file is null)
            return $"{GET_MODS}?{GET_MODS_ID}={modID}&{GET_MODS_VERSION}={version}";

        return $"{GET_MODS}?{GET_MODS_ID}={modID}&{GET_MODS_VERSION}={version}&{GET_MODS_FILE}={file}";
    }

    public const string GET_MAPS = $"{API_PREFIX}/maps";
    public const string GET_MAPS_ID = "id";
    public const string GET_MAPS_VERSION = "version";
    public const string GET_MAPS_TYPE = "type";
    public const string GET_MAPS_INFO = "info";
    public const string GET_MAPS_CONTENT = "content";

    public static string GetMapsUrl(string mapID = null, string version = null, MapType type = MapType.None)
    {
        // TODO: this
        if (mapID is null)
            return GET_MAPS;
        if (version is null)
            return $"{GET_MAPS}?{GET_MAPS_ID}={mapID}";
        if (type is MapType.None)
            return $"{GET_MAPS}?{GET_MAPS_ID}={mapID}&{GET_MAPS_VERSION}={version}";

        string strType = type switch
        {
            MapType.Info => GET_MAPS_INFO,
            MapType.Content => GET_MAPS_CONTENT,
            _ => null,
        };
        if (strType is null)
            return $"{GET_MAPS}?{GET_MAPS_ID}={mapID}&{GET_MAPS_VERSION}={version}";

        return $"{GET_MAPS}?{GET_MAPS_ID}={mapID}&{GET_MAPS_VERSION}={version}&{GET_MAPS_TYPE}={strType}";
    }
}