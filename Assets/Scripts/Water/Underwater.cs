using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
	private Transform _Camera;
	private Collider _Collider;
	private bool _CheckCamera = false;
	private void Start()
	{
		_Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
		_Collider = GetComponent<Collider>();
	}

	private void Update()
	{
		if (_CheckCamera == true)
		{
			if (_Camera.position.y > transform.position.y)
			{
				SetNormal();
			}
			else
			{
				SetUnderwater();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			_CheckCamera = true;
		}
	}

	private void SetUnderwater()
	{
		Debug.Log("SetUnderwater Works");
		RenderSettings.fog = true;
		RenderSettings.fogColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
		RenderSettings.fogDensity = 0.1f;

	}

	private void SetNormal()
	{
		Debug.Log("SetNormal Works");
		RenderSettings.fog = false;
		RenderSettings.fogColor = new Color(0.5f, 0.5f, 0.5f, 1f);
		RenderSettings.fogDensity = 0.01f;
	}
}
