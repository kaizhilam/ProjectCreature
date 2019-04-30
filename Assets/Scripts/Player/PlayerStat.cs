using System.Collections;
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
    public static PlayerInventory<string> Inventory = new PlayerInventory<string>(); //change int to whatever type item is 
}
