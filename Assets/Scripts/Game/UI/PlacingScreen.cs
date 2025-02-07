using UnityEngine;

namespace Islanders.Game.UI
{
    public class PlacingScreen : MonoBehaviour, IScreen
    {
        #region Variables

        [SerializeField] private GameObject _content;

        public Transform ContentTransform => _content.transform;

        #endregion

        #region IScreen

        public void Show()
        {
            _content.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _content.gameObject.SetActive(false);
        }

        #endregion
    }
}