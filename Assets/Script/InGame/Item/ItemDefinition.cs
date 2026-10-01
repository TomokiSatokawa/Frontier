using System.Collections.Generic;
using InGame.UI.Inventory;
using UnityEngine;

namespace InGame.Item
{
    /// <summary>
    /// アイテムの固定値を定義するデータ
    /// </summary>
    // TODO: 現在のInspector管理からCSVのMasterData管理に変更する
    [System.Serializable]
    public class ItemDefinition
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private int _maxStack;
        [SerializeField] private Sprite _image;
        [SerializeField] private InventoryItemLayer _layer;
        [SerializeReference, SubclassSelector] private List<ItemDataModuleBase> _modules = new();

        public int ID => _id;
        public string Name => _name;
        public int MaxStack => _maxStack;
        public Sprite Image => _image; //TODO:CSVにする場合、ID検討
        public InventoryItemLayer Layer => _layer;
        public IReadOnlyList<ItemDataModuleBase> Modules => _modules;

        public override string ToString()
        {
            return $"ID: {_id}, Name: {_name}, MaxStack: {_maxStack}, Image: {_image}";
        }
    }
}