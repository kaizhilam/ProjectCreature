using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpHeight; //jumping force of the player
    public float MovementSpeed; //movement speed of the player
    public Transform CameraFace; //drag child object onto here, need it for camera movement

    private Rigidbody _Rb;
    private bool _CanJump = true;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
    }

    private void FixedUpdate()
    {
        PhysicsMovementController();
    }

    private void PhysicsMovementController()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //get A,W keys
        float vertical = Input.GetAxisRaw("Vertical"); //get W, S keys
        float jump = Input.GetAxisRaw("Jump"); //get <Space> key
        float fire = Input.GetAxisRaw("Fire1");

        //jump mechanics
        if (jump == 1 && _CanJump == true) 
        {
            _CanJump = false;
            _Rb.AddForce(new Vector3(0f, JumpHeight, 0f));
        }

        //movement mechanics
        if (vertical != 0 || horizontal != 0 || fire == 1) //update rotation of the character when WASD is pressed
        {
            transform.eulerAngles = new Vector3(0, CameraFace.transform.eulerAngles.y, 0);
        }
        Vector3 movement = new Vector3(horizontal * MovementSpeed * Time.deltaTime, 0, vertical * MovementSpeed * Time.deltaTime); 
        _Rb.transform.Translate(movement); //move the character
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") //reset jumping once the player touches the ground
        {
            _CanJump = true;
        }
    }
}
