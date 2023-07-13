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

        private float _lastHealTime;

        void Start()
        {
            _lastHealTime = 1f;
            
            PlayerStats.Health = PlayerStats.InitialHealth;
            PlayerStats.MaxHealth = PlayerStats.InitialHealth;
            HealthUpdateEvent?.Invoke( PlayerStats.Health, PlayerStats.MaxHealth);
        }

        public void TakeDamage(float damage)
        {
            if (damage > 0)
            {
                PlayerHurtEvent?.Invoke(damage);
            }
            PlayerStats.Health = Mathf.Clamp(PlayerStats.Health - damage, 0f, PlayerStats.MaxHealth);
            HealthUpdateEvent?.Invoke( PlayerStats.Health, PlayerStats.MaxHealth);
        }

        public void SetHp(float hp)
        {
        }

        public void Heal(float damage)
        {
            TakeDamage(-damage);
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