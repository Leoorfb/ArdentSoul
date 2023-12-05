using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateManager
{
    protected Enemy _enemy;

    public Enemy enemy { get { return _enemy; } protected set { _enemy = value; }}


    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
}
