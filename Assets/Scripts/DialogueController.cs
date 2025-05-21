using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
    public Text DialogueText;
    public Text PageText;
    public float DialogueSpeed = 0.05f;
    public float DelayToWrite = 0.5f;
    
    private List<Paginas> pageList = new List<Paginas>();
    private int currentPageIndex = 0;

    void Start()
    {
        StartCoroutine(GetPagesData());
    }

    IEnumerator GetPagesData()
    {
        int bookId = PlayerPrefs.GetInt("idLib");
        string apiUrl = $"https://10.22.239.19:7291/DanielLara/GetPaginas/{bookId}";
        
        UnityWebRequest web = UnityWebRequest.Get(apiUrl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();
        
        if (web.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log("Error API: " + web.error);
            SetFallbackData();
        }
        else
        {
            pageList = JsonConvert.DeserializeObject<List<Paginas>>(web.downloadHandler.text);
            
            if (pageList != null && pageList.Count > 0)
            {
                // Sort pages by page number if needed
                pageList.Sort((x, y) => x.numeroPagina.CompareTo(y.numeroPagina));
                StartCoroutine(DisplayCurrentPage());
            }
            else
            {
                SetFallbackData();
            }
        }
    }

    IEnumerator DisplayCurrentPage()
    {
        if (currentPageIndex >= 0 && currentPageIndex < pageList.Count)
        {
            var currentPage = pageList[currentPageIndex];
            
            // Update page number display
            PageText.text = currentPage.numeroPagina.ToString();
            yield return new WaitForSeconds(DelayToWrite);
            
            // Animated text display
            DialogueText.text = "";
            foreach (char c in currentPage.texto.ToCharArray())
            {
                DialogueText.text += c;
                yield return new WaitForSeconds(DialogueSpeed);
            }
        }
    }

    void SetFallbackData()
    {
        pageList = new List<Paginas>
        {
            new Paginas { idPagina = 1, numeroPagina = 1, texto = "Sample page 1 content" },
            new Paginas { idPagina = 2, numeroPagina = 2, texto = "Sample page 2 content" },
            new Paginas { idPagina = 3, numeroPagina = 3, texto = "Sample page 3 content" }
        };
        
        StartCoroutine(DisplayCurrentPage());
    }

    public void NextPage()
    {
        if (currentPageIndex < pageList.Count - 1)
        {
            currentPageIndex++;
            StopAllCoroutines();
            StartCoroutine(DisplayCurrentPage());
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            StopAllCoroutines();
            StartCoroutine(DisplayCurrentPage());
        }
    }
}

// Required for SSL certificate bypass (same as in BookController)
