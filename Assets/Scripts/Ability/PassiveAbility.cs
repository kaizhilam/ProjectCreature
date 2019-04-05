using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : Ability
{
    public abstract void Equipped();
    public abstract void Unequipped();
}
