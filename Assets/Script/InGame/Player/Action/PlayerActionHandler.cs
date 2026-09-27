using InGame.Player.Motion;
using PlayerInput;
using R3;
using UnityEngine;

namespace InGame.Player.Action
{
    /// <summary>
    /// プレイヤーのアクションを実行
    /// </summary>
    public class PlayerActionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationPlayer _animationPlayer;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeReference, SubclassSelector] private PlayerActionBase[] _actions;

        private PlayerActionBase _currentAction;
        private ActionContext _actionContext;

        private void Start()
        {
            _actionContext = new(_animationPlayer, _playerMovement, this.gameObject);
            InputManager.Attack.Where(x => x).Subscribe(_ => OnActionKeyClick(ActionKeyType.Attack));//.AddTo(this);
        }

        public void Update()
        {
            if (_currentAction == null) return;

            //Actionが終了している
            if (_currentAction.State == PlayerActionBase.ActionState.End)
            {
                _currentAction.EndAction();
                _currentAction = null;
                return;
            }
            _currentAction.UpdateAction(_actionContext);
        }

        private void OnActionKeyClick(ActionKeyType type)
        {
            foreach (var action in _actions)
            {
                //実行条件が揃った
                if (action.ActionKey == type && action.CanStart(_actionContext))
                {
                    _currentAction = action;
                    _currentAction.StartAction(_actionContext);
                    return;
                }
            }
        }
    }

    public enum ActionKeyType
    {
        Attack,
    }

}