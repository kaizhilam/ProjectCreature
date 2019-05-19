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

    private bool flashActive;
    public float flashLength;
    private float flashCounter;

    private SpriteRenderer playerSprite;


    public Enemy()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        playerSprite = GetComponent<SpriteRenderer>();
    }



    void Update() {
        if (flashActive)
        {
            if (flashCounter > flashLength * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }

            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;


        }
    }




    public virtual void TakeDamage(float damage)
    {
        print(damage + " " + health);
        health -= damage;

        flashActive = true;
        flashCounter = flashLength;

        CheckIfDead();

    }





    public virtual void CheckIfDead()
    {
        if (health <= 0)
        {
            ResolveDeletion();
        }
    }

    public abstract void ResolveDeletion();

    public void FixedGravity()
    {
    }
}
