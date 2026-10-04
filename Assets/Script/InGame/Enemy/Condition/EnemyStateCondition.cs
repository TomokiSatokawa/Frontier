using UnityEngine;

namespace InGame.Enemy
{
    /// <summary>
    /// State‚ğÀs‚·‚éğŒ
    /// </summary>
    [System.Serializable]
    public abstract class EnemyStateCondition
    {
        public abstract bool IsSatisfied(EnemyStateContext context);
    }
}