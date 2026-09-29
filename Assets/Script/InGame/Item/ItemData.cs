using UnityEngine;

namespace InGame.Item
{
    /// <summary>
    /// 内部で使用するアイテムデータ
    /// </summary>
    public class ItemData
    {
        public ItemData(ItemDefinition definition)
        {
            if (definition == null)
            {
                Debug.LogError("[ItemData] ItemDefinition is null.");
                return;
            }

            Definition = definition;
        }

        public ItemDefinition Definition { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemData);
        }

        public override int GetHashCode()
        {
            return Definition.ID.GetHashCode();
        }

        public static bool operator ==(ItemData left, ItemData right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Definition.ID == right.Definition.ID;
        }

        public static bool operator !=(ItemData left, ItemData right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"ItemData: {Definition.Name} \n Definition : {Definition.ToString()}";
        }
    }
}