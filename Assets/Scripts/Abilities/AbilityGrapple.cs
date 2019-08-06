using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AbilityGrapple : MonoBehaviour
{
	/*TODO:
	 * 1. Add disable gravity to grapple upwards
	 * 2. During grapple, lock direction
	 * */
	public float Speed;
	public float GrappleDistance;

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
		InputManager.instance.RightClick += EnableGrapple;
	}

	void Update()
    {
		if (_Grappling == true)
		{
			//_PlayerMovement.Gravity = Vector3.zero;
			Grapple();
			if (ThirdPersonCamera.LookingAtDistance <= 10f)
			{
				//_PlayerMovement.Gravity = _OriginalGravity;
				_Grappling = false;
			}
		}
    }

	private void EnableGrapple()
	{
		if (ThirdPersonCamera.LookingAtDistance < GrappleDistance)
		{
			_Grappling = true;
		}
	}

	private void Grapple()
	{
		float delta = Time.deltaTime;
		//Make player face where the camera is facing
		Vector3 cameraForward = _Camera.transform.forward;
		Debug.Log(cameraForward);
		cameraForward.y = 0;
		transform.forward = cameraForward;
		//Move the player
		_Controller.Move(cameraForward.normalized * Speed * delta);
	}
}
