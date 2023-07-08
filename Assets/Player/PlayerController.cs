using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _pInput;
        private PlayerMovement _pMovement;

        // Start is called before the first frame update
        void Start()
        {
            _pInput = GetComponent<PlayerInput>();
            _pMovement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            Vector2 dir = _pInput.Dir;
            _pMovement.Move(dir);
        }
    }
}