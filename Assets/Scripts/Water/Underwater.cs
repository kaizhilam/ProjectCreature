using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
	private Transform _Camera;
    private Transform _Player;
	private Collider _Collider;
	private bool _CheckCamera = false;
    
    

    public static Underwater instance = null;
        
	private void Start()
	{
		_Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
        _Collider = GetComponent<Collider>();
	}

        

    private void Update()
	{
        if (_CheckCamera)
		{
             if (_Camera.position.y > transform.position.y)
             {
                UnderwaterManager.isUnderwater = false;
                 //SetNormal();
                 if (_Player.position.y > transform.position.y)
                 {
                      //Debug.Log("Bug fixed");
                      _CheckCamera = false;
                 }                   
             }
             else if (_Camera.position.y < transform.position.y)
             {
                UnderwaterManager.isUnderwater = true;
                 //SetUnderwater();
             }     
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player touched water, CC enabled");
            _CheckCamera = true;
        }
        else if (_Camera.position.y > transform.position.y)
        {
            UnderwaterManager.isUnderwater = false;
            //SetNormal();
        }
        else if (_Camera.position.y < transform.position.y)
        {
            UnderwaterManager.isUnderwater = true;
            //SetUnderwater();
            
        }
        
    }

    
}
