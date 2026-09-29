
namespace InGame.Player.Motion
{
    /// <summary>
    /// アニメーション再生中のデータ
    /// </summary>
    public class AnimationPlaybackState : IReadOnlyAnimationPlaybackState
    {
        public AnimationPlaybackState()
        {
            IsPlaying = true;
            Duration = 0f;
        }

        public bool IsPlaying { get; set; }
        public float Duration { get; set; }
    }

    public interface IReadOnlyAnimationPlaybackState
    {
        public bool IsPlaying { get; }
        public float Duration { get; }
    }
}