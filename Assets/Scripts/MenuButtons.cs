using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Button playButton, quitButton, backButton;

    public GameObject homeScreen, levelScreen;

    private void Start()
    {
        playButton.onClick.AddListener(ShowLevelsScreen);
        quitButton.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(HideLevelsScreen);
    }

    public void ShowLevelsScreen()
    {
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
        homeScreen.SetActive(false);
        levelScreen.SetActive(true);
    }

    public void HideLevelsScreen()
    {
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
        homeScreen.SetActive(true);
        levelScreen.SetActive(false);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySound(Sounds.LevelStart);
        Application.Quit();
    }
}
