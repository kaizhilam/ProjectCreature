using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void ActionDelegate();
    public event ActionDelegate Atk;
    public event ActionDelegate RunAbility;
    public SlottedItem selectedItem;
    public Weapon equippedWeapon;

    public static float HP = 100f;

    private void Start()
    {
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


    private void Attack()
    {
        Atk();
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
