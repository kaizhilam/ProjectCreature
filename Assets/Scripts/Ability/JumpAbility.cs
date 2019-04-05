using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : PassiveAbility
{
    GameObject Player;
    Rigidbody Rigid;
    public JumpAbility()
    {
        Player = GameObject.Find("Player");
        Rigid = (Rigidbody)Player.GetComponent(typeof(Rigidbody));
    }
    
    public override void Equipped()
    {
        Rigid.mass *= 2;
    }
    public override void Unequipped()
    {
        Rigid.mass /= 2;
    }
}
