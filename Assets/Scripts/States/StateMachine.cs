using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, EnemyAIState> _availableStates;

    public EnemyAIState CurrentState { get; private set; }
    public event Action<EnemyAIState> OnStateChanged;

    public void SetStates(Dictionary<Type, EnemyAIState> states)
    {
        _availableStates = states;
    }

    private void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = _availableStates?.Values?.First();
        }

        var nextState = CurrentState?.Tick();
        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = _availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
