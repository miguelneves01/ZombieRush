using UnityEngine;

namespace Player.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 Dir { get; private set; }
        public bool Attack { get; private set; }
        public bool Dash { get; private set; }

        void Update()
        {
            Dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            Attack = Input.GetKeyDown(KeyCode.Mouse0);
            Dash = Input.GetKeyDown(KeyCode.Space);
        }
    }
}