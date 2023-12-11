using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que controla o ataque do inimigo.
/// Contem as variaveis e as funções essenciais do ataque
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    public int damage = 0;
    public Vector3 attackerPosition;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask playerShieldLayer;

    bool wasBlocked = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerShieldLayer == (1 << collision.gameObject.layer))
        {
            OnTouchPlayerShield();
            return;
        }

        if (!wasBlocked & playerLayer == (1 << collision.gameObject.layer))
        {
            OnTouchPlayer();
            return;
        }
    }

    public void OnTouchPlayer()
    {
        Player.Instance.TakeDamage(damage, transform.position.x);
        Destroy(gameObject);
    }
    public void OnTouchPlayerShield()
    {
        if(Player.Instance.playerStateMachine.currentStateKey != "Block")
        {
            return;
        }

        if ((Player.Instance.isFacingLeft && Player.Instance.transform.position.x > attackerPosition.x)
            || (!Player.Instance.isFacingLeft && Player.Instance.transform.position.x < attackerPosition.x))
        {
            Debug.Log("PLAYER BLOQUEOU");
            wasBlocked = true;
            Destroy(gameObject);
        }
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
