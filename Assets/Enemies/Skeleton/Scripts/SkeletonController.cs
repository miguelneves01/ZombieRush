using System;
using Enemies.Skeleton.Scripts;
using Interfaces;
using UnityEngine;

namespace Player.Scripts
{
    public class SkeletonController : MonoBehaviour
    {
        private SkeletonEnemy _stats;
        public event Action<Vector2, float> WalkingEvent;
        public event Action AttackEvent;
        
        private float _attackCooldown = 0;
        private Transform _player;

        // Start is called before the first frame update
        void Start()
        {
            _stats = GetComponent<SkeletonEnemy>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            var playerPos = _player.position;
            
            _attackCooldown -= Time.deltaTime;
            if (!InRange(playerPos))
            {
                float speed = _stats.EnemyStats.Speed;
                WalkingEvent?.Invoke(playerPos, speed);
            }
            else if(_attackCooldown <= 0)
            {
                AttackEvent?.Invoke();
                _attackCooldown = 1/_stats.EnemyStats.AttackSpeed;
            }
        }
        

        public bool InRange(Vector3 pos)
        {
            float attackRange = _stats.EnemyStats.AttackRange;
            var position = transform.position;
            var position1 = _player.position;
            return Mathf.Abs(position.x - position1.x) <= attackRange &&
                   Mathf.Abs(position.y - position1.y) <= attackRange;
        }
    }
}