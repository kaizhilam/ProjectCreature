using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Projectile Projectile;
    private ProjectileScriptableObject _Type;

    private bool _CanShoot = true;
    private float _CanShootTimer = 0;

    private void Update()
    {
        _Type = gameObject.GetComponentInParent<PlayerStat>().ProjectileAbility;
        CheckCanShoot();
    }

    private void LateUpdate()
    {
        Shoot();
    }

    private void CheckCanShoot()
    {
        _CanShootTimer += Time.deltaTime;
        if (_CanShootTimer >= _Type.RechargeTimer)
        {
            _CanShoot = true;
        }
    }

    private void Shoot()
    {
        float fire = Input.GetAxisRaw("Fire1");
        if (fire == 1 && Projectile != null && _CanShoot == true)
        {
            _CanShoot = false;
            _CanShootTimer = 0;
            Instantiate(Projectile, transform.position, transform.rotation);
        }
    }
}
