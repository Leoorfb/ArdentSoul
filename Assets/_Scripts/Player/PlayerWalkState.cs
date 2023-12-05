using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerWalkState : PlayerStateBase
{
    public override void CheckExitCondition()
    {
        _context.playerFallState.TryEnterState();
        if (_context.isSwitchingStates) return;

        _context.playerAttackState.TryEnterState();
        if (_context.isSwitchingStates) return;

        _context.playerJumpState.TryEnterState();
        if (_context.isSwitchingStates) return;

        if (_context.moveInput.ReadValue<Vector2>() == Vector2.zero)
        {
            _context.SwitchState("Idle");
        }
    }

    public override void EnterState()
    {
        player.animator.SetInteger("AnimState", 1);
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        player.moveDirectionX = _context.moveInput.ReadValue<Vector2>().x * playerData.movementSpeed;

        CheckExitCondition();
    }
}
