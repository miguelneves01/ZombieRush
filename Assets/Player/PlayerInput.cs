using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 Dir { get; private set; }

        void Update()
        {
            var hMovement = Input.GetAxis("Horizontal");
            var vMovement = Input.GetAxis("Vertical");
            Dir = new Vector2(hMovement, vMovement).normalized;
        }
    }
}