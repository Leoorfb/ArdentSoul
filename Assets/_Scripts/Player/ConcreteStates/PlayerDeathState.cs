using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDeathState : PlayerStateBase
{
    public override void CheckExitCondition()
    {
        
    }

    public override void EnterState()
    {
        player.animator.SetTrigger("Death");
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
}
