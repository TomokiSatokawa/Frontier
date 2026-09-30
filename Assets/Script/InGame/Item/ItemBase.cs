using UnityEngine;

namespace InGame.Item
{

    public class ItemBase : MonoBehaviour, ICollectable
    {
        [SerializeField] private Transform _pickUpPosition;
        [SerializeField] private ItemDefinition _itemData;

        public Vector3 GetPickUpPosition()
        {
            if (this == null)
                return Vector3.zero;

            if (_pickUpPosition == null)
                return transform.position;

            return _pickUpPosition.position;
        }

        public ItemData CollectDelete()
        {
            Destroy(this.gameObject);
            return new(_itemData);
        }
    }
}