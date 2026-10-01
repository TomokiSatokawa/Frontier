using Common;
using Common.UI;
using InGame.Player;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// インベントリ中のアイテムを選択した時に表示する操作ウィンドウを制御する。
    /// </summary>
    public class ItemActionWindowControl : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryManager _inventoryManager;
        [SerializeField] private PanelControl _panelControl;
        [SerializeField] private RectTransform _panelRect;
        [SerializeField] private RectTransform _canvasRect;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private FloatRange _verticalRange;
        [Header("Buttons")]
        [SerializeField] private Button _equipmentButton;

        private InventoryItemView _currentSelect;

        private void Start()
        {
            _equipmentButton.onClick.AddListener(OnEquipment);
        }

        public void OnSelectItem(InventoryItemView inventoryItemView)
        {
            Vector2 position = GetPosition(inventoryItemView.Rect);

            position += _offset;
            position.y = _verticalRange.Clamp(position.y);

            _panelRect.anchoredPosition = position;
            _panelControl.OnActive();

            ButtonActive(inventoryItemView.Data.Definition.Layer);

            _currentSelect = inventoryItemView;
        }

        public void OnEquipment()
        {
            _inventoryManager.EquipmentItem(_currentSelect.Data);
        }

        public void OnHidden()
        {
            _panelControl.OnHidden();
        }

        private void ButtonActive(InventoryItemLayer layer)
        {
            _equipmentButton.gameObject.SetActive(false);
            switch (layer)
            {
                case InventoryItemLayer.Item:
                    break;
                case InventoryItemLayer.Equipment:
                    _equipmentButton.gameObject.SetActive(true);
                    break;
                case InventoryItemLayer.Weapon:
                    break;
            }
        }

        private Vector2 GetPosition(RectTransform item)
        {
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(
                null,
                item.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect,
                screenPosition,
                null,
                out Vector2 position);

            return position;
        }
    }
}