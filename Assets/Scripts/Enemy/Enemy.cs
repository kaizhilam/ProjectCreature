using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    private int MovementSpeed;
    private string EnemyName;
    EnemyScriptable enemyStats;
    public Transform position;

    public int Health { get => health; set => health = value; }
    public int MovementSpeed1 { get => MovementSpeed; set => MovementSpeed = value; }
    public string EnemyName1 { get => EnemyName; set => EnemyName = value; }

    public Enemy(EnemyScriptable enemyStats, Vector3 position)
    {
        this.transform.position = position;
        this.enemyStats = enemyStats;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public abstract Enemy Clone();


    
}
