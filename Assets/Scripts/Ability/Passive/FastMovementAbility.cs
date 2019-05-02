using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMovementAbility : PassiveAbility
{
	private GameObject _Player;
	public override void End()
	{
		_Player.GetComponent<PlayerController>().MovementSpeed /= 3;
	}

	public override void Init()
	{
		_Player = GameObject.FindGameObjectWithTag("Player");
		_Player.GetComponent<PlayerController>().MovementSpeed *= 3;
	}

	public override void Run()
	{
	}

	public override string ToString()
	{
		return "Fast Movement";
	}
}
