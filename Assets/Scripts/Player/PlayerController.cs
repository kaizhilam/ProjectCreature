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
    private Vector3 input;
    private float _Fire;
    private bool _CanJump = false;
    private bool _CanWalk = false;
    //private bool _CanFire = true;
    //private float _FireRechargeTime;
    //private float _FireRechargeTimer = 0f;
    private Transform _CameraFace;
    private Rigidbody _Rb;
    private Animator _Animator;
    private ProjectileAbility proj = null;
    Ability[] abilities;
	private Ability[] _CompareAbilities;

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
		_CompareAbilities = PlayerStat.Abilities;
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

		if (_Fire == 1)
		{
			transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
		}
    if(_CanWalk && input!=Vector3.zero)
 //update rotation of the character when WASD is pressed

        {
            Vector3 movement;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", false);
                _Animator.SetBool("PlayerRun", true);
                //Debug.Log("Running");
                //movement = new Vector3(_Horizontal * MovementSpeed * 1.2f * Time.deltaTime, 0, _Vertical * MovementSpeed * 1.2f * Time.deltaTime);
                movement = input * Time.fixedDeltaTime * MovementSpeed * 1.2f;

            }
            else
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", true);
                _Animator.SetBool("PlayerRun", false);
                //Debug.Log("Walking");
                //movement = new Vector3(_Horizontal * MovementSpeed * Time.deltaTime, 0, _Vertical * MovementSpeed * Time.deltaTime);
                movement = input * Time.fixedDeltaTime * MovementSpeed;

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
        //_Horizontal = Input.GetAxisRaw("Horizontal"); //get A,W keys
        //_Vertical = Input.GetAxisRaw("Vertical"); //get W, S keys
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
}
