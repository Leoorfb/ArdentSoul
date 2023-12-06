using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] PlayerData _playerData;

    [Header("Direction")]
    [SerializeField] bool isFacingLeft = false;
    private Vector2 _moveDirection = Vector2.zero;
    private float _moveRotation = 0;

    [Header("Invulnerability Time")]
    [SerializeField] float invulnerabilityTime = 0.5f;
    float invulnerabilityCounter = 0;
    bool isInvulnerable = false;


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
    public Vector2 moveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
    public float moveDirectionX { get { return _moveDirection.x; } set { _moveDirection.x = value; } }
    public float moveDirectionY { get { return _moveDirection.y; } set { _moveDirection.y = value; } }

    public Rigidbody2D rigidbody2d { get { return _rigidbody2d; }}
    public Animator animator { get { return _animator; }}

    #endregion

    private void Awake()
    {
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
    

    private void OnDrawGizmos()
    {
        if (!displayGismoz) return;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawCube(groundCheckPosition.position, groundCheckSize);
    }

    internal void TakeDamage(int damage)
    {
        if (isInvulnerable || playerData.health <= 0)
        {
            return;
        }

        playerData.health -= damage;
        isInvulnerable = true;
        invulnerabilityCounter = 0;
        if (playerData.health > 0)
            _playerStateMachine.SwitchState("Hurt");
        else
            _playerStateMachine.SwitchState("Death");
    }

    private void GroundedCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.BoxCast(groundCheckPosition.position, groundCheckSize, 0f, Vector2.down, groundCheckSize.y/2, groundLayer);

        if (wasGrounded != isGrounded)
            _animator.SetBool("Grounded", isGrounded);
    }

    private void SetFacingDirection()
    {
        if (moveDirection.x < 0)
            isFacingLeft = true;
        else if (moveDirection.x > 0)
            isFacingLeft = false;

        //_spriteRenderer.flipX = isFacingLeft;
        _moveRotation = isFacingLeft? 180: 0f;
        transform.rotation = Quaternion.Euler(0, _moveRotation, 0);
    }

    private void HandleInvulnerability()
    {
        if (isInvulnerable)
        {
            invulnerabilityCounter += Time.deltaTime;
            if (invulnerabilityCounter >= invulnerabilityTime)
            {
                isInvulnerable = false;
            }
        }
    }
}
