using System;
using System.Collections;
using Player.Scripts;
using UnityEngine;

namespace Enemies.Skeleton.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class SkeletonAnimator : MonoBehaviour
    {

        private Animator _animator;
        private SpriteRenderer _spriteRender;
        private SkeletonController _skeletonController;
        
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Walking = Animator.StringToHash("Walk");
        private static readonly int Attacking = Animator.StringToHash("Attack");
        private static readonly int GetHit = Animator.StringToHash("Hit");
        private SkeletonEnemy _skeleton;


        void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRender = GetComponent<SpriteRenderer>();
            _skeletonController = GetComponent<SkeletonController>();
            _skeleton = GetComponent<SkeletonEnemy>();
        }

        void Start()
        {
            _skeletonController.WalkingEvent += Walk;
            _skeleton.DeathEvent += Die;
            _skeleton.HurtEvent += Hurt;
            _skeleton.AttackEvent += Attack;
        }

        private void Walk(Vector2 dir, float speed)
        {
            var position = transform.position;
            _animator.SetBool(Walking, !dir.Equals(position));
            FlipHorizontally(dir.x - position.x);
        }
        
        private void Die()
        {
            _animator.SetBool(Death, true);
        }
        private void Hurt(float damage)
        {
            _animator.SetTrigger(GetHit);
        }
        private void Attack()
        {
            _animator.SetTrigger(Attacking);
        }

        private void FlipHorizontally(float flipDir)
        {
            if (flipDir == 0f) return;

            var transform1 = transform;
            var scale = transform1.localScale;
            scale.x = flipDir > 0 ? 1 : -1;
            transform1.localScale = scale;
        }
    }
}