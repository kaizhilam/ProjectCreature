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
        if (chaseTarget != null)
        {
            _enemy.Target = chaseTarget;
            return typeof(ChaseState);
        }
        Vector2 match1 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        Vector3 match2 = new Vector2(_destination.x, _destination.z);
        Debug.Log(Vector2.Distance(match1, match2));
        Debug.Log(match1 + " + " + match2);
        if (Vector2.Distance(match1, match2) < 5 || _destination == Vector3.zero)
        {
            GetDestination();

        }
        gameObject.transform.rotation = _desiredRotation;
        _rb.transform.Translate(Vector3.forward* movementSpeed * Time.deltaTime);
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

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

}
