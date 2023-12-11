using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base de todos os estados de comportamento.
/// Define as fun��es que todos os estados deve ou pode ter que s�o acionados por um gerenciador de estados.
/// </summary>
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
