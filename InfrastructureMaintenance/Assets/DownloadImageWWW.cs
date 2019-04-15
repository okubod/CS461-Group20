using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class DownloadImageWWW : MonoBehaviour
{
    string download_url = "https://www.history.com/.image/t_share/MTU3ODc5MDgyNjY5OTc1MjYz/new-york-city.jpg";

    // Use this for initialization
    void Start()
    {

    }

    public void setImage()
    {
        StartCoroutine(getTexture());
    }

    IEnumerator getTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(download_url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            GameObject rawImage = GameObject.Find("RawImage");
            rawImage.GetComponent<RawImage>().texture = myTexture;
            Debug.Log(www.downloadHandler.text);
        }
    }
}