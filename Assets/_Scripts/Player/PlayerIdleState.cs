using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerIdleState : PlayerStateBase
{
    public override void CheckExitCondition()
    {
        _context.playerAttackState.TryEnterState();
        if (_context.isSwitchingStates) return;

        _context.playerJumpState.TryEnterState();
        if (_context.isSwitchingStates) return;

        if (_context.moveInput.ReadValue<Vector2>() != Vector2.zero)
        {
            _context.SwitchState("Walk");
        }
    }

    public override void EnterState()
    {
        player.animator.SetInteger("AnimState", 0);
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }
}