using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputDelegate();
    public delegate void MovementDelegate(Vector2 v1, Vector2 v2);
    public event InputDelegate Space;
    public event InputDelegate QKey;
    public event MovementDelegate Movement;
    public event InputDelegate StoppedMoving; 
    public event InputDelegate LeftClick;
    public event InputDelegate RightClick;
    public event InputDelegate BKey;
    public event InputDelegate EKey;
    //public event InputDelegate ShiftKey; soon
    private Vector3 _Input, _InputRaw;

    public static InputManager instance = null;

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
        if (Input.GetKeyDown(KeyCode.B)) {
            BKey?.Invoke();
        }
        if (Input.GetMouseButtonDown(0)) {
            LeftClick?.Invoke();
        }
        if (Input.GetMouseButtonDown(1)) {
            RightClick?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            EKey?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Space?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QKey?.Invoke();
        }
        Vector2 inputVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(inputRaw != Vector2.zero)
        {
            print("invoke");
            Movement?.Invoke(inputVec, inputRaw);
        }
        else
        {
            StoppedMoving?.Invoke();
        }
    }
}
