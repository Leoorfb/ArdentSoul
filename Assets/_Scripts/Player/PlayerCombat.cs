using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    Animator _animator;
    Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {

        

        
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
