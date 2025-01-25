using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBar : MonoBehaviour
    {
        #region Variables

        [Header("Buttons")]
        [SerializeField] private List<HotBarButton> _buildingsButtons;
        [SerializeField] private HotBarButton _leftArrow;
        [SerializeField] private HotBarButton _rightArrow;
        [SerializeField] private HotBarButton _newBuildingButton;
        
        private Dictionary<PlaceableObject, int> _allBuildings;
        private Player.Player _player;
        private HotBarButtonFactory _buttonFactory;
        private int _overflowPointer;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(Player.Player player, HotBarButtonFactory factory)
        {
            _player = player;
            _player.OnInventoryUpdated += InventoryUpdatedCallback;

            _buttonFactory = factory;
            
            _buttonFactory.Setup(_leftArrow);
            _buttonFactory.Setup(_rightArrow);
            _buttonFactory.Setup(_newBuildingButton);

            foreach (HotBarButton button in _buildingsButtons)
            {
                _buttonFactory.Setup(button);
            }
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
                UpdateNotFullBar();
            }
            else
            {
                UpdateOverflowedBar();
            }
            
            
        }

        private void UpdateOverflowedBar()
        {
            _leftArrow.gameObject.SetActive(true);
            _rightArrow.gameObject.SetActive(true);

            foreach (HotBarButton button in _buildingsButtons)
            {
                button.gameObject.SetActive(true);
            }
            
            
        }

        private void UpdateNotFullBar()
        {
            _leftArrow.gameObject.SetActive(false);
            _rightArrow.gameObject.SetActive(false);

            for (int i = 0; i < _allBuildings.Count; i++)
            {
                _buildingsButtons[i].gameObject.SetActive(true);
            }
                
            for (int i = _allBuildings.Count; i < _buildingsButtons.Count; i++)
            {
                _buildingsButtons[i].gameObject.SetActive(false);
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