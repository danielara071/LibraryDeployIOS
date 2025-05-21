using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class BookController : MonoBehaviour
{

    static public BookController Instance;
    public SelectCover SelectCover;
    public Text nameText;
    public Text authorText;
    public SelectBook SelectBook;
    public int BookSelection;

    private void Awake()
    {
        Instance = this;
        Instance.SetReferences();
        DontDestroyOnLoad(this.gameObject);
    }
    void SetReferences()
    {
        if (SelectCover == null)
        {
            SelectCover = FindFirstObjectByType<SelectCover>();
        }
        if (SelectBook == null)
        {
            SelectBook = FindFirstObjectByType<SelectBook>();
        }
    }
    public void Select(int _selection)
    {
        BookSelection = _selection;
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
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
            LoadBookInfo(BookSelection, bookList);
            PlayerPrefs.SetInt("book_no", BookSelection);
        }
    }
    public void LoadBookInfo(int idBook, List<Libro> bookList)
    {
        string book = bookList[idBook - 1].titulo;
        PlayerPrefs.SetString("book_name", book);
        nameText.text = book;

        string author = bookList[idBook - 1].autor;
        int id = bookList[idBook - 1].idLibro;
        PlayerPrefs.SetString("autor", author);
        PlayerPrefs.SetInt("idLib", id);
        authorText.text = author;
    }
}
