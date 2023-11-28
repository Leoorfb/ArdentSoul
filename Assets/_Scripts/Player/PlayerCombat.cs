using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool canAttack = true;

    private int attackComboIndex = 0; // 0 = não esta em um combo
    private bool isAttacking = false;

    [SerializeField] float attackComboMaxTime = .2f;
    [SerializeField] float attackCooldownTime = .2f;
    private float lastAttackCounter = 0;

    [SerializeField] float attackBufferTime = 0.2f;
    private float attackBufferCounter = 0;

    private InputAction attackInput;
    private InputAction blockInput;

    Animator _animator;
    Player _player;
    PlayerMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _movement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        attackInput = _player.playerInputActions.Player.Attack;
        attackInput.Enable();
        attackInput.performed += AttackBuffer;

        blockInput = _player.playerInputActions.Player.Block;
        blockInput.Enable();
        blockInput.performed += StartBlock;
        blockInput.canceled += StopBlock;
    }

    private void OnDisable()
    {
        attackInput = _player.playerInputActions.Player.Attack;
        attackInput.Disable();

        blockInput = _player.playerInputActions.Player.Block;
        blockInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        lastAttackCounter += Time.deltaTime;
        attackBufferCounter -= Time.deltaTime;

        comboEndCheck();
        canAttackCheck();

        if (canAttack && attackBufferCounter > 0f)
        {
            Attack();
        }
    }

    private void comboEndCheck()
    {
        if (!isAttacking && attackComboIndex != 0 && lastAttackCounter > attackComboMaxTime)
        {
            attackComboIndex = 0;
            Debug.Log("Combo acabou");
        }
    }

    private void canAttackCheck()
    {
        if (isAttacking)
        {
            canAttack = false;
            return;
        }

        if (attackComboIndex != 0)
        {
            canAttack = true;
            return;
        }

        if (lastAttackCounter > attackCooldownTime)
        {
            canAttack = true;
            return;
        }

        canAttack = false;
    }

    private void Attack()
    {
        isAttacking = true;
        attackBufferCounter = 0f;

        switch (attackComboIndex)
        {
            case 1:
                Debug.Log("Combo 2");
                _animator.SetTrigger("Attack2");
                attackComboIndex = 2;
                break;
            case 2:
                Debug.Log("Combo 3");
                _animator.SetTrigger("Attack3");
                attackComboIndex = 0;
                break;
            default:
                Debug.Log("Combo 1");
                _animator.SetTrigger("Attack1");
                attackComboIndex = 1;
                break;
        }
    }

    // Função chamada no fim da animação de ataque por um animation event
    private void AttackEnd()
    {
        isAttacking = false;
        lastAttackCounter = 0;
    }

    private void AttackBuffer(InputAction.CallbackContext obj)
    {
        attackBufferCounter = attackBufferTime;
    }

    private void StopBlock(InputAction.CallbackContext obj)
    {
        _animator.SetBool("IdleBlock", false);
    }

    private void StartBlock(InputAction.CallbackContext obj)
    {
        _animator.SetBool("IdleBlock", true);
        Block(obj);
    }

    private void Block(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("Block");
    }
}
