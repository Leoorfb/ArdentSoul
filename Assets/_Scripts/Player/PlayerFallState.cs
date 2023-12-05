using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerFallState : PlayerStateBase
{
    public override void TryEnterState()
    {
        if (player.moveDirectionY < 0)
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
    }

    public override void UpdateState()
    {
        player.moveDirectionX = _context.moveInput.ReadValue<Vector2>().x * playerData.movementSpeed;

        CheckExitCondition();
    }
}
