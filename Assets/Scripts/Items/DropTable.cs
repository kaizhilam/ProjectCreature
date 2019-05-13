using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable
{
    /*public List<LootDrop> loot;

    public Item GetDrop()
    {
        int roll = Random.Range(0,101);
        int WeightSum = 0;
        foreach (LootDrop drop in loot)
        {
            WeightSum += drop.Weight;
            if (roll < WeightSum)
            {
                return itemdatabase.instance.getitem(drop.ItemSlug);
            }
        }
        return null;
    }*/
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
