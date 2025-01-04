using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    string level = "Level";
    public int keyCount;
    public int requiredKeys;
    public bool setTime = true;
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
    }
    private void OnLevelWasLoaded(int level)
    {
        requiredKeys = SceneManager.GetActiveScene().buildIndex - 1;
    }
    private void Start()
    {
        UnlockLevel(1);

    }
    public void RestartLevel()
    {
        SoundManager.Instance.PlaySound(Sounds.LevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        SoundManager.Instance.PlaySound(Sounds.LevelStart);
        int nextlevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextlevel < SceneManager.sceneCountInBuildSettings)
        {
            UnlockLevel(nextlevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadLevel(int _sceneIndex)
    {
        SoundManager.Instance.PlaySound(Sounds.LevelStart);
        if (_sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }

    public void Menu()
    {
        SoundManager.Instance.PlaySound(Sounds.LevelStart);
        SceneManager.LoadScene(0);
    }

    public LevelStatus GetLevelStatus(int levelIndex)
    {
        return (LevelStatus)PlayerPrefs.GetInt(level + levelIndex, 0);
    }

    public void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(level + levelIndex, (int)LevelStatus.unlocked);
    }

    public void SetLevelComplete()
    {
        PlayerPrefs.SetInt(level + SceneManager.GetActiveScene().buildIndex, (int)LevelStatus.completed);
    }

    public void ActivatePortal()
    {
        if (keyCount >= requiredKeys)
        {
            SetLevelComplete();
            LevelUIManager.instance.OpenGameCompleteMenu();
        }
    }
}
