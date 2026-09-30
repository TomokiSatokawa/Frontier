using UnityEngine;

namespace InGame.Item
{

    public class ItemBase : MonoBehaviour, ICollectable
    {
        [SerializeField] private Transform _pickUpPosition;
        [SerializeField] private ItemDefinition _itemData;

        public Vector3 GetPickUpPosition()
        {
            if(_pickUpPosition == null)
                return this.transform.position;

            return _pickUpPosition.position;
        }

        public ItemData CollectDelete()
        {
            Destroy(this.gameObject);
            return new(_itemData);
        }
    }
}