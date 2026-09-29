using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        public static Vector2 Move;
        public static  float Dash;
        private static ReactiveProperty<bool> _attack = new();
        public static ReadOnlyReactiveProperty<bool> Attack => _attack;

        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new();

            SetAction(_gameInput.Player.Move, OnMove);
            SetAction(_gameInput.Player.Dash, OnDash);
            SetAction(_gameInput.Player.Attack, OnAttack);
        }

        private void SetAction(InputAction action , Action<InputAction.CallbackContext> callback)
        {
            action.performed += callback;
            action.canceled += callback;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            Dash = context.ReadValue<float>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            _attack.Value = context.ReadValueAsButton();
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