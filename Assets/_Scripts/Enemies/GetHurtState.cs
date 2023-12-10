using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        invulnerabilityCounter = 0f;
        _context.enemy.isInvulnerable = true;

        if (hasAnimation)
        {
            _context.enemy.animator.SetTrigger("Hurt");
            hasAnimationEnded = false;
            return;
        }

        _context.enemy.spriteRenderer.color = invulnerabilityColor;
    }

    public override void ExitState()
    {
        _context.enemy.isInvulnerable = false;

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
            _context.SwitchState();
        }

        if (!hasAnimation & invulnerabilityCounter >= invulnerabilityTime)
        {
            _context.SwitchState();
        }
    }

    public override void StateIndenpendetUpdate()
    {
        if (_context.enemy.isInvulnerable)
        {
            invulnerabilityCounter += Time.deltaTime;
            if (invulnerabilityCounter >= invulnerabilityTime)
                _context.enemy.isInvulnerable = false;
        }
    }

    public override void TryEnterState()
    {
        if (invulnerabilityCounter > invulnerabilityTime)
        {
            _context.SwitchState(this);
        }
    }
}
