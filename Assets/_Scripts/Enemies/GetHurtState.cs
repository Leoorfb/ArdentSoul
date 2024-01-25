using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado do inimigo machucado.
/// Contem as variaveis e as fun��es relacionadas ao inimigo do machucado
/// </summary>
[Serializable]
public class GetHurtState : EnemyBaseState
{
    [SerializeField] float invulnerabilityTime = 0.2f;
    float invulnerabilityCounter = 9f;
    [SerializeField] Color invulnerabilityColor = Color.white;
    [SerializeField] bool hasAnimation = false;
    public bool hasAnimationEnded = false;

    public override void EnterState()
    {
        //Debug.Log("MACHUDADO");
        HurtSetUp();

        if (hasAnimation)
        {
            _context.enemy.animator.SetTrigger("Hurt");
            hasAnimationEnded = false;
            return;
        }

    }

    public override void ExitState()
    {
        _context.enemy.isInvulnerable = false;
        //Debug.Log("n�o ta mais machucado");
        if (hasAnimation)
        {
            return;
        }

        _context.enemy.spriteRenderer.color = Color.white;
    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }

    public override void CheckExitCondition()
    {
        if (hasAnimation & hasAnimationEnded)
        {
            //Debug.Log("TERMINOU ANIMA��O MACHUCADO");
            _context.SwitchState();
        }

        if (!hasAnimation & invulnerabilityCounter <= 0)
        {
            _context.SwitchState();
        }
    }

    public override void StateIndenpendetUpdate()
    {
        if (_context.enemy.isInvulnerable)
        {
            invulnerabilityCounter -= Time.deltaTime;
            if (invulnerabilityCounter <= 0)
            {
                _context.enemy.isInvulnerable = false;
                _context.enemy.spriteRenderer.color = Color.white;
            }
        }
    }

    public override void TryEnterState()
    {
        if (invulnerabilityCounter > invulnerabilityTime)
        {
            _context.SwitchState("GetHurt");
        }
    }

    public void HurtSetUp()
    {
        _context.enemy.isInvulnerable = true;
        invulnerabilityCounter = invulnerabilityTime;
        _context.enemy.spriteRenderer.color = invulnerabilityColor;
    }
}
