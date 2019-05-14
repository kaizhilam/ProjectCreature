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


    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
        player = GameObject.Find("Player");
    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        if(chaseTarget != null)
        {
            _enemy.Target = chaseTarget;
            return typeof(ChaseState);
        }
        return typeof(WanderState);
    }

    


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.forward * dist, radius);
    }



}
