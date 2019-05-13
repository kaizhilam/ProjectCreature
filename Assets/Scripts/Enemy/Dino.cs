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
    public GameObject drop;
    //public DropTable DropTable { get; set; }

    public Dino()
    {
        
        Health = 100;
        MovementSpeed1 = 5;
        EnemyName1 = "dino";
    }

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
        /*DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("knife",25),
            new LootDrop("arrow",25),
            new LootDrop("sword",25)
        };*/

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

    public override void DropWeapon() //called, when enemy will be destroyed
    {
        /*SlottedItem item = DropTable.GetDrop();
        if (item != null)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }*/
        Instantiate(drop, transform.position, drop.transform.rotation);
    }
}
