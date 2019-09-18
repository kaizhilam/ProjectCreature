using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    private static AudioSource src;
    public List<AudioClip> runSounds;
    public List<AudioClip> hurtSounds;
    public List<AudioClip> jumpSounds;
    public List<AudioClip> doubleJumpSounds;
    public List<AudioClip> attackSounds;
    public List<AudioClip> exitWaterSounds;

    private static Dictionary<SoundTypes, List<AudioClip>> soundDictionary;

    //we use enums instead of string to prevent risk of programmer mispelling a sound group
    //vs will give error if you mispell an enum
    public enum SoundTypes
    {
        run,
        hurt,
        jump,
        doublejump,
        attack,
        exitwater,
        none
    }

    private void Start()
    {
        soundDictionary = new Dictionary<SoundTypes, List<AudioClip>>();
        //setup sound dictionary, pairing enum with a sound / group of sounds
        soundDictionary.Add(SoundTypes.run, runSounds);
        soundDictionary.Add(SoundTypes.hurt, hurtSounds);
        soundDictionary.Add(SoundTypes.jump, jumpSounds);
        soundDictionary.Add(SoundTypes.doublejump, doubleJumpSounds);
        soundDictionary.Add(SoundTypes.attack, attackSounds);
        soundDictionary.Add(SoundTypes.exitwater, exitWaterSounds);

        //cache audiosrc
        src = GetComponent<AudioSource>();

    }

    //play sound given an enum
    //none will stop playing all sounds
    public void SetSoundOfName(SoundTypes key)
    {
        switch (key)
        {
            case SoundTypes.run:
                PlayRandomSoundOfKey(SoundTypes.run);
                break;
            case SoundTypes.hurt:
                PlayRandomSoundOfKey(SoundTypes.hurt);
                break;
            case SoundTypes.jump:
                PlayRandomSoundOfKey(SoundTypes.jump);
                break;
            case SoundTypes.doublejump:
                PlayRandomSoundOfKey(SoundTypes.doublejump);
                break;
            case SoundTypes.attack:
                PlayRandomSoundOfKey(SoundTypes.attack);
                break;
            case SoundTypes.exitwater:
                PlayRandomSoundOfKey(SoundTypes.exitwater);
                break;
            case SoundTypes.none:
                src.Stop();
                break;
        }
    }

    public void PlayRandomSoundOfKey(SoundTypes key)
    {
        int randomIndex = Random.Range(0, soundDictionary[key].Count);
        float randomPitch = Random.Range(0.95f, 1.05f);
        if (!src.isPlaying)
        {
            src.pitch = randomPitch;
            src.clip = soundDictionary[key][randomIndex];
            src.Play();
        }

    }

    public void StopSounds()
    {
        src.Stop();
    }
}
