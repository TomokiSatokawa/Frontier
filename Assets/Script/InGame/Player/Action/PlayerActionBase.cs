using UnityEngine;

namespace InGame.Player.Action
{
    /// <summary>
    /// プレイヤーアクションの基底クラス
    /// </summary>
    [System.Serializable]
    public abstract class PlayerActionBase
    {
        [SerializeField] private ActionKeyType _actionKey;
        public ActionKeyType ActionKey => _actionKey;
        public ActionState State { get; private set; } = ActionState.Idle;

        protected float _elapsedTime { get; private set; } = 0f;
        /// <summary>
        /// アクションを開始可能か判定
        /// </summary>
        public virtual bool CanStart(ActionContext actionContext)
        {
            return true;
        }

        /// <summary>
        /// アクション開始時に呼び出し
        /// </summary>
        public void StartAction(ActionContext actionContext)
        {
            State = ActionState.Active;
            _elapsedTime = 0f;
            OnStart(actionContext);
        }

        protected abstract void OnStart(ActionContext actionContext);

        /// <summary>
        /// アクション実行中に呼び出し
        /// </summary>
        public void UpdateAction(ActionContext actionContext)
        {
            if(State != ActionState.Active)
            {
                Debug.LogError("[PlayerActionBase] 開始されていないActionがUpdateされました");
                return;
            }
            _elapsedTime += Time.deltaTime;
            OnUpdate(actionContext);
        }

        protected virtual void OnUpdate(ActionContext actionContext) { }

        /// <summary>
        /// アクションの終了を要求
        /// </summary>
        protected void RequestEnd()
        {
            State = ActionState.End;
            _elapsedTime = 0f;
        }

        public enum ActionState
        {
            Idle,Active,End
        }
    }
}