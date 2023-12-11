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

    [SerializeField] LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer == (1 << collision.gameObject.layer))
        {
            OnTouchPlayer(collision.gameObject.GetComponent<Player>());
            return;
        }
    }

    public void OnTouchPlayer(Player player)
    {
        player.TakeDamage(damage, transform.position.x);
        Destroy(gameObject);
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
