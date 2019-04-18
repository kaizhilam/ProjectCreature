using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform FocusOn;
    public float Distance = 5f;

    public float MouseSensitivity = 1f;

    public static GameObject LookingAtGameObject;
    public static Vector3 LookingAtPoint;
    public static float LookingAtDistance;

    private float _CurrentX = 0f;
    private float _CurrentY = 0f;
    private float _CurrentDistance;

    private void Start()
    {
        _CurrentDistance = Distance;
    }

    private void Update()
    {
        CameraZooming();
        CameraMovement();
        CameraAiming();
    }

    private void CameraMovement()
    {
        _CurrentX += Input.GetAxis("Mouse X") * MouseSensitivity;
        _CurrentY += Input.GetAxis("Mouse Y");
		_CurrentY = Mathf.Clamp(_CurrentY * MouseSensitivity, -89, 89); //so the y axis does not clip through ground
		//_CurrentY = Mathf.Clamp(_CurrentY * MouseSensitivity, -15, 89); //so the y axis does not clip through ground

		Vector3 dir = new Vector3(0, 0, -_CurrentDistance); //set distance between LookAt and camera
        Quaternion rotation = Quaternion.Euler(_CurrentY, _CurrentX, 0);
        transform.position = FocusOn.position + rotation * dir; //rotate camera around player
        transform.LookAt(FocusOn); //camera face player
        transform.Translate(new Vector3(_CurrentDistance / Distance, _CurrentDistance / Distance, 0f)); //move the camera slightly to the top right so camera ray casting wont keep hitting the player
        FocusOn.transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); //rotate lookAt object
    }

    private void CameraAiming() //Raycasting for character interaction with objects
    {
        RaycastHit hit;
        Ray objectRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        if (Physics.Raycast(objectRay, out hit))
        {
            //add information as to what the camera is currently looking at
            LookingAtGameObject = hit.collider.gameObject;
            LookingAtPoint = hit.point;
            LookingAtDistance = hit.distance;
            LookingAtGameObject = hit.collider.gameObject;
            //Debug.Log("Name: " + hit.collider.name + " Point: " + hit.point + " Distance: " + hit.distance);
        }
        //if the ray cast from the camera hits nothing, the player is looking at the sky
        else
        {
            LookingAtGameObject = null;
            LookingAtPoint = Vector3.positiveInfinity;
        }
    }
    //casts a ray from the player to the camera
    //if the ray hits anything but the camera, it means something is blocking the cameras view
    //with the raycast returning the position of the obstacle hit, we can place the camera just in front of that obstacle
    //<o is camera, (p) is player
    //if this scenario occurs with wall blocking (p)-----|------<o
    // move camera to be in front of wall        (p)---<o|--------
    private void CameraZooming()
    {
        RaycastHit hitBack;
        Ray frontRay = new Ray(transform.position, FocusOn.position - transform.position);
        Ray backRay = new Ray(FocusOn.position, this.transform.position - FocusOn.position);
        //Debug.DrawRay(FocusOn.position, this.transform.position - FocusOn.position, Color.black);
        //Debug.DrawRay(transform.position, -1 * (FocusOn.position - transform.position), Color.blue);
        if (Physics.Raycast(backRay, out hitBack))
        {
            if (hitBack.collider.gameObject.tag != "Water")
            {
                if (hitBack.collider.tag != "MainCamera")
                {
                    //creates a new distance from ray casting. clamps the distance between 0.01 and maxDist. Linear interpolations to create smooth motion
                    _CurrentDistance = Mathf.Lerp(_CurrentDistance, Mathf.Clamp((Vector3.Distance(hitBack.point, FocusOn.transform.position) - 2), 0.01f, Distance), 0.9f);
                    //Debug.Log("Name: " + hitBack.collider.name + " Point: " + hitBack.point + " Distance: " + hitBack.distance + "Current: " + _CurrentDistance);
                }
            }

        }
        else
        {
            _CurrentDistance = Distance;
        }
    }
}