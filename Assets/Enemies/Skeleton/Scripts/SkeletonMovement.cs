using System;
using System.Collections;
using UnityEngine;

namespace Player.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SkeletonMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;


        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GetComponent<SkeletonController>().WalkingEvent += Move;
        }

        private void Move(Vector2 dir, float speed)
        {
            _rb.position = Vector3.MoveTowards(_rb.position, dir, speed * Time.deltaTime);
        }


    }
}