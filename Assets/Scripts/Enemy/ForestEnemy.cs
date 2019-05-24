using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEnemy : Enemy
{
    public override void ResolveDeletionDropItem()
    {
        DropWeapon();
        ForestEnemyPool.Instance.ReturnToPool(this);
    }

    public override void ResolveDeletion()
    {
        ForestEnemyPool.Instance.ReturnToPool(this);
    }


    public virtual void DropWeapon() { }

}
