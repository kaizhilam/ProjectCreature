using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingKnife : Weapon
{
    private void Awake()
    {
        wieldPos = new Vector3(-0.0159f, 0.0342f, 0.0067f);
        wieldRotation = Quaternion.Euler(new Vector3(185.8f, 10.96f, 4.411f));
        wieldScale = new Vector3(0.02f, 0.02f, 0.02f);
        wieldBone = 0;
    }

    public override void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(daggerHitbox.transform.position, daggerHitbox.transform.lossyScale);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Enemy"))
            {
                colliders[i].gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public override void RunAbility()
    {
        print("running knife ability");
    }

}