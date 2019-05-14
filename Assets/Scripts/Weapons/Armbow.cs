using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armbow : Weapon
{
    private void Awake()
    {
        wieldPos = new Vector3(0.002801714f, 0, 0.005f);
        wieldRotation = Quaternion.Euler(new Vector3(2.13f, -75.311f, 87.899f));
        wieldScale = new Vector3(0.04f, 0.04f, 0.04f);
        wieldBone = -1;
    }
    public override void Attack()
    {
        print("attacking with armbow");
		//SHOOT STUFF
		Object arrow = Resources.Load("Projectile/Arrow");
		Instantiate(arrow);
    }

    public override void RunAbility()
    {
        print("running ability on armbow");
    }
}
