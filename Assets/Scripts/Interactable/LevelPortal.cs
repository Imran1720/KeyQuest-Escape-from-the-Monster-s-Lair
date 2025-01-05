using UnityEngine;

public class LevelPortal : MonoBehaviour
{

    public Animator doorAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if Player is interacting with Gate. 
        //if colliding calls the Portalactivation
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //LevelManager.Instance.ActivatePortal();
            doorAnimator.SetBool("NearDoor", true);
            if (LevelManager.Instance.GetLevelCompleteStatus())
            {
                SoundManager.Instance.PlaySFXSound(Sounds.DoorOpen);
                doorAnimator.SetBool("KeyCollected", true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //LevelManager.Instance.ActivatePortal();
            doorAnimator.SetBool("NearDoor", false);
        }
    }

    public void LevelComplete()
    {
        LevelManager.Instance.ActivatePortal();
    }


}
