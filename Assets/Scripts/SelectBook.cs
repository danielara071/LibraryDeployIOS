using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectBook : MonoBehaviour
{
    public void ChooseBook1()
    {
        BookController.Instance.Select(1);
        // PlayerPrefs.SetInt("idlib", 1);
    }
    public void ChooseBook2()
    {
        BookController.Instance.Select(2);
    }
    public void ChooseBook3()
    {
        BookController.Instance.Select(3);
    }
    public void ChooseBook4()
    {
        BookController.Instance.Select(4);
    }
    public void ChooseBook5()
    {
        BookController.Instance.Select(5);
    }
    public void ChooseBook6()
    {
        BookController.Instance.Select(6);
    }

    public void ChangeSceneToBook()
    {
        SceneManager.LoadScene("BookPreview");
    }
    public void ChangeSceneToLibrary()
    {
        SceneManager.LoadScene("MyLibrary");
    }
}
