using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// ref - https://github.com/Nrjwolf/UnityTelegramExample
public class ScarlettPhotographerBot : MonoBehaviour
{
    public Text status, photoIDTxt;
    private bool _success, _done;
    private long _code;
    private string chatID = "635500116";
    private string token = "54996";
    private string _url => $"https://api.telegram.org/bot{token}/";
    
    public void SendPhotoToGram(string path, string photoID)
    {
        _done = false;
        GetMe();
        var bytes = File.ReadAllBytes(path + photoID);
        SendMessage(GetUserInfo() + "\n\nHi there! Check out the new Photo!" + "\nPhoto ID: " + photoID);
        SendPhoto(bytes, photoID);
    }

    private string GetUserInfo()
    {
        return $"From :\n{SystemInfo.deviceName}\n{SystemInfo.deviceModel}";
    }

    private void GetMe()
    {
        var form = new WWWForm();
        var webRequest = UnityWebRequest.Post(_url + "getMe", form);
        StartCoroutine(PostRequest(webRequest));
    }

    private void SendPhoto(byte[] bytes, string filename)
    {
        var form = new WWWForm();
        form.AddField("chat_id", chatID);
        form.AddBinaryData("photo", bytes, filename, "filename");
        var webRequest = UnityWebRequest.Post(_url + "sendPhoto?", form);
        StartCoroutine(PostRequest(webRequest));
    }

    private new void SendMessage(string text)
    {
        var form = new WWWForm();
        form.AddField("chat_id", chatID);
        form.AddField("text", text);
        var webRequest = UnityWebRequest.Post(_url + "sendMessage?", form);
        StartCoroutine(PostRequest(webRequest));
    }

    private IEnumerator PostRequest(UnityWebRequest webRequest)
    {
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError
            || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            print("Connection to ScarlettAutomatonBot - Error: " + webRequest.error);
            _code = webRequest.responseCode;
            _success = false;
            _done = true;
        }
        else
        {
            print("Success!\n" + webRequest.downloadHandler.text);
            _code = webRequest.responseCode;
            _success = true;
            _done = true;
        }
    }

    private void OnGUI()
    {
        if (_done)
        {
            photoIDTxt.text = "Datetime stamp: " + GetComponent<PhotoMode>().photoID + " -> Copied to clipboard!";   
            status.text = "Success: " + _success + " -> " + _code;
        }
    }
}