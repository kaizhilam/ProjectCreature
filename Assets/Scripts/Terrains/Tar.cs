using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tar : MonoBehaviour
{
	private float _TempPlayerMovement;
	public float SlowDownFactor;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_TempPlayerMovement = other.gameObject.GetComponent<PlayerMovement>().Speed;
			other.gameObject.GetComponent<PlayerMovement>().Speed = _TempPlayerMovement / SlowDownFactor;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerMovement>().Speed = _TempPlayerMovement;
		}
	}
}
