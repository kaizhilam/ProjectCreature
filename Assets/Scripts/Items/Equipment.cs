using UnityEngine;
using System.Collections;

public class Equipment : Item {


    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }

    public EquipmentType EquipType { get; set; }

    public Equipment(int id, string name, string des, ItemType type, int strength,int intellect,int agility,int stamina, EquipmentType equipType)
        : base(id, name, des, type)
    {
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipType = equipType;
    }

    public enum EquipmentType
    {
        None,
        Head,
        Chest,
        Ring,
        Leg,
        Boots
    }
}
