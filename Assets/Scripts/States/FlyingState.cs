﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingState : EnemyAIState
{
    private Enemy _enemy;
    Rigidbody rigid;
    Vector3 velocity;
    Vector3 acceleration;
    Vector3 deceleration;
    float desiredAltitude;
    float circlingRadius;
    float flapSpeed;
    float accelMax;
    float maxVertSpeed;
    float maxHoriSpeed;

    GameObject p;

    public FlyingState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = _enemy.GetComponent<Rigidbody>();
        circlingRadius = 7f;
        maxVertSpeed = 10;
        maxHoriSpeed = 0.5f;
        desiredAltitude = 10.0f;
        flapSpeed = 0.25f;
        accelMax = 5;
        
        
    }

    // Update is called once per frame
    public override Type Tick()
    {
        p = GameObject.Find("Player");
        rigid = _enemy.GetComponent<Rigidbody>();
        //applying gravity
        acceleration = Physics.gravity * rigid.mass;
        //updating velocity
        velocity += acceleration;
        //adding gravity to bird
        rigid.AddForce(acceleration, ForceMode.Acceleration);
        //find vector between bird and player
        Vector3 birdToPlayer = p.transform.position - _enemy.transform.position;
        //use only x and z of this vector
        birdToPlayer.y = 0;
        //use cross product with downwards vector to get vector perpendicular to both up vector and vector to player
        Vector3 travelVector = Vector3.Cross(-transform.up, birdToPlayer);
        //normalise vector then times by speed to get vector of consistent length
        travelVector = travelVector.normalized * maxHoriSpeed;
        //move in direction of this vector and face where moving to
        //rigid.transform.LookAt(transform.position + travelVector);
        //rigid.transform.Translate(travelVector);
        //every 2 seconds-
        //ray - cast downwards
        Ray ground = new Ray(this.transform.position, Physics.gravity);
        //if raycast dist < desired distance from ground
        if (Physics.Raycast(ground, out RaycastHit hit))
        {
            if (hit.distance < desiredAltitude)
            {
                //a = v ^ 2 + u ^ 2 / 2d
                // where v = 0 //we want 0 velocity so we reach an apogee at desired distance
                // where u = current v
                // where d = desired distance from ground - raycast dist
                //cap velocity to maxSpeed
                if (rigid.velocity.y > maxVertSpeed)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, maxVertSpeed, rigid.velocity.z);
                }
                else if (rigid.velocity.y < -maxVertSpeed)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, -+maxVertSpeed, rigid.velocity.z);
                }
                float velocityUp = rigid.velocity.y;
                float upA = -(velocityUp / (2 * (desiredAltitude - hit.distance))) - Physics.gravity.y * rigid.mass;

                //cap acceleration
                if (upA > accelMax)
                {
                    upA = accelMax;
                }
                // add force to bird in acceleration mode;
                //print(velocityUp);
                rigid.AddForce(0, upA, 0, ForceMode.Acceleration);
            }

        }
        else
        {
            // print("bird is not above the ground");
        }
        rigid.AddForce(-Physics.gravity * rigid.mass * 4, ForceMode.Acceleration);
        if (AIAlgorithms.CheckForAggro(gameObject))
        {
            return typeof(ChaseState);
        }
        return typeof(FlyingState);
    }

    //caps vector at length s
    void VectorCap(ref Vector3 v, float s)
    {
        if (v.magnitude > s)
        {
            v = v.normalized * s;
        }
    }
}
