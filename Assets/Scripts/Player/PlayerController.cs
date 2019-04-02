using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpHeight = 200f;
    public float MovementSpeed = 10f;

    private float _Jump;
    private float _Horizontal;
    private float _Vertical;
    private float _Fire;
    private bool _CanJump = false;
    private bool _CanWalk = false;
    private bool _CanFire = true;
    private float _FireRechargeTime;
    private float _FireRechargeTimer = 0f;
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
        _FireRechargeTime = 3f; //set fire recharge time here
        GetInputs();
    }

    public void FixedUpdate()
    {
        Jump();
        Movement();
        Fire();
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

    private void Movement()
    {
        if (_CanWalk && (_Vertical != 0 || _Horizontal != 0)) //update rotation of the character when WASD is pressed
        {
            Vector3 movement;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", false);
                _Animator.SetBool("PlayerRun", true);
                Debug.Log("Running");
                movement = new Vector3(_Horizontal * MovementSpeed * 1.2f * Time.deltaTime, 0, _Vertical * MovementSpeed * 1.2f * Time.deltaTime);
            }
            else
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", true);
                _Animator.SetBool("PlayerRun", false);
                Debug.Log("Walking");
                movement = new Vector3(_Horizontal * MovementSpeed * Time.deltaTime, 0, _Vertical * MovementSpeed * Time.deltaTime);
            }
            transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
            _Rb.transform.Translate(movement); //move the character
        }
        else
        {
            _Animator.SetBool("PlayerIdle", true);
            _Animator.SetBool("PlayerWalk", false);
            _Animator.SetBool("PlayerRun", false);
        }
    }

    private void Fire()
    {
        if (_Fire == 1)
        {
            transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
        }
        if (_FireRechargeTimer > _FireRechargeTime)
        {
            _CanFire = true;
        }
        if (_Fire == 1 && _CanFire)
        {
            //Debug.Log("Name: " + ThirdPersonCamera.LookingAtGameObject.name + " Point: " + ThirdPersonCamera.LookingAtPoint + " Distance: " + ThirdPersonCamera.LookingAtDistance);
            _FireRechargeTimer = 0f;
            _CanFire = false;
        }
        _FireRechargeTimer += Time.deltaTime;
    }

    private void GetInputs()
    {
        _Jump = Input.GetAxisRaw("Jump"); //get space keys
        _Horizontal = Input.GetAxisRaw("Horizontal"); //get A,W keys
        _Vertical = Input.GetAxisRaw("Vertical"); //get W, S keys
        _Fire = Input.GetAxisRaw("Fire1");
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
