using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerSpell : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    public int damage = 2;

    Collider2D _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer == (1 << collision.gameObject.layer))
        {
            OnTouchPlayer();
            return;
        }
    }

    public void SpellCasted()
    {
        _collider.enabled = true;
        Debug.Log("CASTOU");
    }

    public void SpellStoped()
    {
        _collider.enabled = false;
        Debug.Log("PAROU");
    }

    public void SpellEnded()
    {
        Destroy(gameObject);
        Debug.Log("TERMINOU");
    }

    void OnTouchPlayer()
    {
        Debug.Log("ACERTOU PLAYER");
        Player.Instance.TakeDamage(damage, transform.position.x);
        _collider.enabled = false;
    }
}
