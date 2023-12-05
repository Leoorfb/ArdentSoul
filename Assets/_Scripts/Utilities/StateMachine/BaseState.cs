using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract void CheckExitCondition();

    public virtual void TryEnterState()
    {

    }
    public virtual void StateIndenpendetUpdate()
    {

    }
}
