using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WanderState : EnemyBaseState
{
    [SerializeField] float wanderDistance = 3f;
    [SerializeField] bool isDisplayOn = false;
    Enemy _enemy;

    public override void EnterState()
    {
        _enemy = _context.enemy;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        float distanceFromStart = (_enemy.initialPosition.x - _enemy.transform.position.x);

        if ((distanceFromStart >= wanderDistance && !_enemy.isFacingRight) || (distanceFromStart <= -wanderDistance && _enemy.isFacingRight))
        {
            _enemy.isFacingRight = !_enemy.isFacingRight;
        }
        _enemy.moveDirectionX = _enemy.isFacingRight ? _enemy.moveSpeed : -_enemy.moveSpeed;

        _enemy.FlipSpriteRender(_enemy.isFacingRight);
    }

    public override void CheckExitCondition()
    {
    }

    public void OnDrawGizmos()
    {
        if (isDisplayOn && _enemy != null)
        {
            Gizmos.DrawLine(new Vector3(_enemy.initialPosition.x - wanderDistance, _enemy.initialPosition.y, 1),
                new Vector3(_enemy.initialPosition.x + wanderDistance, _enemy.initialPosition.y, 1));
        }
    }
}
