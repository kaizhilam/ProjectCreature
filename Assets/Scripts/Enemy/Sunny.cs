using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunny : ForestEnemy
{
    public GameObject drop;
    // Start is called before the first frame update
    public Sunny()
    {
        Health = 50;
        MovementSpeed1 = 6;
        EnemyName1 = "sunny";
    }

    // Update is called once per frame
    void Start()
    {
        
    }

    public override void DropWeapon() //called, when enemy will be destroyed
    {
        Instantiate(drop, transform.position, drop.transform.rotation);
    }

}
