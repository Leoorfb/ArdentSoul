using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe do estado de morte do player.
/// Contem as variaveis e as funções relacionadas ao morte do player
/// </summary>
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
