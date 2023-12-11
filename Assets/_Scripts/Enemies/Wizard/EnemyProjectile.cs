using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que controla o projétil inimigo.
/// Contem as variaveis e as funções essenciais do projétil
/// </summary>
public class EnemyProjectile : MonoBehaviour
{
    public int damage = 0;

    [SerializeField] float moveSpeed = 3f;
    public Vector2 moveDirection = Vector2.zero;

    [SerializeField] float lifeTime = 3f;


    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask obstaclesLayer;

    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer == (1 << collision.gameObject.layer))
        {
            OnTouchPlayer(collision.gameObject.GetComponent<Player>());
            return;
        }

        if (obstaclesLayer == (1 << collision.gameObject.layer))
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTouchPlayer(Player player)
    {
        player.TakeDamage(damage, transform.position.x);
        Destroy(gameObject);
    }
}
