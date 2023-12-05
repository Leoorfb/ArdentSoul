using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateManager
{
    [SerializeField] PlayerIdleState _playerIdleState;
    [SerializeField] PlayerWalkState _playerWalkState;
    [SerializeField] PlayerAttackState _playerAttackState;
    [SerializeField] PlayerBlockState _playerBlockState;
    [SerializeField] PlayerJumpState _playerJumpState;
    [SerializeField] PlayerFallState _playerFallState;


    private InputAction _moveInput;
    private InputAction _jumpInput;

    private InputAction _attackInput;
    private InputAction _blockInput;

    protected Player _player;


    #region getters e setters
    public PlayerIdleState playerIdleState { get { return _playerIdleState; } protected set { _playerIdleState = value; } }
    public PlayerWalkState playerWalkState { get { return _playerWalkState; } protected set { _playerWalkState = value; } }
    public PlayerAttackState playerAttackState { get { return _playerAttackState; } protected set { _playerAttackState = value; } }
    public PlayerBlockState playerBlockState { get { return _playerBlockState; } protected set { _playerBlockState = value; } }
    public PlayerJumpState playerJumpState { get { return _playerJumpState; } protected set { _playerJumpState = value; } }
    public PlayerFallState playerFallState { get { return _playerFallState; } protected set { _playerFallState = value; } }

    public Player player { get { return _player; } protected set { _player = value; } }
    public InputAction moveInput { get { return _moveInput; } protected set { _moveInput = value; } }
    public InputAction jumpInput { get { return _jumpInput; } protected set { _jumpInput = value; } }
    #endregion

    protected virtual void Awake()
    {
        player = GetComponent<Player>();

        _playerIdleState.SetUpState(this);
        _playerWalkState.SetUpState(this);
        _playerAttackState.SetUpState(this);
        _playerBlockState.SetUpState(this);
        _playerJumpState.SetUpState(this);
        _playerFallState.SetUpState(this);

        states.Add("Idle", _playerIdleState);
        states.Add("Walk", _playerWalkState);
        states.Add("Attack", _playerAttackState);
        states.Add("Block", _playerBlockState);
        states.Add("Jump", _playerJumpState);
        states.Add("Fall", _playerFallState);

        currentState = states["Idle"];
    }

    protected override void Update()
    {
        player.moveDirectionX = 0;
        base.Update();
    }

    private void OnEnable()
    {
        moveInput = _player.playerInputActions.Player.Move;
        moveInput.Enable();

        jumpInput = _player.playerInputActions.Player.Jump;
        jumpInput.Enable();

        jumpInput.performed += _playerJumpState.JumpBuffer;
        jumpInput.canceled += _playerJumpState.JumpRelease;

        
        _attackInput = _player.playerInputActions.Player.Attack;
        _attackInput.Enable();
        _attackInput.performed += _playerAttackState.AttackBuffer;
        
        _blockInput = _player.playerInputActions.Player.Block;
        _blockInput.Enable();
        /*
        blockInput.performed += StartBlock;
        blockInput.canceled += StopBlock;
        */
    }

    private void OnDisable()
    {
        moveInput.Disable();
        jumpInput.Disable();

        _attackInput.Disable();
        _blockInput.Disable();
    }

    private void OnDrawGizmos()
    {
        if (playerAttackState != null)
            playerAttackState.OnDrawGizmos();
    }

    private void OnAttackAnimationEnd()
    {
        playerAttackState.OnAttackAnimationEnd();
    }
}
