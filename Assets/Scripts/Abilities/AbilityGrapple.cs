﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AbilityGrapple : Ability
{
	public float Speed = 10;
	public float MaxGrappleDistance = 100;

	private GameObject _Camera;
	private GameObject _Player;
	private CharacterController _Controller;
	private PlayerMovement _PlayerMovement;
	private bool _Grappling = false;
	private Vector3 _OriginalGravity;
	void Start()
    {
		_Camera = GameObject.FindGameObjectWithTag("MainCamera");
		_Controller = GetComponent<CharacterController>();
		_Player = GameObject.FindGameObjectWithTag("Player");
		_PlayerMovement = _Player.GetComponent<PlayerMovement>();
		_OriginalGravity = _PlayerMovement.Gravity;
		InputManager.instance.RightClick += EnableGrapple; //Change this for grapple bind
	}

	void Update()
    {
		if (_Grappling == true)
		{
			Grapple();
			if (Player.LookingAtDistance <= 2)
			{
				_Grappling = false;
				PlayerMovement.CanMove = true;
				ThirdPersonCamera.CameraLock = false;
				PlayerMovement.EnableGravity = true;
			}
		}
    }

	private void EnableGrapple()
	{
        if (ThirdPersonCamera.LookingAtDistance <= MaxGrappleDistance &&
            !ThirdPersonCamera.isLookingAtSky && !_Grappling && ThirdPersonCamera.LookingAtPoint.y > this.gameObject.transform.position.y+2 && 
            ThirdPersonCamera.LookingAtGameObject.tag!="Ground")
		{
            //play grapple sound here brad
			_Grappling = true;
		}
	}

    public bool Getgrapple()
    {
        if (_Grappling)
        {
            return true;
        }
        else
            return false;
    }

    private void Grapple()
	{
		PlayerMovement.CanMove = false;
		ThirdPersonCamera.CameraLock = true;
		PlayerMovement.EnableGravity = false;
		float delta = Time.deltaTime;
		//Make player face where the camera is facing
		Vector3 cameraForward = _Camera.transform.forward;
		//Debug.Log(cameraForward);
		transform.forward = cameraForward;
		/* Make the player look forward
		 * BUG FOUND: Player grapple supper fast: SOLVED
		 * */
		//Make the player look forward instead of looking where the camera is pointing (x axis)
		//Vector3 temp = GameObject.Find("Idle").transform.eulerAngles;
		Vector3 temp = transform.GetChild(0).eulerAngles;
		temp.x = 0;
		transform.eulerAngles = temp;
		//Move the player
		_Controller.Move(cameraForward.normalized * Speed * delta);
		
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (_Grappling == true && !_Controller.isGrounded)
		{
			_Grappling = false;
			PlayerMovement.CanMove = true;
			ThirdPersonCamera.CameraLock = false;
			PlayerMovement.EnableGravity = true;
		}
	}
}
