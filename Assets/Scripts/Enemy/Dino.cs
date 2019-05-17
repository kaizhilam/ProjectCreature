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
    public GameObject[] ItemsToDrop;
    public DropTable DropTable { get; set; }
    //public SlottedItem slottedItem;

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
            {typeof(ChaseState), new ChaseState(this) },
            {typeof(AvoidanceState), new AvoidanceState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    private void Start()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop> //set loots in enemy
        {
            new LootDrop("BasicKnife",25), //25% chance to get a knife
            new LootDrop("armbow",25), //50% chance to get an arrow
            new LootDrop("Tent",25) //75% chance to get its head
        };

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
        Item item = DropTable.GetDrop(); //hopefully, it get the rolled item or do nothing(if item == null)

        if (item != null)
        {
            //Debug.Log("get item " + item.ObjectSlug);
            //Debug.Log("get drop 1 " + ItemsToDrop[0].name);

            /*for (int i = 0; i < 3; i++)
            {
                Debug.Log(i);
                Debug.Log("gameobject is " + ItemsToDrop[i].name);
            }*/

            foreach (GameObject drop in ItemsToDrop)
            {
                //Debug.Log("get gameobject " + drop.name);
                if (drop.name == item.ObjectSlug)
                {
                    Instantiate(drop, transform.position, drop.transform.rotation);

                }
            }
            //SlottedItem instance = Instantiate(slottedItem, transform.position, Quaternion.identity);
            //instance.ItemDrop = item;
            //Instantiate(item, transform.position, Quaternion.identity); //drop it.
        }
        else {

        }
        //Instantiate(drop, transform.position, drop.transform.rotation);
    }

    /*private void Instantiate(Item item, Vector3 position, Quaternion identity)
    {
        throw new NotImplementedException();
    }*/
}
