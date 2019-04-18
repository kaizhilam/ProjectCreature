using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : Enemy
{
    public Dino(): base()
    {
        
    }

    public void Start()
    {
        enemyStats = GetComponent<Enemy>().enemyStats;
        Health = enemyStats.health;
        MovementSpeed1 = enemyStats.MovementSpeed;
        EnemyName1 = enemyStats.EnemyName;
    }
    public void Awake()
    {
        enemyStats = GetComponent<Enemy>().enemyStats;
    }
}
