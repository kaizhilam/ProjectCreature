﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
	/* Ability list:
	 * NullAbility
	 * FireballAbility
	 * WaterballAbility
	 * LightningballAbility
	 * FastMovementAbility
	 * HighJumpAbility
	 * BlinkAbility
	 * */
    public static float HP = 100f;
<<<<<<< HEAD
    //public ProjectileScriptableObject ProjectileAbility;
    //public static PlayerInventory<string> Inventory = new PlayerInventory<string>(); //change int to whatever type item is 
    //public static PlayerInventory<Item> Inventory = new PlayerInventory<Item>();
    //public PlayerInventory<Item> Inventory = new PlayerInventory<Item>();
    /*
    private void Start()
    {
        Inventory.Capacity = 9999;
    }
    */

=======
    public static PlayerInventory<string> Inventory = new PlayerInventory<string>(); //change int to whatever type item is 
	public static Ability[] Abilities = new Ability[3] { new BlinkAbility(), new WaterballAbility(), new LightningballAbility() }; //change ability here
	public static int AbilitiesIndex = 0;
	public static Ability SelectedAbility = Abilities[AbilitiesIndex];
	private void Update()
	{
		SelectedAbility = Abilities[AbilitiesIndex];
	}
>>>>>>> master
}
