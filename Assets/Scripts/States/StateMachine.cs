using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, State> _availableStates;

    public State CurrentState { get; private set; }
    public event Action<State> OnStateChanged;

    public void SetStates(Dictionary<Type, State> states)
    {
        _availableStates = states;
    }

    private void Update()
    {
        if(Current)
    }
}
