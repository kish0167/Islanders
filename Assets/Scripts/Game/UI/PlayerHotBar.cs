using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI
{
    public class PlayerHotBar : MonoBehaviour, IScreen
    {
        #region Variables

        [SerializeField] private HorizontalLayoutGroup _bar;
        [SerializeField] private Image _selectorWindow;

        private Player.Player _player;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
            _player.OnInventoryUpdated += InventoryUpdatedCallback;
        }

        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            _player.OnInventoryUpdated += InventoryUpdatedCallback;
        }

        private void OnDisable()
        {
            _player.OnInventoryUpdated -= InventoryUpdatedCallback;
        }

        #endregion

        #region IScreen

        public void Show()
        {
            _selectorWindow.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _selectorWindow.gameObject.SetActive(false);
        }

        #endregion

        #region Private methods

        private void InventoryUpdatedCallback(Dictionary<PlaceableObject, int> inventory, PlaceableObject selected)
        {
            // TODO
        }

        #endregion
    }
}