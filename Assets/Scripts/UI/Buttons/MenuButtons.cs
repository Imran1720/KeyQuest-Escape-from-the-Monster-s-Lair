using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Button playButton, quitButton, backButton;

    public GameObject homeScreen, levelScreen;

    private void Start()
    {
        //adding listeners to menu buttons
        //open level screen
        playButton.onClick.AddListener(ShowLevelsScreen);
        //close game
        quitButton.onClick.AddListener(QuitGame);
        //close level screen
        backButton.onClick.AddListener(HideLevelsScreen);
    }

    //Method to show the Level list Pannel
    //Method to Hide the Home screen
    public void ShowLevelsScreen()
    {
        SoundManager.Instance.PlaySFXSound(Sounds.ButtonClick);
        homeScreen.SetActive(false);
        levelScreen.SetActive(true);
    }

    //Method to Show the Home screen
    //Method to Hide the Level list Pannel
    public void HideLevelsScreen()
    {
        SoundManager.Instance.PlaySFXSound(Sounds.ButtonClick);
        homeScreen.SetActive(true);
        levelScreen.SetActive(false);
    }

    //Method to close Game
    public void QuitGame()
    {
        SoundManager.Instance.PlaySFXSound(Sounds.LevelStart);
        Application.Quit();
    }
}
