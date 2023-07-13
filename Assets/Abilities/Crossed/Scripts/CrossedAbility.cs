using System;
using Abilities.Scripts;
using Interfaces;
using UnityEngine;

namespace Abilities.Crossed.Scripts
{
    public class CrossedAbility : MonoBehaviour, IAbility
    {
        [field: SerializeField] public AbilitySO AbilityStats { get; set; } 
        private float _aliveTime;
        private Transform _player;
        private float _dir;

        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _dir = _player.localScale.x;
            FlipHorizontally(); 
        }

        void Update()
        {
            _aliveTime += Time.deltaTime;
            if (_aliveTime >= AbilityStats.Time2Live) Destroy(gameObject);
            
            
            transform.position += new Vector3( _dir * AbilityStats.Speed * Time.deltaTime,0);

        }
        
        private void FlipHorizontally()
        {
            if (_dir < 0f)
            {
                transform.Rotate(Vector3.forward, 180);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.TryGetComponent<IDamage>(out var enemy);
                _player.GetComponent<IDamage>().TakeDamage(-AbilityStats.AbilityDamage);
                enemy?.TakeDamage(AbilityStats.AbilityDamage);
            }
        }
    }
}