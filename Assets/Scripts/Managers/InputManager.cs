using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputDelegate();
    public delegate void MovementDelegate(Vector2 v1, Vector2 v2);
    public delegate void ParamKeyCode(int i);
    public event InputDelegate Space;
    public event ParamKeyCode TopNumbers;
    public event InputDelegate CTRLKey;
    public event MovementDelegate Movement;
    public event InputDelegate StoppedMoving; 
    public event InputDelegate LeftClick;
    public event InputDelegate RightClick;
    public event InputDelegate BKey;
    public event InputDelegate EKey;
    //public event InputDelegate ShiftKey; soon
    private Vector3 _Input, _InputRaw;

    public static InputManager instance = null;

    private bool enabledPlayInput = false;

    public static bool EnabledPlayInput { get => instance.enabledPlayInput; set => instance.enabledPlayInput = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //This update functions looks for all inputs and broadcasts a message out depending on which are pressed
        //To have a method of any class listen for a specific broadcast,

        //let x be the function you are trying to subscribe and y the event that will broadcast
        //inside the class's start method, type InputManager.instance.y += x;
        //when this is done, the method(x) will run when event(y) is invoked

        if (Player.isClimbing == false) //Why is this reversed?
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftClick?.Invoke();
            }
            if (Input.GetMouseButtonDown(1))
            {
                RightClick?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                BKey?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                EKey?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Space?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                CTRLKey?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TopNumbers?.Invoke(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TopNumbers?.Invoke(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TopNumbers?.Invoke(2);
            }
            Vector2 inputVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (inputRaw != Vector2.zero)
            {
                Movement?.Invoke(inputVec, inputRaw);
            }
            else
            {
                StoppedMoving?.Invoke();
            }
        }
    }
}
