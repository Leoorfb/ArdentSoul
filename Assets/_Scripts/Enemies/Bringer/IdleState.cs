using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IdleState : EnemyBaseState
{
    public override void CheckExitCondition()
    {
        _context.states["Attack"].TryEnterState();
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }
}