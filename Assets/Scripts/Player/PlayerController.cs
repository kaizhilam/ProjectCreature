using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpHeight;
    public float MovementSpeed = 10f;

    private Vector3 _InputVector;
    private bool _CanJump = false;
    private bool _CanWalk = false;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    //private bool _CanFire = true;
    //private float _FireRechargeTime;
    //private float _FireRechargeTimer = 0f;
    private Transform _CameraFace;
    private Rigidbody _Rb;
    private Animator _Animator;
	private Ability[] _CompareAbilities;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        _CameraFace = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _Animator = GetComponentInChildren<Animator>();
		_CompareAbilities = PlayerStat.Abilities;
        _InputVector = new Vector3(0, 0, 0);
        AbilityInit();

    }

    public void Update()
    {
        //_FireRechargeTime = 3f; //set fire recharge time here
        GetInputs();
		AbilityMethod();
    }

    public void FixedUpdate()
    {
        Jump();
        Movement();
		//Fire();

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

	private void AbilityMethod()
	{
		bool pressedOne = Input.GetKeyDown(KeyCode.Alpha1);
		bool pressedTwo = Input.GetKeyDown(KeyCode.Alpha2);
		bool pressedThree = Input.GetKeyDown(KeyCode.Alpha3);
		if (pressedOne)
		{
			AbilityEnd();
			if ((PlayerStat.Abilities[0] is ActiveAbility))
			{
				PlayerStat.AbilitiesIndex = 0;
			}
			else
			{
				Debug.Log("Can't choose passive ability");
			}
			AbilityInit();
		}
		if (pressedTwo)
		{
			AbilityEnd();
			if ((PlayerStat.Abilities[1] is ActiveAbility))
			{
				PlayerStat.AbilitiesIndex = 1;
			}
			else
			{
				Debug.Log("Can't choose passive ability");
			}
			AbilityInit();
		}
		if (pressedThree)
		{
			AbilityEnd();
			if ((PlayerStat.Abilities[2] is ActiveAbility))
			{
				PlayerStat.AbilitiesIndex = 2;
			}
			else
			{
				Debug.Log("Can't choose passive ability");
			}
			AbilityInit();
		}
		AbilityRun();
		if (_CompareAbilities != PlayerStat.Abilities)
		{
			AbilityEnd();
			_CompareAbilities = PlayerStat.Abilities;
			AbilityInit();
		}
	}

	private void AbilityInit()
	{
		for (int i = 0; i < _CompareAbilities.Length; i++)
		{
			_CompareAbilities[i].Init();
		}
	}

	private void AbilityRun()
	{
		for (int i = 0; i < _CompareAbilities.Length; i++)
		{
			_CompareAbilities[i].Run();
		}
	}

	private void AbilityEnd()
	{
		for (int i = 0; i < _CompareAbilities.Length; i++)
		{
			_CompareAbilities[i].End();
		}
	}

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _CanJump == true)
        {
            _Animator.SetBool("PlayerIdle", false);
            _Animator.SetTrigger("PlayerJump");
            _CanJump = false;
            _Rb.AddForce(new Vector3(0f, JumpHeight, 0f));
        }
        if (_Rb.velocity.y < 0)
        {
            _Rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(_Rb.velocity.y>0 && !Input.GetButton("Jump"))
        {
            _Rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Movement()
    {
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
        else
        {
            _Animator.SetBool("PlayerIdle", true);
            _Animator.SetBool("PlayerWalk", false);
            _Animator.SetBool("PlayerRun", false);
        }
    }


    private void GetInputs()
    {
        _InputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //get wasd movement
    }
}
