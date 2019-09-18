using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    Vector3 _destination;
    Vector3 _direction;
    Quaternion _desiredRotation;


    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
        player = GameObject.Find("Player");
    }

    public override Type Tick()
    {

        //checks to see if we should leave the wander state
        var chaseTarget = AIAlgorithms.CheckForAggro(gameObject);
        if (chaseTarget)
        {
            _enemy.Target = player;
            return typeof(ChaseState);
        }
        //if (AIAlgorithms.NeedsCorrection(gameObject))
        //{
        //    return typeof(AvoidanceState);
        //}
        Vector2 match1 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        Vector3 match2 = new Vector2(_destination.x, _destination.z);
        if (Vector2.Distance(match1, match2) < 5 || _destination == Vector3.zero)
        {
            GetDestination();

        }
        //gameObject.transform.rotation = _desiredRotation;
        //_rb.transform.Translate(Vector3.forward* movementSpeed * Time.deltaTime);
        if (gameObject.GetComponent<NavMeshAgent>().isOnNavMesh)
        {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(_destination);
        }
        else
        {
            _enemy.ResolveDeletion();
        }
        return typeof(WanderState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.forward * dist, radius);
    }

    

    public void GetDestination()
    {
        Vector3 testPosition = (gameObject.transform.position + (gameObject.transform.forward * 20f)) +
                               new Vector3(UnityEngine.Random.Range(-10f, 10.0f), 0f,
                                   UnityEngine.Random.Range(-10.0f, 10.0f));

        _destination = new Vector3(testPosition.x, gameObject.transform.position.y, testPosition.z);
    }

}
