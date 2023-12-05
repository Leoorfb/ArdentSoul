using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Health Data")]
    [SerializeField] private int _maxHealth = 5;
    int _health = 5;

    [Header("Movement Data")]
    [SerializeField] float _movementSpeed = 5f;

    [SerializeField] float _jumpForce = 5f;

    [Header("Combat Data")]
    [SerializeField] int _attackDamage = 1;

    //getters e setters
    public int maxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public int health { get { return _health; } set { _health = value; } }

    public float movementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }
    public float jumpForce { get { return _jumpForce; } set { _jumpForce = value; } }

    public int attackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
}
