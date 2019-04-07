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
    private ProjectileAbility proj = null;
    Ability[] abilities;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        _CameraFace = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _Animator = GetComponentInChildren<Animator>();
        //examples of how to assign power
        abilities = new Ability[3];
        abilities[0] = new JumpAbility();
        proj = new FireAbility();
    }

    public void Update()
    {
        GetInputs();
        if (Input.GetMouseButton(0))
        {
            Debug.Log("clicking lmb");
            Debug.Log("trying to fire proj");
            proj.Shoot();
        }
        
    }

    public void FixedUpdate()
    {
        Jump();
        Movement();
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
        float jump = JumpHeight;
        if (_Jump   == 1 && _CanJump == true)
        {
            _Animator.SetBool("PlayerIdle", false);
            _Animator.SetTrigger("PlayerJump");
            _CanJump = false;
            _Rb.AddForce(new Vector3(0f, jump, 0f));
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
                //Debug.Log("Running");
                movement = new Vector3(_Horizontal * MovementSpeed * 1.2f * Time.deltaTime, 0, _Vertical * MovementSpeed * 1.2f * Time.deltaTime);
            }
            else
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", true);
                _Animator.SetBool("PlayerRun", false);
                //Debug.Log("Walking");
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

    private void GetInputs()
    {
        _Jump = Input.GetAxisRaw("Jump"); //get space keys
        _Horizontal = Input.GetAxisRaw("Horizontal"); //get A,W keys
        _Vertical = Input.GetAxisRaw("Vertical"); //get W, S keys
    }
}
