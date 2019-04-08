using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
	public string Name;
	public float Damage;
	public float Speed;
	public float ActiveTime;
	public float CoolDownTime;

	private float _Timer = 0;

	protected virtual void Start()
	{
		if (ThirdPersonCamera.LookingAtPoint.ToString() != Vector3.positiveInfinity.ToString())
		{
			transform.LookAt(ThirdPersonCamera.LookingAtPoint);
		}
	}

	protected virtual void Update()
	{
		transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		_Timer += Time.deltaTime;

		if (_Timer >= ActiveTime)
		{
			Destroy(gameObject);
		}
	}

	protected void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}
}
