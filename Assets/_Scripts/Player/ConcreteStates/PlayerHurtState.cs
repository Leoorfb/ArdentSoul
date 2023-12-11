using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado do player machucado.
/// Contem as variaveis e as funções relacionadas ao player do machucado
/// </summary>
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
        AudioManager.Instance.Play("PlayerHurt");
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
