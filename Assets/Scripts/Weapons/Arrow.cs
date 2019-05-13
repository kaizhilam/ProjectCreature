using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	private Rigidbody _Rigidbody;
    public float Speed;

	private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        transform.SetPositionAndRotation(player.transform.position + (Vector3.forward*2), player.transform.rotation);
        transform.LookAt(ThirdPersonCamera.LookingAtPoint);
    }

    private void FixedUpdate()
    {
        _Rigidbody.AddForce(Vector3.forward * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
		//Destroy(_Rigidbody);
	}
}
