using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Button restartButton;
    public Button quitutton;

    // used for buttons in the Gameover Screen

    private void Awake()
    {
        //adding listeners to restart and quit button 
        restartButton.onClick.AddListener(Restart);
        quitutton.onClick.AddListener(Menu);
    }

    //Restart Level
    void Restart()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.RestartLevel();
    }

    //Go to Main menu
    void Menu()
    {
        LevelManager.Instance.Menu();
    }



}
