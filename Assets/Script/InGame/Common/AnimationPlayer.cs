using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using InGame.Player.Motion;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Common.Motion
{
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationContainer _container;
        [SerializeField] private StateMotionData _stateMotionData;
        [SerializeField] private BaseAnimation _baseAnimation;
        [SerializeField] private MotionType _startMotionType;

        private CancellationTokenSource _topLayerToken;
        private AnimationPlaybackState _currentPlayOneShot;
        private enum AnimationLayer
        {
            Base, TopLayer
        }

        private PlayableGraph _graph;
        private AnimationLayerMixerPlayable _layerMixer;
        private AnimationMixerPlayable _baseMixer;

        private void Start()
        {
            _container.Initialize();

            _graph = PlayableGraph.Create("PlayerAnimation");

            //レイヤーミキサー
            _layerMixer = AnimationLayerMixerPlayable.Create(_graph, Enum.GetNames(typeof(AnimationLayer)).Length);

            //OutPut処理
            var output = AnimationPlayableOutput.Create(_graph, "AnimationOutput", _animator);
            output.SetSourcePlayable(_layerMixer);

            //ベースミキサー
            _baseMixer = AnimationMixerPlayable.Create(_graph, 3);

            UpdateBaseClip(_startMotionType);

            _baseMixer.SetInputWeight(0, 1f);
            _baseMixer.SetInputWeight(1, 0f);

            //ベースミキサーをレイヤーに登録
            _layerMixer.ConnectInput((int)AnimationLayer.Base, _baseMixer, 0);
            _layerMixer.SetInputWeight((int)AnimationLayer.Base, 1f);

            _graph.Play();
        }

        private void OnDestroy()
        {
            _graph.Destroy();
            _topLayerToken?.Cancel();
            _topLayerToken?.Dispose();
        }

        public void Tick(float moveAmount)
        {
            _baseAnimation.Tick(_baseMixer, moveAmount);
        }

        public void UpdateBaseClip(MotionType type)
        {
            var group = _stateMotionData.GetMotionGroup(type);

            if (group == null)
                return;

            var idle = AnimationClipPlayable.Create(_graph, _container.GetAnimation(group.IdleClip).Clip);
            var walk = AnimationClipPlayable.Create(_graph, _container.GetAnimation(group.WalkClip).Clip);
            var run = AnimationClipPlayable.Create(_graph, _container.GetAnimation(group.RunClip).Clip);

            if (_baseMixer.GetInput(0).IsValid())
                _baseMixer.DisconnectInput(0);

            if (_baseMixer.GetInput(1).IsValid())
                _baseMixer.DisconnectInput(1);

            if (_baseMixer.GetInput(2).IsValid())
                _baseMixer.DisconnectInput(2);

            _baseMixer.ConnectInput(0, idle, 0);
            _baseMixer.ConnectInput(1, walk, 0);
            _baseMixer.ConnectInput(2, run, 0);
        }

        public IReadOnlyAnimationPlaybackState PlayOneShot(EnumGroupBase groupBase)
        {
            //既に実行中だった場合はキャンセル
            _topLayerToken?.Cancel();
            _topLayerToken?.Dispose();
            _topLayerToken = new CancellationTokenSource();
            var playbackState = new AnimationPlaybackState();
            PlayOneShotAsync(groupBase, playbackState).Forget();
            return playbackState;
        }

        public void StopPlayOneShot()
        {
            _currentPlayOneShot?.OnStop();
        }

        private async UniTask PlayOneShotAsync(EnumGroupBase groupBase, AnimationPlaybackState playbackState)
        {
            var animationData = _container.GetAnimation(groupBase);
            var playable = AnimationClipPlayable.Create(_graph, animationData.Clip);
            _currentPlayOneShot = playbackState;

            //単発クリップを専用レイヤーにセット
            TryDisconnect(_layerMixer, (int)AnimationLayer.TopLayer);
            _layerMixer.ConnectInput((int)AnimationLayer.TopLayer, playable, 0);
            _layerMixer.SetInputWeight((int)AnimationLayer.TopLayer, 0f);

            //Maskを設定
            var mask = _container.GetMask(animationData.AvatarMaskType);
            _layerMixer.SetLayerMaskFromAvatarMask((int)AnimationLayer.TopLayer, mask);

            float elapsedTime = 0f;
            float clipLength = animationData.Clip.length;
            float blendDuration = animationData.Blend.Duration;

            _currentPlayOneShot.StopAnimation += () =>
            {
                elapsedTime = clipLength - blendDuration;
            };

            try
            {
                //Animation中待機処理
                while (elapsedTime < clipLength)
                {
                    // 開始Blend
                    if (elapsedTime < blendDuration)
                    {
                        float normalizedTime = elapsedTime / blendDuration;
                        float curveValue = animationData.Blend.Curve.Evaluate(normalizedTime);

                        _layerMixer.SetInputWeight((int)AnimationLayer.TopLayer, curveValue);
                    }
                    // 終了Blend
                    else if (elapsedTime >= clipLength - blendDuration)
                    {
                        float normalizedTime = (elapsedTime - (clipLength - blendDuration)) / blendDuration;
                        float curveValue = animationData.Blend.Curve.Evaluate(normalizedTime);

                        _layerMixer.SetInputWeight((int)AnimationLayer.TopLayer, 1f - curveValue);
                    }
                    // Blendなし
                    else
                    {
                        _layerMixer.SetInputWeight((int)AnimationLayer.TopLayer, 1f);
                    }

                    await UniTask.Yield(cancellationToken: _topLayerToken.Token);
                    elapsedTime += Time.deltaTime;
                    playbackState.Time = elapsedTime;
                }
            }
            catch (OperationCanceledException)
            {

            }

            //元に戻す
            TryDisconnect(_layerMixer, (int)AnimationLayer.TopLayer);
            _layerMixer.SetInputWeight((int)AnimationLayer.TopLayer, 0f);
            playbackState.IsPlaying = false;
        }

        private void TryDisconnect(Playable playable, int inputPot)
        {
            if (playable.GetInput(inputPot).IsValid())
                playable.DisconnectInput(inputPot);
        }
    }

    [Serializable]
    public class BaseAnimation
    {
        [SerializeField] private BlendData _blendData;

        private float _blendPosition;
        private float _blendStart;
        private float _blendTarget;
        private float _blendTime;

        public void Tick(AnimationMixerPlayable baseMixer,float moveAmount)
        {
            var target = Mathf.Clamp(moveAmount, 0f, 2f);

            if (!Mathf.Approximately(target, _blendTarget))
            {
                _blendStart = _blendPosition;
                _blendTarget = target;
                _blendTime = 0f;
            }

            if (!Mathf.Approximately(_blendStart, _blendTarget))
            {
                _blendTime += Time.deltaTime;

                var t = Mathf.Clamp01(_blendTime / _blendData.Duration);
                t = _blendData.Curve.Evaluate(t);

                _blendPosition = Mathf.Lerp(
                    _blendStart,
                    _blendTarget,
                    t
                );
            }

            SetBlendWeight(baseMixer, _blendPosition);
        }

        private void SetBlendWeight(AnimationMixerPlayable baseMixer, float blendPosition)
        {
            var idleWeight = Mathf.Clamp01(1f - blendPosition);
            var walkWeight = 1f - Mathf.Abs(blendPosition - 1f);
            var runWeight = Mathf.Clamp01(blendPosition - 1f);

            baseMixer.SetInputWeight(0, idleWeight);
            baseMixer.SetInputWeight(1, walkWeight);
            baseMixer.SetInputWeight(2, runWeight);
        }
    }
}
