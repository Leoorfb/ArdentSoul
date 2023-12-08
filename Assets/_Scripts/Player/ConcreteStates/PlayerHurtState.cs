using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerHurtState : PlayerStateBase
{
    private bool isAnimationPlaying = false;

    public override void CheckExitCondition()
    {
        if (!isAnimationPlaying)
            _context.SwitchState();
    }

    public override void EnterState()
    {
        player.animator.SetTrigger("Hurt");
        isAnimationPlaying = true;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }

    public void OnAnimationEnd()
    {
        isAnimationPlaying = false;
    }
}
