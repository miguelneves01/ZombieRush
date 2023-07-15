using System;
using System.Collections.Generic;
using Abilities.Scripts;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        
        [SerializeField] private PlayerScore _playerScore;

        private PlayerInput _pInput;
        private Player _pStats;
        public event Action<Vector2, float> PlayerWalkingEvent;
        public event Action PlayerAttackEvent;
        public event Action<Vector2, float> PlayerDashEvent;

        private AbilitySpawner[] _abilities;
        public float[] AbilitiesCooldown;
        
        private float _attackCooldown = 0;

        private bool _isDead;

        void Awake()
        {
            _playerScore.CurrentScore = 0;
            _pInput = GetComponent<PlayerInput>();
            _pStats = GetComponent<Player>();
            _abilities = GetComponents<AbilitySpawner>();
            AbilitiesCooldown = new float[_abilities.Length + 1];
        }

        // Start is called before the first frame update
        void Start()
        {
            _pStats.PlayerDeathEvent += PlayerOnPlayerDeathEvent;
        }

        private void PlayerOnPlayerDeathEvent()
        {
            _isDead = true;
            if (_playerScore.CurrentScore > _playerScore.HighScore)
            {
                _playerScore.HighScore = _playerScore.CurrentScore;
            }
        }

        void Update()
        {
            if (_isDead) return;
            
            _attackCooldown -= Time.deltaTime;
            for (int i = 0; i < AbilitiesCooldown.Length; i++)
            {
                AbilitiesCooldown[i] -= Time.deltaTime;
            }

            var dir = _pInput.Dir;
            float speed = _pStats.PlayerStats.Speed;
            PlayerWalkingEvent?.Invoke(dir, speed);
            
            if (_pInput.Attack && _attackCooldown <= 0)
            {
                PlayerAttackEvent?.Invoke();
                _attackCooldown = 1/_pStats.PlayerStats.AttackSpeed;
            }

            bool[] fireAbilities = _pInput.Abilities;
            for (int i = 0; i < fireAbilities.Length; i++)
            {
                if (fireAbilities[i] && AbilitiesCooldown[i] < 0)
                {
                    _abilities[i].FireAbility();
                    AbilitiesCooldown[i] = _abilities[i].AbilityStats.AbilityCooldown;
                }
            }
            
            if (_pInput.Dash && AbilitiesCooldown[^1] <= 0)
            {
                float power = _pStats.PlayerStats.DashPower;
                PlayerDashEvent?.Invoke(dir, power);
                AbilitiesCooldown[^1] = _pStats.PlayerStats.DashCooldown;
            }
        }
        
    }
}