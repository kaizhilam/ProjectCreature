using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    private int MovementSpeed;
    private string EnemyName;
    EnemyScriptable enemyStats;
    public Vector3 pos;

    public int Health { get => health; set => health = value; }
    public int MovementSpeed1 { get => MovementSpeed; set => MovementSpeed = value; }
    public string EnemyName1 { get => EnemyName; set => EnemyName = value; }

    public Enemy()
    {
        //this.transform.position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public abstract Enemy Clone();


    
}
