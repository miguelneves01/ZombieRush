using System;
using Enemies.Scripts;
using Interfaces;
using Player.Scripts;
using TMPro;
using UnityEngine;

namespace Enemies.Skeleton.Scripts
{
    public class SkeletonEnemy : MonoBehaviour, IDamage
    {
        [SerializeField] private PlayerScore _playerScore;
        [field: SerializeField] public EnemyStatsSO EnemyStats;
        [SerializeField] private GameObject _floatingText;
        [SerializeField] private Transform _floatingTextPos;
        private float _health;

        public event Action DeathEvent; 
        public event Action<float> HurtEvent;

        void Awake()
        {
            _health = EnemyStats.Health;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            var popup = Instantiate(_floatingText, _floatingTextPos);
            var popupText = popup.GetComponent<TMP_Text>();
            popupText.text = Mathf.FloorToInt(Mathf.Abs(damage)).ToString();
            popupText.color = Color.red;
            if (_health > 0)
            {
                HurtEvent?.Invoke(damage);
            }
            else
            {
                DeathEvent?.Invoke();
                _playerScore.CurrentScore++;
            }
        }

        public void SetHp(float hp)
        {
            _health = hp;
        }
    }
}