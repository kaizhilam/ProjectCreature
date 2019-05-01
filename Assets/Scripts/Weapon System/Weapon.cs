using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    public float durability;
    public Image icon;
    public GameObject prefab;
    public float damage;

    public abstract void RunAbility();

    public abstract void Attack();

}
