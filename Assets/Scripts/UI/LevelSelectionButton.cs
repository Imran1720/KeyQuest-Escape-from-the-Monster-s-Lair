using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelectionButton : MonoBehaviour
{
    public Button button;
    public int levelIndex;

    //This script is used by Level Buttons in the main menu
    private void Start()
    {
        // adding the listener to the button attached
        button.onClick.AddListener(LoadLevel);

        SetButtonInteraction();
    }

    //Method to set Button Interaction if locked no iteraction happens
    private void SetButtonInteraction()
    {
        if (LevelManager.Instance.GetLevelStatus(levelIndex) == LevelStatus.locked)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }


    //Loads the level with index 'levelIndex'
    public void LoadLevel()
    {
        if (PlayerPrefs.GetInt("Level" + levelIndex) == (int)LevelStatus.unlocked || PlayerPrefs.GetInt("Level" + levelIndex) == (int)LevelStatus.completed)
        {
            SoundManager.Instance.PlaySFXSound(Sounds.LevelStart);
            LevelManager.Instance.LoadLevel(levelIndex);
        }
    }

}
