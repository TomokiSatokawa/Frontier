using UnityEngine;

namespace InGame.UI.Inventory
{
    /// <summary>
    /// Inventory横のキャラプレビュー
    /// </summary>
    public class CharacterPreview : MonoBehaviour
    {
        [SerializeField] private Camera _previewCamera;

        public void SetVisible(bool visible)
        {
            _previewCamera.gameObject.SetActive(visible);
        }
    }
}