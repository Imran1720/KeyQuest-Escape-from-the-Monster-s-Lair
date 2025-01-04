using UnityEngine;

public class FootStep : MonoBehaviour
{
    bool leftFoot = false;

    public void PlayFootSound()
    {
        if (leftFoot)
        {
            SoundManager.Instance.PlaySound(Sounds.playerFootStep_1);
        }
        else
        {
            SoundManager.Instance.PlaySound(Sounds.playerFootStep_2);
        }
        leftFoot = !leftFoot;
    }




}
