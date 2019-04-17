﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
	private Transform _Camera;
    private Transform _Player;
	private Collider _Collider;
	private bool _CheckCamera = false;
    private bool isUnderwater;
	private void Start()
	{
		_Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
        _Collider = GetComponent<Collider>();
	}

	private void Update()
	{
        if (_CheckCamera == true)
		{
                if (_Camera.position.y > transform.position.y)
                {
                    //isUnderwater = false;
                    SetNormal();
                    
                }
                else if (_Camera.position.y < transform.position.y)
                {
                    isUnderwater = true;
                    SetUnderwater();
                }
                
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched water, CC enabled");
            _CheckCamera = true;
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if (_Player.position.y > transform.position.y)
        {
            isUnderwater = false;
            _CheckCamera = false;
            Debug.Log("CheckCamera Off");
            SetNormal();
        }
        else if (_Player.position.y > transform.position.y)
        {
            isUnderwater = true;
            SetUnderwater();
        }
    }

    private void SetUnderwater()
	{
        //Debug.Log("SetUnderwater Works");
        if (isUnderwater == true)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
            RenderSettings.fogDensity = 0.1f;
        }
    }

	private void SetNormal()
	{
		//Debug.Log("SetNormal Works");
		RenderSettings.fog = false;
		RenderSettings.fogColor = new Color(0.5f, 0.5f, 0.5f, 1f);
		RenderSettings.fogDensity = 0.01f;
	}
}
