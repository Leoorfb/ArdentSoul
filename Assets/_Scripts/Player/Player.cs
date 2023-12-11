using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Classe principal do player.
/// Contem as variaveis e as funções essenciais do player
/// </summary>
public class Player : Singleton<Player>
{
    [Header("Player Data")]
    [SerializeField] PlayerData _playerData;

    [Header("Direction")]
    [SerializeField] bool _isFacingLeft = false;
    public Vector2 moveDirection = Vector2.zero;
    private float _moveRotation = 0;

    [Header("Invulnerability Time")]
    [SerializeField] float invulnerabilityTime = 0.5f;
    float invulnerabilityCounter = 0;
    bool _isInvulnerable = false;

    [Header("Hit Force")]
    [SerializeField] float hitKnockbackForce = 3f;

    [Header("Ground Check")]

    public bool isGrounded = true;
    [SerializeField] bool displayGismoz = false;
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;

    public PlayerInputActions playerInputActions;

    Rigidbody2D _rigidbody2d;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    PlayerStateMachine _playerStateMachine;


    #region getters e setters
    public PlayerData playerData { get { return _playerData; } protected set { _playerData = value; } }
    public float moveDirectionX { get { return moveDirection.x; } set { moveDirection.x = value; } }
    public float moveDirectionY { get { return moveDirection.y; } set { moveDirection.y = value; } }
    public bool isInvulnerable { get { return _isInvulnerable; } private set { _isInvulnerable = value; } }
    public bool isFacingLeft { get { return _isFacingLeft; } private set { _isFacingLeft = value; } }

    public Rigidbody2D rigidbody2d { get { return _rigidbody2d; }}
    public Animator animator { get { return _animator; }}

    #endregion

    protected override void Awake()
    {
        base.Awake();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerInputActions = new PlayerInputActions();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {
        GroundedCheck();
        SetFacingDirection();
        HandleInvulnerability();
    }

    private void FixedUpdate()
    {
        moveDirectionY = _rigidbody2d.velocity.y;
        _rigidbody2d.velocity = moveDirection;
    }
    
    // debug gizmos
    private void OnDrawGizmos()
    {
        if (!displayGismoz) return;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawCube(groundCheckPosition.position, groundCheckSize);
    }

    internal void TakeDamage(int damage, float sourceXPosition)
    {
        // check vulnerability
        if (_isInvulnerable || playerData.health <= 0)
        {
            return;
        }

        // change values
        playerData.health -= damage;
        _isInvulnerable = true;
        invulnerabilityCounter = 0;

        // update ui
        GameUI.Instance.UpdateHPText();

        // knockback
        moveDirectionY = hitKnockbackForce;
        moveDirectionX = sourceXPosition > transform.position.x ? -hitKnockbackForce : hitKnockbackForce;
        _rigidbody2d.velocity = moveDirection;

        // set state
        if (playerData.health > 0)
            _playerStateMachine.SwitchState("Hurt");
        else
            _playerStateMachine.SwitchState("Death");
    }

    // check if is grounded
    private void GroundedCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.BoxCast(groundCheckPosition.position, groundCheckSize, 0f, Vector2.down, groundCheckSize.y/2, groundLayer);

        if (wasGrounded != isGrounded)
            _animator.SetBool("Grounded", isGrounded);
    }

    // set facing direction based on movement direction
    private void SetFacingDirection()
    {
        if (_playerStateMachine.playerCurrentState == _playerStateMachine.playerHurtState)
            return;

        if (moveDirectionX < 0)
            _isFacingLeft = true;
        else if (moveDirectionX > 0)
            _isFacingLeft = false;

        //_spriteRenderer.flipX = isFacingLeft;
        _moveRotation = _isFacingLeft? 180: 0f;
        transform.rotation = Quaternion.Euler(0, _moveRotation, 0);
    }

    // handle invulnerability timer
    private void HandleInvulnerability()
    {
        if (_isInvulnerable)
        {
            invulnerabilityCounter += Time.deltaTime;
            if (invulnerabilityCounter >= invulnerabilityTime)
            {
                _isInvulnerable = false;
            }
        }
    }
}
