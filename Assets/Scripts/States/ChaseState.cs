using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    private Enemy _enemy;
    public override Type Tick()
    {
        throw new NotImplementedException();
    }

    public ChaseState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }
}
