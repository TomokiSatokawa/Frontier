using System;
using System.Collections.Generic;
using InGame.Item;
using InGame.UI.Inventory;
using UnityEngine;

namespace InGame.Player
{
    /// <summary>
    /// Inventory管理
    /// </summary>
    public class PlayerInventoryManager : MonoBehaviour
    {
        [SerializeField] private PlayerEquipmentController _equipmentController;

        private readonly Dictionary<InventoryItemLayer, List<ItemData>> _inventoryDatas = new();

        private void Start()
        {
            foreach (InventoryItemLayer layer in Enum.GetValues(typeof(InventoryItemLayer)))
            {
                _inventoryDatas.Add(layer, new List<ItemData>());
            }
        }

        /// <summary>
        /// アイテムを追加
        /// </summary>
        public void AddItem(ItemData item)
        {
            //レイヤー内の同じアイテムを探す
            foreach (var data in _inventoryDatas[item.Definition.Layer])
            {
                //同じアイテムを探す
                if (data != item) continue;

                //カウントを追加できる
                if (data.Count < data.Definition.MaxStack)
                {
                    data.AddCount();
                    return;
                }
            }

            //アイテムを追加
            _inventoryDatas[item.Definition.Layer].Add(item);
        }

        /// <summary>
        /// 武器アイテムを装備
        /// </summary>
        public void EquipmentItem(ItemData item)
        {
            if (item.Definition.Layer != InventoryItemLayer.Equipment)
            {
                Debug.LogError($"[{nameof(PlayerEquipmentController)}] Cannot equip item because the item layer is not Equipment. Item: {item.Definition.Name}");
                return;
            }

            _equipmentController.SetEquipment(item.RequireModule<EquipmentData>());
        }

        public List<ItemData> GetItem(InventoryItemLayer layer)
        {
            return _inventoryDatas[layer];
        }
    }
}
