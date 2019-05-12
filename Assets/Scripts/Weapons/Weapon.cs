﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : SlottedItem
{
    public float durability;
    public float damage;

    public abstract void RunAbility();

    public abstract void Attack();

}
