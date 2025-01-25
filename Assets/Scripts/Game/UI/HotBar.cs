using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI
{
    public class HotBar : MonoBehaviour
    {
        #region Variables

        [Header("Buttons")]
        [SerializeField] private List<Button> _buildingsButtons;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _newBuildingButton;
        private Dictionary<PlaceableObject, int> _allBuildings;

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

        #region Private methods

        private void InventoryUpdatedCallback(Dictionary<PlaceableObject, int> inventory, PlaceableObject selected)
        {
            _allBuildings = inventory;

            if (inventory.Count <= _buildingsButtons.Count)
            {
                _leftArrow.onClick.RemoveAllListeners();
                _rightArrow.onClick.RemoveAllListeners();
                _leftArrow.gameObject.SetActive(false);
                _rightArrow.gameObject.SetActive(false);
            }
            else
            {
                _leftArrow.onClick.RemoveAllListeners();
                _rightArrow.onClick.RemoveAllListeners();
                _rightArrow.onClick.AddListener(RightArrowPressedCallback);
                _leftArrow.onClick.AddListener(LeftArrowPressedCallback);
                _leftArrow.gameObject.SetActive(true);
                _rightArrow.gameObject.SetActive(true);
            }
        }

        private void LeftArrowPressedCallback()
        {
            throw new System.NotImplementedException();
        }

        private void RightArrowPressedCallback()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}