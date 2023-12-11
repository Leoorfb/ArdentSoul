using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Classe do estado de pulo do player.
/// Contem as variaveis e as funções relacionadas ao pulo do player
/// </summary>
[Serializable]
public class PlayerJumpState : PlayerStateBase
{
    [Header("Jump Timers")]
    [SerializeField] float _coyoteTime = 0.2f;
    float _coyoteTimeCounter = 0;

    [SerializeField] float _jumpBufferTime = 0.2f;
    float _jumpBufferCounter = 0;

    public override void TryEnterState()
    {
        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
        {
            _context.SwitchState("Jump");
        }
    }

    public override void CheckExitCondition()
    {
        _context.playerFallState.TryEnterState();
        if (_context.isSwitchingStates) return;
        
        if (player.isGrounded & player.moveDirectionY == 0)
        {
            _context.SwitchState("Idle");
        }
    }

    public override void EnterState()
    {
        Jump();
        AudioManager.Instance.Play("PlayerJump");

    }

    public override void ExitState()
    {
    }

    public override void StateIndenpendetUpdate()
    {
        JumpSetup();
    }

    public override void UpdateState()
    {
        player.moveDirectionX = _context.moveInput.ReadValue<Vector2>().x * playerData.movementSpeed;

        CheckExitCondition();
    }

    private void Jump()
    {
        player.rigidbody2d.velocity = new Vector2(player.rigidbody2d.velocity.x, playerData.jumpForce);
        player.animator.SetTrigger("Jump");

        _coyoteTimeCounter = 0f;
        _jumpBufferCounter = 0f;
    }

    #region Jump Setup
    private void JumpSetup()
    {
        _coyoteTimeCounter = player.isGrounded ? _coyoteTime : _coyoteTimeCounter -= Time.deltaTime;
        _jumpBufferCounter -= Time.deltaTime;
    }

    public void JumpBuffer(InputAction.CallbackContext context)
    {
        _jumpBufferCounter = _jumpBufferTime;
        //Debug.Log("jump buffer " + _player.jumpBufferCounter);
    }
    public void JumpRelease(InputAction.CallbackContext context)
    {
        if (player.rigidbody2d.velocity.y > 0f)
        {
            player.rigidbody2d.velocity = new Vector2(player.rigidbody2d.velocity.x, player.rigidbody2d.velocity.y * 0.5f);
        }
    }
    #endregion
}
