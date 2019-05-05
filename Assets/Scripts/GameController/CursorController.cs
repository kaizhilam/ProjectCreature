using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
	public static bool Visible
	{
		get { return Cursor.visible; }
		set { Cursor.visible = value; }
	}

    private void Start()
    {
		Cursor.visible = false;
		//Cursor.lockState = CursorLockMode.Locked;
    }
}
