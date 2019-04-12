using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemies")]
public class EnemyScriptable: ScriptableObject
{
    public int health;
    public int MovementSpeed;
    public string EnemyName;

}
