using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CastState : EnemyBaseState
{
    public bool hasFinishedCasting = false;
    [SerializeField] GameObject spellPrefab;

    [Header("Cast Timers")]
    [SerializeField] float _highCastTimerLimit = 5f;
    float _highCastCounter = 0;
    public bool canHighCast = false;

    Player player;

    [Header("Cast Patterns")]
    [SerializeField] Transform highPattern;
    [SerializeField] Transform[] lowPatterns;

    public override void CheckExitCondition()
    {
        if (hasFinishedCasting)
        {
            _context.SwitchState();
        }
    }

    public override void SetUpState(EnemyStateMachine context)
    {
        base.SetUpState(context);
        player = Player.Instance;
    }

    public override void EnterState()
    {
        Debug.Log("INVOCANDO");
        hasFinishedCasting = false;
        _context.enemy.animator.SetTrigger("Cast");
    }

    public override void ExitState()
    {
        _highCastCounter = _highCastTimerLimit;

    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }

    public override void StateIndenpendetUpdate()
    {
        if (player.transform.position.y > _context.transform.position.y)
        {
            _highCastCounter -= Time.deltaTime;
        }
        else
        {
            _highCastCounter = _highCastTimerLimit;
        }

        canHighCast = _highCastCounter < 0;
    }

    public override void TryEnterState()
    {
        if (canHighCast)
        {
            _context.SwitchState("Cast");
        }
    }

    public void OnCastAnimation()
    {
        if (canHighCast)
        {
            CastSpells(highPattern);
            return;
        }
    }

    public void OnCastAnimationEnd()
    {
        if (canHighCast)
        {
            hasFinishedCasting = true;
            return;
        }
        hasFinishedCasting = true;

    }

    public void CastSpells(Transform spellPattern)
    {
        foreach (Transform position in spellPattern)
        {
            CastSpell(position);
        }
    }

    public void CastSpell(Transform spellPosition)
    {
        BringerSpell spell = GameObject.Instantiate(spellPrefab, spellPosition.position, spellPosition.rotation).GetComponent<BringerSpell>();
        spell.damage = _context.enemy.damage;
    }
}