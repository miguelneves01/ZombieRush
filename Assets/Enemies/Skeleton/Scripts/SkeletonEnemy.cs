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

        void Awake()
        {
            _health = EnemyStats.Health;
        }

        public void TakeDamage(float damage)
        {
            HurtEvent?.Invoke(damage);
            _health -= damage;
            if (_health <= 0) Die();
        }

        public void SetHp(float hp)
        {
            _health = hp;
        }

        private void Die()
        {
            DeathEvent?.Invoke();
        }
    }
}