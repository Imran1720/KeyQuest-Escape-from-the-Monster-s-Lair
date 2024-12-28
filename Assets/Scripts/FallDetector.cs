using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
