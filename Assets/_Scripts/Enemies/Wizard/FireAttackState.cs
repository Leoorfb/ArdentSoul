using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado de ataque a distância do inimigo.
/// Contem as variaveis e as funções relacionadas ao ataque a distância do inimigo
/// </summary>
[Serializable]
public class FireAttackState : EnemyBaseState
{
    [Header("Fire Attack Timers")]
    [SerializeField] float _attackCooldown = 2f;
    float _attackTimeCounter = 0;
    public bool hasAttackAnimationEnded = false;

    [Header("Fire Attack References")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPosition;

    public override void CheckExitCondition()
    {
        if (hasAttackAnimationEnded)
        {
            _context.SwitchState();
        }
    }

    public override void EnterState()
    {
        //Debug.Log("Atacou");
        hasAttackAnimationEnded = false;
        _context.enemy.animator.SetTrigger("Attack");
        _attackTimeCounter = 0;
    }

    public override void ExitState()
    {
        _context.enemy.animator.SetTrigger("StopAttack");
        //Debug.Log("Acabou Ataque");
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
        //Debug.Log("Tentou Atacar ");
        if (_attackTimeCounter >= _attackCooldown)
        {
            _context.SwitchState("FireAttack");
        }
    }

    public void FireProjectile()
    {
        AudioManager.Instance.Play("WizardAttack");

        GameObject projectile = GameObject.Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);
        EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();
        enemyProjectile.moveDirection = _context.enemy.isFacingRight ? Vector2.right : Vector2.left;
        enemyProjectile.damage = _context.enemy.damage;
    }
}
