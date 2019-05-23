using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private float health;
    private float damage;
    private int MovementSpeed;
    private string EnemyName;
    private GameObject target;
    public StateMachine StateMachine => GetComponent<StateMachine>();
    public Rigidbody _rb;
    private Renderer _renderer;

    public float Health { get => health; set => health = value; }
    public float Damage { get => damage; set => damage = value; }
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
        _renderer = GetComponentInChildren<Renderer>();
    }

    IEnumerator Flash(float time, float intervalTime)
    {
        //this counts up time until the float set in FlashingTime
        float elapsedTime = 0f;
        //This repeats our coroutine until the FlashingTime is elapsed
        while (elapsedTime < time)
        {
            _renderer = GetComponentInChildren<Renderer>();
            //change material to red
            //_renderer.material.shader = Shader.Find("_Color");
            _renderer.material.SetColor("_Color", Color.red);
            //then add time to elapsedtime
            elapsedTime += Time.deltaTime;
            //then wait for the Timeinterval set
            yield return new WaitForSeconds(intervalTime);
            //then set the material colour back to white
            _renderer.material.SetColor("_Color", Color.white);   
            elapsedTime += Time.deltaTime;
            //then wait for another interval of time
            yield return new WaitForSeconds(intervalTime);
        }
    }



    public virtual void TakeDamage(float damage)
    {
        print(damage + " " + health);
        print("hit");
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
