﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float HP = 100f;
    //public ProjectileScriptableObject ProjectileAbility;
    public PlayerInventory<string> Inventory = new PlayerInventory<string>(); //change int to whatever type item is 
}
