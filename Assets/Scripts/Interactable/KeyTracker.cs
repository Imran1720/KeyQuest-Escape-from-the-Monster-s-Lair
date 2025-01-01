using UnityEngine;

public class KeyTracker : MonoBehaviour
{
    int key = 0;
    private void Start()
    {
        LevelUIManager.Instance.RefreshScore(key);
    }

    public void IncreaseKeyCount(int value)
    {
        key += value;
        LevelManager.Instance.keyCount = key;
        LevelUIManager.Instance.RefreshScore(key);
    }


}
