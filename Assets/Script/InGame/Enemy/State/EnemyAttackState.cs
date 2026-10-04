using Common;
using UnityEngine;

namespace InGame.Enemy
{
    [System.Serializable]
    public class EnemyAttackState : EnemyStateBase
    {
        [SerializeReference] private EnumGroupBase _attackMotion;
        [SerializeField] private float _attackInterval;

        private float _nextAttackTime;
        protected override void OnEnter(EnemyStateContext context)
        {
            context.Animation.SetMoveAmount(0f);

            Debug.Log("AttackState");
            _nextAttackTime = Time.time;
        }

        protected override void OnExit(EnemyStateContext context)
        {
            Debug.Log("AttackExit");
            _nextAttackTime = float.MinValue;
        }

        protected override void OnUpdate(EnemyStateContext context)
        {
            if (_nextAttackTime <= Time.time)
            {
                Debug.Log("Attack");
                context.Animation.PlayOneShot(_attackMotion);
                _nextAttackTime += _attackInterval;
            }
        }
    }
}
