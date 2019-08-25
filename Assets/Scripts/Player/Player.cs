using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void ActionDelegate();
    public event ActionDelegate GameOver;
    public event ActionDelegate Atk;
    public event ActionDelegate RunAbility;
    public SlottedItem selectedItem;
    public Weapon equippedWeapon;
    public float climbSpeed;
    private bool isGameOver;

    bool canClimb = false;
    public static bool isClimbing = false;
  
    CharacterController controller;
    Collider climbSurface;
    float previousGravityY = 0f;

    public static float HP = 100f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        isGameOver = false;
        selectedItem = this?.GetComponentInChildren<SlottedItem>();
        equippedWeapon = this?.GetComponentInChildren<Weapon>();
        InputManager.instance.LeftClick += Attack;
        InputManager.instance.RightClick += Ability;
        InventoryManager.Instance.SwitchHotbarIndex(0);
        //layer 2 means its ignored by raycast, we don't want camera worrying about an equipped weapon
        if (GetComponentInChildren<Weapon>() != null)
        {
            Atk = this.GetComponentInChildren<Weapon>().Attack;
        }
        else
        {
            //if player not holding weapon, tell em to stop trying to use lmb/rmb?
            Atk = () => print("no weapon equipped, can't perform attack");
        }

        if (GetComponentInChildren<Weapon>() != null)
        {
            RunAbility = this.GetComponentInChildren<Weapon>().RunAbility;
        }
        else
        {
            //if player not holding weapon, tell em to stop trying to use lmb/rmb?
            RunAbility = () => print("no weapon equipped, can't perform ability");
        }
    }

    void Update()
    {
        if (isClimbing && Input.GetKeyDown(KeyCode.C))
        {
            isClimbing = false;
        }
        else if (canClimb && Input.GetKeyDown(KeyCode.C))
        {
            isClimbing = true;
        }


        if (isClimbing)
        {
            float delta = Time.deltaTime;

            if (Input.GetKey(KeyCode.W))
            {
                controller.Move(climbSurface.transform.up * delta * climbSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                controller.Move(climbSurface.transform.up * -delta * climbSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                controller.Move(climbSurface.transform.forward * delta * climbSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                controller.Move(climbSurface.transform.forward * -delta * climbSpeed);
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(400, 5, 500, 50), "C: Climb when close to wall");

        if (isClimbing)
        {
            GUI.Label(new Rect(400, 20, 100, 50), "CLIMBING");
        }
        /*if (canClimb)
        {
            GUI.Label(new Rect(400, 20, 100, 50), "CAN CLIMB");
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Climb"))
        {
            canClimb = true;
            climbSurface = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Climb"))
        {
            isClimbing = false;
            canClimb = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy script = collision.gameObject.GetComponent<Enemy>();
            HP -= script.Damage;
            CheckIfDead();
            //updating health bar in UI. make sure health can never be negative
            
            //print(FindObjectOfType<Hp>().gameObject.name);
        }
    }

    private void Attack()
    {
        Atk();
    }

    public void CheckIfDead()
    {
        if (HP <= 0 && !isGameOver)
        {
            print("GAME OVER");
            GameOver?.Invoke();
            isGameOver = true;
        }
        FindObjectOfType<Hp>().hp = Mathf.Max(0, HP);
    }

    private void Ability()
    {
        //only run the ability if it isn't on cooldown
        if (!InventoryManager.Instance.IsOnCooldown())
        {
            RunAbility();
            InventoryManager.Instance.CooldownToCurrent();
        }
    }

    private void ManageCollisons()
    {
        
    }

    public void UpdateWeaponFunctionality(SlottedItem script)
    {
        if (script?.GetComponentInChildren<Weapon>() !=null)
        {
            AnimationManager.instance.anim.SetBool("wielding", true);
            Weapon wepScript = script.GetComponentInChildren<Weapon>();
            Atk = wepScript.Attack;
            RunAbility = wepScript.RunAbility;
        }
        else
        {
            print("set wielding to false");

            AnimationManager.instance.anim.SetBool("wielding", false);
            Atk = () => print("no weapon equipped, can't perform attack");
            RunAbility = () => print("no weapon equipped, can't perform ability");
        }
        
    }
}
