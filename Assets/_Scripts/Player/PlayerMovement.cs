using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rollSpeed = 8f;
    [SerializeField] bool isGrounded = true;
    [SerializeField] bool isFacingLeft = false;

    private Vector2 moveDirection = Vector2.zero;
    private InputAction moveInput;
    private InputAction jumpInput;


    [SerializeField] float jumpForce = 5f;

    [SerializeField] float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0;

    [SerializeField] float jumpBufferTime = 0.2f;
    private float jumpBufferCounter = 0;

    [SerializeField] bool displayGismoz = false;
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D _rigidbody;
    Animator _animator;
    Player _player;
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        moveInput = _player.playerInputActions.Player.Move;
        moveInput.Enable();

        jumpInput = _player.playerInputActions.Player.Jump;
        jumpInput.Enable();
        jumpInput.performed += JumpBuffer;
        jumpInput.canceled += JumpRelease;
    }

    private void OnDisable()
    {
        moveInput.Disable();
        jumpInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
        GroundedCheck();

        coyoteTimeCounter = isGrounded ? coyoteTime : coyoteTimeCounter -= Time.deltaTime;
        jumpBufferCounter -= Time.deltaTime;
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            Jump();
        }

        moveDirection.x = moveInput.ReadValue<Vector2>().x * movementSpeed;
        isFacingLeft = moveDirection.x < 0;
        _spriteRenderer.flipX = isFacingLeft;

        SetAnimation();
    }
    private void FixedUpdate()
    {
        moveDirection.y = _rigidbody.velocity.y;
        _rigidbody.velocity = moveDirection;
    }

    private void OnDrawGizmos()
    {
        if (!displayGismoz) return;
        Gizmos.color = isGrounded? Color.green: Color.red;
        Gizmos.DrawCube(groundCheckPosition.position, groundCheckSize);
    }

    private void GroundedCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.BoxCast(groundCheckPosition.position, groundCheckSize, 0f, Vector2.down, groundCheckSize.y/2, groundLayer);

        if (wasGrounded != isGrounded)
            _animator.SetBool("Grounded", isGrounded);
    }

    private void JumpBuffer(InputAction.CallbackContext context)
    {
        jumpBufferCounter = jumpBufferTime;
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        _animator.SetTrigger("Jump");

        coyoteTimeCounter = 0f;
        jumpBufferCounter = 0f;
    }


    private void JumpRelease(InputAction.CallbackContext context)
    {
        if(_rigidbody.velocity.y > 0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }


    private void SetAnimation()
    {
        _animator.SetFloat("AirSpeedY", _rigidbody.velocity.y);

        if (isGrounded && moveDirection.x != 0)
        {
            _animator.SetInteger("AnimState", 1);
            return;
        }

        _animator.SetInteger("AnimState", 0);
    }

}
