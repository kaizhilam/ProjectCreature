using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Vector3 Gravity;
	public Vector3 JumpVector;
	public float JumpHeight;
	public float Speed;
	public float Smooth;
	public float AirMovementPenalty; //VALUE BETWEEN 0-1

	public Camera _Camera;
	private CharacterController _Controller;
	private Vector3 _Input, _InputRaw;

	void Start()
    {
		_Controller = GetComponent<CharacterController>();
        //_Camera = GameObject.FindGameObjectWithTag("MainCamera");
        //subscribing Movement function to InputManager -- If InputManager sees WASD or jump pressed, will call
    }

	private void FixedUpdate()
	{
		GetInput();
		Movement();
	}

    private void Movement()
    {
        Vector3 movement = new Vector3();
        float delta = Time.deltaTime;
        Vector3 cameraForward = _Camera.transform.forward;
        cameraForward.y = 0f;
        Vector3 cameraRight = _Camera.transform.right;
        cameraRight.y = 0f;

        //CHARACTER ROTATION WHEN WALKING
        if (_InputRaw != Vector3.zero)
        {
            if (_InputRaw.z >= 0)
            {
                if (_InputRaw.z > 0) //CHARACTER FACING FORWARD
                    transform.forward = Vector3.Lerp(transform.forward, cameraForward, delta * Smooth);
                if (_InputRaw.x != 0) //CHARACTER FACING LEFT AND RIGHT
                    transform.forward = Vector3.Lerp(transform.forward, cameraRight * _InputRaw.x, delta * Smooth);
            }
            else if (_InputRaw.z < 0)
            {
                if (_InputRaw.z < 0) //CHARACTER FACING FORWARD
                    transform.forward = Vector3.Lerp(transform.forward, -cameraForward, delta * Smooth);
                if (_InputRaw.x != 0) //CHARACTER FACING LEFT AND RIGHT, BUT INVERSE
                    transform.forward = Vector3.Lerp(transform.forward, -cameraRight * -_InputRaw.x, delta * Smooth);
            }

            //MOVEMENT CODE
            movement = ((cameraForward * _Input.z) + (cameraRight * _Input.x)).normalized * Speed * delta;//HORIZONTAL + VERTICAL MOVEMENT
        }

        //GRAVITY CODE
        if (_Controller.isGrounded)
            JumpVector.y += -0.5f; //TO COMBAT isGrounded BECAUSE GRAVITY CAN'T CATCH UP TO THE GAME'S TICK RATE
        else
            JumpVector.y += Gravity.y;
        movement += JumpVector;
        if (_Controller.isGrounded)
        {
            //JUMPING CODE
            JumpVector.y = _InputRaw.y * JumpHeight * delta;
            JumpVector.y = Mathf.Clamp(JumpVector.y, -2f, Mathf.Infinity);
            movement += JumpVector;
            _Controller.Move(movement);
        }
        else
        {
            _Controller.Move(movement * AirMovementPenalty);
        }

        ////DEBUG
        Vector3 temp = _Controller.velocity;
        temp.y = 0f;
        //Debug.Log("GROUNDED: " + _Controller.isGrounded + " --- VELOCITY: " + temp + " --- MAGNITUDE: " + temp.magnitude + " --- MOVEMENT: " + movement);
    }

    private void GetInput()
    {
        _Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical")); //NUMBER CONTAINS DECIMALS FOR CHARACTER ACCELERATION
        _InputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"), Input.GetAxisRaw("Vertical")); //NUMBER IS A FULL NUMBER FOR CHECKING IF BUTTON IS PRESSED
    }
}
