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
        UnlockLevel(1);//makes sure that the first level is unlocked
    }

    //Method to set the number of Keys required based on level index
    private void OnLevelWasLoaded(int level)
    {

        SoundManager.Instance.SetVolume(.4f);//Sets the volume of sfx to .7
        requiredKeys = SceneManager.GetActiveScene().buildIndex - 1;
    }


    //method to Reload the current scene
    public void RestartLevel()
    {
        SoundManager.Instance.PlaySFXSound(Sounds.LevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Method to Load next level based on current level Index
    public void LoadNextLevel()
    {
        SoundManager.Instance.PlaySFXSound(Sounds.LevelStart);
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
        SoundManager.Instance.PlaySFXSound(Sounds.LevelStart);
        if (_sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
    //Method to Open Home screen(Main Menu) 
    public void Menu()
    {
        SoundManager.Instance.PlaySFXSound(Sounds.LevelStart);
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
        PlayerPrefs.SetInt(level + SceneManager.GetActiveScene().buildIndex, (int)LevelStatus.completed);
    }

    //Method to Activate Portal
    public void ActivatePortal()
    {

        LevelUIManager.instance.OpenGameCompleteMenu();

    }

    public bool GetLevelCompleteStatus()
    {
        return keyCount >= requiredKeys;
    }
}
