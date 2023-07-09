using System;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private bool _debug = true;
        
        private PlayerInput _pInput;

        public static event Action<Vector2, float> PlayerWalkingEvent; 
        public static event Action PlayerDeathEvent; 
        public static event Action<float> PlayerHurtEvent;
        public static event Action PlayerAttackEvent;
        public static event Action<Vector2, float> PlayerDashEvent;

        // Start is called before the first frame update
        void Start()
        {
            _pInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            var dir = _pInput.Dir;
            float speed = 7f;
            PlayerWalkingEvent?.Invoke(dir, speed);
            
            if (_pInput.Attack)
            {
                PlayerAttackEvent?.Invoke();
            }
            if (_pInput.Dash)
            {
                float power = 10f;
                PlayerDashEvent?.Invoke(dir, power);
            }


            if (_debug)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayerDeathEvent?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    PlayerHurtEvent?.Invoke(10);
                }
            }
        }
    }
}