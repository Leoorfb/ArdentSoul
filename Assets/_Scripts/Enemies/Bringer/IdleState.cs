using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado do inimigo parado.
/// </summary>
[Serializable]
public class IdleState : EnemyBaseState
{
    public override void CheckExitCondition()
    {
        _context.states["Attack"].TryEnterState();
    }

    public override void EnterState()
    {
        Debug.Log("IDLE");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }
}