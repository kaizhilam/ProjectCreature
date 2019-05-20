using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dino : ForestEnemy
{
    public AudioClip idle1;
    public AudioClip idle2;
    public AudioClip idle3;
    public AudioClip idle4;
    protected List<AudioClip> sounds;
    private AudioSource src;



    public Dino()
    {
        
        Health = 100;
        MovementSpeed1 = 5;
        EnemyName1 = "dino";

    }
/*
    IEnumerator collideFlash()
    {
        Material m = this.mainRenderer.material;
        Color32 c = this.mainRenderer.material.color;
        this.mainRenderer.material = null;
        this.mainRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        this.mainRenderer.material = m;
        this.mainRenderer.material.color = c;
    }


    IEnumerator FlashCoroutine()
    {
        float duration = 1f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;

            // flashing code goes here

            return null;
        }
    }
    */



    public void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, EnemyAIState>()
        {
            {typeof(WanderState), new WanderState(this) },
            {typeof(ChaseState), new ChaseState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }


    private void Start()
    {
        src = GetComponent<AudioSource>();
        sounds = new List<AudioClip>
        {
            idle1,
            idle2,
            idle3,
            idle4
        };
        StartCoroutine(IdleSound());

    }





    IEnumerator IdleSound()
    {
        while (true)
        {
            //print("idle sounds");
            SoundManager.instance.RandomizeSfx(sounds, src);
            yield return new WaitForSeconds(UnityEngine.Random.Range(5,8));
        }

    }

}



