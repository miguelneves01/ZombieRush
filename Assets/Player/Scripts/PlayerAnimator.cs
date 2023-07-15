using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {

        private Animator _animator;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;
        private SpriteRenderer _spriteRender;
        [SerializeField] private Transform _floatingTextPos;

        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Walking = Animator.StringToHash("Walk");
        private static readonly int Attacking = Animator.StringToHash("Attack");
        private static readonly int Dashing = Animator.StringToHash("Roll");


        void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRender = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();

        }

        void Start()
        {
            var pc = GetComponent<PlayerController>();
            var p = GetComponent<Player>();
            pc.PlayerWalkingEvent += Walk;
            p.PlayerDeathEvent += Die;
            p.PlayerHurtEvent += Hurt;
            pc.PlayerAttackEvent += Attack;
            pc.PlayerDashEvent += Dash;
        }

        private void Walk(Vector2 dir, float speed)
        {
            //_audioSource.PlayOneShot(_audioClips[3]);
            _animator.SetBool(Walking, dir != Vector2.zero);
            FlipHorizontally(dir.x);
        }
        
        private void Die()
        {
            if (_animator.GetBool(Death))
            {
                return;
            }
            
            _audioSource.PlayOneShot(_audioClips[2]);
            _animator.SetBool(Death, true);
        }
        private void Hurt(float damage)
        {
            _audioSource.PlayOneShot(_audioClips[1]);
            StartCoroutine(TakeDamage());
        }
        private void Attack()
        {
            _audioSource.PlayOneShot(_audioClips[0], 0.3f);
            _animator.SetTrigger(Attacking);
        }
        private void Dash(Vector2 dir, float power)
        {
            _audioSource.PlayOneShot(_audioClips[4]);
            _animator.SetTrigger(Dashing);
        }

        private IEnumerator TakeDamage()
        {
            _spriteRender.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _spriteRender.color = Color.white;
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