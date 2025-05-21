using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IpSet : MonoBehaviour
{
    public InputField ip;

    public void SaveInput()
    {
        string userInput = ip.text;
        PlayerPrefs.SetString("ip", userInput);
        PlayerPrefs.Save();
        Debug.Log("Saved: " + userInput);
        SceneManager.LoadScene("MyLibrary");
    }

    public void Changetoip()
    {
        SceneManager.LoadScene("Assign");
    }
}
