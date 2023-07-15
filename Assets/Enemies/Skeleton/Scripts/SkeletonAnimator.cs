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
        private AudioSource _audioSource;
        private SkeletonController _skeletonController;
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private Transform _floatingTextPos;

        
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Walking = Animator.StringToHash("Walk");
        private static readonly int Attacking = Animator.StringToHash("Attack");
        private static readonly int GetHit = Animator.StringToHash("Hit");
        private SkeletonEnemy _skeleton;


        void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _skeletonController = GetComponent<SkeletonController>();
            _skeleton = GetComponent<SkeletonEnemy>();
        }

        void Start()
        {
            _skeletonController.WalkingEvent += Walk;
            _skeleton.DeathEvent += Die;
            _skeleton.HurtEvent += Hurt;
            _skeletonController.AttackEvent += Attack;
        }

        private void Walk(Vector2 dir, float speed)
        {
            var position = transform.position;
            //_audioSource.PlayOneShot(_audioClips[0]);
            _animator.SetBool(Walking, !dir.Equals(position));
            FlipHorizontally(dir.x - position.x);
        }
        
        private void Die()
        {
            _audioSource.PlayOneShot(_audioClips[1]);
            _animator.SetBool(Death, true);
        }
        private void Hurt(float damage)
        {
            _audioSource.PlayOneShot(_audioClips[2]);
            _animator.SetTrigger(GetHit);
        }
        private void Attack()
        {
            _audioSource.PlayOneShot(_audioClips[3]);
            _animator.SetTrigger(Attacking);
        }

        private void FlipHorizontally(float flipDir)
        {
            if (flipDir == 0f) return;

            var transform1 = transform;
            var scale = transform1.localScale;
            scale.x = flipDir > 0 ? 1 : -1;
            _floatingTextPos.localScale = scale;
            transform1.localScale = scale;
        }
    }
}