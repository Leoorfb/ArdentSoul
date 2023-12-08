using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected int _health = 4;
    [SerializeField] protected LayerMask _playerLayer;

    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected Vector2 _initialPosition = Vector2.zero;

    protected bool _isFacingRight = false;
    protected Vector2 _moveDirection = Vector2.zero;

    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;
    protected EnemyStateMachine _stateMachine;

    public int damage { get { return _damage; } protected set { _damage = value; } }
    public int health { get { return _health; } protected set { _health = value; } }
    public LayerMask playerLayer { get { return _playerLayer; } protected set { _playerLayer = value; } }

    public float moveSpeed { get { return _moveSpeed; } protected set { _moveSpeed = value; } }
    public Vector2 initialPosition { get { return _initialPosition; } }

    public bool isFacingRight { get { return _isFacingRight; } set { _isFacingRight = value; } }
    public Vector2 moveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
    public float moveDirectionX { get { return _moveDirection.x; } set { _moveDirection.x = value; } }
    public float moveDirectionY { get { return _moveDirection.y; } set { _moveDirection.y = value; } }

    public SpriteRenderer spriteRenderer { get { return _spriteRenderer; } }
    public Animator animator { get { return _animator; } }

    public void FlipSpriteRender(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _stateMachine = GetComponent<EnemyStateMachine>();

        _initialPosition = transform.position;
    }

    public abstract void GetDamaged(int damage);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (playerLayer == (1 << collision.gameObject.layer))
        {
            OnTouchPlayer(collision.gameObject.GetComponent<Player>());
        }
    }

    public virtual void OnTouchPlayer(Player player)
    {
        if (health <= 0) 
            return;

        player.TakeDamage(damage, transform.position.x);
    }
}
