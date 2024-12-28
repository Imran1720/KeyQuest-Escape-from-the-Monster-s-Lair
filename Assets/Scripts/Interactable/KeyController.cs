using UnityEngine;

public class KeyController : MonoBehaviour
{

    public int keyCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.GetKey(keyCount);
            Destroy(gameObject);
        }
    }
}
