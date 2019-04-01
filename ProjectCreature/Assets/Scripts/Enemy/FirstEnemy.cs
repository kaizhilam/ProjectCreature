using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : Enemy
{
    public FirstEnemy()
    {
        
    }

    public void Start()
    {
        enemyStats = GetComponent<EnemyScriptable>();
        Health = enemyStats.health;
        MovementSpeed1 = enemyStats.MovementSpeed;
        EnemyName1 = enemyStats.EnemyName;
    }

}
