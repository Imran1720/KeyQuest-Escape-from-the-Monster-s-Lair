public class Spike : InteractableObjects
{
    //This method gets called when interacted with player
    //This method teleport the player to the start position if health greater than 3
    protected override void PerformTriggerAction(PlayerController playerController)
    {
        soundManager.PlaySFXSound(Sounds.Spike);
        playerController.ZeroVelocity();
        playerController.DecreasePlayerHealth();
        playerController.RespawnPlayer();
    }
}
