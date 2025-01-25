using UnityEngine;

public class Door : InteractableObjects
{
    public Animator doorAnimator;
    protected override void PerformTriggerAction(PlayerController playerController)
    {
        //base.PerformTriggerAction(playerController);
        doorAnimator.SetBool("NearDoor", true);
        if (levelManager.GetLevelCompletionStatus())
        {
            soundManager.PlaySFXSound(Sounds.DoorOpen);
            doorAnimator.SetBool("KeyCollected", true);
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
        levelManager.ActivatePortal();
    }


}
