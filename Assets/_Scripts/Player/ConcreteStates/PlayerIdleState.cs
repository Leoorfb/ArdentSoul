using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado do player parado.
/// Contem as variaveis e as funções relacionadas ao player parado
/// </summary>
[Serializable]
public class PlayerIdleState : PlayerStateBase
{
    public override void CheckExitCondition()
    {
        _context.playerRollState.TryEnterState();
        if (_context.isSwitchingStates) return;

        _context.playerBlockState.TryEnterState();
        if (_context.isSwitchingStates) return;

        _context.playerAttackState.TryEnterState();
        if (_context.isSwitchingStates) return;

        _context.playerJumpState.TryEnterState();
        if (_context.isSwitchingStates) return;

        if (_context.moveInput.ReadValue<Vector2>().x != 0)
        {
            _context.SwitchState("Walk");
        }
    }

    public override void EnterState()
    {
        player.animator.SetInteger("AnimState", 0);
        player.animator.Play("Idle");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }
}