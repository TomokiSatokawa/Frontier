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
        [SerializeReference, SubclassSelector] private EnemyStateCondition[] _startConditions;
        [SerializeReference, SubclassSelector] private EnemyStateCondition[] _exitConditions;
        [SerializeField] private bool _isStart;

        protected float _stateTime;

        public bool IsStart => _isStart;

        /// <summary>
        /// ステートに移れるか確認する
        /// </summary>
        public bool IsStartCondition(EnemyStateContext context)
        {
            foreach (var condition in _startConditions)
            {
                if (!condition.IsSatisfied(context))
                    return false;
            }

            return true;
        }

        public bool IsExitCondition(EnemyStateContext context)
        {
            foreach (var condition in _exitConditions)
            {
                if (!condition.IsSatisfied(context))
                    return false;
            }

            return true;
        }

        public void EnterState(EnemyStateContext context)
        {
            OnEnter(context);
        }

        public void UpdateState(EnemyStateContext context)
        {
            OnUpdate(context);
        }

        public void ExitState(EnemyStateContext context)
        {
            OnExit(context);
        }

        protected abstract void OnEnter(EnemyStateContext context);
        protected abstract void OnUpdate(EnemyStateContext context);
        protected abstract void OnExit(EnemyStateContext context);
    }

    public struct EnemyStateContext
    {
        private EnemyAnimationPlayer _animation;
        private Transform _target;
        private Transform _origin;

        public EnemyAnimationPlayer Animation => _animation;
        public Transform Target => _target;
        public Transform Origin => _origin;

        public EnemyStateContext(EnemyAnimationPlayer animation, Transform target,Transform origin)
        {
            _animation = animation;
            _target = target;
            _origin = origin;
        }
    }
}