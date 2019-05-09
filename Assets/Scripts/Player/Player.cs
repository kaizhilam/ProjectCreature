using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    BoxCollider daggerHitbox;

    public static float HP = 100f;

    private void Start()
    {
        daggerHitbox = GetComponent<BoxCollider>();
        GetComponentInChildren<Weapon>().gameObject.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
    }

    private void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(daggerHitbox.center, daggerHitbox.size);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Enemy"))
            {
                if (daggerHitbox.bounds.Intersects(colliders[i].bounds))
                {
                    Debug.Log(colliders[i].name);
                }
            }
        }
    }
}
