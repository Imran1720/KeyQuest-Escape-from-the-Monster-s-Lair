using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    int nextLevelIndex;

    private void Start()
    {
        nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            if (nextLevelIndex < SceneManager.sceneCount)
            {
                SceneManager.LoadScene(nextLevelIndex);
            }
            else
            {

                SceneManager.LoadScene(0);
            }
        }
    }
}
