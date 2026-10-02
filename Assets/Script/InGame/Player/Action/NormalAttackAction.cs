using Common;
using InGame.Player.Actions;
using InGame.Player.Motion;
using UnityEngine;

[System.Serializable]
public class NormalAttackAction : PlayerActionBase
{
    [SerializeReference] private EnumGroupBase _attackMotion;

    private IReadOnlyAnimationPlaybackState _attackAnimationState;
    protected override void OnStart(ActionContext actionContext)
    {
        _attackAnimationState =  actionContext.AnimationPlayer.PlayOneShot(_attackMotion);
        actionContext.Movement.IgnoreInput = true;
    }

    protected override void OnUpdate(ActionContext actionContext)
    {
        //アニメーションが終了
        if (!_attackAnimationState.IsPlaying)
        {
            actionContext.Movement.IgnoreInput = false;
            RequestEnd();
        }
    }
}
