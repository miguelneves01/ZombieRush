using System;
using System.Collections;
using UnityEngine;

namespace Player.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {

        private Animator _animator;
        private SpriteRenderer _spriteRender;
        
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Walking = Animator.StringToHash("Walk");
        private static readonly int Attacking = Animator.StringToHash("Attack");
        private static readonly int Dashing = Animator.StringToHash("Roll");


        void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            PlayerController.PlayerWalkingEvent += Walk;
            Player.PlayerDeathEvent += Die;
            Player.PlayerHurtEvent += Hurt;
            PlayerController.PlayerAttackEvent += Attack;
            PlayerController.PlayerDashEvent += Dash;
        }

        private void Walk(Vector2 dir, float speed)
        {
            _animator.SetBool(Walking, dir != Vector2.zero);
            FlipHorizontally(dir.x);
        }
        
        private void Die()
        {
            _animator.SetBool(Death, true);
        }
        private void Hurt(float damage)
        {
            StartCoroutine(TakeDamage());
        }
        private void Attack()
        {
            _animator.SetTrigger(Attacking);
        }
        private void Dash(Vector2 dir, float power)
        {
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
            transform1.localScale = scale;
        }
    }
}