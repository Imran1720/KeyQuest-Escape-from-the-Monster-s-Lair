using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player");
            collision.gameObject.GetComponent<PlayerController>().DecreasePlayerHealth();
            collision.gameObject.GetComponent<PlayerController>().RespawnPLayer();
        }
    }
}
