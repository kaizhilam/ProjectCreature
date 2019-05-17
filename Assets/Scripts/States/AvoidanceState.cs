using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceState : EnemyAIState
{
    public override Type Tick()
    {
        if (AIAlgorithms.NeedsCorrection(gameObject))
        {
            GameObject rayCastPoint = gameObject.transform.Find("RayCastPoint").gameObject;
            bool contact = false;
            bool avoidCornerTrap = false;
            Vector3 target = gameObject.transform.position;
            Ray middle = new Ray(rayCastPoint.transform.position, gameObject.transform.forward);
            Ray right = new Ray(rayCastPoint.transform.position, gameObject.transform.forward + 0.7f * gameObject.transform.right);
            Ray left = new Ray(rayCastPoint.transform.position, gameObject.transform.forward + 0.7f * -gameObject.transform.right);
            Debug.DrawLine(rayCastPoint.transform.position, rayCastPoint.transform.position + gameObject.transform.forward * 10);
            Debug.DrawLine(rayCastPoint.transform.position, rayCastPoint.transform.position + (gameObject.transform.forward + 0.7f * gameObject.transform.right).normalized * 2);
            Debug.DrawLine(rayCastPoint.transform.position, rayCastPoint.transform.position + (gameObject.transform.forward + 0.7f * -gameObject.transform.right).normalized * 2);
            RaycastHit hit;
            //simplified logic for the correction algo. We only need to return a bool saying if a collision exists, we don't actually want to handle it here
            //but in the AvoidanceState.cs class
            float visionRange = AIAlgorithms.visionRange;
            float avoidDistance = AIAlgorithms.avoidDistance;
            LayerMask EnemyMask = 1 << LayerMask.NameToLayer("enemy");
            if (Physics.Raycast(middle, out hit, visionRange, ~EnemyMask))
            {
                contact = true;
                if (hit.distance < visionRange / 2)
                {
                    avoidCornerTrap = true;
                }
                target = hit.point + hit.normal * avoidDistance;
            }
            else if (Physics.Raycast(left, out RaycastHit leftHit, visionRange / 5, ~EnemyMask))
            {
                contact = true;
                target = hit.point + hit.normal * avoidDistance;
                gameObject.transform.Rotate(Vector3.up);
            }
            else if (Physics.Raycast(right, out RaycastHit rightHit, visionRange / 5, ~EnemyMask))
            {
                contact = true;
                target = hit.point + hit.normal * avoidDistance;
                gameObject.transform.Rotate(-Vector3.up);
            }
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
                _rb.transform.Translate(transform.forward * Time.deltaTime * movementSpeed);
            }
            return typeof(AvoidanceState);
        }
        else if (AIAlgorithms.CheckForAggro(gameObject))
        {
            return typeof(ChaseState);
        }
        return typeof(WanderState);
    }

    public AvoidanceState(Enemy enemy) : base(enemy.gameObject)
    {
        
    }
}
