using System.Collections.Generic;
using InGame.Item;
using InGame.Player.Actions;
using InGame.Player.Motion;
using UnityEngine;

namespace InGame.Player
{
    /// <summary>
    /// プレイヤーの装備品管理
    /// </summary>

    public class PlayerEquipmentController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerAnimationPlayer _animationPlayer;
        [SerializeField] private PlayerActionHandler _actionHandler;
        [SerializeField] private EquipmentData _equipmentDatas;
        [SerializeField] private LayerMask _layerMask;

        private Dictionary<HumanBodyBones, GameObject> _instancedObject = new();
        private Dictionary<GameObject, EquipmentData> _instancedEquipmentData = new();

        public void SetEquipment(EquipmentData equipmentData)
        {
            //同じボーンに生成済みの場合
            if(_instancedObject.TryGetValue(equipmentData.Bone,out var clonedObject) || clonedObject != null)
            {
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
            _instancedEquipmentData[instanced] = equipmentData;

            //プレイヤーのモーションを変化
            PlayerManager.Instance.SetMotionType(equipmentData.MotionType);

            //装備モーションを再生
            _animationPlayer.PlayOneShot(equipmentData.EquipmentEmote);

            //武器専用アクションを追加
            _actionHandler.AddAction(equipmentData.Actions); 
        }

        public EquipmentData TryUnequipEquipment(EquipmentData overrideData)
        {
            // 生成済み装備が存在しない
            if (!_instancedObject.TryGetValue(overrideData.Bone, out var clonedObject) || clonedObject == null)
            {
                return null;
            }

            if (!_instancedEquipmentData.TryGetValue(clonedObject, out var equipmentData))
            {
                Debug.LogError($"[{nameof(PlayerEquipmentController)}] 生成済みオブジェクト '{clonedObject.name}' に対応する EquipmentData が存在しません。");
                return null;
            }

            return equipmentData;
        }

        public void UnequipEquipment(EquipmentData equipmentData)
        {
            // 生成済み装備が存在しない
            if (!_instancedObject.TryGetValue(equipmentData.Bone, out var clonedObject) || clonedObject == null)
            {
                Debug.LogError($"[{nameof(PlayerEquipmentController)}] Bone '{equipmentData.Bone}' に装備されているオブジェクトが存在しません。");
                return;
            }

            _instancedObject.Remove(equipmentData.Bone);

            if (!_instancedEquipmentData.Remove(clonedObject))
            {
                Debug.LogError($"[{nameof(PlayerEquipmentController)}] 生成済みオブジェクトに対応する EquipmentData が見つかりません。");
            }

            //武器専用アクションを外す
            _actionHandler.RemoveAction(equipmentData.Actions);

            Destroy(clonedObject);
        }
    }
}