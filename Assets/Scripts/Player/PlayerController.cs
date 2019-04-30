using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpHeight;
    public float MovementSpeed = 10f;
    public float AirMovementSpeed = 7f;
    private Vector3 _InputVector;
    private bool _CanJump = false;
    private bool _CanWalk = false;
    private float _Fire = 0;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    //private bool _CanFire = true;
    //private float _FireRechargeTime;
    //private float _FireRechargeTimer = 0f;
    private Transform _CameraFace;
    private Rigidbody _Rb;
    private Animator _Animator;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        //getting cameras position, rotation and scale
        _CameraFace = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //for animations
        _Animator = GetComponentInChildren<Animator>();
        _InputVector = new Vector3(0, 0, 0);

    }

    public void Update()
    {
        //_FireRechargeTime = 3f; //set fire recharge time here
        //get keyboard inputs
        GetInputs();
    }

    public void FixedUpdate()
    {
        Jump();
        //wasd movement
        Movement();
		//Fire();

	}

    private void OnCollisionEnter(Collision collision)
    {
        //if player colliding with ground...
        if (collision.gameObject.tag == "Ground")
        {
            //play idle animation and make player able to jump and walk/run
            _Animator.SetBool("PlayerIdle", true);
            _CanJump = true;
            _CanWalk = true;
        }
    }

    private void Jump()
    {
        //if player presses spacebar, and is touching the ground (is allowed to jump)...
        if (Input.GetButtonDown("Jump") && _CanJump == true)
        {
            //change to jump animation
            _Animator.SetBool("PlayerIdle", false);
            _Animator.SetTrigger("PlayerJump");
            //player shouldn't be able to jump until they return to the ground again
            _CanJump = false;
            //add upwards force (jumping force)
            _Rb.AddForce(new Vector3(0f, JumpHeight, 0f));
        }
        //if player is falling...
        if (_Rb.velocity.y < 0)
        {
            //add additional gravity to the player
            _Rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //if player lets go of jump before it reaches apogee, add different amount of additional gravity
        else if(_Rb.velocity.y>0 && !Input.GetButton("Jump"))
        {
            _Rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Movement()
    {
        //if player is firing, have them face the camera on the y
		if (_Fire == 1)
		{
			transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
		}
		if (_CanWalk && _InputVector!=Vector3.zero) //update rotation of the character when WASD is pressed
        {
            Vector3 movement;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", false);
                _Animator.SetBool("PlayerRun", true);
                movement = _InputVector * MovementSpeed * 1.2f * Time.deltaTime;
            }
            else
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", true);
                _Animator.SetBool("PlayerRun", false);
                movement = _InputVector * MovementSpeed * Time.deltaTime;
            }
            transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
            _Rb.transform.Translate(movement); //move the character
        }
        //if player isn't moving, play idle animation
        else
        {
            //_Animator.SetBool("PlayerIdle", true);
            //_Animator.SetBool("PlayerWalk", false);
           // _Animator.SetBool("PlayerRun", false);
        }
    }


    private void GetInputs()
    {
        //get firing input (for projectiles)
        _Fire = Input.GetAxisRaw("Fire1");
        //get movement input - WASD
        _InputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
    }
}
