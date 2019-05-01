using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void RandomizeSfx(List<AudioClip> sounds, AudioSource source)
    {
        int randomIndex = Random.Range(0, sounds.Count);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        if (!source.isPlaying)
        {
            source.pitch = randomPitch;
            source.clip = sounds[randomIndex];
            source.Play();
        }
    }

    public void PlaySfxAtSource(AudioClip clip, AudioSource source)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        if (!source.isPlaying)
        {
            source.pitch = randomPitch;
            source.clip = clip;
            source.Play();
        }
    }
}
