using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	private Rigidbody _Rigidbody;
	private Collider _Collider;
	private bool _IsMoving = true;
	private Vector3 _HitTransform;

    public float Speed;
	public float TimeToDissapear;

	private void Start()
    {
		_Rigidbody = GetComponent<Rigidbody>();
		_Collider = GetComponent<Collider>();
		GameObject player = GameObject.Find("Face");
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		transform.position = player.transform.position + (player.transform.forward * 4);
		transform.rotation = camera.transform.rotation;

		//CIRCUMVENTING AIMING TOWARDS SKYBOX
		Vector3 lookingAt = ThirdPersonCamera.LookingAtPoint;
		if (lookingAt.ToString() != "(Infinity, Infinity, Infinity)")
		{
			transform.LookAt(lookingAt);
		}

		_Rigidbody.AddForce(transform.forward * Speed);
	}

	private void FixedUpdate()
	{
		if (_IsMoving == true)
		{
			_Rigidbody.AddForce(transform.forward * Speed);
		}
		else
		{
			transform.position = _HitTransform;
		}
	}

	private void Update()
	{
		if (_IsMoving == false)
		{
			TimeToDissapear -= Time.deltaTime;
		}
		if (TimeToDissapear <= 0)
		{
			Destroy(gameObject); //CHNAGE THIS FOR THE POOLING THINGO
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "Player")
		{
			_HitTransform = transform.position;
			_IsMoving = false;
			_Rigidbody.velocity = Vector3.zero;
			Destroy(_Rigidbody);
			Destroy(_Collider);
		}
	}
}
