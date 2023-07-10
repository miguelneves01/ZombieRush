using System;
using Enemies.Scripts;
using Interfaces;
using Player.Scripts;
using UnityEngine;

namespace Enemies.Skeleton.Scripts
{
    public class SkeletonEnemy : MonoBehaviour, IDamage
    {
        [field: SerializeField] public EnemyStatsSO EnemyStats;
        private float _health;
        
        public event Action DeathEvent; 
        public event Action<float> HurtEvent;
        public event Action AttackEvent;
        
        [SerializeField] private Transform _attackPos;
        [SerializeField] private LayerMask _enemyLayer;

        void Awake()
        {
            _health = EnemyStats.Health;
            GetComponent<SkeletonController>().AttackEvent += Attack;
        }

        public void TakeDamage(float damage)
        {
            HurtEvent?.Invoke(damage);
            _health -= damage;
        }

        void Update()
        {
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            DeathEvent?.Invoke();
        }
        
        public void Attack()
        {
            AttackEvent?.Invoke();
            Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPos.position, EnemyStats.AttackRange,_enemyLayer);
            
            foreach (var hit in hits)
            {
                hit.GetComponent<IDamage>()?.TakeDamage(EnemyStats.AttackDamage);
            }
        }
    }
}