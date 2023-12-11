using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado de queda do player.
/// Contem as variaveis e as funções relacionadas à queda do player
/// </summary>
[Serializable]
public class PlayerFallState : PlayerStateBase
{
    public override void TryEnterState()
    {
        if (!player.isGrounded && player.moveDirectionY < 0)
        {
            _context.SwitchState("Fall");
        }
    }

    public override void CheckExitCondition()
    {
        if (player.isGrounded)
        {
            _context.SwitchState("Idle");
        }
    }

    public override void EnterState()
    {
        player.animator.SetFloat("AirSpeedY", -1);
    }

    public override void ExitState()
    {
        player.animator.SetFloat("AirSpeedY", 0);
        AudioManager.Instance.Play("PlayerLand");

    }

    public override void UpdateState()
    {
        player.moveDirectionX = _context.moveInput.ReadValue<Vector2>().x * playerData.movementSpeed;

        CheckExitCondition();
    }
}
