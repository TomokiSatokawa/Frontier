using Common;
using InGame.Player.Action;
using UnityEngine;

[System.Serializable]
public class NormalAttackAction : PlayerActionBase
{
    [SerializeReference] private EnumGroupBase _attackMotion;
    protected override void OnStart(ActionContext actionContext)
    {
        actionContext.AnimationPlayer.PlayOneShot(_attackMotion);
    }
}
