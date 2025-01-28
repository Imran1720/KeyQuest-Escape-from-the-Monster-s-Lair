using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class is responsible to perform actions related to levels
//Actions: Loading Levels, Unlocking Levels, Geting Level Status... 
public class LevelManager : MonoBehaviour
{

    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }


    private readonly string level = "Level";
    public LevelData[] levelData;

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
        UnlockLevel((int)Levels.Level1);//makes sure that the first level is unlocked
    }

    //Method to Open Level at index '_sceneIndex' 
    public void LoadLevel(int _sceneIndex)
    {
        if (_sceneIndex < GetTotalLevelCount())
        {
            SceneManager.LoadScene(_sceneIndex);
        }
        else
        {
            LoadLobby();
        }
    }
    //Method to Open Home screen(Main Menu) 
    public void LoadLobby()
    {
        SceneManager.LoadScene((int)Levels.Lobby);
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
    public void SetLevelComplete(int _levelIndex)
    {
        UnlockLevel(_levelIndex);
        PlayerPrefs.SetInt(level + _levelIndex, (int)LevelStatus.completed);
        PlayerPrefs.Save();
    }

    //Method to get count of total number of Playable levels(Lobby is excluded)
    public int GetTotalLevelCount()
    {
        // subtracting 1 to make sure lobby is not counted 
        return levelData.Length;
    }

    //Method that returns current Level(enum)
    public Levels GetCurrentLevel()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        //Checking whether current scene name(string) exists in the levelData Array
        //if exists then returning corresponding level index
        LevelData levelObject = Array.Find(levelData, item => item.levelName == currentLevelName);
        if (levelObject == null)
        {
            return Levels.Lobby;
        }
        return levelObject.levelToLoad;
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
    Lobby = 0,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
}


//This class is used to store and map the levels in the game.
[Serializable]
public class LevelData
{
    public string levelName;
    public Levels levelToLoad;
}