using InGame.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// InventoryItem‚ÌView
    /// </summary>
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        private ItemData _data;
        public void SetData(ItemData data)
        {
            _data = data;

            _text.text = data.Definition.Name;
        }
    }
}