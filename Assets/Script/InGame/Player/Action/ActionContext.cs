using InGame.Player.Motion;
using UnityEngine;

public class ActionContext
{
    public readonly PlayerAnimationPlayer AnimationPlayer;
    public readonly GameObject PlayerObject;

    public ActionContext(PlayerAnimationPlayer animationPlayer, GameObject playerObject)
    {
        AnimationPlayer = animationPlayer;
        PlayerObject = playerObject;
    }
}