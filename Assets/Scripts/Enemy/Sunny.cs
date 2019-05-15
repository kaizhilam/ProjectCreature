using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sunny : ForestEnemy
{
    //public GameObject drop;
    public GameObject[] ItemsToDrop;

    public DropTable DropTable { get; set; }
    //public SlottedItem slottedItem;
    // Start is called before the first frame update
    public Sunny()
    {
        Health = 50;
        MovementSpeed1 = 6;
        EnemyName1 = "sunny";
    }

    // Update is called once per frame
    void Start()
    {
        DropTable = new DropTable(); 
        DropTable.loot = new List<LootDrop> //set loots in enemy
        {
            new LootDrop("BasicKnife",25), //25% chance to get a knife
            new LootDrop("armbow",25), //50% chance to get an arrow
            new LootDrop("Cube",25) //75% chance to get its head
        };
    }

    public override void DropWeapon() //called, when enemy will be destroyed
    {
        Item item = DropTable.GetDrop(); //hopefully, it get the rolled item or do nothing(if item == null)
        
        if (item != null)
        {
            Debug.Log("get item " + item.ObjectSlug);
            foreach (GameObject drop in ItemsToDrop)
            {
                Debug.Log("get drop " + drop.name);
                if (drop.name == item.ObjectSlug)
                {
                    Instantiate(drop, transform.position, drop.transform.rotation);
                    
                }
            }
            //SlottedItem instance = Instantiate(slottedItem, transform.position, Quaternion.identity);
            //instance.ItemDrop = item;
            //Instantiate(item, transform.position, Quaternion.identity); //drop it.
        }
        //Instantiate(drop, transform.position, drop.transform.rotation);
    }

    /*private void Instantiate(Item item, Vector3 position, Quaternion identity)
    {
        throw new NotImplementedException();
    }*/
}
