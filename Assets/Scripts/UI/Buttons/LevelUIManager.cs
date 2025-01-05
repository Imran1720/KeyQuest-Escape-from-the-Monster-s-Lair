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
        SoundManager.Instance.PlaySFXSound(Sounds.portal);
        Time.timeScale = 0f;
        gameCompleteMenu.SetActive(true);
    }

    //Updates the Key count in the key UI(HUD)
    public void RefreshScore(int key)
    {
        keyText.text = key + "X";
    }
}
