using System;
using R3;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        public static Vector2 Move;
        public static float Dash;
        private static ReactiveProperty<bool> _attack = new();
        private static ReactiveProperty<bool> _interact = new();
        private static Subject<Unit> _inventory = new();
        private static Subject<Unit> _nextTab = new();
        private static Subject<Unit> _previousTab = new();

        public static ReadOnlyReactiveProperty<bool> Attack => _attack;
        public static ReadOnlyReactiveProperty<bool> Interact => _interact;
        public static Observable<Unit> Inventory => _inventory;
        public static Observable<Unit> NextTab => _nextTab;
        public static Observable<Unit> PreviousTab => _previousTab;

        private static GameInput _gameInput;
        private static CinemachineInputAxisController _axisController;

        private void Awake()
        {
            _gameInput = new();
            _axisController = FindAnyObjectByType<CinemachineInputAxisController>();

            SetAction(_gameInput.Player.Move, OnMove);
            SetAction(_gameInput.Player.Dash, OnDash);
            SetAction(_gameInput.Player.Attack, OnAttack);
            SetAction(_gameInput.Player.Interact, OnInteract);
            SetAction(_gameInput.UI.NextTab, OnNextTab);
            SetAction(_gameInput.UI.PreviousTab, OnPreviousTab);
            SetAction(_gameInput.Player.Inventory, OnInventory);
            SetAction(_gameInput.UI.Inventory, OnInventory);
        }

        private void SetAction(InputAction action, Action<InputAction.CallbackContext> callback)
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

        public void OnInteract(InputAction.CallbackContext context)
        {
            _interact.Value = context.ReadValueAsButton();
        }

        public void OnNextTab(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
                _nextTab.OnNext(Unit.Default);
        }

        public void OnPreviousTab(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
                _previousTab.OnNext(Unit.Default);
        }
        public void OnInventory(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
                _inventory.OnNext(Unit.Default);
        }

        public static void SetPlayerEnabled(bool enabled)
        {
            if (enabled)
                _gameInput.Player.Enable();
            else
                _gameInput.Player.Disable();

            SetLookEnabled(enabled);
        }

        public static void SetUIEnabled(bool enabled)
        {
            if (enabled)
                _gameInput.UI.Enable();
            else
                _gameInput.UI.Disable();
        }

        public static void SetLookEnabled(bool enabled)
        {
            if (enabled)
            {
                _gameInput.Player.Look.Enable();
            }
            else
            {

                _gameInput.Player.Look.Disable();
            }
            _axisController.enabled = enabled;
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