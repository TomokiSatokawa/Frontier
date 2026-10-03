using Common.Motion;
using UnityEngine;

namespace InGame.Enemy
{
    /// <summary>
    /// 敵ステートのベース
    /// </summary>
    [System.Serializable]   
    public abstract class EnemyStateBase
    {
        public abstract void OnEnter(EnemyStateContext context);
        public abstract void OnUpdate(EnemyStateContext context);
        public abstract void OnExit(EnemyStateContext context);
    }

    public ref struct EnemyStateContext
    {
        private AnimationPlayer _animation;

        public AnimationPlayer Animation => _animation;

        public EnemyStateContext (AnimationPlayer animation)
        {
            _animation = animation;
        }
    }
}