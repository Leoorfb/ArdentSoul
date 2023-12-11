using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base de todos os estados dos inimigos.
/// Contem as variaveis e as funções aos estados dos inimigos
/// </summary>
public abstract class EnemyBaseState : BaseState
{
    protected EnemyStateMachine _context;
    public virtual void SetUpState(EnemyStateMachine context)
    {
        _context = context;
    }
}
