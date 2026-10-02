using Common;
using InGame.Player.Motion;
using UnityEngine;

namespace InGame.Item
{
    [System.Serializable]
    public class EquipmentData : ItemDataModuleBase
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private MotionType _motionType;
        [SerializeField] private HumanBodyBones _bone;
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private Vector3 _offsetRotation;
        [SerializeField] private float _scale;
        [SerializeReference] private EnumGroupBase _equipmentEmote;

        public GameObject Prefab => _prefab;
        public MotionType MotionType => _motionType;
        public HumanBodyBones Bone => _bone;
        public Vector3 OffsetPosition => _offsetPosition;
        public Vector3 OffsetRotation => _offsetRotation;
        public float Scale => _scale;
        public EnumGroupBase EquipmentEmote => _equipmentEmote;
    }
}