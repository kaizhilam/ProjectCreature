using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : ProjectileAbility
{
    public float speed;
    public GameObject prefab;
    
    public FireAbility()
    {
        this.speed = 10;
        this.prefab = (GameObject)Resources.Load("Fireball");
    }

    public override void Shoot()
    {
        base.Run(speed, prefab);
    }
}
