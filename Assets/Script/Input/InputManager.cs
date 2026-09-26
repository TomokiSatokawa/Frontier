using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        public static Vector2 Move;
        public static  float Dash;

        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new();

            _gameInput.Player.Move.performed += OnMove;
            _gameInput.Player.Move.canceled += OnMove;
            _gameInput.Player.Dash.performed += OnDash;
            _gameInput.Player.Dash.canceled += OnDash;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            Dash = context.ReadValue<float>();
        }
        private void OnEnable()
        {
            _gameInput.Player.Enable();
        }

        private void OnDisable()
        {
            _gameInput.Player.Disable();
        }
    }
}