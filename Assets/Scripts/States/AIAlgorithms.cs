using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIAlgorithms
{
    private static GameObject player = GameObject.Find("Player");
    public static GameObject CheckForAggro(GameObject gameObject)
    {
        Ray ray = new Ray(gameObject.transform.position + gameObject.transform.forward, player.transform.position - gameObject.transform.position);
        Debug.DrawLine(gameObject.transform.position + gameObject.transform.forward, player.transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.NameToLayer("player")))
        {
            return player;
        }
        return null;
    }
}
