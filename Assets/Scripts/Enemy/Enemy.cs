using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private float health;
    private int MovementSpeed;
    private string EnemyName;
    private GameObject target;
    public StateMachine StateMachine => GetComponent<StateMachine>();
    public Rigidbody _rb;

    public float Health { get => health; set => health = value; }
    public int MovementSpeed1 { get => MovementSpeed; set => MovementSpeed = value; }
    public string EnemyName1 { get => EnemyName; set => EnemyName = value; }
    public GameObject Target { get => target; set => target = value; }

    public float FlashingTime = .6f;
    public float TimeInterval = .1f;


    public Enemy()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    IEnumerator Flash(float time, float intervalTime)
    {
        //this counts up time until the float set in FlashingTime
        float elapsedTime = 0f;
        //This repeats our coroutine until the FlashingTime is elapsed
        while (elapsedTime < time)
        {
            //This gets an array with all the renderers in our gameobject's children
            Renderer[] RendererArray = GetComponentsInChildren<Renderer>();
            //this turns off all the Renderers
            foreach (Renderer r in RendererArray)
                r.enabled = false;
            //then add time to elapsedtime
            elapsedTime += Time.deltaTime;
            //then wait for the Timeinterval set
            yield return new WaitForSeconds(intervalTime);
            //then turn them all back on
            foreach (Renderer r in RendererArray)
                r.enabled = true;
            elapsedTime += Time.deltaTime;
            //then wait for another interval of time
            yield return new WaitForSeconds(intervalTime);
        }
    }



    public virtual void TakeDamage(float damage)
    {
        print(damage + " " + health);
        health -= damage;
        StartCoroutine(Flash(FlashingTime, TimeInterval));
        CheckIfDead();
    }





    public virtual void CheckIfDead()
    {
        if (health <= 0 || transform.position.y < -100)
        {
            ResolveDeletion();
        }
        
    }

    public abstract void ResolveDeletion();

    public void FixedGravity()
    {
    }

    private void Update()
    {
        CheckIfDead();
    }
}
