using UnityEngine;
using System.Collections;

public class Weapon : Item {

    public int Damage { get; set; }

    public WeaponType WpType { get; set; }

    public Weapon(int id, string name, string des, ItemType type, int capacity, int damage, WeaponType wpType)
        : base(id, name, des, type, capacity)
    {
        this.Damage = damage;
        this.WpType = wpType;
    }

    public enum WeaponType
    {
        None,
        Wond,
        Sword
    }

}
