using UnityEngine;
public class AnimationTriggers : MonoBehaviour
{
    bool leftFoot = false;
    public Animator playerAnimator;
    //Method to plays one of the two different sound on run animation
    //this script is called form the animation trigger event
    public void PlayFootSound()
    {
        if (leftFoot)
        {
            SoundManager.Instance.PlaySFXSound(Sounds.playerFootStep_1);
        }
        else
        {
            SoundManager.Instance.PlaySFXSound(Sounds.playerFootStep_2);
        }
        leftFoot = !leftFoot;
    }

    public void PlayerDeathAnimationComplete()
    {
        playerAnimator.SetBool("PlayerDie", false);
        SoundManager.Instance.SetVolume(.4f);
        SoundManager.Instance.PlaySFXSound(Sounds.GameLoose);
        LevelUIManager.Instance.OpenGameOverMenu();
    }

}
