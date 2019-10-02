using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armbow : Weapon
{
    private void Awake()
    {
        wieldPos = new Vector3(-0.005f,0.0251f,-0.006f);
        wieldRotation = Quaternion.Euler(new Vector3(-4.8f, 181.9f, -55.4f));
        wieldScale = new Vector3(0.03f, 0.03f, 0.03f);
        wieldBone = -1;
    }
    public override void Attack()
    {
        print("attacking with armbow");
        //SHOOT STUFF
        Projectile arrow = ProjPool.Instance.Get();
        arrow.gameObject.SetActive(true);

    }

    public override void RunAbility()
    {
        print("running ability on armbow");
    }
}
