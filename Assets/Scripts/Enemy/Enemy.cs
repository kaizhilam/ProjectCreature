using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    private int MovementSpeed;
    private string EnemyName;
    public EnemyScriptable enemyStats;

    public int Health { get => health; set => health = value; }
    public int MovementSpeed1 { get => MovementSpeed; set => MovementSpeed = value; }
    public string EnemyName1 { get => EnemyName; set => EnemyName = value; }

    // Start is called before the first frame update
    void Start()
    {
        

    }



    
}
