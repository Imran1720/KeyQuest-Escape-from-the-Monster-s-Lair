using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int portalKeys;
    private void Awake()
    {
        Instance = this;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    public void LoadNextScene()
    {
        //Debug.Log(SceneManager.to);

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void LoadScene(int _sceneIndex)
    {
        if (_sceneIndex < SceneManager.sceneCount)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }


    public void ActivatePortal()
    {
        //Debug.Log(portalKeys);
        if (portalKeys >= 3)
        {
            LoadNextScene();
        }
    }
}

