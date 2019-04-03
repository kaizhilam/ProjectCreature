using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : Enemy
{
    public EnemyScriptable enemyStats;
    public Vector3 position;
    public FirstEnemy(EnemyScriptable enemyStats, Vector3 position) : base(enemyStats, position)
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
