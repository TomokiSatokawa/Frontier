using Common.Motion;
using R3;
using UnityEngine;

namespace InGame.Player.Motion
{
    public class PlayerAnimationPlayer : AnimationPlayer
    {
        [SerializeField] private PlayerMovement _playerMovement;

        void Start()
        {
            PlayerManager.Instance.Type.Subscribe(UpdateBaseClip);
        }

        private void Update()
        {
            Tick(_playerMovement.MoveAmount);
        }
    }
}