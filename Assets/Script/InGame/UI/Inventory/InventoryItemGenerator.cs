using System.Collections.Generic;
using InGame.Item;
using R3;
using UnityEngine;
using UnityEngine.Splines;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// Inventoryのアイテム生成
    /// </summary>
    public class InventoryItemGenerator : MonoBehaviour
    {
        [SerializeField] private List<ItemData> A;
        [SerializeField] private List<ItemData> B;
        [SerializeField] private List<ItemData> C;

        [SerializeField] private InventoryHeaderController _headerController;
        [SerializeField] private RectTransform _content;
        [SerializeField] private InventoryItemView _prefab;

        private readonly List<InventoryItemView> _clonedItemUI = new();
        private readonly List<InventoryItemView> _remove;

        private void Start()
        {
            _headerController.Layer.Subscribe(ShowItem);  
        }

        private void ShowItem(InventoryItemLayer layer)
        {
            var items = GetData(layer);
            GenerateItem(items);
        }

        //TODO:Playerからの取得に変更する
        private List<ItemData> GetData(InventoryItemLayer layer)
        {
            return layer switch
            {
                InventoryItemLayer.Item => A,
                InventoryItemLayer.Equipment => B,
                InventoryItemLayer.Weapon => C,
            };
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