﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    private Enemy _enemy;
    public override Type Tick()
    {
        throw new NotImplementedException();
    }

    public WanderState(Enemy enemy): base(enemy.gameObject)
    {
        _enemy = enemy;
    }


}
