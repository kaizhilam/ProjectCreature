﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : ForestEnemy
{
    public AudioClip idle1;
    public AudioClip idle2;
    public AudioClip idle3;
    public AudioClip idle4;

    public Dino()
    {
        sounds.Add(idle1);
        sounds.Add(idle2);
        sounds.Add(idle3);
        sounds.Add(idle4);
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
