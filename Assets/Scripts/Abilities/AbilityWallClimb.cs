using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWallClimb : MonoBehaviour
{
	/*TODO:
	 * 1. Add release space to stop climbing
	 * */

	/*Explaination
	 * Player start with not being able to climb
	 * When facing a wall and player is on the ground, player can climb
	 * Climb speed will decay overtime
	 * When climb speed hits 0, player will fall
	 * */
	public float ClimbSpeed;
	public float CanClimbDistance;
	public float ClimbDecayMultiplier;

	private bool _CanClimb = false;
	private bool _Climbing = false;
	private float _ClimbSpeed;
	private CharacterController _Controller;
	private LayerMask _LayerMask = 1 << 11;

	private void Start()
	{
		InputManager.instance.Space += WallClimb; //CHANGE THIS TO CHANGE BINDING
		_Controller = GetComponent<CharacterController>();
		_ClimbSpeed = ClimbSpeed;
	}

	private void Update()
	{
		//Debug.Log("CanClimb:" + _CanClimb + ", Climbing:" + _Climbing + ", ClimbSpeed:"+_ClimbSpeed); //UNCOMMENT FOR DEBUG
		if (Player.LookingAtDistance <= CanClimbDistance && _Controller.isGrounded && Player.LookingAtGameObject.tag == "Climb")
		{
			_CanClimb = true;
		}
		else
		{
			_CanClimb = false;
		}
		if (_Climbing == true)
		{
			PlayerMovement.CanMove = false;
			PlayerMovement.EnableGravity = false;
			_Controller.Move(Vector3.up * _ClimbSpeed * Time.deltaTime);
			_ClimbSpeed -= ClimbDecayMultiplier / ClimbSpeed;
			if (_ClimbSpeed <= 0 || Player.LookingAtGameObject.tag != "Climb")
			{
				_Climbing = false;
				_CanClimb = false;
				PlayerMovement.CanMove = true;
				PlayerMovement.EnableGravity = true;
				_ClimbSpeed = ClimbSpeed;
			}
		}
	}

	private void WallClimb()
	{
		if (_CanClimb == true)
		{
			_Climbing = true;
		}
	}
}
