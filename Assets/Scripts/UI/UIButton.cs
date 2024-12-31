using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Button restartButton;
    public Button quitutton;

    public GameObject gameOverMenu;


    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        quitutton.onClick.AddListener(Menu);
    }

    void Restart()
    {
        GameManager.Instance.RestartGame();
    }

    void Menu()
    {
        GameManager.Instance.Menu();
    }



}
