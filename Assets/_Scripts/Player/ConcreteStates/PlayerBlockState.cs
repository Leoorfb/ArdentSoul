using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Classe do estado de bloqueio do player.
/// Contem as variaveis e as funções relacionadas ao bloqueio do player
/// </summary>
[Serializable]
public class PlayerBlockState : PlayerStateBase
{
    public bool isBlockInputOn = false;
    public bool isBlocking = false;
    public LayerMask blockLayer;

    public override void CheckExitCondition()
    {
        if (!isBlockInputOn)
            _context.SwitchState();
    }

    public override void EnterState()
    {
        player.animator.SetBool("IdleBlock", true);
        isBlocking = true;
    }

    public override void ExitState()
    {
        player.animator.SetBool("IdleBlock", false);
        isBlocking = false;
    }

    public override void UpdateState()
    {
        CheckExitCondition();
    }

    public override void TryEnterState()
    {
        if (isBlockInputOn)
            _context.SwitchState("Block");
    }

    public void Block(Collider2D collision)
    {
        if (isBlocking)
        {
            player.animator.SetTrigger("Block");
            GameObject.Destroy(collision.gameObject);
            AudioManager.Instance.Play("PlayerBlock");
        }
    }

    public void SetBlockInputOn(InputAction.CallbackContext obj)
    {
        isBlockInputOn = true;
    }

    public void SetBlockInputOff(InputAction.CallbackContext obj)
    {
        isBlockInputOn = false;
    }
}
