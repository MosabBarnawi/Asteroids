using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioSource source { get; set; }
    public AudioClip clip;
    public SoundFX soundFxName;
    public bool isLoop;

    [Range(0f, 1f)] public float volume;
}