using UnityEngine;
using MaskGame.Character;

public class AudioManager : MonoBehaviour
{
    public AudioClip loopingMenuBGM;
    public AudioClip periodBGM1;
    public AudioClip periodBGM2;
    public AudioClip successBGM;

    public AudioClip SwitchMaskClip;
    public AudioClip JockZoneClip;
    public AudioClip NerdZoneClip;
    public AudioClip BusinessZoneClip;
    public AudioClip CheerZoneClip;
    public AudioClip TheaterZoneClip;

    public AudioClip LosingPopularity;
    public AudioClip OnHit;
    public AudioClip PeriodPassed;
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

    public void StartLevelMusic(int period)
    {
        if(period == 0)
        {
            rootAudioSource.generator = periodBGM1;
        }
        if (period == 1)
        {
            rootAudioSource.generator = periodBGM2;
        }

        rootAudioSource.Play();
    }

    public void PlayEndingMusic()
    {
        rootAudioSource.generator = successBGM;

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

    public void PlayLosingPopularity()
    {
        rootAudioSource.PlayOneShot(LosingPopularity);
    }

    public void PlayOnHit()
    {
        rootAudioSource.PlayOneShot(OnHit);
    }

    public void PlayLast10Seconds()
    {
        rootAudioSource.PlayOneShot(Last10Seconds);
    }

    public void PlayOutOfTime()
    {
        rootAudioSource.PlayOneShot(OutOfTime);
    }
    public void PlayPeriodPassed()
    {
        rootAudioSource.PlayOneShot(PeriodPassed);
    }
}
