using Common.UI;
using InGame.Player.Motion;
using InGame.UI.Inventory;
using PlayerInput;
using R3;
using UnityEngine;

namespace InGame.UI
{
    /// <summary>
    /// InGameのUIを管理
    /// </summary>
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private PanelControl _inventoryPanel;
        [SerializeField] private PlayerAnimationPlayer _playerAnimationPlayer;
        [SerializeField] private InventoryItemGenerator _inventoryItemGenerator;
        [SerializeField] private ItemActionWindowControl _actionWindowControl;
        [SerializeField] private CharacterPreview _characterPreview;

        private void Start()
        {
            InputManager.Inventory.Subscribe(_ => SetInventoryVisible());
        }

        private void SetInventoryVisible()
        {
            bool isVisible = !_inventoryPanel.IsActive;

            if (isVisible)
            {
                _inventoryPanel.OnActive();
                _inventoryItemGenerator.OnDefault();
                PlayerManager.Instance.SetPlayerStatus(PlayerManager.PlayerStatus.UIOperation);

            }
            else
            {
                _inventoryPanel.OnHidden();

                //Inventory内で実行したアニメーションを停止
                _playerAnimationPlayer.StopPlayOneShot();
                PlayerManager.Instance.SetPlayerStatus(PlayerManager.PlayerStatus.Normal);
            }

            _actionWindowControl.OnHidden();
            _characterPreview.SetVisible(isVisible);
        }
    }
}