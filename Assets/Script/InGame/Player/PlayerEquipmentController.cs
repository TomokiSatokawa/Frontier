using System.Collections.Generic;
using InGame.Item;
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
        [SerializeField] private LayerMask _layerMask;

        private Dictionary<HumanBodyBones, GameObject> _instancedObject = new();

        public void SetEquipment(EquipmentData equipmentData)
        {
            var targetBone = _animator.GetBoneTransform(equipmentData.Bone);
            var _instancedObject = Instantiate(equipmentData.Prefab, targetBone);

            //Transformを設定
            _instancedObject.transform.localPosition = equipmentData.OffsetPosition;
            _instancedObject.transform.localRotation = Quaternion.Euler(equipmentData.OffsetRotation);
            _instancedObject.transform.localScale = Vector3.one * equipmentData.Scale;

            //レイヤーを設定
            _instancedObject.layer = Mathf.RoundToInt(Mathf.Log(_layerMask.value, 2));

            //プレイヤーのモーションを変化
            PlayerManager.Instance.SetMotionType(equipmentData.MotionType);
        }
    }
}