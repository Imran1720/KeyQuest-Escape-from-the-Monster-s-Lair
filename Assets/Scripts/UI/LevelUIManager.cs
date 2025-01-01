using TMPro;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager instance;
    public static LevelUIManager Instance { get { return instance; } }
    [Header("Health UI")]
    public GameObject[] hearts;

    [Header("Key")]
    public TextMeshProUGUI keyText;

    [Header("Menus")]
    public GameObject gameOverMenu;
    public GameObject gameCompleteMenu;
    private void Awake()
    {
        instance = this;
    }

    public void RefreshHealth(int health)
    {
        for (int i = hearts.Length - 1; i >= health; i--)
        {
            hearts[i].SetActive(false);
        }
    }

    public void OpenGameOverMenu()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    public void OpenGameCompleteMenu()
    {
        Time.timeScale = 0f;
        gameCompleteMenu.SetActive(true);
    }

    public void RefreshScore(int key)
    {

        keyText.text = key + "X";
    }



}
