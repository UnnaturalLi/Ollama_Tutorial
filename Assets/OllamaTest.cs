using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine.UI;

[System.Serializable]
public class OllamaRequest{
    public string model;
    public string prompt;
}
[System.Serializable]
public class OllamaResponse
{
    public string id;
    public string @object;
    public long created;
    public string model;
    public string system_fingerprint;
    public OllamaChoice[] choices;
    public OllamaUsage usage;
}
[System.Serializable]
public class OllamaChoice
{
    public string text;
    public int index;
    public string finish_reason;
}
[System.Serializable]
public class OllamaUsage
{
    public int prompt_tokens;
    public int completion_tokens;
    public int total_tokens;
}
public class OllamaTest : MonoBehaviour
{
    public Text responseTextBox;
    public InputField inputField;

    public void SendMessageToOllama()
    {
        StartCoroutine(Send());
    }
    public IEnumerator Send()
    {
        OllamaRequest request = new OllamaRequest();
        request.model = "llama3.2:latest";
        request.prompt=inputField.text;
        inputField.text = "";
        string jsonToSend = JsonUtility.ToJson(request);
        using (UnityWebRequest webRequest =
               new UnityWebRequest(
                   "http://localhost:11434/v1/completions", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonToSend);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseJson = webRequest.downloadHandler.text;
                OllamaResponse response =
                    JsonUtility
                        .FromJson<OllamaResponse>(responseJson);
                responseTextBox.text = response.choices[0].text;
            }
        }
    }
}
