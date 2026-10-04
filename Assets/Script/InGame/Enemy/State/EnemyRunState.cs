using UnityEngine;

namespace InGame.Enemy
{
    [System.Serializable]
    public class EnemyRunState : EnemyStateBase
    {
        protected override void OnEnter(EnemyStateContext context)
        {
            context.Animation.SetMoveAmount(2);
        }

        protected override void OnExit(EnemyStateContext context)
        {
            context.Agent.ResetPath();
        }

        protected override void OnUpdate(EnemyStateContext context)
        {
            context.Agent.SetDestination(context.Target.position);
        }
    }
}
