using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    public LayerMask obstacle;
    public float avoidDistance = 2f;
    public float visionRange = 5f;
    private Vector3 target;
    private bool avoidCornerTrap;
    //in case we change from rigidbody to charactercontroller;
    //private CharacterController cc;
    private Rigidbody _rb;
    private bool contact = false;
    private GameObject rayCastPoint;
    // Start is called before the first frame update
    void Start()
    {
        rayCastPoint = transform.Find("RayCastPoint").gameObject;
        //cc = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //called from enemyAI class
        //CourseCorrection();
    }

    public bool CourseCorrection()
    {
        //casting 3 rays to check if bumping into walls. 1 ray is often not enough
        //1 long ray straight forward with visionRange length and 2 rays slightly out to the side, at 1/5th the length
        //if ray hits wall, it will create a new destination, which will be 2 units away from the collision point, in the direction of its normal
        //code must be written for the unavoidable situation where the enemy gets itself stuck in a corner
        //my solution checks to see if the player less than half of visionRange away from the wall, and if it is, it is likely trapped, so it will keep turning right until it is far enough away from a wall
        contact = false;
        avoidCornerTrap = false;
        target = transform.position;
        Ray middle = new Ray(rayCastPoint.transform.position, transform.forward);
        Ray right = new Ray(rayCastPoint.transform.position, transform.forward + 0.7f * transform.right);
        Ray left = new Ray(rayCastPoint.transform.position, transform.forward + 0.7f * -transform.right);
        Debug.DrawLine(rayCastPoint.transform.position, rayCastPoint.transform.position + transform.forward * 10);
        Debug.DrawLine(rayCastPoint.transform.position, rayCastPoint.transform.position + (transform.forward + 0.7f * transform.right).normalized * 2);
        Debug.DrawLine(rayCastPoint.transform.position, rayCastPoint.transform.position + (transform.forward + 0.7f * -transform.right).normalized * 2);
        RaycastHit hit;
        //biggest middle ray gets highest precedence
        //if no hit, checks the two side rays
        //with all hits, we are only concerning ourselves with objects that have the layermask called 'obstacle'
        //ground has layermask 'obstacle' and any structure within the game world should also have this layermask when placed into the scene or avoidance rays will ignore it
        if (Physics.Raycast(middle, out hit, visionRange, obstacle))
        {
            contact = true;
            if (hit.distance < visionRange / 2)
            {
                avoidCornerTrap = true;
            }
            target = hit.point + hit.normal * avoidDistance;
        }
        else if (Physics.Raycast(left, out RaycastHit leftHit, visionRange / 5, obstacle))
        {
            contact = true;
            target = hit.point + hit.normal * avoidDistance;
            transform.Rotate(Vector3.up);
        }
        else if (Physics.Raycast(right, out RaycastHit rightHit, visionRange / 5, obstacle))
        {
            contact = true;
            target = hit.point + hit.normal * avoidDistance;
            transform.Rotate(-Vector3.up);
        }
        target.y = transform.position.y;
        //face target

        //move to target
        if (avoidCornerTrap)
        {
            transform.Rotate(-Vector3.up);
        }
        else
        {
            Vector3 desiredRot = target - transform.position;
            if (desiredRot != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredRot), Time.deltaTime / 3f);
            }
            //cc.Move(transform.forward * 0.05f);
            _rb.transform.Translate(transform.forward * 0.05f);
        }
        return contact;
    }

    //purely for debug purposes. shows new target location to move to if about to hit wall
    private void OnDrawGizmos()
    {
        if (contact)
        {
            //draw cube at new suggested location, with size of 0.2
            Gizmos.DrawCube(target, Vector3.one * 0.2f);
        }
    }
}
