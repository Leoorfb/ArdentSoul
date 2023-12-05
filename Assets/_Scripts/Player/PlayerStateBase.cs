using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : BaseState
{
    protected PlayerStateMachine _context;
    protected Player player;
    protected PlayerData playerData;
    public virtual void SetUpState(PlayerStateMachine context)
    {
        _context = context;
        player = context.player;
        playerData = player.playerData;
    }

}
