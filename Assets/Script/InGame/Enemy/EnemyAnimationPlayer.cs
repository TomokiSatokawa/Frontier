using Common.Motion;
using UnityEngine;

namespace InGame.Enemy
{

    public class EnemyAnimationPlayer : AnimationPlayer
    {
        private float _moveAmount;

        private void Update()
        {
            Tick(_moveAmount);
        }

        public void SetMoveAmount(float moveAmount)
        {
            _moveAmount = moveAmount;
        }
    }
}
