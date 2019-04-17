using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : ActiveAbility
{
	private GameObject _Player;
	private float _Timer = 0;

	public override void End()
	{

	}

	public override void Init()
	{
		_Timer = 0;
	}

	public override void Run()
	{
		GameObject projectile = (GameObject)Resources.Load("Projectile/Waterball");
		float coolDownTime = projectile.GetComponent<WaterballBehavior>().CoolDownTime;
		_Player = GameObject.FindGameObjectWithTag("Player");
		Transform shootfrom = _Player.transform.Find("Idle").Find("ShootFrom").transform;
		Debug.Log(_Player.transform.position + (Vector3.forward * 5));

		if (Input.GetAxisRaw("Fire1") == 1f && _Timer >= coolDownTime && PlayerStat.SelectedAbility is WaterballAbility)
		{
			_Timer = 0;
            MonoBehaviour.Instantiate(projectile, shootfrom.position, shootfrom.rotation);
		}
		_Timer += Time.deltaTime;
		if (_Timer >= coolDownTime)
		{
			_Timer = coolDownTime;
		}
		UITimer = coolDownTime - _Timer;
	}

	public override string ToString()
	{
		return "Waterball";
	}
}
