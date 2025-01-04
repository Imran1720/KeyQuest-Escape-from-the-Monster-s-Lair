using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelectionButton : MonoBehaviour
{
    public Button button;
    public int levelIndex;

    private void Start()
    {
        button.onClick.AddListener(LoadLevel);
    }


    public void LoadLevel()
    {
        if (PlayerPrefs.GetInt("Level" + levelIndex) == (int)LevelStatus.unlocked || PlayerPrefs.GetInt("Level" + levelIndex) == (int)LevelStatus.completed)
        {

            SoundManager.Instance.PlaySound(Sounds.LevelStart);
            LevelManager.Instance.LoadLevel(levelIndex);
        }
    }

}
