using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static float LookingAtDistance;
	public static GameObject LookingAtGameObject;
	public delegate void ActionDelegate();
    public event ActionDelegate GameOver;
    public event ActionDelegate Atk;
    public event ActionDelegate RunAbility;
    private SlottedItem selectedItem;
    private Weapon equippedWeapon;
    private bool isGameOver;
    public bool godMode;
    public bool canDie;
  
    CharacterController controller;
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
        print("setting ability function");
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
		RaycastHit hit;
		Debug.DrawRay(transform.position + (Vector3.up * 1.5f), transform.TransformDirection(Vector3.forward * 10), Color.black);
		Ray castRay = new Ray(transform.position + (Vector3.up * 1.5f), transform.TransformDirection(Vector3.forward));
		if (Physics.Raycast(castRay, out hit, Mathf.Infinity))
		{
			LookingAtDistance = hit.distance;
			LookingAtGameObject = hit.collider.gameObject;
			//Debug.Log(hit.collider.gameObject.name);
		}
		else
		{
			LookingAtDistance = Mathf.Infinity;
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy script = collision.gameObject.GetComponent<Enemy>();
            TakeDamage(script.Damage);
            //updating health bar in UI. make sure health can never be negative

            //print(FindObjectOfType<Hp>().gameObject.name);
        }
    }

    private void Attack()
    {
        GetComponent<PlayerSoundManager>().StopSounds();
        GetComponent<PlayerSoundManager>().SetSoundOfName(PlayerSoundManager.SoundTypes.attack);
        Atk();
    }

    public void TakeDamage(float healthToLose)
    {
        if (!godMode)
        {
            HP -= healthToLose;
            GetComponent<PlayerSoundManager>().StopSounds();
            GetComponent<PlayerSoundManager>().SetSoundOfName(PlayerSoundManager.SoundTypes.hurt);
            CheckIfDead();
        }
        
    }

    public void CheckIfDead()
    {
        if (HP <= 0 && !isGameOver && canDie)
        {
            print("GAME OVER");
            GameOver?.Invoke();
            isGameOver = true;
        }
        HP = Mathf.Max(0, HP);
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
