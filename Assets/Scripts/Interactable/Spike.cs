public class Spike : InteractableObjects
{
    //This method gets called when iteracted with player
    //This method is responsible to  perform certain actions when colliding with player
    protected override void PerformTriggerAction(PlayerController playerController)
    {
        soundManager.PlaySFXSound(Sounds.Spike);
        playerController.ZeroVelocity();
        playerController.DecreasePlayerHealth();
        playerController.RespawnPlayer();
    }
}
