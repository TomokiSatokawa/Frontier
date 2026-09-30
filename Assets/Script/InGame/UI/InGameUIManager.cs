using Common.UI;
using InGame.UI.Inventory;
using PlayerInput;
using R3;
using UnityEngine;

namespace InGame.UI
{
    /// <summary>
    /// InGameÇÃUIÇä«óù
    /// </summary>
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private PanelControl _inventoryPanel;
        [SerializeField] private InventoryItemGenerator _inventoryItemGenerator;

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

            }
            else
            {
                _inventoryPanel.OnHidden();

            }
        }
    }
}