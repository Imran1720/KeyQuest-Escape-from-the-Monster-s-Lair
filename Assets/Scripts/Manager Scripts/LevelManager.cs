using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    string level = "Level";
    private int keyCount;
    [SerializeField]
    private int requiredKeys;

    private SoundManager soundManager;
    private LevelUIManager levelUIManager;
    //public bool setTime = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        UnlockLevel((int)Levels.Lobby);
        UnlockLevel((int)Levels.Level1);//makes sure that the first level is unlocked
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
    }
    //Method to set the number of Keys required based on level index
    private void OnLevelWasLoaded(int level)
    {
        ResetKeyCount();
        levelUIManager = LevelUIManager.Instance;
        requiredKeys = SceneManager.GetActiveScene().buildIndex - 1;
    }

    private void ResetKeyCount()
    {
        keyCount = 0;
    }

    //method to Reload the current scene
    public void RestartLevel()
    {
        soundManager.PlaySFXSound(Sounds.LevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Method to Load next level based on current level Index
    public void LoadNextLevel()
    {
        soundManager.PlaySFXSound(Sounds.LevelStart);
        int nextlevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextlevel < SceneManager.sceneCountInBuildSettings)
        {
            UnlockLevel(nextlevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //Method to Open Level at index '_sceneIndex' 
    public void LoadLevel(int _sceneIndex)
    {
        soundManager.PlaySFXSound(Sounds.LevelStart);
        if (_sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
    //Method to Open Home screen(Main Menu) 
    public void Menu()
    {
        soundManager.PlaySFXSound(Sounds.LevelStart);
        SceneManager.LoadScene(0);
    }

    //Returning the status of the level at index 'levelIndex'
    public LevelStatus GetLevelStatus(int levelIndex)
    {
        return (LevelStatus)PlayerPrefs.GetInt(level + levelIndex, 0);
    }

    //Setting the Level status to unlock
    public void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(level + levelIndex, (int)LevelStatus.unlocked);
    }

    //Method to set LevelStatus to Complete
    public void SetLevelComplete()
    {
        UnlockLevel(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt(level + SceneManager.GetActiveScene().buildIndex, (int)LevelStatus.completed);
    }

    //Method to Activate Portal
    public void ActivatePortal()
    {

        soundManager.PlaySFXSound(Sounds.portal);
        SetLevelComplete();
        levelUIManager.OpenGameCompleteMenu();
    }

    public bool GetLevelCompletionStatus()
    {
        return keyCount >= requiredKeys;
    }

    public void CollectKey()
    {
        keyCount++;
        soundManager.PlaySFXSound(Sounds.Collect);
        levelUIManager.RefreshScore(keyCount);
    }

}

//Enum for level status
public enum LevelStatus
{
    locked = 0,
    unlocked,
    completed
}

//Enum representing indexs of scenes
public enum Levels
{
    Lobby,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
}