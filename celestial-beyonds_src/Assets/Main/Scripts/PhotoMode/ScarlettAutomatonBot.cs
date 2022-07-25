using System.IO;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// ref - https://github.com/Nrjwolf/UnityTelegramExample
public class ScarlettAutomatonBot : MonoBehaviour
{
    public string chatID = "635500116"; // ID (you can know your id via @userinfobot)
    public string token = "5499658523:AAFlhX5FwSuBG7nYPFUelUNvUA03Yw6kX_4"; // bot token (@BotFather)
    private string _url => $"https://api.telegram.org/bot{token}/";


    public void SendPhotoToGram(string path, string photoID)
    {
        GetMe();
        var bytes = File.ReadAllBytes(path + photoID);
        SendMessage(GetUserInfo() + "\n\nHi there! Check out the new Photo!" + "\nPhoto ID: \n" + photoID);
        SendPhoto(bytes, photoID);
    }

    private string GetUserInfo()
    {
        return $"From :\n{SystemInfo.deviceName}\n{SystemInfo.deviceModel}";
    }

    private void GetMe()
    {
        var form = new WWWForm();
        var www = UnityWebRequest.Post(_url + "getMe", form);
        StartCoroutine(SendRequest(www));
    }

    private void SendPhoto(byte[] bytes, string filename)
    {
        var form = new WWWForm();
        form.AddField("chat_id", chatID);
        form.AddBinaryData("photo", bytes, filename, "filename");
        var www = UnityWebRequest.Post(_url + "sendPhoto?", form);
        StartCoroutine(SendRequest(www));
    }

    private new void SendMessage(string text)
    {
        var form = new WWWForm();
        form.AddField("chat_id", chatID);
        form.AddField("text", text);
        var www = UnityWebRequest.Post(_url + "sendMessage?", form);
        StartCoroutine(SendRequest(www));
    }

    private IEnumerator SendRequest(UnityWebRequest webRequest)
    {
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
            print(webRequest.error);
        else
            print("Success!\n" + webRequest.downloadHandler.text);
    }
}