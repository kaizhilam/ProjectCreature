using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spider : ForestEnemy
{

    private AudioSource src;
    public GameObject[] ItemsToDrop;
    public DropTable DropTable { get; set; }

    public Spider()
    {

        Health = 150;
        Damage = 30;
        MovementSpeed1 = 7;
        EnemyName1 = "spider";

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
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop> //set loots in enemy
        {
            new LootDrop(ItemsToDrop[0].name,100)
        };

        src = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            this.TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
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
        else
        {

        }
        //Instantiate(drop, transform.position, drop.transform.rotation);
    }
}
