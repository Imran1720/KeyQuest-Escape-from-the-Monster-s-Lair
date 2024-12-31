using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager Instance { get; private set; }
    [Header("Health UI")]
    public GameObject[] hearts;


    private void Awake()
    {
        Instance = this;
    }

    public void RefreshHealth(int health)
    {
        for (int i = hearts.Length - 1; i >= health; i--)
        {
            hearts[i].SetActive(false);
        }
    }


}
