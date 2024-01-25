using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe do estado de ataque de magia do inimigo.
/// Contem as variaveis e as funções relacionadas ao ataque de magia do inimigo
/// </summary>
[Serializable]
public class CastState : EnemyBaseState
{
    public bool hasFinishedCasting = false;
    [SerializeField] GameObject spellPrefab;

    [Header("Cast Timers")]
    [SerializeField] float _highCastTimerLimit = 5f;
    float _highCastCounter = 0;
    public bool canHighCast = false;

    float _lastCastCounter = 0;
    float _minCastDelay = 1;
    bool animationTriggered = false;

    int castIndex = 0;

    Player player;

    [Header("Cast Patterns")]
    [SerializeField] Transform highPattern;
    [SerializeField] Transform[] lowPatterns;

    public override void CheckExitCondition()
    {
        if (!hasFinishedCasting) return;

        if (!canHighCast && castIndex < lowPatterns.Length)
        {
            _context.SwitchState(this);
            return;
        }
        castIndex = 0;
        _context.SwitchState();
    }

    public override void SetUpState(EnemyStateMachine context)
    {
        base.SetUpState(context);
        player = Player.Instance;
    }

    public override void EnterState()
    {
        Debug.Log("INVOCANDO");
        animationTriggered = false;
        hasFinishedCasting = false;

        if (_lastCastCounter > _minCastDelay) 
        {
            TriggerAnimation();
        }
        
    }

    void TriggerAnimation()
    {
        animationTriggered = true;
        _context.enemy.animator.SetTrigger("Cast");
    }

    public override void ExitState()
    {
        _highCastCounter = _highCastTimerLimit;
        _lastCastCounter = 0;

    }

    public override void UpdateState()
    {
        if (!animationTriggered && _lastCastCounter > _minCastDelay)
        {
            TriggerAnimation();
        }

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
        _lastCastCounter += Time.deltaTime;
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
        AudioManager.Instance.Play("BossSpell");

        if (canHighCast)
        {
            CastSpells(highPattern);
            return;
        }

        CastSpells(lowPatterns[castIndex]);
        castIndex++;
    }

    public void OnCastAnimationEnd()
    {
        
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