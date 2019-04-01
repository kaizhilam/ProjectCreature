﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpHeight = 200f;
    public float MovementSpeed = 10f;

    private float _Jump;
    float _Horizontal;
    float _Vertical;
    private bool _CanJump = false;
    private bool _CanWalk = false;
    private Transform _CameraFace;
    private Rigidbody _Rb;
    private Animator _Animator;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        _CameraFace = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _Animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        GetInputs();
    }

    public void FixedUpdate()
    {
        Jump();
        Walk();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _Animator.SetBool("PlayerIdle", true);
            _CanJump = true;
            _CanWalk = true;
        }
    }

    private void Jump()
    {
        if (_Jump == 1 && _CanJump == true)
        {
            _Animator.SetBool("PlayerIdle", false);
            _Animator.SetTrigger("PlayerJump");
            _CanJump = false;
            _Rb.AddForce(new Vector3(0f, JumpHeight, 0f));
        }
    }

    private void Walk()
    {
        if (_CanWalk && (_Vertical != 0 || _Horizontal != 0/* || fire == 1*/)) //update rotation of the character when WASD is pressed
        {
            _Animator.SetBool("PlayerIdle", false);
            _Animator.SetBool("PlayerWalk", true);
            transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
            Vector3 movement = new Vector3(_Horizontal * MovementSpeed * Time.deltaTime, 0, _Vertical * MovementSpeed * Time.deltaTime);
            _Rb.transform.Translate(movement); //move the character
        }
        else
        {
            _Animator.SetBool("PlayerIdle", true);
            _Animator.SetBool("PlayerWalk", false);
        }
    }

    private void GetInputs()
    {
        _Jump = Input.GetAxisRaw("Jump"); //get space keys
        _Horizontal = Input.GetAxisRaw("Horizontal"); //get A,W keys
        _Vertical = Input.GetAxisRaw("Vertical"); //get W, S keys
    }
}

//public class PlayerController : MonoBehaviour //NEED TO FIX ANIMATION
//{
//    public float JumpHeight = 200f; //jumping force of the player
//    public float MovementSpeed = 10f; //movement speed of the player

//    private Transform _CameraFace; //drag child object onto here, need it for camera movement
//    private Rigidbody _Rb;
//    private Animator _Animator;
//    private bool _CanJump = true;
//    private readonly int aIdle = 0, aWalk = 1, aRun = 2, aJump = 3, aShoot = 4;
//    private bool _CanShoot = true;
//    private float _CanShootTimer = 0; 

//    private void Start()
//    {
//        _Rb = GetComponent<Rigidbody>();
//        _Rb.angularDrag = 0;
//        _CameraFace = GameObject.FindGameObjectWithTag("MainCamera").transform;
//        _Animator = GetComponentInChildren<Animator>();
//    }

//    private void FixedUpdate()
//    {
//        PhysicsMovementController();
//    }

//    private void Update()
//    {
//        Shoot();
//    }

//    private void PhysicsMovementController()
//    {
//        float horizontal = Input.GetAxisRaw("Horizontal"); //get A,W keys
//        float vertical = Input.GetAxisRaw("Vertical"); //get W, S keys
//        float jump = Input.GetAxisRaw("Jump"); //get <Space> key
//        float fire = Input.GetAxisRaw("Fire1");

//        //jump mechanics
//        if (jump == 1 && _CanJump == true) 
//        {
//            _CanJump = false;
//            _Rb.AddForce(new Vector3(0f, JumpHeight, 0f));
//        }
//        //movement mechanics
//        if (vertical != 0 || horizontal != 0 || fire == 1) //update rotation of the character when WASD is pressed
//        {
//            transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
//        }
//        //animation script
//        if (fire == 1)
//        {
//            _Animator.SetTrigger("PlayerStateShoot"); 
//        }
//        else if (jump == 1)
//        {
//            _Animator.SetTrigger("PlayerStateJump");
//        }
//        else if (vertical != 0 || horizontal != 0)
//        {
//            _Animator.SetInteger("PlayerState", aWalk);
//        }
//        else
//        {
//            _Animator.SetInteger("PlayerState", aIdle);
//        }
//        Vector3 movement = new Vector3(horizontal * MovementSpeed * Time.deltaTime, 0, vertical * MovementSpeed * Time.deltaTime); 
//        _Rb.transform.Translate(movement); //move the character
//    }

//    private void Shoot()
//    {
//        if (_CanShoot == true)
//        {
//            _CanShoot = false;
//            //enter shoot script here
//        }
//        else
//        {
//            _CanShootTimer += Time.deltaTime;
//        }
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "Ground") //reset jumping once the player touches the ground
//        {
//            _CanJump = true;
//        }
//    }
//}
