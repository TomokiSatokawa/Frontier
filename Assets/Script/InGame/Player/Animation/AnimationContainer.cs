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

        private Dictionary<EnumID, AnimationClip> _animationDictionary = new();
        public IReadOnlyList<AnimationClip> AnimationList => _animationDictionary.Values.ToList();

        public void Initialize()
        {
            AddDictionary(new BasicMotion(), _basicClip);
            AddDictionary(new SwordMotion(), _swordClip);
        }

        public AnimationClip GetAnimation(EnumGroupBase enumGroup)
        {
            return GetAnimation(enumGroup.Create());
        }

        public AnimationClip GetAnimation(EnumID id)
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
                _animationDictionary.Add(id, clipData.Clip);
            }
        }

        [System.Serializable]
        public class AnimationData<T> where T : Enum
        {
            public T Name;
            public AnimationClip Clip;
            public BlendData BlendData;
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

    public enum SwordAnimationType: byte
    {
        Idol, Walk, Run,Attack
    }
}