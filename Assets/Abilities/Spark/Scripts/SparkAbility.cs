using Abilities.Scripts;
using Interfaces;
using UnityEngine;

namespace Abilities.Spark.Scripts
{
    public class SparkAbility : MonoBehaviour, IAbility
    {
        [field: SerializeField] public AbilitySO AbilityStats { get; set; } 
        private float _aliveTime;
        private Transform _player;
        private float _dir;
        private bool _hasDamaged;

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
                enemy?.TakeDamage(AbilityStats.AbilityDamage);
                if (!_hasDamaged)
                {
                    _player.GetComponent<IDamage>().TakeDamage(AbilityStats.AbilityDamage);
                    _hasDamaged = true;
                }
            }
        }
    }
}