using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : ActiveAbility
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
		GameObject projectile = (GameObject)Resources.Load("Projectile/Fireball");
		float coolDownTime = projectile.GetComponent<FireballBehavior>().CoolDownTime;
		_Player = GameObject.FindGameObjectWithTag("Player");
		if (Input.GetAxisRaw("Fire1") == 1f && _Timer >= coolDownTime && PlayerStat.SelectedAbility is FireballAbility)
		{
			_Timer = 0;
			Instantiate(projectile, _Player.transform.position + (Vector3.forward * 2) + (Vector3.up * 2), _Player.transform.rotation); //fix this
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
		return "Fireball";
	}
}
