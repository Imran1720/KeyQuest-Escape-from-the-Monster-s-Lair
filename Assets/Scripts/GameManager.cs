using UnityEngine;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int portalKeys;

    private void Awake()
    {
        instance = this;
    }

    public void ActivatePortal()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}

