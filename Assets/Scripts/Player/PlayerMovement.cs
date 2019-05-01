using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private GameObject cam;
	public Vector3 gravity;
	public Vector3 jumpVector;
	public float jumpHeight;
	public float speed;
	public float smooth;
	
	CharacterController controller;
	Vector3 cameraForward;
	Vector3 cameraRight;
	
    void Start()
    {
		controller = GetComponent<CharacterController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        float delta = Time.deltaTime;

        cameraForward = cam.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        cameraRight = cam.transform.right;
        cameraRight.y = 0f;
		cameraRight.Normalize();
		
		//MOVEMENT WASD
        if (Input.GetKey(KeyCode.W))
        {
            controller.Move(cameraForward * speed * delta);
            transform.forward = Vector3.Lerp(transform.forward, cameraForward, delta * smooth);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            controller.Move(-cameraForward * speed * delta);
            transform.forward = Vector3.Lerp(transform.forward, -cameraForward, delta * smooth);
        }
        if (Input.GetKey(KeyCode.D))
        {

            controller.Move(cam.transform.right * speed * delta);
            transform.forward = Vector3.Lerp(transform.forward, cameraRight, delta * smooth);
        }
        if (Input.GetKey(KeyCode.A))
        {
            controller.Move(-cam.transform.right * speed * delta);
            transform.forward = Vector3.Lerp(transform.forward, -cameraRight, delta * smooth);
		}

		//GRAVITY FALLING (To prevent not being able to jump while going downhill)
		jumpVector.y += gravity.y;
		controller.Move(jumpVector);

		//JUMPING
		if (Input.GetKey(KeyCode.Space) && controller.isGrounded == true)
		{
			jumpVector.y = jumpHeight * delta;
		}
		jumpVector.y = Mathf.Clamp(jumpVector.y, -2f, Mathf.Infinity);
		controller.Move(jumpVector);


		//JUMPING
		/*
        if (Input.GetKey(KeyCode.Space) && controller.isGrounded == true)
        {
            jumpVector.y = jumpHeight * delta;
		}	
		
		jumpVector.y += gravity.y;
        jumpVector.y = Mathf.Clamp(jumpVector.y, -2f, Mathf.Infinity);
		controller.Move(jumpVector);*/
	}
}
