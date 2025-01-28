using UnityEngine;

public class Door : InteractableObjects
{
    [SerializeField]
    private Animator doorAnimator;

    protected override void Start()
    {
        base.Start();
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
    }

    //This method gets called when interacted with player
    //This method is responsible to open gate
    protected override void PerformTriggerAction(PlayerController playerController)
    {
        //Door glows when player is within interaction zone
        doorAnimator.SetBool("NearDoor", true);
        if (uiManager.GetLevelCompletionStatus())
        {
            //Door Opens
            soundManager.PlaySFXSound(Sounds.DoorOpen);
            doorAnimator.SetBool("KeyCollected", true);
        }
    }

    //This method removes door glow.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            doorAnimator.SetBool("NearDoor", false);
        }
    }

    //This method is called from the animation event when the door open animation finishes
    public void LevelComplete()
    {
        uiManager.ActivatePortal();
    }


}
