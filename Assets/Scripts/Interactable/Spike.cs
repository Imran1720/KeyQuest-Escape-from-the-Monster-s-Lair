public class Spike : InteractableObjects
{
    protected override void PerformTriggerAction(PlayerController playerController)
    {
        soundManager.PlaySFXSound(Sounds.Spike);
        playerController.ZeroVelocity();
        playerController.DecreasePlayerHealth();
        playerController.RespawnPlayer();
    }
}
