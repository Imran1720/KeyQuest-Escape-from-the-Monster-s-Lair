using UnityEngine;

public class KeyTracker : MonoBehaviour
{
    int key = 0;
    private void Start()
    {
        LevelUIManager.Instance.RefreshScore(key);
    }

    //Increases the count of keys and updates in UI
    public void IncreaseKeyCount(int value)
    {
        key += value;
        LevelUIManager.Instance.RefreshScore(key);
        LevelManager.Instance.keyCount = key;
    }


}
