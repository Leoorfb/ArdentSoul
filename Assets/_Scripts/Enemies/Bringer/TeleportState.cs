using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado de teleporte do inimigo.
/// Contem as variaveis e as funções relacionadas ao teleporte do inimigo
/// </summary>
[Serializable]
public class TeleportState : EnemyBaseState
{
    public bool hasAnimationEnded = false;
    [SerializeField] private Transform teleportPosition;

    public override void CheckExitCondition()
    {
        if (hasAnimationEnded)
        {
            _context.SwitchState("Cast");
        }
    }

    public override void EnterState()
    {
        hasAnimationEnded = false;
        _context.enemy.animator.SetTrigger("Teleport");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }

    public void Teleport()
    {
        _context.transform.position = teleportPosition.position;
    }
}
