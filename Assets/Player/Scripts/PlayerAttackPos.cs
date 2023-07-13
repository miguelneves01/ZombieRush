using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Player.Scripts;
using UnityEngine;

public class PlayerAttackPos : MonoBehaviour
{
    private bool _attack;

    private Player.Scripts.Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponentInParent<Player.Scripts.Player>();
        PlayerController.PlayerAttackEvent += PlayerControllerOnPlayerAttackEvent;
    }

    private void PlayerControllerOnPlayerAttackEvent()
    {
        _attack = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_attack && other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<IDamage>();
            enemy?.TakeDamage(_player.PlayerStats.AttackDamage);
            _attack = false;
        }
    }
}
