using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Vector3 Gravity;
	//public float JumpAmount;
	public float JumpHeight;
	public float Speed;
	public float Smooth;
	public float AirMovementPenalty; //VALUE BETWEEN 0-1
    public static float GetGravity;

	private GameObject _Camera;
	private CharacterController _Controller;
    private Vector3 _JumpAmount = new Vector3(0, 0, 0);

	public static bool CanMove = true;
	public static bool EnableGravity = true;

    private float lastPositionY;
    private bool lastGrounded;
    private float fallDistance;
    public float damageFromFallMinDistance = 5f;
    public float fallDamageMult = 0.5f;
    public bool isApplyingFallDamage = true;


    void Start()
    {
        InputManager.instance.Space += Jump;
        InputManager.instance.Movement += Movement;
		_Controller = GetComponent<CharacterController>();
        _Camera = GameObject.FindGameObjectWithTag("MainCamera");
        //subscribing Movement function to InputManager -- If InputManager sees WASD or jump pressed, will call
    }

	private void Update()
	{
        RunGravity();
        if (isApplyingFallDamage)
        {
            FallDamage();
        }

    }

    private void FallDamage()
    {
        if (!lastGrounded && _Controller.isGrounded)
        {
            //player has hit the ground
            if (fallDistance > damageFromFallMinDistance)
            {
                GetComponent<Player>().TakeDamage(fallDamageMult * (fallDistance * fallDistance));
            }
        }

        if (lastPositionY > _Controller.transform.position.y)
        {
            fallDistance += lastPositionY - _Controller.transform.position.y;

        }
        if (_Controller.isGrounded)
        {
            fallDistance = 0;
        }

        lastPositionY = _Controller.transform.position.y;
        lastGrounded = _Controller.isGrounded;
    }

    private void RunGravity()
    {
        if (EnableGravity == true)
        {
            _JumpAmount.y += Gravity.y * Time.deltaTime;
            _Controller.Move(_JumpAmount * Time.deltaTime);
        }
        if (EnableGravity == false)
        {
            _JumpAmount.y = 0;
        }
        GetGravity = Gravity.y;
    }

    private void Movement(Vector2 _Input, Vector2 _InputRaw)
    {
        Vector3 movement = new Vector3();
        float delta = Time.deltaTime;
        Vector3 cameraForward = _Camera.transform.forward;
        cameraForward.y = 0f;
        Vector3 cameraRight = _Camera.transform.right;
        cameraRight.y = 0f;
		if (CanMove == true)
        {
            if (_InputRaw.y >= 0)
            {
                if (_InputRaw.y > 0) //CHARACTER FACING FORWARD
                    transform.forward = Vector3.Lerp(transform.forward, cameraForward, delta * Smooth);
                if (_InputRaw.x != 0) //CHARACTER FACING LEFT AND RIGHT
                    transform.forward = Vector3.Lerp(transform.forward, cameraRight * _InputRaw.x, delta * Smooth);
            }
            else if (_InputRaw.y < 0)
            {
                if (_InputRaw.y < 0) //CHARACTER FACING FORWARD
                    transform.forward = Vector3.Lerp(transform.forward, -cameraForward, delta * Smooth);
                if (_InputRaw.x != 0) //CHARACTER FACING LEFT AND RIGHT, BUT INVERSE
                    transform.forward = Vector3.Lerp(transform.forward, -cameraRight * -_InputRaw.x, delta * Smooth);
            }
			//MOVEMENT CODE
			//print((cameraForward * _Input.y) + (cameraRight * _Input.x));
			movement = ((cameraForward * _Input.y) + (cameraRight * _Input.x)).normalized * Speed * delta;//HORIZONTAL + VERTICAL MOVEMENT

            _Controller.Move(new Vector3(movement.x, 0, movement.z));
        }

		//DEBUG
		//Vector3 temp = _Controller.velocity;
		//temp.y = 0f;
		//Debug.Log("GROUNDED: " + _Controller.isGrounded + " --- VELOCITY: " + temp + " --- MAGNITUDE: " + temp.magnitude + " --- MOVEMENT: " + movement);
	}

	private void Jump()
    {
		AnimatorClipInfo info = AnimationManager.instance.clipInfo;
        GetComponent<PlayerSoundManager>().StopSounds();
        GetComponent<PlayerSoundManager>().SetSoundOfName(PlayerSoundManager.SoundTypes.jump);
        if (_Controller.isGrounded == true && info.clip.name != "Dodge_Dive_anim")
        {
            _JumpAmount.y = JumpHeight;
        }
    }
}
