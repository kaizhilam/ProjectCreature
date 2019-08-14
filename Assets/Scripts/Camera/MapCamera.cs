using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
	public float CameraHeight;

	private GameObject _Player;
	private GameObject _PlayerLocation;

	private void Start()
	{
		_Player = GameObject.FindGameObjectWithTag("Player");
		transform.rotation = Quaternion.AngleAxis(90, Vector3.right);

		_PlayerLocation = transform.GetChild(0).GetChild(0).gameObject;
	}

	private void Update()
	{
		//MOVE CAMERA WITH THE PLAYER
		Vector3 playerPosition = _Player.transform.position;
		playerPosition.y += CameraHeight;
		transform.position = playerPosition;

		//ROTATE THE PLAYER ICON WHEN ROTATING PLAYER
		Vector3 playerRotation = _Player.transform.eulerAngles;
		_PlayerLocation.transform.eulerAngles = new Vector3(90, playerRotation.y, 0);
		Debug.Log(_PlayerLocation.transform.eulerAngles);

	}
}
