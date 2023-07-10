using System;
using Interfaces;
using UnityEngine;

namespace Player.Scripts
{
    public class Player : MonoBehaviour, IDamage
    {
        [field: SerializeField] public PlayerStatsSO PlayerStats { get; set; }
        
        public static event Action<float, float> HealthUpdateEvent;
        public static event Action PlayerDeathEvent; 
        public static event Action<float> PlayerHurtEvent;
        public static event Action PlayerAttackEvent;
        
        private float _lastHealTime;
        
        [SerializeField] private Transform _attackPos;
        [SerializeField] private LayerMask _enemyLayer;

        void Start()
        {
            _lastHealTime = 1f;
            
            PlayerStats.Health = PlayerStats.InitialHealth;
            PlayerStats.MaxHealth = PlayerStats.InitialHealth;
            HealthUpdateEvent?.Invoke( PlayerStats.Health, PlayerStats.MaxHealth);

            PlayerController.PlayerAttackEvent += Attack;
        }

        public void TakeDamage(float damage)
        {
            PlayerStats.Health = Mathf.Clamp(PlayerStats.Health - damage, 0f, PlayerStats.MaxHealth);
            HealthUpdateEvent?.Invoke( PlayerStats.Health, PlayerStats.MaxHealth);
            if (damage > 0)
            {
                PlayerHurtEvent?.Invoke(damage);
            }
        }

        public void Heal(float damage)
        {
            TakeDamage(-damage);
        }
        
        public void Attack()
        {
            PlayerAttackEvent?.Invoke();
            Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPos.position, PlayerStats.AttackRange,_enemyLayer);

            foreach (var hit in hits)
            {
                hit.GetComponent<IDamage>()?.TakeDamage(PlayerStats.AttackDamage);
            }
        }

        void Update()
        {
            _lastHealTime -= Time.deltaTime;
            
            float curHealth = PlayerStats.Health;
            if (curHealth <= 0)
            {
                PlayerDeathEvent?.Invoke();
                return;
            }
            
            if (_lastHealTime <= 0 && curHealth < PlayerStats.MaxHealth)
            {
                Heal(PlayerStats.HealthRegen);
                _lastHealTime = 1;
            }
        }
    }
}