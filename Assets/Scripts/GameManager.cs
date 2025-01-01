using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverMenu;

    public int portalKeys;
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        gameOverMenu.SetActive(false);
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

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void ActivatePortal()
    {
        //Debug.Log(portalKeys);
        if (portalKeys >= 3)
        {
            LoadNextScene();
        }
    }

    public void OpenGameOverMenu()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);

    }
}

