using UnityEngine;
using UnityEngine.UI;

public class BookInfo : MonoBehaviour
{
    public Text nameText;
    public Text authorText;
    void Start()
    {
        nameText.text = PlayerPrefs.GetString("book_name");
        // Debug.Log(PlayerPrefs.GetString("autor"));
        authorText.text = PlayerPrefs.GetString("autor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
