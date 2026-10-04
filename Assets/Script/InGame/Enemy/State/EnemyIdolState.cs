using UnityEngine;

namespace InGame.Enemy
{
    [System.Serializable]
    public class EnemyIdolState : EnemyStateBase
    {
        protected override void OnEnter(EnemyStateContext context)
        {
            Debug.Log("Idol");
            context.Animation.SetMoveAmount(0);
        }

        protected override void OnExit(EnemyStateContext context)
        {

        }

        protected override void OnUpdate(EnemyStateContext context)
        {

        }
    }
}
