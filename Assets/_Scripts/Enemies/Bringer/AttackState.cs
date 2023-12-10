using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackState : EnemyBaseState
{
    [Header("Attack Timers")]
    [SerializeField] float _attackCooldown = 2f;
    float _attackTimeCounter = 0;
    public bool hasAttackAnimationEnded = false;

    [Header("Attack Range")]
    public float attackRange = 2f;

    [Header("Attack References")]
    [SerializeField] GameObject attackPrefab;
    [SerializeField] Transform attackSpawnPosition;
    EnemyAttack enemyAttack;

    public override void CheckExitCondition()
    {
        if (hasAttackAnimationEnded)
        {
            _context.SwitchState();
        }
    }

    public override void EnterState()
    {
        Debug.Log("Atacou");
        hasAttackAnimationEnded = false;
        _context.enemy.animator.SetTrigger("Attack");
        _attackTimeCounter = 0;
    }

    public override void ExitState()
    {
        _context.enemy.animator.ResetTrigger("StopAttack");
        _context.enemy.animator.SetTrigger("StopAttack");
        hasAttackAnimationEnded = true;
        _attackTimeCounter = 0;
        Debug.Log("Acabou Ataque");
    }

public override void UpdateState()
    {
        CheckExitCondition();
    }

    public override void StateIndenpendetUpdate()
    {
        _attackTimeCounter += Time.deltaTime;
    }

    public override void TryEnterState()
    {
        if (_attackTimeCounter < _attackCooldown)
        {
            return;
        }
        if (_context.distanceToPlayer() < attackRange)
        {
            _context.SwitchState("Attack");
            return;
        }
        if (_context.currentStateKey != "Chase")
        {
            Debug.Log("Muito Longe para atacar");
            _context.SwitchState("Chase");
        }

    }

    public void AttackHitboxOn()
    {
        GameObject attack = GameObject.Instantiate(attackPrefab, attackSpawnPosition.position, attackSpawnPosition.rotation);
        enemyAttack = attack.GetComponent<EnemyAttack>();
        enemyAttack.damage = _context.enemy.damage;
    }

    public void AttackHitboxOff()
    {
        if (enemyAttack != null)
            enemyAttack.OnAnimationEnd();
    }
}
