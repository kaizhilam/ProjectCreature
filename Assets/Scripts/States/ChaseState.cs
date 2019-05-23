using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyAIState
{
    private Enemy _enemy;

    public override Type Tick()
    {
        //return typeof(WanderState);

        //transform.LookAt(player.transform.position + (Vector3.up * 4)); //look at player
        //_rb.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime); //move towards the player
        gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position-gameObject.transform.forward*4);
        //if (AIAlgorithms.NeedsCorrection(gameObject))
        //{
        //    return typeof(AvoidanceState);
        //}
        if (AIAlgorithms.CheckForAggro(gameObject))
        {
            return typeof(ChaseState);
        }
        return typeof(WanderState);
    }

    public ChaseState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }
}
