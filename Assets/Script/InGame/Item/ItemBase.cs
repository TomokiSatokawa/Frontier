using UnityEngine;

namespace InGame.Item
{

    public class ItemBase : MonoBehaviour, ICollectable
    {
        [SerializeField] private Transform _pickUpPosition;
        [SerializeField] private ItemDefinition _itemData;

        public Vector3 GetPickUpPosition()
        {
            return _pickUpPosition.position;
        }

        public ItemData CollectDelete()
        {
            Destroy(this.gameObject);
            return new(_itemData);
        }
    }
}