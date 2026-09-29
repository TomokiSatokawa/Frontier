using UnityEngine;

namespace InGame.Item
{

    public class ItemBase : MonoBehaviour, ICollectable
    {
        [SerializeField] private Transform _pickUpPosition;

        public Vector3 GetPickUpPosition()
        {
            return _pickUpPosition.position;
        }
    }
}