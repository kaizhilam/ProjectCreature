using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

public static class WeaponFactory
{
    private static Dictionary<string, Type> abilitiesByName;
    private static bool isInitialized => abilitiesByName != null;

    private static void InitializeFactory()
    {
        if (isInitialized)
            return;

        var abilityTypes = Assembly.GetAssembly(typeof(Weapon)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Weapon)));

        abilitiesByName = new Dictionary<string, Type>();

        foreach (var type in abilityTypes)
        {
            var tempEffect = Activator.CreateInstance(type) as Weapon;
            abilitiesByName.Add(tempEffect.name, type);
        }
    }

    public static Weapon GetWeapon(string weaponType)
    {
        InitializeFactory();

        if (abilitiesByName.ContainsKey(weaponType))
        {
            Type type = abilitiesByName[weaponType];
            var weapon = Activator.CreateInstance(type) as Weapon;
            return weapon;
        }
        return null;
    }

    internal static IEnumerable<string> GetWeaponNames()
    {
        InitializeFactory();
        return abilitiesByName.Keys;
    }
}
