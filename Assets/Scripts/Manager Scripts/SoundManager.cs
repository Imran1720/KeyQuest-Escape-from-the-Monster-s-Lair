using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource audioSource_BG;
    [SerializeField]
    private AudioSource audioSource_SFX;

    [Header("Audio Clips")]
    [SerializeField]
    private SoundType[] soundClips;

    private float volume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGMusic(Sounds.GameMusic);
    }

    //Method to Mute BG audioSource
    public void MuteBG()
    {
        audioSource_BG.volume = 0;
    }

    //Method to unmute BG audioSource
    public void UnMuteBG()
    {
        audioSource_BG.volume = .3f;
    }


    //Method to Play Background Music
    public void PlayBGMusic(Sounds _sound)
    {
        AudioClip clip = GetAudioClip(_sound);
        if (clip != null)
        {
            audioSource_BG.clip = clip;
            audioSource_BG.Play();
        }
    }

    //Method to Play SFX Sounds using SFX Sound Source
    public void PlaySFXSound(Sounds _sound)
    {
        AudioClip clip = GetAudioClip(_sound);
        if (clip != null)
        {
            audioSource_SFX.PlayOneShot(clip);
        }
    }

    //Method to get the Audio clip corresponding to enum Sound from the class objectSoundType
    private AudioClip GetAudioClip(Sounds sound)
    {

        SoundType soundobject = Array.Find(soundClips, item => item.soundType == sound);

        if (soundobject == null)
        {
            return null;
        }
        return soundobject.soundClip;
    }

    //setting Volume of the SFX auido source
    public void SetVolume(float vol)
    {
        audioSource_SFX.volume = vol;
    }


}

//class for storing type of sound and sound itself into one object
[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

//List of all sound types used
public enum Sounds
{
    ButtonClick = 0,
    LevelStart,
    GameMusic,
    portal,
    Collect,
    EllenHurt,
    playerFootStep_1,
    playerFootStep_2,
    Jump,
    Land,
    LevelEnter,
    GameWin,
    GameLoose,
    DoorOpen,
    Spike

}
