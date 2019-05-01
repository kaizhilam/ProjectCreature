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
		Transform shootfrom = _Player.transform.Find("ShootFrom").transform;
		if (Input.GetAxisRaw("Fire1") == 1f && _Timer >= coolDownTime && PlayerStat.SelectedAbility is FireballAbility)
		{
			_Timer = 0;
            MonoBehaviour.Instantiate(projectile, shootfrom.position, shootfrom.rotation); //fix this
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
