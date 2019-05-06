using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputDelegate();
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

    }
}
