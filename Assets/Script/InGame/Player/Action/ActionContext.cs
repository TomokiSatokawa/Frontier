using InGame.Player.Motion;
using UnityEngine;

public class ActionContext
{
    public readonly PlayerAnimationPlayer AnimationPlayer;
    public readonly PlayerMovement Movement;
    public readonly GameObject PlayerObject;

    public ActionContext(PlayerAnimationPlayer animationPlayer,PlayerMovement movement, GameObject playerObject)
    {
        AnimationPlayer = animationPlayer;
        Movement = movement;
        PlayerObject = playerObject;
    }
}