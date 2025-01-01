using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            LevelManager.Instance.ActivatePortal();
        }
    }


}
