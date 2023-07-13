using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Skeleton.Scripts;
using Interfaces;
using Player.Scripts;
using UnityEngine;

public class SkeletonAttackPos : MonoBehaviour
{
    private bool _attack;

    private SkeletonEnemy _skeleton;
    private SkeletonController _skeletonCtrl;
    // Start is called before the first frame update
    void Start()
    {
        _skeleton = GetComponentInParent<SkeletonEnemy>();
        _skeletonCtrl = GetComponentInParent<SkeletonController>();
        
        _skeletonCtrl.AttackEvent += SkeletonControllerAttackEvent;
    }

    private void SkeletonControllerAttackEvent()
    {
        _attack = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_attack && other.gameObject.CompareTag("Player"))
        {
            var enemy = other.GetComponent<IDamage>();
            enemy?.TakeDamage(_skeleton.EnemyStats.AttackDamage);
            _attack = false;
        }
    }
}
