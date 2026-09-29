using R3;
using PlayerInput;
using UnityEngine;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// InventoryHeader‚ð“®‚©‚·
    /// </summary>
    public class InventoryHeaderController : MonoBehaviour
    {
        [SerializeField] private InventoryHeaderView[] _buttons;

        private ReactiveProperty<InventoryItemLayer> _layer = new();
        public ReadOnlyReactiveProperty<InventoryItemLayer> Layer => _layer;

        private int _currentSelect = 0;
        void Start()
        {
            InputManager.NextTab.Subscribe(_ => MoveSelect(1));
            InputManager.PreviousTab.Subscribe(_ => MoveSelect(-1));

            foreach(var button in _buttons)
            {
                button.OnClick += OnSelect;
            }
        }

        private void MoveSelect(int amount)
        {
            var clamped = Mathf.Clamp(_currentSelect + amount, 0, _buttons.Length - 1);
            if (_currentSelect == clamped) return;

            _currentSelect = clamped;
            _buttons[_currentSelect].SetSelect();
        }

        public void OnSelect(InventoryItemLayer layer)
        {
            _layer.Value = layer;
        }
    }
    public enum InventoryItemLayer
    {
        Item,
        Equipment,
        Weapon,
    }

}