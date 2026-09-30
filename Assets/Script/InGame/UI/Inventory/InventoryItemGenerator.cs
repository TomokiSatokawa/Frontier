using System.Collections.Generic;
using InGame.Item;
using InGame.Player;
using R3;
using UnityEngine;
using UnityEngine.Splines;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// InventoryÇÃÉAÉCÉeÉÄê∂ê¨
    /// </summary>
    public class InventoryItemGenerator : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryManager _inventoryManager;
        [SerializeField] private InventoryHeaderController _headerController;
        [SerializeField] private RectTransform _content;
        [SerializeField] private InventoryItemView _prefab;

        private readonly List<InventoryItemView> _clonedItemUI = new();

        private void Start()
        {
            _headerController.Layer.Subscribe(ShowItem);  
        }

        public void OnDefault()
        {
            _headerController.OnSelect(InventoryItemLayer.Item);
        }

        private void ShowItem(InventoryItemLayer layer)
        {
            var items = _inventoryManager.GetItem(layer);
            GenerateItem(items);
        }

        public void GenerateItem(List<ItemData> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                ItemData item = data[i];

                if(i < _clonedItemUI.Count)
                {
                    _clonedItemUI[i].SetData(item);
                    continue;
                }

                var itemUI = Instantiate(_prefab, _content);
                itemUI.SetData(item);
                _clonedItemUI.Add(itemUI);
            }

            for (int i = _clonedItemUI.Count - 1; i >= data.Count; i--)
            {
                Destroy(_clonedItemUI[i].gameObject);
                _clonedItemUI.RemoveAt(i);
            }
        }
    }
}