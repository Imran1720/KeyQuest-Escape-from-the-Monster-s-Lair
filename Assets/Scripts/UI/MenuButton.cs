using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button button;

    public bool isQuitButton;
    public int levelIndex;

    private void Start()
    {
        if (!isQuitButton)
        {
            button.onClick.AddListener(LoadLevel);
        }
        else
        {
            button.onClick.AddListener(Quit);
        }
    }


    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
