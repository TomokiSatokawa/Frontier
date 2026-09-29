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
        private int _id;
        private string _name;
        private int _maxStack;
        private Sprite _image;

        public int ID => _id;
        public string Name => _name;
        public int MaxStack => _maxStack;
        public Sprite Image => _image;//TODO:CSVにする場合、ID検討

        public override string ToString()
        {
            return $"ID: {_id}, Name: {_name}, MaxStack: {_maxStack}, Image: {_image}";
        }
    }
}