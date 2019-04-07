using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbility : ProjectileAbility
{
    public float speed;
    public GameObject prefab;

    public WaterAbility()
    {
        this.speed = 20;
        this.prefab = (GameObject)Resources.Load("WaterBall");
    }

    public override void Shoot()
    {
        base.Run(speed, prefab);
    }
}
