using UnityEngine;

namespace InGame.Enemy
{
    [System.Serializable]
    public class PlayerDistanceCondition : EnemyStateCondition
    {
        [SerializeField] private float _distance;
        [SerializeField] private ComparisonType _comparisonType;

        public override bool IsSatisfied(EnemyStateContext context)
        {
            var sqrDistance = (context.Target.position - context.Origin.position).sqrMagnitude;
            var sqrThreshold = _distance * _distance;

            switch (_comparisonType)
            {
                case ComparisonType.LessThan:
                    return sqrDistance < sqrThreshold;

                case ComparisonType.LessThanOrEqual:
                    return sqrDistance <= sqrThreshold;

                case ComparisonType.GreaterThan:
                    return sqrDistance > sqrThreshold;

                case ComparisonType.GreaterThanOrEqual:
                    return sqrDistance >= sqrThreshold;

                default:
                    return false;
            }
        }

        public enum ComparisonType
        {
            LessThan,         // ñ¢ñû
            LessThanOrEqual,  // à»â∫
            GreaterThan,      // ÇÊÇËëÂÇ´Ç¢
            GreaterThanOrEqual // à»è„
        }
    }
}
