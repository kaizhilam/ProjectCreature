using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : ForestEnemy
{
    public AudioClip idle1;
    public AudioClip idle2;
    public AudioClip idle3;
    public AudioClip idle4;
    protected List<AudioClip> sounds;

    public Dino()
    {
        sounds = new List<AudioClip>
        {
            idle1,
            idle2,
            idle3,
            idle4
        };
        Health = 100;
        MovementSpeed1 = 5;
        EnemyName1 = "dino";
    }

    private void Start()
    {
        StartCoroutine(IdleSound());
    }

    IEnumerator IdleSound()
    {
        while (true)
        {
            print("idle sounds");
            SoundManager.instance.RandomizeSfx(sounds, transform.position);
            yield return new WaitForSeconds(1.0f);
        }

    }

}
