using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que gerencia os estados do mago.
/// Controla e define o estado do mago
/// </summary>
public class WizardStateMachine : EnemyStateMachine
{
    [SerializeField] ProtectState _protectState;
    [SerializeField] FireAttackState _fireAttackState;
    [SerializeField] GetHurtState _getHurtState;
    [SerializeField] DieState _dieState;


    protected override void Awake()
    {
        base.Awake();
        // enemy = GetComponent<Slime>();

        _protectState.SetUpState(this);
        _getHurtState.SetUpState(this);
        _fireAttackState.SetUpState(this);
        _dieState.SetUpState(this);

        states.Add("Protect", _protectState);
        states.Add("FireAttack", _fireAttackState);
        states.Add("GetHurt", _getHurtState);
        states.Add("Die", _dieState);

        currentState = states["Protect"];
        baseState = states["Protect"];
    }

    protected override void Update()
    {
        enemy.moveDirectionX = 0;
        base.Update();
    }

    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }

    private void AttackEndTrigger()
    {
        _fireAttackState.hasAttackAnimationEnded = true;
    }

    private void AttackFireTrigger()
    {
        _fireAttackState.FireProjectile();
    }
}
