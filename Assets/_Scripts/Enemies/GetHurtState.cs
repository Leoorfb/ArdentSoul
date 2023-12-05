using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GetHurtState : EnemyBaseState
{
    [SerializeField] float invulnerabilityTime = 0.2f;
    float invulnerabilityCounter = 0f;
    [SerializeField] Color invulnerabilityColor = Color.white;

    public override void EnterState()
    {
        invulnerabilityCounter = 0f;
        _context.enemy.spriteRenderer.color = invulnerabilityColor;
    }

    public override void ExitState()
    {
        _context.enemy.spriteRenderer.color = Color.white;
    }

    public override void UpdateState()
    {
        invulnerabilityCounter += Time.deltaTime;
        CheckExitCondition();
    }

    public override void CheckExitCondition()
    {
        if (invulnerabilityCounter >= invulnerabilityTime)
        {
            _context.SwitchState();
        }
    }
}
