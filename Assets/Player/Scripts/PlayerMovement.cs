using System;
using System.Collections;
using UnityEngine;

namespace Player.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        
        private bool _isDashing = false;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            PlayerController.PlayerWalkingEvent += Move;
            PlayerController.PlayerDashEvent += Dash;
        }

        private void Dash(Vector2 dir, float power)
        {
            if (_isDashing) return;

            Vector2 curDir = dir == Vector2.zero? new Vector2(transform.localScale.x,0) : dir;
            Vector2 dashForce = curDir * power;
            _isDashing = true;
            _rb.velocity = dashForce;
            StartCoroutine(StopDashing());
        }

        private void Move(Vector2 dir, float speed)
        {
            if (_isDashing) return;
            
            Vector2 moveForce = dir * speed;
            _rb.velocity = moveForce;
        }

        private IEnumerator StopDashing()
        {
            yield return new WaitForSeconds(1);
            _isDashing = false;
        }
        
        
    }
}