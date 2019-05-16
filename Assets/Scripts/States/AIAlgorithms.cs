using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIAlgorithms
{
    private static GameObject player = GameObject.Find("Player");
    public static float visionRange = 5f;
    public static float avoidDistance = 2f;
    private static LayerMask EnemyMask = 1 << LayerMask.NameToLayer("enemy");
    public static GameObject CheckForAggro(GameObject gameObject)
    {
        Ray ray = new Ray(gameObject.transform.position + gameObject.transform.forward, player.transform.position - gameObject.transform.position);
        Debug.DrawLine(gameObject.transform.position + gameObject.transform.forward, player.transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, ~LayerMask.NameToLayer("enemy")))
        {
            if(hit.collider.name == "Player")
            return player;
        }
        return null;
    }

    public static bool NeedsCorrection(GameObject gameObject)
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
        //checks collisions with everything but enemies
        if (Physics.Raycast(middle, out hit, visionRange, ~EnemyMask))
        {
            return true;
        }
        else if (Physics.Raycast(left, out RaycastHit leftHit, visionRange / 5, ~EnemyMask))
        {
            return true;
        }
        else if (Physics.Raycast(right, out RaycastHit rightHit, visionRange / 5, ~EnemyMask))
        {
            return true;
        }
        return false;
    }
}
