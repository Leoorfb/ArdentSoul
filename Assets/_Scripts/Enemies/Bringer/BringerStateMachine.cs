using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que gerencia os estados do Bringer of Death.
/// Controla e define o estado do Bringer of Death
/// </summary>
public class BringerStateMachine : EnemyStateMachine
{
    [SerializeField] IdleState _idleState;
    [SerializeField] ChaseState _chaseState;
    [SerializeField] CastState _castState;
    [SerializeField] AttackState _attackState;
    [SerializeField] GetHurtState _getHurtState;
    [SerializeField] DieState _dieState;
    [SerializeField] TeleportState _teleportState;

    #region getters e setters
    public IdleState idleState { get { return _idleState; } protected set { _idleState = value; } }
    public ChaseState chaseState { get { return _chaseState; } protected set { _chaseState = value; } }
    public CastState castState { get { return _castState; } protected set { _castState = value; } }
    public AttackState attackState { get { return _attackState; } protected set { _attackState = value; } }
    public GetHurtState getHurtState { get { return _getHurtState; } protected set { _getHurtState = value; } }
    public DieState dieState { get { return _dieState; } protected set { _dieState = value; } }
    public TeleportState teleportState { get { return _teleportState; } protected set { _teleportState = value; } }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        // enemy = GetComponent<Slime>();

        _idleState.SetUpState(this);
        _chaseState.SetUpState(this);
        _castState.SetUpState(this);
        _attackState.SetUpState(this);
        _getHurtState.SetUpState(this);
        _dieState.SetUpState(this);
        _teleportState.SetUpState(this);

        states.Add("Idle", _idleState);
        states.Add("Chase", _chaseState);
        states.Add("Cast", _castState);
        states.Add("Attack", _attackState);
        states.Add("GetHurt", _getHurtState);
        states.Add("Die", _dieState);
        states.Add("Teleport", _teleportState);
        
        currentState = states["Idle"];
        baseState = states["Idle"];
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


}
