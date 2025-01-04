using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [Header("Audio Sources")]
    public AudioSource audioSource_BG;
    public AudioSource audioSource_SFX;

    [Header("Audio Clips")]
    public SoundType[] soundClips;

    public bool isMute;
    public float volume;

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
        PlayMusic(Sounds.GameMusic);
    }

    public void MuteSFX()
    {

    }



    public void PlayMusic(Sounds _sound)
    {
        AudioClip clip = GetAudioClip(_sound);
        if (clip != null)
        {
            audioSource_BG.clip = clip;
            audioSource_BG.Play();
        }

    }

    public void PlaySound(Sounds _sound)
    {
        AudioClip clip = GetAudioClip(_sound);
        if (clip != null)
        {
            audioSource_SFX.PlayOneShot(clip);
        }
    }

    private AudioClip GetAudioClip(Sounds sound)
    {

        SoundType soundobject = Array.Find(soundClips, item => item.soundType == sound);

        if (soundobject == null)
        {
            return null;
        }
        return soundobject.soundClip;
    }

    public void DestroySoundManager()
    {
        Destroy(gameObject);
    }

    public void SetVolume(float vol)
    {
        audioSource_SFX.volume = vol;
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds
{
    ButtonClick = 0,
    LevelStart,
    GameMusic,
    portal,
    Collect,
    EnemyHit,
    playerFootStep_1,
    playerFootStep_2,
    Jump,
    Land,
    LevelEnter,
    GameWin,
    GameLoose



}
