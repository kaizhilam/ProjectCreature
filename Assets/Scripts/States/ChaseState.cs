using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyAIState
{
    private Enemy _enemy;

    public override Type Tick()
    {
        return typeof(WanderState);
        transform.LookAt(player.transform.position + (Vector3.up * 2)); //look at player
        Vector3 movement = Vector3.forward * movementSpeed * Time.deltaTime; //move forward
        _rb.transform.Translate(movement); //move towards the player
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
