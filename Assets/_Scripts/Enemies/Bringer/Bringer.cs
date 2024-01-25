using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe principal do Bringer of Death.
/// Contem as variaveis e as funções essenciais do Bringer of Death
/// </summary>
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
            GameUI.Instance.OnPlayerWin();
            return;
        }

        if (timesDamaged % 4 == 0)
        {
            _stateMachine.SwitchState("Teleport");
            GetHurtState hurtState = _stateMachine.states["GetHurt"] as GetHurtState;
            hurtState.HurtSetUp();
            return;
        }

        if (_stateMachine.currentStateKey == "Attack")
        {
            GetHurtState hurtState = _stateMachine.states["GetHurt"] as GetHurtState;
            hurtState.HurtSetUp();
            return;
        }

        _stateMachine.states["GetHurt"].TryEnterState();
    }

}
