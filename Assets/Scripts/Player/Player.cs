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
        InventoryManager.Instance.SwitchHotbarIndex(0);
        //layer 2 means its ignored by raycast, we don't want camera worrying about an equipped weapon
        if (GetComponentInChildren<Weapon>() != null)
        {
            Atk = this.GetComponentInChildren<Weapon>().Attack;
        }
        else
        {
            //if player not holding weapon, tell em to stop trying to use lmb/rmb?
            Atk = () => print("no weapon equipped, can't perform actions");
        }
    }


    private void Attack()
    {
        Atk();
    }

    private void Ability()
    {
        RunAbility();
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
            print("updating for weapon " + script.name);
            Atk = wepScript.Attack;
        }
        else
        {
            AnimationManager.instance.anim.SetBool("wielding", false);
            Atk = () => print("no weapon equipped, can't perform actions");
        }
        
    }
}
