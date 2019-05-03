using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputDelegate();
    public delegate void MovementDelegate(Vector3 input, Vector3 raw);
    public event InputDelegate LeftClick;
    public event InputDelegate RightClick;
    public event InputDelegate BKey;
    public event InputDelegate EKey;
    //public event InputDelegate ShiftKey; not yet
    public event MovementDelegate WASDJump;
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
        if (Input.GetKeyDown(KeyCode.B)) { BKey(); }
        if (Input.GetMouseButtonDown(0)) { LeftClick(); }
        if (Input.GetMouseButtonDown(1)) { RightClick(); }
        if (Input.GetKeyDown(KeyCode.E)) { EKey(); }
        _Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical")); //NUMBER CONTAINS DECIMALS FOR CHARACTER ACCELERATION
        _InputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"), Input.GetAxisRaw("Vertical")); //NUMBER IS A FULL NUMBER FOR CHECKING IF BUTTON IS PRESSED
        if (_Input!= Vector3.zero)
        {
            WASDJump(_Input, _InputRaw);
        }

    }
}
