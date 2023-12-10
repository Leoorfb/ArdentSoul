using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChaseState : EnemyBaseState
{
    float distanceToPlayer = 999f;
    int directionToPlayer = 1;
    Enemy _enemy;

    public override void CheckExitCondition()
    {
        _context.states["Attack"].TryEnterState();
    }

    public override void EnterState()
    {
        _enemy = _context.enemy;
        _context.enemy.animator.SetBool("isWalking", true);
    }

    public override void ExitState()
    {
        _context.enemy.animator.SetBool("isWalking", false);
    }

    public override void UpdateState()
    {
        distanceToPlayer = _context.distanceToPlayer(out directionToPlayer);

        _enemy.isFacingRight = directionToPlayer > 0 ? false : true;
        Vector3 playerDir = _enemy.isFacingRight? new Vector3(1f,1f,1f): new Vector3(-1f, 1f, 1f);

        _context.transform.localScale = playerDir;

        _enemy.moveDirectionX = directionToPlayer * _enemy.moveSpeed;

        CheckExitCondition();
    }
}