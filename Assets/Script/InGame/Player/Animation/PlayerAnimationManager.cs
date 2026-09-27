using Common;
using UnityEngine;

namespace InGame.Player.Motion
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Animator _animator;

        private readonly EnumID _walk = EnumGroup.BasicMotion.Create(BasicAnimationType.Walk);
        private readonly EnumID _idol = EnumGroup.BasicMotion.Create(BasicAnimationType.Idol);

        // Update is called once per frame
        void Update()
        {

        }
    }
}