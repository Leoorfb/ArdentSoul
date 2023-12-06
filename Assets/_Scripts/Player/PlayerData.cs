using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Health Data")]
    public int maxHealth = 5;
    public int health = 5;

    [Header("Movement Data")]
    public float movementSpeed = 5f;

    public float jumpForce = 5f;

    [Header("Combat Data")]
    public int attackDamage = 1;



    private void Awake()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        health = maxHealth;
    }
}
