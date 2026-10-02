using UnityEngine;

namespace InGame.Item
{
    /// <summary>
    /// アイテムデータモジュールのベースクラス
    /// </summary>
    [System.Serializable]
    public abstract class ItemDataModuleBase
    {
        public ItemData Owner {  get; set; }
        
        public void SetOwner(ItemData owner)
        {
            Owner = owner;
        }
    }
}