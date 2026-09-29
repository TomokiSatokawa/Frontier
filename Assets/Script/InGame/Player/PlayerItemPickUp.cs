using InGame.Item;
using PlayerInput;
using R3;
using UnityEngine;

namespace InGame.Player
{
    /// <summary>
    /// Itemを表示、取得する
    /// </summary>
    public class PlayerItemPickUp : MonoBehaviour
    {
        [SerializeField] private float _sightAngle;
        [SerializeField] private float _pickUpRange;
        [SerializeField] private LayerMask _itemLyreMask;

        /// <summary> Hit対象オブジェクト </summary>
        private readonly Collider[] _hitCollider = new Collider[32];
        /// <summary> PickUp対象 </summary>
        private ICollectable _hitCollectable;

        void Start()
        {
            InputManager.Interact.Where(x => x).Subscribe(_ => OnPickUp());
        }

        void Update()
        {
            var count = Physics.OverlapSphereNonAlloc(this.transform.position, _pickUpRange, _hitCollider, _itemLyreMask);

            //最も近いアイテムを取得する
            GameObject index = null;
            float minDistance = float.MaxValue;
            for (int i = 0; i < count; i++)
            {
                var target = _hitCollider[i].transform.root.gameObject;
                var sqr = (this.transform.position - target.transform.position).sqrMagnitude;

                //距離判定、視界判定
                if (sqr < minDistance && IsVisible(target.transform))
                {
                    minDistance = sqr;
                    index = target;
                }
            }

            //ICollectableを取得
            index?.TryGetComponent(out _hitCollectable);
        }

        public void OnPickUp()
        {
            if (_hitCollectable == null) return;

            _hitCollectable.CollectDelete();
        }

        private bool IsVisible(Transform target)
        {
            // ターゲットまでの向きと距離計算
            var targetDirection = target.position - this.transform.position;
            var targetDistance = targetDirection.magnitude;

            // cos(θ/2)を計算
            var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);

            //内積取得
            var innerProduct = Vector3.Dot(this.transform.forward, targetDirection.normalized);

            //判定
            return innerProduct > cosHalf;
        }
    }
}