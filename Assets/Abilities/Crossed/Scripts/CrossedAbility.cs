using System;
using Abilities.Scripts;
using Interfaces;
using Player.Scripts;
using UnityEngine;

namespace Abilities.Crossed.Scripts
{
    public class CrossedAbility : MonoBehaviour, IAbility
    {
        [field: SerializeField] public AbilitySO AbilityStats { get; set; } 
        private float _aliveTime;
        private Transform _player;
        private float _dir;
        private bool _changedDir;
        [SerializeField] private AudioClip[] _audioClips;
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource.PlayOneShot(_audioClips[0]);
        }

        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _dir = _player.localScale.x;
            FlipHorizontally(_dir); 
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            _aliveTime += Time.deltaTime;
            // if (_aliveTime >= AbilityStats.Time2Live) Destroy(gameObject);

            if (_aliveTime <= AbilityStats.Time2Live/2)
            {
                transform.position += new Vector3( _dir * AbilityStats.Speed * Time.deltaTime,0);
            }
            else
            {
                if (!_changedDir)
                {
                    FlipHorizontally(-1);
                    _changedDir = true;
                }
                
                transform.position = Vector3.MoveTowards(transform.position, _player.position,
                    AbilityStats.Speed * Time.deltaTime);
            }

        }
        
        private void FlipHorizontally(float dir)
        {
            if (dir < 0f)
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
            if (other.gameObject.CompareTag("Player") && _aliveTime >= AbilityStats.Time2Live/2)
            {
                _player.GetComponent<PlayerController>().AbilitiesCooldown[0] -= AbilityStats.Time2Live/2;
                Destroy(gameObject);
            }
        }
    }
}