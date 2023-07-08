using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;

        [SerializeField] private float _speed = 5f;
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 dir)
        {
            Vector2 moveForce = dir * _speed;
            _rb.velocity = moveForce;

        }
    }
}