using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado de proteger do inimigo.
/// Contem as variaveis e as funções relacionadas a proteger do inimigo.
/// O inimigo fica parado em um local e ataca quando o player se aproxima.
/// </summary>
[Serializable]
public class ProtectState : EnemyBaseState
{
    [SerializeField] float protectRadius = 5f;

    [SerializeField] float checkCooldown = .15f;
    private float checkTimeCounter = 0f;

    private bool hasPlayerInRange = false;
    public override void CheckExitCondition()
    {
        if (hasPlayerInRange)
        {
            _context.states["FireAttack"].TryEnterState();
        }
    }

    public override void EnterState()
    {
        checkTimeCounter = checkCooldown;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        checkTimeCounter -= Time.deltaTime;

        if (checkTimeCounter <= 0f)
        {
            CheckPlayerIsInRange();
        }

        CheckExitCondition();
    }

    private void CheckPlayerIsInRange()
    {
        checkTimeCounter = checkCooldown;
        Collider2D hitPlayer = Physics2D.OverlapCircle(_context.transform.position, protectRadius, _context.enemy.playerLayer);

        if (hitPlayer == null)
        {
            hasPlayerInRange = false;
            return;
        }

        float playerXPos = hitPlayer.transform.position.x;
        _context.enemy.isFacingRight = playerXPos > _context.transform.position.x;
        float playerDir = _context.enemy.isFacingRight ? 180 : 0;
        _context.transform.rotation = Quaternion.Euler(0, playerDir, 0);

        hasPlayerInRange = true;
    }
}
