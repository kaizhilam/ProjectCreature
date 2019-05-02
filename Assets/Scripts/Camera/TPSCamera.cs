using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    public float maxDist = 5.0f;
    public float minDist = 1.0f;
    public float clampMin = -70f;
    public float clampMax = 28f;
    public float smooth;

    float speed = 10f;
    float rotX, rotY;
    Vector3 dolly;
    Vector3 camPos;
    float dist;
    
    void Start()
    {
        dolly = cam.transform.localPosition.normalized;
        dist = cam.transform.localPosition.magnitude;  
    }

    void Update()
    {
        float delta = Time.deltaTime;

        //MOUSE
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            rotX += Input.GetAxis("Mouse X") * speed;
            rotY += Input.GetAxis("Mouse Y") * speed;
            rotY = Mathf.Clamp(rotY, clampMin, clampMax);
            transform.rotation = Quaternion.Euler(-rotY, rotX, 0f);

            camPos = transform.TransformPoint(dolly * maxDist);
        }

        //CLAMP
        RaycastHit hit;
        if(Physics.Linecast(player.transform.position, camPos, out hit))
        {
            dist = Mathf.Clamp(hit.distance, minDist, maxDist); 
        } 
        else
        {
            dist = maxDist; 
        }

        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, dolly * dist, smooth * Time.deltaTime);
    }
}
