using UnityEngine;
using System.Collections;
/// <summary>
/// 消耗品类
/// </summary>
public class Consumable : Item
{

    public int HP { get; set; }
    public int MP { get; set; }

    public Consumable(int id, string name, string des, ItemType type, int capacity, int hp, int mp)
        :base(id, name, des, type, capacity)
    {
        objID = id;
        objName = name;
        this.HP = hp;
        this.MP = mp;
        //Type = ItemType.Consumable;
    }

    /*
    public override string ToString()
    {
        string s = "";
        s += objID.ToString();
        s += Type;        
        s += description;       
        s += HP;
        s += MP;
        return s;
    }
    */

}
