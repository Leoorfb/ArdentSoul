using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : BaseState
{
    protected EnemyStateMachine _context;
    public virtual void SetUpState(EnemyStateMachine context)
    {
        _context = context;
    }
}
