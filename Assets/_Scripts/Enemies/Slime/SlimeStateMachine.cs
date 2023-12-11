using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que gerencia os estados do slime.
/// Controla e define o estado do slime
/// </summary>
public class SlimeStateMachine : EnemyStateMachine
{
    [SerializeField] WanderState _wanderState;
    [SerializeField] GetHurtState _getHurtState;
    [SerializeField] DieState _dieState;

    protected override void Awake()
    {
        base.Awake();
        // enemy = GetComponent<Slime>();

        _wanderState.SetUpState(this);
        _getHurtState.SetUpState(this);
        _dieState.SetUpState(this);

        states.Add("Wander", _wanderState);
        states.Add("GetHurt",_getHurtState);
        states.Add("Die",_dieState);

        currentState = states["Wander"];
        baseState = states["Wander"];
    }

    protected override void Update()
    {
        enemy.moveDirectionX = 0;
        base.Update();
    }

    private void OnDrawGizmos()
    {
        if (_wanderState != null)
            _wanderState.OnDrawGizmos();
    }
}
