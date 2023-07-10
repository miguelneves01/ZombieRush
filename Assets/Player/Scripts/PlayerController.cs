using System;
using Interfaces;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _pInput;
        private Player _pStats;
        public static event Action<Vector2, float> PlayerWalkingEvent;
        public static event Action PlayerAttackEvent;
        public static event Action<Vector2, float> PlayerDashEvent;
        public static event Action PlayerLoadShopEvent;


        private float _dashCooldown = 0;
        private float _attackCooldown = 0;

        private bool _isDead;

        // Start is called before the first frame update
        void Start()
        {
            _pInput = GetComponent<PlayerInput>();
            _pStats = GetComponent<Player>();
            
            Player.PlayerDeathEvent += PlayerOnPlayerDeathEvent;
        }

        private void PlayerOnPlayerDeathEvent()
        {
            _isDead = true;
        }

        void Update()
        {
            if (_isDead) return;

            _dashCooldown -= Time.deltaTime;
            _attackCooldown -= Time.deltaTime;
            
            var dir = _pInput.Dir;
            float speed = _pStats.PlayerStats.Speed;
            PlayerWalkingEvent?.Invoke(dir, speed);
            
            if (_pInput.Attack && _attackCooldown <= 0)
            {
                PlayerAttackEvent?.Invoke();
                _attackCooldown = 1/_pStats.PlayerStats.AttackSpeed;
            }
            
            if (_pInput.Dash && _dashCooldown <= 0)
            {
                float power = _pStats.PlayerStats.DashPower;
                PlayerDashEvent?.Invoke(dir, power);
                _dashCooldown = _pStats.PlayerStats.DashCooldown;
            }
            
            if (_pInput.Shop)
            {
                PlayerLoadShopEvent?.Invoke();
            }
        }
        
    }
}