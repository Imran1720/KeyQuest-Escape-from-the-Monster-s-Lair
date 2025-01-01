using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    string level = "Level";
    public int keyCount;

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

    private void Start()
    {
        UnlockLevel(1);


    }
    public void RestartLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadLevel(int _sceneIndex)
    {
        if (_sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }

    public void Menu()
    {
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

    public void ActivatePortal()
    {

        if (keyCount >= 3)
        {
            LoadNextLevel();
        }
    }
}
