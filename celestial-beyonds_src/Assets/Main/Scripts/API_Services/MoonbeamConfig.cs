using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// API URI loaded from StreamingAssets/moonbeam.json so builds can retarget without recompile.
public static class MoonbeamConfig
{
    private const string _configFile = "moonbeam.json";
    private static bool _loaded;

    public static string ApiUri { get; private set; }

    [System.Serializable]
    private class Payload
    {
        public string apiUri;
    }

    public static IEnumerator EnsureLoaded()
    {
        if (_loaded) yield break;

        var path = System.IO.Path.Combine(Application.streamingAssetsPath, _configFile);
        using var req = UnityWebRequest.Get(path);
        yield return req.SendWebRequest();

        var p = JsonUtility.FromJson<Payload>(req.downloadHandler.text);
        ApiUri = p.apiUri;
        _loaded = true;
    }
}
