using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base para todos gerenciadores de estados.
/// </summary>
public abstract class StateManager : MonoBehaviour 
{
    protected Dictionary<string, BaseState> _states = new Dictionary<string,BaseState>();
    protected BaseState currentState;
    public string currentStateKey; // para debug

    protected BaseState baseState;
    protected bool _isSwitchingStates = false;
    public bool isSwitchingStates { get { return _isSwitchingStates; } protected set { _isSwitchingStates = value; } }


    #region getters e setters
    public Dictionary<string, BaseState> states { get { return _states; } protected set { _states = value; } }

    #endregion

    protected virtual void Start()
    {
        if (baseState == null) baseState = currentState;
        currentState.EnterState();
    }

    protected virtual void Update()
    {
        isSwitchingStates = false;

        foreach (BaseState state in states.Values)
        {
            state.StateIndenpendetUpdate();
        }

        currentState.UpdateState();
    }

    public void SwitchState()
    {
        SwitchState(baseState);
        currentStateKey = "default";
    }
    public void SwitchState(string stateKey)
    {
        SwitchState(states[stateKey]);
        currentStateKey = stateKey;
    }
    public void SwitchState(BaseState state)
    {
        isSwitchingStates = true;
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }

}