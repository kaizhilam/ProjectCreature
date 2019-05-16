using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : EnemyAIState
{
    private float aggroRange = 50.0f;
    private float dist = 10.0f;
    private float radius = 3.0f;
    private Enemy _enemy;
    private GameObject player;
    Quaternion oldRot;
    Quaternion newRot;
    float rotationSpeed = 40f;
    int spinDirection = -1;


    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
        player = GameObject.Find("Player");
    }

    public override Type Tick()
    {
        //checks to see if we should leave the wander state
        //var chaseTarget = CheckForAggro();
        //if (chaseTarget != null)
        //{
        //    _enemy.Target = chaseTarget;
        //    return typeof(ChaseState);
        //}
        gameObject.transform.Rotate(spinDirection * Vector3.up * Time.deltaTime * rotationSpeed);
        _rb.transform.Translate(Vector3.forward* movementSpeed * Time.deltaTime);

        return typeof(WanderState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.forward * dist, radius);
    }

    IEnumerator changeSpinDirection()
    {
        while (true)
        {
            spinDirection *= -1;
            yield return new WaitForSeconds(UnityEngine.Random.Range(4f, 9f));
        }
    }

}
