using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerRollState : PlayerStateBase
{
    [SerializeField] float rollSpeed = 8;
    private bool isAnimationPlaying = false;

    [SerializeField] float _rollBufferTime = 0.2f;
    float _rollBufferCounter = 0;

    [SerializeField] float _rollCooldownTime = 0.5f;
    float _rollCooldownCounter = 0;

    public override void TryEnterState()
    {
        if (_rollBufferCounter > 0f && _rollCooldownCounter < 0f)
        {
            _context.SwitchState("Roll");
        }
    }

    public override void CheckExitCondition()
    {
        if (!isAnimationPlaying)
            _context.SwitchState();
    }

    public override void EnterState()
    {
        player.animator.SetTrigger("Roll");
        isAnimationPlaying = true;
    }

    public override void ExitState()
    {
        _rollCooldownCounter = _rollCooldownTime;
    }

    public override void StateIndenpendetUpdate()
    {
        _rollBufferCounter -= Time.deltaTime;
        _rollCooldownCounter -= Time.deltaTime;
    }

    public override void UpdateState()
    {
        CheckExitCondition();
        player.moveDirectionX = player.isFacingLeft ? -rollSpeed : rollSpeed;
    }

    public void OnAnimationEnd()
    {
        isAnimationPlaying = false;
    }

    public void RollBuffer(InputAction.CallbackContext context)
    {
        _rollBufferCounter = _rollBufferTime;
    }
}
