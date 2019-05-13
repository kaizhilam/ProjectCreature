﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIState
{
    protected GameObject gameObject;
    protected Transform transform;
    public abstract Type Tick();
    public float aggroRange = 50.0f;
    protected GameObject player;
    protected Rigidbody _rb;
    protected float movementSpeed = 5f;

    public EnemyAIState(GameObject gameObject)
    {
        player = GameObject.Find("Player");
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    protected GameObject CheckForAggro()
    {
        Ray ray = new Ray(gameObject.transform.position + gameObject.transform.forward , player.transform.position - gameObject.transform.position);
        Debug.DrawLine(gameObject.transform.position + gameObject.transform.forward, player.transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.NameToLayer("player")))
        {
            return player;
        }
        return null;
    }


}
