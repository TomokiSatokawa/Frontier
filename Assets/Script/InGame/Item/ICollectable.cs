using UnityEngine;

namespace InGame.Item
{

    public interface ICollectable
    {
        public Vector3 GetPickUpPosition();
        public ItemData CollectDelete();
    }
}