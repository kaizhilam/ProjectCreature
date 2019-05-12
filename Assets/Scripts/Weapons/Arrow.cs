using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	//private Rigidbody _Rigidbody;

	private void Start()
    {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		//transform.SetPositionAndRotation(player.transform.position + (Vector3.forward * 5), player.transform.rotation);
	}

	private void Update()
    {
    }

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
		//Destroy(_Rigidbody);
	}
}
