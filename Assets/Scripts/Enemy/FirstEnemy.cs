using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : Enemy
{
    public FirstEnemy()
    {
        Health = enemyStats.health;
        MovementSpeed1 = enemyStats.MovementSpeed;
        EnemyName1 = enemyStats.EnemyName;
    }

    public void Start()
    {
        
    }

    public override Enemy Clone()
    {
        FirstEnemy cloned = (FirstEnemy)this.MemberwiseClone();
        cloned.MovementSpeed1 = this.MovementSpeed1;
        cloned.EnemyName1 = this.EnemyName1;
        cloned.Health = this.Health;
        return cloned as Enemy;
    }

}
