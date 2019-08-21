using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public int MAX_DJUMP = 1;
    public int currentjump;
    private CharacterController _Controller;
    public float JumpHeight = 25f;
    private Vector3 _JumpAmount = new Vector3(0, 0, 0);
    private bool DJ;
    // Start is called before the first frame update
    private float _Gravity;
    void Start()
    {
        InputManager.instance.Space += DJump;
        _Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _Gravity = PlayerMovement.GetGravity;
        if (DJ)
        {
            _JumpAmount.y += _Gravity * Time.deltaTime;
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
