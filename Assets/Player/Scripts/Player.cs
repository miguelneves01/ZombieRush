using System;
using Interfaces;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player.Scripts
{
    public class Player : MonoBehaviour, IDamage
    {
        [field: SerializeField] public PlayerStatsSO PlayerStats { get; set; }
        
        public event Action<float, float> HealthUpdateEvent;
        public event Action PlayerDeathEvent; 
        public event Action<float> PlayerHurtEvent;

        private float _lastHealTime;
        [SerializeField] private GameObject _floatingText;
        [SerializeField] private Transform _floatingTextPos;

        void Start()
        {
            _lastHealTime = 1f;
            
            PlayerStats.Health = PlayerStats.InitialHealth;
            PlayerStats.MaxHealth = PlayerStats.InitialHealth;
            HealthUpdateEvent?.Invoke( PlayerStats.Health, PlayerStats.MaxHealth);
        }

        public void TakeDamage(float damage)
        {
            var popup = Instantiate(_floatingText, _floatingTextPos);
            var popupText = popup.GetComponent<TMP_Text>();
            popupText.text = Mathf.FloorToInt(Mathf.Abs(damage)).ToString();
            popupText.color = Color.green;
            if (damage > 0)
            {
                PlayerHurtEvent?.Invoke(damage);
                popupText.color = Color.red;
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