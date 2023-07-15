using System;
using Abilities.Scripts;
using Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.Scripts
{
    public class PlayerInput : MonoBehaviour
    {

        public Vector2 Dir { get; private set; }
        public bool Attack { get; private set; }
        public bool[] Abilities { get; private set; }
        public bool Dash { get; private set; }
        public bool Shop { get; private set; }
        
        private bool _canShop;
        [SerializeField] private bool _hasDash;

        void Start()
        {
            Abilities = new bool[GetComponents<AbilitySpawner>().Length];
        }

        void Update()
        {
            Dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            Attack = Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Alpha4);
            Dash = _hasDash && Input.GetKeyDown(KeyCode.Space);
            Shop = _canShop && Input.GetKeyDown(KeyCode.E);
            for (int i = 0; i < Abilities.Length; i++)
            {
                Abilities[i] = Input.GetKeyDown(GetKeyCodeFromInt(i));
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Tilemap>(out var tilemap))
            {
                Debug.Log("HERE");
                _canShop = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Tilemap>(out var tilemap))
            {
                _canShop = false;
            }
        }

        private KeyCode GetKeyCodeFromInt(int num)
        {
            switch (num)
            {
                case 0: return KeyCode.Alpha1;
                case 1: return KeyCode.Alpha2;
                case 2: return KeyCode.Alpha3;
                default: return default;
            }
        }
    }
}