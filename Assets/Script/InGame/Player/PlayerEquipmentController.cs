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
            //同じボーンに生成済みの場合
            if(_instancedObject.TryGetValue(equipmentData.Bone,out var clonedObject) || clonedObject != null)
            {
                //装備外す処理
                Debug.Log("装備済み");
                return;
            }

            var targetBone = _animator.GetBoneTransform(equipmentData.Bone);
            var instanced = Instantiate(equipmentData.Prefab, targetBone);

            //Transformを設定
            instanced.transform.localPosition = equipmentData.OffsetPosition;
            instanced.transform.localRotation = Quaternion.Euler(equipmentData.OffsetRotation);
            instanced.transform.localScale = Vector3.one * equipmentData.Scale;

            //レイヤーを設定
            instanced.layer = Mathf.RoundToInt(Mathf.Log(_layerMask.value, 2));

            //生成済み武器に追加
            _instancedObject[equipmentData.Bone] = instanced;

            //プレイヤーのモーションを変化
            PlayerManager.Instance.SetMotionType(equipmentData.MotionType);
        }
    }
}