using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 Dir { get; private set; }
        public bool Attack { get; private set; }
        public bool Dash { get; private set; }
        public bool Shop { get; private set; }
        
        private bool _canShop;
        

        void Update()
        {
            Dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            Attack = Input.GetKeyDown(KeyCode.Mouse0);
            Dash = Input.GetKeyDown(KeyCode.Space);
            Shop = _canShop && Input.GetKeyDown(KeyCode.E);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Tilemap>(out var tilemap))
            {
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
    }
}