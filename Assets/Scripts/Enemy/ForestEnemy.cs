using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEnemy : Enemy
{
    public override void ResolveDeletion()
    {
        ForestEnemyPool.Instance.ReturnToPool(this);
    }
}
