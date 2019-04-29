using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleAbility : PassiveAbility
{
	private GameObject _Player;
	public override void End()
	{
		//_Player.GetComponent<PlayerController>().name = "Player";
        Renderer rend = _Player.GetComponentInChildren<Renderer>();
        if (rend.enabled)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
        }
    }

	public override void Init()
	{
		_Player = GameObject.FindGameObjectWithTag("Player");
        Renderer rend = _Player.GetComponentInChildren<Renderer>();
        if (rend.enabled)
        {
            rend.enabled = false;
        }
        else {
            rend.enabled = true;
        }
	}

	public override void Run()
	{
	}

	public override string ToString()
	{
		return "Invisible";
	}
}
