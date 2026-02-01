using UnityEngine;
using MaskGame.Character;

public class AudioManager : MonoBehaviour
{
    public AudioClip loopingMenuBGM;
    public AudioClip loopingLevelBGM;

    public AudioClip SwitchMaskClip;
    public AudioClip JockZoneClip;
    public AudioClip NerdZoneClip;
    public AudioClip BusinessZoneClip;
    public AudioClip CheerZoneClip;
    public AudioClip TheaterZoneClip;

    public AudioClip OutOfTime;
    public AudioClip Last10Seconds;

    public AudioSource rootAudioSource;

    public static AudioManager instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void StartLevelMusic()
    {
        rootAudioSource.generator = loopingLevelBGM;
        rootAudioSource.Play();
    }

    public void PlayMaskSwitch()
    {
        rootAudioSource.PlayOneShot(SwitchMaskClip);
    }

    public void PlayZoneEnter(MaskState state)
    {
        switch (state)
        {
            case MaskState.NONE:
                break;
            case MaskState.JOCK:
                rootAudioSource.PlayOneShot(JockZoneClip);
                break;
            case MaskState.NERD:
                rootAudioSource.PlayOneShot(NerdZoneClip);
                break;
            case MaskState.BASIC:
                break;
            case MaskState.BUSINESS:
                rootAudioSource.PlayOneShot(BusinessZoneClip);
                break;
            case MaskState.CHEER:
                rootAudioSource.PlayOneShot(CheerZoneClip);
                break;
            case MaskState.THEATER:
                rootAudioSource.PlayOneShot(TheaterZoneClip);
                break;
        }
    }

    public void PlayLast10Seconds()
    {
        rootAudioSource.PlayOneShot(Last10Seconds);
    }

    public void PlayOutOfTime()
    {
        rootAudioSource.PlayOneShot(OutOfTime);
    }
}
