using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace InGame.Player.Motion
{
    [CreateAssetMenu(fileName = "AnimationContainer", menuName = "Scriptable Objects/AnimationContainer")]
    public class AnimationContainer : ScriptableObject
    {
        [SerializeField] private AnimationData<BasicAnimationType>[] _basicClip;
        [SerializeField] private AnimationData<SwordAnimationType>[] _swordClip;

        private Dictionary<EnumID, IReadOnlyAnimationData> _animationDictionary = new();
        public IReadOnlyList<IReadOnlyAnimationData> AnimationList => _animationDictionary.Values.ToList();

        public void Initialize()
        {
            AddDictionary(new BasicMotion(), _basicClip);
            AddDictionary(new SwordMotion(), _swordClip);
        }

        public IReadOnlyAnimationData GetAnimation(EnumGroupBase enumGroup)
        {
            return GetAnimation(enumGroup.Create());
        }

        public IReadOnlyAnimationData GetAnimation(EnumID id)
        {
            if (_animationDictionary.TryGetValue(id, out var result))
            {
                return result;
            }

            Debug.LogError(id.ToString() + " is not found");
            return null;
        }

        private void AddDictionary<T>(EnumGroupBase<T> group, AnimationData<T>[] clips) where T : Enum
        {
            foreach (var clipData in clips)
            {
                EnumID id = EnumID.Create(group, clipData.Name);
                _animationDictionary.Add(id, clipData);
            }
        }

        public interface IReadOnlyAnimationData
        {
            public AnimationClip Clip { get; }
            public BlendData Blend { get; }
        }

        [System.Serializable]
        public class AnimationData<T> : IReadOnlyAnimationData where T : Enum
        {
            [SerializeField] private T _name;
            [SerializeField] private AnimationClip _clip;
            [SerializeField] private BlendData _blendData;

            public T Name => _name;
            public AnimationClip Clip => _clip;
            public BlendData Blend => _blendData;


        }
    }

    [System.Serializable]
    public struct BlendData
    {
        public float Duration;
        public AnimationCurve Curve;
    }

    public enum BasicAnimationType : byte
    {
        Idol, Walk, Run
    }

    public enum SwordAnimationType : byte
    {
        Idol, Walk, Run, Attack
    }
}