using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //This detects the Player when player falls on spikes and decrease the player health and respawns
        //the player to te spawn point
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.PlaySFXSound(Sounds.Spike);
            collision.gameObject.GetComponent<PlayerController>().DecreasePlayerHealth();
            collision.gameObject.GetComponent<PlayerController>().RespawnPlayer();
        }
    }
}
