using InGame.Player;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Item
{
    /// <summary>
    /// 拾えるアイテムにUIで表示
    /// </summary>
    public class PickupMarkerController : MonoBehaviour
    {
        [SerializeField] private PlayerItemPickUp _playerItemPickUp;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private RectTransform _mainCanvas;
        [SerializeField] private Image _image;

        private void LateUpdate()
        {
            if (_playerItemPickUp.HitCollectable == null)
            {
                _image.gameObject.SetActive(false);
                return;
            }
            _image.gameObject.SetActive(true);

            Vector3 worldPosition = _playerItemPickUp.HitCollectable.GetPickUpPosition();
            Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _mainCanvas,
                screenPosition,
                null,
                out var localPosition);

            _image.rectTransform.anchoredPosition = localPosition;
        }
    }
}