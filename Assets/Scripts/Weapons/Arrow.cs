using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
	private Rigidbody _Rigidbody;
	private Collider _Collider;
	private bool _IsMoving = true;
	private Vector3 _HitTransform;
    private LayerMask allButPlayerMask; 
    public float Speed;

	private void Start()
    {
        LayerMask playerLayer = LayerMask.NameToLayer("player");
        allButPlayerMask = ~(1<<playerLayer);
        _Rigidbody = GetComponent<Rigidbody>();
		_Collider = GetComponent<Collider>();
		GameObject player = GameObject.Find("Face");
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		transform.position = player.transform.position + (player.transform.forward * 4);
		transform.rotation = camera.transform.rotation;

        //CIRCUMVENTING AIMING TOWARDS SKYBOX
		if(Physics.Raycast(ThirdPersonCamera.castRay, out RaycastHit hit,Mathf.Infinity,allButPlayerMask))
        {
            print("not looking at sky");
        }
        else
        {
            print("looking at sky");
            transform.LookAt(camera.transform.position + camera.transform.forward*Time.deltaTime*900f);

        }
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

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.gameObject.name);
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
