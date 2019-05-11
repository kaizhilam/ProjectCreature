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
        equippedWeapon.gameObject.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        //layer 2 means its ignored by raycast, we don't want camera worrying about an equipped weapon
        equippedWeapon.gameObject.layer = 2;
        Atk = GetComponentInChildren<Weapon>().Attack;
    }

    private void Attack()
    {
        Atk();
    }

    private void ManageCollisons()
    {
        
    }
}
