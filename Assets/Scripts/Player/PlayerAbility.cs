using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private Ability[] _CompareAbilities;

    private void Start()
    {
        _CompareAbilities = PlayerStat.Abilities;
    }

    private void Update()
    {
        AbilityMethod();
    }

    private void AbilityMethod()
    {
        bool pressedOne = Input.GetKeyDown(KeyCode.Alpha1);
        bool pressedTwo = Input.GetKeyDown(KeyCode.Alpha2);
        bool pressedThree = Input.GetKeyDown(KeyCode.Alpha3);
        //player shouldn't be able to run passive abilities with any key bindings as
        //they are active as soon as they are put in the hotbar
        if (pressedOne)
        {
            AbilityEnd();
            if ((PlayerStat.Abilities[0] is ActiveAbility))
            {
                PlayerStat.AbilitiesIndex = 0;
            }
            else
            {
                Debug.Log("Can't choose passive ability");
            }
            AbilityInit();
        }
        if (pressedTwo)
        {
            AbilityEnd();
            if ((PlayerStat.Abilities[1] is ActiveAbility))
            {
                PlayerStat.AbilitiesIndex = 1;
            }
            else
            {
                Debug.Log("Can't choose passive ability");
            }
            AbilityInit();
        }
        if (pressedThree)
        {
            AbilityEnd();
            if ((PlayerStat.Abilities[2] is ActiveAbility))
            {
                PlayerStat.AbilitiesIndex = 2;
            }
            else
            {
                Debug.Log("Can't choose passive ability");
            }
            AbilityInit();
        }
        AbilityRun();
        //if player has switched out an ability for another one...
        if (_CompareAbilities != PlayerStat.Abilities)
        {
            //end all passive abilities effects and run all passive effects in the updated hotbar
            AbilityEnd();
            _CompareAbilities = PlayerStat.Abilities;
            AbilityInit();
        }
    }

    private void AbilityInit()
    {
        //run all passive abilities buffs - abilities that are in hotbar
        for (int i = 0; i < _CompareAbilities.Length; i++)
        {
            _CompareAbilities[i].Init();
        }
    }

    private void AbilityRun()
    {
        //run active abilities if they are pressed - eg shoot projectile
        for (int i = 0; i < _CompareAbilities.Length; i++)
        {
            _CompareAbilities[i].Run();
        }
    }

    private void AbilityEnd()
    {
        //take all buffs from passive abilities from the player
        for (int i = 0; i < _CompareAbilities.Length; i++)
        {
            _CompareAbilities[i].End();
        }
    }
}
