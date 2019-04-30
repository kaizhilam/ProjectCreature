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
    private float _Fire = 0;
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
        //getting cameras position, rotation and scale
        _CameraFace = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //for animations
        _Animator = GetComponentInChildren<Animator>();
        //used for updating abilities when swapping them
		_CompareAbilities = PlayerStat.Abilities;
        _InputVector = new Vector3(0, 0, 0);
        AbilityInit();

    }

    public void Update()
    {
        //_FireRechargeTime = 3f; //set fire recharge time here
        //get keyboard inputs
        GetInputs();
        //checking if player pressed 1,2 or 3 and running ability if it can
		AbilityMethod();
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

	private void AbilityMethod()
	{
		bool pressedOne = Input.GetKeyDown(KeyCode.Alpha1);
		bool pressedTwo = Input.GetKeyDown(KeyCode.Alpha2);
		bool pressedThree = Input.GetKeyDown(KeyCode.Alpha3);
        //player shouldn't be able to run passive abilities with any key bindings as
        //they are active as soon as they are put in the hotbar
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
        //if player has switched out an ability for another one...
		if (_CompareAbilities != PlayerStat.Abilities)
		{
            //end all passive abilities effects and run all passive effects in the updated hotbar
			AbilityEnd();
			_CompareAbilities = PlayerStat.Abilities;
			AbilityInit();
		}
	}

	private void AbilityInit()
	{
        //run all passive abilities buffs - abilities that are in hotbar
		for (int i = 0; i < _CompareAbilities.Length; i++)
		{
			_CompareAbilities[i].Init();
		}
	}

	private void AbilityRun()
	{
        //run active abilities if they are pressed - eg shoot projectile
		for (int i = 0; i < _CompareAbilities.Length; i++)
		{
			_CompareAbilities[i].Run();
		}
	}

	private void AbilityEnd()
	{
        //take all buffs from passive abilities from the player
		for (int i = 0; i < _CompareAbilities.Length; i++)
		{
			_CompareAbilities[i].End();
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
