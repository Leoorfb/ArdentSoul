using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe do estado de morte do inimigo.
/// Contem as variaveis e as funções relacionadas ao morte do inimigo
/// </summary>
[Serializable]
public class DieState : EnemyBaseState
{
    [SerializeField] bool isDestroyOnAnimationEnd = false;
    [SerializeField] float deathDestroyTime = 1f;
    float deathDestroyCounter = 0f;
    

    public override void EnterState()
    {
        _context.enemy.isInvulnerable = true;
        deathDestroyCounter = 0f;
        _context.enemy.animator.SetTrigger("Death");
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if (isDestroyOnAnimationEnd) return;

        deathDestroyCounter += Time.deltaTime;

        if (deathDestroyCounter > deathDestroyTime)
        {
            GameObject.Destroy(_context.gameObject);
        }
    }

    public override void CheckExitCondition()
    {
        
    }
}
