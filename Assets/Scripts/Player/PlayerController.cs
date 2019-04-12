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
    //private bool _CanFire = true;
    //private float _FireRechargeTime;
    //private float _FireRechargeTimer = 0f;
    private Transform _CameraFace;
    private Rigidbody _Rb;
    private Animator _Animator;
	private Ability[] _CompareAbilities;

    private PlayerInventory<Item> Inventory = new PlayerInventory<Item>();
    private GameObject selectedObj;
    private Item selectedItem;
    private bool isChecked = false;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        _CameraFace = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _Animator = GetComponentInChildren<Animator>();
		_CompareAbilities = PlayerStat.Abilities;
		AbilityInit();
    }

    public void Update()
    {
        //_FireRechargeTime = 3f; //set fire recharge time here
        GetInputs();
		AbilityMethod();

        //Pick up operation
        MouseOperation();
        PickUpOperation();
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
		if (_Fire == 1)
		{
			transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
		}
		if (_CanWalk && (_Vertical != 0 || _Horizontal != 0)) //update rotation of the character when WASD is pressed
        {
            Vector3 movement;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", false);
                _Animator.SetBool("PlayerRun", true);
                movement = new Vector3(_Horizontal * MovementSpeed * 1.2f * Time.deltaTime, 0, _Vertical * MovementSpeed * 1.2f * Time.deltaTime);
            }
            else
            {
                _Animator.SetBool("PlayerIdle", false);
                _Animator.SetBool("PlayerWalk", true);
                _Animator.SetBool("PlayerRun", false);
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

    //private void Fire()
    //{
    //    if (_Fire == 1)
    //    {
    //        transform.eulerAngles = new Vector3(0, _CameraFace.transform.eulerAngles.y, 0);
    //    }
    //    if (_FireRechargeTimer > _FireRechargeTime)
    //    {
    //        _CanFire = true;
    //    }
    //    if (_Fire == 1 && _CanFire)
    //    {
    //        //Debug.Log("Name: " + ThirdPersonCamera.LookingAtGameObject.name + " Point: " + ThirdPersonCamera.LookingAtPoint + " Distance: " + ThirdPersonCamera.LookingAtDistance);
    //        _FireRechargeTimer = 0f;
    //        _CanFire = false;
    //    }
    //    _FireRechargeTimer += Time.deltaTime;
    //}

    private void GetInputs()
    {
        _Jump = Input.GetAxisRaw("Jump"); //get space keys
        _Horizontal = Input.GetAxisRaw("Horizontal"); //get A,W keys
        _Vertical = Input.GetAxisRaw("Vertical"); //get W, S keys
        //_Fire = Input.GetAxisRaw("Fire1");
    }

    //Pick up operation
    private void MouseOperation()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                selectedObj = hitInfo.collider.gameObject;
                SelectItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(Inventory);
        }
    }

    private void SelectItem()
    {
        if (selectedObj.CompareTag("T1"))
        {
            selectedItem = (Item)selectedObj.GetComponent<Item>();
            if (selectedItem.IsCloseEnough() == true)
            {
                Debug.Log("The collectable item " + selectedItem.objName + "has been selected but not be added in your pack");
                isChecked = true;
            }
        }
    }

    private void PickUpOperation()
    {
        if (Input.GetKeyDown(KeyCode.E) && isChecked == true && selectedItem.IsCloseEnough() == true)
        {
            Inventory.Add(selectedItem);
            Debug.Log("The item has been added");
            isChecked = false;
            selectedObj.SetActive(false);
        }
    }
}
