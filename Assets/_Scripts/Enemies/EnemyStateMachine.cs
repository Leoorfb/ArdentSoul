using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base para os gerenciadores os estados dos inimigos.
/// Controla e define o estado dos inimigos
/// </summary>
public class EnemyStateMachine : StateManager
{
    protected Enemy _enemy;

    public Enemy enemy { get { return _enemy; } protected set { _enemy = value; }}


    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public float distanceToPlayer(out int direction)
    {
        float distance = Player.Instance.transform.position.x - transform.position.x;
        direction = distance > 0 ? 1 : -1;
        return Mathf.Abs(distance);
    }

    public float distanceToPlayer()
    {
        int direction;
        return distanceToPlayer(out direction);
    }
}
