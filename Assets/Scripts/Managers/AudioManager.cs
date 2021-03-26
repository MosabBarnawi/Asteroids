using Random = UnityEngine.Random;
using UnityEngine;
using System;

// TODO:: DONT MAKE A SINGLETON
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private Sound[] sounds;
    private AudioSource oneShotAudioSorce;

    private AudioSource currentPlayingFX;
    bool isPlayingOnCurrentSoundFx = false;

    #region Unity Calls
    protected override void Awake()
    {
        base.Awake();

        oneShotAudioSorce = gameObject.AddComponent<AudioSource>();

        foreach (Sound item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.clip;
            item.source.loop = item.isLoop;
            item.source.volume = item.volume;
        }

        PlaySoundFX(SoundFX.Thrust1);
    }

    #endregion

    #region Public Mehthods
    /// <summary>
    /// To Play As One Shot
    /// </summary>
    /// <param name="soundFx"></param>
    public void PlayOnce(SoundFX soundFx)
    {
        Sound sound = FindNameInSoundFX(soundFx);

        if (sound != null)
            oneShotAudioSorce.PlayOneShot(sound.clip);
    }

    /// <summary>
    /// Plays Sound Fx . Play()
    /// </summary>
    /// <param name="soundFx"></param>
    /// <param name="randomPitch">Option for Minor Pitch Changing</param>
    public void PlaySoundFX(SoundFX soundFx, bool randomPitch = false)
    {
        Sound s = Array.Find(sounds, sound => sound.soundFxName == soundFx);

        if (s != null)
        {
            if (randomPitch)
                s.source.pitch = Random.Range(0.6f, 1.5f);

            s.source.Play();
        }
    }

    /// <summary>
    /// Used For Playing Ship Thrust Sound Fx when Moving
    /// </summary>
    /// <param name="isInMovement"></param>
    public void PlayThrustSoundFX(bool isInMovement)
    {
        if (isInMovement)
        {
            if (!isPlayingOnCurrentSoundFx)
            {
                StartPlayFX(SoundFX.Thrust2);
                isPlayingOnCurrentSoundFx = !isPlayingOnCurrentSoundFx;
            }
        }
        else
        {
            if (isPlayingOnCurrentSoundFx)
            {
                isPlayingOnCurrentSoundFx = !isPlayingOnCurrentSoundFx;
                PausePlayingFX();
            }
        }
    }

    #endregion

    /// <summary>
    /// TODO:: CAN BE IMPROVED
    /// </summary>
    /// <param name="soundFx"></param>
    private void StartPlayFX(SoundFX soundFx)
    {
        Sound sound = FindNameInSoundFX(soundFx);

        if (sound != null)
        {
            currentPlayingFX = sound.source;
            sound.source.Play();
        }
    }

    private void PausePlayingFX()
    {
        if (currentPlayingFX != null)
            currentPlayingFX.Pause();
    }

    private Sound FindNameInSoundFX(SoundFX soundFX)
    {
        return Array.Find(sounds, sound => sound.soundFxName == soundFX);
    }
}
