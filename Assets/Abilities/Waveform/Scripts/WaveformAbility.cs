using System;
using System.Collections;
using Abilities.Scripts;
using Interfaces;
using UnityEngine;

namespace Abilities.Waveform.Scripts
{
    public class WaveformAbility : MonoBehaviour, IAbility
    {
        [field: SerializeField] public AbilitySO AbilityStats { get; set; }
        [SerializeField] private RuntimeAnimatorController _explosionAnimator;
        private float _aliveTime;
        private Transform _player;
        private float _dir;
        private float _speed;
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
            FlipHorizontally();
            _speed = AbilityStats.Speed;
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            _aliveTime += Time.deltaTime;
            if (_aliveTime >= AbilityStats.Time2Live) Destroy(gameObject);
            transform.position += new Vector3( _dir * _speed * Time.deltaTime,0);

        }
        
        private void FlipHorizontally()
        {
            if (_dir < 0f)
            {
                transform.Rotate(Vector3.forward, 180);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                other.gameObject.TryGetComponent<IDamage>(out var enemy);
                enemy?.TakeDamage(AbilityStats.AbilityDamage * 2);
                var collider = GetComponent<CircleCollider2D>();
                collider.isTrigger = true;
                collider.radius = 5f;
                _speed = 0;
                _audioSource.PlayOneShot(_audioClips[1]);
                GetComponent<Animator>().runtimeAnimatorController = _explosionAnimator;
                StartCoroutine(SelfDestruct());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.TryGetComponent<IDamage>(out var enemy);
                enemy?.TakeDamage(AbilityStats.AbilityDamage * 2);
            }
        }

        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}