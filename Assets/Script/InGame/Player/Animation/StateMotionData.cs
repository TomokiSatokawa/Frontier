using System.Linq;
using Common;
using UnityEngine;

[CreateAssetMenu(fileName = "StateMotionData", menuName = "Scriptable Objects/StateMotionData")]
public class StateMotionData : ScriptableObject
{
    [SerializeField] private MotionGroup[] _motionGroups;

    [System.Serializable]
    public class MotionGroup
    {
        [SerializeField] private MotionType _motionType;
        [SerializeReference] private EnumGroupBase _idleClip;
        [SerializeReference] private EnumGroupBase _walkClip;
        [SerializeReference] private EnumGroupBase _runClip;

        public MotionType Type => _motionType;
        public EnumGroupBase IdleClip => _idleClip;
        public EnumGroupBase WalkClip => _walkClip;
        public EnumGroupBase RunClip => _runClip;
    }

    public MotionGroup GetMotionGroup(MotionType type)
    {
        return _motionGroups.FirstOrDefault(x => x.Type == type);
    }
}

public enum MotionType
{
    Free, Sword
}