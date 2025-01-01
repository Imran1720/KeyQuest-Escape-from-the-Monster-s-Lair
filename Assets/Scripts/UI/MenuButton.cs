using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
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
        if (PlayerPrefs.GetInt("Level" + levelIndex) == (int)LevelStatus.unlocked || PlayerPrefs.GetInt("Level" + levelIndex) == (int)LevelStatus.completed)
        {
            LevelManager.Instance.LoadLevel(levelIndex);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
