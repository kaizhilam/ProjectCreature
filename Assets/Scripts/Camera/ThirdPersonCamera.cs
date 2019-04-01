using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //To access this class in another script,
    //type this:
    //ThirdPersonCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPersonCamera>();
    //camera.LookingAtPoint - to get the Vector3 point where the crosshair is pointing
    //camera.LookingAtDistance - to get the float distance where the crosshair is pointing
    //Uncomment line 62 Debug.Log(...) if needed

    public Transform FocusOn;
    public float Distance;
    public GameObject LookingAtGameObject { get; private set; } //to allow player to interact with object that the camera is looking at (pick up object)
    public Vector3 LookingAtPoint { get; private set; } //allow player to interact with object that the camera is looking at (shooting)
    public float LookingAtDistance { get; private set; } //allow player to interact with object that the camera is looking at (calculate distance)
    public GameObject LookingAtObject { get; private set; }
    public float MouseSensitivity;

    private float _CurrentX = 0.0f;
    private float _CurrentY = 0.0f;
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
        _CurrentY = Mathf.Clamp(_CurrentY * MouseSensitivity, -15, 89); //so the y axis does not clip through ground

        Vector3 dir = new Vector3(0, 0, -_CurrentDistance); //set distance between LookAt and camera
        Quaternion rotation = Quaternion.Euler(_CurrentY , _CurrentX , 0);
        transform.position = FocusOn.position + rotation * dir; //rotate camera around player
        transform.LookAt(FocusOn); //camera face player
        transform.Translate(new Vector3(_CurrentDistance/Distance, _CurrentDistance / Distance, 0f)); //move the camera slightly to the top right so camera ray casting wont keep hitting the player
        FocusOn.transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); //rotate lookAt object
    }

    private void CameraAiming() //Raycasting for character interaction with objects
    {
        RaycastHit hit;
        Ray objectRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        if (Physics.Raycast(objectRay, out hit))
        {
            LookingAtGameObject = hit.collider.gameObject;
            LookingAtPoint = hit.point;
            LookingAtDistance = hit.distance;
            LookingAtGameObject = hit.collider.gameObject;
            //Debug.Log("Name: " + hit.collider.name + " Point: " + hit.point + " Distance: " + hit.distance);
        }
        else
        {
            LookingAtGameObject = null;
            LookingAtPoint = Vector3.positiveInfinity;
        }
    }

    private void CameraZooming() 
    {
        RaycastHit hitBack;
        Ray frontRay = new Ray(transform.position, FocusOn.position - transform.position);
        Ray backRay = new Ray(FocusOn.position, this.transform.position - FocusOn.position);
        Debug.DrawRay(FocusOn.position, this.transform.position - FocusOn.position, Color.black);
        Debug.DrawRay(transform.position, -1*(FocusOn.position - transform.position), Color.blue);

        if (Physics.Raycast(backRay, out hitBack))
        {
            if (hitBack.collider.tag != "MainCamera")
            {
                //creates a new distance from ray casting. clamps the distance between 0.01 and maxDist. Linear interpolations to create smooth motion
                _CurrentDistance = Mathf.Lerp(_CurrentDistance, Mathf.Clamp((Vector3.Distance(hitBack.point, FocusOn.transform.position) - 2), 0.01f, Distance), 0.9f);
                //Debug.Log("Name: " + hitBack.collider.name + " Point: " + hitBack.point + " Distance: " + hitBack.distance + "Current: " + _CurrentDistance);
            }
                
        }
        else
        {
            _CurrentDistance = Distance;
        }
            
    }
}
