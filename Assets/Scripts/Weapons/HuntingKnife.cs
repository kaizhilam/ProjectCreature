using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingKnife : Weapon
{
    private void Awake()
    {
        //right hand bone
        wieldPos = new Vector3(-0.0069f, -0.0766f, 0.0167f);
        wieldRotation = Quaternion.Euler(new Vector3(80.921f, 0.0f, 0.0f));
        wieldScale = new Vector3(1.2f, 1.2f, 1.2f);
        wieldBone = 0;
    }



    public override void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(daggerHitbox.transform.position, daggerHitbox.transform.lossyScale*3);
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