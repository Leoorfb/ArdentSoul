using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerAttackState : PlayerStateBase
{
    [SerializeField] LayerMask enemyLayers;

    [SerializeField] float _attackComboMaxTime = .2f;
    [SerializeField] float _attackCooldownTime = .2f;
    float _lastAttackCounter = 0;

    [SerializeField] float _attackBufferTime = 0.2f;
    float _attackBufferCounter = 0;

    private bool _canAttack = true;

    private int _attackComboIndex = 0; // 0 = não esta em um combo
    private bool _hasAttackAnimationEnded = true;


    [SerializeField] Color gizmosColor = Color.white;
    [SerializeField] bool isAttack1HitboxOn = true;
    [SerializeField] Vector2 attack1Size = Vector2.one;
    [SerializeField] Transform attack1Center;

    [SerializeField] bool isAttack2HitboxOn = true;
    [SerializeField] Vector2 attack2Size = Vector2.one;
    [SerializeField] Transform attack2Center;

    [SerializeField] bool isAttack3HitboxOn = true;
    [SerializeField] Vector2 attack3Size = Vector2.one;
    [SerializeField] Transform attack3Center;

    public override void TryEnterState()
    {
        if (_canAttack && _attackBufferCounter > 0f)
        {
            _context.SwitchState("Attack");
        }
    }

    public override void StateIndenpendetUpdate()
    {
        AttackSetup();
    }

    public override void CheckExitCondition()
    {
        if (_hasAttackAnimationEnded == true)
        {
            _context.SwitchState("Idle");
        }
    }

    public override void EnterState()
    {
        Attack();
    }

    public override void ExitState()
    {
        _hasAttackAnimationEnded = true;
    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }

    private void Attack()
    {
        _hasAttackAnimationEnded = false;
        _attackBufferCounter = 0f;

        Collider2D[] hitEnemies = null;
        switch (_attackComboIndex)
        {
            case 1:
                //Debug.Log("Combo 2");
                player.animator.SetTrigger("Attack2");
                _attackComboIndex = 2;
                hitEnemies = Physics2D.OverlapBoxAll(attack2Center.position, attack2Size, 0f, enemyLayers);
                break;
            case 2:
                //Debug.Log("Combo 3");
                player.animator.SetTrigger("Attack3");
                _attackComboIndex = 0;
                hitEnemies = Physics2D.OverlapBoxAll(attack3Center.position, attack3Size, 0f, enemyLayers);
                break;
            default:
                //Debug.Log("Combo 1");
                player.animator.SetTrigger("Attack1");
                _attackComboIndex = 1;
                hitEnemies = Physics2D.OverlapBoxAll(attack1Center.position, attack1Size, 0f, enemyLayers);
                break;
        }

        if (hitEnemies is null) return;

        foreach (Collider2D hit in hitEnemies)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            enemy.GetDamaged(playerData.attackDamage);
        }
    }

    #region Attack Setup
    private void AttackSetup()
    {
        _lastAttackCounter += Time.deltaTime;
        _attackBufferCounter -= Time.deltaTime;
        ComboEndCheck();
        CanAttackCheck();
    }

    // Função chamada no fim da animação de ataque por um animation event
    public void OnAnimationEnd()
    {
        _hasAttackAnimationEnded = true;
        _lastAttackCounter = 0;
    }

    public void AttackBuffer(InputAction.CallbackContext obj)
    {
        _attackBufferCounter = _attackBufferTime;
    }

    private void ComboEndCheck()
    {
        if (_hasAttackAnimationEnded && _attackComboIndex != 0 && _lastAttackCounter > _attackComboMaxTime)
        {
            _attackComboIndex = 0;
            //Debug.Log("Combo acabou");
        }
    }

    private void CanAttackCheck()
    {
        if (!_hasAttackAnimationEnded)
        {
            _canAttack = false;
            return;
        }

        if (_attackComboIndex != 0)
        {
            _canAttack = true;
            return;
        }

        if (_lastAttackCounter > _attackCooldownTime)
        {
            _canAttack = true;
            return;
        }

        _canAttack = false;
    }
    #endregion

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;

        if (isAttack1HitboxOn)
            Gizmos.DrawCube(attack1Center.position, attack1Size);

        if (isAttack2HitboxOn)
            Gizmos.DrawCube(attack2Center.position, attack2Size);

        if (isAttack3HitboxOn)
            Gizmos.DrawCube(attack3Center.position, attack3Size);
    }
}