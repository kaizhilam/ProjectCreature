using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingKnife : Weapon
{


    public override void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(daggerHitbox.transform.position, daggerHitbox.transform.lossyScale);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Enemy"))
            {
                print(colliders[i].name);
                colliders[i].gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public override void RunAbility()
    {
        throw new System.NotImplementedException();
    }

}
