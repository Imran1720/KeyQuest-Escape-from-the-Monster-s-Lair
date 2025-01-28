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

    //This method gets called when iteracted with player
    //This method is responsible to  perform certain actions when colliding with player
    protected override void PerformTriggerAction(PlayerController playerController)
    {
        //base.PerformTriggerAction(playerController);
        doorAnimator.SetBool("NearDoor", true);
        if (uiManager.GetLevelCompletionStatus())
        {
            soundManager.PlaySFXSound(Sounds.DoorOpen);
            doorAnimator.SetBool("KeyCollected", true);
        }
    }


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
