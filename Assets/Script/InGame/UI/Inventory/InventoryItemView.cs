using System;
using InGame.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// InventoryItem‚ÌView
    /// </summary>
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public RectTransform Rect => _rect;

        private ItemData _data;
        public ItemData Data => _data;

        private Action<InventoryItemView> _onSelect;

        private void Start()
        {
            _selectButton.onClick.AddListener(OnClick);
        }

        public void SetData(ItemData data,Action<InventoryItemView> onSelect)
        {
            _data = data;
            _onSelect = onSelect;
            _text.text = data.Definition.Name;
        }

        public void OnClick()
        {
            _onSelect.Invoke(this);
        }
    }
}