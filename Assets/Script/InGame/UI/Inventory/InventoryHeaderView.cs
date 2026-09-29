using System;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// InventoryHeader‚ÌView
    /// </summary>
    public class InventoryHeaderView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InventoryItemLayer _layer;

        public event Action<InventoryItemLayer> OnClick;

        private void Start()
        {
            _button.onClick.AddListener(() => OnClick.Invoke(_layer));
        }

        public void SetSelect()
        {
            _button.onClick.Invoke();
        }
    }
}
