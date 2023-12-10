using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bringer : Enemy
{
    int timesDamaged = 0;

    private void FixedUpdate()
    {
        moveDirectionY = _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = moveDirection;
    }

    public override void GetDamaged(int damage)
    {
        if (isInvulnerable)
            return;
        timesDamaged++;
        health -= damage;

        if (health <= 0)
        {
            _stateMachine.SwitchState("Die");
            return;
        }

        if (timesDamaged % 2 == 0)
        {
            // teleporta e casta spell
        }

        _stateMachine.states["GetHurt"].TryEnterState();
    }

}
