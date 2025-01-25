using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    private static LevelUIManager instance;
    public static LevelUIManager Instance { get { return instance; } }
    [Header("Health UI")]
    [SerializeField]
    private GameObject[] hearts;

    [Header("Key")]
    [SerializeField]
    private TextMeshProUGUI keyText;

    [Header("MENUS")]
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject gameCompleteMenu;

    [Header("GAME COMPLETE PANEL")]
    public TextMeshProUGUI gameCompleteText;
    public GameObject homeButton, nextButton;
    private void Awake()
    {
        instance = this;
    }

    //This method updates the number of hearts player has in HUD based on player Health
    public void RefreshHealth(int health)
    {
        for (int i = hearts.Length - 1; i >= health; i--)
        {
            hearts[i].SetActive(false);
        }
    }

    //when Health drops to zero this method is called to show Gameover Screen
    public void OpenGameOverMenu()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    //When level completes this method is called to show level complete Screen
    public void OpenGameCompleteMenu()
    {
        Time.timeScale = 0f;
        if (PlayerPrefs.GetInt("Level5") == (int)LevelStatus.completed && SceneManager.GetActiveScene().buildIndex == (int)Levels.Level5)
        {
            gameCompleteText.text = "You have completed\r\nall levels!!";
            homeButton.SetActive(true);
        }
        else
        {
            gameCompleteText.text = "You have completed\r\nthis level!!";
            nextButton.SetActive(true);
        }
        gameCompleteMenu.SetActive(true);
    }

    //Updates the Key count in the key UI(HUD)
    public void RefreshScore(int key)
    {
        keyText.text = key + "X";
    }
}
