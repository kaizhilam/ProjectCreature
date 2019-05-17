using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable
{
    public List<LootDrop> loot;

    public Item GetDrop()
    {
        int roll = Random.Range(0,101); //get a random number from 0 to 100
        int WeightSum = 0;
        foreach (LootDrop drop in loot) //searching which item to drop
        {
            WeightSum += drop.Weight; 
            if (roll < WeightSum)
            {
                return ItemDatabase.Instance.GetItem(drop.ItemSlug); //hopefully, this mwthod can get the item from item database and return it.
            }
        }
        return null;
    }
}

public class LootDrop
{
    public string ItemSlug { get; set; }
    public int Weight { get; set; }

    public LootDrop(string ItemSlug, int Weight)
    {
        this.ItemSlug = ItemSlug;
        this.Weight = Weight;

    }
}
