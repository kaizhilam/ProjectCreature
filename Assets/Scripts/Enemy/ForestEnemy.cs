using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEnemy : Enemy
{
    public override void ResolveDeletion()
    {
        Debug.Log("enemy killed");
        DropWeapon();
        ForestEnemyPool.Instance.ReturnToPool(this);
    }

    public virtual void DropWeapon() { }

}
