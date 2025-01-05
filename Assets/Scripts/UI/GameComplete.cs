using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour
{
    public GameObject homeButton, nextButton;
    public TextMeshProUGUI message;

    private void OnEnable()
    {

        homeButton.GetComponent<Button>().onClick.AddListener(GoToHome);
        nextButton.GetComponent<Button>().onClick.AddListener(GoToNextLevel);
        if (PlayerPrefs.GetInt("Level5") == (int)LevelStatus.completed)
        {
            message.text = "You have completed\r\nall levels!!";
            homeButton.SetActive(true);
        }
        else
        {
            message.text = "You have completed\r\nthis level!!";
            nextButton.SetActive(true);
        }
    }

    public void GoToHome()
    {
        LevelManager.Instance.Menu();
    }

    public void GoToNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }

}
