using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAbility : ActiveAbility
{
    private float _Timer;
    private float ActiveTime;
    private float speed;
    private GameObject prefab;

    public ProjectileAbility()
    {
       
    }

    public abstract void Shoot();

    public void Run(float speed, GameObject prefab)
    {
        this.speed = speed;
        this.prefab = prefab;
        if (ThirdPersonCamera.LookingAtPoint.ToString() != Vector3.positiveInfinity.ToString())
           transform.LookAt(ThirdPersonCamera.LookingAtPoint);
        Instantiate(prefab, this.transform);
    }

    protected virtual void Update()
    {
        Despawn();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        _Timer += Time.deltaTime;
        
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    protected void Despawn()
    {
        if (_Timer >= ActiveTime)
        {
            Destroy(gameObject);
        }
    }
}
