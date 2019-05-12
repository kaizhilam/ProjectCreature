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
    private AudioSource src;
    public GameObject drop;

    public Dino()
    {
        
        Health = 100;
        MovementSpeed1 = 5;
        EnemyName1 = "dino";
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
            yield return new WaitForSeconds(Random.Range(5,8));
        }

    }

    public override void ResolveDeletion() //called, when enemy will be destroyed
    {
        Instantiate(drop, transform.position, drop.transform.rotation);
        ForestEnemyPool.Instance.ReturnToPool(this);
    }


}
