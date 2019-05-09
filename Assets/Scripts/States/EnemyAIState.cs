using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIState
{
    protected GameObject gameObject;
    protected Transform transform;
    public abstract Type Tick();
    public float aggroRange = 50.0f;
    private GameObject player;

    public EnemyAIState(GameObject gameObject)
    {
        player = GameObject.Find("Player");
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    protected GameObject CheckForAggro()
    {
        Ray ray = new Ray(this.transform.position, player.transform.position - this.transform.position);
        Debug.DrawLine(this.transform.position, player.transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, aggroRange))
        {
            if (hit.collider.gameObject.name == "Player")
            {
                return player;
            }
        }
        return null;
    }


}
