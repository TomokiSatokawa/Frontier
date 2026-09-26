using UnityEngine;

namespace InGame.Player
{
    /// <summary>
    /// プレイヤーの装備品管理
    /// </summary>

    public class PlayerEquipmentController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EquipmentData _equipmentDatas;

        private EquipmentData _currentData;

        [System.Serializable]
        public class EquipmentData
        {
            [SerializeField] private GameObject _prefab;
            [SerializeField] private MotionType _motionType;
            [SerializeField] private HumanBodyBones _bones;
            [SerializeField] private Vector3 _offsetPosition;
            [SerializeField] private Vector3 _offsetRotation;
            [SerializeField] private float _scale;
            public MotionType MotionType => _motionType;

            private GameObject _instancedObject;
            public void SetEquipment(Animator animator)
            {
                var targetBone = animator.GetBoneTransform(_bones);
                _instancedObject = Instantiate(_prefab, targetBone);
                _instancedObject.transform.localPosition = _offsetPosition;
                _instancedObject.transform.localRotation = Quaternion.Euler(_offsetRotation);
                _instancedObject.transform.localScale = Vector3.one * _scale;
            }
        }

        public void Start()
        {
        
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetEquipment(_equipmentDatas);
            }
        }

        private void SetEquipment(EquipmentData equipmentData)
        {
            if (_currentData != null && equipmentData == _currentData)
                return;

            _currentData = equipmentData;
            equipmentData.SetEquipment(_animator);
            PlayerManager.Instance.SetMotionType(equipmentData.MotionType);
        }
    }
}