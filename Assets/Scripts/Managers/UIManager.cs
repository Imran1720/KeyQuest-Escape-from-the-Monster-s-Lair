using TMPro;
using UnityEngine;

//This class is responsible to handle UI related Actions
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [Header("HEALTH UI")]
    [SerializeField]
    private GameObject[] hearts;

    [Header("KEY")]
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

    private int keyCount;
    private int totalKeysRequired;
    private Levels currentLevel;

    private SoundManager soundManager;
    private LevelManager levelManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitializeData();
    }

    //This method is responsible for initialization and resetting the data
    public void InitializeData()
    {
        if (soundManager == null)
        {
            soundManager = SoundManager.Instance;
        }
        if (levelManager == null)
        {
            levelManager = LevelManager.Instance;
        }
        currentLevel = levelManager.GetCurrentLevel();
        ResetKeyCount();
        SetKeyCountForGate();
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
        soundManager.PlaySFXSound(Sounds.GameLoose);
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }


    //Updates the Key count in the key UI(HUD)
    public void CollectKey()
    {
        keyCount++;
        keyText.text = keyCount + "X";
    }

    //Setting total number of keys required to open gate based on Level
    private void SetKeyCountForGate()
    {
        totalKeysRequired = (int)currentLevel - 1;
    }

    //called at the start of each level to reset the keycount to 0
    private void ResetKeyCount()
    {
        keyCount = 0;
    }

    //Method to check whether the player has collected all the keys
    public bool GetLevelCompletionStatus()
    {
        return keyCount >= totalKeysRequired;
    }

    //Method to Activate Portal
    public void ActivatePortal()
    {
        levelManager.SetLevelComplete((int)currentLevel + 1);
        OpenGameCompleteMenu();
    }

    //When level completes this method is called to show level complete Screen
    public void OpenGameCompleteMenu()
    {
        Time.timeScale = 0f;
        bool isLevel5Completed = PlayerPrefs.GetInt("Level5") == (int)LevelStatus.completed;
        soundManager.PlaySFXSound(Sounds.portal);
        if (isLevel5Completed && currentLevel == Levels.Level5)
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

}
