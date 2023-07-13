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

        private bool _isDead;
        // Start is called before the first frame update
        void Start()
        {
            _stats = GetComponent<SkeletonEnemy>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            
            _stats.DeathEvent += StatsOnDeathEvent;
        }

        private void StatsOnDeathEvent()
        {
            _isDead = true;
        }

        void Update()
        {
            if (_isDead) return;

                var playerPos = _player.position;
            
            _attackCooldown -= Time.deltaTime;
            if (!InRange(playerPos))
            {
                
                WalkingEvent?.Invoke(playerPos, _stats.EnemyStats.Speed);
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
            return Mathf.Abs(position.x - pos.x) <= attackRange &&
                   Mathf.Abs(position.y - pos.y) <= attackRange;
        }
    }
}