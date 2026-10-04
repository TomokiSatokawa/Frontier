using UnityEngine;

namespace InGame.Enemy
{
    [System.Serializable]
    public class EnemyRunState : EnemyStateBase
    {
        protected override void OnEnter(EnemyStateContext context)
        {
            Debug.Log("Run");
            context.Animation.SetMoveAmount(2);
        }

        protected override void OnExit(EnemyStateContext context)
        {

        }

        protected override void OnUpdate(EnemyStateContext context)
        {

        }
    }
}
