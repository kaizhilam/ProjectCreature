using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public int MAX_DJUMP = 1;
    public int currentjump;
    private CharacterController _Controller;
    private float JumpHeight = 10f;
    private Vector3 _JumpAmount = new Vector3(0, 0, 0);
    private bool DJ;
    PlayerMovement PlayerMovement = new PlayerMovement();
    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.Space += DJump;
        _Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DJ)
        {
            _Controller.Move(_JumpAmount * Time.deltaTime);
        }

        if (_Controller.isGrounded == true)
        {
            currentjump = 0;
            DJ = false;
        }
    }
    void DJump()
    {
        if (_Controller.isGrounded == false && currentjump < MAX_DJUMP)
        {
            _JumpAmount.y = JumpHeight;
            currentjump++;
            DJ = true;
        }
    }
}
