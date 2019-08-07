using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDoubleJump : MonoBehaviour
{
	private uint _Jump = 0;
	private CharacterController _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (_Player.isGrounded == false)
		{

		}
    }
}
