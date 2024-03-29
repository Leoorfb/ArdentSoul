using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para Gatilho de fun��es pela anima��o do Bringer of Death.
/// </summary>
public class BringerAnimationTriggers : MonoBehaviour
{
    [SerializeField] BringerStateMachine bringerStateMachine;

    private void AttackEndTrigger()
    {
        bringerStateMachine.attackState.hasAttackAnimationEnded = true;
    }
    private void AttackHitboxOnTrigger()
    {
        bringerStateMachine.attackState.AttackHitboxOn();
    }
    private void AttackHitboxOffTrigger()
    {
        bringerStateMachine.attackState.AttackHitboxOff();
    }

    private void HurtEndTrigger()
    {
        bringerStateMachine.getHurtState.hasAnimationEnded = true;
    }

    private void CastTrigger()
    {
        bringerStateMachine.castState.OnCastAnimation();
    }

    private void CastEndTrigger()
    {
        bringerStateMachine.castState.OnCastAnimationEnd();
    }
    
    private void TeleportTrigger()
    {
        bringerStateMachine.teleportState.Teleport();
    }
    private void TeleportEndTrigger()
    {
        bringerStateMachine.teleportState.hasAnimationEnded = true;
    }
}
