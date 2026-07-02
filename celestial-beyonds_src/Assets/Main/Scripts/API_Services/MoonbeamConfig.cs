using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class MoonbeamConfig
{
    private const string ConfigFile = "moonbeam.json";
    private static bool _loaded;

    public static string ApiUri { get; private set; }

    [Serializable]
    private class Payload
    {
        public string apiUri;
    }

    public static IEnumerator EnsureLoaded()
    {
        if (_loaded) yield break;

        var path = Path.Combine(Application.streamingAssetsPath, ConfigFile);

        if (!path.Contains("://"))
            path = "file://" + path;

        using var req = UnityWebRequest.Get(path);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"MoonbeamConfig: failed to load {ConfigFile} — {req.error}");
            yield break;
        }

        var payload = JsonUtility.FromJson<Payload>(req.downloadHandler.text);
        if (payload == null || string.IsNullOrEmpty(payload.apiUri))
        {
            Debug.LogError($"MoonbeamConfig: {ConfigFile} missing or invalid apiUri");
            yield break;
        }

        ApiUri = payload.apiUri;
        _loaded = true;
    }
}
