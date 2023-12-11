using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe principal do slime.
/// Contem as variaveis e as funções essenciais do slime
/// </summary>
public class Slime : Enemy
{
    private void FixedUpdate()
    {
        moveDirectionY = _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = moveDirection;
    }

    public override void GetDamaged(int damage)
    {
        health -= damage;

        if (health <= 0)
            _stateMachine.SwitchState("Die");

        else
            _stateMachine.SwitchState("GetHurt");
    }
}
