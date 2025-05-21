using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;


public class SelectCover : MonoBehaviour
{
    Texture2D image;
    Sprite newSprite;
    public Image newImage;
    public LibroNumber booknumber = LibroNumber.book1;
    

    
    public enum LibroNumber
    {
        book1, book2, book3, book4, book5, book6
    }

    IEnumerator DownloadImage(string MediaURL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaURL);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            image = ((DownloadHandlerTexture)request.downloadHandler).texture;
            newSprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
            newImage.sprite = newSprite;
        }
    }

    IEnumerator Start()
    {
        string curr = PlayerPrefs.GetString("ip");
        string JSONurl = $"https://{curr}:7291/DanielLara/GetAllLibros";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log("Error API: " + web.error);

        }
        else
        {
            List<Libro> bookList = new List<Libro>();
            bookList = JsonConvert.DeserializeObject<List<Libro>>(web.downloadHandler.text);
            string cover = bookList[(int)booknumber].imagen_url;
            StartCoroutine(DownloadImage(cover));
        }
    }
    
    
}
